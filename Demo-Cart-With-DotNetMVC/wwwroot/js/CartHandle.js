
function handleDelteCart(bookId) {

	var r = confirm("Bạn có chắc chắn muốn xóa hủy đơn hàng? ");
	if (r == true) {
		/*var bookId = $(".delete-cart-btn").attr("data-order-id");*/

		$.ajax({

			type: "POST",
			url: "/Cart/Delete?bookId=" + bookId,
			success: function (value) {
				console.log(value)
			}, error: (er) => {
				console.log(er.responseText)
				if (er.status == 401) {
					cart.deleteItem(bookId)
				}
			}
		})

	} else {
		alert("Thao tác bị hủy");
	}
}