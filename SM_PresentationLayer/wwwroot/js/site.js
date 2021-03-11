function formatTime(timeCreated) {

    var diff = Math.floor((Date.now() - timeCreated) / 1000);
    var interval = Math.floor(diff / 31536000);

    if (interval >= 1) {
        return interval + "y";
    }
    interval = Math.floor(diff / 2592000);
    if (interval >= 1) {
        return interval + "m";
    }
    interval = Math.floor(diff / 604800);
    if (interval >= 1) {
        return interval + "w";
    }
    interval = Math.floor(diff / 86400);
    if (interval >= 1) {
        return interval + "d";
    }
    interval = Math.floor(diff / 3600);
    if (interval >= 1) {
        return interval + "h";
    }
    interval = Math.floor(diff / 60);
    if (interval >= 1) {
        return interval + " m";
    }
    return "<1m";
}

function DeletePost(id) {

    var model = {
        Post: id,
    };
    $.ajax({
        data: { id: id },
        type: "POST",
        url: "/Post/DeletePost/",
        dataType: "JSON",
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (result) {
            $("#post_" + id).remove();
        }
    });
}

function Like(id) {
    let $likecount = $("#likecount_" + id);
    let likecount = Number($likecount.val() + $likecount.next().text()) || 0;

    var model = {
        PostId: id,
        AppUserId: 0
    };
    $.ajax({
        data: JSON.stringify(model),
        type: "POST",
        url: "/Like/Like/",
        contentType: "application/json",
        dataType: "JSON",
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (result) {
            if (result == "Success") {
                likecount = likecount + 1;
                let html = '<a onclick="Unlike(' + id + ')"id="' + id + '"> <i class="fa fa-heart"></i><span id="likecount' + id + '">' + likecount + '</span></a>';

                $("#" + id).replaceWith(html);
            }
        }
    });
}
function Unlike(id) {
    let $likecount = $("#likecount_" + id);
    let likecount = Number($likecount.val() + $likecount.next().text()) || 0;
    var model = {
        PostId: id,
        AppUserId: 0
    };
    $.ajax({
        data: JSON.stringify(model),
        type: "POST",
        url: "/Like/Unlike/",
        contentType: "application/json",
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        dataType: "JSON",
        success: function (result) {
            if (result == "Success") {

                if (likecount != 0)
                    likecount = likecount - 1;

                let html = '<a onclick="Like(' + id + ')"id="' + id + '"> <i class="fa fa-heart-o"></i><span id="likecount' + id + '">' + likecount + '</span></a>';

                $("#" + id).replaceWith(html);
            }

        }
    });
}

$(document).ready(function () {

    $('#btnSendPost').click(function (e) {
        var formData = new FormData();

        formData.append("AppUserId", JSON.stringify(parseInt($("#AppUserId").val())));
        formData.append("Text", $("#Text").val());
        formData.append("Image", document.getElementById("Image").files[0]);

        $.ajax({
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            url: "/Post/AddPost/",
            success: function (result) {
                if (result == "Success") {
                    document.getElementById("Text").value = "";
                    $("#postValidation").addClass("alert alert-success").text("Sent Successfully!");
                    $("#postValidation").alert();
                    $("#postValidation").fadeTo(3000, 3000).slideUp(800, function () {
                    });
                }
                else {
                    $("#postValidation").addClass("alert alert-danger").text("Error Occured!");
                    $("#postValidation").alert();
                    $("#postValidation").fadeTo(3000, 3000).slideUp(800, function () {
                    });
                }
            },
            error: function (result) {
                console.log(result);
                $("#postValidation").addClass("alert alert-danger").text(result.responseText);
                $("#postValidation").alert();
                $("#postValidation").fadeTo(3000, 3000).slideUp(2000, function () {
                });
            }
        });
    });
});


function Follow(isExist) {
    var model = {
        FollowerId: parseInt($("#FollowerId").val()),
        FollowingId: parseInt($("#FollowingId").val()),
        isExist: isExist
    };
    $.ajax({
        data: { FollowerId: model.FollowerId, FollowingId: model.FollowingId, isExist: model.isExist },
        type: "POST",
        url: "/Follow/Follow/",
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        dataType: "JSON",
        success: function (result) {
            if (result == "Success") {
                if (!isExist) {
                    $("#Follow").replaceWith('<button onclick="Follow(true)" id="UnFollow" class="btn btn-rounded btn-info"><i class="fa fa-minus"></i> Unfollow</button>');
                    FollowersCount = FollowersCount + 1;
                    $("#FollowersCount").replaceWith('<li id="FollowersCount"><strong>' + FollowersCount + '</strong>Followers</li>');
                }
                else {
                    $("#UnFollow").replaceWith('<button onclick="Follow(false)" id="Follow" type="submit" class="btn btn-rounded btn-info"><i class="fa fa-plus"></i> Follow</button>');
                    FollowersCount = FollowersCount - 1;
                    $("#FollowersCount").replaceWith('<li id="FollowersCount"><strong>' + FollowersCount + '</strong>Followers</li>');
                }
            }
        }
    });
}

/* left navbar */
$(document).ready(function () {
    var url = window.location.href;
    $('a.list-group-item').each(function () {
        if (this.href == url) {
            $(this).addClass('active');
            return false;
        }
    });
});

$(document).ready(function () {

    $('#btnSendMention').click(function (e) {
        var model = {
            Text: $("#Text").val(),
            PostId: parseInt($("#Id").val())
        };

        $.ajax({
            data: model,
            type: "POST",
            headers: {
                RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            dataType: "JSON",
            url: "/Mention/AddMention/",
            success: function (result) {
                if (result == "Success") {
                    $("#postValidation").addClass("alert alert-success").text("Sent Successfully!");
                    $("#postValidation").alert();
                    $("#postValidation").fadeTo(3000, 3000).slideUp(800, function () {
                    });
                }
                else {
                    $("#postValidation").addClass("alert alert-danger").text("Error Occured!");
                    $("#postValidation").alert();
                    $("#postValidation").fadeTo(3000, 3000).slideUp(800, function () {
                    });
                }
            }
        });
    });
});

function keypress(e) {
    if (e.keyCode === 13) {
        document.getElementById('searchform').submit();
    }
}