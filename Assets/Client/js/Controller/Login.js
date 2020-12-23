
var filter = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

function CheckForm()
{
    var err = false
    var em = gde("idemail");
    var emlc = em.value.toLowerCase();
    var pw = gde("idpassword");

    if(em.value == "")
    {
        $("#help1").empty();
        $("#help1").append('<span class="error">Vui lòng nhập email</span>');
        err = true;
    }
    else if (!filter.test(emlc))
    {
        $("#help1").empty();
        $("#help1").append('<span class="error">Email bạn nhập không đúng</span>');
        err = true;
    }

    if(pw.value.length < 6)
    {
        $("#help2").empty();
        $("#help2").append('<span class="error">Mật khẩu phải dài tối thiểu 6 ký tư</span>');
        err = true;
    }

    if(err)
    {
        Boxy.alert('Dữ liệu nhập vào không đúng, vui lòng kiểm tra lại', null, {title: 'Lỗi'});
        return false;
    }
    else
    {
        return true;
    }
}

function showhelp(id)
{
    if(id == 1)
    {
        $("#help1").empty();
        $("#help1").append('Nhập email của bạn');
    }

    if(id == 2)
    {
        $("#help2").empty();
        $("#help2").append('Nhập mật khẩu của bạn');
        err = true;
    }
}
