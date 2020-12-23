var detail = {
    init: function () {
        detail.regEvents();
    },
    regEvents: function () {
        $('.DatTruoc').off('click').on('click', function () {

            var book = $('.main_image');
            var listBook = [];
            $.each(book, function (i, item) {
                listBook.push({
                    quantity: $(item).val(1),
                    BookID: {
                        ID: $(item).data('id')
                    }
                });
            });
            $.ajax({
                url: '/Cart/AddItem',
                dataType: 'Json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/Cart/Payment";
                    }
                }
            });
        });

    }
}

detail.init();