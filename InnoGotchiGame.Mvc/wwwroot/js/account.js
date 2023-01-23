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
                var resp = JSON.parse(xhr.responseText);
                alert(resp.Message);
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
                alert("profile changes successfully saved");
            },
            error: function (xhr, status) {
                var resp = JSON.parse(xhr.responseText);
                alert(resp.Message);
            }
        });
    });
    $('#avatar-upload-button').click(function () {
        let jwtToken = localStorage.getItem('jwtToken');
        let file = $('#avatar-choosing-button').prop('files')[0];

        readFile(file, function (content) {
            sendData(content, jwtToken);
        });
    });
    function readFile(file, cb) {
        var content = "";
        var reader = new FileReader();

        reader.onload = function (e) {
            content = reader.result;
            cb(content);
        }

        reader.readAsDataURL(file);
    }

    function sendData(data, jwtToken) {
        console.log("Try to send the data");
        console.log(data);
        let fileBase64 = JSON.stringify(data).split(',')[1].replace(/.$/, '');
        console.log(fileBase64);
        $.ajax({
            url: `https://localhost:44336/api/Users/my-profile/avatar`,
            type: "POST",
            headers: {
                'Content-Encoding': 'gzip',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${jwtToken}`
            },
            crossDomain: true,
            data: JSON.stringify(fileBase64),
            dataType: "json",
            success: function (response) {
                alert("avatar successfully uploaded");
            },
            error: function (xhr, status, p3, p4) {
                var resp = JSON.parse(xhr.responseText);
                alert(resp.Message);
            }
        });
    }
});