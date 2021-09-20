﻿$("#left-menu").on('click', 'li', function () {
    // remove classname 'active' from all li who already has classname 'active'
    $("#left-menu li.active").removeClass("active");
    // adding classname 'active' to current click li 
    $(this).addClass("active");
});

function openCreateModal(title, ajaxURL) {

    $('#create-modal').modal('toggle')
    $('#create-modal-title').text(title)

    $.ajax({
        url: ajaxURL,
        type: 'GET',
        success: function (data) {
            $('#create-modal-body').html(data)
        }

    });
}

$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name] !== undefined) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};

$(document).on('click', '.closeModal', function () {

    $('#create-modal').modal('toggle')
})