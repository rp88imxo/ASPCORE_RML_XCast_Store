
const { createApp } = Vue

const app = createApp({
    data() {
        return {
            editing: false,
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
                    console.log(res);
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