var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
        $('.btn-Edit').off('click').on('click', function () {
            var listbook = $('.txtQuantity');
            var cartList = [];
            $.each(listbook, function (i, item) {
                cartList.push({
                    Quantity: $(item).val(),
                    Book: {
                        ID: $(item).data('id')
                    }
                });
            });

            //Phương thức Ajax dùng để đẩy lên Controller
            $.ajax({
                url: '/Cart/Edit',
                data: { cartModel: JSON.stringify(cartList) },
                dataType: 'Json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/Cart";
                    }
                }
            });
        });

        $('#btnDeleteAll').off('click').on('click', function () {
            var conf = confirm("Bạn có muốn xóa giỏ hàng??");
            if (conf == true) {
                $.ajax({
                    url: '/Cart/DeleteAll',
                    dataType: 'Json',
                    type: 'POST',
                    success: function (res) {
                        if (res.status == true) {
                            window.location.href = "/Cart";
                        }
                    }
                });
            }

        });

        $('.btn-Delete').off('click').on('click', function () {
            $.ajax({
                data: { id: $(this).data('id') },
                url: '/Cart/Delete',
                dataType: 'Json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/Cart";
                    }
                }
            });
        });


    }

}

cart.init();