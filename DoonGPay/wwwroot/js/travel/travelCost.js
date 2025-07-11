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
    if (value != 1 && value != 2) {
        $("#manual").attr("hidden", false)
        if (value == 3) {
            $("#typeStr").text("نفرات")
        }
        if (value == 4) {
            $("#typeStr").text("درصد")
        }
        if (value == 5) {
            $("#typeStr").val("مقدار")
        }
    }
}
$(document).on("change", "#TravelCost_Type", function () {
    var value =$(this).val();
    Table(value);
});
//function selectCost(travelId, costId, value) {
//    debugger;
//    if (value != 1 && value != 2) {
//        $("#manual").attr("hidden", false)
//        if (value == 3) {
//            $("#typeStr").text("نفرات")
//        }
//        if (value == 4) {
//            $("#typeStr").text("درصد")
//        }
//        if (value == 5) {
//            $("#typeStr").text("مقدار")
//        }
//        $.ajax({
//            url: "Travel/SelectTable",        
//            type: 'POST',
//            data: {
//                costId: costId,
//                travelId: travelId  
//            },     
//            success: function (response) {
//                debugger;
//                $("#manual").attr("hidden",false)
//            },
//            error: function (xhr, status, error) {
//                if (errorCallback) {
//                    errorCallback(xhr, status, error);
//                }
//            }
//        });
//    }
//    } 

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
            var value = $("#TravelCost_Type").val();
            Table(value);
        },
        error: function () {
            $("#errorMessage").text("خطا در ثبت اطلاعات").show();
        }
    });
}

// ویرایش

$(document).on("click", "#btnSaveCost", function () {
    var travelId = $("#CostForm #TravelCost_Id").val();
    var type = $("#TravelCost_Type").val();
    $.ajax({
        url: "/Travel/SaveCost",
        type: "POST",
        data: $("#CostForm").serialize() + "&Type=" + type,


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



