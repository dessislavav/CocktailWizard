let currPage = 1;

$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/Manager/Ingredients/GetTenIngredients/",
        data: { 'currPage': currPage },
        contentType: "application/json",
        dataType: "html",
        success: function (response) {
            $('#ingredienttableplaceholder').replaceWith(response);
        },
        failure: function (response) {
            alert(response.responseText);
            console.log(fail);
        },
        error: function (response) {
            alert(response.responseText);
            console.log(error);
        }
    });
})

function nextPage() {
    currPage++;
    $.ajax({
        type: "GET",
        url: "/Manager/Ingredients/GetTenIngredients/",
        data: { 'currPage': currPage },
        contentType: "application/json",
        dataType: "html",
        success: function (response) {
            $('#ingredienttableplaceholder').replaceWith(response);
        },
        failure: function (response) {
            alert(response.responseText);
            console.log(fail);
        },
        error: function (response) {
            alert(response.responseText);
            console.log(error);
        }
    });
};

function prevPage() {
    currPage--;
    $.ajax({
        type: "GET",
        url: "/Manager/Ingredients/GetTenIngredients/",
        data: { 'currPage': currPage },
        contentType: "application/json",
        dataType: "html",
        success: function (response) {
            $('#ingredienttableplaceholder').replaceWith(response);
        },
        failure: function (response) {
            alert(response.responseText);
            console.log(fail);
        },
        error: function (response) {
            alert(response.responseText);
            console.log(error);
        }
    });
};