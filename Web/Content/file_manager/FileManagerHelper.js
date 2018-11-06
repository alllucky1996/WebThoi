var myWindow;
function showFileManagerPage(url, id) {
    myWindow = window.open(url, "_blank", "toolbar=no, scrollbars=no, resizable=no, top=0, left=0, width=1000, height=500");    // Opens a new window
}

function choseFile(id, url) {
    $("#" + id).val(url);
    myWindow.close();
}