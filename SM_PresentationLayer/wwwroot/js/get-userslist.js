$(document).ready(function () {
    var pageIndex = 1;
    loadUserResults(pageIndex, userName, controllerName, actionName);

    $(window).scroll(function () {
        if ($(window).scrollTop() + $(window).height() > $(document).height() - 50) {
            pageIndex = pageIndex + 1;
            loadUserResults(pageIndex, userName, controllerName, actionName);
        }
    });
});

function loadUserResults(pageIndex, userName, controllerName, actionName) {
    $.ajax({
        url: "/" + controllerName + "/" + actionName,
        type: "POST",
        beforeSend: function () {
            $("#loader").show();
        },
        complete: function () {
            $("#loader").hide();
        },
        async: true,
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        dataType: "json",
        data: { userName: userName, pageIndex: pageIndex },
        success: function (result) {
            console.log(result);
            var html = '';
            if (result.length != 0) {
                $.each(result, function (key, item) {
                    html += '<li><i class="activity__list__icon fa fa-question-circle-o"></i><div class="activity__list__header"><img src="' + item.ImagePath + '" alt="" /><a href="/profile/' + item.UserName + '">' + item.Name + '</a> @' + item.UserName + '</div></li>';
                });
                if (pageIndex == 1) {
                    $('#UsersResult').html(html);
                }
                else {
                    $('#UsersResult').append(html);
                }
            }
            else {
                $(window).unbind('scroll');
            }

        },
        error: function (errormessage) {
            $('#UsersResult').html('<li><p class="text-center">There were no results found</p></li>');
            $(window).unbind('scroll');
        }
    });
}