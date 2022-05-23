const cartId = "cart";

//Xây dựng đối tượng dùng để tương tác với LocalStorage
var localAdapter = {

    saveCart: function (object) {

        var stringified = JSON.stringify(object);
        localStorage.setItem(cartId, stringified);
        return true;

    },
    getCart: function () {

        return JSON.parse(localStorage.getItem(cartId));

    },
    clearCart: function () {

        localStorage.removeItem(cartId);

    }

};

//Đối tượng dùng để call ajax
var ajaxAdapter = {
    getCart: function () {
        $.ajax({
            type: "GET",
            url: "/Cart/Cart",
            success: function (value) {
                helpers.addCart(value)
            }, error: (er) => {
                console.log(er.responseText)
                if (er.status == 401 && localAdapter.getCart()) {
                    
                    helpers.addCart(localAdapter.getCart())
                }
            }
        })
        

    }

};

//đối tượng xử lý View
var helpers = {
    cartSelector:"dropdown-cart",
    addCart: function (items) {
        const html = items.map(item =>( `<li>
                        <span class="item">
                            <span class="item-left">
                                <img src="/img/logo-mwg.png" style="width:50px" alt="" />
                                <span class="item-info">
                                    <span class="book-info">${item.book_name}</span>
                                    <span class="book-info">${item.price}</span>
                                </span>
                            </span>
                            <span class="item-right">
                                <button onclick="handleDelteCart(${item.book_id})" class="btn btn-danger fa fa-close delete-cart-btn" data-order-id="${item.book_id}">X</button>
                            </span>
                        </span>
                    </li>`))

        document.getElementById(this.cartSelector).innerHTML = html.join("");

    }

};

//Đối tượng giỏ hàng
var cart = {
    items: [],
    getItems: function () {

        return this.items;

    },
    setItems: function (items) {

        this.items = items;

    },
    addItem: function (item) {

        if (this.containsItem(item.book_id) === false) {

            this.items.push({
                book_id: item.book_id,
                book_name: item.book_name,
                price: item.price
            });

            localAdapter.saveCart(this.items);


        } 

    },
    deleteItem: function (book_id) {
        this.items = this.items.filter(item => item.book_id !== book_id)

        localAdapter.saveCart(this.items);
    },
    containsItem: function (id) {

        if (this.items === undefined) {
            return false;
        }

        for (var i = 0; i < this.items.length; i++) {

            var _item = this.items[i];

            if (id == _item.book_id) {
                return true;
            }

        }
        return false;

    }
    

};


document.addEventListener('DOMContentLoaded', function () {

    if (localAdapter.getCart()) {

        cart.setItems(localAdapter.getCart());
    } 
});