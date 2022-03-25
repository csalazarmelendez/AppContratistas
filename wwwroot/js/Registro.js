const { get } = require("jquery");

function verificarPasswords() {
    // Ontenemos los valores de los campos de contraseñas
    pass1 = document.getElementById('pass1');
    pass2 = document.getElementById('pass2');
    var error = document.getElementById("error");
    var ok = document.getElementById("ok");
    // Verificamos si las constraseñas no coinciden
    if (pass1.value != pass2.value) {

        // Si las constraseñas no coinciden mostramos un mensaje
        error.style.display = "block";
        if (ok.style.display !== "none") {
            ok.style.display = "none";
        }
    } else {

        error.style.display = "none";
        ok.style.display = "block";
    }

}

function operadorId() {
    document.getElementById("operador").disabled = false;
    var getData = document.getElementById("operador").value;
    var operador = getData.split(',');
    var id = operador[0];
    var nombre = operador[1];
    var numero = operador[2];
    document.getElementById("id").value = id;
    document.getElementById("nombre").value = nombre;
    document.getElementById("numero").value = numero;
    document.getElementById("numerodos").value = numero;
}

function nuevoOperador() {
    document.getElementById("operador").value = "";
    document.getElementById("numero").value = "";
    document.getElementById("numerodos").value = "";
    document.getElementById("operador").disabled = true;
    document.getElementById("numerodos").disabled = false;
}

function auxiliar() {
    var x = document.getElementById("nuevooperador").value;
    document.getElementById("nombre").value = x;
    var y = document.getElementById("numerodos").value;
    document.getElementById("numero").value = y;
}
