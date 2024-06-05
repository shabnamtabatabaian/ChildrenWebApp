document.addEventListener('DOMContentLoaded', (event) => {
    var alertElement = document.getElementById('success-alert');
    if (alertElement) {
        setTimeout(function () {
            alertElement.style.display = 'none';
        }, 5000);
    }
});
