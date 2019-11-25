$('#comment-to-db').submit(function (e) {
    e.preventDefault();
    const body = $('#comment-body').val();
    const parts = location.pathname.split('/');
    const barId = parts[parts.length - 1];
    const data = {
        'Body': body,
        'BarId': barId
    }

    $.ajax({
        type: "POST",
        url: "/Member/BarComments/Create/",
        data: JSON.stringify(data),
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val(),
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },

        success: function (data) {
            $('#new-comment-here').append(data);
            $('#comment-body').val('');
        },
 
        error: function (msg) {
            $('#comment-body').focus();
            console.dir(msg);
        }
    })
});