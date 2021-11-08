
LinkFish = {
    Ajax_exec: function (url, parameters, async, funcionSuccess) {
        var rsp;
        $.ajax({
            type: "POST",
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: async,
            data: JSON.stringify(parameters),
            success: function (response) {
                rsp = response;
                if (typeof (funcionSuccess) == 'function') {
                    funcionSuccess();
                }
            },
            failure: function (msg) {
                alert(msg); 
            },
            error: function (xhr, status, error) {
                alert(error); 
            }
        });
        return rsp;
    }
}