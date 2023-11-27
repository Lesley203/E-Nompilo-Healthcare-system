
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
$(document).ready(function () {
    console.log("Its working");
    document.getElementById('Information').style.visibility = "hidden";
    

});
const Prntbtn = document.getElementById('btnPrint');
Prntbtn.addEventListener("click", myPrint);

function myPrint() {

    document.getElementById('Information').style.visibility = "visible";
    document.getElementById('sidebar').style.visibility = "hidden";
    document.getElementById('header').style.visibility = "hidden";
    Prntbtn.style.visibility = "hidden";
    window.print();
    Prntbtn.style.visibility = "visible";
    document.getElementById('Information').style.visibility = "hidden";
    document.getElementById('sidebar').style.visibility = "visible";
    document.getElementById('header').style.visibility = "visible";
}



