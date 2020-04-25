$(function () {
    $(".btn-toggle").click(function (e) {
        e.preventDefault();
        el = $(this).data('element');
        $(el).toggle();
    });
});