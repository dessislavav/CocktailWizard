let currPage = 1;
let currentSort = null;

$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/Bars/GetFiveBars/",
        data: { 'currPage': currPage, 'sortOrder': currentSort },
        contentType: "application/json",
        dataType: "html",
        success: function (response) {
            $('#bartableplaceholder').replaceWith(response);
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
        url: "/Bars/GetFiveBars/",
        data: { 'currPage': currPage, 'sortOrder': currentSort },
        contentType: "application/json",
        dataType: "html",
        success: function (response) {
            $('#bartableplaceholder').replaceWith(response);
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
        url: "/Bars/GetFiveBars/",
        data: { 'currPage': currPage, 'sortOrder': currentSort },
        contentType: "application/json",
        dataType: "html",
        success: function (response) {
            $('#bartableplaceholder').replaceWith(response);
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
        url: "/Bars/GetFiveBars/",
        data: { 'currPage': currPage, 'sortOrder': currentSort },
        contentType: "application/json",
        dataType: "html",
        success: function (response) {
            $('#bartableplaceholder').replaceWith(response);
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
        url: "/Bars/GetFiveBars/",
        data: { 'currPage': currPage, 'sortOrder': currentSort },
        contentType: "application/json",
        dataType: "html",
        success: function (response) {
            $('#bartableplaceholder').replaceWith(response);
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
