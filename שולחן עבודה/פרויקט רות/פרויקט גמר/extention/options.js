/*
(c) Copyright 2009-2016 iOpus Software GmbH - http://www.iopus.com
*/

"use strict";


function setSecurityLevel() {
    if (!Storage.isSet("encryption-type"))
        Storage.setChar("encryption-type", "no");
    let type = Storage.getChar("encryption-type");
    if (!/^(?:no|stored|tmpkey)$/.test(type))
        type = "no";
    let stored = Storage.getChar("stored-password");
    if (stored) {
        $("#stored-password-box").val(decodeURIComponent(atob(stored)));
    }

    switch(type) {
    case "no":
        $("#type_no").prop("checked", true);
        $("#stored-password-field").hide()
        $("#temp-password-field").hide()
        break;
    case "stored":
        $("#type_stored").prop("checked", true);
        $("#stored-password-field").show()
        $("#temp-password-field").hide()
        break;
    case "tmpkey":
        $("#type_tmpkey").prop("checked", true);
        $("#stored-password-field").hide()
        $("#temp-password-field").show()
        break;
    }
}

function onSecurityChage(e) {
    let type = e.target.id.substring(5)
    switch(type) {
    case "no":
        $("#stored-password-field").hide()
        $("#temp-password-field").hide()
        break;
    case "stored":
        $("#stored-password-field").show()
        $("#temp-password-field").hide()
        $("#stored-password-box").focus()
        $("#stored-password-box").select()
        break;
    case "tmpkey":
        $("#stored-password-field").hide()
        $("#temp-password-field").show()
        $("#temp-password-box").focus()
        $("#temp-password-box").select()
        break;
    }
    Storage.setChar("encryption-type", type)
}

function onPathChange(which) {
    Storage.setChar(which, $("#"+which).val());
}


function choosePath(which) {
    var features = "titlebar=no,menubar=no,location=no,"+
        "resizable=yes,scrollbars=no,status=no,"+
        "width=200,height=300";
    var win = window.open("browse.html", "iMacros_browse_dialog", features);
    
    win.args = {path: Storage.getChar(which), which: which};
}

function savePath(which, path) {
    Storage.setChar(which, path);
    $("#"+which).val(path);
}


function onAFLoginButton() {
    var features = "titlebar=no,menubar=no,location=no,"+
        "resizable=yes,scrollbars=no,status=no,"+
        "height=250,width=380";
    var win = window.open("AlertFoxLoginDialog.html",
                          "AlertFox Login Dialog", features);

}

window.addEventListener("load", function () {
    $("#show-before-play-dialog").prop(
        "checked", Storage.getBool("before-play-dialog")
    ).change(function(event) {
        let checked = event.target.checked
        Storage.setBool("before-play-dialog", checked)
    })

    $("#dock-panel").prop(
        "checked", Storage.getBool("dock-panel")
    ).change(function (event) {
        let checked = event.target.checked
        Storage.setBool("dock-panel", checked);
    })

    $("#enable-profiler").prop(
        "checked", Storage.getBool("profiler-enabled")
    ).change(function (event) {
        let checked = event.target.checked
        Storage.setBool("profiler-enabled", checked);
    })
    
    // paths
    $("#defsavepath").val(Storage.getChar("defsavepath"))
        .change(onPathChange.bind(null, "defsavepath"))
    $("#defsavepath-browse").click(choosePath.bind(null, "defsavepath"))
    $("#defdatapath").val(Storage.getChar("defdatapath"))
        .change(onPathChange.bind(null, "defdatapath"))
    $("#defdatapath-browse").click(choosePath.bind(null, 'defdatapath'))
    $("#defdownpath").val(Storage.getChar("defdownpath"))
        .change(onPathChange.bind(null, 'defdownpath'))
    $("#defdownpath-browse").click(choosePath.bind(null, 'defdownpath'))

    // encryption
    setSecurityLevel()
    $("#type_no").change(onSecurityChage);
    $("#type_stored").change(onSecurityChage);
    $("#type_tmpkey").change(onSecurityChage);
    $("#stored-password-box").on("input", function() {
        let pwd = $("#stored-password-box").val();
        pwd = btoa(encodeURIComponent(pwd));
        Storage.setChar("stored-password", pwd);
    })
    $("#temp-password-box").on("input", function() {
        let bg = chrome.extension.getBackgroundPage()
        bg.Rijndael.tempPassword = $("#temp-password-box").val()
    })
    $("#af-login-button").button().click(onAFLoginButton);

    // links
    $("#more-info-bp").click(function() {
        link('http://wiki.imacros.net/iMacros_for_Chrome#iMacros_as_Bookmarklets');
    });
    $("#more-info-profiler").click(function() {
        link('http://www.iopus.com/imacros/home/cr/rd.asp?helpid=profiler');
    });
    $("#password-tool-page").click(function() {
        link("http://demo.imacros.net/PasswordTool.aspx");
    });
    $("#more-info-encryption").click(function() {
        link('http://wiki.imacros.net/!ENCRYPTION');
    });
    $("#whatis-af").click(function() {
        link('http://imacros.net/about/alertfox');
    });

    // record modes
    var record_modes = ["conventional", "event"];
    var record_radio = $("#record-mode-"+Storage.getChar("record-mode"));
    if (!record_radio) {
        alert("Unknown record mode type: "+Storage.getChar("record-mode"))
    } else {
        record_radio.prop("checked", true)
        for (let r of record_modes) {
            $("#record-mode-"+r).change(function(e) {
                Storage.setChar("record-mode", e.target.id.substring(12))
            });
        }
    }

    $("#more-info-event").click(function() {
        link("http://wiki.imacros.net/EVENT")
    })

    $("#favorid-panel").prop(
        "checked", Storage.getBool("recording-prefer-id")
    ).change(function(e) {
        Storage.setBool("recording-prefer-id", e.target.checked)
    })
    
    $("#css-selectors").prop(
        "checked", Storage.getBool("recording-prefer-css-selectors")
    ).change(function(e) {
        Storage.setBool("recording-prefer-css-selectors", e.target.checked)
    })
});
