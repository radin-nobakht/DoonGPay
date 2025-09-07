// بارگذاری لیست سفرها
function loadTravels() {
    $.get("/Travel/Index", function (result) {
        $("#travelList").html(result);
    });
}

// ویرایش سفر
function editTravel(id) {
    $.get("/Travel/EditTravel", { id: id }, function (result) {
        $("#modal").html(result);
        new bootstrap.Modal(document.getElementById("editTravel")).show();
    });
}

// ذخیره سفر
function saveTravel() {
    $.post("/Travel/SaveTravel", $("#travelForm").serialize(), function (res) {
        if (res.success) {
            $("#editTravel").modal("hide");
            loadTravels();
        } else {
            $("#errorMessage").text(res.message).show();
        }
    });
}

// حذف سفر
function deleteTravel(id, row) {
    if (!confirm("آیا از حذف اطمینان دارید؟")) return;
    $.post("/Travel/DeleteTravel", { id: id }, function (res) {
        if (res.success) {
            row.fadeOut(300, function () { $(this).remove(); });
        } else {
            alert(res.message || "حذف ناموفق بود.");
        }
    });
}

// Events
$(document).on("click", ".btnEditTravel", function () {
    editTravel($(this).data("id"));
});
$(document).on("click", "#btnSaveTravel", function () {
    saveTravel();
});
$(document).on("click", ".btnDeleteTravel", function () {
    var row = $(this).closest("tr");
    deleteTravel($(this).data("id"), row);
});
