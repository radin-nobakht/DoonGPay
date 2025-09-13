// ویرایش دوست
$(document).on("click", ".btnEditFriend", function () {
    var travelFriendId = $(this).data("id");
    var travelId = $(this).data("travel-id");

    $.ajax({
        url: "/Travel/EditFriend",
        type: "GET",
        data: { travelFriendId, travelId },
        success: function (result) {
            $("#modal").html(result);
            new bootstrap.Modal(document.getElementById("editFriend")).show();
        },
        error: function () {
            $("#errorMessage").text("خطا در بارگذاری اطلاعات دوست").show();
        }
    });
});

$(document).on("click", ".btn-friendInfo", function () {
    var travelFriendId = $(this).data("id");
    var travelId = $(this).data("travel-id");

    $.ajax({
        url: "/Travel/TravelCostFriend",
        type: "GET",
        data: { travelFriendId, travelId },
        success: function (result) {
            $("#modal").html(result);
            $("#frinedInfoModal").modal("show");

        },
        error: function () {
            $("#errorMessage").text("خطا در بارگذاری اطلاعات دوست").show();
        }
    });
});

// ذخیره دوست
$(document).on("click", "#btnSaveFriend", function () {
    $.ajax({
        url: "/Travel/SaveFriend",
        type: "POST",
        data: $("#friendForm").serialize(),
        success: function (res) {
            if (res.success) {
                bootstrap.Modal.getInstance(document.getElementById("editFriend")).hide();
                loadTravels();
            } else {
                $("#errorMessage").text(res.message).show();
            }
        },
        error: function () {
            $("#errorMessage").text("خطا در ذخیره اطلاعات").show();
        }
    });
});

// حذف دوست
$(document).on("click", ".btn-deleteFriend", function () {
    var row = $(this).closest("tr");
    var id = $(this).data("id");

    if (!confirm("آیا از حذف اطمینان دارید؟")) return;

    $.ajax({
        url: "/Travel/DeleteFriend",
        type: "POST",
        data: { id },
        success: function (res) {
            if (res.success) {
                row.fadeOut(300, function () { $(this).remove(); });
            } else {
                alert(res.message || "حذف ناموفق بود.");
            }
        },
        error: function () {
            alert("خطا در حذف دوست");
        }
    });
});



// لیست دوستان همیشگی
$(document).on("click", "#btnUsuallyFriend", function () {
    $.ajax({
        url: "/UsuallyFriend/UsuallyFriendsModal",
        type: "GET",
        success: function (result) {
            $("#modal").html(result);
            new bootstrap.Modal(document.getElementById("usuallyFriendsModal")).show();
        },
        error: function () {
            $("#errorMessage").text("خطا در بارگذاری دوستان همیشگی").show();
        }
    });
});


// انتخاب دوست از دوستان همیشگی
$(document).on("click", ".usuallyFriend", function () {
    var id = $(this).data("id");

    $.ajax({
        url: "/UsuallyFriend/GetAndConvertUsuallyFriendDtoTotravelFriendDto",
        type: "POST",
        data: { id },
        success: function (result) {
            bootstrap.Modal.getInstance(document.getElementById("usuallyFriendsModal")).hide();
            $("#modal").html(result);
            new bootstrap.Modal(document.getElementById("editFriend")).show();
        },
        error: function () {
            $("#errorMessage").text("خطا در انتخاب دوست").show();
        }
    });
});

