const { get } = require("jquery");

function verificarPasswords() {
    // Ontenemos los valores de los campos de contraseñas 
    pass1 = document.getElementById('pass1');
    pass2 = document.getElementById('pass2');

    // Verificamos si las constraseñas no coinciden 
    if (pass1.value != pass2.value) {

        // Si las constraseñas no coinciden mostramos un mensaje 
        document.getElementById("error").classList.add("mostrar");
        alert("Las contraseñas no coinciden");
        return false;
    } else {

        // Si las contraseñas coinciden ocultamos el mensaje de error
        document.getElementById("error").classList.remove("mostrar");

        // Mostramos un mensaje mencionando que las Contraseñas coinciden 
        document.getElementById("ok").classList.remove("ocultar");
        
        // Refrescamos la página (Simulación de envío del formulario) 
        alert("Se ha enviado la solicitud de registro");
        return true;
    }

}

function operadorId() {
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