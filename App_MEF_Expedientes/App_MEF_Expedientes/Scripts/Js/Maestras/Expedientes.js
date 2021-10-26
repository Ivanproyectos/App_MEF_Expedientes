var Expedientes_grilla = 'Expedientes_grilla';
var Expedientes_barra = 'Expedientes_barra';
var id;

//$(document).ready(function () {
//    Expedientes_ConfigurarGrilla();
//    //Expedientes_CargarGrilla();
//});

function LimpiarExpedientes() {
    $("#txt_empresa").val('');
    $("#txt_Ruc").val('');
    $("#CBO_TIPOEMPRESA").val('');
    $('#CBOESTADO').val('');
    Expedientes_ConfigurarGrilla();
}

function Expedientes_ConfigurarGrilla() {
    var url = baseUrl + 'Maestras/Expedientes/Expedientes_Paginado';
    $("#" + Expedientes_grilla).GridUnload();
    var colNames = ['Editar', 'Eliminar', 'Activo', 'ID_EXPEDIENTE', 'Nro. Expediente', 'Personal', 'Oficina', 'Reg. Laboral', 'Cargo', 'Falta', 'Sanción',
        'Hechos', 'Situación','Estado', 'FLG_ESTADO', 'Usuario Creación', 'Fecha Creación', 'Usuario Modificación', 'Fecha Modificación'];
    var colModels = [
        { name: 'EDITAR', index: 'EDITAR', align: 'center', width: 60, hidden: false, sortable: false, formatter: Expedientes_actionEditar },
        { name: 'ELIMINAR', index: 'ELIMINAR', align: 'center', width: 70, hidden: false, sortable: false, formatter: Expedientes_actionEliminar },
        { name: 'ACTIVO', index: 'ACTIVO', align: 'center', width: 55, hidden: false, sortable: false, formatter: Expedientes_actionActivo },
        { name: 'ID_EXPEDIENTE', index: 'ID_EXPEDIENTE', align: 'center', width: 50, hidden: true },
        { name: 'COD_EXPEDIENTE', index: 'COD_EXPEDIENTE', align: 'center', width: 150, hidden: false },
        { name: 'PERSONAL', index: 'PERSONAL', align: 'center', width: 200, hidden: false },
        { name: 'OFICINA', index: 'OFICINA', align: 'center', width: 200, hidden: false },
        { name: 'REGIMEN_LABORAL', index: 'REGIMEN_LABORAL', width: 200, align: 'center', hidden: false},
        { name: 'CARGO', index: 'CARGO', align: 'center', width: 100, hidden: false },
        { name: 'CORREO', index: 'CORREO', align: 'center', width: 200, hidden: false, resizable: true },
        { name: 'DIRECCION', index: 'DIRECCION', align: 'center', width: 120, hidden: false, hidden: false },
        { name: 'DEPARTAMENTO', index: 'DEPARTAMENTO', align: 'center', width: 140, hidden: false, hidden: false },
        { name: 'PROVINCIA', index: 'PROVINCIA', align: 'center', width: 140, hidden: false, hidden: false },
        { name: 'DISTRITO', index: 'DISTRITO', align: 'center', width: 140, hidden: false, hidden: false },
        { name: 'FLG_ESTADO', index: 'FLG_ESTADO', align: 'center', width: 140, hidden: true, sortable: true },
        { name: 'USU_CREACION', index: 'USU_CREACION', align: 'center', width: 140, hidden: false, sortable: true },
        { name: 'FEC_CREACION', index: 'FEC_CREACION', align: 'center', width: 160, hidden: false, sortable: true },
        { name: 'USU_MODIFICACION', index: 'USU_MODIFICACION', align: 'center', width: 150, hidden: false, sortable: true },
        { name: 'FEC_MODIFICACION', index: 'FEC_MODIFICACION', align: 'center', width: 160, hidden: false, sortable: true },
    ];
    var opciones = {
        GridLocal: false, multiselect: false, CellEdit: false, Editar: false, nuevo: false, eliminar: false, search: false, sort: 'DESC', rules: true,
        gridCompleteFunc: function () {
            $('#Grilla_Load').hide();
            debugger;
            var filas = $("#Expedientes_grilla").jqGrid('getGridParam', 'records');
            if (filas == 0) {
                //BockGrilla(Expedientes_grilla);
            } else {
                //DescbockGrilla(Expedientes_grilla);
            }



        },

    };
    SICA.Grilla(Expedientes_grilla, Expedientes_barra, '', '400', '', "Lista de Expedientes", url, 'ID_EXPEDIENTE', colNames, colModels, 'ID_EXPEDIENTE', opciones);
}

