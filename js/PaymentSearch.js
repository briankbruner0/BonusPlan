$(document).ready(function () {
    PageInit();
});

// This functino is the one called on page load. It initalizes the page.
function PageInit() {
    // This is used for when you hover over a note. Updates the design without removing this functionality.
    $('[data-toggle="popover"]').popover();

    // Initalize tablesorter
    $("#PaymentSearchGrid").tablesorter({
        debug: false,
        cssHeader: "headerSort",
        cssDesc: "headerSortDown",
        cssAsc: "headerSortUp",
        headers: {
            11: { sorter: false }
        }
    });

    // Call change event for the Country dropdown
    $("#Country").change(function () {
        CommunityByCountryGet();
    });

    // Initialize the datepicker on all date fields.
    $('.date').datepicker({
        clearBtn: true,
        autoclose: true,
        todayHighlight: true,
    });

    // Initalize the fileter for the results table.
    $.tablefilter({
        inputElement: "#PaymentSearchFilter",
        tableElement: "#PaymentSearchGrid"
    });

    // Gets the information for the payment on click of the modal edit button.
    $('.modalButton').click(function () {
        PaymentDetailModalGet($(this).data('id'));
    });

    // Updates the payment information on the submit click for the modal.
    $('#PaymentEditSubmit').click(function () {
        PaymentDetailModalUpdate();
    });
}

