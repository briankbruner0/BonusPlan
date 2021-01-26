<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPayment.aspx.cs" Inherits="BonusPlan.AddAPayment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <!--#include file="include/hdrMain.html"-->
    <script src="../../../../js/jquery.autocomplete.min.js" type="text/javascript"></script>

    <link href="css/BonusPlan.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/PaymentAdd.js"></script>

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            PageInit();
        });

        function PageInit() {
            // Get all the inital data for the page.
            DataToFormBind();

            //on document ready initalize the autocomplete on the resolution team textbox
            initEmployeeSearchAutoComplete();
            initResidentSearchAutoComplete();
            initClickEvent();
        }

        function DataToFormBind() {
            PaymentBonusPlanGet();
        }

        function initEmployeeSearchAutoComplete() {
            $("#SearchEmployeeID").autocomplete({
                minChars: 4,
                noCache: true,
                lookup: function (query, done) {
                    var employeeSearch = new Object();
                    employeeSearch.ADPEmpID = query;

                    //web method url
                    var ajaxMethodURL = location.protocol + "//" + location.host + "/Application/BonusPlan/AJAXData/BonusPlanAJAX.svc/BonusPlanEmployeeSearch";

                    var result = {};

                    //ajax call to web method which passes the object created above
                    $.ajax({
                        type: "POST",
                        url: ajaxMethodURL,
                        data: JSON.stringify(employeeSearch),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            var suggestions = $.map(msg, function (item) {
                                var tmpValue = item.Employee.replace(/[\|&;\$%@'{}"<>\(\)\+,]/g, "");
                                return {
                                    value: tmpValue,
                                    data: '[{"JobTitle":"' + item.JobTitle + '","PhotoPath":"' + item.PhotoPath + '","EmployeeID":"' + item.EmployeeID + '","Community":"' + item.Community + '"}]'
                                };
                            });

                            result.suggestions = suggestions;
                            done(result);
                        },
                        error: function (response, status, errorMsg) {
                            console.log(response.responseText, errorMsg);
                        }
                    });
                },
                onSelect: function (suggestion) {
                    var objDataset = JSON.parse(suggestion.data);

                    $('#EmployeeInfo').show();
                    $('#EmployeeInfoSearch').hide();

                    $('#JobTitle').html(objDataset[0].JobTitle);
                    $('#Employee').html('<b>' + suggestion.value + '</b>');
                    $('#EmployeeCommunity').html(objDataset[0].Community);
                    $("#EmployeePhoto").attr('src', objDataset[0].PhotoPath);
                    
                    $('#SearchEmployeeHiddenID').val(objDataset[0].EmployeeID);
                    $("#SearchEmployeeID").val('');
                }
            });
        }

        function initResidentSearchAutoComplete() {
            var ResidentSearch;

            $("#ResidentSearch").autocomplete({
                minChars: 4,
                noCache: true,
                lookup: function (query, done) {
                    var residentSearch = new Object();
                    residentSearch.query = query;

                    //web method url
                    var ajaxMethodURL = location.protocol + "//" + location.host + "/Application/BonusPlan/AJAXData/PaymentAjax.svc/acCustomerSearch";

                    var result = {};

                    if (ResidentSearch && ResidentSearch.readyState != 4) {
                        ResidentSearch.abort();
                    }

                    //ajax call to web method which passes the object created above
                    ResidentSearch = $.ajax({
                        type: "POST",
                        url: ajaxMethodURL,
                        data: JSON.stringify(residentSearch),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            var objJSON = $.parseJSON(msg);
                            var objDataSet = objJSON.Table;

                            var suggestions = $.map(objDataSet, function (item) {
                                var tmpValue = item.Resident.replace(/[\|&;\$%@{}"<>\(\)\+]/g, "");
                                return {
                                    value: tmpValue + ' (' + item.CustomerID + '-' + item.BillingID + ')',
                                    data: '[{"CustomerID":"' + item.CustomerID + '","CommunityNumber":"' + item.CommunityNumber + '","BillingID":"' + item.BillingID + '","Resident":"' + item.Resident + '","Community":"' + item.Community + '","MoveInDT":"' + item.MoveInDT + '","MoveOutDT":"' + item.MoveOutDT + '"}]'
                                };
                            });

                            result.suggestions = suggestions;
                            done(result);
                        },
                        error: function (XMLHttpRequest, response, status, errorMsg) {
                            if (XMLHttpRequest.getAllResponseHeaders()) {
                                console.log(response.responseText, errorMsg);
                            }
                        }
                    });
                },
                onSelect: function (suggestion) {
                    if (ResidentSearch && ResidentSearch.readyState != 4) {
                        ResidentSearch.abort();
                    }
                    
                    var objDataset = JSON.parse(suggestion.data);

                    //console.log(objDataset[0].MoveInDT, objDataset[0].MoveOutDT);
                    var moveInDT = Date.parse(objDataset[0].MoveInDT);
                    var moveOutDT = '';

                    if (objDataset[0].MoveOutDT.length > 0 && objDataset[0].MoveOutDT != 'null') {
                        moveOutDT = Date.parse(objDataset[0].MoveOutDT);
                    }
                    else {
                        moveOutDT = 'Present';
                    }

                    //console.log(moveInDT, moveOutDT);

                    $('#ResidentInfo').show();
                    $('#ResidentInfoSearch').hide();

                    $('#Resident').html('<b>' + objDataset[0].Resident + '</b>');
                    $('#ResidentCommunity').html(objDataset[0].Community);
                    $('#CustomerID').html(objDataset[0].CustomerID + ' - ' + objDataset[0].BillingID);
                    $("#MoveInRange").html(moveInDT.toString('MM/dd/yyyy') + ' - ' + moveOutDT.toString('MM/dd/yyyy'));

                    $('#SearchResidentHiddenID').val(objDataset[0].CustomerID);
                    $('#SearchCommunityNumberHiddenID').val(objDataset[0].CommunityNumber);
                    $("#ResidentSearch").val('');
                }
            });
        }
        
        function initClickEvent() {
            $('#Submit').click(function () {
                if (ValidatePage()) {
                    PaymentInsert();
                }
                else {
                    return false;
                }
            });

            $('#Cancel').click(function () {
                location.reload();
            });

            $('#removeEmployee').click(function () {
                $('#SearchEmployeeHiddenID').val('');
                $('#EmployeeInfo').hide();
                $('#EmployeeInfoSearch').show();
            });

            $('#removeResident').click(function () {
                $('#SearchResidentHiddenID').val('');
                $('#SearchCommunityNumberHiddenID').val('');
                $('#ResidentInfo').hide();
                $('#ResidentInfoSearch').show();
            });
        }
    </script>

    <style>
        .reset {
            padding-right: 15px;
        }

        body {
            background-color: #f1efe8;
        }

        input[type='checkbox'] {
            cursor: pointer;
        }

        .fa-trash-o {
            cursor: pointer;
        }

        .datepicker-dropdown {
            z-index: 2000;
        }

        .ui-autocomplete {
            z-index: 2000;
        }

        .row {
            margin: 10px auto;
        }

        .Role {
            left: 0;
            width: 275px;
            margin: 3px 3px 3px 3px;
            display: inline-block;
            border-radius: 50px;
            border: 1px solid silver;
            /* float: left; */
        }

        .residentPhotoBirthdayDiv {
            border-radius: 50%;
            position: relative;
            height: 70px;
            width: 70px;
            overflow: hidden;
            float: left;
            margin: 5px 10px 5px 5px;
            background-color: silver;
        }
		
		.residentPhotoBirthdayDiv.horizontal img {
            position: absolute;
            left: 50%;
            width: auto;
            height: 80px;
            transform: translateX(-50%);
        }

        .residentPhotoDiv {
            border-radius: 50%;
            position: relative;
            height: 50px;
            width: 70px;
            overflow: hidden;
            float: left;
            margin: 5px 10px 5px 5px;
        }

        .RoleTitle {
            font-size: 12px;
            font-weight: bold;
            max-width: 100px;
            overflow: hidden;
            text-overflow: ellipsis;
            white-space: nowrap;
            padding: 7px 7px 0px 0px;
            text-align: left !important;
        }


        .RoleResource {
            font-size: 12px;
            overflow: hidden;
            text-overflow: ellipsis;
            white-space: nowrap;
        }

        #dynamicEmployeeSearch {
            width: 306px;
        }

        .autocomplete-suggestions {
            border: 1px solid #999;
            background: #FFF;
            overflow: auto;
        }

        .autocomplete-suggestion {
            padding: 2px 5px;
            white-space: nowrap;
            overflow: hidden;
        }

        .autocomplete-selected {
            background: #F0F0F0;
            cursor: pointer;
        }

        .autocomplete-suggestions strong {
            font-weight: normal;
            color: #3399FF;
        }

        .autocomplete-group {
            padding: 2px 5px;
        }

        .autocomplete-group strong {
            display: block;
            border-bottom: 1px solid #000;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="CommunityID" runat="server" />
        <asp:HiddenField ID="Username" runat="server" />

        <div class="navbar navbar-default navbar-static" role="navigation">
            <asp:Literal ID="SecurityMenu" runat="server" EnableViewState="false"></asp:Literal>
        </div>
        <div class="container" role="main">
            <div class="row">
                <asp:Literal ID="PageMessage" runat="server"></asp:Literal>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <h1>
                        <asp:Literal ID="SecurityTitle" runat="server" EnableViewState="false"></asp:Literal>
                    </h1>
                    <p>
                        <asp:Literal ID="SecurityBody" runat="server" EnableViewState="false"></asp:Literal>
                    </p>
                </div>
            </div>
        </div>

        <div class="container">
            <div class="row">
                <div class="col-md-7">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Add a Payment
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div id="Message"></div>
                                </div>
                            </div>

                            <div class="row">
                                <input type="hidden" id="SearchEmployeeHiddenID" />
                                <div class="col-md-3">
                                    <label class="required">Employee:</label>
                                </div>
                                <div class="col-md-7">
                                    <div id="EmployeeInfoSearch" class="input-group">
                                        <input type="text" id="SearchEmployeeID" class="form-control" placeholder="Find an Employee" />
                                        <span class="input-group-addon"><i class="fa fa-search"></i></span>
                                    </div>
                                    <div id="EmployeeInfo" style="display: none;">
                                        <div class="Role btn-group">
                                            <div class="residentPhotoBirthdayDiv horizontal">
                                                <img id="EmployeePhoto" src="" align="center" border="0" data-pin-nopin="true" />
                                            </div>
                                            <div class="reset pull-right">
                                                <button id="removeEmployee" type="button" class="close">
                                                    <span>×</span>
                                                </button>
                                            </div>
                                            <div id="Employee" class="RoleResource">
                                            </div>
                                            <div id="JobTitle" class="RoleResource">
                                            </div>
                                            <div id="EmployeeCommunity" class="RoleResource">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-3">
                                    <label class="required">Amount:</label>
                                </div>
                                <div class="col-md-7">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="fa fa-dollar"></i></span>
                                        <input type="text" id="BonusValue" class="form-control" placeholder="Amount of Payment" />
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <input type="hidden" id="SearchResidentHiddenID" />
                                <input type="hidden" id="SearchCommunityNumberHiddenID" />
                                <div class="col-md-3">
                                    <label class="required">Resident:</label>
                                </div>
                                <div class="col-md-7">
                                    <div id="ResidentInfoSearch" class="input-group">
                                        <input type="text" id="ResidentSearch" class="form-control" placeholder="Find a Resident" />
                                        <span class="input-group-addon"><i class="fa fa-search"></i></span>
                                    </div>
                                    <div id="ResidentInfo" style="display: none;">
                                        <div class="Role btn-group">
                                            <div class="residentPhotoDiv horizontal">
                                            </div>
                                            <div class="reset pull-right">
                                                <button id="removeResident" type="button" class="close">
                                                    <span>×</span>
                                                </button>
                                            </div>
                                            <div class="ResidentInfoContainer">
                                                <div id="Resident" class="RoleResource">
                                                </div>
                                                <div id="ResidentCommunity" class="RoleResource">
                                                </div>
                                                <div id="CustomerID" class="RoleResource">
                                                </div>
                                                <div id="MoveInRange" class="RoleResource">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            
                            <div class="row">
                                <div class="col-md-3">
                                    <label class="required">Bonus Plan:</label>
                                </div>
                                <div class="col-md-7">
                                    <select id="BonusPlanID" class="form-control"></select>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-3">
                                    <label class="required">Payment Note:</label>
                                </div>
                                <div class="col-md-9">
                                    <textarea id="PaymentNote" rows="3" cols="43" class="form-control"></textarea>
                                </div>
                            </div>

                            <br />

                            <div class="row">
                                <div class="col-md-3">
                                    &nbsp;
                                </div>
                                <div class="col-md-9">
                                    <input type="button" id="Submit" value="Submit" class="btn btn-primary" />
                                    <input type="button" id="Cancel" value="Cancel" class="btn btn-default" />
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!--Output Modal START-->
        <div id="OutputModal" class="modal fade" role="dialog" data-keyboard="false">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="padding: 10px 15px;">
                        <button type="button" id="OutputModalClose" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Manual Payment</h4>
                    </div>

                    <div class="modal-body">
                        <div id="OutputModalBody">
                        </div>
                    </div>

                    <div class="modal-footer">
                        <input type="button" id="OutputModalCancel" data-dismiss="modal" class="btn btn-default pull-right btn-sm" value="Ok" />
                    </div>
                </div>
            </div>
        </div>
        <!--Output Modal END-->

        <!--#include file="include/GoogleAnalytic.html"-->
    </form>
</body>
</html>