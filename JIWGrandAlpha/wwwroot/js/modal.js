function openModal(parameters) {
    const id = parameters.id;
    const idClass = parameters.idClass;
    const idStudent = parameters.idStudent;
    const idObject = parameters.idObject;
    const idGroup = parameters.idGroup;
    const type = parameters.type;
    const url = parameters.url;
    const modalCell = $('#modalCell');
    const modalClass = $('#modalClass');
    if (id === undefined || url === undefined) {
        alert('Упссс.... что-то пошло не так');
        return;
    }
    switch (type) {
        case 'Cell':
            $.ajax({
                type: 'GET',
                url: url,
                data: { "id": id, "idClass": idClass, "idStudent": idStudent },
                success: function (response) {
                    modalCell.find(".modal-body").html(response);
                    modalCell.modal('show')
                },
                failure: function () {
                    modalCell.modal('hide')
                },
                error: function (response) {
                    alert(response.responseText);
                }
            });
            break;
        case 'Class':
            $.ajax({
                type: 'GET',
                url: url,
                data: { "id": id, "idGroup": idGroup, "idObject": idObject },
                success: function (response) {
                    modalClass.find(".modal-body").html(response);
                    modalClass.modal('show')
                },
                failure: function () {
                    modalClass.modal('hide')
                },
                error: function (response) {
                    alert(response.responseText);
                }
            });
            break;
        default:
    }
};

