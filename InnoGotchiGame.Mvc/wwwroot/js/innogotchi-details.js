$(document).ready(function () {
    $('.feeding-button').click(function () {
        let petId = this.dataset.petId;
        let jwtToken = localStorage.getItem('jwtToken');

        $.ajax({
            url: `https://localhost:44336/api/Pets/pet/${petId}/feed`,
            type: "POST",
            headers: {
                'Authorization': `Bearer ${jwtToken}`
            },
            crossDomain: true,
            data: JSON.stringify({}),
            dataType: "json",
            success: function (response) {
                var resp = response.content;
                $('hunger-lvl-property').value = resp;
                alert(resp);
            },
            error: function (xhr, status) {
                let resp = JSON.parse(xhr.responseText);
                alert(resp.Message);
            }
        });
    });
    $('.give-a-drink-button').click(function () {
        let petId = this.dataset.petId;
        let jwtToken = localStorage.getItem('jwtToken');

        $.ajax({
            url: `https://localhost:44336/api/Pets/pet/${petId}/give-drink`,
            type: "POST",
            headers: {
                'Authorization': `Bearer ${jwtToken}`
            },
            crossDomain: true,
            data: JSON.stringify({}),
            dataType: "json",
            success: function (response) {
                let resp = response.content;
                $('thirst-lvl-property').value = resp;
                alert(resp);
            },
            error: function (xhr, status) {
                let resp = JSON.parse(xhr.responseText);
                alert(resp.Message);
            }
        });
    });
});