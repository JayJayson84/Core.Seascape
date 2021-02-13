const Alert = {
    elements: {
        alertId: '[role="alert"]'
    },

    showAlert(msg) {
        const alert = document.querySelector(this.elements.alertId);

        if (alert !== null) {
            alert.textContent = msg;
            alert.classList.remove("d-none");
        }
    },

    hideAlert() {
        const alert = document.getElementById(this.elements.alertId);

        if (alert !== null) {
            alert.textContent = "";
            alert.classList.add("d-none");
        }
    }
}

export default Alert