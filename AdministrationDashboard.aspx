<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdministrationDashboard.aspx.cs" Inherits="BonusPlan.AdministrationDashboard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <%=objSecurity.Title%>
    </title>

    <link href="../../../../../lib/style/Portal.css" type="text/css" rel="Stylesheet" />

    <script type="text/javascript" src="../../../lib/jquery-min.js"></script>
    <script src="../../../../lib/jquery.tablesorter.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {

            $("#QuickLinksGrid")
            .tablesorter({
                debug: false,
                cssHeader: "tablesorterHeaderPortal",
                cssDesc: "headerSortDownPortal",
                cssAsc: "headerSortUpPortal"
            });

            $("#RevenueAcccountsGrid")
            .tablesorter({
                debug: false,
                cssHeader: "tablesorterHeaderPortal",
                cssDesc: "headerSortDownPortal",
                cssAsc: "headerSortUpPortal"
            });

            $("#PaymentApprovalExceptionGrid")
            .tablesorter({
                debug: false,
                cssHeader: "tablesorterHeaderPortal",
                cssDesc: "headerSortDownPortal",
                cssAsc: "headerSortUpPortal"
            });

            $("#EmployeeOverrideGrid")
            .tablesorter({
                debug: false,
                cssHeader: "tablesorterHeaderPortal",
                cssDesc: "headerSortDownPortal",
                cssAsc: "headerSortUpPortal"
            });

            $("#ManagePaymentApprovalGrid")
            .tablesorter({
                debug: false,
                cssHeader: "tablesorterHeaderPortal",
                cssDesc: "headerSortDownPortal",
                cssAsc: "headerSortUpPortal"
            });

            $("#PayrollScheduleGrid")
            .tablesorter({
                debug: false,
                cssHeader: "tablesorterHeaderPortal",
                cssDesc: "headerSortDownPortal",
                cssAsc: "headerSortUpPortal"
            });

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="AtriaMenu">
            <asp:Literal ID="SecurityMenu" EnableViewState="false" runat="server"></asp:Literal>
        </div>
        <br />
        <br />
        <br />
        <br />
        <table style="width: 1000px; margin-left: 7px;" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td class="Atria_Title">
                    <asp:Literal ID="SecurityTitle" runat="server" EnableViewState="false"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Literal ID="SecurityBody" runat="server" EnableViewState="false"></asp:Literal>
                </td>
            </tr>
        </table>
        <br />
        <table style="width: 1000px; margin-left: 7px;" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td align="left">
                    <!-- Left Side -->
                    <table style="width: 500px;" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <asp:Literal ID="snpRevenueAcccounts" runat="server" EnableViewState="false"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <td>&nbsp;
                                </td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="snpEmployeeOverride" runat="server" EnableViewState="false"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                </td>
                <td align="left">&nbsp;</td>
                <td align="left">
                    <!-- Right Side -->
                    <table style="width: 500px;" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <asp:Literal ID="snpQuickLinks" EnableViewState="false" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="snpPaymentApprovalException" EnableViewState="false" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="snpManagePaymentApproval" EnableViewState="false" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="snpPayrollSchedule" EnableViewState="false" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <!--#include file="include/GoogleAnalytic.html"-->
    </form>
</body>
</html>