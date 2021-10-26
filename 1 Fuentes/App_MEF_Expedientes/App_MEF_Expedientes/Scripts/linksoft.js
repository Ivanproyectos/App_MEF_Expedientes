var seconds = 420;  //..ajustar acorde al tiempo límite de inactividad

function countDown() {
    if (seconds == 0) {
        //document.getElementById("btnSalir").click();
        alert('Tiempo expirado!');
        document.getElementById('logoutForm').submit();
       // alert('Tiempo expirado!');
        self.close();
    }
    seconds--;
  //  document.getElementById("counter").innerHTML = "<b>" + seconds.toString() + "</b>";
    document.getElementById("hdfcounter").value = seconds;
    window.setTimeout("countDown()", 1000);
}

function resetCounter() {
    //window.setTimeout("countDown()", 0);
    seconds = 420;
    //document.getElementById("counter").innerHTML = "<b>" + seconds.toString() + "</b>";
    //countDown();
}

function panelHidden(ctrl) {
    if (ctrl == null) $.unblockUI();
    else $("#" + ctrl).unblock();
    document.body.style.cursor = 'cross';
}
/*************************************************/
function panelLoanding(msg, ctrl) {
    if (ctrl == null) {
        $.blockUI(
                {
                    baseZ: 99999,
                    message: '<img src="' + baseUrl + 'assets/images/loading.gif' + '" style="width: 30px; height: 30px;" /> <h1 style="font-size:11px">' + (msg == null ? '' : msg == 'default' ? '<b>Procesando, por favor, espere</b>' : msg) + '</h1>'
                    , css: {
                        border: msg == null || msg.trim() == '' ? 'none' : '3px solid #dbe1eb',
                        backgroundColor: msg == null || msg.trim() == '' ? 'none' : '#fff'
                    }
                });
    }
    else {
        $("#" + ctrl).block({
            message: '<img src="' + baseUrl + 'assets/images/loading.gif' + '" style="width: 30px; height: 30px;" /> <h1 style="font-size:11px">' + (msg == null ? '' : msg == 'default' ? 'Procesando, por favor, espere' : msg) + '</h1>'
                    , css: {
                        border: msg == null || msg.trim() == '' ? 'none' : '3px solid #dbe1eb',
                        backgroundColor: msg == null || msg.trim() == '' ? 'none' : '#fff'
                         
                    }
        })
    }
}