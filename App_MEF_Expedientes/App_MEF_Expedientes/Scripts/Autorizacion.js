

Autorizacion = {
    Ajax: function (url, parameters, async, funcionSuccess) {
        var rsp;
        $.ajax({
            type: "POST",
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: async,
            data: JSON.stringify(parameters),
            success: function (response) {
                debugger;
                rsp = response; 
                if (!response.AUTORIZADO) {
                    rsp = null;
                    window.location = response.URL;
                }
                if (typeof (funcionSuccess) == 'function') {
                    funcionSuccess(response);
                }
            },
            failure: function (msg) {
                alert(msg);
                rsp = msg;
            },
            error: function (xhr, status, error) {
                alert(error);
                rsp = error;
            }
        });

        return rsp;
    },

    InvocarVista: function (url, item) {
        $.ajax({
            url: url + "?parametro=" + JSON.stringify(item),
            dataType: 'html',
            type: 'post',
            contentType: 'text/xml; charset=utf-8',
            async: false,
            success: function (result) { 
                var asdsa = result.substr(0, 1);
                if (asdsa == "{") {
                    var json = $.parseJSON(result); 
                    if (!json.AUTORIZADO) { 
                        window.location = json.URL;
                    }   
                } else {
                    $('#divProceso').html("");
                    $('#divProceso').html(result);
                }
                
            },
            error: function (request, status, error) {
                $('#divProceso').show();
            }
        });
    }
}