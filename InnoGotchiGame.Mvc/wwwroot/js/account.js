$(document).ready(function () {
    $('#password-changing-button').click(function () {
        let oldPassword = $('#old-password')[0].value;
        let newPassword = $('#new-password')[0].value;
        let jwtToken = localStorage.getItem('jwtToken');

        $.ajax({
            url: `https://localhost:44336/api/Users/my-profile/new-password`,
            type: "PUT",
            headers: {
                'Content-Encoding': 'gzip',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${jwtToken}`
            },
            crossDomain: true,
            data: JSON.stringify({ OldPassword: oldPassword, NewPassword: newPassword }),
            dataType: "json",
            success: function (response) {
                if (resp.status)
                    $('#password-changing-modal-close-button').click();
                alert();
            },
            error: function (xhr, status) {
                alert("error");
            }
        });
    });
    $('#save-profile-button').click(function () {
        let firstName = $('#acc-name')[0].value;
        let lastName = $('#acc-surname')[0].value;
        let jwtToken = localStorage.getItem('jwtToken');

        $.ajax({
            url: `https://localhost:44336/api/Users/my-profile`,
            type: "PUT",
            headers: {
                'Content-Encoding': 'gzip',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${jwtToken}`
            },
            crossDomain: true,
            data: JSON.stringify({ firstName: firstName, lastName: lastName }),
            dataType: "json",
            success: function (response) {
                alert(resp.status);
            },
            error: function (xhr, status) {
                alert("error");
            }
        });
    });
});