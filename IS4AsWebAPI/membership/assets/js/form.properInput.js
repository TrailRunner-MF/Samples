// Created at 2019/10/07
// Created by JSS Pav Vanna

//execute onload 
$(document).ready(function () {

    //適切な誕生日表示
    setProperBirthdaySelection();
    // 変更あった場合、適切な誕生日表示
    $('[id$=ddlBirthMonth]').on('change', function () {
        setProperBirthdaySelection();
    });
    $('[id$=ddlBirthYear]').on('change', function () {
        setProperBirthdaySelection();
    });

    //パスワードのマスク有無
    setPasswordMask();
    //パスワードを表示/非表示する
    $("[id$=chkShowPassword]").click(function () {
        setPasswordMask();
    });

});

function setProperBirthdaySelection() {
    var year = $('[id$=ddlBirthYear]').find(':selected').val();
    var month = $('[id$=ddlBirthMonth]').find(':selected').val();

    var day = $('[id$=ddlBirthDay]').find(':selected').val();
    if (day !== undefined) {
        rewriteBirthdaySelectOption(year.replace("年", ""), month.replace("月", ""), day.replace("日", ""));
    }
}

function setPasswordMask() {
    var checked = $('[id$=chkShowPassword]').prop('checked');

    var $targetObj = $("[id$=txtPassword]");
    if (checked) {
        $targetObj.prop('type', 'text');
        $("[id$=txtPassword2]").val(""); $("[id$=Pw2Area]").hide();
    } else {
        $targetObj.prop('type', 'password');
        $("[id$=Pw2Area]").show();
    }
}

function rewriteBirthdaySelectOption(year, month, day) {
    var initVal = '<option selected="selected" value="">--日</option>';   
    var str = '';
    for (var i = 1; i <= dayInMonth(year, month) ; i++) {
        str += "<option value ='" + i + "'";
        if (i == day) {
            initVal = '<option value="">--日</option>';
            str += " selected='selected' >" + i + "日</option>";
        } else {
            str += ">" + i + "日</option>";
        }
    }
    $('[id$=ddlBirthDay]').html(initVal + str);
}

// if leap year return 1 otherwise 0.
function isLeapYear(year) {
    if (((year % 4) == 0) && ((year % 100) != 0) || ((year % 400) == 0) == 1) return 1;
    else return 0;
}
// get number of days in month
function dayInMonth(year, month) {
    if (month == 2) return (28 + isLeapYear(year));
    else if (month == 4 || month == 6 || month == 9 || month == 11) return 30;
    else return 31;
}