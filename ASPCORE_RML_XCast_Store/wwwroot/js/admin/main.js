
const { createApp } = Vue

const app = createApp({
    data() {
        return {
            loading: false,
            productModel: {
                name: "Product Name",
                shortDescription: "Product Short Description",
                price: 25,
                published: false,
                stockQuantity: 1000
                },
            products: []
        }
    },
    methods: {
        getProducts() {
            this.loading = true;
            axios.get('/AdminProducts/api/GetAllProducts')
                .then(res => {
                    console.log(res)
                    this.products = res.data;
                })
                .catch(err => {
                    console.log(err)
                })
                .then(() => {
                    this.loading = false;
                });
        },
        createProduct() {
            this.loading = true;
            axios.post('/AdminProducts/CreateProduct', this.productModel)
                .then(res => {
                    console.log(res);
                    this.products.push(res.data);
                })
                .catch(err => {
                    console.log(err)
                })
                .then(() => {
                    this.loading = false;
                });
        }
    },
    computed: {
        formatPrice: function () {
            return "$ " + this.price;
        }
    }
})

app.mount('#app')