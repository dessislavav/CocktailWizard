$(function () {
    $(document).on('click', '.delete-item', function (e) {
        e.preventDefault();

       let form = $(this).closest('form');
        bootbox.confirm("Are you sure you want to delete this cocktail?", function (result) {
            if (result === true) {
                form.trigger('submit');
            }
        });
    });
});
