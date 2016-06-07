window.onload = function () {
    document.getElementById("omega").onclick = function fun() {
        senderFunct();
    }
}


function senderFunct() {

    var controllerList = document.getElementsByClassName("ctrl");
    var actionList = document.getElementsByClassName("act");
    var paramList = document.getElementsByClassName("pmts");
    var retList = document.getElementsByClassName("ret");
    var chkList = document.getElementsByClassName("chk");

    var count = document.getElementById("actionCount").innerHTML;

    var jsonString = "[";

    for (var i = 0; i < count; ++i) {
        jsonString += "{" + "\"Controller\":\"" + controllerList[i].innerHTML + "\",";
        jsonString += "\"Action\":\"" + actionList[i].innerHTML + "\",";

        var paramArr = paramList[i].innerHTML.split(",");

        jsonString += "\"Parameters\":[";

        for (var j = 0; j < paramArr.length; j++) {
            jsonString += "\"" + paramArr[j].trim() + "\"";

            if (j != paramArr.length - 1) {
                jsonString += ",";
            }
        }

        jsonString += "],";
        jsonString += "\"ReturnType\":\"" + retList[i].innerHTML + "\",";

        jsonString += "\"disabledStatus\" : " + chkList[i].disabled + ",";

        jsonString += "\"checkedStatus\" : " + chkList[i].checked + "}";

        if (i != count - 1) {
            jsonString += ",";
        }
    }

    jsonString += "]";


    var id = document.getElementById("userId").innerHTML;

    var tempModel = 
        {
            "UserID" : id,
            "PrivilageStructs" : "",
            "Count" : "",
            "ReturnPriv": jsonString
        }

    $.ajax({
        url: '../SavePrivilages',
        type: 'POST',
        data: JSON.stringify(tempModel),
        dataType: 'json',
        crossDomain: true,
        contentType: 'application/json; charset=utf-8',
        complete: function () {
            alert("Access Privilages Updated");
        },
        async: true,
        processData: false
    });

}