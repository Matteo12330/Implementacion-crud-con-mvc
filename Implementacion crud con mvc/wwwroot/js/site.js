// Función para mostrar/ocultar contraseña
function togglePassword(id, button) {
    const input = document.getElementById(id);
    const isVisible = input.type === "text";
    input.type = isVisible ? "password" : "text";
    button.innerText = isVisible ? "❌" : "👁️";
}
