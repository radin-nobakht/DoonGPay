// بارگذاری لیست سفرها
function loadTravels() {
    $.ajax({
        url: "/Travel/LoadTravels",
        type: "GET",
        success: function (result) {
            $("#travels").html(result); // ← تغییر از #travelList به #travels
        },
        error: function () {
            $("#errorMessage").text("خطا در بارگذاری لیست سفرها").show();
        }
    });
};

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
// ویرایش سفر
$(document).on("click", ".btnEditTravel", function () {
    var id = $(this).data("id");
    $.ajax({
        url: "/Travel/EditTravel",
        type: "GET",
        data: { id: id },
        success: function (result) {
            $("#modal").html(result);
            new bootstrap.Modal(document.getElementById("editTravel")).show();
        },
        error: function () {
            $("#errorMessage").text("خطا در بارگذاری فرم ویرایش سفر").show();
        }
    });
});

// ذخیره سفر
$(document).on("click", "#btnSaveTravel", function () {
    $.ajax({
        url: "/Travel/SaveTravel",
        type: "POST",
        data: $("#travelForm").serialize(),
        success: function (res) {
            if (res.success) {
                bootstrap.Modal.getInstance(document.getElementById("editTravel")).hide();
                loadTravels();
            } else {
                $("#errorMessage").text(res.message).show();
            }
        },
        error: function () {
            $("#errorMessage").text("خطا در ذخیره سفر").show();
        }
    });
});

// حذف سفر
$(document).on("click", ".btnDeleteTravel", function () {
    var row = $(this).closest("tr");
    var id = $(this).data("id");

    if (!confirm("آیا از حذف اطمینان دارید؟")) return;

    $.ajax({
        url: "/Travel/DeleteTravel",
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
            alert("خطا در حذف سفر");
        }
    });
});
