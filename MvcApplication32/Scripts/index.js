$(function () {
    $(".delete").on('click', function() {
        var personId = $(this).data('person-id');
        $.post("/home/deleteperson", { personId: personId }, function() {
            window.location.reload();
        });
    });
});
