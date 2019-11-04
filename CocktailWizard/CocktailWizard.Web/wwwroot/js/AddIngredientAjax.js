$('#load-add-ingredient-form').click(function () {
    let div = $('#ingrediens-div');
    div.show();
});


$('#ingrediens-to-db').click(function () {
    let name = $('#ingredient-name').val();
    let data = {
        'Name': name
    }

    $.ajax({
        type: "POST",
        url: "/Manager/Ingredients/Add",
        data: JSON.stringify(data),
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val(),
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        dataType: 'json',
        success: function (response) {
            $('#list-of-ingredients').append("<tr><td>" + response.Name + "</td> </tr>");
        },
        error: function (msg) {
            console.dir(msg);
        }
    })
});