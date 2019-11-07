$('#ingredients-table').click(function (e) {
    var $this2 = $(this);
    e.preventDefault();
    debugger;
    var targetPage = parseInt($this2.attr('at'));
    var urlPagination = "/Manager/Ingredients/Index?=" + targetPage;

    $.ajax({
        type: "GET",
        url: urlPagination,
        data: targetPage,
        success: function (data) {
            console.log("asdasd");
        },
    })
});
