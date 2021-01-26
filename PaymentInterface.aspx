<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentInterface.aspx.cs" Inherits="BonusPlan.PaymentInterface" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%=objSecurity.Title%>
    </title>
    <link href="../../../../../lib/style/Portal.css" type="text/css" rel="stylesheet" />

    <script src="../../../../../lib/jquery-min.js" type="text/javascript"></script>
    <script src="../../../../../lib/jquery.tablesorter.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {

            $("#PaymentSearchGrid").tablesorter({
                debug: false
            });
        });

        function ValidatePage() {

            var confirmed = confirm('Are you sure you want to approve this payroll?');

            if (confirmed) {
                return true;
            }
            else{
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Literal ID="SecurityMenu" EnableViewState="false" runat="server"></asp:Literal>
        <table style="width: 1040px; margin-left: 7px;" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td class="Atria_Title">
                    <asp:Literal ID="SecurityTitle" EnableViewState="false" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Literal ID="SecurityBody" EnableViewState="false" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
        <br />
        <table style="margin-left: 7px; width: 767px" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td>
                    <fieldset>
                        <legend>Payroll Generate</legend>
                        <br />
                        <table cellpadding="0" cellspacing="2" border="0" style="width: 100%;">
                            <tr>
                                <td class="Label">Bonus Plan:
                                </td>
                                <td class="Data">
                                    <asp:DropDownList ID="BonusPlanID" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="Label">Output To:
                                </td>
                                <td class="Data">
                                    <asp:RadioButton ID="Screen" runat="server" GroupName="Export" Text="Screen" Checked="true" /><br />
                                    <asp:RadioButton ID="Excel" runat="server" GroupName="Export" Text="Excel" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                                <td>
                                    <asp:Button ID="GeneratePayroll" runat="server" CssClass="button"
                                        Text="Generate Payroll" OnClick="GeneratePayroll_Click" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
        </table>
        <br />
        <table style="margin-left: 7px; width: 1040px;" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td>
                    <asp:Button ID="Submit" runat="server" CssClass="button" Text="Submit"
                        OnClientClick="return ValidatePage();" OnClick="Submit_Click" Visible="false" />
                    <asp:Button Text="Cancel" CssClass="button" ID="Cancel" runat="server" Visible="false" OnClientClick="window.location='PaymentInterface.aspx'" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Literal ID="PaymentGrid" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
    </form>
    <!--#include file="include/GoogleAnalytic.html"-->
</body>
</html>