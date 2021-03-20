$(function () {

    $("#incomeTable").on("click", ".incomeDelete", function () {
        var btn = $(this);
        bootbox.confirm("Kaydı silmek istediğinize emin misiniz?", function (result) {
            if (result) {
                var btnId = btn.data("id");
                $.ajax({
                    method: "Post",
                    dataType: "Json",
                    url: "/Income/Delete",
                    data: { id: btnId }
                }).done(function (data) {
                    if (!data.hasError) {
                        alert(data.Message);
                        btn.parent().parent().parent().parent().parent().parent().remove();
                    } else if (data.hasError) {
                        alert(data.Message);
                    } else if (data.results) {
                        alert(data.Message);
                    }
                }).fail(function () {
                    alert('Sunucu ile bağlantı kurulurken hata oluştu.')
                });
            }
        });

    });

    $("#expenseTable").on("click", ".expenseDelete", function () {
        var btn2 = $(this);
        bootbox.confirm("Kaydı silmek istediğinize emin misiniz?", function (result) {
            if (result) {
                var btnId2 = btn2.data("id");
                $.ajax({
                    method: "Post",
                    dataType: "Json",
                    url: "/Expense/Delete",
                    data: { id: btnId2 }
                }).done(function (data) {
                    if (!data.hasError) {
                        alert(data.Message);
                        btn.parent().parent().parent().parent().parent().parent().remove();
                    } else if (data.hasError) {
                        alert(data.Message);
                    } else if (data.results) {
                        alert(data.Message);
                    }
                }).fail(function () {
                    alert('Sunucu ile bağlantı kurulurken hata oluştu.')
                });
            }
        });

    });

    $("#staffTable").on("click", ".staffDelete", function () {
        var btn3 = $(this);
        bootbox.confirm("Kaydı silmek istediğinize emin misiniz?", function (result) {
            if (result) {
                var btnId3 = btn3.data("id");
                $.ajax({
                    method: "Post",
                    dataType: "Json",
                    url: "/Staff/Delete",
                    data: { id: btnId3 }
                }).done(function (data) {
                    if (!data.hasError) {
                        alert(data.Message);
                        btn.parent().parent().parent().parent().parent().parent().remove();
                    } else if (data.hasError) {
                        alert(data.Message);
                    } else if (data.results) {
                        alert(data.Message);
                    }
                }).fail(function () {
                    alert('Sunucu ile bağlantı kurulurken hata oluştu.')
                });
            }
        });

    });
});