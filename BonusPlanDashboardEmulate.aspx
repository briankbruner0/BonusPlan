﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BonusPlanDashboardEmulate.aspx.cs" Inherits="BonusPlan.BonusPlanDashboardEmulate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!--#include file="include/hdrMain.html"-->

    <%--Menu Bar Component--%>
    <script src="js/component/MenuBar.js"></script>

    <%--Move In Revenue Component--%>
    <script src="js/component/MoveInRevenuePanel.js"></script>

    <%--Bonus Message Component--%>
    <script src="js/component/BonusMessage.js"></script>

    <%--Pending Payment Component--%>
    <script src="js/component/PendingPaymentPanel.js"></script>

    <%--Processed Payment Component--%>
    <script src="js/component/ProcessedPaymentPanel.js"></script>

    <%--Approval Component--%>
    <script src="js/component/DashboardApprovalPanel.js"></script>

    <link href="css/BonusPlan.css" rel="stylesheet" type="text/css" />

    <script>

        var RevenueList = null;

        $(document).ready(function ()
        {
            $(window).keydown(function (event)
            {
                if (event.keyCode == 13)
                {
                    event.preventDefault();
                    UserGet();
                    return false;
                }
            });

            PageLoad();
        });

        // Changes the emulated user for this page
        function UserGet()
        {
            var username = "";
            username = $("#EmulateUsername").val();

            if (username.length == 0)
            {
                // Get username from cookie (resets user to whoever is logged in)
                username = getCookie("USERNAME");
            }

            $("#Username").val(username);
            $(document).unbind("monthChange");
            PageLoad();
        }

        function PageLoad()
        {
            var menu = new MenuBar("Community", "MenuBarContainer");
            menu.MenuBarByCommunityNumberGet($("#CommunityNumber").val());

            var moveInRevenue = new MoveInRevenuePanel("MoveInRevenueContainer");
            moveInRevenue.MoveInRevenueActivityGet($("#CommunityNumber").val(), "", Date.today().toString("MM/dd/yyyy"), $("#Username").val(), "1");

            var bonusMessage = new BonusMessage("BonusMessageContainer");

            var pendingPanel = new PendingPaymentPanel("PendingPaymentContainer");
            pendingPanel.PendingPaymentGet($("#CommunityNumber").val(), Date.today().toString("MM/dd/yyyy"), $("#Username").val());

            var processedPanel = new ProcessedPaymentPanel("ProcessedPaymentContainer");
            processedPanel.ProcessedPaymentGet($("#CommunityNumber").val(), Date.today().toString("MM/dd/yyyy"), $("#Username").val());

            var approvalPanel = new DashboardApprovalPanel("ApprovalContainer");
            approvalPanel.ApprovalByUsernameGet($("#Username").val());

            $("#BonusPlanRevenue").tablesorter({
                debug: false,
                headers: {
                    1: { sorter: false },
                    7: { sorter: false }
                },
                cssHeader: "headerSort",
                cssDesc: "headerSortDown",
                cssAsc: "headerSortUp"
            });

            $("#snpPending").tablesorter({
                debug: false,
                cssHeader: "headerSort",
                cssDesc: "headerSortDown",
                cssAsc: "headerSortUp"
            });

            $("#snpProcessed").tablesorter({
                debug: false,
                cssHeader: "headerSort",
                cssDesc: "headerSortDown",
                cssAsc: "headerSortUp"
            });
        }

        function getCookie(cname)
        {
            var name = cname + "=";
            var decodedCookie = decodeURIComponent(document.cookie);
            var ca = decodedCookie.split(';');
            for (var i = 0; i < ca.length; i++)
            {
                var c = ca[i];
                while (c.charAt(0) == ' ')
                {
                    c = c.substring(1);
                }
                if (c.indexOf(name) == 0)
                {
                    return c.substring(name.length, c.length);
                }
            }
            return "";
        }
    </script>
</head>

