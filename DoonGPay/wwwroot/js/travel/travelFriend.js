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

$(document).on("click", ".btnEditFriend", function () {
    var travelId = $(this).data("travel-id");
    var travelFriendId = $(this).data("id");
    $.ajax({
        url: "/travel/EditFriend",
        type: "GET",
        data: {
            travelFriendId: travelFriendId,
            travelId: travelId
        },
        success: function (result) {
            $("#modal").html(result);
            $('#editFriend').modal('show');
        },
        error: function () {
            $("#errorMessage").text("خطا در ثبت اطلاعات").show();
        }
    });
})

$(document).on("click", "#btnSaveFriend", function () {
    var travelId = $("#friendForm #TravelId").val();
    $.ajax({
        url: "/Travel/SaveFriend",
        type: "POST",
        data: $("#friendForm").serialize(),

        success: function (res) {
            if (res.success) {
                $("#editFriend").modal("hide");
            } else {
                $("#errorMessage").text(res.message).show();
            }
        },
        error: function () {
            $("#errorMessage").text("خطا در ثبت اطلاعات").show();
        }
    });
})