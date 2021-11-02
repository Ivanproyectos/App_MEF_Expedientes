var Adjuntos_grilla = 'Adjuntos_grilla';
var Adjuntos_barra = 'Adjuntos_barra';
var id;



function LimpiarProyectos() {
    $("#txt_NroOrden").val('');
    $("#ID_EMPRESA_CONTRATA_SEARCH").val('');
    $("#ID_EMPRESA_SEARCH").val('');
    $("#ID_ESTADO_SEARCH").val('');
    $('#CBOESTADO').val('');
    Adjuntos_ConfigurarGrilla();
}

function Adjuntos_ConfigurarGrilla() {
    $("#" + Adjuntos_grilla).GridUnload();
    var colNames = ['Eliminar','Descargar','id_archivo', 'Tipo Documento','Número','Fecha' ,'Observación','FLG_ESTADO', 'Usuario Creación', 'Fecha Creación'];
    var colModels = [
        { name: 'ELIMINAR', index: 'ELIMINAR', align: 'center', width: 65, hidden: false, sortable: false, formatter: Adjuntos_actionEliminar },
        { name: 'DESCARGAR', index: 'DESCARGAR', align: 'center', width: 80, hidden: false, sortable: false, formatter: Adjuntos_actionDescargar },
        { name: 'ID_ARCHIVO', index: 'ID_ARCHIVO', align: 'center', width: 50, hidden: true },
        { name: 'TIPO_DOCUMENTO', index: 'TIPO_DOCUMENTO', align: 'center', width: 200, hidden: false },
        { name: 'NUMERO', index: 'NUMERO', align: 'center', width: 100, hidden: false },
        { name: 'FECHA', index: 'FECHA', align: 'center', width: 100, hidden: false, sortable: false },
        { name: 'OBSERVACION', index: 'OBSERVACION', align: 'center', width: 300, hidden: false },  
        { name: 'FLG_ESTADO', index: 'FLG_ESTADO', align: 'center', width: 140, hidden: true, sortable: true },
        { name: 'USU_CREACION', index: 'USU_CREACION', align: 'center', width: 140, hidden: false, sortable: true },
        { name: 'FEC_CREACION', index: 'FEC_CREACION', align: 'center', width: 160, hidden: false, sortable: true },
,
    ];
    var opciones = {
        GridLocal: true, multiselect: false, CellEdit: false, Editar: false, nuevo: false, eliminar: false, search: false, sort: 'DESC', rules: true,


    };
    SICA.Grilla(Adjuntos_grilla, Adjuntos_barra, '', '200', '', "Lista de adjuntos", '', 'ID_ARCHIVO', colNames, colModels, 'ID_ARCHIVO', opciones);
}

function Adjuntos_actionDescargar(cellvalue, options, rowObject) {
    var _btn = "<button title='Descargar' onclick='Adjuntos__Descargar(" + rowObject.ID_ARCHIVO + ");' class=\"btn btn-link\" type=\"button\" style=\"text-decoration: none !important;\"> <i class=\"clip-download\" style=\"color:#c35245;font-size:17px\"></i></button>";
    return _btn;
}



function Adjuntos__Descargar(ID_ARCHIVO) {
    jQuery("#myModalDescargar").html('');
    jQuery("#myModalDescargar").load(baseUrl + "Sispro/Proyectos/Mantenimiento_Descargar?id=" + ID_ARCHIVO, function (responseText, textStatus, request) {
        $.validator.unobtrusive.parse('#myModalDescargar');
        if (request.status != 200) return;
    });
}

