var Correlativo_Expediente_grilla = 'Correlativo_Expediente_grilla';
var Correlativo_Expediente_barra = 'Correlativo_Expediente_barra';


function LimpiarCorrelativo_Expediente() {
    $("#txt_DescripcionCorta").val('');
    $("#txt_DescripcionLarga").val('');
    $('#CBOESTADO').val('');
    Correlativo_Expediente_CargarGrilla();
}

function Correlativo_Expediente_ConfigurarGrilla() {
    $("#" + Correlativo_Expediente_grilla).GridUnload();
    var colNames = ['Editar', 'Eliminar', 'Activo', 'ID_DOMINIO', 'Año', 'Número Expediente', 'Correlativo'
        , 'FLG_ESTADO', 'Usuario Creación', 'Fecha Creación', 'Usuario Modificación', 'Fecha Modificación'];
    var colModels = [
        { name: 'EDITAR', index: 'EDITAR', align: 'center', width: 60, hidden: false, sortable: false, formatter: Correlativo_Expediente_actionEditar },
        { name: 'ELIMINAR', index: 'ELIMINAR', align: 'center', width: 70, hidden: false, sortable: false, formatter: Correlativo_Expediente_actionEliminar },
        { name: 'ACTIVO', index: 'ACTIVO', align: 'center', width: 55, hidden: false, sortable: false, formatter: Correlativo_Expediente_actionActivo },
        { name: 'ID_DOMINIO', index: 'ID_DOMINIO', align: 'center', width: 50, hidden: true },
        { name: 'COD_DOMINIO', index: 'COD_DOMINIO', align: 'center', width: 200, hidden: false },
        { name: 'DESC_CORTA_DOMINIO', index: 'DESC_CORTA_DOMINIO', align: 'center', width: 300, hidden: false, sortable: false },
        { name: 'DESC_LARGA_DOMINIO', index: 'DESC_LARGA_DOMINIO', align: 'center', width: 300, hidden: false },
        { name: 'FLG_ESTADO', index: 'FLG_ESTADO', align: 'center', width: 140, hidden: true, sortable: true },
        { name: 'USU_CREACION', index: 'USU_CREACION', align: 'center', width: 140, hidden: false, sortable: true },
        { name: 'FEC_CREACION', index: 'FEC_CREACION', align: 'center', width: 160, hidden: false, sortable: true, formatter: 'date', formatoptions: { srcformat: 'd/m/Y h:i A', newformat: 'd/m/Y h:i A' } },
        { name: 'USU_MODIFICACION', index: 'USU_MODIFICACION', align: 'center', width: 150, hidden: false, sortable: true },
        { name: 'FEC_MODIFICACION', index: 'FEC_MODIFICACION', align: 'center', width: 160, hidden: false, sortable: true, formatter: 'date', formatoptions: { srcformat: 'd/m/Y h:i A', newformat: 'd/m/Y h:i A' } },
    ];
    var opciones = {
        GridLocal: true, multiselect: false, CellEdit: false, Editar: false, nuevo: false, eliminar: false, search: false, sort: 'DESC',

    };
    SICA.Grilla(Correlativo_Expediente_grilla, Correlativo_Expediente_barra, '', '400', '', "Lista de Correlativo Expediente", '', 'ID_DOMINIO', colNames, colModels, 'ID_DOMINIO', opciones);
}




function Correlativo_Expediente_CargarGrilla() {
    $('#Grilla_Load').show();
    var item =
    {
        COD_DOMINIO: $("#txt_DescripcionCorta").val(),
        //DESC_LARGA_DOMINIO: $("#txt_DescripcionLarga").val(),
        FLG_ESTADO: $("#CBOESTADO").val(),
        NOM_DOMINIO: 'NUM_EXP' // Correlativo_Expediente
    };
    var url = baseUrl + 'Administracion/Correlativo_Expediente/Correlativo_Expediente_Listar';

    var auditoria = Autorizacion.Ajax(url, item, false);
    jQuery("#" + Correlativo_Expediente_grilla).jqGrid('clearGridData', true).trigger("reloadGrid");
    if (auditoria != null) {
        if (auditoria.EJECUCION_PROCEDIMIENTO) {
            $.each(auditoria.OBJETO, function (i, v) {
                var rowKey = jQuery("#" + Correlativo_Expediente_grilla).getDataIDs();
                var ix = rowKey.length;
                ix++;
                var myData =
                {
                    CODIGO: ix,
                    ID_DOMINIO: v.ID_DOMINIO,
                    COD_DOMINIO: v.COD_DOMINIO,
                    DESC_CORTA_DOMINIO: v.DESC_CORTA_DOMINIO,
                    DESC_LARGA_DOMINIO: v.DESC_LARGA_DOMINIO,
                    FLG_ESTADO: v.FLG_ESTADO,
                    USU_CREACION: v.USU_CREACION,
                    FEC_CREACION: v.FEC_CREACION,
                    USU_MODIFICACION: v.USU_MODIFICACION,
                    FEC_MODIFICACION: v.FEC_MODIFICACION

                };
                jQuery("#" + Correlativo_Expediente_grilla).jqGrid('addRowData', v.ID_DOMINIO, myData);
            });
            jQuery("#" + Correlativo_Expediente_grilla).trigger("reloadGrid");
            $('#Grilla_Load').hide();
        } else {
            jAlert(auditoria.MENSAJE_SALIDA, 'Atención');
            $('#Grilla_Load').hide();
        }
    }
}







