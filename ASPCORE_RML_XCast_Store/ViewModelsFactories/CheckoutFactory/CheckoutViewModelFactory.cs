using RMLXCast.Core.Domain.Cart;
using RMLXCast.Core.Domain.Catalog;
using RMLXCast.Core.Domain.User;
using RMLXCast.Web.ViewModels.Checkout;

namespace RMLXCast.Web.ViewModelsFactories.CheckoutFactory
{
    public class CheckoutViewModelFactory : ICheckoutViewModelFactory
    {
        public CheckoutProductsViewModel CreateCheckoutProductsViewModel(
            ApplicationUser applicationUser,
            IList<Product> products, IList<CartProduct> cartProducts
            )
        {
            var checkoutProducts = new List<CheckoutViewProductViewModel>(products.Count);

            foreach (var product in products)
            {
                foreach (var cartProduct in cartProducts)
                {
                    if (cartProduct.Id == product.Id)
                    {
                        checkoutProducts.Add(new CheckoutViewProductViewModel()
                        {
                            ProductName = product.Name,
                            Amount = cartProduct.Amount,
                            PricePerSlot = product.Price
                        });
                    }
                }
            }

            var shippmentAddressViewModel = new ShippmentAddress();
            var address = applicationUser.Addresses.FirstOrDefault();
            if (address != null)
            {
                shippmentAddressViewModel.Id = address.Id;
                shippmentAddressViewModel.Country = address.Country;
                shippmentAddressViewModel.Address1 = address.Address1;
                shippmentAddressViewModel.Address2 = address.Address2;
                shippmentAddressViewModel.City = address.City;
                shippmentAddressViewModel.ZipPostalCode = address.ZipPostalCode;
            }

            var model = new CheckoutProductsViewModel()
            {
                FirstName = applicationUser.FirstName,
                LastName = applicationUser.LastName,
                Products = checkoutProducts,
                ShippmentAddress = shippmentAddressViewModel
            };

            return model;
        }
    }
}
