$(document).ready(function () {
    const $modal = $('#addReview');

    $(document).on('click', '.review-button', function () {
        $modal.addClass('show');
    });

    $modal.find('.close-modal, .cancel-button').on('click', function () {
        $modal.removeClass('show');
    });

    $modal.on('click', function (e) {
        if ($(e.target).is($modal)) {
            $modal.removeClass('show');
        }
    });
});