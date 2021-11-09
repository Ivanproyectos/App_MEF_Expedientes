var Adjuntos_grilla = 'Adjuntos_grilla';
var Adjuntos_barra = 'Adjuntos_barra';
var id;



function LimpiarAdjuntos() {
    $("#txt_NroOrden").val('');
    $("#ID_EMPRESA_CONTRATA_SEARCH").val('');
    $("#ID_EMPRESA_SEARCH").val('');
    $("#ID_ESTADO_SEARCH").val('');
    $('#CBOESTADO').val('');
    Adjuntos_ConfigurarGrilla();
}

function Adujuntos_Limpiar(){
    $("#ID_TIPO_ARCHIVO").val('');
    $("#NUMERO").val('');
    $("#FECHA").val('');
    $("#OBSERVACION").val('');
    Archivo_cambiado = false;
    $("#lbl_file").html("Seleccionar archivo");
}

function Adjuntos_ConfigurarGrilla() {
    $("#" + Adjuntos_grilla).GridUnload();
    var colNames = ['Eliminar','Ver','id_archivo', 'Tipo Documento','Número','Fecha' ,'Observación','FLG_ESTADO', 'Usuario Creación', 'Fecha Creación','EXTENSION'];
    var colModels = [
        { name: 'ELIMINAR', index: 'ELIMINAR', align: 'center', width: 65, hidden: false, sortable: false, formatter: Adjuntos_actionEliminar },
        { name: 'DESCARGAR', index: 'DESCARGAR', align: 'center', width: 80, hidden: false, sortable: false, formatter: Adjuntos_actionDescargar },
        { name: 'ID_ARCHIVO', index: 'ID_ARCHIVO', align: 'center', width: 50, hidden: true },
        { name: 'DESC_ARCHIVO', index: 'DESC_ARCHIVO', align: 'center', width: 200, hidden: false },
        { name: 'NUMERO', index: 'NUMERO', align: 'center', width: 100, hidden: false },
        { name: 'FECHA', index: 'FECHA', align: 'center', width: 100, hidden: false, sortable: false, formatter: 'date', formatoptions: { srcformat: 'd/m/Y', newformat: 'd/m/Y' }  },
        { name: 'OBSERVACION', index: 'OBSERVACION', align: 'center', width: 300, hidden: false },  
        { name: 'FLG_ESTADO', index: 'FLG_ESTADO', align: 'center', width: 140, hidden: true, sortable: true },
        { name: 'USU_CREACION', index: 'USU_CREACION', align: 'center', width: 140, hidden: false, sortable: true },
        { name: 'FEC_CREACION', index: 'FEC_CREACION', align: 'center', width: 160, hidden: false, sortable: true, formatter: 'date', formatoptions: { srcformat: 'd/m/Y h:i A', newformat: 'd/m/Y h:i A' } },
        { name: 'EXTENSION_ARCHIVO', index: 'EXTENSION_ARCHIVO', align: 'center', width: 300, hidden: true }, 
        
    ];
    var opciones = {
        GridLocal: true, multiselect: false, CellEdit: false, Editar: false, nuevo: false, eliminar: false, search: false, sort: 'DESC', rules: true,


    };
    SICA.Grilla(Adjuntos_grilla, Adjuntos_barra, '', '200', '', "Lista de adjuntos", '', 'ID_ARCHIVO', colNames, colModels, 'ID_ARCHIVO', opciones);
}

function Adjuntos_actionDescargar(cellvalue, options, rowObject) {
    if (rowObject.EXTENSION_ARCHIVO ==".pdf") {
        var _btn = "<button title='Descargar' onclick='Adjuntos_Ver(" + rowObject.ID_ARCHIVO + ");' class=\"btn btn-link\" type=\"button\" style=\"text-decoration: none !important;\" data-toggle=\"modal\"  style=\"text-decoration: none !important;\" data-target=\"#myModalNuevoAdjuntos\"> <i class=\"clip-file-pdf\" style=\"color:#c35245;font-size:17px\"></i></button>";
    } else {
        var _btn = "<button title='Descargar' onclick='Adjuntos_Descargar(" + rowObject.ID_ARCHIVO + ");' class=\"btn btn-link\" type=\"button\" style=\"text-decoration: none !important;\" > <i class=\"clip-download\" style=\"color:#c35245;font-size:17px\"></i></button>";
    }
    return _btn;
}

