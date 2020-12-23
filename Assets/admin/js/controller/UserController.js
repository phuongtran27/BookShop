var user = {
    init: function () {
        user.registerEvents();
    },
    registerEvents: function () {
        //gọi sự kiện class= "btn-active" trong button, off('click'): gỡ sự kiện clíck ra và reset lại, sau đó on('click') vào một funtion
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();

            var btn = $(this);
            var id = $(this).data('id');//lấy ra tất cả thuộc tính mà đằng trước nó là data và sau nó là id, hoặc có thể dùng Prop('id') đều đc
            //Gọi Ajax
            $.ajax({
                url: "/Admin/User/ChangeStatus",
                data: { id: id }, //Truyền vào id của public JsonResult ChangeStatus(long id)
                dataType: "json",
                type: "POST",
                success: function (response) {
                    //đặt debug
                    console.log(response);//response lấy về từ view
                    if (response.status == true) {
                        btn.text('Kích hoạt');
                    }
                    else {
                        btn.text('Khóa');
                    }
                }
            });
        })

        //Xóa người dùng
        $('.btn-deleteUser').off('click').on('click', function (e) {
            var conf = confirm('Bạn có muốn xóa người dùng này??');
            if (conf == false)
                return;
            e.preventDefault();
            var btn = $(this);
            var id = $(this).data('id');
            $.ajax({
                url: "/Admin/User/Delete",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/Admin/User/Index";
                        alert('Xóa thành công!!');
                    }
                    else {
                        window.location.href = "/Admin/User/Index";
                        alert('Xóa không thành công!');
                    }
                }
            });
        });
        //Xóa sách
        $('.btn-deleteBook').off('click').on('click', function (e) {
            var conf = confirm('Bạn có muốn xóa sách này??');
            if (conf == false)
                return;
            e.preventDefault();
            var btn = $(this);
            var id = $(this).data('id');
            $.ajax({
                url: "/Admin/Book/Delete",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (res) {
                    if (res.status == true) {

                        window.location.href = "/Admin/Book/Index";
                        alert('Xóa thành công!!');
                    }
                    else {
                       
                        window.location.href = "/Admin/Book/Index";
                        alert('Xóa không thành công!!');
                    }
                }
            });
        });


        //Xóa slide
        $('.btn-deleteSLide').off('click').on('click', function (e) {
            var conf = confirm('Bạn có muốn xóa Slide này??');
            if (conf == false)
                return;
            e.preventDefault();
            var btn = $(this);
            var id = $(this).data('id');
            $.ajax({
                url: "/Admin/Banner/Delete",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (res) {
                    if (res.status == true) {                        
                        window.location.href = "/Admin/Banner/Index";
                        alert('Xóa thành công!!');
                    }
                    else {                       
                        window.location.href = "/Admin/Banner/Index";
                        alert('Xóa không thành công!');
                    }
                }
            });
        });

        $('#email').focus();
    }
}

user.init();

