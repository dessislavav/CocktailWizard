$(function () {
    $('#modal-delete').on('show.bs.modal', function (event) {
        console.log(event)
        var button = $(event.relatedTarget); // Button that triggered the modal
        console.log(button)
        var id = button.data("id");
        console.log(id)
        var modal = $(this);
        console.log(modal)
        modal.find('.modal-content input').val(id);
    });
});