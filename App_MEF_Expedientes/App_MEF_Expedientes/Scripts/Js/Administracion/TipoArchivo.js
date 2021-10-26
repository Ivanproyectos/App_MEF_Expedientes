var TipoArchivo_grilla = 'TipoArchivo_grilla';
var TipoArchivo_barra = 'TipoArchivo_barra';


//$(document).ready(function () {
//    TipoArchivo_ConfigurarGrilla();
//    //TipoArchivo_CargarGrilla();
//});

function LimpiarTipoArchivo() {
    $("#txt_empresa").val('');
    $("#txt_Ruc").val('');
    $('#CBOESTADO').val('');
    TipoArchivo_ConfigurarGrilla();
}

function TipoArchivo_ConfigurarGrilla() {
    $("#" + TipoArchivo_grilla).GridUnload();
    var colNames = ['Editar', 'Eliminar', 'Activo', 'ID_DOMINIO', 'Código', 'Descripción Corta', 'Descripción Larga'
        , 'FLG_ESTADO', 'Usuario Creación', 'Fecha Creación', 'Usuario Modificación', 'Fecha Modificación'];
    var colModels = [
        { name: 'EDITAR', index: 'EDITAR', align: 'center', width: 60, hidden: false, sortable: false, formatter: TipoArchivo_actionEditar },
        { name: 'ELIMINAR', index: 'ELIMINAR', align: 'center', width: 70, hidden: false, sortable: false, formatter: TipoArchivo_actionEliminar },
        { name: 'ACTIVO', index: 'ACTIVO', align: 'center', width: 55, hidden: false, sortable: false, formatter: TipoArchivo_actionActivo },
        { name: 'ID_DOMINIO', index: 'ID_DOMINIO', align: 'center', width: 50, hidden: true },
        { name: 'COD_DOMINIO', index: 'COD_DOMINIO', align: 'center', width: 200, hidden: false },
        { name: 'DESC_CORTA_DOMINIO', index: 'DESC_CORTA_DOMINIO', align: 'center', width: 300, hidden: false, sortable: false },
        { name: 'DESC_LARGA_DOMINIO', index: 'DESC_LARGA_DOMINIO', align: 'center', width: 300, hidden: false },
        { name: 'FLG_ESTADO', index: 'FLG_ESTADO', align: 'center', width: 140, hidden: true, sortable: true },
        { name: 'USU_CREACION', index: 'USU_CREACION', align: 'center', width: 140, hidden: false, sortable: true },
        { name: 'FEC_CREACION', index: 'FEC_CREACION', align: 'center', width: 160, hidden: false, sortable: true },
        { name: 'USU_MODIFICACION', index: 'USU_MODIFICACION', align: 'center', width: 150, hidden: false, sortable: true },
        { name: 'FEC_MODIFICACION', index: 'FEC_MODIFICACION', align: 'center', width: 160, hidden: false, sortable: true },
    ];
    var opciones = {
        GridLocal: true, multiselect: false, CellEdit: false, Editar: false, nuevo: false, eliminar: false, search: false, sort: 'DESC',

    };
    SICA.Grilla(TipoArchivo_grilla, TipoArchivo_barra, '', '400', '', "Lista de Tipo Archivo", '', 'ID_DOMINIO', colNames, colModels, 'ID_DOMINIO', opciones);
}






function TipoArchivo_actionEditar(cellvalue, options, rowObject) {
    var _btn = "<button title='Editar' onclick='TipoArchivo_Editar(" + rowObject.ID_DOMINIO + ");' class=\"btn btn-link\" type=\"button\" data-toggle=\"modal\"  style=\"text-decoration: none !important;\" data-target=\"#myModalNuevo\"> <i class=\"clip-pencil-3\" style=\"color:#e68c1b;font-size:17px\"></i></button>";
    return _btn;
}



function TipoArchivo_actionEliminar(cellvalue, options, rowObject) {
    var _btn = "<button title='Eliminar' onclick=\"TipoArchivo_Eliminar(" + rowObject.ID_DOMINIO + ");\" class=\"btn btn-link\" type=\"button\" style=\"text-decoration: none !important;\"> <i class=\"clip-cancel-circle-2\" style=\"color:#c35245;font-size:17px\"></i></button>";
    return _btn;
}


function TipoArchivo_actionActivo(cellvalue, options, rowObject) {
    var check_ = 'check';
    if (rowObject.FLG_ESTADO == 1)
        check_ = 'checked';

    var _btn = "<label class=\"switch\">"
        + "<input id=\"TipoArchivo_chk_" + rowObject.ID_DOMINIO + "\" type=\"checkbox\" onchange=\"TipoArchivo_CambiarEstado(" + rowObject.ID_DOMINIO + "this)" + check_ + ">"
        + "<span class=\"slider round\"></span>"
        + "</label>";
    return _btn;
}





function TipoArchivo_Editar(ID_DOMINIO) {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Administracion/TipoArchivo/Mantenimiento?id=" + ID_DOMINIO + "&Accion=M", function (responseText, textStatus, request) {
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}

function TipoArchivo_Nuevo() {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Administracion/TipoArchivo/Mantenimiento?id=0&Accion=N", function (responseText, textStatus, request) {
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}



