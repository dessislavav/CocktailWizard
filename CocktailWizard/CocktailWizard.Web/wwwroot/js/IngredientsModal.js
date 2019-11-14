function showModal(id) {
    $(`.${id}`).modal('show');
};
function SaveModal(id) {
    let currentElementId = id + '+IngName';
    let input = document.getElementById(currentElementId);
    let newNameValue = $(input).val();

    let oldElementId = id + '+name';
    let oldElement = document.getElementById(oldElementId);
    $(oldElement).text(newNameValue);

    $(`.${id}`).modal('hide');

    $.ajax(

        {
            type: "Post",
            url: "Ingredients/Edit",

            data: {
                'id': id,
                'newName': newNameValue
            },
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
            },

        })
}