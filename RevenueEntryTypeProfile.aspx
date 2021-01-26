<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RevenueEntryTypeProfile.aspx.cs" Inherits="BonusPlan.RevenueEntryTypeProfile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%=objSecurity.Title%></title>
    <link href="../../../../../lib/style/Portal.css" type="text/css" rel="Stylesheet" />
    <script src="../../../../lib/jquery-min.js" type="text/javascript"></script>
    <script src="../../../../lib/jquery.tablesorter.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $("#RevenueEntryTypeGrid").tablesorter({
                debug: false,
                headers: { 0: { sorter: false} }
            });
        });

        function ValidatePage() {
            var error = "";

            var $RevenueEntryType = $("#RevenueEntryType");
            if ($("#RevenueEntryType").val().length > 100) {
                error += "Revenue Entry Type value is too long (Max Length - 100 characters).\n";
                $RevenueEntryType.css('backgroundColor', 'Pink');
            }
            else {
                if ($("#RevenueEntryType").val().length == 0) {
                    error += "Please enter a Revenue Entry Type\n";
                    $RevenueEntryType.css('backgroundColor', 'Pink');
                }
                else {
                    $RevenueEntryType.css('backgroundColor', 'White');
                }
            }

            if (error.length > 0) {
                alert(error);
                return false
            }
            else {
                return true;
            }
        }
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
        <table style="width: 650px; margin-left: 7px;" cellpadding="0" cellspacing="0" border="0">
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
        <table cellspacing="0" cellpadding="0" border="0" style="width: 650px; margin-left: 7px;">
            <tr>
                <td>
                    <fieldset>
                        <legend>Revenue Entry Type</legend>
                        <br />
                        <table cellspacing="0" cellpadding="0" border="0">
                            <tr>
                                <td colspan='2'>
                                    <asp:Label ID="ErrorMessage" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="LabelRequired">Revenue Entry Type:
                                </td>
                                <td class="value">
                                    <asp:TextBox ID="RevenueEntryType" runat="server" Width="250"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="Label">Create By:</td>
                                <td class="value">
                                    <asp:Label ID="CreateBy" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Label">Create Date:</td>
                                <td class="value">
                                    <asp:Label ID="CreateDT" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Label">Modify By:</td>
                                <td class="value">
                                    <asp:Label ID="ModifyBy" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Label">Modify Date:</td>
                                <td class="value">
                                    <asp:Label ID="ModifyDT" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Label">Active:
                                </td>
                                <td class="value">
                                    <asp:Literal ID="ActiveFlg" runat="server" EnableViewState="false"></asp:Literal>
                                    <asp:CheckBox ID="ActiveFlgCB" runat="server" Visible="false"></asp:CheckBox>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                                <td>
                                    <br />
                                    <asp:Button ID="Submit" CssClass="button" runat="server" Text="Submit" OnClientClick="return ValidatePage();"
                                        OnClick="Submit_Click" />
                                    <asp:Button ID="Cancel" CssClass="button" runat="server" Text="Cancel" OnClick="Cancel_Click" />
                                    <asp:Button ID="Delete" CssClass="button" runat="server" Text="Delete" Visible="false"
                                        OnClick="Delete_Click" />
                                    <asp:HiddenField ID="RevenueEntryTypeID" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
        </table>
        <br />
        <table cellspacing="0" cellpadding="0" border="0" style="width: 650px; margin-left: 7px;">
            <tr>
                <td>
                    <asp:Literal ID="RevenueEntryTypeGrid" runat="server" EnableViewState="false"></asp:Literal>
                </td>
            </tr>
        </table>
    </form>
    <!--#include file="include/GoogleAnalytic.html"-->
</body>
</html>