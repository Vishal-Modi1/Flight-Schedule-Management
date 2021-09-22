$(function () {

    $('#userList').DataTable({
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
            "url": "/User/ListAsync",
            "type": "get",
            "datatype": "json"
        },
        aoColumns: [

            { mData: 'FirstName' },
            { mData: 'LastName' },
            { mData: 'Email' },
            { mData: 'UserRole' },
            {
                mData: 'IsInstructor',
                "render": function (data, type, row) {

                    if (row.IsInstructor == true) {
                        return 'Yes';
                    } else {
                        return 'No';
                    }
                }
            },
            {
                mData: 'IsActive',
                "render": function (data, type, row) {

                    if (row.IsActive == true) {
                        return '<div class="icheck-primary d-inline"><input checked type="checkbox" class="userStatus" '
                            + ' data-id="' + row.Id + '" data-name="' + row.FirstName + ' ' + row.LastName + '"'
                            + ' id = "userStatus' + row.Id + '" > <label for="userStatus' + row.Id +'"></label>';
                    } else {
                        return '<div class="icheck-primary d-inline"><input type="checkbox" class="userStatus"'
                            + ' data-id="' + row.Id + '" data-name="' + row.FirstName + ' ' + row.LastName + '"'
                            + ' id = "userStatus' + row.Id + '" > <label for="userStatus' + row.Id + '"></label>';
                    }
                }
            },
            {
                targets: 6,
                mData: null,
                "render": function (data, type, row) {

                    var editHtml = '<button type="button" class="btn btn-success  btn-sm btnedit" style="border-radius:50%" data-id="' + row.Id + '" ><i class="fas fa-pencil-alt"></i></button>';
                    var deleteHtml = '<button type="button" class="btn btn-danger btn-sm btndelete" style="border-radius:50%"'
                        + 'data-id="' + row.Id + '" data-name="' + row.FirstName + ' ' + row.LastName +'"><i class="far fa-trash-alt"></i></button>';

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

        openCreateModal('Create User', '/User/createasync')
    });

    $(document).on('click', '.btnedit', function () {

        var id = $(this).attr('data-id');
        openCreateModal('Edit User', 'user/editasync?id=' + id)
    });

    $(document).on('click', '.btndelete', function () {

        var id = $(this).attr('data-id');
        var name = $(this).attr('data-name');
        openDeleteModal('Delete User', name, id)
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
                refreshTable()
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

    $('.btnDeleteModalButton').on('click', function () {

        var id = $(this).attr('data-id')

        $.ajax({
            url: 'User/DeleteAsync?id=' + id,
            type : 'GET',
            success: function (data) {

                closeDeleteModal();
                toastr.success(data.Message)
                refreshTable()

            },
            error: function (error) {

                toastr.success(data.Message)
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
                toastr.success(data.Message)
                refreshTable()
            },
            error: function (error) {

                toastr.success(data.Message)
            }
        })
    })

    function refreshTable() {

        var oTable = $('#userList').DataTable();
        oTable.ajax.reload();
    }

});


