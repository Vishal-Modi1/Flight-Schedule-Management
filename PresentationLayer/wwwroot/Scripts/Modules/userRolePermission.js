$(function () {

    var operationType = '';
    var isUpdateMultiplePermission = '';

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
            "lengthMenu": [[10, 25, 50, 100, 500], [10, 25, 50, 100, 500]],
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
                            return '<div class="icheck-primary d-inline"><input checked type="checkbox" class="permission" data-permissiontype="' + row.permissionType + '" '
                                + ' data-id="' + row.id + '" data-name="' + row.roleName + '"' + '" data-modulename="' + row.moduleName + '"'
                                + ' id = "' + row.permissionType + row.id + '" > <label for="' + row.permissionType + row.id + '"></label>';
                        } else {
                            return '<div class="icheck-primary d-inline"><input type="checkbox" class="permission" data-permissiontype="' + row.permissionType + '" '
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


    $(document).on('change', '#isAllowedAll', function () {

        isUpdateMultiplePermission = true;
        var status = $(this).is(":checked");
        var title = 'Grant All Permissions';

        if (!status)
        {
            title = 'Deny All Permissions';
        }

        $('.permission').each(function ()
        {
            $(this).prop('checked', status)

        });

        openInfoModal(title, title, 0)

    });

    $(document).on('change', '.permission', function () {

        var id = $(this).attr('data-id')
        var roleName = $(this).attr('data-name')
        var moduleName = $(this).attr('data-modulename')
        var status = $(this).is(":checked");
        var title = 'Grant Permission';
        operationType = $(this).attr('data-permissiontype')

        var content = ' grant the ' + operationType + ' permission to ' + roleName + ' role for ' + moduleName + ' module';

        if (!status) {

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

        var url = '/userrolepermission/updatepermission';

        if (!isUpdateMultiplePermission)
        {
            var id = $(this).attr('data-id')
            var userPermission = $('#' + operationType + id).is(':checked');

            UpdateSinglePermission(url, id, userPermission)
        }
        else
        {
            url = '/userrolepermission/updatemultiplepermissions';
            UpdateMultiplePermissions(url, $('#isAllowedAll').is(":checked"))
        }
        
    })

    function UpdateSinglePermission(url, id, userPermission) {

    
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

    }

    function UpdateMultiplePermissions(url, userPermission) {

        var permissionModel =
        {
            UserRoleId: $('#UserRoleId').val(),
            ModuleId: $('#ModuleId').val(),
            CompanyId: $('#CompanyId').val(),
            IsAllow: userPermission
        }

        $.ajax({
            url: url,
            type: 'POST',
            data: permissionModel,
            
            success: function (data) {

                closeInfoModal();
                toastr.success(data.message)

            },
            error: function (error) {

                toastr.success(data.message)
            }
        })

    }
})