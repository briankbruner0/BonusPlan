﻿var objPayment = new Object();
var PaymentModel = new Object();
var Audit = new Object();
PaymentModel.EmployeeID = "";
PaymentModel.CommunityNumber = "";
PaymentModel.Amount = "";
PaymentModel.BonusPlanID = "";
PaymentModel.Note = "";
PaymentModel.CustomerID = "";
Audit.CreateBy = "";
Audit.CreateDT = "";
Audit.ModifyBy = "";
Audit.ModifyDt = "";
Audit.UserTitle = "";
Audit.ActiveFlg = "";
Audit.ApplicationID = "";
PaymentModel.Audit = Audit;
objPayment.PaymentModel = PaymentModel;


function PaymentInsert() {
    objPayment.PaymentModel.EmployeeID = $('#SearchEmployeeHiddenID').val();
    objPayment.PaymentModel.CommunityNumber = $('#SearchCommunityNumberHiddenID').val();
    objPayment.PaymentModel.Amount = $('#BonusValue').val();
    objPayment.PaymentModel.BonusPlanID = $('#BonusPlanID').val();
    objPayment.PaymentModel.Note = $('#PaymentNote').val();
    objPayment.PaymentModel.CustomerID = $('#SearchResidentHiddenID').val();
    objPayment.PaymentModel.Audit.CreateBy = $('#Username').val();

    var url = location.protocol + "//" + location.host + '/application/BonusPlan/AJAXData/PaymentAjax.svc/PaymentInsert';

    jQuery.ajax({
        url: url,
        type: "POST",
        cache: false,
        data: JSON.stringify(objPayment),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $('#OutputModal .modal-title').html('Manual Payment Success');

            var html = '<div>The manual payment for ' + $('#Employee').html();
            html += ' for the amount of $' + objPayment.PaymentModel.Amount;
            html += ' for the resident ' + $('#Resident').html();
            html += ' has been successfully added.</div>';

            $('#OutputModalBody').html(html);

            $('#OutputModal').modal('show');

            PaymentReset();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert('Error :' + XMLHttpRequest.responseText);
        }
    });
}

function PaymentBonusPlanGet() {
    objPayment.PaymentModel.Audit.ActiveFlg = 1;

    var ajaxMethodURL = location.protocol + "//" + location.host + "/Application/BonusPlan/AJAXData/PaymentAjax.svc/PaymentBonusPlanGet";

    var result = {};

    //ajax call to web method which passes the object created above
    $.ajax({
        type: "POST",
        url: ajaxMethodURL,
        data: JSON.stringify(objPayment),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            tmpDataset = JSON.parse(msg);
            objDataset = tmpDataset.Table;

            var html = '<option value="0">Select One...</option>\n';

            $.map(objDataset, function (item) {
                html += '<option value="' + item.BonusPlanID + '">' + item.BonusPlan + '</option>\n';
            });

            $('#BonusPlanID').html(html);

            $('#loadingBonusPlan').html('');
        },
        error: function (response, status, errorMsg) {
            console.log(response.responseText, errorMsg);
        }
    });
}

function ValidatePage() {
    var error = "";

    $('#Message').html('');

    var $Value = $("#BonusValue");

    if ($("#BonusValue").val().length == 0) {
        error += "<div>-- Bonus Amount is required.</div>";
    }

    if ($("#BonusValue").val() < -5000) {
        error += "<div>-- Bonus Amount cannot exceed -5000.00.</div>";
    }

    if ($("#BonusValue").val() > 20000) {
        error += "<div>-- Bonus Amount cannot exceed 20000.00.</div>";
    }

    var re = /^[0-9]*$-/;
    if (isNaN($("#BonusValue").val())) {
        error += "<div>-- Bonus Amount must be numeric.</div>";
    }

    if ($("#PaymentNote").val().length == 0) {
        error += "<div>-- A Note is required.</div>";
    }
    if ($("#PaymentNote").val().length > 200) {
        error += "<div>-- You have reached the Max Length of 200 characters for a Note.\n";
    }

    if ($("#SearchResidentHiddenID").val() == 0 || $("#SearchCommunityNumberHiddenID").val() == 0) {
        error += "<div>-- A Resident is required.</div>";
    }

    if ($("#BonusPlanID").val() == 0) {
        error += "<div>-- A Bonus Plan is required.</div>";
    }

    if ($("#SearchEmployeeHiddenID").val().length == 0) {
        error += "<div>-- An Employee is required.</div>";
    }

    if (error.length > 0) {
        var html = '<div>Please review the following issues and correct them before submitting again. <br /><br /></div>';
        html += '<div>' + error + '</div>';

        $('#OutputModal .modal-title').html('Manual Payment');
        $('#OutputModalBody').html(html);

        $('#OutputModal').modal('show');

        return false;
    }
    else {
        return true;
    }
}

function PaymentReset() {
    objPayment.PaymentModel.EmployeeID = '';
    objPayment.PaymentModel.CommunityNumber = '';
    objPayment.PaymentModel.Amount = '';
    objPayment.PaymentModel.BonusPlanID = '';
    objPayment.PaymentModel.Note = '';
    objPayment.PaymentModel.CustomerID = '';
    objPayment.PaymentModel.Audit.CreateBy = '';

    $('#SearchResidentHiddenID').val('');
    $('#SearchCommunityNumberHiddenID').val('');
    $('#ResidentInfo').hide();
    $('#ResidentInfoSearch').show();

    $('#SearchEmployeeHiddenID').val('');
    $('#EmployeeInfo').hide();
    $('#EmployeeInfoSearch').show();
    
    $('#BonusValue').val('');
    $('#BonusPlanID').val(0);
    $('#PaymentNote').val('');
}