$(document).ready(function () {
    $('#new-farm-modal-submit-button').click(function () {
        let farmName = $('#new-farm-modal-name')[0].value;
        let jwtToken = localStorage.getItem('jwtToken');

        $.ajax({
            url: `https://localhost:44336/api/Farms`,
            method: 'POST',
            headers: {
                'Content-Encoding': 'gzip',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${jwtToken}`
            },
            crossDomain: true,
            data: JSON.stringify({ name: farmName }),
            dataType: "json",
            success: function (response) {
                alert("farm successfully created");
            },
            error: function (xhr, status) {
                var resp = JSON.parse(xhr.responseText);
                alert(resp.Message);
            }
        });
    });
});