function Adjuntos_Ver(ID_ARCHIVO) {
    jQuery("#myModalNuevoAdjuntos").html('');
    jQuery("#myModalNuevoAdjuntos").load(baseUrl + "Maestras/Adjuntos/Mantenimiento_Ver?id=" + ID_ARCHIVO, function (responseText, textStatus, request) {
        $.validator.unobtrusive.parse('#myModalNuevoAdjuntos');
        if (request.status != 200) return;
    });
}



function Adjuntos_Descargar(ID_ARCHIVO) {
    jQuery("#myModalDescargar").html('');
    jQuery("#myModalDescargar").load(baseUrl + "Maestras/Adjuntos/Descargar_Archivo?id=" + ID_ARCHIVO, function (responseText, textStatus, request) {
        $.validator.unobtrusive.parse('#myModalDescargar');
        if (request.status != 200) return;
    });
}



function Adjuntos_Listar() {;
    var item =
    {
        ID_MAESTRA: $("#hdfID_EXPEDIENTE").val(),
    };
    var url = baseUrl + 'Maestras/Adjuntos/Adjuntos_Listar';
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
                     ID_ARCHIVO: v.ID_ADJUNTO,
                     DESC_ARCHIVO: v.DESC_ARCHIVO,
                     OBSERVACION: v.OBSERVACION,
                     FECHA: v.FECHA,
                     NUMERO: v.NUMERO,
                     USU_CREACION: v.USU_CREACION,
                    FEC_CREACION: v.FEC_CREACION,
                    EXTENSION_ARCHIVO: v.EXTENSION_ARCHIVO


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

/************************************************* Nuevo Adjuntos ***************************************************/



function Adjuntos_Registrar() {
    if ($("#frmMantenimientoAdjuntos").valid()) {
        if (Archivo_cambiado) {
            jConfirm("¿Desea guardar este registro ?", "Atención", function (r) {
                if (r) {
                    var item =
                    {
                            ID_MAESTRA: $("#hdfID_EXPEDIENTE").val(),
                            ID_TIPO_ARCHIVO: $("#ID_TIPO_ARCHIVO").val(),
                            NUMERO: $("#NUMERO").val(),
                            OBSERVACION: $("#OBSERVACION").val(),
                            FECHA: $("#FECHA").val(),
                            MiArchivo: Archivo_cambiado == true ? ArchivoNuevo_Array[0] : '',
                            USU_CREACION: $("#inputHddcod_usuario").val(),
                            Accion: $("#AccionAdjuntos").val()
                        };
                    var url = baseUrl + 'Maestras/Adjuntos/Adjuntos_Insertar';
                    var auditoria = Autorizacion.Ajax(url, item, false);
                    if (auditoria != null) {
                        if (auditoria.EJECUCION_PROCEDIMIENTO) {
                            if (!auditoria.RECHAZAR) {
                                Adjuntos_Listar();
                                jAlert("Datos guardados satisfactoriamente", "Proceso");
                                Adujuntos_Limpiar(); 
                               
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

/************************************************ Elimina Adjuntos **************************************************/

function Adjuntos_actionEliminar(cellvalue, options, rowObject) {
    var _btn = "<button title='Eliminar' onclick='Adjuntos_Eliminar(" + rowObject.ID_ARCHIVO + ");' class=\"btn btn-link\" type=\"button\" style=\"text-decoration: none !important;\"> <i class=\"clip-cancel-circle-2\" style=\"color:#c35245;font-size:17px\"></i></button>";
    return _btn;
}

function Adjuntos_Eliminar(ID_ARCHIVO) {
    jConfirm("¿ Desea eliminar este registro ?", "Atención", function (r) {
        if (r) {
            var url = baseUrl + 'Maestras/Adjuntos/Adjuntos_Eliminar';
            var item = {
                ID_ADJUNTO: ID_ARCHIVO,
            };
            var auditoria = Autorizacion.Ajax(url, item, false);
            if (auditoria != null) {
                if (auditoria.EJECUCION_PROCEDIMIENTO) {
                    if (!auditoria.RECHAZAR) {
                        jAlert("Registro eliminado", "Proceso");
                        Adjuntos_Listar();
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

