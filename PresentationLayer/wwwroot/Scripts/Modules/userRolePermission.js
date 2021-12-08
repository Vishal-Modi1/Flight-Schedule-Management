$(function () {

    var operationType = '';

    LoadUserRolePermissions()

    function LoadUserRolePermissions() {
        $('#userRolePermissionList').DataTable({
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
                "url": "/UserRolePermission/List",
                "data": function (d) {
                    return $.extend({}, d, {
                        "roleid": $('#UserRoleId').val(),
                        "moduleid": $('#ModuleId').val(),
                        "companyid": $('#CompanyId').val()
                    })
                },
                "type": "get",
                "datatype": "json",
            },
            aoColumns: [
                { mData: 'roleName' },
                { mData: 'displayName' },
                { mData: 'permissionType' },
                {
                    mData: 'isAllowed',
                    "render": function (data, type, row) {

                        if (row.isAllowed == true) {
                            return '<div class="icheck-primary d-inline"><input checked type="checkbox" class="permission" data-permissiontpe="' + row.permissionType + '" '
                                + ' data-id="' + row.id + '" data-name="' + row.roleName + '"' + '" data-modulename="' + row.moduleName + '"'
                                + ' id = "' + row.permissionType + row.id + '" > <label for="' + row.permissionType + row.id + '"></label>';
                        } else {
                            return '<div class="icheck-primary d-inline"><input type="checkbox" class="permission" data-permissiontpe="' + row.permissionType + '" '
                                + ' data-id="' + row.id + '" data-name="' + row.roleName + '"' + '" data-modulename="' + row.moduleName + '"'
                                + ' id = "' + row.permissionType + row.id + '" > <label for="' + row.permissionType + row.id + '"></label>';
                        }
                    }
                }
            ]
        });
    }

    $(document).on('change', '#UserRoleId,#ModuleId,#CompanyId', function () {

        refreshTable();

    });

    $(document).on('change', '.permission', function () {

        var id = $(this).attr('data-id')
        var roleName = $(this).attr('data-name')
        var moduleName = $(this).attr('data-modulename')
        var status = $(this).is(":checked");
        var title = 'Grant Permission';
        operationType = $(this).attr('data-permissiontpe')

        var content = ' grant the ' + operationType + ' permission to ' + roleName + ' role for ' + moduleName + ' module';

        if (status == false) {

            title = 'Deny Permission';
            content = ' deny the ' + operationType + ' permission to ' + roleName + ' role for ' + moduleName + ' module';
        }

        openInfoModal(title, content, id)
    });


    $('#info-modal').on('hidden.bs.modal', function () {
        refreshTable()
    });

    function refreshTable() {

        operationType = '';
        var oTable = $('#userRolePermissionList').DataTable();
        oTable.ajax.reload();

    }

    $('.btnInfoModalButton').on('click', function () {

        var id = $(this).attr('data-id')
        var userPermission = $('#' + operationType + id).is(':checked');
        var url = '/userrolepermission/updatepermission';

        $.ajax({
            url: url + '?id=' + id + '&isAllow=' + userPermission,
            type: 'GET',
            success: function (data) {

                closeInfoModal();
                toastr.success(data.message)

            },
            error: function (error) {

                toastr.success(data.message)
            }
        })
    })
})