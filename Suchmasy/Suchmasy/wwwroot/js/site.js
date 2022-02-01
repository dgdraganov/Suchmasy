// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


//success: function(data) {
//    $('#created-success').html(data).fadeIn('slow');
//    //$('#msg').html("data insert successfully").fadeIn('slow') //also show a success message 
//    $('#created-success').delay(5000).fadeOut('slow');
//}

//$('#created-success').on('click', function() {
//    $('#created-success').delay(5000).fadeOut();
//});

function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

$(document).ready(function () {
    sleep(3000).then(() => {
        $("#created-success").fadeOut(5000)
    });
})

function copy(text) {
    let target = `#${text}`
    setTimeout(function () {
        $(target).html("Copy!")
    }, 800);
    $(target).html("Copied!");//append("<div class='tip' id='copied_tip'>Copied!</div>");
    var input = document.createElement('input');
    input.setAttribute('value', text);
    document.body.appendChild(input);
    input.select();
    var result = document.execCommand('copy');
    document.body.removeChild(input)
    return result;
}