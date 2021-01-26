<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BonusPlanEDDashboard.aspx.cs" Inherits="BonusPlan.BonusPlanEDDashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!--#include file="include/hdrMain.html"-->

    <%--Menu Bar Component--%>
    <script src="js/component/MenuBar.js"></script>

    <%--Move In Revenue Component--%>
    <script src="js/component/MoveInRevenueEDProfile.js"></script>

    <link href="css/BonusPlan.css" rel="stylesheet" type="text/css" />

    <script>

        var RevenueList = null;

        $(document).ready(function ()
        {
            var menu = new MenuBar("Community", "MenuBarContainer", "BonusPlanEDDashboard.aspx");
            menu.MenuBarByCommunityNumberGet($("#CommunityNumber").val());

            var moveInRevenue = new MoveInRevenuePanel("MoveInRevenueContainer");
            moveInRevenue.MoveInRevenueActivityGet($("#CommunityNumber").val(), Date.today().toString("MM/dd/yyyy"), "1");

            $(".BonusPlanRevenue").tablesorter({
                debug: false,
                cssHeader: "headerSort",
                cssDesc: "headerSortDown",
                cssAsc: "headerSortUp"
            });

            //setting a wait period so the menu bar will have time to render
            //was unable to get a deferred object to work on the menu rendering so an explicit timeout is set
            //hiding the cluster nav for this panel only
            setTimeout(function() {$("#clusterHome").css("display", "none")}, 30);
        });
    </script>
</head>

<body>
    <form id="form1" runat="server">
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
                <div id="MenuBarContainer"></div>
                <div class="row">
                    <div class="col-lg-12">
                        <div id="MoveInRevenueContainer"></div>
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
    </form>
    <!--#include file="include/hdrFooter.html"-->
    <!--#include file="include/GoogleAnalytic.html"-->
</body>
</html>