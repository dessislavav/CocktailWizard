﻿$('input[type="file"]').change(function (e) {
    let fileName = e.target.files[0].name;
    $('#custom-file-label').append(fileName);
    console.log(fileName)
});