<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BonusPlanProfile.aspx.cs" Inherits="BonusPlan.BonusPlanProfile" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!--#include file="include/hdrMain.html"-->

    <%--Bonus Plan Detail Component--%>
    <script src="js/component/BonusPlanDetailPanel.js"></script>

    <%--Bonus Plan To Community Component--%>
    <script src="js/component/BonusPlanToCommunityPanel.js"></script>

    <%--Ledger Entry Component--%>
    <script src="js/component/BonusPlanLedgerEntryPanel.js"></script>

    <%--Job Code Component--%>
    <script src="js/component/BonusPlanJobCodePanel.js"></script>

    <%--Approval Component--%>
    <script src="js/component/BonusPlanApprovalPanel.js"></script>

    <style>
        .tab-pane {
            padding-top: 20px;
        }

        table > thead > tr > th.tableTitle {
            font-size: 14px;
        }

        .TableRowBorderTop > th {
            border-top: 2px solid rgb(221,221,211) !important;
        }

        #JobCodeTitle, #CommunityTitle, #RevenueAccountTitle {
            font-size: 14px;
            font-weight: 700;
            font-family: Verdana, Arial, Arial, Helvetica, sans-serif;
        }

        .form-group {
            margin-bottom: 5px;
        }

        .col-form-label {
            padding-right: 0px;
        }

        .ControlButtonRow {
            padding-bottom: 10px;
        }

        .AddUserButtonColumn {
            text-align: left;
        }

        .UserFormGroup {
            margin-bottom: 10px;
        }

        .btn-sm {
            margin-left: 5px;
        }

        .JobCodeInput {
            display: inline;
        }

        div.JobCodeInput {
            padding-right: 0px;
            padding-left: 0px;
        }

        #JobCodeTotalPercentageLabel {
            margin-top: 5px;
        }

        .JobCodeInputSeparator {
            text-align: right;
            width: 10%;
        }

        #JobCodeInsert {
            margin-top: 5px;
        }

        @media (max-width: 768px) {
            .col-xs-9 {
                max-width: 250px;
            }
        }
    </style>
    <script type="text/javascript">

        var CommunityList = null;  // List of communities available for drop down box selection
        var UserCommunityList = null; // List of user/community bonus configurations for this Bonus Plan.  Populated by BonusPlanToUserCommunityGet()

        $(document).ready(function ()
        {
            //INVOKE CALENDAR CONTROL FOR Start and End Dates
            $('.datepicker').datepicker({
                clearBtn: true,
                autoclose: true,
                todayHighlight: true,
            });

            $.tablefilter({
                inputElement: "#UserCommunityFilter",
                tableElement: "#UserCommunityGrid"
            });

            $.tablefilter({
                inputElement: "#RevenueAccountFilter",
                tableElement: "#RevenueAccountGrid"
            });

            // Set up Bonus Plan Detail
            var bonusPlanDetail = new BonusPlanDetailPanel("BonusPlanDetailTab");
            bonusPlanDetail.BonusPlanDetailGet($("#BonusPlanHiddenID").val());

            // Set up Bonus Plan To Community Panel
            var communityPanel = new BonusPlanToCommunityPanel("CommunityTab");
            communityPanel.BonusPlanToCommunityGet($("#BonusPlanHiddenID").val());

            // Set up Ledger Entry Panel
            var ledgerPanel = new BonusPlanLedgerEntryPanel("LedgerEntryTab");
            ledgerPanel.BonusPlanToRevenueDetailGet($("#BonusPlanHiddenID").val());

            // Set up Job Code Panel
            var jobCodePanel = new BonusPlanJobCodePanel("JobCodeTab");
            jobCodePanel.BonusPlanToJobCodeDetailGet($("#BonusPlanHiddenID").val());

            // Set up Job Code Panel
            var approvalPanel = new BonusPlanApprovalPanel("ApprovalTab");
            approvalPanel.BonusPlanToApprovalWorkflowGet($("#BonusPlanHiddenID").val());

            // Get Bonus Plan User to Community Grid
            BonusPlanToUserCommunityGet();

            $("#RevenueEntryTypeID").change(function ()
            {
                LedgerEntryTypeGet();
            });

            var url = function (request, response)
            {
                //object(s) to pass to web method
                var employeeSearch = new Object();
                employeeSearch.ADPEmpID = $("#" + this.element.attr("id")).val();

                //web method url
                var ajaxMethodURL = location.protocol + "//" + location.host + "/Application/BonusPlan/AJAXData/BonusPlanAJAX.svc/BonusPlanEmployeeSearch";

                //ajax call to web method which passes the object created above
                $.ajax({
                    type: "POST",
                    url: ajaxMethodURL,
                    data: JSON.stringify(employeeSearch),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg)
                    {
                        response($.map(msg, function (item)
                        {
                            return {
                                label: item.Employee,
                                key: item.EmployeeID
                            }
                        }))
                    } //,
                    //                        error: function (response, status, errorMsg) {
                    //                            alert(response.responseText);
                    //                        }
                });
            }

            //on document ready initalize the autocomplete on the resolution team textbox
            $("#EmployeeID").autocomplete({
                source: url,
                minLength: 4,
                delay: 0,
                select: function (event, ui)
                {
                    $("#EmployeeHiddenID").val(ui.item.key);
                },
                change: function (event, ui)
                { //clears data if no user was found
                    var data = $.data(this);
                    if (ui.item == null)
                    {
                        $("#EmployeeHiddenID").val('');
                    }
                    //if (data.autocomplete.selectedItem == undefined) {
                    //    $("#EmployeeHiddenID").val('');
                    //}
                }
            });

            $("#UserCommunityGrid").tablesorter({
                debug: false,
                cssHeader: "headerSort",
                cssDesc: "headerSortDown",
                cssAsc: "headerSortUp"
            });
        });

        function filterUsersTab() {
            if (!$('#UserCommunityFilter').val()) {
                var filterMonth = new Date().getMonth() + 1;
                var filterYear = new Date().getFullYear();
                if (filterMonth  == 0) {
                    filerMonth = 12;
                }
                if (filterMonth < 10) {
                    filterMonth = '0' + filterMonth
                }
                $('#UserCommunityFilter').val(filterMonth + '/01/' + filterYear);
                $('#UserCommunityFilter').trigger('keyup');
            }
        }

        // Gets the communities available to this user
        function CommunityUserAccessGet()
        {
            var objSecurity = new Object();
            objSecurity.Username = $("#Username").val();

            var ajaxMethodURL = location.protocol + '//' + location.host + '/Application/BonusPlan/AJAXData/BonusPlanAJAX.svc/CommunityUserAccessGet';

            return $.ajax({
                type: 'POST',
                url: ajaxMethodURL,
                cache: false,
                data: JSON.stringify(objSecurity),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (msg)
                {
                    try
                    {
                        CommunityList = JSON.parse(msg);
                    }
                    catch (err) { }
                }.bind(self),
                error: function (XMLHttpRequest, textStatus, errorThrown)
                {
                    alert('Error :' + XMLHttpRequest.responseText);
                }
            });
        }

        // Retrieves the list of user/community bonus configurations for this bonus plan
        function BonusPlanToUserCommunityGet()
        {
            var objBonusPlan = new Object();
            objBonusPlan.BonusPlanID = $("#BonusPlanHiddenID").val();

            var ajaxMethodURL = location.protocol + '//' + location.host + '/Application/BonusPlan/AJAXData/BonusPlanAJAX.svc/BonusPlanToUserCommunityGet';

            $.ajax({
                type: 'POST',
                url: ajaxMethodURL,
                cache: false,
                data: JSON.stringify(objBonusPlan),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (msg)
                {
                    SetUserCommunityList(JSON.parse(msg));
                    BonusPlanUserCommunityGridGet();
                },
                error: function (XMLHttpRequest, textStatus, errorThrown)
                {
                    alert('Error :' + XMLHttpRequest.responseText);
                }
            });
        }

        // Sets the Bonus Plan User Community tab table to the loading spinner.
        // This will be removed when the call to BonusPlanToUserCommunityGet() completes
        function BonusPlanUserCommunitySpinnerGet()
        {
            var SpinnerHTML = "<tr><td colspan='8'><img src='/images/progressSnapIn.gif' alt='Loading' />&nbsp;Please wait downloading...</td></tr>";

            $("#UserCommunityGrid>tbody").html(SpinnerHTML);
        }

        // Creates the bonus plan user to community grid based on what's in UserCommunityList
        function BonusPlanUserCommunityGridGet()
        {
            var TableHTML = "";

            UserCommunityList.forEach(function (user)
            {
                TableHTML += "<tr>";
                TableHTML += "  <td class='columnData hidden-xs'>" + user.EmployeeID + "</td>";
                TableHTML += "  <td class='columnData hidden-xs'>" + user.ADP_EmployeeID + "</td>";
                TableHTML += "  <td class='columnData'>" + user.UserName + "</td>";
                TableHTML += "  <td class='columnData'>" + user.Community + "</td>";
                TableHTML += "  <td class='columnData numericShort'>" + user.Percentage + "</td>";
                //TableHTML += "  <td class='columnData numericShort'>" + user.FlatRate + "</td>";
                TableHTML += "  <td class='columnData'>" + user.BeginDT + "</td>";
                TableHTML += "  <td class='columnData'>" + user.EndDT + "</td>";
                TableHTML += "  <td class='columnData style='text-align:center;'>" + (user.Rollforwardflg == "1" ? "<i class=\"fa fa-lg fa-check-square fa-fw\"></i>" : "<i class=\"fa fa-lg fa-square-o fa-fw\"></i>") + "</td>";
                TableHTML += "  <td><i class=\"fa fa-2x fa-trash-o\"  title=\"delete\" aria-hidden=\"true\" onClick=\"BonusPlanToUserCommunityUpdate('" + user.BonusPlanToUserCommunityID + "')\"></i></td>";
                TableHTML += "</tr>";
            });

            $("#UserCommunityGrid>tbody").html(TableHTML);
            $("#UserCommunityGrid").trigger('update');
        }

        // The update function (and corresponding stored procedure) are used to delete users
        function BonusPlanToUserCommunityUpdate(BonusPlanToUserCommunityID)
        {
            if (!confirm("Are you sure you want to delete this user?"))
            {
                return;
            }

            var objBonusPlan = new Object();
            objBonusPlan.BonusPlanID = $("#BonusPlanHiddenID").val();
            objBonusPlan.BonusPlanToUserCommunityID = BonusPlanToUserCommunityID

            var ajaxMethodURL = location.protocol + '//' + location.host + '/Application/BonusPlan/AJAXData/BonusPlanAJAX.svc/BonusPlanToUserCommunityUpdate';

            $.ajax({
                type: 'POST',
                url: ajaxMethodURL,
                cache: false,
                data: JSON.stringify(objBonusPlan),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                beforeSend: BonusPlanUserCommunitySpinnerGet(),
                success: function (msg)
                {
                    SetUserCommunityList(JSON.parse(msg));
                    BonusPlanUserCommunityGridGet();
                    if ($('#UserCommunityFilter').val()) {
                        $('#UserCommunityFilter').trigger('keyup');
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown)
                {
                    alert('Error :' + XMLHttpRequest.responseText);
                }
            });
        }

        // Validates input on the user to community tab
        function BonusPlanToUserCommunityValidate()
        {
            var error = "";

            if ($("#BeginDt").val().length == 0)
            {
                error += "A begin date is required.\n";
            }

            if ($("#EmployeeHiddenID").val() == "")
            {
                error += "An employee is required.\n";
            }

            if ($("#UserBonusPercentage").val().length == 0)
            {
                error += "A percentage is required.\n";
            }

            if (isNaN(parseInt($("#UserBonusPercentage").val())))
            {
                error += "The bonus percentage must be a number.\n";
            }

            if (parseInt($("#UserBonusPercentage").val()) < 0)
        {
                error += "The bonus percentage must be positive.\n";
            }

            if ($("#EndDt").val().length == 0 && $("#RollforwardFlg").is(":checked") == false)
            {
                error += "A Recurring flag or End Date is required.\n";
            }

            if ($("#EndDt").val().length > 0 && $("#RollforwardFlg").is(":checked") == true)
            {
                error +="A Recurring flag cannot be set when an End Date has a value.\n";
            }

            if (error.length > 0)
            {
                alert(error);
                return false;
            }
            else
            {
                AddUser();
            }
        }

        function AddUser()
            {
                var objUserToCommunity = new Object();
                objUserToCommunity.BonusPlanID = $("#BonusPlanHiddenID").val();
                objUserToCommunity.EmployeeID = $("#EmployeeHiddenID").val();
                objUserToCommunity.CommunityNumber = $("#CommunityNumber").val();
                objUserToCommunity.Percentage = $("#UserBonusPercentage").val();
                objUserToCommunity.BeginDt = $("#BeginDt").val();
                objUserToCommunity.EndDt = $("#EndDt").val();
                objUserToCommunity.RollforwardFlg = $("#RollforwardFlg").is(":checked") == false ? "0" : "1";
                objUserToCommunity.Username = "<%=objSecurity.Username %>";

                var ajaxMethodURL = location.protocol + '//' + location.host + '/Application/BonusPlan/AJAXData/BonusPlanAJAX.svc/BonusPlanToUserCommunityInsert';

                $.ajax({
                    type: 'POST',
                    url: ajaxMethodURL,
                    cache: false,
                    data: JSON.stringify(objUserToCommunity),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    beforeSend: BonusPlanUserCommunitySpinnerGet(),
                    success: function (msg)
                    {
                        SetUserCommunityList(JSON.parse(msg));
                        BonusPlanUserCommunityGridGet();
                        if ($('#UserCommunityFilter').val()) {
                            $('#UserCommunityFilter').trigger('keyup');
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown)
                    {
                        alert('Error :' + XMLHttpRequest.responseText);
                    }
                });
        }

        function SetUserCommunityList(msg) {
            UserCommunityList = null;
            UserCommunityList = msg;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" class="form-horizontal">
        <asp:HiddenField ID="BonusPlanHiddenID" runat="server" />
        <asp:HiddenField ID="Username" runat="server" />
        <asp:HiddenField ID="DeleteFlg" runat="server" />
        <div class="navbar navbar-default navbar-static navbar-fixed-top" role="navigation">
            <asp:Literal ID="SecurityMenu" runat="server" EnableViewState="false"></asp:Literal>
        </div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <div class="container">
            <div class="col-lg-1"></div>
            <div class="col-lg-10">

                <asp:Literal ID="PageMessage" runat="server" EnableViewState="false"></asp:Literal>
                <h1>
                    <asp:Literal ID="PageTitle" runat="server" EnableViewState="false"></asp:Literal></h1>
                <p>
                    <asp:Literal ID="PageBody" runat="server" EnableViewState="false"></asp:Literal>
                </p>
                <br />
                <ul class="nav nav-tabs" data-tabs="tabs">
                    <li class="active" role="presentation"><a role="tab" aria-controls="BonusPlanDetailTab" href="#BonusPlanDetailTab" data-toggle="tab">Bonus Plan</a></li>
                    <li role="presentation"><a role="tab" aria-controls="SystemTab" href="#SystemTab" data-toggle="tab">System Information</a></li>
                    <li role="presentation"><a role="tab" aria-controls="CommunityTab" href="#CommunityTab" data-toggle="tab">Community</a></li>
                    <li role="presentation"><a role="tab" aria-controls="LedgerEntryTab" href="#LedgerEntryTab" data-toggle="tab">Ledger Entry</a></li>
                    <li role="presentation"><a role="tab" aria-controls="JobCodeTab" href="#JobCodeTab" data-toggle="tab">Job Code</a></li>
                    <li role="presentation"><a role="tab" aria-controls="ApprovalTab" href="#ApprovalTab" data-toggle="tab">Approval</a></li>
                    <li role="presentation"><a role="tab" aria-controls="UserTab" href="#UserTab" data-toggle="tab" onclick="filterUsersTab()">User</a></li>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane active col-xs-12 col-sm-12 col-md-12 col-lg-12" id="BonusPlanDetailTab" role="tabpanel">
                    </div>
                    <div class="tab-pane col-xs-12 col-sm-12 col-md-12 col-lg-12" id="SystemTab" role="tabpanel">
                    </div>
                    <div class="tab-pane col-xs-12 col-sm-12 col-md-12 col-lg-12" id="CommunityTab" role="tabpanel">
                    </div>
                    <div class="tab-pane col-xs-12 col-sm-12 col-md-12 col-lg-12" id="LedgerEntryTab" role="tabpanel">
                    </div>
                    <div class="tab-pane col-xs-12 col-sm-12 col-md-12 col-lg-12" id="JobCodeTab" role="tabpanel">
                        <div class="col-xs-12 col-sm-12 col-md-8 col-lg-8">
                            <div class="form-group row">
                                <label class="col-xs-12 col-sm-3 col-md-3 col-lg-3 control-label col-form-label" for="JobCodeID">Job Code:</label>
                                <div class="col-xs-12 col-sm-8 col-md-8 col-lg-8">
                                    <asp:DropDownList ID="JobCodeID" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-xs-12 col-sm-3 col-md-3 col-lg-3 control-label col-form-label" for="JobCodeCommunity">Community:</label>
                                <div class="col-xs-12 col-sm-8 col-md-8 col-lg-8">
                                    <asp:DropDownList ID="JobCodeCommunity" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-xs-12 col-sm-3 col-md-3 col-lg-3 control-label col-form-label" for="JobCodeEffectiveDt">Effective Dt:</label>
                                <div class="col-xs-12 col-sm-8 col-md-8 col-lg-8">
                                    <asp:DropDownList ID="JobCodeEffectiveDt" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <div class="form-group row">
                                <div class="col-xs-10 col-sm-10 col-md-10 col-lg-10">
                                    <div class="row">
                                        <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4 JobCodeInput">
                                            <label class="col-xs-12 col-sm-12 col-md-12 col-lg-12 col-form-label" for="JobCodeCommissionBase">Base:</label>
                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                <asp:TextBox ID="JobCodeCommissionBase" runat="server" onchange="JobCodeTotalPercentageGet();" CssClass="form-control JobCodeInput" Width="80%" MaxLength="6" EnableViewState="false"></asp:TextBox>
                                                <label class="JobCodeInputSeparator">X</label>
                                            </div>
                                        </div>
                                        <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4 JobCodeInput">
                                            <label class="col-xs-12 col-sm-12 col-md-12 col-lg-12 col-form-label" for="JobCodeMultiplier">Multiplier:</label>
                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                <asp:TextBox ID="JobCodeMultiplier" runat="server" onchange="JobCodeTotalPercentageGet();" CssClass="form-control JobCodeInput" Width="80%" MaxLength="6" EnableViewState="false"></asp:TextBox>
                                                <label class="JobCodeInputSeparator">X</label>
                                            </div>
                                        </div>
                                        <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4 JobCodeInput">
                                            <label class="col-xs-12 col-sm-12 col-md-12 col-lg-12 col-form-label" for="JobCodePercentage">Percentage:</label>
                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 pull-right">
                                                <asp:TextBox ID="JobCodePercentage" runat="server" onchange="JobCodeTotalPercentageGet();" CssClass="form-control JobCodeInput" Width="80%" MaxLength="6" EnableViewState="false"></asp:TextBox>
                                                <label class="JobCodeInputSeparator">=</label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-2 col-sm-2 col-md-2 col-lg-2 JobCodeInput">
                                    <label class="col-xs-12 col-sm-12 col-md-12 col-lg-12 col-form-label" for="JobCodePercentage">Total:</label>
                                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 pull-right">
                                        <label id="JobCodeTotalPercentageLabel">--%</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <input class="btn btn-sm btn-primary pull-left " id="JobCodeInsert" onclick="JobCodeValidate();" type="button" value="Add" />
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <div class="row" style="margin-top: 10px;">
                                <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                                    <h4 id="JobCodeTitle">Job Codes Eligible For Bonus Plan</h4>
                                </div>
                                <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                                    <div class="input-group">
                                        <input class="form-control input-sm pull-right" id="JobCodeFilter" type="search" placeholder="To filter your results, enter your search criteria here" />
                                        <span class="input-group-addon"><span class="glyphicon glyphicon-search" title="search"></span></span>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <div class="table-responsive">
                                        <table class="tablesorter table-striped table table-condensed" id="JobCodeGrid">
                                            <thead>
                                                <tr class="TableRowBorderTop">
                                                    <th class="hidden-xs">ID</th>
                                                    <th class="hidden-xs">Job Code</th>
                                                    <th>Job Title</th>
                                                    <th>Community</th>
                                                    <th>Base Commission</th>
                                                    <th>Multiplier</th>
                                                    <th>Percentage</th>
                                                    <th>Effective Dt</th>
                                                    <th>&nbsp;</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane col-xs-12 col-sm-12 col-md-12 col-lg-12" id="ApprovalTab" role="tabpanel">
                        <div class="col-xs-12 col-sm-12 col-md-8 col-lg-8">
                            <div class="form-group row">
                                <label class="col-xs-12 col-sm-3 col-md-3 col-lg-3 control-label col-form-label" for="JobCategoryID">Job Role:</label>
                                <div class="col-xs-12 col-sm-8 col-md-8 col-lg-8">
                                    <asp:DropDownList ID="JobCategoryID" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-xs-2 col-sm-3 col-md-3 col-lg-3 control-label col-form-label" for="ApprovalSort">Sort:</label>
                                <div class="col-xs-10 col-sm-8 col-md-8 col-lg-8">
                                    <asp:TextBox ID="ApprovalSort" runat="server" CssClass="form-control" Width="100px" MaxLength="5" EnableViewState="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-xs-2 col-sm-3 col-md-3 col-lg-3 control-label col-form-label" for="ApprovalAmount">Amount:</label>
                                <div class="col-xs-10 col-sm-8 col-md-8 col-lg-8">
                                    <asp:TextBox ID="ApprovalAmount" runat="server" CssClass="form-control" Width="100px" EnableViewState="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                <input class="btn btn-sm btn-primary pull-left " id="ApproverInsert" onclick="ApproverValidate();" type="button" value="Add" />
                            </div>
                            <br />
                            <div class="row" style="margin-top: 10px;">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <div class="table-responsive">
                                        <table class="tablesorter table-striped table table-condensed" id="ApprovalWorkflowGrid">
                                            <thead>
                                                <tr>
                                                    <th class="tableTitle" colspan="5">Approval Workflow</th>
                                                </tr>
                                                <tr>
                                                    <th>ID</th>
                                                    <th>Job Role</th>
                                                    <th>Sort</th>
                                                    <th>Amount</th>
                                                    <th>&nbsp;</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane col-xs-12 col-sm-12 col-md-12 col-lg-12" id="UserTab" role="tabpanel">
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <div class="form-group row">
                                <label class="col-xs-12 col-sm-3 col-md-3 col-lg-3 control-label col-form-label" for="EmployeeID">ADP Employee ID:</label>
                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                    <asp:TextBox ID="EmployeeID" runat="server" CssClass="form-control" placeholder="Enter ADP ID or employee name to search " EnableViewState="false"></asp:TextBox>
                                    <asp:HiddenField ID="EmployeeHiddenID" runat="server" EnableViewState="false" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <div class="panel panel-default">
                                        <div class="panel-body">
                                            <div class="form-group row">
                                                <div class="col-xs-12 col-sm-4 col-md-4 col-lg-4 UserFormGroup">
                                                    <label class="col-xs-12 col-sm-12 col-md-12 col-lg-12">Community</label>
                                                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                        <asp:DropDownList ID="CommunityNumber" runat="server" CssClass="form-control" EnableViewState="false"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-sm-4 col-md-4 col-lg-4 UserFormGroup">
                                                    <label class="col-xs-3 col-sm-12 col-md-12 col-lg-12">Percentage</label>
                                                    <div class="col-xs-9 col-sm-12 col-md-12 col-lg-12">
                                                        <asp:TextBox ID="UserBonusPercentage" runat="server" CssClass="form-control" EnableViewState="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-xs-12 col-sm-12 col-md-12 col-lg-12">Effective Date</label>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-xs-12 col-sm-4 col-md-4 col-lg-4 UserFormGroup">
                                                    <label class="col-xs-3 col-sm-12 col-md-12 col-lg-12">From</label>
                                                    <div class=" col-xs-9 col-sm-12 col-md-12 col-lg-12">
                                                        <div class="input-group date">
                                                            <input type="text" id="BeginDt" data-date-format="mm/dd/yyyy" placeholder="MM/DD/YYYY" class="datepicker form-control" />
                                                            <label for="BeginDt" class="input-group-addon btn">
                                                                <span class="glyphicon glyphicon-calendar" title="calendar" style="font-size: 16px;"></span>
                                                            </label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-sm-4 col-md-4 col-lg-4 UserFormGroup">
                                                    <label class="col-xs-3 col-sm-12 col-md-12 col-lg-12">To</label>
                                                    <div class=" col-xs-9 col-sm-12 col-md-12 col-lg-12">
                                                        <div class="input-group date">
                                                            <input type="text" id="EndDt" data-date-format="mm/dd/yyyy" placeholder="MM/DD/YYYY" class="datepicker form-control" />
                                                            <label for="EndDt" class="input-group-addon btn">
                                                                <span class="glyphicon glyphicon-calendar" title="calendar" style="font-size: 16px;"></span>
                                                            </label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-sm-2 col-md-2 col-lg-2" style="display: flex;">
                                                    <div class="row" style="align-self: center;">
                                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                            <asp:CheckBox ID="RollforwardFlg" runat="server" /><label>&nbsp;Recurring</label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-sm-8 col-md-8 col-lg-8 AddUserButtonColumn">
                                                    <input type="button" value="Add" class="btn btn-primary pull-right" onclick="BonusPlanToUserCommunityValidate();" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="margin-top: 10px;">
                                <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                                    <h4 id="UserApprovalTitle">User Eligible For Bonus</h4>
                                </div>
                                <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                                    <div class="input-group">
                                        <input class="form-control input-sm pull-right" id="UserCommunityFilter" type="search" placeholder="To filter your results, enter your search criteria here" />
                                        <span class="input-group-addon"><span class="glyphicon glyphicon-search" title="search"></span></span>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <div class="table-responsive">
                                        <table class="tablesorter table-striped table table-condensed" id="UserCommunityGrid">
                                            <thead>
                                                <tr class="TableRowBorderTop">
                                                    <th class="hidden-xs">ID</th>
                                                    <th class="hidden-xs">ADP ID</th>
                                                    <th>Employee</th>
                                                    <th>Community</th>
                                                    <th>%</th>
                                                    <th>Begin Dt</th>
                                                    <th>End Dt</th>
                                                    <th>Recurring</th>
                                                    <th>&nbsp;</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td colspan="8">
                                                        <img src='/images/progressSnapIn.gif' alt='Loading' />&nbsp;Please wait downloading...</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-1"></div>
        </div>
    </form>
    <!--#include file="include/GoogleAnalytic.html"-->
</body>
</html>