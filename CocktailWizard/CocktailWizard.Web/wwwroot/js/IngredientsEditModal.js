$(function () {
    $(document).on('click', '.edit-item', function (e) {
        e.preventDefault();

        var form = $(this).closest('form');
        bootbox.prompt("Are you sure you want to edit this ingredient?", function (result) {
            if (result != null && result != '') {
                $('#newName').val(result);
                form.trigger('submit');
            };
        });
    })
});
