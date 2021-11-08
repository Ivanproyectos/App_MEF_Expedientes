$(document).ready(function () {

    $('body').on("click", ".modalMinem", function () {

        var url = $(this).attr('data-url');


        jQuery("#contenedor").html('');
        jQuery("#contenedor").load(url, function (responseText, textStatus, request) {
            $.validator.unobtrusive.parse('#contenedor');
            if (request.status != 200) return;
            //$('.contenedor').modal({ backdrop: true });
            //$('.contenedor').modal({ keyboard: true });
            $('.contenedor').modal('show');
            $(".modal-content").draggable({
                handle: ".modal-header", cursor: "move"
            })
        });


    });



    bootbox.setDefaults({
        locale: "es"
    });

    //$('body').on("click", ".modalSegundoMTC", function () {

    //    var url = $(this).attr('data-url');
    //    //$("").app
    //    $.get(url, function (data) {
    //        $('.seccionModalSegundo').html(data);
    //        $('.contenedorSegundo').modal('show');
    //        $(".modal-content").draggable({
    //            handle: ".modal-header", cursor: "move"
    //        })
    //    });
    //});

    //$.datepicker.regional['es'] =
    //{
    //    closeText: 'Cerrar',
    //    prevText: 'Previo',
    //    nextText: 'Próximo',

    //    monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio',
    //    'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
    //    monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun',
    //    'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
    //    monthStatus: 'Ver otro mes', yearStatus: 'Ver otro año',
    //    dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
    //    dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mie', 'Jue', 'Vie', 'Sáb'],
    //    dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sa'],
    //    dateFormat: 'dd/mm/yy', firstDay: 0,
    //    initStatus: 'Selecciona la fecha', isRTL: false
    //};

    //$.datepicker.setDefaults($.datepicker.regional['es']);

});