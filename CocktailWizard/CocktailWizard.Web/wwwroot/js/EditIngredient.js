function showEditModal(id) {
    $(`.${id}`).modal('show');
};

function SaveEditModal(id) {
    let currentNameId = id + '+IngName';
    let nameInput = document.getElementById(currentNameId);
    let newNameInput = $(nameInput).val();

    let oldNameId = id + '+name';
    let oldName = document.getElementById(oldNameId);
    $(oldName).text(newNameInput);
    $(`.${id}`).modal('hide');

    $.ajax(

        {
            type: "Post",
            url: "/Manager/Ingredients/Edit",

            data: {
                'id': id,
                'newName': newNameInput,
            },
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
            },

        })
}
