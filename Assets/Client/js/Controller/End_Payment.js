
//Khi thanh toán xong, thì giỏ hàng sẽ trống
var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
        $('#End-Payment').off('click').on('click', function () {            
            $.ajax({
                url: '/Cart/DeleteAll',
                dataType: 'Json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/thanh-toan-thanh-cong";
                    }
                }
            });
        });

        $('#End-Payment').off('click').on('click', function () {
            var BookID = $('.Book-Name');
            var listBook = [];
            $.each(BookID, function (i, item) {
                listBook.push({
                    Book: {
                        ID: $(item).data('id')
                    }
                });
            });

            $.ajax({
                url: '/Cart/Success',
                data: { id: JSON.stringify(listBook) },
                dataType: 'Json',
                Type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "thanh-toan-thanh-cong";
                    }
                }

            });
        });
    }
}

cart.init();