﻿$("#left-menu").on('click', 'li', function () {
    // remove classname 'active' from all li who already has classname 'active'
    $("#left-menu li.active").removeClass("active");
    // adding classname 'active' to current click li 
    $(this).addClass("active");
});

function openCreateModal(title, ajaxURL, fnCallBackAfterLoad) {

    $('#create-modal').modal('toggle')
    $('#create-modal-title').text(title)

    startLoading();

    $.ajax({
        url: ajaxURL,
        type: 'GET',
        success: function (data) {
            $('#create-modal-body').html(data)
            fnCallBackAfterLoad();
            //InjectClientsideValidation();

        },
        complete: function () {
            stopLoading();
        }

    });
}

function openSmallModal(title, ajaxURL, fnCallBackAfterLoad) {

    $('#create-small-modal').modal('toggle')
    $('#create-small-modal-title').text(title)

    startLoading();

    $.ajax({
        url: ajaxURL,
        type: 'GET',
        success: function (data) {

            $('#create-small-body').html(data)

            fnCallBackAfterLoad();
            //InjectClientsideValidation();

        },
        complete: function () {
            stopLoading();
        }
    });
}

function InjectClientsideValidation() {
    $("form").removeData("validator");
    $("form").removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse("form");
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

    closeCreateModal();
})

$(document).on('click', '.closeSmallModal', function () {

    closeSmallModal();

})

$(document).on('click', '.closeDeleteModal', function () {

    closeDeleteModal();
})

function closeCreateModal() {
    $('#create-modal').modal('toggle')
}

function closeSmallModal() {
    $('#create-small-modal').modal('toggle')
}

function closeDeleteModal() {

    $('#delete-modal').modal('toggle')
}

function openDeleteModal(title, content, id) {

    $('#delete-modal').modal('toggle')
    $('#delete-modal-title').text(title)

    $('#deleteItem').text(content);
    $('.btnDeleteModalButton').attr('data-id', id);
}

function closeInfoModal() {

    $('#info-modal').modal('toggle')
}

function openInfoModal(title, content, id) {

    debugger
    $('#info-modal').modal('toggle')
    $('#info-modal-title').text(title)

    $('#infoItem').text(content);
    $('.btnInfoModalButton').attr('data-id', id);
}

function enableForm(formId) {
    $("#" + formId + " :input").prop("disabled", false);
}

function disableForm(formId) {
    $("#" + formId + " :input").prop("disabled", true);
}

function startLoading() {
    $('#loader').css('display','block')
}

function stopLoading() {
    $('#loader').css('display', 'none')
}

$(document).ajaxError(function (event, request, settings) {
   // alert()
});

$("form").submit(function ()
{

    if ($(this).valid())
    {

        $(this).find(":submit").attr('disabled', 'disabled');
        $(this).find(":submit").html('<i class="fa fa-spinner fa-spin fa-lg"></i>')
    }

});

$('#btnChangePassword').on('click', function () {

    openSmallModal('Change Password', '/myaccount/changepassword')

    //var data = $("#changePassword").serializeObject();

    //disableForm('changePassword');
    //startLoading();

    //$.ajax({
    //    url: "/myaccount/changepassword",
    //    type: "POST",
    //    data: data,
    //    contentType: 'application/x-www-form-urlencoded',
    //    dataType: "json",
    //    success: function (data)
    //    {

    //        closeCreateModal();
    //        toastr.success(data.message)
    //        refreshTable()

    //    },
    //    error: function (data) {
            
    //    },
    //    complete: function ()
    //    {
    //        enableForm('createuser')
    //        stopLoading();
    //    }
    //});

});