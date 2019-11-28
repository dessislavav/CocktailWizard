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

    let oldNameId = id + '+name';
    let oldName = document.getElementById(oldNameId);
    $(oldName).text(newNameInput);

    let oldInfoId = id + '+info';
    let oldInfo = document.getElementById(oldInfoId);
    $(oldInfo).text(newInfoInput);

    let oldAddressId = id + '+address';
    let oldAddress = document.getElementById(oldAddressId);
    $(oldAddress).text(newAddressInput);
    console.log(newAddressInput);

    let oldPhoneId = id + '+phone';
    let oldPhone = document.getElementById(oldPhoneId);
    $(oldPhone).text(newPhoneInput);

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
            },
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
            },

        })
}