function Correlativo_Expediente_actionEditar(cellvalue, options, rowObject) {
    var _btn = "<button title='Editar' onclick='Correlativo_Expediente_Editar(" + rowObject.ID_DOMINIO + ");' class=\"btn btn-link\" type=\"button\" data-toggle=\"modal\"  style=\"text-decoration: none !important;\" data-target=\"#myModalNuevo\"> <i class=\"clip-pencil-3\" style=\"color:#e68c1b;font-size:17px\"></i></button>";
    return _btn;
}



function Correlativo_Expediente_actionEliminar(cellvalue, options, rowObject) {
    var _btn = "<button title='Eliminar' onclick=\"Correlativo_Expediente_Eliminar(" + rowObject.ID_DOMINIO + ");\" class=\"btn btn-link\" type=\"button\" style=\"text-decoration: none !important;\"> <i class=\"clip-cancel-circle-2\" style=\"color:#c35245;font-size:17px\"></i></button>";
    return _btn;
}


function Correlativo_Expediente_actionActivo(cellvalue, options, rowObject) {
    var check_ = 'check';
    if (rowObject.FLG_ESTADO == 1)
        check_ = 'checked';

    var _btn = "<label class=\"switch\">"
        + "<input id=\"Correlativo_Expediente_chk_" + rowObject.ID_DOMINIO + "\" type=\"checkbox\" onchange=\"Correlativo_Expediente_CambiarCorrelativo_Expediente(" + rowObject.ID_DOMINIO + ",this)\" " + check_ + ">"
        + "<span class=\"slider round\"></span>"
        + "</label>";
    return _btn;
}





function Correlativo_Expediente_Editar(ID_DOMINIO) {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Administracion/Correlativo_Expediente/Mantenimiento?id=" + ID_DOMINIO + "&Accion=M", function (responseText, textStatus, request) {
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}

function Correlativo_Expediente_Nuevo() {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Administracion/Correlativo_Expediente/Mantenimiento?id=0&Accion=N", function (responseText, textStatus, request) {
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}



function Correlativo_Expediente_Actualizar() {
    if ($("#frmMantenimientoCorrelativo_Expediente").valid()) {
        var item =
        {
            ID_DOMINIO: $("#hdfID_DOMINIO").val(),
            COD_DOMINIO: $("#ANIO").val(),
            DESC_CORTA_DOMINIO: $("#NUMERO_EXPEDIENTE").val(),
            DESC_LARGA_DOMINIO: $("#CORRELATIVO").val(),
            USU_MODIFICACION: $("#inputHddcod_usuario").val(),
            TIPO: $("#HDF_Tipo_Correlativo_Expediente").val(),
            Accion: $("#AccionCorrelativo_Expediente").val()
        };
        jConfirm("¿ Desea actualizar este tipo correlativo expediente ?", "Atención", function (r) {
            if (r) {
                var url = baseUrl + 'Administracion/Correlativo_Expediente/Correlativo_Expediente_Actualizar';
                var auditoria = Autorizacion.Ajax(url, item, false);
                if (auditoria != null) {
                    if (auditoria.EJECUCION_PROCEDIMIENTO) {
                        if (!auditoria.RECHAZAR) {
                            Correlativo_Expediente_CargarGrilla();
                            jAlert("Datos actualizados satisfactoriamente", "Proceso");
                            $('#myModalNuevo').modal('hide');
                            jQuery("#myModalNuevo").html('');
                        } else {
                            jAlert(auditoria.MENSAJE_SALIDA, 'Atención');
                        }
                    } else {
                        jAlert(auditoria.MENSAJE_SALIDA, 'Atención');
                    }
                }
            }
        });

    }
}







