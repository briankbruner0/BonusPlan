

function readCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}
function CommunityModal() {
    var objCommunity = new Object();

   
    objCommunity.Username = readCookie("USERNAME");
    var ajaxMethodURL = location.protocol + '//' + location.host + '/Application/BonusPlan/AJAXData/CommunityModalAJAX.aspx/CommunityModalGet';

    $.ajax({
        type: 'POST',
        url: ajaxMethodURL,
        cache: false,
        data: JSON.stringify(objCommunity),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        beforeSend: function () {
            var overlay = $('<div id="Overlay"><img src="../../../../images/progress.gif" alt="Loading" /></div>');
            overlay.appendTo(document.body);
        },
        success: function (msg) {
            var response = msg.d;
            var modal = $(response)
            modal.appendTo(document.body);
            $("#Overlay").remove();
            $("#communityModal").modal('toggle');

            $('#communityModal').on('shown.bs.modal', function () {
                $('#communityFilter').focus();
            })//sets cursor to community search box

            $('#communityFilter').focus(); //for IE 9 only
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            if (XMLHttpRequest.readyState < 4) {
                return true;
            }
            else {
                alert('Error :' + XMLHttpRequest.responseText);
            }
        }
    });
}