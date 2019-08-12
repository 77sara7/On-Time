/*
(c) Copyright 2010 iOpus Software GmbH - http://www.iopus.com
*/

function ok() {
    window.close();
}

window.addEventListener("beforeunload", function() {
    args.mplayer.waitingForExtract = false;
    args.mplayer.next("extractDialog");
    return null;
});

window.addEventListener("load", function(evt) {
    var field = document.getElementById("data-field");
    field.focus();
    if (args) {
        field.value = args.data;
        field.select();
    }

    document.getElementById("ok-button").addEventListener("click", ok);
    resizeToContent(window, document.getElementById('container'));
});
