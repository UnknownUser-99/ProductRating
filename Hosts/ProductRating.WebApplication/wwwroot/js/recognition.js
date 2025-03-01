$(document).ready(function () {
    $("#imageUpload").on("change", function () {
        let input = this;
        let preview = $("#imagePreview");

        if (input.files && input.files[0]) {
            let reader = new FileReader();
            reader.onload = function (e) {
                preview.html('<img src="' + e.target.result + '" class="preview-image">');
            };
            reader.readAsDataURL(input.files[0]);
        }
    });
});