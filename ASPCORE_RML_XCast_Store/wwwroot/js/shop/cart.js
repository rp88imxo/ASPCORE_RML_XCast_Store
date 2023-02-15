
const { createApp } = Vue

const app = createApp({
    data() {
        return {
            editing: false,
            loading: false,
            cartProductModel: {
                id: 0,
                amount: 1
            },
        }
    },
    mounted() {
        $(document).trigger('vue-loaded');
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
                        alert('Товар добавлен в корзину!');
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
        formatPrice: function (price) {
            return price + "₽";
        }
    }
})

app.mount('#app')