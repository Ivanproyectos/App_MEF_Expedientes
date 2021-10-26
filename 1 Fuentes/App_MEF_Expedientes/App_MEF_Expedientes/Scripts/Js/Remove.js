function Remove_RemoverClases(li_seleccionado) {
    var input_li = document.getElementById('menuPrincipal').getElementsByTagName('li');
    if (input_li.length > 0) {
        $.each(input_li, function (i, v) {
            if (v.id != "") {
                $("#" + v.id + " ul").css("display", "none");
                $("#" + v.id).removeClass("active open");
            }
        });
    }
    var Mi_li = $("#" + li_seleccionado);
    if (Mi_li.length > 0) {
        $("#" + Mi_li[0].id).addClass("active open");
        var Texto_Padre = "Seguridad";
        var dsa = Mi_li[0].parentNode.parentNode.id;
        if (Mi_li[0].parentNode.parentNode.id != "menuPrincipal") {
            Texto_Padre = Mi_li[0].parentNode.parentNode.outerText;
            $("#" + Mi_li[0].parentNode.parentNode.id).addClass("active open");
            $("#" + Mi_li[0].parentNode.parentNode.id + " ul").css("display", "block");
        } else {

        }
        $("#lbNombreMenuPrincipal").text(Texto_Padre);
        $("#lbNombreMenuSecundario").text(Mi_li[0].outerText);

    }
}