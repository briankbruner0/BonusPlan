<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MoveInRevenueProfile.aspx.cs" Inherits="BonusPlan.MoveInRevenueProfile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <%=objSecurity.Title%>
    </title>

    <link href="../../../../../lib/style/Portal.css" type="text/css" rel="Stylesheet" />

    <script type="text/javascript" src="../../../lib/jquery-min.js"></script>
    <script src="../../../../lib/jquery.tablesorter.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $("#RevenueDetailGrid")
                .tablesorter({
                    debug: false,
                    headers: {
                    }
                });

        });

        function ValidatePage() {
            var error = "";

            if (error.length > 0) {
                alert(error);
                return false;
            }
            else {
                return true;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="MoveInRevenueIDHidden" runat="server" />
        <div class="AtriaMenu">
            <asp:Literal ID="SecurityMenu" EnableViewState="false" runat="server"></asp:Literal>
        </div>
        <br />
        <br />
        <br />
        <br />
        <table style="width: 750px; margin-left: 7px;" cellpadding="0" cellspacing="0" border="0">
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
        <table style="width: 750px; margin-left: 7px;" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td>
                    <fieldset>
                        <legend>Customer Detail</legend>
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="Label">Move In Revenue ID:
                                </td>
                                <td class="Label">
                                    <asp:Label ID="MoveInRevenueID" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Label">Community:
                                </td>
                                <td>
                                    <asp:Label ID="Community" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Label">Community ID:
                                </td>
                                <td>
                                    <asp:Label ID="CommunityID" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Label">Customer ID:
                                </td>
                                <td>
                                    <asp:Label ID="CustomerID" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Label">Move In Date:
                                </td>
                                <td>
                                    <asp:Label ID="MoveInDT" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Label">Move Out Date:
                                </td>
                                <td>
                                    <asp:Label ID="MoveOutDT" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Label">Create By:
                                </td>
                                <td>
                                    <asp:Label ID="CreateBy" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Literal ID="LedgerEntry" runat="server" EnableViewState="false"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button runat="server" ID="Cancel" Text="Cancel" CssClass="button" OnClick="Cancel_Click" />
                                    <asp:Button runat="server" ID="Delete" Text="Delete" CssClass="button" OnClick="Delete_Click" Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <table cellpadding="0" cellspacing="0" border="0" style="margin-left: 7px">
                        <tr>
                            <td>
                                <asp:Literal ID="RevenueDetailGrid" runat="server" EnableViewState="false"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
    <!--#include file="include/GoogleAnalytic.html"-->
</body>
</html>