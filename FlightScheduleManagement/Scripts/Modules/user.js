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
    });

    $('#createUser').on('click', function () {

        openCreateModal('Create User', '/User/CreateAsync')
    });

    $(document).on('click', '#btnsubmit', function () {

        $.ajax({
            url: "/user/createasync",
            type: "POST",
            data: JSON.stringify($("#createuser").serializeObject()),
            contentType: 'application/json; charset=utf-8',
            dataType: "json",
            success: function (data) {
                if (data != null && data.Success) {
                    alert(data.Message);
                } else {
                    // DoSomethingElse()
                    alert(data.Message);
                }
            },
            error: function (data) {
                $('#create-modal-body').html(data.responseText)
            }

        });
    });

   });


