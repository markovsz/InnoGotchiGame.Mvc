$(document).ready(function () {
    let jwtToken = localStorage.getItem('jwtToken');
    $('#sign-out-button').click(function signOut() {
        localStorage.removeItem('jwtToken');
        document.cookie = 'jwtToken=; Max-Age=0';
        window.location.href = '/login';
    });
    $.ajax({
        url: `https://localhost:44336/api/Users/my-profile/min`,
        type: "GET",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${jwtToken}`
        },
        crossDomain: true,
        dataType: "json",
        success: function (response) {
            $('#user-name-span')[0].textContent = response.firstName;
            $('#user-surname-span')[0].textContent = response.lastName;
            $('#avatar-pic').attr('src', 'https://localhost:44336/images/'  + response.pictureSrc);
        },
        error: function (xhr, status) {
            var resp = JSON.parse(xhr.responseText);
            alert(resp.Message);
        }
    });
});