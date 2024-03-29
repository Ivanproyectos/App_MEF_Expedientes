﻿var Situacion_grilla = 'Situacion_grilla';
var Situacion_barra = 'Situacion_barra';


function LimpiarSituacion() {
    $("#txt_DescripcionCorta").val('');
    $("#txt_DescripcionLarga").val('');
    $('#CBOESTADO').val('');
    Situacion_CargarGrilla();
}

function Situacion_ConfigurarGrilla() {
    $("#" + Situacion_grilla).GridUnload();
    var colNames = ['Editar', 'Eliminar', 'Activo', 'ID_DOMINIO', 'Código', 'Descripción Corta', 'Descripción Larga'
        , 'FLG_ESTADO', 'Usuario Creación', 'Fecha Creación', 'Usuario Modificación', 'Fecha Modificación'];
    var colModels = [
        { name: 'EDITAR', index: 'EDITAR', align: 'center', width: 60, hidden: false, sortable: false, formatter: Situacion_actionEditar },
        { name: 'ELIMINAR', index: 'ELIMINAR', align: 'center', width: 70, hidden: false, sortable: false, formatter: Situacion_actionEliminar },
        { name: 'ACTIVO', index: 'ACTIVO', align: 'center', width: 55, hidden: false, sortable: false, formatter: Situacion_actionActivo },
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
    SICA.Grilla(Situacion_grilla, Situacion_barra, '', '400', '', "Lista de Situacion", '', 'ID_DOMINIO', colNames, colModels, 'ID_DOMINIO', opciones);
}




function Situacion_CargarGrilla() {
    $('#Grilla_Load').show();
    var item =
    {
        DESC_CORTA_DOMINIO: $("#txt_DescripcionCorta").val(),
        DESC_LARGA_DOMINIO: $("#txt_DescripcionLarga").val(),
        FLG_ESTADO: $("#CBOESTADO").val(),
        NOM_DOMINIO: 'SITUACION' // SITUACION
    };
    var url = baseUrl + 'Administracion/Situacion/Situacion_Listar';

    var auditoria = Autorizacion.Ajax(url, item, false);
    jQuery("#" + Situacion_grilla).jqGrid('clearGridData', true).trigger("reloadGrid");
    if (auditoria != null) {
        if (auditoria.EJECUCION_PROCEDIMIENTO) {
            $.each(auditoria.OBJETO, function (i, v) {
                var rowKey = jQuery("#" + Situacion_grilla).getDataIDs();
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
                jQuery("#" + Situacion_grilla).jqGrid('addRowData', v.ID_DOMINIO, myData);
            });
            jQuery("#" + Situacion_grilla).trigger("reloadGrid");
            $('#Grilla_Load').hide();
        } else {
            jAlert(auditoria.MENSAJE_SALIDA, 'Atención');
            $('#Grilla_Load').hide();
        }
    }
}







function Situacion_actionEditar(cellvalue, options, rowObject) {
    var _btn = "<button title='Editar' onclick='Situacion_Editar(" + rowObject.ID_DOMINIO + ");' class=\"btn btn-link\" type=\"button\" data-toggle=\"modal\"  style=\"text-decoration: none !important;\" data-target=\"#myModalNuevo\"> <i class=\"clip-pencil-3\" style=\"color:#e68c1b;font-size:17px\"></i></button>";
    return _btn;
}



function Situacion_actionEliminar(cellvalue, options, rowObject) {
    var _btn = "<button title='Eliminar' onclick=\"Situacion_Eliminar(" + rowObject.ID_DOMINIO + ");\" class=\"btn btn-link\" type=\"button\" style=\"text-decoration: none !important;\"> <i class=\"clip-cancel-circle-2\" style=\"color:#c35245;font-size:17px\"></i></button>";
    return _btn;
}


function Situacion_actionActivo(cellvalue, options, rowObject) {
    var check_ = 'check';
    if (rowObject.FLG_ESTADO == 1)
        check_ = 'checked';

    var _btn = "<label class=\"switch\">"
        + "<input id=\"Situacion_chk_" + rowObject.ID_DOMINIO + "\" type=\"checkbox\" onchange=\"Situacion_CambiarEstado(" + rowObject.ID_DOMINIO + ",this)\" " + check_ + ">"
        + "<span class=\"slider round\"></span>"
        + "</label>";
    return _btn;
}





function Situacion_Editar(ID_DOMINIO) {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Administracion/Situacion/Mantenimiento?id=" + ID_DOMINIO + "&Accion=M", function (responseText, textStatus, request) {
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}

function Situacion_Nuevo() {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Administracion/Situacion/Mantenimiento?id=0&Accion=N", function (responseText, textStatus, request) {
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}



function Situacion_Actualizar() {
    if ($("#frmMantenimientoSituacion").valid()) {
        var item =
        {
            ID_DOMINIO: $("#hdfID_DOMINIO").val(),
            COD_DOMINIO: $("#COD_DOMINIO").val(),
            DESC_CORTA_DOMINIO: $("#DESC_CORTA_DOMINIO").val(),
            DESC_LARGA_DOMINIO: $("#DESC_LARGA_DOMINIO").val(),
            USU_MODIFICACION: $("#inputHddcod_usuario").val(),
            TIPO: $("#HDF_Tipo_Situacion").val(),
            Accion: $("#AccionSituacion").val()
        };
        jConfirm("¿ Desea actualizar este tipo estado ?", "Atención", function (r) {
            if (r) {
                var url = baseUrl + 'Administracion/Situacion/Situacion_Actualizar';
                var auditoria = Autorizacion.Ajax(url, item, false);
                if (auditoria != null) {
                    if (auditoria.EJECUCION_PROCEDIMIENTO) {
                        if (!auditoria.RECHAZAR) {
                            Situacion_CargarGrilla();
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

/************************************************* Nuevo Situacion ***************************************************/



function Situacion_Registrar() {
    if ($("#frmMantenimientoSituacion").valid()) {
        if ($('#AccionSituacion').val() == "M") {
            Situacion_Actualizar();
        } else {
            jConfirm("¿Desea guardar este registro ?", "Atención", function (r) {
                if (r) {
                    var item =
                    {
                        ID_DOMINIO_PADRE: 6, //CODITO TABLA SITUACION
                        NOM_DOMINIO: 'SITUACION', // CODITO SITUACION
                        COD_DOMINIO: $("#COD_DOMINIO").val(),
                        DESC_CORTA_DOMINIO: $("#DESC_CORTA_DOMINIO").val(),
                        DESC_LARGA_DOMINIO: $("#DESC_LARGA_DOMINIO").val(),
                        USU_CREACION: $("#inputHddcod_usuario").val(),
                        Accion: $("#AccionSituacion").val()
                    };
                    var url = baseUrl + 'Administracion/Situacion/Situacion_Insertar';
                    var auditoria = Autorizacion.Ajax(url, item, false);
                    if (auditoria != null) {
                        if (auditoria.EJECUCION_PROCEDIMIENTO) {
                            if (!auditoria.RECHAZAR) {
                                Situacion_CargarGrilla();
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



function Situacion_CambiarEstado(ID_DOMINIO, MiCheck) {
    var url = baseUrl + 'Administracion/Situacion/Situacion_Estado';
    var item = {
        ID_DOMINIO: ID_DOMINIO,
        FLG_ESTADO: MiCheck.checked == true ? 1 : 0,
        USU_MODIFICACION: $('#inputHddcod_usuario').val()
    };
    var auditoria = Autorizacion.Ajax(url, item, false);
    if (auditoria != null && auditoria != "") {
        if (auditoria.EJECUCION_PROCEDIMIENTO) {
            if (!auditoria.RECHAZAR) {
                //$("#" + Situacion_grilla).jqGrid('setCell', data.CODIGO, 'FLG_ESTADO', MiCheck.checked ? '1' : '0');
            } else {
                jAlert(auditoria.MENSAJE_SALIDA, 'Atención');
            }
        } else {
            jAlert(auditoria.MENSAJE_SALIDA, 'Atención');
        }
    }
}

/*********************************************** ----------------- *************************************************/

/************************************************ Elimina Situacion **************************************************/


function Situacion_Eliminar(ID_DOMINIO) {
    //var data = jQuery("#" + Situacion_grilla).jqGrid('getRowData', CODIGO);
    jConfirm("¿ Desea eliminar este tipo estado ?", "Atención", function (r) {
        if (r) {
            var url = baseUrl + 'Administracion/Situacion/Situacion_Eliminar';
            var item = {
                ID_DOMINIO: ID_DOMINIO,
            };
            var auditoria = Autorizacion.Ajax(url, item, false);
            if (auditoria != null) {
                if (auditoria.EJECUCION_PROCEDIMIENTO) {
                    if (!auditoria.RECHAZAR) {
                        jAlert("Tipo Archivo eliminado", "Proceso");
                        Situacion_CargarGrilla();
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