// Validates the search requirments to make sure that they are correct.
function ValidatePage() {
    var error = "";

    //Regular expression for date check
    var dateRegEx = /^(0?[1-9]|1[012])[\/\-](0?[1-9]|[12][0-9]|3[01])[\/\-](19|20)[0-9][0-9]$/;

    var paymentDateFrom = $("#PaymentFromDT").val();
    var paymentDateTo = $("#PaymentToDT").val();
    var payrollDateFrom = $("#PayrollFromDT").val();
    var payrollDateTo = $("#PayrollToDT").val();
    var moveInDateFrom = $("#MoveInFromDT").val();
    var moveInDateTo = $("#MoveInToDT").val();

    // 02/08/2018 Fritz Kern - Changed to be using XOR so we can make sure only one of these is true. 
    // ex: 1 ^ 0 ^ 1 = FALSE since there are 2 1's. 0 ^ 1 ^ 0 = TRUE since there is only a single 1.
    if (((paymentDateFrom.length > 0 || paymentDateTo.length > 0) ^ (payrollDateFrom.length > 0 || payrollDateTo.length > 0) ^ (moveInDateFrom.length > 0 || moveInDateTo.length > 0)) == 0) {
        error += 'You cannot search on a Payment Date range, a Payroll Date range, and a Move In Date range simultaneously.\n';
        $("#PaymentFromDT").css('backgroundColor', 'Pink');
        $("#PaymentToDT").css('backgroundColor', 'Pink');
        $("#PayrollFromDT").css('backgroundColor', 'Pink');
        $("#PayrollToDT").css('backgroundColor', 'Pink');
        $("#MoveInFromDT").css('backgroundColor', 'Pink');
        $("#MoveInToDT").css('backgroundColor', 'Pink');
    }
    else if ((paymentDateFrom.length == 0 && paymentDateTo.length == 0) && (payrollDateFrom.length == 0 && payrollDateTo.length == 0) && (moveInDateFrom.length == 0 && moveInDateTo.length == 0)) {
        error += 'A Payment Date range OR a Payroll Date range OR a Move In Date range is required.\n';
        $("#PaymentFromDT").css('backgroundColor', 'Pink');
        $("#PaymentToDT").css('backgroundColor', 'Pink');
        $("#PayrollFromDT").css('backgroundColor', 'Pink');
        $("#PayrollToDT").css('backgroundColor', 'Pink');
        $("#MoveInFromDT").css('backgroundColor', 'Pink');
        $("#MoveInToDT").css('backgroundColor', 'Pink');
    }
    else {

        // Payment Validation
        if (paymentDateFrom.length > 0) {
            //Validate Proper Date Format
            if (!dateRegEx.test(paymentDateFrom)) {
                error += 'Payment From Date Format Is Incorrect.\n';
                $("#PaymentFromDT").css('backgroundColor', 'Pink');
            }
            else {
                $("#PaymentFromDT").css('backgroundColor', 'White');
            }
        }
        else {
            if (paymentDateTo.length > 0) {
                error += 'Payment From Date is required.\n';
                $("#PaymentFromDT").css('backgroundColor', 'Pink');
            }
        }

        if (paymentDateTo.length > 0) {
            //Validate Proper Date Format
            if (!dateRegEx.test(paymentDateTo)) {
                error += 'Payment To Date Format Is Incorrect.\n';
                $("#PaymentToDT").css('backgroundColor', 'Pink');
            }
            else {
                $("#PaymentToDT").css('backgroundColor', 'White');
            }
        }
        else {
            if (paymentDateFrom.length > 0) {
                error += 'Payment To Date is required.\n';
                $("#PaymentToDT").css('backgroundColor', 'Pink');
            }
        }

        // Payroll validation
        if (payrollDateFrom.length > 0) {
            //Validate Proper Date Format
            if (!dateRegEx.test(payrollDateFrom)) {
                error += 'Payroll From Date Format Is Incorrect.\n';
                $("#PayrollFromDT").css('backgroundColor', 'Pink');
            }
            else {
                $("#PayrollFromDT").css('backgroundColor', 'White');
            }
        }
        else {
            if (payrollDateTo.length > 0) {
                error += 'Payroll From Date is required.\n';
                $("#PayrollFromDT").css('backgroundColor', 'Pink');
            }
        }

        if (payrollDateTo.length > 0) {
            //Validate Proper Date Format
            if (!dateRegEx.test(payrollDateTo)) {
                error += 'Payroll To Date To Format Is Incorrect.\n';
                $("#PayrollToDT").css('backgroundColor', 'Pink');
            }
            else {
                $("#PayrollToDT").css('backgroundColor', 'White');
            }
        }
        else {
            if (payrollDateFrom.length > 0) {
                error += 'Payroll To Date is required.\n';
                $("#PayrollToDT").css('backgroundColor', 'Pink');
            }
        }

        // Move in validation
        if (moveInDateFrom.length > 0) {
            //Validate Proper Date Format
            if (!dateRegEx.test(moveInDateFrom)) {
                error += 'Move In From Date Format Is Incorrect.\n';
                $("#MoveInFromDT").css('backgroundColor', 'Pink');
            }
            else {
                $("#MoveInFromDT").css('backgroundColor', 'White');
            }
        }
        else {
            if (moveInDateTo.length > 0) {
                error += 'Move In From Date is required.\n';
                $("#MoveInFromDT").css('backgroundColor', 'Pink');
            }
        }

        if (moveInDateTo.length > 0) {
            //Validate Proper Date Format
            if (!dateRegEx.test(moveInDateTo)) {
                error += 'Move In To Date To Format Is Incorrect.\n';
                $("#MoveInToDT").css('backgroundColor', 'Pink');
            }
            else {
                $("#MoveInToDT").css('backgroundColor', 'White');
            }
        }
        else {
            if (moveInDateFrom.length > 0) {
                error += 'Move In To Date is required.\n';
                $("#MoveInToDT").css('backgroundColor', 'Pink');
            }
        }

        //Validate Payment To Date is not the same as Payment From Date
        var fromDatePayment = Date.parse(paymentDateFrom);
        var toDatePayment = Date.parse(paymentDateTo);
        var fromDatePayroll = Date.parse(payrollDateFrom);
        var toDatePayroll = Date.parse(payrollDateTo);
        var fromDateMoveIn = Date.parse(moveInDateFrom);
        var toDateMoveIn = Date.parse(moveInDateTo);

        if (paymentDateFrom.length > 0 && paymentDateTo.length > 0) {
            // If the From date minus the To date is greater than zero means the From date is greater than the To date.
            if ((fromDatePayment - toDatePayment) > 0) {
                error += 'Payment To Date must be later than the Payment From Date.\n';
                $("#PaymentFromDT").css('backgroundColor', 'Pink');
                $("#PaymentToDT").css('backgroundColor', 'Pink');
            }

            //Validate that the To & From dates are not more than 6 months apart
            //First subtract the beginDT val from the endDT giving you the date diff in milliseconds
            //Then divide that result by (24*60*60*100) or the number of milliseconds in 1 day
            //If the result is > 180 days then flag the date values.
            if (((toDatePayment - fromDatePayment) / (24 * 60 * 60 * 1000)) > 180) {
                error += 'Payment To Date cannot be more than 6 months greater than the Payment From Date.\n';
                $("#PaymentFromDT").css('backgroundColor', 'Pink');
                $("#PaymentToDT").css('backgroundColor', 'Pink');
            }
        }

        if (payrollDateFrom.length > 0 && payrollDateTo.length > 0) {
            // If the From date minus the To date is greater than zero means the From date is greater than the To date.
            if ((fromDatePayroll > toDatePayroll) > 0) {
                error += 'Payroll To Date must be later than the Payroll From Date.\n';
                $("#PayrollFromDT").css('backgroundColor', 'Pink');
                $("#PayrollToDT").css('backgroundColor', 'Pink');
            }

            //Validate that the To & From dates are not more than 6 months apart
            //First subtract the beginDT val from the endDT giving you the date diff in milliseconds
            //Then divide that result by (24*60*60*100) or the number of milliseconds in 1 day
            //If the result is > 180 days then flag the date values.
            if (((toDatePayroll - fromDatePayroll) / (24 * 60 * 60 * 1000)) > 180) {
                error += 'Payroll To Date cannot be more than 6 months greater than the Payroll From Date.\n';
                $("#PayrollFromDT").css('backgroundColor', 'Pink');
                $("#PayrollToDT").css('backgroundColor', 'Pink');
            }
        }

        if (moveInDateFrom.length > 0 && moveInDateTo.length > 0) {
            // If the From date minus the To date is greater than zero means the From date is greater than the To date.
            if ((fromDateMoveIn > toDateMoveIn) > 0) {
                error += 'MoveIn To Date must be later than the MoveIn From Date.\n';
                $("#MoveInFromDT").css('backgroundColor', 'Pink');
                $("#MoveInToDT").css('backgroundColor', 'Pink');
            }

            //Validate that the To & From dates are not more than 6 months apart
            //First subtract the beginDT val from the endDT giving you the date diff in milliseconds
            //Then divide that result by (24*60*60*100) or the number of milliseconds in 1 day
            //If the result is > 180 days then flag the date values.
            if (((toDateMoveIn - fromDateMoveIn) / (24 * 60 * 60 * 1000)) > 180) {
                error += 'MoveIn To Date cannot be more than 6 months greater than the MoveIn From Date.\n';
                $("#MoveInFromDT").css('backgroundColor', 'Pink');
                $("#MoveInToDT").css('backgroundColor', 'Pink');
            }
        }

        var CommunityID = "";

        if ($("#uiAssignedCommunityNumber option").val() != undefined) {
            $("#uiAssignedCommunityNumber option").each(function () {
                CommunityID += $(this).val() + ",";
            });

            CommunityID = CommunityID.replace(/\,$/, '');
            $("#CommunityID").val(CommunityID);
        }
        else {
            error += 'At least one Community is Required.\n';
        }

        if ($("#EmployeeID").val().length > 0 && isNaN(parseInt($("#EmployeeID").val()))) {
            error += 'Employee ID must be a number.\n';
        }
    }

    if (error.length > 0) {
        alert(error);
        return false;
    }
    else {
        return true;
    }
}

