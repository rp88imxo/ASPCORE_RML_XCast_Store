
const { createApp } = Vue

const app = createApp({
    data() {
        return {
            editing: false,
            addedToCart: false,
            objectIndex: 0,
            id: 0,
            loading: false,
            cartProductModel: {
                id: 0,
                amount:1
                },
            products: []
        }
    },
    mounted() {
        $(document).trigger('product-detail-loaded');
    },
    methods: {
        addToCart() {
            this.loading = true;
            var element = document.getElementById('productId');
            this.cartProductModel.id = element.value;

            var amount = document.getElementById('productAmount').value;
            this.cartProductModel.amount = amount;

            axios
                .post('/api/cart/AddProductToCart', this.cartProductModel)
                .then(res => {

                    if (res.status == 200) {
                        this.addedToCart = true;
                      
                        document.getElementById('updateCartBtn').click();
                        // TODO: OPEN A SIDEBAR CART
                       
                        alert('Товар добавлен в корзину!');

                        document.getElementById('cartSidebarToggle').click();
                    }
                    else if (res.status == 400) {
                        alert('Что-то пошло не так при добавлении товара в корзину!');
                    }
                })
                .catch(err => {
                    console.log(err)
                })
                .then(() => {
                    this.loading = false;
                });
        },
    },
    computed: {
        formatPrice: function () {
            return "$ " + this.price;
        }
    }
})

app.mount('#app')