<body>
    <form id="form1" runat="server" class="form-horizontal">
        <asp:HiddenField ID="CommunityNumber" runat="server" />
        <asp:HiddenField ID="Username" runat="server" />

        <div class="navbar navbar-default navbar-static" role="navigation">
            <asp:Literal ID="SecurityMenu" runat="server" EnableViewState="false"></asp:Literal>
        </div>

        <div class="container">
            <div class="col-lg-12">

                <asp:Literal ID="PageMessage" runat="server"></asp:Literal>
                <h1>
                    <asp:Literal ID="PageTitle" runat="server" EnableViewState="false"></asp:Literal></h1>
                <p>
                    <asp:Literal ID="PageBody" runat="server" EnableViewState="false"></asp:Literal>
                </p>
                <br />
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-10 col-lg-10">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div class="form-group row" style="margin-bottom: 0px;">
                                    <label class="col-xs-12 col-sm-4 col-md-4 col-lg-4 control-label col-form-label LabelRequired" for="EmulateUsername">Emulate Username:</label>
                                    <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                        <input name="EmulateUsername" class="form-control" id="EmulateUsername" type="text" value="" />
                                    </div>
                                    <div class="col-xs-12 col-sm-2 col-md-2 col-lg-2 btn-group-sm">
                                        <input class="btn btn-sm btn-primary pull-left " id="UserEmulateButton" onclick="UserGet();" type="button" value="Go" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="MenuBarContainer"></div>
                <div id="BonusMessageContainer"></div>
                <div class="row">
                    <div class="col-lg-9">
                        <div id="MoveInRevenueContainer"></div>
                    </div>
                    <div class="col-lg-3">
                        <div class="row">
                            <div id="PendingPaymentContainer"></div>
                        </div>
                        <div class="row">
                            <div id="ProcessedPaymentContainer"></div>
                        </div>
                        <div class="row">
                            <div id="ApprovalContainer"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Revenue Detail Modal -->
        <div class="modal fade" id="RevenueDetailModal" tabindex="-1" role="dialog">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">Revenue Detail</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-lg-12 modalResidentHeading">
                                <span id="RevenueDetailModalResidentName">Bruning, Delores</span>
                            </div>
                        </div>
                        <div class="row" style="padding-bottom: 5px;">
                            <div class="col-lg-12">
                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                    <div class="row">
                                        <div class="col-xs-5 col-sm-5 col-md-5 col-lg-5" style="text-align: right;">
                                            <label>Customer ID: </label>
                                        </div>
                                        <div class="col-xs-7 col-sm-7 col-md-7 col-lg-7" id="RevenueDetailModalCustomerID">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-5 col-sm-5 col-md-5 col-lg-5" style="text-align: right;">
                                            <label>Room: </label>
                                        </div>
                                        <div class="col-xs-7 col-sm-7 col-md-7 col-lg-7">
                                            <span id="RevenueDetailModalRoomNumber"></span>
                                            <span class="badge roomBadge-AL" id="RevenueDetailModalCareTypeCode">AL</span>&nbsp;<span id="RevenueDetailModalRoomType"></span>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-5 col-sm-5 col-md-5 col-lg-5" style="text-align: right;">
                                            <label>Type: </label>
                                        </div>
                                        <div class="col-xs-7 col-sm-7 col-md-7 col-lg-7" id="RevenueDetailModalRoomStyle">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                    <div class="row">
                                        <div class="col-xs-5 col-sm-6 col-md-6 col-lg-6" style="text-align: right;">
                                            <label>Move In: </label>
                                        </div>
                                        <div class="col-xs-7 col-sm-6 col-md-6 col-lg-6" id="RevenueDetailModalMoveInDT">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-5 col-sm-6 col-md-6 col-lg-6" style="text-align: right;">
                                            <label>Move Out: </label>
                                        </div>
                                        <div class="col-xs-7 col-sm-6 col-md-6 col-lg-6" id="RevenueDetailModalMoveOutDT">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <div class="row" style="padding-top: 5px;">
                                        <div class="col-xs-5 col-sm-7 col-md-7 col-lg-7" style="padding: 0px; text-align: right;">
                                            <label>Has the Residency Agreement been Verified?</label>
                                        </div>
                                        <div class="col-xs-7 col-sm-5 col-md-5 col-lg-5" id="RevenueDetailModalResidencyAgreementFlg">
                                            <span class="glyphicon glyphicon-unchecked"></span>
                                            <label>No</label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <table class="table table-striped table-condensed" id="RevenueDetailModalRevenueTable">
                                    <thead>
                                        <tr>
                                            <th>Revenue Type</th>
                                            <th class="numericShort">Amount</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                    <tfoot>
                                    </tfoot>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
        <!-- /.modal -->

        <!-- Processed Payment Detail Modal -->
        <div class="modal fade" id="ProcessedPaymentDetailModal" tabindex="-1" role="dialog">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">Processed Payment Detail</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-lg-12">
                                <table class="table table-striped table-condensed" id="ProcessPaymentDetailModalTable">
                                    <thead>
                                        <tr>
                                            <th>Customer ID</th>
                                            <th>Resident</th>
                                            <th>Move In</th>
                                            <th>*Date</th>
                                            <th class="numericShort">Amount</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                    <tfoot>
                                        <tr>
                                            <td colspan="4" class="disclaimer">*Processing Date may not match the payroll date on your pay stub</td>
                                            <td class="numericShort pendingFooter">$--</td>
                                        </tr>
                                    </tfoot>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
        <!-- /.modal -->

        <!-- Approval Detail Modal -->
        <div class="modal fade" id="ApprovalDetailModal" tabindex="-1" role="dialog">
            <input type="hidden" id="ApprovalDetailModalPaymentToApprovalID" />
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">Approval Detail</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row" style="padding-bottom: 5px;">
                            <div class="col-lg-12">
                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                    <div class="row">
                                        <div class="col-xs-5 col-sm-5 col-md-5 col-lg-5" style="text-align: right;">
                                            <label>Name: </label>
                                        </div>
                                        <div class="col-xs-7 col-sm-7 col-md-7 col-lg-7" id="ApprovalDetailModalEmployeeName">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-5 col-sm-5 col-md-5 col-lg-5" style="text-align: right;">
                                            <label>Amount: </label>
                                        </div>
                                        <div class="col-xs-7 col-sm-7 col-md-7 col-lg-7" id="ApprovalDetailModalAmount">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-5 col-sm-5 col-md-5 col-lg-5" style="text-align: right;">
                                            <label>Approver: </label>
                                        </div>
                                        <div class="col-xs-7 col-sm-7 col-md-7 col-lg-7" id="ApprovalDetailModalApprover">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                    <div class="row">
                                        <div class="col-xs-5 col-sm-6 col-md-6 col-lg-6" style="text-align: right;">
                                            <label>Employee ID: </label>
                                        </div>
                                        <div class="col-xs-7 col-sm-6 col-md-6 col-lg-6" id="ApprovalDetailModalEmployeeID">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                    <div class="row">
                                        <div class="col-xs-5 col-sm-6 col-md-6 col-lg-6" style="text-align: right;">
                                            <label>Community: </label>
                                        </div>
                                        <div class="col-xs-7 col-sm-6 col-md-6 col-lg-6" id="ApprovalDetailModalCommunityName">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <table class="table table-striped table-condensed" id="ApprovalDetailModalTable">
                                    <thead>
                                        <tr>
                                            <th>Customer ID</th>
                                            <th>Resident</th>
                                            <th>Move In</th>
                                            <th>Note</th>
                                            <th>Amount</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                    <tfoot>
                                    </tfoot>
                                </table>
                            </div>
                        </div>
                        <%-- <div class="row" id="ApprovalApproveControl">
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                <input class="btn btn-primary btn-sm" onclick="ApprovePayment();" type="button" value="Approve" />
                                <input class="btn btn-danger btn-sm" onclick="$('#ApprovalExceptionControl').toggle(); $('#ApprovalApproveControl').toggle();" type="button" value="Exception" />
                                <input class="btn btn-default btn-sm" onclick="$('#ApprovalDetailModal').modal('toggle');" type="button" value="Cancel" />
                            </div>
                        </div>
                        <div class="row" id="ApprovalExceptionControl" style="display: none;">
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                <div class="row">
                                    <div class="col-xs-2 col-sm-2 col-md-2 col-lg-2" style="text-align: right;">
                                        <label>Note: </label>
                                    </div>
                                    <div class="col-xs-10 col-sm-10 col-md-10 col-lg-10">
                                        <textarea id="ApprovalExceptionNote" class="form-control"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                <input class="btn btn-primary btn-sm" onclick="CreateException();" type="button" value="Submit" />
                                <input class="btn btn-default btn-sm" onclick="$('#ApprovalExceptionControl').toggle(); $('#ApprovalApproveControl').toggle();" type="button" value="Cancel" />
                            </div>
                        </div>--%>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
        <!-- /.modal -->
    </form>
    <!--#include file="include/GoogleAnalytic.html"-->
</body>
</html>