// This AJAX call feeds the Community side by side box based on what is selected in the country drop down.
function CommunityByCountryGet() {
    var objCommunity = new Object();
    objCommunity.CountryCode = $("#Country").val();

    var ajaxMethodURL = location.protocol + '//' + location.host + '/Application/BonusPlan/AJAXData/BonusPlanAJAX.svc/CommunityByCountryGet';

    $.ajax({
        type: 'POST',
        url: ajaxMethodURL,
        cache: false,
        data: JSON.stringify(objCommunity),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (msg) {
            // If all communities have been selected,
            // auto populate the Assigned communities box with everything
            if ($("#uiUnAssignedCommunityNumber option").length == 0) {
                $("#uiAssignedCommunityNumber").html(msg);
                $("#uiUnAssignedCommunityNumber").html('');
            }
            else {
                $("#uiUnAssignedCommunityNumber").html(msg);

                // Remove any previously selected communities from the unassigned lsit
                $("#uiAssignedCommunityNumber option").each(function (i, community) {
                    $("#uiUnAssignedCommunityNumber option[value='" + $(community).val() + "']").remove();
                });
            }

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert('Error :' + XMLHttpRequest.responseText);
        }
    });
}

// This AJAX call feeds the payment information to the payment edit modal on load.
function PaymentDetailModalGet(id) {
    PaymentDeatilModalReset();
    
    var objPayment = new Object();
    objPayment.PaymentID = id;

    $.ajax({
        type: "POST",
        url: location.protocol + "//" + location.host + "/Application/BonusPlan/AJAXData/PaymentAjax.svc/PaymentDetailGet",
        cache: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(objPayment),
        async: true,
        beforeSend: function () {
            $('.overlay').show();
        },
        success: function (msg) {
            var data = $.parseJSON(msg)[0];
            
            $('#PaymentID').val(data.PaymentID);
            $('#PaymentAmount').val(data.Amount.toFixed(2));
            $('#PaymentProcessedDate').val(data.PaymentDt);

            $('.overlay').hide();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            setTimeout(function () {
                $('.overlay').hide();
                alert('Something went wrong when retrieving the data.');
            }, 1000);

            if (XMLHttpRequest.readyState < 4) {
                
            }
            else {
                console.log(XMLHttpRequest.responseText);
            }
        }
    });
}

