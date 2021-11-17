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
                "type": "get",
                "datatype": "json",
            },
            aoColumns: [
                { mData: 'roleName' },
                { mData: 'moduleName' },
                {
                    mData: 'canCreate',
                    "render": function (data, type, row) {

                        if (row.canCreate == true) {
                            return '<div class="icheck-primary d-inline"><input checked type="checkbox" class="canCreate" '
                                + ' data-id="' + row.id + '" data-name="' + row.roleName + '"' + '" data-modulename="' + row.moduleName + '"'
                                + ' id = "canCreate' + row.id + '" > <label for="canCreate' + row.id + '"></label>';
                        } else {
                            return '<div class="icheck-primary d-inline"><input type="checkbox" class="canCreate"'
                                + ' data-id="' + row.id + '" data-name="' + row.roleName + '"' + '" data-modulename="' + row.moduleName + '"'
                                + ' id = "canCreate' + row.id + '" > <label for="canCreate' + row.id + '"></label>';
                        }
                    }
                },
                {
                    mData: 'canUpdate',
                    "render": function (data, type, row) {

                        if (row.canUpdate == true) {
                            return '<div class="icheck-primary d-inline"><input checked type="checkbox" class="canUpdate" '
                                + ' data-id="' + row.id + '" data-name="' + row.roleName + '"' + '" data-modulename="' + row.moduleName + '"'
                                + ' id = "canUpdate' + row.id + '" > <label for="canUpdate' + row.id + '"></label>';
                        } else {
                            return '<div class="icheck-primary d-inline"><input type="checkbox" class="canUpdate"'
                                + ' data-id="' + row.id + '" data-name="' + row.roleName + '"' + '" data-modulename="' + row.moduleName + '"'
                                + ' id = "canUpdate' + row.id + '" > <label for="canUpdate' + row.id + '"></label>';
                        }
                    }
                },
                {
                    mData: 'canView',
                    "render": function (data, type, row) {

                        if (row.canView == true) {
                            return '<div class="icheck-primary d-inline"><input checked type="checkbox" class="canView" '
                                + ' data-id="' + row.id + '" data-name="' + row.roleName + '"' + '" data-modulename="' + row.moduleName + '"'
                                + ' id = "canView' + row.id + '" > <label for="canView' + row.id + '"></label>';
                        } else {
                            return '<div class="icheck-primary d-inline"><input type="checkbox" class="canView"'
                                + ' data-id="' + row.id + '" data-name="' + row.roleName + '"' + '" data-modulename="' + row.moduleName + '"'
                                + ' id = "canView' + row.id + '" > <label for="canView' + row.id + '"></label>';
                        }
                    }
                },
                {
                    mData: 'canDelete',
                    "render": function (data, type, row) {

                        if (row.canDelete == true) {
                            return '<div class="icheck-primary d-inline"><input checked type="checkbox" class="canDelete" '
                                + ' data-id="' + row.id + '" data-name="' + row.roleName + '"' + '" data-modulename="' + row.moduleName + '"'
                                + ' id = "canDelete' + row.id + '" > <label for="canDelete' + row.id + '"></label>';
                        } else {
                            return '<div class="icheck-primary d-inline"><input type="checkbox" class="canDelete"'
                                + ' data-id="' + row.id + '" data-name="' + row.roleName + '"' + '" data-modulename="' + row.moduleName + '"'
                                + ' id = "canDelete' + row.id + '" > <label for="canDelete' + row.id + '"></label>';
                        }
                    }
                }
            ]
        });
    }


    $(document).on('change', '.canCreate', function () {

        var id = $(this).attr('data-id')
        var roleName = $(this).attr('data-name')
        var moduleName = $(this).attr('data-modulename')
        var status = $(this).is(":checked");
        var title = 'Grant Permission';
        var content = ' grant the create permission to ' + roleName + ' role for ' + moduleName + ' module';

        if (status == false) {

            title = 'Deny Permission';
            content = ' deny the create permission to ' + roleName + ' role for ' + moduleName + ' module';
        }

        operationType = 'Create'
        openInfoModal(title, content, id)
    });

    $(document).on('change', '.canUpdate', function () {

        var id = $(this).attr('data-id')
        var roleName = $(this).attr('data-name')
        var moduleName = $(this).attr('data-modulename')
        var status = $(this).is(":checked");
        var title = 'Grant Permission';
        var content = ' grant the update permission to ' + roleName + ' role for ' + moduleName + ' module';

        if (status == false) {

            title = 'Deny Permission';
            content = ' deny the update permission to ' + roleName + ' role for ' + moduleName + ' module';
        }

        operationType = 'Update'
        openInfoModal(title, content, id)
    });

    $(document).on('change', '.canView', function () {

        var id = $(this).attr('data-id')
        var roleName = $(this).attr('data-name')
        var moduleName = $(this).attr('data-modulename')
        var status = $(this).is(":checked");
        var title = 'Grant Permission';
        var content = ' grant the view permission to ' + roleName + ' role for ' + moduleName + ' module';

        if (status == false) {

            title = 'Deny Permission';
            content = ' deny the view permission to ' + roleName + ' role for ' + moduleName + ' module';
        }

        operationType = 'View'
        openInfoModal(title, content, id)
    })

    $(document).on('change', '.canDelete', function () {

        var id = $(this).attr('data-id')
        var roleName = $(this).attr('data-name')
        var moduleName = $(this).attr('data-modulename')
        var status = $(this).is(":checked");
        var title = 'Grant Permission';
        var content = ' grant the delete permission to ' + roleName + ' role for ' + moduleName + ' module';

        if (status == false) {

            title = 'Deny Permission';
            content = ' deny the delete permission to ' + roleName + ' role for ' + moduleName + ' module';
        }

        operationType = 'Delete'
        openInfoModal(title, content, id)
    })

    $('#info-modal').on('hidden.bs.modal', function () {
        refreshTable()
    });

    function refreshTable() {

        var oTable = $('#userRolePermissionList').DataTable();
        oTable.ajax.reload();

    }

    $('.btnInfoModalButton').on('click', function () {

        var id = $(this).attr('data-id')
        var userPermission = false;
        var url = '';

        if (operationType == 'Create') {
            url = '/userrolepermission/updatecreatepermission';
            userPermission = $('#canCreate' + id).is(':checked');
        }
        else if (operationType == 'View') {
            url = '/userrolepermission/updateviewpermission';
            userPermission = $('#canView' + id).is(':checked');
        }
        else if (operationType == 'Update') {
            url = '/userrolepermission/updateeditpermission';
            userPermission = $('#canUpdate' + id).is(':checked');
        }
        else if (operationType == 'Delete') {
            url = '/userrolepermission/updatedeletepermission';
            userPermission = $('#canDelete' + id).is(':checked');
        }

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