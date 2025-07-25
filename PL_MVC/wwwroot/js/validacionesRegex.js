
function validateLetters(input, event) {

    console.log(event)

    var nombreId = $(input).attr('id');
    var inputKey = event.key;
    var inputLength = $(`#${nombreId}`).val()
    console.log(inputLength)
    const regex = /^[a-zA-Z\s]+$/;
    var padre = $(input).closest("p");
    var span = padre.find('span');

    if (event.type == "keypress") {
        if (!regex.test(inputKey)) {
            console.log("No es valido")
            $(input).removeClass("border border-success")
            $(input).addClass("border border-danger")
            $(span).removeAttr("hidden")
            event.preventDefault();
        } else {
            $(input).removeClass("border border-danger")
            $(input).addClass("border border-success")
            $(span).attr("hidden", "hidden")

        }
    } else if (event.type == "blur") {
        if (!regex.test($(input).val())) {
            // false
            $(input).removeClass("border border-success")
            $(input).addClass("border border-danger")
            $(span).removeAttr("hidden")
        } else {
            // true
            $(input).removeClass("border border-danger")
            $(input).addClass("border border-success")
            $(span).attr("hidden", "hidden")
        }
    }



}

function validateNumber(input, event) {

    var nombreId = $(input).attr('id');
    console.log(nombreId)
    const regex = /^\d+$/
    var testTest = event.key;
    console.log(`#${nombreId}`)
    var idValue = $(`#${nombreId}`).val();

    if (event.type == "keypress") {
        if (!regex.test(testTest)) {
            console.log("No es valido")
            $(`#${nombreId}`).removeClass("border border-success")
            $(`#${nombreId}`).addClass("border border-danger")
            $("#invalidInput").show()
            $(`#invalidInput${nombreId}`).show()
            event.preventDefault()

        } else {
            $(`#${nombreId}`).removeClass("border border-danger")
            $(`#${nombreId}`).addClass("border border-success")
            $(`#invalidInput${nombreId}`).hide()

        }
    } else if (event.type == "blur") {

        if (!regex.test($(input).val())) {
            // false
            $(input).removeClass("border border-success")
            $(input).addClass("border border-danger")
            $(`#invalidInput${nombreId}`).show()
        } else {
            // true
            $(input).removeClass("border border-danger")
            $(input).addClass("border border-success")
            $(`#invalidInput${nombreId}`).hide()

        }
    }


}



function validateCURP() {
    var curp = $("#curp").val();
    console.log(curp)
    const regex = /^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$/

    if (!regex.test(curp)) {
        console.log("No es valido")
        $("#curp").removeClass("border border-success")
        $("#curp").addClass("border border-danger")
        $("#invalidInputCurp").show()

    } else {
        $("#curp").removeClass("border border-danger")
        $("#curp").addClass("border border-success")
        $("#invalidInputCurp").hide()
    }
}

function validateRFC() {
    var rfc = $("#rfc").val();
    console.log(rfc)
    const regex = /^([A-Z,Ñ,&]{3,4}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1])[A-Z|\d]{3})$/

    if (!regex.test(rfc)) {
        console.log("No es valido")
        $("#rfc").removeClass("border border-success")
        $("#rfc").addClass("border border-danger")
        $("#invalidInputCurp").show()

    } else {
        $("#rfc").removeClass("border border-danger")
        $("#rfc").addClass("border border-success")
        $("#invalidInputCurp").hide()
    }
}

// Example starter JavaScript for disabling form submissions if there are invalid fields
(function () {
    'use strict'

    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    var forms = document.querySelectorAll('.needs-validation')

    // Loop over them and prevent submission
    Array.prototype.slice.call(forms)
        .forEach(function (form) {
            form.addEventListener('submit', function (event) {
                if (!form.checkValidity()) {
                    event.preventDefault()
                    event.stopPropagation()
                }

                form.classList.add('was-validated')
            }, false)
        })
})()