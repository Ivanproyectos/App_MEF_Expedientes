// BUSCAR OFICINA //////////////////////////////////////////////////////////////

$('#SEARCH_REMITENTE').autocomplete(
    {
        source: function (request, response) {

            $.getJSON(url_autocomplete_oficina, { DESC_OFICINA: request.term.toUpperCase() },
                response);
        },
        minLength: 4,
        select: function (event, ui) {
            $("#SEARCH_REMITENTE").val( ui.item.DESC_OFICINA );
            $("#ID_REMITENTE").val(ui.item.ID_OFICINA);
            return false;
        },
        response: function (event, ui) {
            $("#SEARCH_REMITENTE").val('');
            return false;
        }
    })
    .data("autocomplete")._renderItem = function (ul, item) {
        debugger;
        return $("<li></li>")
            .data("item.autocomplete", item)
            .append($("<a></a>").html(item.DESC_OFICINA))
            .appendTo(ul);
    }

///////////////////////////////////************************ //////////////////////////////////////////////////////////////



// BUSCAR PERSONAL//////////////////////////////////////////////////////////////

$('#SEARCH_PERSONAL').autocomplete(
    {
        source: function (request, response) {

            $.getJSON(url_autocomplete_personal, { NOMBRES_APE: request.term.toUpperCase() },
                response);
        },
        minLength: 4,
        select: function (event, ui) {
            $("#SEARCH_PERSONAL").val(ui.item.NOMBRE_COMPLETO);
            $("#REGIMEN_LABORAL").val(ui.item.REGIMEN_LABORAL);
            $("#OFICINA").val(ui.item.OFICINA);

            $("#ID_PERSONAL").val(ui.item.ID_PERSONAL);
            return false;
        },
        response: function (event, ui) {
            $("#SEARCH_PERSONAL").val('');
            return false;
        }
    })
    .data("autocomplete")._renderItem = function (ul, item) {
        debugger;
        return $("<li></li>")
            .data("item.autocomplete", item)
            .append($("<a></a>").html(item.NOMBRE_COMPLETO))
            .appendTo(ul);
    }

///////////////////////////////////************************ //////////////////////////////////////////////////////////////


// BUSCAR ORGNAO SANCIONADOR //////////////////////////////////////////////////////////////

$('#SEARCH_ORGANO_SANCIONADOR').autocomplete(
    {
        source: function (request, response) {

            $.getJSON(url_autocomplete_oficina, { DESC_OFICINA: request.term.toUpperCase() },
                response);
        },
        minLength: 4,
        select: function (event, ui) {
            $("#SEARCH_ORGANO_SANCIONADOR").val(ui.item.DESC_OFICINA);
            $("#ID_ORGANO_SANCIONADOR").val(ui.item.ID_OFICINA);
            return false;
        },
        response: function (event, ui) {
            $("#SEARCH_ORGANO_SANCIONADOR").val('');
            return false;
        }
    })
    .data("autocomplete")._renderItem = function (ul, item) {
        return $("<li></li>")
            .data("item.autocomplete", item)
            .append($("<a></a>").html(item.DESC_OFICINA))
            .appendTo(ul);
    }

///////////////////////////////////************************ //////////////////////////////////////////////////////////////



// BUSCAR ORGNAO INSTRUCTOR //////////////////////////////////////////////////////////////

$('#SEARCH_ORGANO_INSTRUCTOR').autocomplete(
    {
        source: function (request, response) {

            $.getJSON(url_autocomplete_oficina, { DESC_OFICINA: request.term.toUpperCase() },
                response);
        },
        minLength: 4,
        select: function (event, ui) {
            $("#SEARCH_ORGANO_INSTRUCTOR").val(ui.item.DESC_OFICINA);
            $("#ID_ORGANO_INSTRUCTOR").val(ui.item.ID_OFICINA);
            return false;
        },
        response: function (event, ui) {
            $("#SEARCH_ORGANO_INSTRUCTOR").val('');
            return false;
        }
    })
    .data("autocomplete")._renderItem = function (ul, item) {
        debugger;
        return $("<li></li>")
            .data("item.autocomplete", item)
            .append($("<a></a>").html(item.DESC_OFICINA))
            .appendTo(ul);
    }

///////////////////////////////////************************//////////////////////////////////////////////////////////////