function Expedientes_actionActivo(cellvalue, options, rowObject) {
    var check_ = 'check';
    if (rowObject[13] == 1)
        check_ = 'checked';

    var _btn = "<label class=\"switch\">"
        + "<input id=\"Expedientes_chk_" + rowObject[3] + "\" type=\"checkbox\" onchange=\"Expedientes_CambiarEstado(" + rowObject[3] + ",this)\" " + check_ + ">"
        + "<span class=\"slider round\"></span>"
        + "</label>";
    return _btn;
}


function GetRules(Usuario_Grilla) {
    var rules = new Array();
    var EMPRESA = "'" + jQuery('#txt_empresa').val() + "'";
    var RUC = "'" + jQuery('#txt_Ruc').val() + "'";
    var TIPO_EMPRESA = jQuery('#CBO_TIPOEMPRESA').val() == '' ? null : "'" + jQuery('#CBO_TIPOEMPRESA').val() + "'";
    var FLG_ESTADO = jQuery('#CBOESTADO').val() == '' ? null : "'" + jQuery('#CBOESTADO').val() + "'";
    var POR = "'%'";
    rules = []
    rules.push({ field: 'UPPER(DESC_EMPRESA)', data: POR + ' + ' + EMPRESA + ' + ' + POR, op: " LIKE " });
    rules.push({ field: 'UPPER(RUC)', data: POR + ' + ' + RUC + ' + ' + POR, op: " LIKE " });
    rules.push({ field: 'FLG_ESTADO', data: '  ISNULL(' + FLG_ESTADO + ',FLG_ESTADO) ', op: " = " });
    rules.push({ field: 'FLG_TIPO', data: '  ISNULL(' + TIPO_EMPRESA + ',FLG_TIPO) ', op: " = " });

    return rules;
}




/*********************************************** Editar el Expedientes *************************************************/

function Expedientes_actionEditar(cellvalue, options, rowObject) {
    var _btn = "<button title='Editar' onclick='Expedientes_Editar(" + rowObject[3] + ");' class=\"btn btn-link\" type=\"button\" data-toggle=\"modal\"  style=\"text-decoration: none !important;\" data-target=\"#myModalNuevo\"> <i class=\"clip-pencil-3\" style=\"color:#e68c1b;font-size:17px\"></i></button>";
    return _btn;
}

function Expedientes_Editar(ID_EXPEDIENTE) {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Maestras/Expedientes/Mantenimiento?id=" + ID_EXPEDIENTE + "&Accion=M", function (responseText, textStatus, request) {
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}


function Expedientes_Nuevo() {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Maestras/Expedientes/Mantenimiento?id=0&Accion=N", function (responseText, textStatus, request) {
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}



