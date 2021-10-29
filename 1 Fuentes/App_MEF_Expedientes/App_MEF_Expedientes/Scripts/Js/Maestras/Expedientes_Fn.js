// BUSCAR OFICINA //////////////////////////////////////////////////////////////

$('#SEARCH_REMITENTE').autocomplete(
    {
        source: function (request, response) {

            $.getJSON(url_autocomplete_oficina, { DESC_OFICINA: request.term.toUpperCase() },
                response);
        },
        minLength: 4,
        select: function (event, ui) {
            $("#SEARCH_REMITENTE").val(ui.item.ACRONIMO +' ' + ui.item.DESC_OFICINA );
            $("#hdfID_ID_DOCUMENTO_REF").val(ui.item.ID_OFICINA);
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