$(document).on("click", ".btnDeleteTravelCost", function () {
    var row = $(this).closest("tr");
    var id = $(this).data("id");
    if (!confirm("آیا از حذف اطمینان دارید؟")) return;

    $.post("/travel/DeleteTravelCost", { id: id }, function (res) {
        if (res.success) {
            row.fadeOut(300, function () {
                $(this).remove();
            });
        } else {
            alert("حذف ناموفق بود.");
        }
    });

});


$(document).on("click", "#btnAddCost", function () {
    var travelId = $(this).data("travel-id");
    editTravelCost(null, travelId);
});
$(document).on("click", ".btnEditTravelCost", function () {

    var travelCostId = $(this).data("id");
    var travelId = $(this).data("travel-id");
    editTravelCost(travelCostId, travelId);
})
function editTravelCost(travelCostId, travelId) {
    $.ajax({
        url: "/travel/EditTravelCost",
        type: "POST",
        data: {
            travelCostId: travelCostId,
            travelId: travelId
        },
        success: function (result) {
            $("#modal").html(result)
            $("#editCost").modal("show");
        },
        error: function () {
            $("#errorMessage").text("خطا در ثبت اطلاعات").show();
        }
    });
}

// ویرایش

$(document).on("click", "#btnSaveCost", function () {
    var travelId = $("#CostForm #TravelId").val();
    $.ajax({
        url: "/Travel/SaveCost",
        type: "POST",
        data: $("#CostForm").serialize(),

        success: function (res) {
            if (res.success) {
                $("#editCost").modal("hide");
                travelCosts(travelId);
            } else {
                $("#errorMessage").text(res.message).show();
            }
        },
        error: function () {
            $("#errorMessage").text("خطا در ثبت اطلاعات").show();
        }
    });
})



// حذف
$(".btnDeleteTravelCost").click(function () {
    var row = $(this).closest("tr");
    var id = $(this).data("id");
    if (!confirm("آیا از حذف اطمینان دارید؟")) return;

    $.post("/travel/DeleteTravelCost", { id: id }, function (res) {
        if (res.success) {
            row.fadeOut(300, function () {
                $(this).remove();
            });
        } else {
            alert("حذف ناموفق بود.");
        }
    });
});