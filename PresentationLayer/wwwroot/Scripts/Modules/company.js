$(function () {

    $('#companyList').DataTable({
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
            "url": "/Company/List",
            "type": "get",
            "datatype": "json",
        },
        aoColumns: [
            { mData: 'name' },
            { mData: 'address' },
            { mData: 'contactNo' },
            {
                targets: 1,
                mData: null,
                "render": function (data, type, row) {

                    var editHtml = '';
                    var deleteHtml = '';

                    if ($('#hdnEdit').val() == "true") {

                        editHtml = '<button type="button" class="btn btn-success  btn-sm btnedit" style="border-radius:50%" data-id="' + row.id + '" ><i class="fas fa-pencil-alt"></i></button>';
                    }

                    if ($('#hdnDelete').val() == "true") {
                        deleteHtml = '<button type="button" class="btn btn-danger btn-sm btndelete" style="border-radius:50%"'
                            + 'data-id="' + row.id + '" data-name="' + row.name + '"><i class="far fa-trash-alt"></i></button>';
                    }

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

    $('#createCompany').on('click', function () {

        openSmallModal('Create Company', '/Company/create', fnCallBackAfterLoad)
    });

    $(document).on('click', '.btnedit', function () {

        var id = $(this).attr('data-id');
        openSmallModal('Edit Company', '/company/edit?id=' + id, fnCallBackAfterLoad)
    });

    $(document).on('click', '.btndelete', function () {

        var id = $(this).attr('data-id');
        var name = $(this).attr('data-name');
        openDeleteModal('Delete Company', name, id)
    });

    function fnCallBackAfterLoad() {

        $('#companyForm').validate({
            rules: {
                Name: {
                    required: true
                },
                Address: {
                    required: true
                }
            },
            messages: {
                Name: {
                    required: "Please enter company"
                },
                Address: {
                    required: "Please enter address"
                }
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

        if (!$("#companyForm").valid()) {
            return false;
        }

        var data = $("#companyForm").serializeObject();

        disableForm('companyForm');
        startLoading();

        $.ajax({
            url: "/company/create",
            type: "POST",
            data: data,
            contentType: 'application/x-www-form-urlencoded',
            dataType: "json",
            success: function (data) {

                if (data.status == 200) {
                    toastr.success(data.message)
                    refreshTable()
                }
                else {
                    toastr.error(data.message)
                }

            },
            error: function (data) {

                toastr.error(data.message)

            },
            complete: function () {

                closeSmallModal();
                enableForm('companyForm')
                stopLoading();
            }
        });
    });

    function refreshTable() {

        var oTable = $('#companyList').DataTable();
        oTable.ajax.reload();
    }

    $('.btnDeleteModalButton').on('click', function () {

        var id = $(this).attr('data-id')

        $.ajax({
            url: '/company/Delete?id=' + id,
            type: 'GET',
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

})