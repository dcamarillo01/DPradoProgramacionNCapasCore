

// DATE PICKER
$(document).ready(function () {

    
    //$("#DatePicker").datepicker({
    //    dateFormat: 'dd/mm/yy', // Date format
    //    changeMonth: true,      // Month dropdown
    //    changeYear: false,       // Year dropdown
    //    yearRange: "1980:2025",  // Year range
    //    showAnim: "drop",
    //    maxDate: '+30Y',
    //    inline: true
    //});




    $("#DatePicker").flatpickr({
        dateFormat: "m-d-Y",
    });

    $("#DatePicker").attr("readonly", true);
    //$("#DatePicker2").datepicker({
    //    dateFormat: 'dd/mm/yy', // Date format
    //    changeMonth: true,      // Month dropdown
    //    changeYear: true,       // Year dropdown
    //    yearRange: "1980:2025",  // Year range
    //    showAnim: "drop",
    //    maxDate: '+30Y',
    //    inline: true
    //});

    $("#DatePicker2").flatpickr({
        dateFormat: "m-d-Y",
    });

    $("#DatePicker2").attr("readonly", true);

});

$(document).ready(function () {
    $("#buscarBTN").click(function () {
        $("#tablaBuscar").toggle();
        if ($("#tablaBuscar").is(":visible")) {
            $('#tablaBuscar').show();

        } else {
            $('#tablaBuscar').hide();

        }
    });
});

// LLenar Departamentos
function FillDepartamentos() {


    $.ajax({
        url: urlActionDepartamento,
        dataType: 'JSON',
        method: 'GET',
        data: {},
        success: function (resultDepartamento) {
            console.log(resultDepartamento)
            console.log(resultDepartamento.correct)
            if (resultDepartamento.correct) {

                $("#ddlDepartamentos").empty()


                $("#ddlDepartamentos").append('<option value=""> Seleccione un departamento</option>')

                $.each(resultDepartamento.objects, function (i, departamento) {

                    $("#ddlDepartamentos").append('<option value="' + departamento.idDepartamento + '">' + departamento.descripcion + '</option>')
                })
                $("#ddlDepartamentos2").empty()


                $("#ddlDepartamentos2").append('<option value=""> Seleccione un departamento</option>')

                $.each(resultDepartamento.objects, function (i, departamento) {

                    $("#ddlDepartamentos2").append('<option value="' + departamento.idDepartamento + '">' + departamento.descripcion + '</option>')
                })

            }
        },
        error: function (ex) {
            alert("Error al obtener departamentos")
        }
    })
}

// MODAL FORMULARIO
function abrirModal() {
    $("#formEmpleado")[0].reset();
    $('#exampleModal').modal('show');
}


// ========================== CRUD JS =================== \\

// ========================== GetAll =======================\\
$(document).ready(function () {
    //Mando los datos a mi controlador


    var empleado = {
        IdDepartamento: 0,
        Nombre: "",
        ApellidoPaterno: "",
        ApellidoMaterno: ""
    };


    $.ajax({
        url: urlActionGetAll,
        type: "GET",
        data: { empleado },
        success: function (data) {
            $.each(data.objects, function (index, value) {
                let imagen = value.imagen;
                $("#bodyCard").append(
                    `
                                            <div class="col-4" style="padding-bottom:30px">
                                <div class="brutalist-card">
                                    <div class="brutalist-card__header">
                                        <div class="brutalist-card__icon" style="color:white">
                                           ${value.idEmpleado}
                                        </div>
                                        <div class="brutalist-card__alert">${value.nombre} ${value.apellidoPaterno}</div>
                                    </div>
                                    <div class="brutalist-card__message">

                                        FechaNacimiento: ${value.fechaNacimiento}
                                        <hr/>
                                        Departamento: ${value.departamento.descripcion}
                                        <hr/>
                                        RFC:${value.rfc}
                                        <hr/>
                                        NSS:${value.nss}
                                        <hr/>
                                        CURP: ${value.curp}
                                        <hr/>
                                        FechaIngreso: ${value.fechaIngreso}
                                        <hr/>
                                        SalarioBase: $${value.salarioBase}
                                        <hr/>
                                        NoFaltas: ${value.noFaltas}
                                    </div>
                                    <div class="brutalist-card__actions">

                                        <button id="btnEditar"
                                            class="brutalist-card__button brutalist-card__button--mark">
                                            <i class="bi bi-pencil-square"></i>
                                        </button>

                                        <button id="btnDelete"
                                            class="brutalist-card__button brutalist-card__button--read">
                                            <i class="bi bi-trash3"></i>
                                        </button>



                                    </div>
                                </div>


                            </div>
                        `
                )
            });
        }
    })
});

