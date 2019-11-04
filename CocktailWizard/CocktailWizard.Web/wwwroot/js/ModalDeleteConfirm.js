$(function () {
    $('#modal-delete').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget); // Button that triggered the modal
        var id = button.data("id");
        var modal = $(this);

        modal.find('.modal-content input').val(id);
    });
});