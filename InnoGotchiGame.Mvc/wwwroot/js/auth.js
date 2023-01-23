function submitLoginForm() {
    let emailValue = $('#email-input').val();
    let passwordValue = $('#password-input').val();
    $.ajax({
        url: `https://localhost:44336/api/Auth/sign-in`,
        type: "POST",
        headers: {
            'Content-Encoding': 'gzip',
            'Content-Type': 'application/json'
        },
        crossDomain: true,
        data: JSON.stringify({ email: emailValue, password: passwordValue }),
        dataType: "json",
        success: function (data) {
            document.cookie = `jwtToken=${data.jwtToken}; max-age=8000000; path=/`;
            localStorage.setItem('jwtToken', data.jwtToken);
            window.location.href = '/farms/overview';
        },
        error: function (xhr, status) {
            var resp = JSON.parse(xhr.responseText);
            alert(resp.Message);
        }
    });
}