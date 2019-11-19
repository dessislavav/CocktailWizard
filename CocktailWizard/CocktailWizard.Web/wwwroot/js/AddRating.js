$(".star-container").on("change", (getId) => {
    let rt = $(getId)[0].target.defaultValue;
    console.log(rt);
    let a = $("#currentValue").val(rt);
    console.log(a);
});