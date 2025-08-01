
function travels() {
    
    $.ajax({
        url: "/Travel/Travel",
        type: "POST",
        success: function (result) {
            $("#travels").html(result)
        },
        error: function () {

        }
    });
}
/*travelFriendsAndCosts();*/
 function travelFriendsAndCosts() {
     var travelId = $(".travel").attr("data-id");
     travelCosts(travelId);
    travelFriends(travelId);
};
$(document).on("click", "tr.travels", function () {

});
$(document).on("click", "#morebtn", function () {
    $.ajax({
        url: "/Travel/Travels",
        type: "POST",
        success: function (result) {
            $("#modal").html(result);
            $("#travelsModal").modal("show");
        },
        error: function () {
            $("#errorMessage").text("خطا در ثبت اطلاعات").show();
        }
    });
});





function travelFriends(travelId) {
    $.ajax({
        url: "/travel/Friend",
        type: "POST",
        data: { travelId: travelId },
        success: function (result) {
            $("#travelFriend").html(result)
        },
        error: function () {
            $("#errorMessage").text("خطا در ثبت اطلاعات").show();

        }
    });
}
function travelCosts(travelId) {
    $.ajax({
        url: "/Travel/TravelCosts",
        type: "POST",
        data: { travelId: travelId },
        success: function (result) {
            $("#travelCosts").html(result)
        },
        error: function () {
            $("#errorMessage").text("خطا در ثبت اطلاعات").show();
        }
    });
}
$(document).on("click", ".travels", function () {
    var id = $(this).attr("data-id");
    $.ajax({
        url: "/Travel/ChangeTravel",
        type: "POST",
        data: { id: id },
        success: function (result) {
            $("#travels").html(result)
            $('#travelsModal').modal('hide');

        },
        error: function () {

        }
    });
});


$(document).on("click", ".btnDeleteTravel", function () {
    var row = $(this).closest("tr");
    var id = $(this).data("id");
    if (!confirm("آیا از حذف اطمینان دارید؟")) return;

    $.post("/travel/DeleteTravel", { id: id }, function (res) {
        if (res.success) {
            row.fadeOut(300, function () {
                $(this).remove();
            });
        } else {
            alert("حذف ناموفق بود.");
        }
    });

});


//$("#btnAddTravel").on("click",function () {

//    editTravel(null);
//});
//$(document).on("click", ".btnEditTravel", function () {
//    var id = $(this).data("id");
//    editTravel(id);
//})
$(document).on("click", ".btnEditTravel", function () {
    var Id = $(this).data("travel-id");
    editTravel(id);
});
function editTravel(id) {
    $.ajax({
        url: "/travel/EditTravel",
        type: "POST",
        data: { id: id },
        success: function (result) {
            $("#modal").html(result)
            $("#editTravel").modal("show");
        },
        error: function () {
            $("#errorMessage").text("خطا در ثبت اطلاعات").show();
        }
    });
}

// ویرایش

$(document).on("click", "#btnSaveTravel", function () {

    $.ajax({
        url: "/travel/SaveTravel",
        type: "POST",
        data: $("#travelForm").serialize(),

        success: function (res) {
            if (res.success) {
                $("#editTravel").modal("hide");
                travels();
            } else {
                $("#errorMessage").text(res.message).show();
            }
        },
        error: function () {
            $("#errorMessage").text("خطا در ثبت اطلاعات").show();
        }
    });
})
