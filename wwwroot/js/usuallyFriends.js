$(function () {
    var id = $("#Id").val();

    if (id == 0) 
        $(".modal-title").text("اضافه کردن دوستان همیشگی")
    else
        $(".modal-title").text("به روزرسانی ی دوستان همیشگی")

});


$(document).on("click", ".btnDeleteUsuallyFriend", function () {
    var row = $(this).closest("tr");
    var id = $(this).data("id");
    if (!confirm("آیا از حذف اطمینان دارید؟d")) return;

    $.post("/UsuallyFriend/DeleteUsuallyFriend", { id: id }, function (res) {
        if (res.success) {
            row.fadeOut(300, function () {
                $(this).remove();
            });
        } else {
            alert("حذف ناموفق بود.");
        }
    });

});
$(document).on("click", ".btnEditusuallyFriend", function () {
    debugger;
    var id = $(this).data("id");
    $.ajax({
        url: "/UsuallyFriend/EditUsuallyFriend",
        type: "GET",
        data: { id: id },
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
    debugger;
    $.ajax({
        url: "/UsuallyFriend/SaveUsuallyFriend",
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