function TipoArchivo_Actualizar() {
    if ($("#frmMantenimientoTipoArchivo").valid()) {
        var item =
        {
            ID_DOMINIO: $("#hdfID_DOMINIO").val(),
            COD_DOMINIO: $("#COD_DOMINIO").val(),
            DESC_CORTA_DOMINIO: $("#DESC_CORTA_DOMINIO").val(),
            DESC_LARGA_DOMINIO: $("#DESC_LARGA_DOMINIO").val(),
            USU_MODIFICACION: $("#inputHddcod_usuario").val(),
            TIPO: $("#HDF_Tipo_TipoArchivo").val(),
            Accion: $("#AccionTipoArchivo").val()
        };
        jConfirm("¿ Desea actualizar este tipo archivo ?", "Atención", function (r) {
            if (r) {
                var url = baseUrl + 'Administracion/TipoArchivo/TipoArchivo_Actualizar';
                var auditoria = Autorizacion.Ajax(url, item, false);
                if (auditoria != null) {
                    if (auditoria.EJECUCION_PROCEDIMIENTO) {
                        if (!auditoria.RECHAZAR) {
                            TipoArchivo_CargarGrilla();
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





function TipoArchivo_CargarGrilla() {
    $('#Grilla_Load').show();
    var item =
    {
        DESC_CORTA_DOMINIO: $("#txt_desc_corta").val(),
        FLG_ESTADO: $("#CBOESTADO").val(),
    };
    var url = baseUrl + 'Administracion/TipoArchivo/TipoArchivo_Listar';

    var auditoria = Autorizacion.Ajax(url, item, false);
    jQuery("#" + TipoArchivo_grilla).jqGrid('clearGridData', true).trigger("reloadGrid");
    if (auditoria != null) {
        if (auditoria.EJECUCION_PROCEDIMIENTO) {
            $.each(auditoria.OBJETO, function (i, v) {
                var rowKey = jQuery("#" + TipoArchivo_grilla).getDataIDs();
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
                jQuery("#" + TipoArchivo_grilla).jqGrid('addRowData', v.ID_DOMINIO, myData);
            });
            jQuery("#" + TipoArchivo_grilla).trigger("reloadGrid");
            $('#Grilla_Load').hide();
        } else {
            jAlert(auditoria.MENSAJE_SALIDA, 'Atención');
            $('#Grilla_Load').hide();
        }
    }
}







/*********************************************** ----------------- *************************************************/

/************************************************* Nuevo TipoArchivo ***************************************************/



function TipoArchivo_Registrar() {
    if ($("#frmMantenimientoTipoArchivo").valid()) {
        if ($('#AccionTipoArchivo').val() == "M") {
            TipoArchivo_Actualizar();
        } else {
            jConfirm("¿Desea guardar este registro ?", "Atención", function (r) {
                if (r) {
                    var item =
                    {
                        COD_DOMINIO: $("#COD_DOMINIO").val(),
                        DESC_CORTA_DOMINIO: $("#DESC_CORTA_DOMINIO").val(),
                        DESC_LARGA_DOMINIO: $("#DESC_LARGA_DOMINIO").val(),
                        USU_CREACION: $("#inputHddcod_usuario").val(),
                        Accion: $("#AccionTipoArchivo").val()
                    };
                    var url = baseUrl + 'Administracion/TipoArchivo/TipoArchivo_Insertar';
                    var auditoria = Autorizacion.Ajax(url, item, false);
                    if (auditoria != null) {
                        if (auditoria.EJECUCION_PROCEDIMIENTO) {
                            if (!auditoria.RECHAZAR) {
                                TipoArchivo_CargarGrilla();
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

/*********************************************** Cambia el estado *************************************************/



function TipoArchivo_CambiarEstado(ID_DOMINIO, MiCheck) {
    var url = baseUrl + 'Administracion/TipoArchivo/TipoArchivo_Estado';
    var item = {
        ID_DOMINIO: ID_DOMINIO,
        FLG_ESTADO: MiCheck.checked == true ? 1 : 0,
        USU_MODIFICACION: $('#inputHddcod_usuario').val()
    };
    var auditoria = Autorizacion.Ajax(url, item, false);
    if (auditoria != null && auditoria != "") {
        if (auditoria.EJECUCION_PROCEDIMIENTO) {
            if (!auditoria.RECHAZAR) {
                //$("#" + TipoArchivo_grilla).jqGrid('setCell', data.CODIGO, 'FLG_ESTADO', MiCheck.checked ? '1' : '0');
            } else {
                jAlert(auditoria.MENSAJE_SALIDA, 'Atención');
            }
        } else {
            jAlert(auditoria.MENSAJE_SALIDA, 'Atención');
        }
    }
}

/*********************************************** ----------------- *************************************************/

/************************************************ Elimina TipoArchivo **************************************************/


function TipoArchivo_Eliminar(ID_DOMINIO) {
    //var data = jQuery("#" + TipoArchivo_grilla).jqGrid('getRowData', CODIGO);
    jConfirm("¿ Desea eliminar este tipo archivo ?", "Atención", function (r) {
        if (r) {
            var url = baseUrl + 'Administracion/TipoArchivo/TipoArchivo_Eliminar';
            var item = {
                ID_DOMINIO: ID_DOMINIO,
            };
            var auditoria = Autorizacion.Ajax(url, item, false);
            if (auditoria != null) {
                if (auditoria.EJECUCION_PROCEDIMIENTO) {
                    if (!auditoria.RECHAZAR) {
                        jAlert("Tipo Archivo eliminado", "Proceso");
                        TipoArchivo_CargarGrilla();
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
