$(document).on("click", ".btn-deleteFriend", function () {
    var row = $(this).closest("tr");
    var id = $(this).data("id");
    if (!confirm("آیا از حذف اطمینان دارید؟")) return;

    $.post("/travel/DeleteFriend", { id: id }, function (res) {
        if (res.success) {
            row.fadeOut(300, function () {
                $(this).remove();
            });
        } else {
            alert("حذف ناموفق بود.");
        }
    });

});


//$(document).on("click", "#btnAddFriend", function () {
//    var travelId = $(this).data("travel-id");
//    editTravelFriend(null, travelId);
//});
//$(document).on("click", ".btnEditFriend", function () {

//    var travelFriendId = $(this).data("id");
//    var travelId = $(this).data("travel-id");
//    editTravelFriend(travelFriendId, travelId);
//})
$(document).on("click", ".btnEditFriend", function () {

    var travelId = $(this).data("travel-id");
    var travelFriendId = $(this).data("id");
    editTravelFriend(travelFriendId, travelId)
})
function editTravelFriend(travelFriendId, travelId) {
    $.ajax({
        url: "/travel/EditFriend",
        type: "POST",
        data: {
            travelFriendId: travelFriendId,
            travelId: travelId
        },
        success: function (result) {
            $("#modal").html(result)
            $("#editFriend").modal("show");
        },
        error: function () {
            $("#errorMessage").text("خطا در ثبت اطلاعات").show();
        }
    });
}

// ویرایش

$(document).on("click", "#btnSaveFriend", function () {
    var travelId = $("#friendForm #TravelId").val();
    $.ajax({
        url: "/Travel/SaveFriend",
        type: "POST",
        data: $("#friendForm").serialize(),

        success: function (res) {
            if (res.success) {
                $("#editFriend").modal("hide");
                travelFriends(travelId);
            } else {
                $("#errorMessage").text(res.message).show();
            }
        },
        error: function () {
            $("#errorMessage").text("خطا در ثبت اطلاعات").show();
        }
    });
})