// Calls the Ajax method that updates the payment.
function PaymentDetailModalUpdate() {
    if (ValidatePaymentUpdate()) {
        $('#PaymentAmount').val();
        $('#PaymentProcessedDate').val();

        var objPayment = new Object();
        objPayment.Username = $('#Username').val();
        objPayment.PaymentID = $('#PaymentID').val();
        objPayment.Amount = $('#PaymentAmount').val();
        objPayment.ProcessedDate = $('#PaymentProcessedDate').val();

        $.ajax({
            type: "POST",
            url: location.protocol + "//" + location.host + "/Application/BonusPlan/AJAXData/PaymentAjax.svc/PaymentDetailUpdate",
            cache: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(objPayment),
            async: true,
            beforeSend: function () {
                $('.GenerateMessage').html('Please wait while your data is being updated.');
                $('.overlay').show();
            },
            success: function (msg) {
                var rowsAffected = msg;

                if (msg == 0) {
                    alert('The Payment was not updated.');
                }

                $('#Submit').click();
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert('The Payment was not updated.');

                if (XMLHttpRequest.readyState < 4) {
                    $('#Submit').click();
                }
                else {
                    console.log(XMLHttpRequest.responseText);
                }
            }
        });
    }
}

// Validates the payment update information.
function ValidatePaymentUpdate() {
    var amount = $('#PaymentAmount').val();
    var paymentDate = $('#PaymentProcessedDate').val();
    var errors = '';

    var patt = /^(\-?)(\d+)(\.?\d*)$/gm;

    var valid = patt.test(amount);

    if (amount.length == 0) {
        errors += 'Payment Amount is required.\n';
    }

    if (!valid) {
        errors += 'The amount must only contain numbers, a negative, and a decimal point';
    }

    if (paymentDate.length == 0) {
        errors += 'Processed Date is required.\n';
    }
    
    if (errors.length > 0) {
        alert(errors);
        return false;
    }
    else {
        return true;
    }
}

// Resets the payment update modal.
function PaymentDeatilModalReset() {
    $('#PaymentAmount').val('');
    $('#PaymentProcessedDate').val('');
    $('#PaymentID').val('');
    $('#PaymentEditSubmit').prop('disabled', false);
}