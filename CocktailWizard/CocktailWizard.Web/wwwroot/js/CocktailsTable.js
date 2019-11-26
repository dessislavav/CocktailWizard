let currPage = 1;
let currentSort = null;

$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/Cocktails/GetFiveCocktails/",
        data: { 'currPage': currPage, 'sortOrder': currentSort },
        contentType: "application/json",
        dataType: "html",
        success: function (response) {
            $('#cocktailtableplaceholder').replaceWith(response);
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
        url: "/Cocktails/GetFiveCocktails/",
        data: { 'currPage': currPage, 'sortOrder': currentSort },
        contentType: "application/json",
        dataType: "html",
        success: function (response) {
            $('#cocktailtableplaceholder').replaceWith(response);
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
    console.log(currPage);
    $.ajax({
        type: "GET",
        url: "/Cocktails/GetFiveCocktails/",
        data: { 'currPage': currPage, 'sortOrder': currentSort },
        contentType: "application/json",
        dataType: "html",
        success: function (response) {
            $('#cocktailtableplaceholder').replaceWith(response);
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

function sortByRating() {
    currPage = 1;
    if (currentSort === null || currentSort === "Rating") {
        currentSort = "rating_desc"
    }
    else {
        currentSort = "Rating";
    }
    $.ajax({
        type: "GET",
        url: "/Cocktails/GetFiveCocktails/",
        data: { 'currPage': currPage, 'sortOrder': currentSort },
        contentType: "application/json",
        dataType: "html",
        success: function (response) {
            $('#cocktailtableplaceholder').replaceWith(response);
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

function sortByName() {
    currPage = 1;
    if (currentSort === null || currentSort === "Name") {
        currentSort = "name_desc"
    }
    else {
        currentSort = "Name";
    }
    $.ajax({
        type: "GET",
        url: "/Cocktails/GetFiveCocktails/",
        data: { 'currPage': currPage, 'sortOrder': currentSort },
        contentType: "application/json",
        dataType: "html",
        success: function (response) {
            $('#cocktailtableplaceholder').replaceWith(response);
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
