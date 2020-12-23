// JavaScript Document

var content = $('#contentID');
var hieght = content.height();
$('#ViewMore').hide();
if(hieght < 400){
	content.addClass('cc');
	$('#ViewMore').show();
}
$('.viewmore').ready(function() {
    $('.viewmore').click(function(){
	if(content.hasClass('cc')){
		content.removeClass('cc');
		$('#ViewMore span').empty();
		$('#ViewMore span').append('Thu gọn <i class="fa fa-sort-asc"></i>');
	}
	else
	{		
		content.addClass('cc');
		$('#ViewMore span').empty();
		$('#ViewMore span').append('Xem thêm nội dung <i class="fa fa-sort-desc"></i>');
	}

});
});

$(document).ready(function () {
    $("#Edit").click(function () {
        $("#quatity").val();
    });
    
});

			
	