function Adjuntos__CargarGrilla() {;
    var item =
    {
        ID_PROYECTO: $("#hdfID_PROYECTO").val(),
    };
    var url = baseUrl + 'Sispro/Proyectos/Adjuntos__Listar';
    var auditoria = Autorizacion.Ajax(url, item, false);
    jQuery("#" + Adjuntos_grilla).jqGrid('clearGridData', true).trigger("reloadGrid");
    if (auditoria != null) {
        if (auditoria.EJECUCION_PROCEDIMIENTO) {
            $.each(auditoria.OBJETO, function (i, v) {
                var rowKey = jQuery("#" + Adjuntos_grilla).getDataIDs();
                var ix = rowKey.length;
                ix++;
                var myData =
                 {
                     CODIGO: ix,
                     ID_ARCHIVO: v.ID_ARCHIVO,
                     TIPO_DOCUMENTO: v.TIPO_ARCHIVO,
                     OBSERVACION: v.OBSERVACION,
                     VERSION: v.VERSION,
                     NOMBRE_ARCHIVO: v.MiArchivo.NOMBRE_ARCHIVO,
                     USU_CREACION: v.USU_CREACION,
                     FEC_CREACION: v.FEC_CREACION,
                     USU_MODIFICACION: v.USU_MODIFICACION,
                     FEC_MODIFICACION: v.FEC_MODIFICACION

                 };
                jQuery("#" + Adjuntos_grilla).jqGrid('addRowData', ix, myData);
            });
            jQuery("#" + Adjuntos_grilla).trigger("reloadGrid");
        } else {
            jAlert(auditoria.MENSAJE_SALIDA, 'Atención');
        }
    }
}



/*********************************************** ----------------- *************************************************/

/************************************************* Nuevo Proyectos ***************************************************/



function Adjuntos__Registrar() {
    if ($("#frmMantenimientoProyectos_Detlale").valid()) {
        if (Archivo_cambiado) {
            jConfirm("¿Desea guardar este registro ?", "Atención", function (r) {
                if (r) {
                    var item =
                        {
                            ID_PROYECTO: $("#hdfID_PROYECTO").val(),
                            ID_TIPO_DOCUMENTO_PROYECTO: $("#ID_TIPO_DOCUMENTO_PROYECTO").val(),
                            OBSERVACION: $("#OBSERVACION").val(),
                            VERSION: $("#VERSION").val(),
                            MiArchivo: Archivo_cambiado == true ? ArchivoNuevo_Array[0] : '',
                            USU_CREACION: $("#inputHddcod_usuario").val(),
                            Accion: $("#AccionProyectos").val()
                        };
                    var url = baseUrl + 'Sispro/Proyectos/Adjuntos__Insertar';
                    var auditoria = Autorizacion.Ajax(url, item, false);
                    if (auditoria != null) {
                        if (auditoria.EJECUCION_PROCEDIMIENTO) {
                            if (!auditoria.RECHAZAR) {
                                Adjuntos_ConfigurarGrilla();
                                jAlert("Datos guardados satisfactoriamente", "Proceso");
                                //$('#myModalNuevo').modal('hide');
                                //jQuery("#myModalNuevo").html('');
                            } else {
                                jAlert(auditoria.MENSAJE_SALIDA, 'Atención');
                            }
                        } else {
                            jAlert(auditoria.MENSAJE_SALIDA, 'Atención');
                        }
                    }
                }
            });
        } else {
            $('#validar_file').show(); 
        }
    }
}



/*********************************************** ----------------- *************************************************/

/************************************************ Elimina Proyectos **************************************************/

function Adjuntos_actionEliminar(cellvalue, options, rowObject) {
    var _btn = "<button title='Eliminar' onclick='Adjuntos_Eliminar(" + rowObject.ID_ARCHIVO + ");' class=\"btn btn-link\" type=\"button\" style=\"text-decoration: none !important;\"> <i class=\"clip-cancel-circle-2\" style=\"color:#c35245;font-size:17px\"></i></button>";
    return _btn;
}

function Adjuntos_Eliminar(ID_ARCHIVO) {
    //var data = jQuery("#" + Adjuntos_grilla).jqGrid('getRowData', CODIGO);
    jConfirm("¿ Desea eliminar este registro ?", "Atención", function (r) {
        if (r) {
            var url = baseUrl + 'Sispro/Proyectos/Adjuntos__Eliminar';
            var item = {
                ID_ARCHIVO: ID_ARCHIVO,
            };
            var auditoria = Autorizacion.Ajax(url, item, false);
            if (auditoria != null) {
                if (auditoria.EJECUCION_PROCEDIMIENTO) {
                    if (!auditoria.RECHAZAR) {
                        jAlert("Registro eliminado", "Proceso");
                        Adjuntos__CargarGrilla();
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

