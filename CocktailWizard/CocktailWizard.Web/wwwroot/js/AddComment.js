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
        url: "/BarComments/Create/",
        data: JSON.stringify(data),
        headers: {
            'Content-Type': 'application/json'
        },
        error: function (msg) {
            console.dir(msg);
        }
    })
});