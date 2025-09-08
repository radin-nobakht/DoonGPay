// ویرایش هزینه سفر
$(document).on("click", ".btnTravelCostEdit", function () {
    var travelCostId = $(this).data("id");
    var travelId = $(this).data("travel-id");

    $.ajax({
        url: "/Travel/EditTravelCost",
        type: "GET",
        data: { travelCostId: travelCostId, travelId: travelId },
        success: function (result) {
            $("#modal").html(result);
            new bootstrap.Modal(document.getElementById("editCost")).show();

            // نمایش/پنهان جدول دستی
            var value = $("#Type").val();
            if (value == 3 || value == 4 || value == 5) {
                $("#manual").show();
                $("#typeStr").text(value == 3 ? "نفرات" : value == 4 ? "درصد" : "مقدار");
            } else {
                $("#manual").hide();
            }
        },
        error: function () {
            $("#errorMessage").text("خطا در بارگذاری فرم هزینه").show();
        }
    });
});

// ذخیره هزینه سفر
$(document).on("click", "#btnSaveCost", function () {
    $.ajax({
        url: "/Travel/SaveTravelCost",
        type: "POST",
        data: $("#CostForm").serialize(),
        success: function (res) {
            if (res.success) {
                bootstrap.Modal.getInstance(document.getElementById("editCost")).hide();
                loadTravels();
            } else {
                $("#errorMessage").text(res.message).show();
            }
        },
        error: function () {
            $("#errorMessage").text("خطا در ذخیره هزینه").show();
        }
    });
});

// حذف هزینه سفر
$(document).on("click", ".btnDeleteTravelCost", function () {
    var row = $(this).closest("tr");
    var id = $(this).data("id");

    if (!confirm("آیا از حذف اطمینان دارید؟")) return;

    $.ajax({
        url: "/Travel/DeleteTravelCost",
        url: "/Travel/DeleteTravelCost",
        type: "POST",
        data: { id: id },
        success: function (res) {
            if (res.success) {
                row.fadeOut(300, function () { $(this).remove(); });
            } else {
                alert(res.message || "حذف ناموفق بود.");
            }
        },
        error: function () {
            alert("خطا در حذف هزینه");
        }
    });
});

// تغییر نوع هزینه (نمایش/پنهان جدول دستی)
$(document).on("change", "#Type", function () {
    var value = $(this).val();
    if (value == 3 || value == 4 || value == 5) {
        $("#manual").show();
        $("#typeStr").text(value == 3 ? "نفرات" : value == 4 ? "درصد" : "مقدار");
    } else {
        $("#manual").hide();
    }
});
