$(document).ready(function () {
    jQuery('.datatable').DataTable({
        "language": {
            "sProcessing": "Procesando...",
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": "No se encontraron resultados",
            "sEmptyTable": "Ningún dato disponible en esta tabla",
            "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
            "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            "sInfoPostFix": "",
            "sSearch": "Buscar:",
            "sUrl": "",
            "sInfoThousands": ",",
            "sLoadingRecords": "Cargando...",
            "oPaginate": {
                "sFirst": "Primero",
                "sLast": "Último",
                "sNext": "Siguiente",
                "sPrevious": "Anterior"
            },
            "oAria": {
                "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                "sSortDescending": ": Activar para ordenar la columna de manera descendente"
            }
        }
    });

    jQuery(".select2").select2();

    jQuery(".date").datetimepicker({
        format: "DD/MM/YYYY"
    });

    jQuery(".date-time").datetimepicker();

    jQuery(".date-todate").datetimepicker({
        format: "DD/MM/YYYY",
        disable: "true"
    });

    jQuery("#enfermedad-auxiliar").on("click", function () {
        jQuery("#modal-enfermedad").modal("show");
    })

    jQuery("#cargar-enfermedad").on("click", function () {
        var id = jQuery("#ID").val();
        var tipo = jQuery("#tipo");
        var enfermedad = jQuery("#enfermedad");
        var nombreTipo = jQuery("#tipo option:selected").text();
        var nombreEnfer = jQuery("#enfermedad option:selected").text();
        if (id != '' && id != null && id != 0) {
            if (tipo.val() != "" && enfermedad.val() != "" && enfermedad.val() != "0") {
                jQuery("#enfermedadPaciente").append(jQuery('<tr class="content"><td>' + nombreEnfer + '<input type="hidden" class="idEnfermedad" value="' + enfermedad.val() + '"/>' +
                                                            '<input type="hidden" class="idPaciente" value="' + id + '"/></td><td>' + nombreTipo + '</td><td><a class="remove" ' +
                                                            'style="cursor: pointer;"><i class="glyphicon glyphicon-remove"></i></a></td></tr>'));
                OrdenarEnfermedad();
                jQuery("#enfermedad").val("0");
                jQuery("#enfermedad").trigger("change.select2");
            }
            else {
                alert("Debe seleccionar una enfermedad.");
            }
        }
        else {
            alert("Debe registrar primero al paciente.");
        }
    });

    function OrdenarEnfermedad() {
        var cont = 0;
        $this = jQuery("#enfermedadPaciente tr.content");
        $this.each(function () {
            var option = jQuery(this);
            option.attr("id", "tr-" + cont);
            option.find("input.idEnfermedad").attr("id", "Enfermedades[" + cont + "].ID_ENFERMEDAD");
            option.find("input.idEnfermedad").attr("name", "Enfermedades[" + cont + "].ID_ENFERMEDAD");
            option.find("input.idPaciente").attr("id", "Enfermedades[" + cont + "].ID_PACIENTE");
            option.find("input.idPaciente").attr("name", "Enfermedades[" + cont + "].ID_PACIENTE");
            option.find("a.remove").data("tr", "tr-" + cont);
            cont = cont + 1;
        })
    }

    jQuery("#enfermedadPaciente").on("click", ".remove", function () {
        var data = jQuery(this).data("tr");
        jQuery("#enfermedadPaciente tr#" + data).remove();
        OrdenarEnfermedad();
    });

    jQuery("#tipo").on("change", function () {
        var valor = jQuery(this).val();
        jQuery.ajax({
            type: "GET",
            url: "/PACIENTE/ObtenerEnfermedad",
            data: { id: valor }
        }).done(function (myData) {
            var select = jQuery("#enfermedad");
            select.empty();
            jQuery("#enfermedad").append(jQuery('<option/>', { text: "Seleccione..", value: "0" }));
            jQuery.each(myData, function (index, itemData) {
                select.append(jQuery('<option/>', {
                    value: itemData.ID,
                    text: itemData.NOMBRE
                }));
            });
        }).fail(function () {
            alert("Ocurrio un error inesperado")
        })
    });

    jQuery("#nueva-enfermedad").on("click", function () {
        var nombre = jQuery("#NOMBRE").val();
        var descripcion = jQuery("#DESCRIPCION").val();
        var tipo = jQuery("#TIPO").val()
        var tipoText = jQuery("#TIPO :selected").text();
        var error = '';
        if (nombre == '' || nombre == null) {
            error = "Debe introducir un nombre para la enfermedad";
        }
        if (tipo == "0") {
            error = "Debe seleccionar el tipo de enfemedad";
        }
        if (error == '') {
            jQuery("#tabla-nueva-enfermedad").append(jQuery('<tr class="content"><td>' + nombre + '<input type="hidden" class="nombreEn" value="' + nombre + '"/>' +
                                                                '<input type="hidden" class="descripcionEn" value="' + descripcion + '"/><input type="hidden" class="tipoEn" ' +
                                                                'value = "' + tipo + '"/></td><td>' + descripcion + '</td><td>' + tipoText + '</td><td><a class="remove" ' +
                                                                ' style="cursor: pointer;"><i class="glyphicon glyphicon-remove"></i></a></td></tr>'))
        }
        else {
            alert(error);
        }
        OrdenarEnfermedadNueva();
        jQuery("#NOMBRE").val("");
        jQuery("#DESCRIPCION").val("");
        jQuery("#TIPO").val("0");
        jQuery("#TIPO").trigger("change.select2");
    });

    jQuery("#nuevo-modulo").on("click", function () {
        var nombre = $("#NOMBRE").val();
        var descripcion = $("#DESCRIPCION").val();
        if (nombre != '')
        {
            $("#tabla-nueva-modulo").append(jQuery('<tr class="content"><td>' + nombre + '<input type="hidden" class="nombreMod" value="' + nombre + '"/>' +
                                                   '<input type="hidden" class="descripcionMod" value="' + descripcion + '"/></td><td>' + descripcion + '</td>'+
                                                   '<td><a class="remove" style="cursor: pointer;"><i class="glyphicon glyphicon-remove"></i></a></tr>'));
        }
        else
        {
            alert("Introduzca un nombre para el modulo");
        }
        OrdenarModuloNuevo();
        $("#NOMBRE").val("");
        $("#DESCRIPCION").val("");
    })

    function OrdenarEnfermedadNueva() {
        var cont = 0;
        $this = jQuery("#tabla-nueva-enfermedad tr.content");
        $this.each(function () {
            var option = jQuery(this);
            option.attr("id", "tr-" + cont);
            option.find("input.nombreEn").attr("id", "listaEnfermedad[" + cont + "].NOMBRE");
            option.find("input.nombreEn").attr("name", "listaEnfermedad[" + cont + "].NOMBRE");
            option.find("input.descripcionEn").attr("id", "listaEnfermedad[" + cont + "].DESCRIPCION");
            option.find("input.descripcionEn").attr("name", "listaEnfermedad[" + cont + "].DESCRIPCION");
            option.find("input.tipoEn").attr("id", "listaEnfermedad[" + cont + "].TIPO");
            option.find("input.tipoEn").attr("name", "listaEnfermedad[" + cont + "].TIPO");
            option.find("a.remove").data("tr", "tr-" + cont);
            cont = cont + 1;
        })
    }

    function OrdenarModuloNuevo() {
        var cont = 0;
        $this = jQuery("#tabla-nueva-modulo tr.content");
        $this.each(function () {
            var option = jQuery(this);
            option.attr("id", "tr-" + cont);
            option.find("input.nombreMod").attr("id", "listaModulo[" + cont + "].NOMBRE");
            option.find("input.nombreMod").attr("name", "listaModulo[" + cont + "].NOMBRE");
            option.find("input.nombreMod").attr("id", "listaModulo[" + cont + "].DESCRIPCION");
            option.find("input.nombreMod").attr("name", "listaModulo[" + cont + "].DESCRIPCION");
            option.find("a.remove").data("tr", "tr-" + cont);
            cont = cont + 1;
        })
    }

    jQuery("#tabla-nueva-enfermedad").on("click", ".remove", function () {
        var data = jQuery(this).data("tr");
        jQuery("#tabla-nueva-enfermedad tr#" + data).remove();
        OrdenarEnfermedadNueva();
    });

    jQuery("#tabla-nueva-modulo").on("click", ".remove", function () {
        var data = jQuery(this).data("tr");
        jQuery("#tabla-nueva-modulo tr#" + data).remove();
        OrdenarModuloNuevo();
    });

    jQuery("#nueva-aseguradora").on("click", function () {
        var nombre = jQuery("#NOMBRE").val();
        var descripcion = jQuery("#DESCRIPCION").val();
        if (nombre != '') {
            jQuery("#tabla-nueva-aseguradora").append(jQuery('<tr class="content"><td>' + nombre + '<input type="hidden" class="nombreAs" value="' + nombre + '"/>' +
                            '<input type="hidden" class="descripcionAs" value="' + descripcion + '"/></td><td>' + descripcion + '</td>' +
                            '<td><a class="remove" style="cursor: pointer;"><i class="glyphicon glyphicon-remove"></i></a></td></tr>'));
            jQuery("#NOMBRE").val("");
            jQuery("#DESCRIPCION").val("");
        }
        else {
            alert("Debe introducir un nombre para la aseguradora");
        }
        OrdenarAseguradoraNueva();

    });

    function OrdenarAseguradoraNueva() {
        var cont = 0;
        $this = jQuery("#tabla-nueva-aseguradora tr.content");
        $this.each(function () {
            var option = jQuery(this);
            option.attr("id", "tr-" + cont);
            option.find("input.nombreAs").attr("id", "listaAseguradora[" + cont + "].NOMBRE");
            option.find("input.nombreAs").attr("name", "listaAseguradora[" + cont + "].NOMBRE");
            option.find("input.descripcionAs").attr("id", "listaAseguradora[" + cont + "].DESCRIPCION");
            option.find("input.descripcionAs").attr("name", "listaAseguradora[" + cont + "].DESCRIPCION");
            option.find("a.remove").data("tr", "tr-" + cont);
            cont = cont + 1;
        })
    }

    jQuery("#tabla-nueva-aseguradora").on("click", ".remove", function () {
        var data = jQuery(this).data("tr");
        jQuery("#tabla-nueva-aseguradora tr#" + data).remove();
        OrdenarEnfermedadNueva();
    });

    jQuery("#nueva-tratamiento").on("click", function () {
        var arreglo = [];
        var arreglo2 = [];
        jQuery("#diente :selected").each(function () { arreglo[$(this).val()] = $(this).text() });
        jQuery("#diente :selected").each(function () { arreglo2[$(this).val()] = $(this).val() });
        var nombre = jQuery("#NOMBRE").val();
        var descripcion = jQuery("#DESCRIPCION").val();
        var color = jQuery("#color").val();
        var monto = jQuery("#MONTO").val();
        if (arreglo) {

            for (var i = 1; i < 100; i++) {
                if (arreglo[i] != null) {
                    jQuery("#tabla-nueva-tratamiento").append(jQuery('<tr class="content"><td>' + nombre + '</td><td>' + descripcion + '<input type="hidden" class="idDiente"' +
                                                                    ' value="' + arreglo2[i] + '"/><input type="hidden" class="nombreTra" value="' + nombre + '"/><td>'+ color +'<input type="hidden" class="color" value="'+ color +'"/>'+
                                                                    '</td></td><td>' + arreglo[i] + '</td><input type="hidden" class="descripcionTra" value="' + descripcion + '"/><input type="hidden" class="monto" value="' + monto + '"/><td>' +
                                                                    '<a class="remove" style="cursor: pointer; color:red;"><i class="glyphicon glyphicon-remove"></i></a></td></tr>'));
                }
            }
            OrdenarTratamiento();
            jQuery("#NOMBRE").val("");
            jQuery("#DESCRIPCION").val("");
            jQuery("#diente").val("");
            jQuery("#diente").trigger("change.select2");
        }
    });

    function OrdenarTratamiento() {
        var cont = 0;
        var cont2 = 0;
        var cont3 = 0;
        var nombre = "";
        var vari = "";
        $this = jQuery("#tabla-nueva-tratamiento tr.content");
        $this.each(function () {
            var option = jQuery(this);
            option.attr("id", "tr-" + cont3);
            option.find("a.remove").data("tr", "tr-" + cont3);
            if (option.find("input.nombreTra").val() != nombre) {
                cont2 = 0;
                nombre = option.find("input.nombreTra").val();
                if (vari == "") {
                    vari = "listo";
                }
                else {
                    cont = cont + 1;
                }
            }

            option.find("input.nombreTra").attr("id", "listaTratamiento[" + cont + "].NOMBRE");
            option.find("input.nombreTra").attr("name", "listaTratamiento[" + cont + "].NOMBRE");
            option.find("input.descripcionTra").attr("id", "listaTratamiento[" + cont + "].DESCRIPCION");
            option.find("input.descripcionTra").attr("name", "listaTratamiento[" + cont + "].DESCRIPCION");
            option.find("input.color").attr("id", "listaTratamiento[" + cont + "].color");
            option.find("input.color").attr("name", "listaTratamiento[" + cont + "].color");
            option.find("input.monto").attr("id", "listaTratamiento[" + cont + "].monto");
            option.find("input.monto").attr("name", "listaTratamiento[" + cont + "].monto");
            option.find("input.idDiente").attr("id", "listaTratamiento[" + cont + "].listaTraDien[" + cont2 + "].ID_DIENTE");
            option.find("input.idDiente").attr("name", "listaTratamiento[" + cont + "].listaTraDien[" + cont2 + "].ID_DIENTE");

            cont2 = cont2 + 1;
            cont3 = cont3 + 1;
        });

    }

    jQuery("#agregarContrato").on("click", function () {
        if (Validador() == false) {
            var doctor = jQuery("#ID").val();
            //var doctor = 1;
            var seguro = jQuery("#ID_ASEGURADORA").val();
            var nombreSeguro = jQuery("#ID_ASEGURADORA option:selected").text()
            var finicio = jQuery("#FECHA_INICIO").val();
            var ffin = jQuery("#FECHA_FIN").val();
            if (doctor != 0 && doctor != undefined) {
                if (seguro != "" && finicio != "" && ffin != "") {
                    jQuery("#tablaContrato").append(jQuery('<tr class="contrato"><td><input type="hidden" class="idDoctor" value="' + doctor + '"/><input type="hidden" class="idSeguro" value="' + seguro + '"/>' +
                                                           '' + nombreSeguro + '</td><td><input type="hidden" class="finicio" value="' + finicio + '"/>' + finicio + '</td><td><input type="hidden" class="ffin" value="' + ffin + '"/>' +
                                                           '' + ffin + '</td><td><a class="remove" style="cursor: pointer; color:red;"><i class="glyphicon glyphicon-remove"></i></a></td></tr>'));
                    OrdenarContrato();
                }
                else {
                    alert("debe llenar todos los datos")
                }
            }
            else {
                alert('Debe crear doctor');
            }
        }
        else {
            alert('Ya esta aseguradora esta seleccionada.');
        }
    });

    function Validador() {
        var validador = false;
        var seguro = jQuery("#ID_ASEGURADORA").val();
        jQuery("#tablaContrato .contrato").each(function () {
            $this = jQuery(this);
            var valor = $this.find("input.idSeguro").val();
            if (valor == seguro) {
                validador = true;
            }
        });
        return validador;
    }

    function OrdenarContrato() {
        var cont = 0;
        jQuery("#tablaContrato .contrato").each(function () {
            $this = jQuery(this);
            $this.attr("id", "contrato-" + cont);
            $this.find("input.idDoctor").attr("id", "listaContratos[" + cont + "].ID").attr("name", "listaContratos[" + cont + "].ID");
            $this.find("input.idSeguro").attr("id", "listaContratos[" + cont + "].ID_ASEGURADORA").attr("name", "listaContratos[" + cont + "].ID_ASEGURADORA");
            $this.find("input.finicio").attr("id", "listaContratos[" + cont + "].FECHA_INICIO").attr("name", "listaContratos[" + cont + "].FECHA_INICIO");
            $this.find("input.ffin").attr("id", "listaContratos[" + cont + "].FECHA_FIN").attr("name", "listaContratos[" + cont + "].FECHA_FIN");
            $this.find("a.remove").data("tr", "contrato-" + cont);
            cont = cont + 1;
        })
    }

    jQuery("#tablaContrato").on("click", ".remove", function () {
        var data = jQuery(this).data("tr");
        jQuery("#" + data).remove();
    });

    jQuery(".diente").on("click", function () {
        if ($("#ID").val() != "" || $("#ID").val() != 0) {
            var update = $("#update").val();
            var nombre = $(this).data("nombre");
            console.log(nombre);
            var modal = $(this).data("modal");
            var table = $(this).data("table");
            var check = $(this).data("valuecheck");
            var idHistorial = $(this).data("idHistorial");
            console.log(modal);
            if (modal === undefined) {
                if (update == "false" || $(this).data('update') == true)
                {
                    jQuery.ajax({
                        type: "get",
                        url: "/PACIENTE/ObtenerDiente",
                        data: { nombre: nombre }
                    }).done(function (data) {
                        jQuery("#svg").empty().append(data.svg);
                        $("#modalhistorial h4").empty().text(nombre);
                        $("#dienteReal").val(nombre);
                        $("#modalhistorial").modal('show');
                    }).fail(function () {
                        alert("Ocurrio un error inesperado.")
                    });
                }
                else
                {
                    jQuery.ajax({
                        type: "get",
                        url: "/PACIENTE/GetHistorial",
                        data: { id: $("#ID").val(), diente: nombre }
                    }).done(function (data) {
                        jQuery("#svg").empty().append(data.svg_modal);
                        $("#modalhistorial h4").empty().text(nombre);
                        $("#dienteReal").val(nombre);
                        jQuery("#tablaHistoria tbody").empty();
                        $("#id_historia").val(data.id);
                        for (var i = 0; i < data.listaDetalle.length; i++)
                        {
                            jQuery("#tablaHistoria").append(jQuery('<tr class="tbh"><td>' + data.listaDetalle[i].nombreTratamiento + '<input type="hidden" class="post" value="' + data.listaDetalle[i].posicion + '"/></td>' +
                                                         '<td>' + data.listaDetalle[i].diente + '<input type="hidden" class="dient" value="' + data.listaDetalle[i].diente + '"/></td>' +
                                                         '<td>' + data.listaDetalle[i].color + '<input type="hidden" class="colors" value="' + data.listaDetalle[i].color + '"/></td>' +
                                                         '<td>' + data.listaDetalle[i].fecha + '<input type="hidden" class="fecha" value="' + data.listaDetalle[i].fecha + '"/></td>' +
                                                         '<td>' + data.listaDetalle[i].descripcion + '<input type="hidden" class="descr" value="' + data.listaDetalle[i].descripcion + '" />' +
                                                         '<input type="hidden" class="trat" value="' + data.listaDetalle[i].id_tratamiento + '"/><input type="hidden" class="id"' +
                                                         ' value="' + data.listaDetalle[i].id + '"/></td>' +
                                                         '<td><a class="removeHistoria" style="cursor:pointer; color:red;"><i class="glyphicon glyphicon-minus"/></a></td></tr>'));
                        }
                        OrdenarTablaHistorial();
                        $("#modalhistorial").modal('show');
                    });
                }
               
            }
            else {
                $("#modalhistorial h4").empty().text(nombre);
                $("#dienteReal").val(nombre);
                jQuery("#check").val(check);
                jQuery("#svg").empty().append(modal);
                $("#id_historia").val(idHistorial);
                jQuery("#tablaHistoria tbody").empty().append(table);
                if (check != "") {
                    var arr = check.split(",");
                    for (var i = 0; i < arr.length; i++) {
                        jQuery("#bodymodal").find("svg path").each(function () {
                            if ($(this).data('position') == arr[i]) {
                                $(this).data('check', true);
                            }
                        });
                        jQuery("#bodymodal").find("svg rect").each(function () {
                            if ($(this).data('position') == arr[i]) {
                                $(this).data('check', true);
                            }
                        });
                    }
                }
                OrdenarTablaHistorial();
                $("#modalhistorial").modal('show');
            }
        }
        else {
            alert("Se debe registrar primero al paciente.")
        }
    });

    jQuery("#agregar_tr_diente").on("click", function () {
        var diente = jQuery("#dienteReal").val();
        var pocision = jQuery("#position").val();
        var tratamiento = jQuery("#ID_TRATAMIENTO").val();
        var nomTrata = jQuery("#ID_TRATAMIENTO option:selected").text();
        var color = jQuery("#color").val();
        var fecha = jQuery("#FECHA_TRATA").val();
        var descripcion = jQuery("#descripcion").val();
        var cita = jQuery("#ID").val();
        if (pocision != "") {
            if (color != "000000" && tratamiento != "" && fecha != "") {
                jQuery("#tablaHistoria").append(jQuery('<tr class="tbh"><td>' + nomTrata + '<input type="hidden" class="post" value="' + pocision + '"/></td>' +
                                                        '<td>' + diente + '<input type="hidden" class="dient" value="' + diente + '"/><input ' +
                                                        'type="hidden" class="cita" value="' + cita + '"/></td>' +
                                                        '<td>' + color + '<input type="hidden" class="colors" value="' + color + '"/></td>' +
                                                        '<td>' + fecha + '<input type="hidden" class="fecha" value="' + fecha + '"/></td>' +
                                                        '<td>' + descripcion + '<input type="hidden" class="descr" value="' + descripcion + '" />' +
                                                        '<input type="hidden" class="trat" value="' + tratamiento + '"/><input type="hidden" class="id"' +
                                                        ' value=""/></td>' +
                                                        '<td><a class="removeHistoria" style="cursor:pointer; color:red;"><i class="glyphicon glyphicon-minus"/></a></td></tr>'));
                jQuery("#svg svg").find("path").each(function () {
                    $this = jQuery(this);
                    if ($this.data('position') == pocision) {
                        $this.data("check", true);
                    }
                });
                jQuery("#svg svg").find("rect").each(function () {
                    $this = jQuery(this);
                    if ($this.data('position') == pocision) {
                        $this.data("check", true);
                    }
                });
                OrdenarTablaHistorial();
                ResetModalTrat();
                jQuery("#position").val("");
                var check = jQuery("#check").val();
                if (check == "") {
                    jQuery("#check").val(pocision);
                }
                else {
                    jQuery("#check").val(check + "," + pocision);
                }
            }
            else {
                alert('Debe llenar todos los campos.')
            }
        }
        else {
            alert('Debe seleccionar una casilla del diente.')
        }
    });

    function OrdenarTablaHistorial() {
        var cont = 0;
        $("#tablaHistoria .tbh").each(function () {
            $this = $(this);
            $this.addClass("tbh-" + cont);
            $this.find("input.post").attr("id", "historial[" + cont + "].posicion");
            $this.find("input.post").attr("name", "historial[" + cont + "].posicion");
            $this.find("input.dient").attr("id", "historial[" + cont + "].diente");
            $this.find("input.dient").attr("name", "historial[" + cont + "].diente");
            $this.find("input.cita").attr("id", "historial[" + cont + "].id_cita");
            $this.find("input.cita").attr("name", "historial[" + cont + "].id_cita");
            $this.find("input.colors").attr("id", "historial[" + cont + "].color");
            $this.find("input.colors").attr("name", "historial[" + cont + "].color");
            $this.find("input.fecha").attr("id", "historial[" + cont + "].fecha");
            $this.find("input.fecha").attr("name", "historial[" + cont + "].fecha");
            $this.find("input.descr").attr("id", "historial[" + cont + "].descripcion");
            $this.find("input.descr").attr("name", "historial[" + cont + "].descripcion");
            $this.find("input.trat").attr("id", "historial[" + cont + "].id_tratamiento");
            $this.find("input.trat").attr("name", "historial[" + cont + "].id_tratamiento");
            $this.find("a.removeHistoria").data("valor", cont);
            cont = cont + 1;
        });
    };

    jQuery("#tablaHistoria").on("click", ".removeHistoria", function () {
        var valor = jQuery(this).data('valor');
        var tr = jQuery(".tbh-" + valor);
        var posit = jQuery(".tbh-" + valor).find('input.post').val();
        var diente = jQuery(".tbh-" + valor).find('input.dient').val();
        jQuery("#bodymodal svg").find("path").each(function () {
            $this = jQuery(this);
            if ($this.data('position') == posit) {
                $this.data('check', false).data('active', false).attr("style", "fill:black;");
            }
        });
        jQuery("#bodymodal svg").find("rect").each(function () {
            $this = jQuery(this);
            if ($this.data('position') == posit) {
                $this.data('check', false).data('active', false).attr("style", "fill:black;");
            }
        });
        tr.remove()
    });

    function ResetModalTrat() {
        jQuery("#ID_TRATAMIENTO").val("").change();
        jQuery("#color").val("000000").focus();
        jQuery("#descripcion").val("");
    }

    function ResetTotalTrat() {
        jQuery("#ID_TRATAMIENTO").val("").change();
        jQuery("#color").val("000000").focus();
        jQuery("#descripcion").val("");
        jQuery("#check").val("");
        $("#tablaHistoria tbody").empty();
        $("#id_historia").val(0);
    }

    jQuery("#bodymodal").on('click', '.casilla', function () {
        $this = jQuery(this);
        var color = $("#color").val();
        if (jQuery(this).data('check') === undefined || jQuery(this).data('check') == false) {
            console.log('bolas');
            if (jQuery(this).data('active') === undefined || jQuery(this).data('active') == false) {
                jQuery("#position").val($this.data('position'));
                jQuery("#diente").val($this.data('diente'));
                jQuery("#bodymodal").find("svg path").each(function () {
                    if ($(this).data('check') === undefined || jQuery(this).data('check') == false) {
                        $(this).removeAttr("style", "stroke:red;stroke-width: 3px;").data('active', false);
                    }
                });
                jQuery("#bodymodal").find("svg rect").each(function () {
                    if ($(this).data('check') === undefined || jQuery(this).data('check') == false) {
                        $(this).removeAttr("style", "stroke:red;stroke-width: 3px;").data('active', false);
                    }
                });
                $this.attr("style", "stroke:red;stroke-width: 3px;");
                $this.data("active", true);
                if (color != "000000") {
                    $this.attr("style", "fill:#" + color);
                }
            }
            else {
                $this.data("active", false);
                $this.removeAttr("style", "stroke:red;stroke-width: 3px;");
                $this.attr("style", "fill:black;");
            }
        }
    });

    jQuery("#ID_TRATAMIENTO").on("change", function () {
        var valor = $(this).val();
        if (valor != "") {
            jQuery.ajax({
                type: "get",
                url: "/PACIENTE/ObtenerColor",
                data: { id: valor }
            }).done(function (data) {
                var color = data;
                jQuery("#color").val(color);
                jQuery("#color").focus();
                jQuery("#svg").find("svg path").each(function () {
                    $this = jQuery(this);
                    if (($this.data('active') == true) && ($this.data('check') == false || $this.data('check') === undefined)) {
                        $this.attr("style", "fill:#" + color);
                    }
                });
                jQuery("#svg").find("svg rect").each(function () {
                    $this = jQuery(this);
                    if (($this.data('active') == true) && ($this.data('check') == false || $this.data('check') === undefined)) {
                        $this.attr("style", "fill:#" + color);
                    }
                });
            }).fail(function () {
                alert('Ocurrio un error inesperado');
            });
        }
    });

    jQuery("#save").on("click", function () {
        var diente = jQuery("#diente").val();
        if (diente != "") {
            var tr = $("#tablaHistoria tr.tbh");
            jQuery("." + diente + ".diente").find("path").each(function () {
                $(this).attr('style', 'fill:#000000');
            });
            jQuery("." + diente + ".diente").find("rect").each(function () {
                $(this).attr('style', 'fill:#000000');
            });
            var historial;
            var arr = [];
            var cont = 0;
            if (tr.length || $("#id_historia").val() != 0) {
                tr.each(function () {
                    $this = $(this);
                    var position = $this.find("input.post").val();
                    var color = $this.find("input.colors").val();
                    jQuery("." + diente + ".diente").find("path").each(function () {
                        $diente = $(this);
                        if ($diente.data('position') == position) {
                            $diente.attr("style", "fill:#" + color);
                        }
                    });
                    jQuery("." + diente + ".diente").find("rect").each(function () {
                        $diente = $(this);
                        if ($diente.data('position') == position) {
                            $diente.attr("style", "fill:#" + color);
                        }
                    });

                    arr[cont] = {
                        id: $this.find("input.id").val(),
                        id_tratamiento: $this.find("input.trat").val(),
                        fecha: $this.find("input.fecha").val(),
                        color: $this.find("input.colors").val(),
                        descripcion: $this.find("input.descr").val(),
                        posicion: $this.find("input.post").val(),
                        diente: $this.find("input.dient").val()
                    };
                    cont = cont + 1;
                });

                jQuery("." + diente + ".diente").data('update', "false");

                historial = {
                    id: $("#id_historia").val(),
                    id_paciente: $("#ID").val(),
                    id_cita: $("#id_cita").val(),
                    svg_doctor: $("#svg svg").prop('outerHTML'),
                    svg_paciente: jQuery("." + diente + ".diente").prop('outerHTML'),
                    svg_modal: $("#svg svg").prop('outerHTML'),
                    diente: jQuery("#dienteReal").val(),
                    listaDetalle: arr
                }

                jQuery.ajax({
                     type: "post",
                     url: "/PACIENTE/CrearHistorial",
                     data: {historial:historial}
                }).done(function (data) {
                    var i = 0;
                     tr.each(function () {
                         $this = $(this);
                         $this.find("input.id").val(data.listaDetalle[i].id);
                         i = i + 1;
                     });
                     $("#id_historia").val(data.id);
                     console.log(data.id);
                     var modal = $("#svg svg").clone();
                     var table = $("#tablaHistoria tr.tbh").clone();
                     var check = jQuery("#check").val();
                     jQuery("." + diente + ".diente").data('modal', modal).data("valuecheck", check).data("table", table).data("idHistorial",data.id);
                     ResetTotalTrat();
                     $("#modalhistorial").modal('hide');
                }).fail(function () {
                    alert("Ocurrio un error inesperado.");
                })
                
            }
            else {
                jQuery("." + diente + ".diente").find("path").each(function () {
                    $(this).attr('style', 'display:none;');
                });
                jQuery("." + diente + ".diente").find("rect").each(function () {
                    $(this).attr('style', 'display:none;');
                });
                var modal = $("#svg svg").clone();
                var table = $("#tablaHistoria tr.tbh").clone();
                var check = jQuery("#check").val();
                jQuery("." + diente + ".diente").data('modal', modal).data("valuecheck", check).data("table", table);
                ResetTotalTrat();
                $("#modalhistorial").modal('hide');
            }
        }
    });

    jQuery("#close").on("click", function () {
        ResetTotalTrat();
    });

    jQuery("#ID_CITA").on("change", function () {
        var id = $(this).val();
        if (id != "")
        {
            jQuery.ajax({
                type: "get",
                url: "/Pago/ObtenerCita",
                data: { id: id }
            }).done(function (data) {
                $("#doctor").html(data.cita.nombreDoctor);
                $("#paciente").html(data.cita.nombrePaciente);
                $("#fecha").html(data.cita.FECHA);
                $("#id_paciente").val(data.cita.ID_PACIENTE);
                var monto = 0;
                $("#tabla_detalle_tratamiendo tbody").empty();
                for (var i = 0; i < data.cita.historial.length; i++) {
                    for (var j = 0; j < data.cita.historial[i].listaDetalle.length; j++) {
                        $("#tabla_detalle_tratamiendo").append(jQuery('<tr><td>' + data.cita.historial[i].listaDetalle[j].nombreTratamiento + '</td><td>' +
                                                                    '' + data.cita.historial[i].diente + '</td><td style="text-align:center">' + data.cita.historial[i].listaDetalle[j].monto + '</td></tr>'))
                        monto = monto + data.cita.historial[i].listaDetalle[j].monto;
                    }
                }
                $("#tabla_detalle_tratamiendo").append(jQuery('<tr><td></td><td><h4 style="margin:0;float:right;">Total</h4></td><td style="text-align:center">' + monto + '</td></tr>'));
                $("#monto").val(monto);
                $("#montoTotal").val(monto);
                $("#montoRestante").val(monto);
                if (data.aseguradora != null) {
                    $("#idase").empty().append(jQuery('<select id="idAseguradora" name="idAseguradora" class="form-control"></select>'));
                    for (var t = 0; t < data.aseguradora.length; t++) {
                        $("#idAseguradora").append(jQuery('<option value="' + data.aseguradora[t].id + '">' + data.aseguradora[t].nombre + '</option>'));
                    }

                    $("#idAseguradora").select2();
                }

                jQuery.ajax({
                    type: "get",
                    url: "/Pago/ObtenerCobertura",
                    data: { idPaciente: $("#id_paciente").val() }
                }).done(function (data) {
                    $("#cober").attr("style", "color:black;");
                    if (data == "0.00")
                    {
                        $("#cober").text(data + " Bsf").attr("style", "color:red;");
                        $("#coberReal").val(data);
                    }
                    else
                    {
                        $("#cober").text(data + " Bsf");
                        $("#coberReal").val(data);
                    }
                }).fail(function () {
                    alert("Ocurrio un error");
                })

            }).fail(function () {
                alert("Ocurrio un error inesperado");
            });
        }
        else
        {
            alert("Debe seleccionar una cita valida");
        }
    });

    function ResetPago()
    {
        $("#doctor").html("");
        $("#paciente").html("");
        $("#fecha").html("");
        $("#div_seguro").attr("style", "display:none;");
        $("#tabla_detalle_tratamiendo tbody").empty();
    }

    $("#id_tipocobro").on("change", function () {
        
        if ($(this).val() == "Seguro")
        {
            $("#div_seguro").attr("style", "display:block;");
            $("#registrar_pago").attr("style", "display:block;float: right;margin-right: 15px;margin-top:20px;");
            $("#Pago").attr("style", "display:none;");
            $("#cobertura").attr("style", "display:block;");
        }
        else {
            $("#div_seguro").attr("style", "display:none;");
            $("#registrar_pago").attr("style", "display:none;");
            $("#Pago").attr("style", "display:block;float: right;margin-right: 15px;margin-top:20px;");
            $("#cobertura").attr("style", "display:none;");
        }
    });

    $("#idReporte").val("");
    $("#idReporte").trigger("change");

    $("#idReporte").on("change", function () {
        var valor = $(this).val();
        switch(valor)
        {
            case "0":$(".div-reporte").attr("style", "display:none;");
                $("#div-fechas").attr("style", "display:block;");
                break;
            case "1": $(".div-reporte").attr("style", "display:none;");
                $("#div-paciente").attr("style", "display:block;");
                $("#idPaciente").select2();
                break;
            case "2": $(".div-reporte").attr("style", "display:none;");
                $("#div-seguro").attr("style", "display:block;");
                $("#idSeguro").select2();
                break;
            case "3": $(".div-reporte").attr("style", "display:none;");
                $("#div-tratamiento").attr("style", "display:block;");
                $("#idTrata").select2();
                break;
            case "4": $(".div-reporte").attr("style", "display:none;");
                $("#div-paciente").attr("style", "display:block;");
                $("#idPaciente").select2();
                break;
            case "5": $(".div-reporte").attr("style", "display:none;");
                $("#div-seguro").attr("style", "display:block;");
                $("#idSeguro").select2();
                break;
            case "6": $(".div-reporte").attr("style", "display:none;");
                $("#div-fechas").attr("style", "display:block;");
                break;
            case "7": $(".div-reporte").attr("style", "display:none;");
                break;
            case "8": $(".div-reporte").attr("style", "display:none;");
                $("#div-seguro").attr("style", "display:block;");
                $("#idSeguro").select2();
                break;
            case "": $(".div-reporte").attr("style", "display:none;");
                break;
        }
    });

    $("#cargarReporte").on("click", function () {
        var reporte = $("#idReporte").val();
        var titulo = $("#idReporte :selected").text();
        var nombre;
        var fechaInicio = $("#fechaInicio").val();
        var fechaFin = $("#fechaFin").val();
        var seguro = $("#idSeguro").val();
        var paciente = $("#idPaciente").val();
        var query;
        switch (reporte)
        {
            case "0":
                nombre = "ObtenerCitaPorRango";
                query = { fechaInicio: fechaInicio, fechaFin: fechaFin };
                break;
            case "1":
                nombre = "ObtenerTratamientoPaciente";
                query = { id: paciente };
                break;
            case "2":
                nombre = "ObtenerPacienteAseguradora";
                query = { id: seguro };
                break;
            case "5":
                nombre = "ContratoPorAseguradora";
                query = { id: seguro };
                break;
            case "8":
                nombre = "EstatusPagoAseguradora";
                query = { id: seguro };
                break;
        }

        jQuery.ajax({
            type: "get",
            url: "/Reporte/" + nombre,
            data: query
        }).done(function (data) {
            $("#titulo").text(titulo);
            $("#reporte").empty().append(data).attr("style", "display:block");
        }).fail(function () {
            alert('Ocurrio un error inesperado');
        })

        /*var data = $("#reporte").data('visual');

        if (data == 0) {
            $("#reporte").attr("style", "display:block");
            $("#reporte").data('visual', '1');
        }

        var titulo = $("#idReporte :selected").text();
        $("#titulo").text(titulo);*/
    });

    $("#Pago").on("click", function () {
        $("#modalPago").modal("show");
    });

    $("#IdTipoPago").on("change", function () {
        var valor = $(this).val();
        var texto = $("#IdTipoPago selected").text();
        switch (valor) {
            case "1":
                $(".tipos").attr("style", "display:none;");
                break;
            case "2":
                $(".tipos").attr("style", "display:none;");
                $(".comprobante").attr("style", "display:block");
                break;
            case "3":
                $(".tipos").attr("style", "display:none;");
                $(".credito").attr("style", "display:block");
                break;
            case "4":
                $(".tipos").attr("style", "display:none;");
                $(".cheque").attr("style", "display:block");
                break;
        }
    });

    $("#agregarPago").on("click", function () {
        var tipoPago = $("#IdTipoPago").val();
        var textoPago = $("#IdTipoPago :selected").text();
        var tipoTarjeta = $("#idTipoTarjeta").val();
        var textoTarjeta = ($("#idTipoTarjeta :selected").text() == "Seleccione" ? "-" : $("#idTipoTarjeta :selected").text());
        var cheque = ($("#numeroCheque").val() == "" ? "-" : $("#numeroCheque").val());
        var comprobante = ($("#numeroComprobante").val() == "" ? "-" : $("#numeroComprobante").val());
        var monto = $("#montoPagado").val();
        var montoRestante = $("#montoRestante").val();
        if (monto != 0)
        {
            $("#tablaPago").append('<tr class="pago"><td>' + textoPago + '</td><td>' + textoTarjeta + '</td><td>' + cheque + '</td><td>' + comprobante + '</td><td class="monto">' + monto + '</td>' +
                                    '<td><a class="removePago" style="cursor:pointer; color:red;"><i class="glyphicon glyphicon-minus"/></a></td></tr>');
            $("#montoRestante").val(montoRestante - monto);
            Ordenar();
            ResetPago();
        }
        else {
            alert('Debe ingresar un monto');
        }
    });

    function ResetPago() {
        $("#IdTipoPago").val("");
        $("#idTipoTarjeta").val("");
        $("#numeroCheque").val("");
        $("#numeroComprobante").val("");
        $("#montoPagado").val("");
        $(".tipos").attr("style", "display:none;");
    };

    function Ordenar() {
        var cont = 0;
        $(".pago").each(function () {
            $(this).addClass("pago-" + cont);
            $(this).find("a").data("tr", "pago-" + cont);
            cont = cont + 1;
        });
    }

    $("#tablaPago").on("click", ".removePago", function () {
        var tr = $(this).data('tr');
        var monto = parseInt($("." + tr).find(".monto").text());
        var restante = parseInt($("#montoRestante").val());
        var total = restante + monto;
        $("#montoRestante").val(total);
        $("." + tr).remove();
    });

    $("#seguro").on("change", function () {
        var valor = $("#seguro :selected").text();
        var cedula = $("#cedula").val();
        if (cedula != "") {
            loader(valor, cedula);
        }
        else {
            alert("Debe colocar la cedula");
        }
    });

    $("#cedula").on("change", function () {
        var valor = $("#seguro :selected").text();
        var cedula = $("#cedula").val();
        if (valor != "") {
            loader(valor, cedula);
        }
        else {
            alert("Debe colocar la cedula");
        }
    })

    function loader(valor, cedula)
    {
        jQuery("#cober").empty().append(jQuery('<img id="img-cober" src="../images/2.gif" />'));
        if (valor == "Seguros Universitas") {
            setTimeout(function () {
                if (cedula == "17299794") {
                    jQuery("#cober").empty().append(jQuery("<span>200.000,00 Bsf.</span>"));
                    jQuery("#cobertura").val("200000");
                }
                else {
                    jQuery("#cober").empty().append(jQuery("<span>0 Bsf.</span>"));
                    jQuery("#cobertura").val("0");
                }
            }, 5000);
        }
        else {
            setTimeout(function () {
                jQuery("#cober").empty().append(jQuery("<span>No posee cuenta en este seguro</span>"));
            }, 5000);
        }
    }

    jQuery("#registrar_pago").on("click", function () {
        var cober = $("#coberReal").val();
        console.log(cober);
        if (cober == 0) {
            alert("No se puede procesar la factura sin cobertura disponible");
        }
        else {
            ResetPago();
            jQuery("#crear-pago").trigger("submit");
        }
        
    })

    //////////////////////////////////////////////////////////////////////submit////////////////////////////////////////////////////////////
    jQuery("#crear-paciente").ajaxForm({
        dataType: "json",
        beforeSubmit: function () {
            jQuery.blockUI({
                centerY: 0,
                css: { top: '10px', left: '', right: '10px' },
                message: "Procesando Solicitud"
            });
        },
        success: function (data) {
            jQuery.unblockUI();
            if (data) {
                jQuery("#ID").val(data.ID);
                if (data.messageError != '' && data.messageError != null) {
                    jQuery("#mensaje").append(jQuery('<div class="alert alert-danger alert-dismissible" style="margin-bottom:-20px;" id="alerta" role="alert">' +
                                                    '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
                                                    '<strong>' + data.messageError + '</strong></div>'));
                    window.setTimeout(function () {
                        jQuery("#alerta").remove()
                    }, 5000);
                }
                else {
                    jQuery("#mensaje").append(jQuery('<div class="alert alert-success alert-dismissible" style="margin-bottom:-20px;" id="alerta" role="alert">' +
                                                    '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
                                                    '<strong>Los datos fueron registrados con exito.</strong></div>'));
                    window.setTimeout(function () {
                        jQuery("#alerta").remove()
                    }, 5000);
                }
            }
        }

    });

    jQuery("#enfermedad-paciente").ajaxForm({
        dataType: "json",
        beforeSubmit: function () {
            jQuery.blockUI({ message: "Ocurrio un error inesperado" });
        },
        success: function (data) {
            jQuery.unblockUI();
        }
    });

    jQuery("#crear-enfermedad").ajaxForm({
        dataType: "json",
        beforeSubmit: function () {
            jQuery.blockUI({
                centerY: 0,
                css: { top: '10px', left: '', right: '10px' },
                message: "Procesando Solicitud"
            });
        },
        success: function (data) {
            jQuery.unblockUI();
            if (data.messageError != '' && data.messageError != null) {
                jQuery("#mensaje").append(jQuery('<div class="alert alert-danger alert-dismissible" style="margin-bottom:-20px;" id="alerta" role="alert">' +
                                                '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
                                                '<strong>' + data.messageError + '</strong></div>'));
                window.setTimeout(function () {
                    jQuery("#alerta").remove()
                }, 5000);
            }
            else {
                jQuery("#mensaje").append(jQuery('<div class="alert alert-success alert-dismissible" style="margin-bottom:-20px;" id="alerta" role="alert">' +
                                                '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
                                                '<strong>Los datos fueron registrados con exito.</strong></div>'));
                window.setTimeout(function () {
                    jQuery("#alerta").remove()
                }, 5000);
            }
        }
    });

    jQuery("#crear-aseguradora").ajaxForm({
        dataType: "json",
        beforeSubmit: function () {
            jQuery.blockUI({
                centerY: 0,
                css: { top: '10px', left: '', right: '10px' },
                message: "Procesando Solicitud"
            });
        },
        success: function (data) {
            jQuery.unblockUI();
            if (data.messageError != '' && data.messageError != null) {
                jQuery("#mensaje").append(jQuery('<div class="alert alert-danger alert-dismissible" style="margin-bottom:-20px;" id="alerta" role="alert">' +
                                                '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
                                                '<strong>' + data.messageError + '</strong></div>'));
                window.setTimeout(function () {
                    jQuery("#alerta").remove()
                }, 5000);
            }
            else {
                jQuery("#mensaje").append(jQuery('<div class="alert alert-success alert-dismissible" style="margin-bottom:-20px;" id="alerta" role="alert">' +
                                                '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
                                                '<strong>Los datos fueron registrados con exito.</strong></div>'));
                window.setTimeout(function () {
                    jQuery("#alerta").remove()
                }, 5000);
            }
        }
    })

    jQuery("#guardaModal").on("click", function () {
        var nombre = jQuery("#nombreModal").val();
        var descripcion = jQuery("#descripcionModal").val();
        var tipo = jQuery("#tipoModal").val();
        var mensaje = '';
        if (nombre == '' || nombre == null) {
            mensaje = "El nombre no puede estar vacio";
        }
        if (tipo == "0") {
            mensaje = "Debe seleccionar el tipo";
        }
        if (mensaje == "") {
            jQuery.ajax({
                type: "post",
                url: "/PACIENTE/EnfermedadModal",
                data: { NOMBRE: nombre, DESCRIPCION: descripcion, TIPO: tipo }
            }).done(function (data) {
                if (data.mensaje) {
                    jQuery("#mensaje-modal").append(jQuery('<div class="alert alert-danger alert-dismissible" id="alerta" style="margin-bottom:0px;" role="alert">' +
                                                            '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
                                                            '<strong>' + data.messageError + '</strong></div>'))
                }
                else {
                    jQuery("#mensaje-modal").append(jQuery('<div class="alert alert-success alert-dismissible" id="alerta" style="margin-bottom:0px;" role="alert">' +
                                                            '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
                                                            '<strong>Fue registrado con exito.</strong></div>'))
                }
                jQuery("#nombreModal").val("");
                jQuery("#descripcionModal").val("");
                jQuery("#tipoModal").val("");
                jQuery("#tipoModal").trigger("change.select2");
                window.setTimeout(function () {
                    jQuery("#alerta").remove()
                }, 5000);
            }).fail(function () {
                alert("Ocurrio un error inesperado");
            })
        }
        else { alert(mensaje); }
    })

    jQuery("#closeModal").on("click", function (evt) {
        evt.preventDefault();
        jQuery("#enfermedad").empty();
        jQuery("#enfermedad").append(jQuery('<option/>', { text: "Seleccione", value: "0" }));
        jQuery("#enfermedad").val("0");
        jQuery("#enfermedad").trigger("change.select2");
        jQuery("#tipo").val("0");
        jQuery("#tipo").trigger("change.select2");
        jQuery("#tipoModal").trigger("change.select2");
        jQuery("#modal-enfermedad").modal("close");
    })

    jQuery("#modificar-enfermedad").ajaxForm({
        dataType: "json",
        beforeSubmit: function () {
            jQuery.blockUI({
                centerY: 0,
                css: { top: '10px', left: '', right: '10px' },
                message: "Procesando Solicitud"
            });
        },
        success: function (data) {
            jQuery.unblockUI();
            if (data.messageError != '' && data.messageError != null) {
                jQuery("#mensaje").append(jQuery('<div class="alert alert-danger alert-dismissible" style="margin-bottom:-20px;" id="alerta" role="alert">' +
                                                '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
                                                '<strong>' + data.messageError + '</strong></div>'));
                window.setTimeout(function () {
                    jQuery("#alerta").remove()
                }, 5000);
            }
            else {
                jQuery("#mensaje").append(jQuery('<div class="alert alert-success alert-dismissible" style="margin-bottom:-20px;" id="alerta" role="alert">' +
                                                '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
                                                '<strong>Los datos fueron registrados con exito.</strong></div>'));
                window.setTimeout(function () {
                    jQuery("#alerta").remove()
                }, 5000);
            }
        }
    });

    jQuery("#crear-tratamiento").ajaxForm({
        dataType: "json",
        beforeSubmit: function () {
            jQuery.blockUI({
                centerY: 0,
                css: { top: '10px', left: '', right: '10px' },
                message: "Procesando Solicitud"
            });
        },
        success: function (data) {
            jQuery.unblockUI();
            if (data.messageError != '' && data.messageError != null) {
                jQuery("#mensaje").append(jQuery('<div class="alert alert-danger alert-dismissible" style="margin-bottom:-20px;" id="alerta" role="alert">' +
                                                '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
                                                '<strong>' + data + '</strong></div>'));
                window.setTimeout(function () {
                    jQuery("#alerta").remove()
                }, 5000);
            }
            else {
                jQuery("#mensaje").append(jQuery('<div class="alert alert-success alert-dismissible" style="margin-bottom:-20px;" id="alerta" role="alert">' +
                                                '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
                                                '<strong>Los datos fueron registrados con exito.</strong></div>'));
                window.setTimeout(function () {
                    jQuery("#alerta").remove()
                }, 5000);
            }
        }
    });

    jQuery("#crear-doctor").ajaxForm({
        dataType: "json",
        beforeSubmit: function () {
            jQuery.blockUI({
                centerY: 0,
                css: { top: '10px', left: '', right: '10px' },
                message: "Procesando Solicitud"
            });
        },
        success: function (data) {
            jQuery.unblockUI();
            if (data.messageError != '' && data.messageError != null) {
                jQuery("#mensaje").append(jQuery('<div class="alert alert-danger alert-dismissible" style="margin-bottom:-20px;" id="alerta" role="alert">' +
                                                '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
                                                '<strong>' + data + '</strong></div>'));
                window.setTimeout(function () {
                    jQuery("#alerta").remove()
                }, 5000);

            }
            else {
                jQuery("#mensaje").append(jQuery('<div class="alert alert-success alert-dismissible" style="margin-bottom:-20px;" id="alerta" role="alert">' +
                                                '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
                                                '<strong>Los datos fueron registrados con exito.</strong></div>'));
                window.setTimeout(function () {
                    jQuery("#alerta").remove()
                }, 5000);

                jQuery("#ID").val(data.ID);
            }
        }
    })

    jQuery("#modificar-enfermedad-paciente").ajaxForm({
        dataType: "json",
        beforeSubmit: function () {
            jQuery.blockUI({
                centerY: 0,
                css: { top: '10px', left: '', right: '10px' },
                message: "Procesando Solicitud"
            });
        },
        success: function (data) {
            jQuery.unblockUI();
            if (data.messageError != '' && data.messageError != null) {
                jQuery("#mensaje-error-enfermedad").append(jQuery('<div class="alert alert-danger alert-dismissible" style="margin-bottom:-20px;" id="alerta" role="alert">' +
                                                '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
                                                '<strong>' + data + '</strong></div>'));
                window.setTimeout(function () {
                    jQuery("#alerta").remove()
                }, 5000);
            }
            else {
                jQuery("#mensaje-error-enfermedad").append(jQuery('<div class="alert alert-success alert-dismissible" style="margin-bottom:-20px;" id="alerta" role="alert">' +
                                                '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
                                                '<strong>Los datos fueron registrados con exito.</strong></div>'));
                window.setTimeout(function () {
                    jQuery("#alerta").remove()
                }, 5000);
            }
        }
    });

    jQuery("#crear-contrato").ajaxForm({
        dataType: "json",
        beforeSubmit: function () {
            jQuery.blockUI({
                centerY: 0,
                css: { top: '10px', left: '', right: '10px' },
                message: "Procesando Solicitud"
            });
        },
        success: function (data) {
            jQuery.unblockUI();
            if (data.messageError != '' && data.messageError != null) {
                jQuery("#mensaje-error-contrato").append(jQuery('<div class="alert alert-danger alert-dismissible" style="margin-bottom:-20px;" id="alerta" role="alert">' +
                                                '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
                                                '<strong>' + data.messageError + '</strong></div>'));
                window.setTimeout(function () {
                    jQuery("#alerta").remove()
                }, 5000);
            }
            else {
                jQuery("#mensaje-error-contrato").append(jQuery('<div class="alert alert-success alert-dismissible" style="margin-bottom:-20px;" id="alerta" role="alert">' +
                                                '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
                                                '<strong>Los datos fueron registrados con exito.</strong></div>'));
                window.setTimeout(function () {
                    jQuery("#alerta").remove()
                }, 5000);
            }
        }
    })

    jQuery("#crear-cita").ajaxForm({
        dataType: "json",
        beforeSubmit: function () {
            jQuery.blockUI({
                centerY: 0,
                css: { top: '10px', left: '', right: '10px' },
                message: "Procesando Solicitud"
            });
        },
        success: function (data) {
            jQuery.unblockUI();
            if (data.messageError != '' && data.messageError != null) {
                jQuery("#mensaje").append(jQuery('<div class="alert alert-danger alert-dismissible" style="margin-bottom:-20px;" id="alerta" role="alert">' +
                                                '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
                                                '<strong>' + data.messageError + '</strong></div>'));
                window.setTimeout(function () {
                    jQuery("#alerta").remove()
                }, 5000);
            }
            else {
                jQuery("#mensaje").append(jQuery('<div class="alert alert-success alert-dismissible" style="margin-bottom:-20px;" id="alerta" role="alert">' +
                                                '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
                                                '<strong>Los datos fueron registrados con exito.</strong></div>'));
                window.setTimeout(function () {
                    jQuery("#alerta").remove()
                }, 5000);
            }
        }
    });

    jQuery("#modificar-cita").ajaxForm({
        dataType: "json",
        beforeSubmit: function () {
            jQuery.blockUI({
                centerY: 0,
                css: { top: '10px', left: '', right: '10px' },
                message: "Procesando Solicitud"
            });
        },
        success: function (data) {
            jQuery.unblockUI();
            if (data.messageError != '' && data.messageError != null) {
                jQuery("#mensaje").append(jQuery('<div class="alert alert-danger alert-dismissible" style="margin-bottom:-20px;" id="alerta" role="alert">' +
                                                '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
                                                '<strong>' + data.messageError + '</strong></div>'));
                window.setTimeout(function () {
                    jQuery("#alerta").remove()
                }, 5000);
            }
            else {
                jQuery("#mensaje").append(jQuery('<div class="alert alert-success alert-dismissible" style="margin-bottom:-20px;" id="alerta" role="alert">' +
                                                '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
                                                '<strong>Los datos fueron registrados con exito.</strong></div>'));
                window.setTimeout(function () {
                    jQuery("#alerta").remove()
                }, 5000);
            }
        }
    })

    jQuery("#crear-pago").ajaxForm({
        dataType: "json",
        beforeSubmit: function () {
            jQuery.blockUI({
                centerY: 0,
                css: { top: '10px', left: '', right: '10px' },
                message: "Procesando Solicitud"
            });
        },
        success: function (data) {
            jQuery.unblockUI();
            if (data.messageError != '' && data.messageError != null) {
                jQuery("#mensaje").append(jQuery('<div class="alert alert-danger alert-dismissible" style="margin-bottom:-20px;" id="alerta" role="alert">' +
                                                '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
                                                '<strong>' + data.messageError + '</strong></div>'));
                window.setTimeout(function () {
                    jQuery("#alerta").remove()
                }, 5000);
            }
            else {
                jQuery("#mensaje").append(jQuery('<div class="alert alert-success alert-dismissible" style="margin-bottom:-20px;" id="alerta" role="alert">' +
                                                '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
                                                '<strong>Los datos fueron registrados con exito.</strong></div>'));
                window.setTimeout(function () {
                    jQuery("#alerta").remove()
                }, 5000);
            }
        }
    })

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

});