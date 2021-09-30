$(function () {
    $('#userList').DataTable({
        "processing": true,
        "serverSide": true,
        "PageLength": 10,
        "paging": true,
        "lengthChange": true,
        "filter": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": true,
        "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
        "ajax": {
            "url": "/User/List",
            "type": "get",
            "datatype": "json",
        },
        aoColumns: [
            { mData: 'firstName' },
            { mData: 'lastName' },
            { mData: 'email' },
            { mData: 'userRole' },
            {
                mData: 'isInstructor',
                "render": function (data, type, row) {

                    if (row.isInstructor == true) {
                        return 'Yes';
                    } else {
                        return 'No';
                    }
                }
            },
            {
                mData: 'isActive',
                "render": function (data, type, row) {

                    if (row.isActive == true) {
                        return '<div class="icheck-primary d-inline"><input checked type="checkbox" class="userStatus" '
                            + ' data-id="' + row.id + '" data-name="' + row.firstName + ' ' + row.lastName + '"'
                            + ' id = "userStatus' + row.id + '" > <label for="userStatus' + row.id + '"></label>';
                    } else {
                        return '<div class="icheck-primary d-inline"><input type="checkbox" class="userStatus"'
                            + ' data-id="' + row.id + '" data-name="' + row.firstName + ' ' + row.lastName + '"'
                            + ' id = "userStatus' + row.id + '" > <label for="userStatus' + row.id + '"></label>';
                    }
                }
            },
            {
                targets: 6,
                mData: null,
                "render": function (data, type, row) {

                    var editHtml = '<button type="button" class="btn btn-success  btn-sm btnedit" style="border-radius:50%" data-id="' + row.id + '" ><i class="fas fa-pencil-alt"></i></button>';
                    var deleteHtml = '<button type="button" class="btn btn-danger btn-sm btndelete" style="border-radius:50%"'
                        + 'data-id="' + row.id + '" data-name="' + row.firstName + ' ' + row.lastName + '"><i class="far fa-trash-alt"></i></button>';

                    return editHtml + '&nbsp;&nbsp;&nbsp;' + deleteHtml;
                }
            }
        ]
    });

    var Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000
    });

    $('#createUser').on('click', function () {

        openCreateModal('Create User', '/User/create', fnCallBackAfterLoad)
    });

    $(document).on('click', '.btnedit', function () {

        var id = $(this).attr('data-id');
        openCreateModal('Edit User', 'user/edit?id=' + id)
    });

    $(document).on('click', '.btndelete', function () {

        var id = $(this).attr('data-id');
        var name = $(this).attr('data-name');
        openDeleteModal('Delete User', name, id)
    });

    function fnCallBackAfterLoad() {
        
        $('#createuser').validate({
            rules: {
                FirstName: {
                    required: true
                },
                LastName: {
                    required: true
                },
                RoleId: {
                    required: true
                },
                InstructorTypeId: {
                    required: true
                },
                Phone: {
                    maxlength: 10,
                    minlength : 10
                },
                Email: {
                    required: true,
                    email: true,
                    remote: {
                        type: 'post',
                        url: 'user/IsEmailExist?email=' + $('#Email').val(),
                        dataType: 'json'
                    }
                }
            },
            messages: {
                FirstName: {
                    required: "Please enter first name"
                },
                LastName: {
                    required: "Please enter last name",
                },
                RoleId: {
                    required: "Please select a role",
                },
                InstructorTypeId: {
                    required: "Please select an instructor",
                },
                Email: {
                    required: "Please enter a email address",
                    email: "Please enter a vaild email address"
                },
                Phone: {
                    maxlength: "Please enter vlaid phone no",
                    minlength: "Please enter vlaid phone no"
                },
            },

            errorElement: 'span',
            errorPlacement: function (error, element) {
                error.addClass('invalid-feedback');
                element.closest('.form-group').append(error);
            },
            highlight: function (element, errorClass, validClass) {

                $(element).addClass('is-invalid');
            },
            unhighlight: function (element, errorClass, validClass) {
                $(element).removeClass('is-invalid');
            }
        });
    }
 

    $(document).on('click', '#btnsubmit', function () {

        if (!$("#createuser").valid()) 
        {
            return false;
        }

      //  var isInstructor = $('#IsInstructorSwitch').prop('checked');
        $('#IsInstructor').val($('#IsInstructorSwitch').prop('checked'))
        $('#IsSendEmailInvite').val($('#IsSendEmailInvite').prop('checked'))
        
        var data = $("#createuser").serializeObject();

        disableForm('createuser');
        startLoading();

        alert()
        $.ajax({
            url: "/user/create",
            type: "POST",
            data: data,
            contentType: 'application/x-www-form-urlencoded',
            dataType: "json",
            success: function (data) {

                closeCreateModal();
                toastr.success(data.message)
                refreshTable()

            },
            error: function (data) {
                //$('#create-modal-body').html(data.responseText)

                //$('#IsInstructorSwitch').bootstrapSwitch('state', isInstructor);

                //if (isInstructor == true) {

                //    $('#instructorTypeDiv').css('display', 'block')
                //    $('#IsInstructor').val(true)
                //}

                //else {
                //    $('#instructorTypeDiv').css('display', 'none')
                //    $('#IsInstructor').val(false)
                //}
            },
            complete: function () {
                enableForm('createuser')
                stopLoading();
            }
        });
    });


    $(document).on('change', '.userStatus', function () {

        var id = $(this).attr('data-id')
        var name = $(this).attr('data-name')
        var status = $(this).is(":checked");
        var title = 'Activate user';
        var content = 'Activate ' + name;

        if (status == false) {

            title = 'Deactivate user';
            content = 'Deactivate ' + name;
        }

        openInfoModal(title, content, id)
    })

    function IsEmailExist() {

        var email = $('#Email').val()

        $.ajax({
            url: 'user/IsEmailExist?email=' + email,
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

    $('.btnDeleteModalButton').on('click', function () {

        var id = $(this).attr('data-id')

        $.ajax({
            url: 'User/Delete?id=' + id,
            type : 'GET',
            success: function (data) {

                closeDeleteModal();
                toastr.success(data.message)
                refreshTable()

            },
            error: function (error) {

                toastr.success(data.message)
            }
        })
    })

    $('.btnInfoModalButton').on('click', function () {

        var id = $(this).attr('data-id')
        var userStatus = $('#userStatus' + id).is(':checked');

        $.ajax({
            url: 'User/UpdateStatus?id=' + id + '&isActive=' + userStatus,
            type: 'GET',
            success: function (data) {

                closeInfoModal();
                toastr.success(data.message)
                refreshTable()
            },
            error: function (error) {

                toastr.success(data.message)
            }
        })
    })

    function refreshTable() {

        var oTable = $('#userList').DataTable();
        oTable.ajax.reload();
    }

});


