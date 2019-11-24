$(function () {
    $(document).on('click', '.delete-item', function (e) {
        e.preventDefault();

        var form = $(this).closest('form');
        bootbox.confirm("Are you sure you want to delete this ingredient?", function (result) {
            if (result === true) {
                form.trigger('submit');
            }
        });
    });
});