function Expedientes_Actualizar() {
    if ($("#frmMantenimientoExpedientes").valid()) {
        var item =
        {
            ID_EXPEDIENTE: $("#hdfID_EXPEDIENTE").val(),
            FLG_TIPO: $("#FLG_TIPO").val(),
            RUC: $("#RUC").val(),
            DESC_EMPRESA: $("#DESC_EMPRESA").val(),
            RAZON_SOCIAL: $("#DESC_EMPRESA").val(),
            TELEFONO: $("#TELEFONO").val(),
            DIRECCION: $("#DIRECCION").val(),
            CORREO: $("#CORREO").val(),
            ID_DEPARTAMENTO: $("#ID_DEPARTAMENTO").val(),
            REPRESENTANTE_LEGAL: $("#REPRESENTANTE_LEGAL").val(),
            ID_PROVINCIA: $("#ID_PROVINCIA").val(),
            ID_DISTRITO: $("#ID_DISTRITO").val(),
            USU_MODIFICACION: $("#inputHddcod_usuario").val(),
            Accion: $("#AccionExpedientes").val()
        };
        jConfirm("¿ Desea actualizar este personal ?", "Atención", function (r) {
            if (r) {
                var url = baseUrl + 'Maestras/Expedientes/Expedientes_Actualizar';
                var auditoria = Autorizacion.Ajax(url, item, false);
                if (auditoria != null) {
                    if (auditoria.EJECUCION_PROCEDIMIENTO) {
                        if (!auditoria.RECHAZAR) {
                            Expedientes_ConfigurarGrilla();
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

/************************************************* Nuevo Expedientes ***************************************************/



function Expedientes_Registrar() {
    if ($("#frmMantenimientoExpedientes").valid()) {
        if ($('#AccionExpedientes').val() == "M") {
            Expedientes_Actualizar();
        } else {
            jConfirm("¿Desea guardar este registro ?", "Atención", function (r) {
                if (r) {
                    var item =
                    {
                        FLG_TIPO: $("#FLG_TIPO").val(),
                        RUC: $("#RUC").val(),
                        DESC_EMPRESA: $("#DESC_EMPRESA").val(),
                        RAZON_SOCIAL: $("#DESC_EMPRESA").val(),
                        TELEFONO: $("#TELEFONO").val(),
                        DIRECCION: $("#DIRECCION").val(),
                        CORREO: $("#CORREO").val(),
                        REPRESENTANTE_LEGAL: $("#REPRESENTANTE_LEGAL").val(),
                        ID_DEPARTAMENTO: $("#ID_DEPARTAMENTO").val(),
                        ID_PROVINCIA: $("#ID_PROVINCIA").val(),
                        ID_DISTRITO: $("#ID_DISTRITO").val(),
                        USU_CREACION: $("#inputHddcod_usuario").val(),
                        Accion: $("#AccionExpedientes").val()
                    };
                    var url = baseUrl + 'Maestras/Expedientes/Expedientes_Insertar';
                    var auditoria = Autorizacion.Ajax(url, item, false);
                    if (auditoria != null) {
                        if (auditoria.EJECUCION_PROCEDIMIENTO) {
                            if (!auditoria.RECHAZAR) {
                                Expedientes_ConfigurarGrilla();
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



function Expedientes_CambiarEstado(ID_EXPEDIENTE, MiCheck) {
    var url = baseUrl + 'Maestras/Expedientes/Expedientes_Estado';
    var item = {
        ID_EXPEDIENTE: ID_EXPEDIENTE,
        FLG_ESTADO: MiCheck.checked == true ? 1 : 0,
        USU_MODIFICACION: $('#inputHddcod_usuario').val()
    };
    var auditoria = Autorizacion.Ajax(url, item, false);
    if (auditoria != null && auditoria != "") {
        if (auditoria.EJECUCION_PROCEDIMIENTO) {
            if (!auditoria.RECHAZAR) {
                //$("#" + Expedientes_grilla).jqGrid('setCell', data.CODIGO, 'FLG_ESTADO', MiCheck.checked ? '1' : '0');
            } else {
                jAlert(auditoria.MENSAJE_SALIDA, 'Atención');
            }
        } else {
            jAlert(auditoria.MENSAJE_SALIDA, 'Atención');
        }
    }
}

/*********************************************** ----------------- *************************************************/

/************************************************ Elimina Expedientes **************************************************/

function Expedientes_actionEliminar(cellvalue, options, rowObject) {
    var _btn = "<button title='Eliminar' onclick='Expedientes_Eliminar(" + rowObject[3] + ");' class=\"btn btn-link\" type=\"button\" style=\"text-decoration: none !important;\"> <i class=\"clip-cancel-circle-2\" style=\"color:#c35245;font-size:17px\"></i></button>";
    return _btn;
}

function Expedientes_Eliminar(ID_EXPEDIENTE) {
    //var data = jQuery("#" + Expedientes_grilla).jqGrid('getRowData', CODIGO);
    jConfirm("¿ Desea eliminar este personal ?", "Atención", function (r) {
        if (r) {
            var url = baseUrl + 'Maestras/Expedientes/Expedientes_Eliminar';
            var item = {
                ID_EXPEDIENTE: ID_EXPEDIENTE,
            };
            var auditoria = Autorizacion.Ajax(url, item, false);
            if (auditoria != null) {
                if (auditoria.EJECUCION_PROCEDIMIENTO) {
                    if (!auditoria.RECHAZAR) {
                        jAlert("Expedientes eliminado", "Proceso");
                        Expedientes_ConfigurarGrilla();
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

/*********************************************** ----------------- *************************************************/





///*********************************************** ----------------- **********************************************************************/

function Provincia_Listar(COD_UBIGEO) {
    if (COD_UBIGEO == '') {
        ReiniciarProvinciaListar();
        ReiniciarDistritoListar();
    } else {
        var item =
        {
            COD_UBIGEO: COD_UBIGEO,

        };
        var url = baseUrl + 'Seguridad/Ubigeo/Provincia_Listar';
        var auditoria = SICA.Ajax(url, item, false);
        if (auditoria != null) {
            if (auditoria.EJECUCION_PROCEDIMIENTO) {
                var items = "<select class=\"form-control\" id=\"ID_PROVINCIA\">";
                items += "<option value=\"" + "" + "\"> --Seleccione-- </option>";
                for (i_ = 0; i_ < auditoria.OBJETO.length; i_++) {
                    items += "<option value=\"" + auditoria.OBJETO[i_].COD_UBIGEO + "\" > " + auditoria.OBJETO[i_].DESC_UBIGEO + " </option>";
                }
                items += "</select>";
                $("#ID_PROVINCIA").html(items);
                if (!combo1 && $('#AccionExpedientes').val() == "M") {
                    $("#ID_PROVINCIA").val($("#hdfID_PROVINCIA").val());
                    Distrito_Listar($("#hdfID_PROVINCIA").val());
                    combo1 = true;
                } else {
                    ReiniciarDistritoListar();
                }
            } else {
                jAlert(auditoria.MENSAJE_SALIDA, 'Atención');
            }
        }

    }
}

function ReiniciarProvinciaListar() {
    var items = "<select class=\"form-control\" id=\"ID_PROVINCIA\">";
    items += "<option value=\"" + "" + "\"> --Seleccione-- </option>";
    items += "</select>";
    $("#ID_PROVINCIA").html(items);
}

function ReiniciarDistritoListar() {
    var items = "<select class=\"form-control\" id=\"ID_DISTRITO\">";
    items += "<option value=\"" + "" + "\"> --Seleccione-- </option>";
    items += "</select>";
    $("#ID_DISTRITO").html(items);
}

function Distrito_Listar(COD_UBIGEO) {
    if (COD_UBIGEO != '') {
        var item =
        {
            COD_UBIGEO: COD_UBIGEO,

        };
        var url = baseUrl + 'Seguridad/Ubigeo/Distrito_Listar';
        var auditoria = SICA.Ajax(url, item, false);
        if (auditoria != null) {
            if (auditoria.EJECUCION_PROCEDIMIENTO) {
                var items = "<select class=\"form-control\" id=\"ID_DISTRITO\">";
                items += "<option value=\"" + "" + "\"> --Seleccione-- </option>";
                for (i_ = 0; i_ < auditoria.OBJETO.length; i_++) {
                    items += "<option value=\"" + auditoria.OBJETO[i_].COD_UBIGEO + "\" > " + auditoria.OBJETO[i_].DESC_UBIGEO + " </option>";
                }
                items += "</select>";
                $("#ID_DISTRITO").html(items);
                debugger;
                if (!combo2 && $('#AccionExpedientes').val() == "M") {
                    $("#ID_DISTRITO").val($("#hdfID_DISTRITO").val());
                    combo2 = true;
                }
            } else {
                jAlert(auditoria.MENSAJE_SALIDA, 'Atención');
            }
        }
    }
}

