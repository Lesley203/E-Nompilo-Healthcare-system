 
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

document.addEventListener('DOMContentLoaded', function () {
    var hasFile = true;

    if (hasFile) {
        var modal = new bootstrap.Modal(document.getElementById("myModal"));
        modal.show();

        var closeButton = document.getElementById("close");
        closeButton.addEventListener("click", function () {
            modal.hide();
        })
    }
});



