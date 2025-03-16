$(document).ready(function () {
    let selectedFile = null;

    $("#imageUpload").on("change", function () {
        let input = this;
        let preview = $("#imagePreview");

        if (input.files && input.files[0]) {
            selectedFile = input.files[0];
            let reader = new FileReader();
            reader.onload = function (e) {
                preview.html('<img src="' + e.target.result + '" class="preview-image">');
            };
            reader.readAsDataURL(selectedFile);
        }
    });

    $("form").on("submit", function (e) {
        e.preventDefault();

        let formData = new FormData(this);

        if (selectedFile) {
            formData.append("imageFile", selectedFile);
        }

        $.ajax({
            url: $(this).attr("action"),
            type: $(this).attr("method"),
            data: formData,
            processData: false,
            contentType: false,
            success: function (data) {
                $("#content").html(data).addClass("fade-in");
                setTimeout(() => $("#content").removeClass("fade-in"), 300);

                if (selectedFile) {
                    restoreFilePreview(selectedFile);
                }
            },
            error: function () {
                alert("Ошибка при отправке формы.");
            }
        });
    });

    function restoreFilePreview(file) {
        let preview = $("#imagePreview");
        let reader = new FileReader();
        reader.onload = function (e) {
            preview.html('<img src="' + e.target.result + '" class="preview-image">');
        };
        reader.readAsDataURL(file);

        let fileInput = $("#imageUpload")[0];
        let dataTransfer = new DataTransfer();
        dataTransfer.items.add(file);
        fileInput.files = dataTransfer.files;
    }
});