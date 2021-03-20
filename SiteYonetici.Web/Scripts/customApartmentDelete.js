$(function () {
    $("#apartmentTable").on("click", ".apartmentDelete", function () {
        var btn = $(this);
        bootbox.confirm("Kaydı silmek istediğinize emin misiniz?", function (result) {
            if (result) {
                var btnId = btn.data("id");
                $.ajax({
                    method: "Post",
                    dataType: "Json",
                    url: "/Apartment/Delete",
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
});