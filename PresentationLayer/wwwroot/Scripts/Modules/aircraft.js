$(function () {

   LoadAircrafts()

    //$('#aircraftList').DataTable({
    //    "processing": true,
    //    "serverSide": true,
    //    "PageLength": 10,
    //    "paging": true,
    //    "lengthChange": true,
    //    "filter": true,
    //    "searching": true,
    //    "ordering": true,
    //    "info": true,
    //    "autoWidth": false,
    //    "responsive": true,
    //    "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
    //    "ajax": {
    //        "url": "/Aircraft/List",
    //        "type": "get",
    //        "datatype": "json",
    //    },
    //    aoColumns: [
    //        { mData: 'name' },
    //        {
    //            targets: 1,
    //            mData: null,
    //            "render": function (data, type, row) {

    //                var editHtml = '<button type="button" class="btn btn-success  btn-sm btnedit" style="border-radius:50%" data-id="' + row.id + '" ><i class="fas fa-pencil-alt"></i></button>';
    //                var deleteHtml = '<button type="button" class="btn btn-danger btn-sm btndelete" style="border-radius:50%"'
    //                    + 'data-id="' + row.id + '" data-name="' + row.name + '"><i class="far fa-trash-alt"></i></button>';

    //                return editHtml + '&nbsp;&nbsp;&nbsp;' + deleteHtml;
    //            }
    //        }
    //    ]
    //});

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
        openCreateModal('Update Aircraft', '/aircraft/updateAircraft?aircraftAsync=' + craftId , ValidateAircraftForm)
    });

    $(document).on('click', '#btnAddNewEquipment', function () {
        var aircraftId = $(this).attr('data-craftid');
        openCreateModal('Add new Equipment', '/aircraft/addupdateequipment?aircraftId=' + aircraftId, ValidateAircraftForm)
    });

    $(document).on('click', '.btnEditAirCraftEquipment', function () {
        var id = $(this).attr('data-edird');
        var aircraftId = $(this).attr('data-craftid');
        var actionbtn = $(this).attr('data-actionbtn');
        var modalTitle = actionbtn == "edit" ? 'Edit Equipment' : (actionbtn == "view" ? 'View Equipment' : 'Delete Equipment');
        openCreateModal(modalTitle, '/aircraft/addupdateequipment?id=' + id + "&aircraftId=" + aircraftId + "&actionbtn=" + actionbtn);
    });
    $(document).on('click', '.btnDeleteAirCraftEquipment', function () {
        var id = $(this).attr('data-edird');
        var aircraftId = $(this).attr('data-craftid');
        var actionbtn = $(this).attr('data-actionbtn');
        const swalWithBootstrapButtons = Swal.mixin({
            customClass: {
                confirmButton: 'btn btn-primary mr-2',
                cancelButton: 'btn btn-danger'
            },
            buttonsStyling: false
        })
        if (id == 0) {
            return false;
        }
        swalWithBootstrapButtons.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Yes, delete it!',
            cancelButtonText: 'No, cancel!',
            reverseButtons: false
        }).then((result) => {
            if (result.isConfirmed) {
                //delete api 

                debugger
                $.ajax({
                    url: '/aircraft/deleteequipment?airCraftEquipmentid=' + id,
                    type: 'POST',
                    success: function (data) {
                        //toastr.success(data.message)
                        swalWithBootstrapButtons.fire(
                            'Deleted!',
                            data.message,//'Equipment has been deleted.',
                            'success'
                        )
                        RefreshEquipmentListing();
                        loadDatatables();
                    },
                    error: function (error) {

                        toastr.error(data.message)
                    }
                })
               
            }
        })
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
        } else {
            var popupaircraftid = $(".popupaircraftid").val();
            if (parseInt(popupaircraftid) > 0) {
                var noOfEngine = $("#NoofEngines").val();
                var noOfPropellers = $("#NoofPropellers").val();
                setAirCreaftEquipmentTime(noOfEngine, noOfPropellers,true);
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

                if (data.status == 200)
                {
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

    function setAirCreaftEquipmentTime(noOfEngine, noOfPropellers,isEdit = false) {

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
            url: '/aircraft/Delete?id=' + id,
            type: 'GET',
            success: function (data) {

                toastr.success(data.message)
                LoadAircrafts();

            },
            error: function (error) {

                toastr.error(data.message)
            }
        })
    })
    $(document).on('click', '#btnAddUpdateEquipmentSubmit', function () {  

        var data = $("#formAddUpdateEquipment").serializeObject();
        
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
        startLoading();
        var airCraftId = parseInt($("#AirCraftId").val());
        $.ajax({
            url: "/aircraft/AirCraftEquipmentListing?id=" + airCraftId,
            type: "GET",
            success: function (data) {
                $('#divAirCraftEquipmentListing').html(data);
            },
            error: function (data) {
                toastr.error('Error while loading model list')
            },
            complete: function () {
            stopLoading();
            }
        });
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

            openSmallModal('Add Aircraft Make', '/aircraft/createmake', ValidateAircraftMakeForm )
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

    $(document).on('click', '#airCraftImage', function(){

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
})
