$(document).ready(function () {
    $('#reg-submit-button').click(function () {
        let firstName = $('#input-name')[0].value;
        let lastName = $('#input-surname')[0].value;
        let email = $('#input-email')[0].value;
        let password = $('#input-password')[0].value;

        fetch('https://localhost:44336/api/Users', {
            method: 'POST',
            mode: 'cors',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ firstName: firstName, lastName: lastName, email: email, password: password })
        })
        .then(response => response.json())
        .then((data) => {
            window.location.href = '/login';
        });
    });
});