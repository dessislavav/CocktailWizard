function showEditModal(id) {
    $(`.${id}`).modal('show');
};

function SaveEditModal(id) {
    let currentNameId = id + '+BarName';
    let nameInput = document.getElementById(currentNameId);
    let newNameInput = $(nameInput).val();

    let currentInfoId = id + '+BarInfo';
    let infoInput = document.getElementById(currentInfoId);
    let newInfoInput = $(infoInput).val();

    let currentAddressId = id + '+BarAddress';
    let addressInput = document.getElementById(currentAddressId);
    let newAddressInput = $(addressInput).val();

    let currentPhoneId = id + '+BarPhone';
    let phoneInput = document.getElementById(currentPhoneId);
    let newPhoneInput = $(phoneInput).val();

    let currentImagePathId = id + '+BarImagePath';
    let imagePathInput = document.getElementById(currentImagePathId);
    let newImagePathInput = $(imagePathInput).val();

    let oldNameId = id + '+name';
    let oldName = document.getElementById(oldNameId);
    $(oldName).text(newNameInput);
    $(`.${id}`).modal('hide');

    $.ajax(

        {
            type: "Post",
            url: "Manager/Bars/Edit",

            data: {
                'id': id,
                'newName': newNameInput,
                'newInfo': newInfoInput,
                'newAddress': newAddressInput,
                'newPhone': newPhoneInput,
                'newImagePath': newImagePathInput,
            },
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
            },

        })
}
