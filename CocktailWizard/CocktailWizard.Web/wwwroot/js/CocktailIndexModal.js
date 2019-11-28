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

    let oldNameId = id + '+name';
    let oldName = document.getElementById(oldNameId);
    $(oldName).text(newNameInput);

    let oldInfoId = id + '+info';
    let oldInfo = document.getElementById(oldInfoId);
    $(oldInfo).text(newInfoInput);
    console.log(oldInfo);
    $(`.${id}`).modal('hide');

    $.ajax(

        {
            type: "Post",
            url: "Manager/Cocktails/Edit",

            data: {
                'id': id,
                'newName': newNameInput,
                'newInfo': newInfoInput,
            },
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
            },

        })
}
