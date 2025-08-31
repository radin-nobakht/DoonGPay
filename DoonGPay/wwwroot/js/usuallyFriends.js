$(document).on("click", ".deleteUsuallyFriend", function () {
    var row = $(this).closest("tr");
    var id = $(this).data("id");
    if (!confirm("آیا از حذف اطمینان دارید؟")) return;

    $.post("/UsuallyFriends/DeleteUsuallyFriend", { id: id }, function (res) {
        if (res.success) {
            row.fadeOut(300, function () {
                $(this).remove();
            });
        } else {
            alert("حذف ناموفق بود.");
        }
    });

});
$(document).on("click", ".btnEditUsuallyFriend", function () {
    debugger;
    var UsuallyFriendId = $(this).data("id");
    $.ajax({
        url: "/UsuallyFriends/SaveUsuallyFriend",
        type: "POST",
        data: $(this).serialize(),
        success: function (res) {
            if (res.success) {
                $('#editUsuallyFriend').modal('hide');
                location.reload();
            } else {
                $("#modal").html(res);
                $('#editUsuallyFriend').modal('show');
            }
        },
        error: function () {
            alert("خطا در ذخیره اطلاعات");
        }
    });
})

$(document).on("click", "#btnSaveUsuallyFriend", function () {
    $.ajax({
        url: "/Travel/SaveFriend",
        type: "POST",
        data: $("#usuallyFriendForm").serialize(),

        success: function (res) {
            if (res.success) {
                $("#editUsuallyFriend").modal("hide");
            } else {
                $("#errorMessage").text(res.message).show();
            }
        },
        error: function () {
            $("#errorMessage").text("خطا در ثبت اطلاعات").show();
        }
    });
})