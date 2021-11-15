var FormatoCorreo_grilla = 'FormatoCorreo_grilla';
var FormatoCorreo_barra = 'FormatoCorreo_barra';



function LimpiarFormatoCorreo() {
    $("#txt_Asunto").val('');
    $('#CBOESTADO').val('');
    FormatoCorreo_CargarGrilla();
}

function FormatoCorreo_ConfigurarGrilla() {
    $("#" + FormatoCorreo_grilla).GridUnload();
    var colNames = ['Editar', 'Eliminar', 'Activo', 'ID_FORMATO', 'Asunto'
        , 'FLG_ESTADO', 'Usuario Creación', 'Fecha Creación', 'Usuario Modificación', 'Fecha Modificación'];
    var colModels = [
        { name: 'EDITAR', index: 'EDITAR', align: 'center', width: 60, hidden: false, sortable: false, formatter: FormatoCorreo_actionEditar },
        { name: 'ELIMINAR', index: 'ELIMINAR', align: 'center', width: 70, hidden: false, sortable: false, formatter: FormatoCorreo_actionEliminar },
        { name: 'ACTIVO', index: 'ACTIVO', align: 'center', width: 55, hidden: false, sortable: false, formatter: FormatoCorreo_actionActivo },
        { name: 'ID_FORMATO', index: 'ID_FORMATO', align: 'center', width: 50, hidden: true },
        { name: 'ASUNTO', index: 'ASUNTO', align: 'center', width: 200, hidden: false },
        { name: 'FLG_ESTADO', index: 'FLG_ESTADO', align: 'center', width: 140, hidden: true, sortable: true },
        { name: 'USU_CREACION', index: 'USU_CREACION', align: 'center', width: 140, hidden: false, sortable: true },
        { name: 'FEC_CREACION', index: 'FEC_CREACION', align: 'center', width: 160, hidden: false, sortable: true, formatter: 'date', formatoptions: { srcformat: 'd/m/Y h:i A', newformat: 'd/m/Y h:i A' } },
        { name: 'USU_MODIFICACION', index: 'USU_MODIFICACION', align: 'center', width: 150, hidden: false, sortable: true },
        { name: 'FEC_MODIFICACION', index: 'FEC_MODIFICACION', align: 'center', width: 160, hidden: false, sortable: true, formatter: 'date', formatoptions: { srcformat: 'd/m/Y h:i A', newformat: 'd/m/Y h:i A' } },
    ];
    var opciones = {
        GridLocal: true, multiselect: false, CellEdit: false, Editar: false, nuevo: false, eliminar: false, search: false, sort: 'DESC',

    };
    SICA.Grilla(FormatoCorreo_grilla, FormatoCorreo_barra, '', '400', '', "Lista de FormatoCorreo", '', 'ID_FORMATO', colNames, colModels, 'ID_FORMATO', opciones);
}






function FormatoCorreo_actionEditar(cellvalue, options, rowObject) {
    var _btn = "<button title='Editar' onclick='FormatoCorreo_Editar(" + rowObject.ID_FORMATO + ");' class=\"btn btn-link\" type=\"button\" data-toggle=\"modal\"  style=\"text-decoration: none !important;\" data-target=\"#myModalNuevo\"> <i class=\"clip-pencil-3\" style=\"color:#e68c1b;font-size:17px\"></i></button>";
    return _btn;
}



function FormatoCorreo_actionEliminar(cellvalue, options, rowObject) {
    var _btn = "<button title='Eliminar' onclick=\"FormatoCorreo_Eliminar(" + rowObject.ID_FORMATO + ");\" class=\"btn btn-link\" type=\"button\" style=\"text-decoration: none !important;\"> <i class=\"clip-cancel-circle-2\" style=\"color:#c35245;font-size:17px\"></i></button>";
    return _btn;
}


function FormatoCorreo_actionActivo(cellvalue, options, rowObject) {
    var check_ = 'check';
    if (rowObject.FLG_ESTADO == 1)
        check_ = 'checked';

    var _btn = "<label class=\"switch\">"
        + "<input id=\"FormatoCorreo_chk_" + rowObject.ID_FORMATO + "\" type=\"checkbox\" onchange=\"FormatoCorreo_CambiarFormatoCorreo(" + rowObject.ID_FORMATO + ",this)\" " + check_ + ">"
        + "<span class=\"slider round\"></span>"
        + "</label>";
    return _btn;
}





function FormatoCorreo_Editar(ID_FORMATO) {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Administracion/FormatosCorreo/Mantenimiento?id=" + ID_FORMATO + "&Accion=M", function (responseText, textStatus, request) {
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}

function FormatoCorreo_Nuevo() {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Administracion/FormatosCorreo/Mantenimiento?id=0&Accion=N", function (responseText, textStatus, request) {
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}



