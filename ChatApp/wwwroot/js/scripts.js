var passwordInput = document.getElementById('passwordInput');
var confirmPassword = document.getElementById('confirmPasswordInput');


document.addEventListener('DOMContentLoaded', (event) => {
    document.getElementById('Create_Account_Button').addEventListener('click', function () {
        if (passwordInput.value != confirmPassword.value) {
            alert("Passwords do not match");
            event.preventDefault();
        }
    })
})