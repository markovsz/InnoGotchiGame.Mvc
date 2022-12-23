let bodyPartPictureSizes = null;
$(document).ready(function () {
    $('.feeding-button').click(function () {
        let petId = this.dataset.petId;
        let jwtToken = localStorage.getItem('jwtToken');

        //fetch(`https://localhost:44336/api/Pets/pet/${petId}/feed`, {
        //    header: {
        //        'Authorization': `Bearer ${jwtToken}`
        //    },
        //    mode: 'cors',
        //    method: 'POST',
        //    body: JSON.stringify({})
        //})
        //    .then(response => { console.log(response); return response.json(); })
        //.then((data) => {
        //    if (data.content == 'full') {
        //        this.classList.add('disabled');
        //    }
        //});

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
                $('#hunger-lvl-property').val(resp);
                alert(resp);
            },
            error: function (xhr, status) {
                let resp = JSON.parse(xhr.responseText);
                $('[data-toggle="tooltip"]').tooltip({ trigger: 'click' });
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
                $('#thirst-lvl-property').val(resp);
                alert(resp);
            },
            error: function (xhr, status) {
                let resp = JSON.parse(xhr.responseText);
                alert(resp.Message);
            }
        });
    });
    $('#invite-friend-modal-submit-button').click(function () {
        let farmId = $('#invite-friend-modal-farm-id').data('farm-id');
        let userId = $('#invite-friend-modal-user-id')[0].value;
        let jwtToken = localStorage.getItem('jwtToken');

        $.ajax({
            url: `https://localhost:44336/api/FarmFriends`,
            type: "POST",
            headers: {
                'Content-Encoding': 'gzip',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${jwtToken}`
            },
            crossDomain: true,
            data: JSON.stringify({ farmId: farmId, userId: userId }),
            dataType: "json",
            success: function (response) {
                let resp = JSON.parse(response)
                if (data.content == 'full') {
                    this.classList.add('disabled');
                }
                alert(resp.status);
            },
            error: function (xhr, status) {
                let resp = JSON.parse(xhr.responseText);
                alert(resp.Message);
            }
        });
    });
    $('#delete-friend-button').click(function () {
        let farmFriendId = $('#delete-friend-button').data('farm-friend-id');
        let jwtToken = localStorage.getItem('jwtToken');

        $.ajax({
            url: `https://localhost:44336/api/FarmFriends/farm-friend/${farmFriendId}`,
            type: "DELETE",
            headers: {
                'Content-Encoding': 'gzip',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${jwtToken}`
            },
            crossDomain: true,
            dataType: "json",
            success: function (response) {
                alert(resp.status);
            },
            error: function (xhr, status) {
                let resp = JSON.parse(xhr.responseText);
                alert(resp.Message);
            }
        });
    });
    $('#new-pet-modal-submit-button').click(function () {
        let normalPicEdgeSize = 134.0;
        let farmId = $('#new-pet-modal-farm-id').data('farmId');
        let jwtToken = localStorage.getItem('jwtToken');
        let petName = $('#new-pet-modal-name').val();

        let bodyPic = $(`#pet-body-pic`);
        let eyesPic = $(`#pet-eyes-pic`);
        let mouthPic = $(`#pet-mouth-pic`);
        let nosePic = $(`#pet-nose-pic`);

        let bodyPicName = $(`#${selectedBodyPicId}`).data('pic-name');
        let eyesPicName = $(`#${selectedEyesPicId}`).data('pic-name');
        let mouthPicName = $(`#${selectedMouthPicId}`).data('pic-name');
        let nosePicName = $(`#${selectedNosePicId}`).data('pic-name');

        let bodyPicWidth = bodyPartPictureSizes.get(`${selectedBodyPicId}`).width;
        let eyesPicWidth = bodyPartPictureSizes.get(`${selectedEyesPicId}`).width;
        let mouthPicWidth = bodyPartPictureSizes.get(`${selectedMouthPicId}`).width;
        let nosePicWidth = bodyPartPictureSizes.get(`${selectedNosePicId}`).width;

        $.ajax({
            url: `https://localhost:44336/api/Pets`,
            type: "POST",
            headers: {
                'Content-Encoding': 'gzip',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${jwtToken}`
            },
            crossDomain: true,
            data: JSON.stringify({
                name: petName,
                farmId: farmId,
                bodyPicName: bodyPicName,
                bodyPictureX: bodyPic.position().left,
                bodyPictureY: bodyPic.position().top,
                bodyPictureScale: bodyPic.width() / normalPicEdgeSize,
                eyesPicName: eyesPicName,
                eyesPictureX: eyesPic.position().left,
                eyesPictureY: eyesPic.position().top,
                eyesPictureScale: eyesPic.width() / normalPicEdgeSize,
                mouthPicName: mouthPicName,
                mouthPictureX: mouthPic.position().left,
                mouthPictureY: mouthPic.position().top,
                mouthPictureScale: mouthPic.width() / normalPicEdgeSize,
                nosePicName: nosePicName,
                nosePictureX: nosePic.position().left,
                nosePictureY: nosePic.position().top,
                nosePictureScale: nosePic.width() / normalPicEdgeSize
            }),
            dataType: "json",
            success: function (response) {
                var resp = JSON.parse(response)
                if (data.content == 'full') {
                    this.classList.add('disabled');
                }
                alert(resp.status);
            },
            error: function (xhr, status) {
                alert("error");
            }
        });
    });

    bodyPartPictureSizes = new Map();

    let selectedBodyPicId = null;
    let selectedBodyPicScale = null;
    let selectedEyesPicId = null;
    let selectedMouthPicId = null;
    let selectedNosePicId = null;

    let selectedBodyPartPicId = null;
    let selectedDraggablePicId = null;
    $('#pet-body-part-scale').on("input change", function () {
        let pic = $(`#${selectedDraggablePicId}`);
        let scale = this.value;
        pic.width(bodyPartPictureSizes.get(`${selectedBodyPartPicId}`).width * scale);
        pic.height(bodyPartPictureSizes.get(`${selectedBodyPartPicId}`).height * scale);
    });
    $('.pet-body-part').click(function () {
        let picName = this.dataset.picName;
        let destPicId = this.dataset.destPicId;

        let destPic = $(`${destPicId}`);
        destPic.attr("src", `https://localhost:44336/images/${picName}`);
        destPic.draggable();

        destPic[0].dataset.bodyPartId = `${this.id}`
        bodyPartPictureSizes.set(this.id, { width: destPic[0].width, height: destPic[0].height });
    });

    $('.pet-body').click(function () {
        selectedBodyPicId = this.id;
    });
    $('.pet-eyes').click(function () {
        selectedEyesPicId = this.id;
    });
    $('.pet-mouth').click(function () {
        selectedMouthPicId = this.id;
    });
    $('.pet-nose').click(function () {
        selectedNosePicId = this.id;
    });

    $('.draggable-pic').mousedown(function () {
        selectedBodyPartPicId = this.dataset.bodyPartId;
        selectedDraggablePicId = this.id;
    });
});