
var filter = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
var filterphone = /^[0-9-+]+$/;

function fonblur(id) {
    var err = false
    var em = gde("idemail");
    var pw = gde("idpassword");
    var rpw = gde("repassword");
    var fn = gde("fullname");
    var tlp = gde("telephone");

    if (id == 2) {
        if (pw.value.length < 6) {
            $("#help2").empty();
            $("#help2").append('<span class="alert alert-danger">Mật khẩu phải dài tối thiểu 6 ký tự</span>');
        }
        else {
            $("#help2").empty();
            $("#help2").append('<span class="ok">&nbsp;</span>');
        }
    }

    if (id == 3) {
        if (pw.value != rpw.value) {
            $("#help3").empty();
            $("#help3").append('<span class="alert alert-danger">Xác nhận mật khẩu không đúng</span>');
        }
        else if (rpw.value == '') {

        }
        else {
            $("#help3").empty();
            $("#help3").append('<span class="ok">&nbsp;</span>');
        }
    }

    if (id == 4) {
        if (fn.value == "") {
            $("#help4").empty();
            $("#help4").append('<span class="alert alert-danger">Vui lòng nhập tên của bạn</span>');
        }
        else {
            $("#help4").empty();
            $("#help4").append('<span class="ok">&nbsp;</span>');
        }
    }

    if (id == 5) {
        if (tlp.value == "") {
            $("#help5").empty();
            $("#help5").append('<span class="alert alert-danger">Vui lòng nhập điện thoại</span>');
            err = true;
        }
        else if (!filterphone.test(tlp.value) || tlp.value.length < 10) {
            $("#help5").empty();
            $("#help5").append('<span class="alert alert-danger">Số điện thoại không đúng</span>');
            err = true;
        }
        else {
            $("#help5").empty();
            $("#help5").append('<span class="ok">&nbsp;</span>');
        }
    }

    return false;
}

function CheckForm() {
    var err = false
    var em = gde("idemail");
    var pw = gde("idpassword");
    var rpw = gde("repassword");
    var fn = gde("fullname");
    var tlp = gde("telephone");

    if (em.value == "") {
        $("#help1").empty();
        $("#help1").append('<span class="alert alert-danger">Vui lòng nhập email</span>');
        err = true;
    }
    else if (!filter.test(em.value)) {
        $("#help1").empty();
        $("#help1").append('<span class="alert alert-danger">Email bạn nhập không đúng</span>');
        err = true;
    }

    if (pw.value.length < 6) {
        $("#help3").empty();
        $("#help2").empty();
        $("#help2").append('<span class="alert alert-danger">Mật khẩu phải dài tối thiểu 6 ký tư</span>');
        err = true;
    }
    else if (pw.value != rpw.value) {
        $("#help3").empty();
        $("#help3").append('<span class="alert alert-danger">Xác nhận mật khẩu không đúng</span>');
        err = true;
    }

    if (fn.value == "") {
        $("#help4").empty();
        $("#help4").append('<span class="alert alert-danger">Vui lòng nhập tên của bạn</span>');
        err = true;
    }

    if (tlp.value == "") {
        $("#help5").empty();
        $("#help5").append('<span class="alert alert-danger">Vui lòng nhập điện thoại</span>');
        err = true;
    }
    else if (!filterphone.test(tlp.value) || tlp.value.length < 9) {
        $("#help5").empty();
        $("#help5").append('<span class="alert alert-danger">Số điện thoại không đúng</span>');
        err = true;
    }

    if (err) {
        Boxy.alert('Dữ liệu nhập vào không đúng, vui lòng kiểm tra lại', null, { title: 'Lỗi' });
        return false;
    }
    else {
        return true;
    }
}

function showhelp(id) {
    if (id == 1) {
        $("#help1").empty();
        $("#help1").append('Nhập email của bạn');
    }

    if (id == 2) {
        $("#help2").empty();
        $("#help2").append('Mật khẩu phải có ít nhất 6 ký tự');
        err = true;
    }

    if (id == 3) {
        $("#help3").empty();
        $("#help3").append('Xác nhận lại mật khẩu');
        err = true;
    }

    if (id > 3) {
        $("#help" + id).empty();
    }
}

function cemail(email, onblur) {
    if (filter.test(email)) {
        address = '/users/checkemail/index.html';
        address = addQuery(address, 'email=' + email);
        $.ajax({
            url: address,
            dataType: "json",
            error: function (e) {
                alert(address);
                return;
            },
            success: function (data) {
                if (data.err == 0) {
                    $("#help1").empty();
                    $("#help1").append('<span class="ok">Email có thể dùng</span>');
                }
                else {
                    $("#help1").empty();
                    $("#help1").append('<span class="error">Email này đã có người dùng</span>');
                }
            }
        });
    }
    else if (onblur) {
        if (email != "") {
            $("#help1").empty();
            $("#help1").append('<span class="alert alert-danger">Email bạn nhập không đúng</span>');
        }
        else {
            $("#help1").empty();
            $("#help1").append('<span class="error">Vui lòng nhập email</span>');
        }
    }
}

