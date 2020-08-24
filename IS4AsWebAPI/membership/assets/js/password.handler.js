/********
// Created at 2019/10/10
// Created by JSS Pav Vanna
// software keyboard for Kana input
*********/


$(function () {

    switchPwInputType();
    $("[id$=chkShowPassword]").click(function () {
        switchPwInputType();
    });

});

function switchPwInputType() {
    var $targetObj = $("[id$=txtPassword]");
    var checked = $('[id$=chkShowPassword]').prop('checked');
    if (checked) {
        $targetObj.prop('type', 'text');
    } else {
        $targetObj.prop('type', 'password');
    }
}


$(function () {

    var minLen = 1; var maxLen = 20;
    //initialize elements
    if ($(window).width() > 750) {
        drawKanaKeyboard();
    }
    else {
        drawKanaKeyboardSm();
    }
    //clear error message
    $("#skError").attr("class", "errorMessage hidden").html("");
    $("[id$=txtPasswordSK]").attr("class", "form-control");
    countCharNum();

    // start of events
    $(".softKeyboard").click(function () {
        //clear error message
        $("#skError").attr("class", "errorMessage hidden").html("");
        $("[id$=txtPasswordSK]").attr("class", "form-control");
    });

    $(".btnShowHide").click(function () {
        var $targetObj = $("[id$=txtPasswordSK]");
        if ('password' == $targetObj.attr('type')) {
            $targetObj.prop('type', 'text');
            $(this).html('<span class="glyphicon glyphicon-eye-open"></span>');
        } else {
            $targetObj.prop('type', 'password');
            $(this).html('<span class="glyphicon glyphicon-eye-close"></span>');
        }
    });

    $('.sk').on('click', function () {
        var inputVal = $(this).text();
        var $targetObj = $("[id$=txtPasswordSK]");
        var currPw = $targetObj.val();
        if (currPw.length < maxLen) {
            $targetObj.val(currPw + inputVal);
            countCharNum();
        }
    });

    //確定ボタンクリック
    $('.btnCommit').on('click', function () {
        var $targetObj = $("[id$=txtPasswordSK]");
        var currPw = $targetObj.val();

        //check empty error
        if (currPw.length < minLen) {
            $("#skError").attr("class", "errorMessage").html("パスワードを入力してください。");
            $targetObj.attr("class", "form-control input-error");
            return;
        }
        $("[id$=txtPassword]").val(currPw);
        $targetObj.val("");
        countCharNum();
        $("#skModal").modal('hide');
    });

    $('.btnBackKey').on('click', function () {
        backKey();
    });

    $('[id$=txtPasswordSK]').on('keyup', function () {
        countCharNum();
    });


});


//general layout
function drawKanaKeyboard() {
    var aryKana =
    Array('ア', 'カ', 'サ', 'タ', 'ナ', 'ハ', 'マ', 'ヤ', 'ラ', 'ワ', 'ガ', 'ザ', 'ダ', 'バ', 'パ', 'ァ', 'ャ',
'イ', 'キ', 'シ', 'チ', 'ニ', 'ヒ', 'ミ', '', 'リ', 'ヲ', 'ギ', 'ジ', 'ヂ', 'ビ', 'ピ', 'ィ', 'ュ',
'ウ', 'ク', 'ス', 'ツ', 'ヌ', 'フ', 'ム', 'ユ', 'ル', 'ン', 'グ', 'ズ', 'ヅ', 'ブ', 'プ', 'ゥ', 'ョ',
'エ', 'ケ', 'セ', 'テ', 'ネ', 'ヘ', 'メ', '', 'レ', 'ー', 'ゲ', 'ゼ', 'デ', 'ベ', 'ペ', 'ェ', 'ッ',
'オ', 'コ', 'ソ', 'ト', 'ノ', 'ホ', 'モ', 'ヨ', 'ロ', 'ヴ', 'ゴ', 'ゾ', 'ド', 'ボ', 'ポ', 'ォ', '');

    var breakPoint = 17;
    var k = 0;
    var str = '<table class="keyboard"><tr>';
    for (var i = 0; i < aryKana.length ; i++) {
        k = i % breakPoint;
        if (aryKana[i] == "") {
            str += "<td class='sk bg-disabled'>" + aryKana[i] + "</td>";
        }
        else if(k >= 10 && k < 14) {
                str += "<td class='sk bg-sk-2'>" + aryKana[i] + "</td>";
        }
        else if (k == 14) {
            str += "<td class='sk bg-sk-3'>" + aryKana[i] + "</td>";
        }
        else {
              str += "<td class='sk bg-sk-1'>" + aryKana[i] + "</td>";
        }
        
        if ((i+1) % breakPoint == 0) {
            str += "</tr><tr>";
        }
    }
    str += "</tr></table>";
    $('[id$=keyboardArea]').html(str);
}

//small size device layout
function drawKanaKeyboardSm() {
    var aryKana = Array('ア', 'カ', 'サ', 'タ', 'ナ', 'ハ', 'マ', 'ヤ', 'ラ',
'イ', 'キ', 'シ', 'チ', 'ニ', 'ヒ', 'ミ', '', 'リ',
'ウ', 'ク', 'ス', 'ツ', 'ヌ', 'フ', 'ム', 'ユ', 'ル',
'エ', 'ケ', 'セ', 'テ', 'ネ', 'ヘ', 'メ', '', 'レ',
'オ', 'コ', 'ソ', 'ト', 'ノ', 'ホ', 'モ', 'ヨ', 'ロ',
'ワ', 'ガ', 'ザ', 'ダ', 'バ', 'パ', 'ァ', 'ャ', '',
'ヲ', 'ギ', 'ジ', 'ヂ', 'ビ', 'ピ', 'ィ', 'ュ', '',
'ン', 'グ', 'ズ', 'ヅ', 'ブ', 'プ', 'ゥ', 'ョ', '',
'ー', 'ゲ', 'ゼ', 'デ', 'ベ', 'ペ', 'ェ', 'ッ', '',
'ヴ', 'ゴ', 'ゾ', 'ド', 'ボ', 'ポ', 'ォ', '', '');

    var breakPoint = 9;
    var k = 0; var dist = 0;
    var str = '<table class="keyboard"><tr>';
    for (var i = 0; i < aryKana.length ; i++) {
        dist = i % breakPoint;
        if (aryKana[i] == "") {
            str += "<td class='sk bg-disabled'>" + aryKana[i] + "</td>";
        }
        else if (k > 4 && k < 10 && dist > 0 && dist < 5) {
            str += "<td class='sk bg-sk-2'>" + aryKana[i] + "</td>";
        }
        else if (k >= 5 && dist == 5) {
            str += "<td class='sk bg-sk-3'>" + aryKana[i] + "</td>";
        }
        else {
            str += "<td class='sk bg-sk-1'>" + aryKana[i] + "</td>";
        }

        if ((i + 1) % breakPoint == 0) {
            str += "</tr><tr>";
            k++;
        }
    }
    str += "</tr></table>";
    $('[id$=keyboardArea]').html(str);
}


//表示文字数内容セット
function countCharNum() {
    $("#countCharArea").html($("[id$=txtPasswordSK]").val().length);
}

//一文字修正
function backKey() {
    var $targetObj = $("[id$=txtPasswordSK]");
    var currPw = $targetObj.val();
    var len = currPw.length;

    if (len > 0) {
        $targetObj.val(currPw.substring(0, len - 1));
        countCharNum();
    }
}