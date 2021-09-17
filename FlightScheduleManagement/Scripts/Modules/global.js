$("#left-menu").on('click', 'li', function () {
    // remove classname 'active' from all li who already has classname 'active'
    $("#left-menu li.active").removeClass("active");
    // adding classname 'active' to current click li 
    $(this).addClass("active");
});

function openCreateModal(title, ajaxURL) {

    $('#create-modal').modal('toggle')
    $('#create-modal-title').text(title)

    debugger

    $.ajax({
        url: ajaxURL,
        type: 'GET',
        success: function (data) {
            $('#create-modal-body').html(data)
        }

    });
}