// ========================== GetAllOpen ================== \\
$(document).ready(function () {
    $("#formBuscar").on("submit", function (e) {
        e.preventDefault();



        var Departamento = {
            IdDepartamento: $("#ddlDepartamentos").find(":selected").val(),
        };

        console.log($("#ddlDepartamentos").find(":selected").val());

        var empleado = {
            Nombre: $("#buscarNombre").val(),
            ApellidoPaterno: $("#buscarApellidoPaterno").val(),
            ApellidoMaterno: $("#buscarApellidoMaterno").val(),
            Departamento: Departamento
        };

        console.log(empleado)


        $.ajax({
            url: urlActionGetAllOpen,
            type: "POST",
            data: { empleado },
            success: function (data) {
                $("#bodyCard").empty();
                $.each(data.objects, function (index, value) {
                    let imagen = value.imagen;
                    $("#bodyCard").append(
                        `
                                            <div class="col-4">
                                <div class="brutalist-card">
                                    <div class="brutalist-card__header">
                                        <div class="brutalist-card__icon" style="color:white">
                                           ${value.idEmpleado}
                                        </div>
                                        <div class="brutalist-card__alert">${value.nombre} ${value.apellidoPaterno}</div>
                                    </div>
                                    <div class="brutalist-card__message">

                                        FechaNacimiento: ${value.fechaNacimiento}
                                        <hr />
                                        RFC:${value.rfc}
                                        <hr />
                                        NSS:${value.nss}
                                        <hr />
                                        CURP: ${value.curp}
                                        <hr />
                                        FechaIngreso: ${value.fechaIngreso}
                                        <hr />
                                        SalarioBase: ${value.salarioBase}
                                        <hr />
                                        NoFaltas: ${value.noFaltas}
                                    </div>
                                    <div class="brutalist-card__actions">


                                        <button id="btnEditar"
                                            class="brutalist-card__button brutalist-card__button--mark">
                                            <i class="bi bi-pencil-square"></i>
                                        </button>

                                        <button id="btnDelete"
                                            class="brutalist-card__button brutalist-card__button--read">
                                            <i class="bi bi-trash3"></i>
                                        </button>



                                    </div>
                                </div>


                            </div>
                        `
                    )
                });
            }
        })
    }
    )
});

// =========================== GET BY ID ================== \\

$(document).on('click', '#btnEditar', function () {

    FillDepartamentos()
    // Obtener Id

    var EmpleadoID = ($(this).parent().parent().find('div').find('div')[0].childNodes[0].data);
    console.log(EmpleadoID)

    // Realizar get by Id

    $.ajax({
        url: urlActionGetById,
        type: "GET",
        data: { EmpleadoID },
        success: function (resultGetById) {

            console.log(resultGetById.object)
            console.log(resultGetById.object.nombre)

            // Llenar modal con datos


            $("#idEmpleado").val(resultGetById.object.idEmpleado),
                $("#nombre").val(resultGetById.object.nombre),
                $("#apellidoPaterno").val(resultGetById.object.apellidoPaterno),
                $("#apellidoMaterno").val(resultGetById.object.apellidoMaterno),
                $("#DatePicker").val(resultGetById.object.fechaNacimiento),
                $("#DatePicker2").val(resultGetById.object.fechaIngreso),
                $("#rfc").val(resultGetById.object.rfc),
                $("#nss").val(resultGetById.object.nss),
                $("#curp").val(resultGetById.object.curp),
                $("#ddlDepartamentos2").val(resultGetById.object.departamento.idDepartamento)
            $("#salarioBase").val(resultGetById.object.salarioBase),
                $("#noFaltas").val(resultGetById.object.noFaltas)
            $('#exampleModal').modal('show');



        },
        error: function (resultGetById) {

        }
    })

});

// =========================== DELETE ======================= \\

$(document).on('click', '#btnDelete', function () {

    var IdEmpleado = ($(this).parent().parent().find('div').find('div')[0].childNodes[0].data);

    var TheCard = $(this).parent().parent();

    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: urlActionDelete,
                type: 'GET',
                data: { IdEmpleado },
                success: function (result) {

                    Swal.fire({
                        title: "Deleted!",
                        text: "Your file has been deleted.",
                        icon: "success"
                    });

                    TheCard.hide('slow');

                },
                error: function (result) {

                }
            });

        }
    });




});

// ========================= AGREGAR ====================== \\
$(document).ready(function () {
    $("#formEmpleado").on("submit", function (e) {
        e.preventDefault();


        var Departamento = {
            IdDepartamento: $("#ddlDepartamentos2").find(":selected").val(),
        };

        console.log($("#idEmpleado").val());

        var empleado = {
            IdEmpleado: $("#idEmpleado").val() || 0,
            Nombre: $("#nombre").val(),
            ApellidoPaterno: $("#apellidoPaterno").val(),
            ApellidoMaterno: $("#apellidoMaterno").val(),
            FechaNacimiento: $("#DatePicker").val(),
            RFC: $("#rfc").val(),
            NSS: $("#nss").val(),
            CURP: $("#curp").val(),
            Departamento: Departamento,
            SalarioBase: $("#salarioBase").val(),
            NoFaltas: $("#noFaltas").val()

        };

        var url = empleado.IdEmpleado == 0
            ? urlActionAdd
            : urlActionUpdate

        console.log(url);

        $.ajax({
            type: "POST",
            url: url,
            data: empleado,
            success: function (result) {
                console.log(result)
                if (result.correct) {
                    Swal.fire({
                        icon: 'success',
                        title: empleado.IdEmpleado == 0 ? '¡Agregado!' : '¡Actualizado!',
                        text: 'El empleado se registro correctamente.'
                    }).then(() => {
                        $('#exampleModal').modal('hide');
                        location.reload();
                    });
                    $("#formEmpleado")[0].reset();
                    $("#idEmpleado").val(0);
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: 'No se pudo guardar: ' + result.ErrorMessage
                    });
                }
            },
            error: function (xhr, status, error) {
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: 'Error inesperado: ' + error
                });
            }
        });
    })
}
);
