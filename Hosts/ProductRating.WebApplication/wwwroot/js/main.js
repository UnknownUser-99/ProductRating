function loadPage(page) {
    $.get("/Main/" + page, function (data) {
        $("#content").html(data).addClass("fade-in");
        setTimeout(() => $("#content").removeClass("fade-in"), 300);
    });
}

$(document).ready(function () {
    loadPage('Profile');
});