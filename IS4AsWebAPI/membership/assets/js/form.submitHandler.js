//execute onload 
$(document).ready(function () {

    $("[id$=txtConfirmCode]").keypress(function (e) {
        if (e.which == 13) {
            $("[id$=btnToNext]").click();
            return false;
        }
    });

    $("[id$=txtMailAddress]").keypress(function (e) {
        if (e.which == 13) {
            $("[id$=btnToNext]").click();
            return false;
        }
    });

    $("[id$=txtMyCode]").keypress(function (e) {
        if (e.which == 13) {
            $("[id$=btnToNext]").click();
            return false;
        }
    });

    $("[id$=txtPassword]").keypress(function (e) {
        if (e.which == 13) {
            $("[id$=btnToNext]").click();
            return false;
        }
    });

    $("[id$=txtPassword2]").keypress(function (e) {
        if (e.which == 13) {
            $("[id$=btnToNext]").click();
            return false;
        }
    });


});
