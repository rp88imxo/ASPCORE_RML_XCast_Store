using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RMLXCast.Core.Domain.Catalog;
using RMLXCast.Core.Domain.ShippmentAddress;
using RMLXCast.Core.Domain.User;
using RMLXCast.Services.AddressService;
using RMLXCast.Services.Catalog;
using RMLXCast.Services.Catalog.Category;
using RMLXCast.Services.Catalog.User;
using RMLXCast.Services.Orders.OrderService;
using RMLXCast.Web.Services.Cart;
using RMLXCast.Web.ViewModels;
using RMLXCast.Web.ViewModels.Checkout;
using RMLXCast.Web.ViewModelsFactories.CheckoutFactory;
using RMLXCast.Web.ViewModelsFactories.ShopProducts;
using System.Diagnostics;

namespace RMLXCast.Web.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductCategoryService productCategoryService;
        private readonly IProductService productService;
        private readonly IShopProductViewModelFactory shopProductViewModelFactory;
        private readonly ICartService cartService;
        private readonly ICheckoutViewModelFactory checkoutViewModelFactory;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IApplicationUserService applicationUserService;
        private readonly IAddressService addressService;
        private readonly IOrderService orderService;

        public ShopController(
            IProductCategoryService productCategoryService,
            IProductService productService,
            IShopProductViewModelFactory shopProductViewModelFactory,
            ICartService cartService,
            ICheckoutViewModelFactory checkoutViewModelFactory,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IApplicationUserService applicationUserService,
            IAddressService addressService,
            IOrderService orderService
            )
        {
            this.productCategoryService = productCategoryService;
            this.productService = productService;
            this.shopProductViewModelFactory = shopProductViewModelFactory;
            this.cartService = cartService;
            this.checkoutViewModelFactory = checkoutViewModelFactory;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.applicationUserService = applicationUserService;
            this.addressService = addressService;
            this.orderService = orderService;
        }

        public async Task<IActionResult> Products(string? searchString, int? page, int? categoryId)
        {
            int currentPage = page != null ? Math.Max(0, page.Value) : 1;
            int defaultPageSize = 20;
            
            ProductCategory? productCategory = null;
            if (categoryId != null)
            {
                var categoryIdValue = categoryId.Value;

                productCategory = await productCategoryService.GetProductCategoryByIdAsync(categoryIdValue);

                if (productCategory == null)
                {
                    return BadRequest();
                }
            }

            var products = await productService.GetPagedProductsAsync(currentPage, defaultPageSize, searchString ?? string.Empty, productCategory);

            int totalProducts = productCategory != null ? await productService.GetTotalProductCountByCategoryAsync(productCategory.Id) 
                : await productService.GetTotalProductCountAsync();

            var productCategories = await productCategoryService.GetAllProductCategoriesAsync();

            var model = shopProductViewModelFactory.CreateShopProductsPagedViewModel(
                products,
                productCategories,
                currentPage,
                defaultPageSize,
                totalProducts,
                searchString);
            model.CategoryId = productCategory?.Id;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetail(int? productId)
        {
            if (productId == null)
            {
                return BadRequest();
            }

            var productIdValue = productId.Value;

            var product = await productService.GetProductByIdAsync(productIdValue, true);

            if (product == null)
            {
                return BadRequest();
            }

            var model = shopProductViewModelFactory.CreateShopProductDetailViewModel(product);

            return View(model);
        }

        [HttpGet]
        public IActionResult Cart()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            var cartProducts = cartService.GetCartProducts(HttpContext.Session);

            if (cartProducts == null)
            {
                return RedirectToAction("Products", "Shop");
            }

            var productIds = cartProducts.Select(x => x.Id).ToList();
            var products = await productService.GetProductsByIdAsync(productIds, true);

            if (products == null)
            {
                return RedirectToAction("Products", "Shop");
            }

            products.RemoveAll(x => !x.Published);

            var user = await userManager.GetUserAsync(HttpContext.User);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            user = await applicationUserService.GetUserWithAddress(user);

            var model = checkoutViewModelFactory.CreateCheckoutProductsViewModel(user, products, cartProducts);

            return View(model);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Checkout(CheckoutProductsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var cartProducts = cartService.GetCartProducts(HttpContext.Session);

            if (cartProducts == null)
            {
                return RedirectToAction("Products", "Shop");
            }

            var productIds = cartProducts.Select(x => x.Id).ToList();

            var products = await productService.GetProductsByIdAsync(productIds, true);

            if (products == null)
            {
                return RedirectToAction("Products", "Shop");
            }

            var someProductsMissing =
                products.Any(x => !productIds.Contains(x.Id))
                || products.RemoveAll(x => !x.Published || !x.Stocks.Any(x => x.StockQuantity > 0)) > 0;

            if (someProductsMissing)
            {
                cartService.ClearCart(HttpContext.Session);
                return RedirectToAction("Products", "Shop");
            }

            var user = await userManager.GetUserAsync(HttpContext.User);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            Address? address = null;
            if (model.ShippmentAddress.Id == -1)
            {
                var newAddress = new Address()
                {
                    Address1 = model.ShippmentAddress.Address1,
                    Address2 = model.ShippmentAddress.Address2,
                    ApplicationUser = user,
                    City = model.ShippmentAddress.City,
                    Country = model.ShippmentAddress.Country,
                    ZipPostalCode = model.ShippmentAddress.ZipPostalCode,
                    DeletedByUser = false
                };

                address = await addressService.CreateAddressAsync(newAddress);
            }
            else
            {
                address = await addressService.GetAddressByIdAsync(model.ShippmentAddress.Id);
            }

            if (address == null)
            {
                cartService.ClearCart(HttpContext.Session);
                return RedirectToAction("Products", "Shop");
            }

            // TODO: Create an Order

            await orderService.CreateOrderAsync(products, cartProducts, user, address);

            return View("OrderPlaced", model);
        }

        public IActionResult ErrorNotFound()
        {
            return View();
        }
    }
}
