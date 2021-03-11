$(document).ready(function () {
    var pageIndex = 1;
    if (typeof userName === 'undefined') {
        userName = null;
    }
    if (typeof authUser === 'undefined') {
        authUser = null;
    }
    loadPostList(pageIndex, userName, authUser);
    console.log(authUser, userName);
    $(window).scroll(function () {
        if ($(window).scrollTop() + $(window).height() > $(document).height() - 50) {
            pageIndex = pageIndex + 1;
            loadPostList(pageIndex, userName, authUser);
        }
    });
});

function loadPostList(pageIndex, userName, authUser) {

    $.ajax({
        url: "/Post/GetPosts",
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
        data: { pageIndex: pageIndex, userName: userName },
        success: function (result) {
            var html = '';
            if (result.length != 0) {
                $.each(result, function (key, item) {
                    if (item.ImagePath == null) {
                        html += '<li id="post_' + item.Id + '"><i class="activity__list__icon fa fa-question-circle-o"></i><div class="activity__list__header"><img src="' + item.UserImage + '" alt="" /><a href="/profile/' + item.UserName + '">' + item.Name + '</a> @' + item.UserName + '</div><div class="activity__list__body entry-content"><p>' + item.Text + '</p></div><div class="activity__list__footer">';
                    }
                    else {
                        html += '<li id="post_' + item.Id + '"><i class="activity__list__icon fa fa-question-circle-o"></i><div class="activity__list__header"><img src="' + item.UserImage + '" alt="" /><a href="/profile/' + item.UserName + '">' + item.Name + '</a> @' + item.UserName + '</div><div class="activity__list__body entry-content"><p>' + item.Text + '</p><ul class="gallery"><li><img src="' + item.ImagePath + '" alt="" /></li></ul></div><div class="activity__list__footer">';
                    }
                    if (item.isLiked) {
                        html += '<a onclick="Unlike(' + item.Id + ')" id="' + item.Id + '"> <i class="fa fa-heart"></i><span id="likecount_' + item.Id + '">' + item.LikesCount + '</span></a>';
                    }
                    else {
                        html += '<a onclick="Like(' + item.Id + ')" id="' + item.Id + '"> <i class="fa fa-heart-o"></i><span id="likecount_' + item.Id + '">' + item.LikesCount + '</span></a>';
                    }
                    console.log(item.UserName);
                    console.log(item.UserName == authUser);
                    if (item.UserName == authUser) {
                        html += '<a href="/post/' + item.Id + '"> <i class="fa fa-comments"></i>' + item.MentionsCount + '</a><a onclick="DeletePost(' + item.Id + ')" id="' + item.Id + '"> <i class="fa fa-trash"></i></a><span><a href="/post/' + item.Id + '"> <i class="fa fa-clock"></i>' + formatTime(Date.parse(item.CreateDate)) + '</a></span></div></li>';
                    }
                    else {
                        html += '<a href="/tweet/' + item.Id + '"> <i class="fa fa-comments"></i>' + item.MentionsCount + '</a><a href="#"> <i class="fa fa-share"></i>' + item.SharesCount + '</a><span><a href="/post/' + item.Id + '"> <i class="fa fa-clock"></i>' + formatTime(Date.parse(item.CreateDate)) + '</a></span></div></li>';
                    }

                });
                if (pageIndex == 1) {
                    $('#PostList').html(html);
                }
                else {
                    $('#PostList').append(html);
                }

            }
            else {
                $(window).unbind('scroll');
            }

        },
        error: function (errormessage) {
            $('#PostList').html('<li><p class="text-center">There were no results found</p></li>');
            $(window).unbind('scroll');
        }
    });
}