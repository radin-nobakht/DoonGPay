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
function Table(value) {
     
    if (value == 3 || value == 4 || value == 5) {
         
        $("#manual").show();
        if (value == 3) {
            $("#typeStr").text("نفرات")
        }
        if (value == 4) {
            $("#typeStr").text("درصد")
        }
        if (value == 5) {
            $("#typeStr").text("مقدار")
        }
    }
    else {
        $("#manual").hide();

    }
}
$(document).on("change", "#Type", function () {
     
    var value =$(this).val();
    Table(value);
});


//$(document).on("click", "#btnAddCost", function () {
//    var travelId = $(this).data("travel-id");
//    editTravelCost(null, travelId);
//});
//$(document).on("click", ".btnEditTravelCost", function () {
    
//    var travelCostId = $(this).data("id");
//    var travelId = $(this).data("travel-id");
//    editTravelCost(travelCostId, travelId);
//})
$(document).on("click", ".btnTravelCostEdit", function () {
    var travelId = $(this).data("travel-id");
    var travelCostId = $(this).data("id");
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
            var value = $("#TravelCost_Type").val();
            Table(value);
        },
        error: function () {
            $("#errorMessage").text().show();
        }
    });
}

// ویرایش

$(document).on("click", "#btnSaveCost", function () {
    var travelId = $("#CostForm #TravelCost_Id").val();

    $.ajax({
        url: "/Travel/SaveTravelCost",
        type: "POST",
        data: $("#CostForm").serialize() ,
        success: function (res) {
            if (res.result) {
                $("#editCost").modal("hide");
                travelCosts(travelId);
            } else {
                $("#errorMessage").text(res.message).show();
            }
        },
        error: function (result) {
            $("#errorMessage").text("خطا در ....");
        }
    });
})



