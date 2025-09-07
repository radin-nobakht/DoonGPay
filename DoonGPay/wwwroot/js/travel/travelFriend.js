// ویرایش دوست
function editFriend(travelFriendId, travelId) {
    $.get("/TravelFriend/EditFriend", { travelFriendId, travelId }, function (result) {
        $("#modal").html(result);
        new bootstrap.Modal(document.getElementById("editFriend")).show();
    });
}

// ذخیره دوست
function saveFriend() {
    $.post("/TravelFriend/SaveFriend", $("#friendForm").serialize(), function (res) {
        if (res.success) {
            $("#editFriend").modal("hide");
            loadTravels();
        } else {
            $("#errorMessage").text(res.message).show();
        }
    });
}

// حذف دوست
function deleteFriend(id, row) {
    if (!confirm("آیا از حذف اطمینان دارید؟")) return;
    $.post("/TravelFriend/DeleteFriend", { id }, function (res) {
        if (res.success) {
            row.fadeOut(300, function () { $(this).remove(); });
        } else {
            alert(res.message || "حذف ناموفق بود.");
        }
    });
}

// لیست دوستان همیشگی
function showUsuallyFriends() {
    $.get("/UsuallyFriend/UsuallyFriendsModal", function (result) {
        $("#modal").html(result);
        new bootstrap.Modal(document.getElementById("usuallyFriendsModal")).show();
    });
}

// انتخاب دوست از دوستان همیشگی
function selectUsuallyFriend(id) {
    $.post("/UsuallyFriend/GetAndConvertUsuallyFriendDtoTotravelFriendDto", { id }, function (result) {
        $("#usuallyFriendsModal").modal("hide");
        $("#modal").html(result);
        new bootstrap.Modal(document.getElementById("editFriend")).show();
    });
}

// Events
$(document).on("click", ".btnEditFriend", function () {
    editFriend($(this).data("id"), $(this).data("travel-id"));
});
$(document).on("click", "#btnSaveFriend", function () {
    saveFriend();
});
$(document).on("click", ".btn-deleteFriend", function () {
    var row = $(this).closest("tr");
    deleteFriend($(this).data("id"), row);
});
$(document).on("click", "#btnUsuallyFriend", function () {
    showUsuallyFriends();
});
$(document).on("click", ".usuallyFriend", function () {
    selectUsuallyFriend($(this).data("id"));
});
