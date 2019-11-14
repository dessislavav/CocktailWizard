function showEditModal(id) {
    $(`.${id}`).modal('show');
};

function SaveEditModal(id) {
    let currentNameId = id + '+CocktailName';
    let nameInput = document.getElementById(currentNameId);
    let newNameInput = $(nameInput).val();

    let currentInfoId = id + '+CocktailInfo';
    let infoInput = document.getElementById(currentInfoId);
    let newInfoInput = $(infoInput).val();

    let currentImagePathId = id + '+CocktailImagePath';
    let imagePathInput = document.getElementById(currentImagePathId);
    let newImagePathInput = $(imagePathInput).val();

    let oldNameId = id + '+name';
    let oldName = document.getElementById(oldNameId);
    $(oldName).text(newNameInput);
    $(`.${id}`).modal('hide');

    $.ajax(

        {
            type: "Post",
            url: "Manager/Cocktails/Edit",

            data: {
                'id': id,
                'newName': newNameInput,
                'newInfo': newInfoInput,
                'newImagePath': newImagePathInput,
            },
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
            },

        })
}
