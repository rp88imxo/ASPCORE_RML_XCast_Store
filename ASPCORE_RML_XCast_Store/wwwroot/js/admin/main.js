
const { createApp } = Vue

const app = createApp({
    data() {
        return {
            editing: false,
            objectIndex: 0,
            id: 0,
            loading: false,
            productModel: {
                id: 0,
                name: "Product Name",
                shortDescription: "Product Short Description",
                price: 25,
                published: false,
                stockQuantity: 1000
            },
            products: []
        }
    },
    mounted() {
        this.getProducts();
    },
    methods: {
        getProduct(id) {
            this.loading = true;
            axios.get('/AdminProducts/api/getProduct/' + id)
                .then(res => {
                    console.log(res);
                    var product = res.data;
                })
                .catch(err => {
                    console.log(err)
                })
                .then(() => {
                    this.loading = false;
                });
        },
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
                    this.editing = false;
                });
        },
        editProduct(product, index) {
            this.editing = true;
            this.objectIndex = index;
            this.productModel = {
                id: product.id,
                name: product.name,
                shortDescription: product.shortDescription,
                price: product.price,
                published: product.published,
                stockQuantity: product.stockQuantity
            };
        },
        updateProduct() {
            this.loading = true;
            axios.post('/AdminProducts/UpdateProduct', this.productModel)
                .then(res => {
                    console.log(res);
                    this.products.splice(this.objectIndex, 1, res.data);
                })
                .catch(err => {
                    console.log(err)
                })
                .then(() => {
                    this.loading = false;
                    this.editing = false;
                });
        },
        deleteProduct(id, index) {
            this.loading = true;
            axios.delete('/AdminProducts/DeleteProduct/' + id)
                .then(res => {
                    console.log(res)
                    this.products.splice(index, 1);
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