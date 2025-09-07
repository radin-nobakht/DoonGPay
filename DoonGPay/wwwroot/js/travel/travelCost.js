// ویرایش هزینه سفر
function editTravelCost(travelCostId, travelId) {
    $.get("/TravelCost/EditTravelCost", { travelCostId: travelCostId, travelId: travelId }, function (result) {
        $("#modal").html(result);
        new bootstrap.Modal(document.getElementById("editCost")).show();
        updateCostTableVisibility($("#Type").val());
    });
}

// ذخیره هزینه سفر
function saveTravelCost() {
    $.post("/TravelCost/SaveTravelCost", $("#CostForm").serialize(), function (res) {
        if (res.success) {
            $("#editCost").modal("hide");
            loadTravels();
        } else {
            $("#errorMessage").text(res.message).show();
        }
    });
}

// حذف هزینه سفر
function deleteTravelCost(id, row) {
    if (!confirm("آیا از حذف اطمینان دارید؟")) return;
    $.post("/TravelCost/DeleteTravelCost", { id: id }, function (res) {
        if (res.success) {
            row.fadeOut(300, function () { $(this).remove(); });
        } else {
            alert(res.message || "حذف ناموفق بود.");
        }
    });
}

// Events
$(document).on("click", ".btnTravelCostEdit", function () {
    editTravelCost($(this).data("id"), $(this).data("travel-id"));
});
$(document).on("click", "#btnSaveCost", function () {
    saveTravelCost();
});
$(document).on("click", ".btnDeleteTravelCost", function () {
    var row = $(this).closest("tr");
    deleteTravelCost($(this).data("id"), row);
});

// نمایش/پنهان جدول دستی هزینه
function updateCostTableVisibility(value) {
    if (value == 3 || value == 4 || value == 5) {
        $("#manual").show();
        $("#typeStr").text(value == 3 ? "نفرات" : value == 4 ? "درصد" : "مقدار");
    } else {
        $("#manual").hide();
    }
}
$(document).on("change", "#Type", function () {
    updateCostTableVisibility($(this).val());
});
