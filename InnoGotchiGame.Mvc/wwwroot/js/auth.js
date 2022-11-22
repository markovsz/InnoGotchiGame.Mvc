function submitLoginForm() {
    let emailValue = document.getElementById('email-input').value;
    let passwordValue = document.getElementById('password-input').value;
    fetch('https://localhost:44336/api/Auth/signIn', {
        method: 'POST',
        mode: 'cors',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ email: emailValue, password: passwordValue })
    })
    .then(response => response.json())
    .then((data) => {
        localStorage.setItem('jwtToken', data.jwtToken);
    });
}