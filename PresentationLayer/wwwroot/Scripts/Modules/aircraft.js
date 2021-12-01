$(function () {

    var deleteURL = '';
    var isAircraftDeleted = true;

    LoadAircrafts()

    $(document).on('click', '#btnSearchAircraft', function () {

        $('#txtAircraftSearch').val('');
        $('#ddlStatus').val('true')
        LoadAircrafts();

    });

    $("#expandeProfile").hide();

    $(document).on('click', '#collapseExpandeProfilePanel', function () {
        collapseDefault();
    });

    $(document).on('input', '#txtAircraftSearch', function () {

        LoadAircrafts();
    });

    $(document).on('change', '#ddlStatus', function () {

        LoadAircrafts();
    });

    function LoadAircrafts() {
        startLoading();

        var data =
        {
            TailNo: $('#txtAircraftSearch').val(),
            IsActive: $('#ddlStatus').val()
        }

        $.ajax({
            url: "/aircraft/List",
            type: "POST",
            data: data,
            success: function (htmlResponse) {

                $('#aircraftList').html(htmlResponse)

            },
            error: function (data) {

                toastr.error('Error while loading aircrafts, Please try again later')

            },
            complete: function () {

                //  closeCreateModal();
                // enableForm('aircraftForm')
                stopLoading();
            }
        });
    }

    var Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000
    });

    $('#createAircraft').on('click', function () {
        openCreateModal('Create Aircraft', '/aircraft/create', ValidateAircraftForm)
    });

    $(document).on('click', '#updateAircraft', function () {
        var craftId = $(this).attr('data-craftid');
        openCreateModal('Update Aircraft', '/aircraft/updateAircraft?aircraftAsync=' + craftId, ValidateAircraftForm)
    });

    $(document).on('click', '#btnAddNewEquipment', function () {

        var aircraftId = $(this).attr('data-craftid');
        openCreateModal('Add new Equipment', '/aircraft/addupdateequipment?aircraftId=' + aircraftId, ValidateAircraftEquipmentForm)

    });

    $(document).on('click', '.btnEditAirCraftEquipment', function () {
        var id = $(this).attr('data-equipmentid');
        var aircraftId = $(this).attr('data-craftid');
        var actionbtn = $(this).attr('data-actionbtn');
        var modalTitle = actionbtn == "edit" ? 'Edit Equipment' : (actionbtn == "view" ? 'View Equipment' : 'Delete Equipment');
        openCreateModal(modalTitle, '/aircraft/addupdateequipment?id=' + id + "&aircraftId=" + aircraftId + "&actionbtn=" + actionbtn, ValidateAircraftEquipmentForm);

    });

    $(document).on('click', '.btnViewAirCraftEquipment', function () {

        var id = $(this).attr('data-equipmentid');

        var modalTitle = 'Equipment Details';
        openCreateModal(modalTitle, '/aircraft/ViewEquipmentDetails?id=' + id);

    });

    $(document).on('click', '.btnDeleteAirCraftEquipment', function () {

        var id = $(this).attr('data-equipmentid');
        var aircraftId = $(this).attr('data-craftid');
        var actionbtn = $(this).attr('data-actionbtn');

        openDeleteModal('Delete Equipment', 'test', id)
        deleteURL = '/aircraft/deleteequipment?airCraftEquipmentid=';

        isAircraftDeleted = false;

    });

    $(document).on('click', '.btndelete', function () {

        var id = $(this).attr('data-id');
        var name = $(this).attr('data-name');
        openDeleteModal('Delete User', name, id)
    });

    function ValidateAircraftForm() {

        var popupaircraftid = $(".popupaircraftid").val();

        if (parseInt(popupaircraftid) > 0) {
            var noOfEngine = $("#NoofEngines").val();
            $('#NoofEngines').val(noOfEngine).change();
            var noOfPropellers = $("#NoofPropellers").val();
            $('#NoofPropellers').val(noOfPropellers).change();
            var aircraftCategoryId = $("#AircraftCategoryId").val();
            $('#AircraftCategoryId').val(aircraftCategoryId).change();
        }

        $('#aircraftForm').validate({
            rules: {
                TailNo: {
                    required: true
                },
                AircraftMakeId: {
                    required: true
                },
                AircraftModelId: {
                    required: true
                },
                AircraftCategoryId: {
                    required: true
                },
                AircraftClassId:
                {
                    required: true
                },
                FlightSimulatorClassId:
                {
                    required: true
                }
            },
            messages: {
                TailNo: {
                    required: "Please enter tail no"
                },
                AircraftMakeId: {
                    required: "Please select aircraft make"
                },
                AircraftModelId: {
                    required: "Please select aircraft model"
                },
                AircraftCategoryId: {
                    required: "Please select aircraft category"
                },
                AircraftClassId: {
                    required: "Please select aircraft class"
                },
                FlightSimulatorClassId:
                {
                    required: "Please select simulator class"
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

        $('input[name="IsEngineshavePropellersSwitch"]').on('switchChange.bootstrapSwitch', function (event, state) {
            console.log(state); // true | false
            $("#NoofPropellers").val(0);

            if (state) {
                $('#divNoofPropellers').show();
            } else {
                $('#divNoofPropellers').hide();
            }
        });
    }

    function ValidateAircraftEquipmentForm() {

        $('#formAddUpdateEquipment').validate({
            rules: {

                airCraftEquipmentsVM_StatusId: {
                    required: true
                },
                airCraftEquipmentsVM_ClassificationId: {
                    required: true
                },
                airCraftEquipmentsVM_Item: {
                    required: true
                }
            },
            messages: {
                airCraftEquipmentsVM_StatusId: {
                    required: "Please select status"
                },
                airCraftEquipmentsVM_ClassificationId: {
                    required: "Please select classification"
                },
                airCraftEquipmentsVM_Item: {
                    required: "Please enter item name"
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

    $(document).on('change', '#AircraftClassId', function () {
        if ($("#AircraftClassId option:selected").text().toLowerCase().includes("single")) {
            $("#NoofEngines").val(1);
        }
    });

    $(document).on('click', '#btnNext', function () {

        if (!$("#aircraftForm").valid()) {
            if ($('#AircraftClassId-error').length > 0) {
                $('#AircraftClassId-error').css('display', 'inline-block')
            }

            return false;
        }
        else {
            var popupaircraftid = $(".popupaircraftid").val();
            if (parseInt(popupaircraftid) > 0) {
                var noOfEngine = $("#NoofEngines").val();
                var noOfPropellers = $("#NoofPropellers").val();
                setAirCreaftEquipmentTime(noOfEngine, noOfPropellers, true);
                stepper.next();
            } else {
                var isNameExist = IsNameExist();
            }
        }
    });

    function IsNameExist() {

        startLoading();

        var airCraftVM =
        {
            TailNo: $('#TailNo').val(),
            Id: $('#Id').val()
        }

        $.ajax({
            url: "/aircraft/isaircraftexist",
            type: "POST",
            data: airCraftVM,
            success: function (data) {

                if (data.status == 200) {
                    var noOfEngine = $("#NoofEngines").val();
                    var noOfPropellers = $("#NoofPropellers").val();
                    setAirCreaftEquipmentTime(noOfEngine, noOfPropellers);
                    stepper.next();
                }
                else {

                    toastr.error(data.message)

                }

            },
            error: function (data) {

                toastr.error('Error while loading model data')
            },
            complete: function () {

                stopLoading();
            }
        });

    }

    function setAirCreaftEquipmentTime(noOfEngine, noOfPropellers, isEdit = false) {

        startLoading();
        if (isEdit == false) {

            $.ajax({
                url: "/aircraft/aircraftequipmenttimeslistform?noOfEngines=" + noOfEngine + "&noOfPropellers=" + noOfPropellers,
                type: "GET",
                success: function (data) {

                    $('#div_aircraftEquipmentTimesListForm').html(data);

                },
                error: function (data) {

                    toastr.error('Error while loading model data')
                },
                complete: function () {

                    stopLoading();
                }
            });
        } else {

            $.ajax({
                url: "/aircraft/aircraftequipmenttimeslistform?noOfEngines=" + noOfEngine + "&noOfPropellers=" + noOfPropellers,
                type: "GET",
                success: function (data) {

                    $('#div_aircraftEquipmentTimesListForm').html(data);

                },
                error: function (data) {

                    toastr.error('Error while loading model data')
                },
                complete: function () {

                    stopLoading();
                }
            });
        }
    }

    $(document).on('click', '#btnsubmit', function () {

        RemoveValidation()

        if (!$("#aircraftForm").valid()) {

            if ($('#AircraftClassId-error').length > 0) {
                $('#AircraftClassId-error').css('display', 'inline-block')
            }

            return false;
        }

        $('#IsEngineshavePropellers').val($('#IsEngineshavePropellersSwitch').prop('checked'))
        $('#IsEnginesareTurbines').val($('#IsEnginesareTurbinesSwitch').prop('checked'))
        $('#IsIdentifyMeterMismatch').val($('#IsIdentifyMeterMismatchSwitch').prop('checked'))
        $('#TrackOilandFuel').val($('#TrackOilandFuelSwitch').prop('checked'))

        var data = $("#aircraftForm").serializeObject();

        disableForm('aircraftForm');
        startLoading();

        $.ajax({
            url: "/aircraft/create",
            type: "POST",
            data: data,
            contentType: 'application/x-www-form-urlencoded',
            dataType: "json",
            success: function (data) {

                if (data.status == 200) {

                    if ($('#File').val() != '') {
                        UploadImage(data.message);
                    }
                    else {

                        if (document.getElementById('IsEdit') == null) {
                            LoadAircrafts()
                            closeCreateModal();

                        } else {
                            //After Edit reload page data
                            RefreshAircraftEdit();
                            closeCreateModal();
                        }
                    }
                }
                else {
                    toastr.error(data.message)
                }

            },
            error: function (data) {

                toastr.error(data.message)

            },
            complete: function () {

                enableForm('aircraftForm')
                stopLoading();
            }
        });
    });

    $('.btnDeleteModalButton').on('click', function () {

        var id = $(this).attr('data-id')

        $.ajax({
            url: deleteURL + '' + id,
            type: 'GET',
            success: function (data) {

                toastr.success(data.message)

                if (isAircraftDeleted) {

                    LoadAircrafts();
                }
                else {

                    closeDeleteModal();
                    RefreshEquipmentListing();
                }

            },
            error: function (error) {

                toastr.error(error.responseText)
            }
        })
    })

    $(document).on('click', '#btnAddUpdateEquipmentSubmit', function () {

        if (!$("#formAddUpdateEquipment").valid()) {
            return false;
        }

        var data = $("#formAddUpdateEquipment").serializeArray();

        data.push
            (
            {
                name: "airCraftEquipmentsVM.Item",
                value: $('#airCraftEquipmentsVM_Item').val()
            },
            {
                name: "airCraftEquipmentsVM.StatusId",
                value: $('#airCraftEquipmentsVM_StatusId').val()
            },
            {
                name: "airCraftEquipmentsVM.ClassificationId",
                value: $('#airCraftEquipmentsVM_ClassificationId').val()
            })

        disableForm('formAddUpdateEquipment');
        startLoading();

        $.ajax({
            url: "/aircraft/addupdateequipment",
            type: "POST",
            data: data,
            contentType: 'application/x-www-form-urlencoded',
            dataType: "json",
            success: function (data) {

                if (data.status == 200) {

                    toastr.success(data.message)
                    closeCreateModal();
                    RefreshEquipmentListing();
                }
                else {
                    toastr.error(data.message)
                }

            },
            error: function (data) {

                toastr.error(data.message)

            },
            complete: function () {

                enableForm('formAddUpdateEquipment')
                stopLoading();
            }
        });
    })

    function RefreshEquipmentListing() {

        var oTable = $('#tbllistaircraftequipment').DataTable();
        oTable.ajax.reload();
    }

    $(document).on('click', '#btnAircraftMakeSubmit', function () {

        if (!$("#aircraftMakeForm").valid()) {
            return false;
        }

        var data = $("#aircraftMakeForm").serializeObject();

        disableForm('aircraftMakeForm');
        startLoading();

        $.ajax({
            url: "/aircraft/createmake",
            type: "POST",
            data: data,
            contentType: 'application/x-www-form-urlencoded',
            dataType: "json",
            success: function (data) {

                if (data.status == 200) {

                    RefreshAircraftMakeDropdown()
                    closeSmallModal();
                }
                else {
                    toastr.error(data.message)
                }

            },
            error: function (data) {

                toastr.error(data.message)


            },
            complete: function () {


                enableForm('aircraftMakeForm')
                stopLoading();
            }
        });
    });

    function RefreshAircraftMakeDropdown() {
        $.ajax({
            url: "/aircraft/listmake",
            type: "GET",
            success: function (data) {

                toastr.success('make added successfully')

                $('#divAirCraftMake').html(data);
            },
            error: function (data) {

                toastr.error('Error while loading make list')
            },
            complete: function () {

                closeSmallModal();
                enableForm('aircraftMakeForm')
                stopLoading();
            }
        });
    }

    function collapseDefault(showDefault) {
        $("#collapseProfile").hide();
        $("#expandeProfile").hide();
        var vProfilePanel = document.getElementById("profilePanel");
        var vDescPanel = document.getElementById("descPanel");
        if (vProfilePanel.classList.contains("show")) {
            vProfilePanel.classList.remove("col-md-3");
            vProfilePanel.classList.remove("col-12");
            vProfilePanel.classList.remove("show");
            vDescPanel.classList.remove("col-md-9");
            $("#collapseProfile").hide();
            $("#expandeProfile").show();
        } else {
            $("#collapseProfile").show();
            $("#expandeProfile").hide();
            vProfilePanel.classList.add("col-md-3");
            vProfilePanel.classList.add("col-12");
            vProfilePanel.classList.add("show");
            vDescPanel.classList.add("col-md-9");
        }
        if (showDefault) {
            $("#collapseProfile").show();
            $("#expandeProfile").hide();
            vProfilePanel.classList.add("col-md-3");
            vProfilePanel.classList.add("col-12");
            vProfilePanel.classList.add("show");
            vDescPanel.classList.add("col-md-9");
        }
    }

    function RefreshAircraftEdit() {

        var airCraftId = parseInt($("#AirCraftId").val());

        if (!isNaN(parseInt(airCraftId))) {

            airCraftId = parseInt(airCraftId);

            $.ajax({
                url: "/aircraft/editpartial/" + airCraftId,
                type: "GET",
                success: function (data) {

                    toastr.success('details updated successfully')

                    $('.air-craft-edit-container').html(data);
                    collapseDefault(true);
                },
                error: function (data) {

                    toastr.error('Error while loading updated data')

                },
                complete: function () {

                    stopLoading();
                }
            });
        }
    }

    $(document).on('change', '#AircraftMakeId', function () {

        if ($(this).find(":selected").text() == "+ Add New") {

            openSmallModal('Add Aircraft Make', '/aircraft/createmake', ValidateAircraftMakeForm)
        }
    });

    function ValidateAircraftMakeForm() {

        $('#aircraftMakeForm').validate({
            rules: {
                Name: {
                    required: true
                }
            },
            messages: {
                Name: {
                    required: "Please enter make"
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

    $(document).on('click', '#btnAircraftModelSubmit', function () {

        if (!$("#aircraftModelForm").valid()) {
            return false;
        }

        var data = $("#aircraftModelForm").serializeObject();

        disableForm('aircraftModelForm');
        startLoading();

        $.ajax({
            url: "/aircraft/createmodel",
            type: "POST",
            data: data,
            contentType: 'application/x-www-form-urlencoded',
            dataType: "json",
            success: function (data) {

                if (data.status == 200) {

                    RefreshAircraftModelDropdown()
                    closeSmallModal();
                }
                else {
                    toastr.error(data.message)
                }

            },
            error: function (data) {

                toastr.error(data.message)
            },
            complete: function () {
                enableForm('aircraftModelForm')
                stopLoading();
            }
        });
    });

    function RefreshAircraftModelDropdown() {
        $.ajax({
            url: "/aircraft/listmodel",
            type: "GET",
            success: function (data) {

                toastr.success('model added successfully')

                $('#divAirCraftModel').html(data);
            },
            error: function (data) {

                toastr.error('Error while loading model list')
            },
            complete: function () {

                closeSmallModal();
                enableForm('aircraftModelForm')
                stopLoading();
            }
        });
    }

    $(document).on('change', '#AircraftModelId', function () {

        if ($(this).find(":selected").text() == "+ Add New") {

            openSmallModal('Add Aircraft Model', '/aircraft/createmodel', ValidateAircraftModelForm)
        }
    });

    function ValidateAircraftModelForm() {

        $('#aircraftModelForm').validate({
            rules: {
                Name: {
                    required: true
                }
            },
            messages: {
                Name: {
                    required: "Please enter model"
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

    $(document).on('change', '#AircraftCategoryId', function () {
        if ($(this).find(":selected").text() == "Airplane") {

            $('#divairModule').css('display', 'block')
            $('#divairCraftClass').css('display', 'block')
            $('#divenginePropellers').css('display', 'block')
            $('#divNoofPropellers').css('display', 'block')
            $('#divengineTurbines').css('display', 'block')
        }
        else {

            $('#divairModule').css('display', 'none')
            $('#divairCraftClass').css('display', 'none');
            $("#AircraftClassId").val(0);
            $('#divenginePropellers').css('display', 'none')
            $("#IsEngineshavePropellers").val('');
            $('#divNoofPropellers').css('display', 'none')
            $("#NoofPropellers").val(0);
            $('#divengineTurbines').css('display', 'none')
            $("#IsEnginesareTurbines").val('');

            if ($(this).find(":selected").text() == "Helicopter/Rotorcraft") {

                $('#divengineTurbines').css('display', 'block')

            }
        }

        if ($(this).find(":selected").text() == "Flight Simulator") {
            $('#divairModule').css('display', 'block')
            $('#divairflightSimulatorClass').css('display', 'block')

        }
        else {
            $('#divairModule').css('display', 'none')
            $('#divairflightSimulatorClass').css('display', 'none')
            $("#FlightSimulatorClassId").val(0);
        }


        ManageNoofEngineDropdown()

    })

    $(document).on('change', '#AircraftClassId', function () {

        ManageNoofEngineDropdown()
    })

    $(document).on('click', '#airCraftImage', function () {

        $('#imageUploader').trigger('click');

    });

    $("#imageUploader").on('change', function () {

        alert()

    });

    function afterResetAircraftCategoryId() {
        $("#AircraftClassId").val(0);
        $("#FlightSimulatorClassId").val(0);
        $("#NoofEngines").val(0);
        $("#NoofPropellers").val(0);
    }

    function ManageNoofEngineDropdown() {

        var aircraftClass = $('#AircraftClassId').find(":selected").text();
        var aircraftCategory = $('#AircraftCategoryId').find(":selected").text();

        if (((aircraftClass == "Single Engine Land (ASEL)" || aircraftClass == "Single Engine Sea (ASES)"
            || $('#AircraftClassId').val() == '') && $('#AircraftClassId').is(":visible") == true)) {
            $('#divnoofEngines').css('display', 'none')

        }
        else {
            $('#divnoofEngines').css('display', 'block')
        }
    }

    function RemoveValidation() {

        //  $(this).rules('remove', 'required');
    }

    function UploadImage(message) {

        var files = $('#File').prop("files");
        var url = "/Aircraft/UploadFile";
        formData = new FormData();
        formData.append("file", files[0]);

        jQuery.ajax({
            type: 'POST',
            url: url,
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            success: function (data) {

                if (data.status == 200) {

                    toastr.success(message)
                    LoadAircrafts()
                    closeCreateModal();
                }
                else {
                    toastr.error(data.message)
                }
            },
            error: function () {

                toastr.success('Error while uploading image, please try again later')
            }
        });
    }

    function refreshTable() {

        var oTable = $('#aircraftList').DataTable();
        oTable.ajax.reload();
    }

    $(document).on('click', '#equipmentList', function () {

        LoadEquipmentList();
    });

    function LoadEquipmentList() {

        if ($.fn.dataTable.isDataTable('#tbllistaircraftequipment')) {

            // already table is rendered
            return;
        }

        $('#tbllistaircraftequipment').DataTable({
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
            "createdRow": function (row, data, dataIndex) {

                if (data.status == 'Installed') {
                    $(row).css("background-color", "#CEF9D0");
                }
                else if (data.status == 'Repaired') {
                    $(row).css("background-color", "#C8E6FA");
                }
                else {
                    $(row).css("background-color", "#F8D1D1");
                }
            },
            "ajax": {
                "url": "/Aircraft/ListAirCraftEquipment",
                "data": function (d) {
                    return $.extend({}, d, {
                        "aircraftid": $('#AirCraftId').val(),
                    })
                },
                "type": "get",
                "datatype": "json",
            },
            aoColumns: [
                {
                    mData: 'status',
                    "render": function (data, type, row) {

                        return row.status;
                    }
                },
                { mData: 'item' },
                { mData: 'classification' },
                { mData: 'make' },
                { mData: 'model' },
                {
                    targets: 6,
                    mData: null,
                    className: 'text-center',
                    "render": function (data, type, row) {

                        var viewHtml = '<button type="button" class="btn btn-primary  btn-sm btnViewAirCraftEquipment" style="border-radius:50%"'
                            + 'data-craftid="' + row.aircraftid + '" data-equipmentid="' + row.id + '" data-actionbtn="view"><i class="fas fa-eye"></i></button>';

                        var editHtml = '<button type="button" class="btn btn-success  btn-sm btnEditAirCraftEquipment" style="border-radius:50%" data-id="' + row.id
                            + 'data-craftid="' + row.aircraftid + '" data-equipmentid="' + row.id + '" data-actionbtn="edit" > <i class="fas fa-pencil-alt"></i></button> ';

                        var deleteHtml = '<button type="button" class="btn btn-danger btn-sm btnDeleteAirCraftEquipment" style="border-radius:50%"'
                            + 'data-craftid="' + row.aircraftid + '" data-equipmentid="' + row.id + '" data-actionbtn="delete"><i class="far fa-trash-alt"></i></button>';

                        return viewHtml + '&nbsp;&nbsp;&nbsp;' + editHtml + '&nbsp;&nbsp;&nbsp;' + deleteHtml;
                    }
                }
            ]
        });
    }
})