/*********************************************** ----------------- *************************************************/

/************************************************* Nuevo Correlativo_Expediente ***************************************************/



function Correlativo_Expediente_Registrar() {
    if ($("#frmMantenimientoCorrelativo_Expediente").valid()) {
        if ($('#AccionCorrelativo_Expediente').val() == "M") {
            Correlativo_Expediente_Actualizar();
        } else {
            jConfirm("¿Desea guardar este registro ?", "Atención", function (r) {
                if (r) {
                    var item =
                    {
                        ID_DOMINIO_PADRE: 5, //CODITO TABLA Correlativo_Expediente
                        NOM_DOMINIO: 'NUM_EXP', // CODITO Correlativo_Expediente
                        COD_DOMINIO: $("#ANIO").val(),
                        DESC_CORTA_DOMINIO: $("#NUMERO_EXPEDIENTE").val(),
                        DESC_LARGA_DOMINIO: $("#CORRELATIVO").val(),
                        USU_CREACION: $("#inputHddcod_usuario").val(),
                        Accion: $("#AccionCorrelativo_Expediente").val()
                    };
                    var url = baseUrl + 'Administracion/Correlativo_Expediente/Correlativo_Expediente_Insertar';
                    var auditoria = Autorizacion.Ajax(url, item, false);
                    if (auditoria != null) {
                        if (auditoria.EJECUCION_PROCEDIMIENTO) {
                            if (!auditoria.RECHAZAR) {
                                Correlativo_Expediente_CargarGrilla();
                                jAlert("Datos guardados satisfactoriamente", "Proceso");
                                $('#myModalNuevo').modal('hide');
                                jQuery("#myModalNuevo").html('');
                            } else {
                                jAlert(auditoria.MENSAJE_SALIDA, 'Atención');
                            }
                        } else {
                            jAlert(auditoria.MENSAJE_SALIDA, 'Atención');
                        }
                    }
                }
            });
        }
    }
}



/*********************************************** ----------------- *************************************************/

/*********************************************** Cambia el Correlativo_Expediente *************************************************/



function Correlativo_Expediente_CambiarCorrelativo_Expediente(ID_DOMINIO, MiCheck) {
    var url = baseUrl + 'Administracion/Correlativo_Expediente/Correlativo_Expediente_Correlativo_Expediente';
    var item = {
        ID_DOMINIO: ID_DOMINIO,
        FLG_ESTADO: MiCheck.checked == true ? 1 : 0,
        USU_MODIFICACION: $('#inputHddcod_usuario').val()
    };
    var auditoria = Autorizacion.Ajax(url, item, false);
    if (auditoria != null && auditoria != "") {
        if (auditoria.EJECUCION_PROCEDIMIENTO) {
            if (!auditoria.RECHAZAR) {
                //$("#" + Correlativo_Expediente_grilla).jqGrid('setCell', data.CODIGO, 'FLG_ESTADO', MiCheck.checked ? '1' : '0');
            } else {
                jAlert(auditoria.MENSAJE_SALIDA, 'Atención');
            }
        } else {
            jAlert(auditoria.MENSAJE_SALIDA, 'Atención');
        }
    }
}

/*********************************************** ----------------- *************************************************/

/************************************************ Elimina Correlativo_Expediente **************************************************/


function Correlativo_Expediente_Eliminar(ID_DOMINIO) {
    //var data = jQuery("#" + Correlativo_Expediente_grilla).jqGrid('getRowData', CODIGO);
    jConfirm("¿ Desea eliminar este tipo Correlativo_Expediente ?", "Atención", function (r) {
        if (r) {
            var url = baseUrl + 'Administracion/Correlativo_Expediente/Correlativo_Expediente_Eliminar';
            var item = {
                ID_DOMINIO: ID_DOMINIO,
            };
            var auditoria = Autorizacion.Ajax(url, item, false);
            if (auditoria != null) {
                if (auditoria.EJECUCION_PROCEDIMIENTO) {
                    if (!auditoria.RECHAZAR) {
                        jAlert("Tipo Archivo eliminado", "Proceso");
                        Correlativo_Expediente_CargarGrilla();
                    } else {
                        jAlert(auditoria.MENSAJE_SALIDA, 'Atención');
                    }
                }
                else {
                    jAlert(auditoria.MENSAJE_SALIDA, 'Atención');
                }
            }
        }
    });
}
