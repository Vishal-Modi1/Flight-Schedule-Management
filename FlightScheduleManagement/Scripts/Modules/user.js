$(function () {

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