function FormatoCorreo_Actualizar() {
    if ($("#frmMantenimientoFormatoCorreo").valid()) {
        var item =
        {
            ID_FORMATO: $("#hdfID_FORMATO").val(),
            ASUNTO: $("#ASUNTO").val(),
            BODY: $('#BODY').summernote('code'),
            USU_MODIFICACION: $("#inputHddcod_usuario").val(),
            TIPO: $("#HDF_Tipo_FormatoCorreo").val(),
            Accion: $("#AccionFormatoCorreo").val()
        };
        jConfirm("¿ Desea actualizar este tipo archivo ?", "Atención", function (r) {
            if (r) {
                var url = baseUrl + 'Administracion/FormatosCorreo/FormatoCorreo_Actualizar';
                var auditoria = Autorizacion.Ajax(url, item, false);
                if (auditoria != null) {
                    if (auditoria.EJECUCION_PROCEDIMIENTO) {
                        if (!auditoria.RECHAZAR) {
                            FormatoCorreo_CargarGrilla();
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





function FormatoCorreo_CargarGrilla() {
    $('#Grilla_Load').show();
    var item =
    {
        ASUNTO: $("#txt_Asunto").val(),
        FLG_ESTADO: $("#CBOESTADO").val(),
    };
    var url = baseUrl + 'Administracion/FormatosCorreo/FormatoCorreo_Listar';

    var auditoria = Autorizacion.Ajax(url, item, false);
    jQuery("#" + FormatoCorreo_grilla).jqGrid('clearGridData', true).trigger("reloadGrid");
    if (auditoria != null) {
        if (auditoria.EJECUCION_PROCEDIMIENTO) {
            $.each(auditoria.OBJETO, function (i, v) {
                var rowKey = jQuery("#" + FormatoCorreo_grilla).getDataIDs();
                var ix = rowKey.length;
                ix++;
                var myData =
                {
                    CODIGO: ix,
                    ID_FORMATO: v.ID_FORMATO,
                    ASUNTO: v.ASUNTO,
                    FLG_ESTADO: v.FLG_ESTADO,
                    USU_CREACION: v.USU_CREACION,
                    FEC_CREACION: v.FEC_CREACION,
                    USU_MODIFICACION: v.USU_MODIFICACION,
                    FEC_MODIFICACION: v.FEC_MODIFICACION

                };
                jQuery("#" + FormatoCorreo_grilla).jqGrid('addRowData', v.ID_FORMATO, myData);
            });
            jQuery("#" + FormatoCorreo_grilla).trigger("reloadGrid");
            $('#Grilla_Load').hide();
        } else {
            jAlert(auditoria.MENSAJE_SALIDA, 'Atención');
            $('#Grilla_Load').hide();
        }
    }
}







/*********************************************** ----------------- *************************************************/

/************************************************* Nuevo FormatoCorreo ***************************************************/

function FormatoCorreo_Registrar() {
    if ($("#frmMantenimientoFormatoCorreo").valid()) {
        if ($('#AccionFormatoCorreo').val() == "M") {
            FormatoCorreo_Actualizar();
        } else {
            jConfirm("¿Desea guardar este registro ?", "Atención", function (r) {
                if (r) {
                    var item =
                    {
                        ASUNTO: $("#ASUNTO").val(),
                        BODY: $('#BODY').summernote('code'),
                        USU_CREACION: $("#inputHddcod_usuario").val(),
                        Accion: $("#AccionFormatoCorreo").val()
                    };
                    if (item.BODY.length < 4000) {
                        var url = baseUrl + 'Administracion/FormatosCorreo/FormatoCorreo_Insertar';
                        var auditoria = Autorizacion.Ajax(url, item, false);
                        if (auditoria != null) {
                            if (auditoria.EJECUCION_PROCEDIMIENTO) {
                                if (!auditoria.RECHAZAR) {
                                    FormatoCorreo_CargarGrilla();
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
                    } else {
                        jAlert('El tamaño del contenido formato es demasiado grande, max(4000 caract). ', 'Atención');
                    }
                }

            });
        }
    }
}

/*********************************************** ----------------- *************************************************/

/*********************************************** Cambia el estado *************************************************/



function FormatoCorreo_CambiarFormatoCorreo(ID_FORMATO, MiCheck) {
    var url = baseUrl + 'Administracion/FormatosCorreo/FormatoCorreo_Estado';
    var item = {
        ID_FORMATO: ID_FORMATO,
        FLG_ESTADO: MiCheck.checked == true ? 1 : 0,
        USU_MODIFICACION: $('#inputHddcod_usuario').val()
    };
    var auditoria = Autorizacion.Ajax(url, item, false);
    if (auditoria != null && auditoria != "") {
        if (auditoria.EJECUCION_PROCEDIMIENTO) {
            if (!auditoria.RECHAZAR) {
                //$("#" + FormatoCorreo_grilla).jqGrid('setCell', data.CODIGO, 'FLG_ESTADO', MiCheck.checked ? '1' : '0');
            } else {
                jAlert(auditoria.MENSAJE_SALIDA, 'Atención');
            }
        } else {
            jAlert(auditoria.MENSAJE_SALIDA, 'Atención');
        }
    }
}

/*********************************************** ----------------- *************************************************/

/************************************************ Elimina FormatoCorreo **************************************************/


function FormatoCorreo_Eliminar(ID_FORMATO) {
    //var data = jQuery("#" + FormatoCorreo_grilla).jqGrid('getRowData', CODIGO);
    jConfirm("¿ Desea eliminar este tipo archivo ?", "Atención", function (r) {
        if (r) {
            var url = baseUrl + 'Administracion/FormatosCorreo/FormatoCorreo_Eliminar';
            var item = {
                ID_FORMATO: ID_FORMATO,
            };
            var auditoria = Autorizacion.Ajax(url, item, false);
            if (auditoria != null) {
                if (auditoria.EJECUCION_PROCEDIMIENTO) {
                    if (!auditoria.RECHAZAR) {
                        jAlert("Tipo Archivo eliminado", "Proceso");
                        FormatoCorreo_CargarGrilla();
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
