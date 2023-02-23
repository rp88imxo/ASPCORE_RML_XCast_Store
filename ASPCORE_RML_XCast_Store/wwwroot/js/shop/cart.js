
const { createApp } = Vue

const app = createApp({
    data() {
        return {
            editing: false,
            loading: false,
            cartProducts: [],
            cartProduct: {
                productId: 0,
                productName: "None",
                pricePerSlot: 0,
                amount: 0,
                orderMinimumQuantity: 0,
                orderMaximumQuantity: 0
            },
            sessionCartProductViewModel:
            {
                id: 0,
                amount: 0
            },
            UpdateCartViewModel:
            {
                SessionCartProducts: []
            },
            startPrice: 0,
            endPrice: 0,
        }
    },
    mounted() {
        $(document).trigger('vue-loaded');
        this.getCart();
    },
    methods: {
        getCart() {
            this.loading = true;
            axios
                .get('/api/cart/GetCart')
                .then(res => {
                    if (res.status == 200) {
                        console.log(res.data)
                        this.cartProducts = res.data.cartProducts;
                    }
                    else {
                        alert('Что-то пошло не так при получении корзины!');
                    }
                })
                .catch(err => {
                    console.log(err)
                })
                .then(() => {
                    this.loading = false;
                });
        },
        deleteProductFromCart(cartProduct, index) {
            this.loading = true;

            this.sessionCartProductViewModel = {
                id: cartProduct.productId,
                amount: cartProduct.amount
            };

            axios
                .post('/api/cart/DeleteProductFromCart', this.sessionCartProductViewModel)
                .then(res => {

                    if (res.status == 200) {
                        this.cartProducts.splice(index, 1);
                    }
                    else if (res.status == 400) {
                        alert('Что-то пошло не так при удалении товара из корзины!');
                    }
                })
                .catch(err => {
                    console.log(err)
                })
                .then(() => {
                    this.loading = false;
                });
        },
        handleCheckoutClick() {

            if (!this.haveAnyProducts) {
                alert("Корзина пуста!");
                return;
            }

            this.UpdateCartViewModel.SessionCartProducts = this.cartProducts;

            axios
                .post('/api/cart/updateProductCart', this.UpdateCartViewModel)
                .then(res => {

                    if (res.status == 200) {
                        window.location.href = "/Shop/Checkout";
                    }
                    else if (res.status == 400) {
                        window.location.href = "/Shop";
                    }
                })
                .catch(err => {
                    console.log(err)
                })
                .then(() => {
                    this.loading = false;
                });
        },
        decreaseAmountClick(cartProduct) {
            cartProduct.amount = cartProduct.amount - 1;

            if (cartProduct.amount < cartProduct.orderMinimumQuantity) {
                cartProduct.amount = cartProduct.orderMinimumQuantity;
            }
            else if (cartProduct.amount < 1) {
                cartProduct.amount = 1;
            }
        },
        increaseAmountClick(cartProduct) {
            cartProduct.amount = cartProduct.amount + 1;

            if (cartProduct.amount > cartProduct.orderMaximumQuantity) {
                cartProduct.amount = cartProduct.orderMaximumQuantity;
            }
        },
        totalPrice(cartProduct) {
            return cartProduct.pricePerSlot * cartProduct.amount + " ₽";
        },
        formatPrice(cartProduct) {
            return cartProduct.pricePerSlot + " ₽";
        },
        formatPriceOnly(valueToFormat) {
            return valueToFormat + " ₽";
        }
    },
    computed: {
        startPriceComputed() {
            var sum = 0;
            for (let cartProduct of this.cartProducts) {
                sum += cartProduct.amount * cartProduct.pricePerSlot;
            }
            return this.formatPriceOnly(sum);
        },
        endPriceComputed() {
            var sum = 0;
            for (let cartProduct of this.cartProducts) {
                sum += cartProduct.amount * cartProduct.pricePerSlot;
            }
            return this.formatPriceOnly(sum);
        },
        haveAnyProducts() {
            if (typeof this.cartProducts !== 'undefined' && this.cartProducts.length > 0) {
                return true;
            }

            return false;
        }
    }
})

app.mount('#app')