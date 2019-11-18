function showModal(id) {
    $(`.${id}`).modal('show');
};
function SaveModal(id) {
    let currentElementId = id + '+CommentBody';
    let input = document.getElementById(currentElementId);
    let newBodyValue = $(input).val();

    let oldElementId = id + '+body';
    let oldElement = document.getElementById(oldElementId);
    $(oldElement).text(newBodyValue);

    $(`.${id}`).modal('hide');

    $.ajax(

        {
            type: "Post",
            url: "/Member/BarComments/Edit",

            data: {
                'id': id,
                'newBody': newBodyValue
            },
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
            },

        })
}