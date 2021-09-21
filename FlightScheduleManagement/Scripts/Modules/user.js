$(function () {

    $(document).ready(function () {
        $('#example2').DataTable({
            "processing": true,
            "serverSide": true,
            "paging": true,
            "lengthChange": true,
            "searching": true,
            "ordering": true,
            "info": true,
            "autoWidth": false,
            "responsive": true,
            "ajax": {
                "url": "/User/GetDetailsAsync",
                "type": "get",
                "datatype": "json"
            },
            "columns": [
                {
                    "data": "FirstName",
                    "autoWidth": true,
                    "searchable": true
                },
                {
                    "data": "LastName",
                    "autoWidth": true,
                    "searchable": true
                },
                {
                    "data": "Email",
                    "autoWidth": true,
                    "searchable": true
                },
                {
                    "data": "UserRole",
                    "autoWidth": true,
                    "searchable": true
                }, {
                    "data": "IsInstructore",
                    "autoWidth": true,
                    "searchable": true
                }, {
                    "data": "IsActive",
                    "autoWidth": true,
                    "searchable": true
                },
            ]
        });
    var isEmailExist = false;

    $("#example1").DataTable({
        "responsive": true, "lengthChange": false, "autoWidth": false,
        // "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"]
    }).buttons().container().appendTo('#example1_wrapper .col-md-6:eq(0)');
    $('#example2').DataTable({
        "paging": true,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": true,
    });


    var Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000
    });

    $('#createUser').on('click', function () {

        openCreateModal('Create User', '/User/editasync?id=15')
    });

    $(document).on('click', '#btnsubmit', function () {

        var isInstructor = $('#IsInstructorSwitch').prop('checked');
        $('#IsInstructorSwitch').val($('#IsInstructorSwitch').prop('checked'))


        $.ajax({
            url: "/user/createasync",
            type: "POST",
            data: JSON.stringify($("#createuser").serializeObject()),
            contentType: 'application/json; charset=utf-8',
            dataType: "json",
            success: function (data) {

                closeCreateModal();
                toastr.success(data.Message)
            },
            error: function (data) {
                $('#create-modal-body').html(data.responseText)

                $('#IsInstructorSwitch').bootstrapSwitch('state', isInstructor);

                if (isInstructor == true) {

                    $('#instructorTypeDiv').css('display', 'block')
                    $('#IsInstructor').val(true)
                }

                else {
                    $('#instructorTypeDiv').css('display', 'none')
                    $('#IsInstructor').val(false)
                }
            }
        });
    });


    //$(document).on('blur', '#Email', function () {

    //    IsEmailExist();
    //})

    function IsEmailExist() {

        var email = $('#Email').val()

        $.ajax({
            url: 'user/IsEmailExistAsync?email=' + email,
            type: 'GET',
            success: function (data) {

                if (data == 'true') {
                    isEmailExist = true;
                }
                else {
                    isEmailExist = false;
                }
            }
        })
    }

});


