function submitLoginForm() {
    let emailValue = document.getElementById('email-input').value;
    let passwordValue = document.getElementById('password-input').value;
    fetch('https://localhost:44336/api/Auth/sign-in', {
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
        document.cookie = `jwtToken=${data.jwtToken}; max-age=8000000; path=/`;
        localStorage.setItem('jwtToken', data.jwtToken);
        window.location.href = '/farms/overview';
    });
}