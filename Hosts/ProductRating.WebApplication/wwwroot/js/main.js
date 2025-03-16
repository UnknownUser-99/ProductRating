function loadPage(page) {
    $.get("/Main/" + page + "/" + page, function (data) {
        $("#content").html(data).addClass("fade-in");
        setTimeout(() => $("#content").removeClass("fade-in"), 300);

        history.pushState({ page: page }, "", "/" + page.toLowerCase());
    });
}

$(document).ready(function () {
    loadPage('Profile');
});