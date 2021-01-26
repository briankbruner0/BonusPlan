<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptBonusCodeQuery.aspx.cs" Inherits="BonusPlan.rptBonusCodeQuery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%=objSecurity.Title%>
    </title>
    <link href="../../../../../lib/style/Portal.css" type="text/css" rel="stylesheet" />
    <link href="../../../../../lib/Style/CalendarControl.css" type="text/css" rel="StyleSheet" />
    <link href="Styles/printing.css" type="text/css" rel="Stylesheet" media="print" />
    <script src="../../../../../lib/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../../../../lib/jquery.tablesorter.js" type="text/javascript"></script>
    <script src="../../../../../lib/jQuery.helpMessage.js" type="text/javascript"></script>
    <script src="../../../../../lib/CalendarControl.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#SearchResultGrid").tablesorter({
                debug: false,
                headers: { 0: { sorter: false} }
            });
        });
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
        <table style="margin-left: 7px; width: 1040px" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td>
                    <fieldset>
                        <legend>Search</legend>
                        <br />
                        <table cellpadding="0" cellspacing="2" border="0" style="width: 100%;">
                            <tr>
                                <td class="Label" style="width: 180px;">Create Date:
                                </td>
                                <td class="Data">
                                    <asp:TextBox ID="CreateDT" runat="server" Style="width: 60px" MaxLength="10" helpMessage="The Date the Bonus was Created"></asp:TextBox>
                                    <a href="Javascript:showCalendarControl(document.getElementById('CreateDT'));void(0);">
                                        <img alt="Calendar" src="../../../images/calendar_icon.gif" name="CreateDate" id="CreateDatePicker" onmouseover="style.cursor='hand';" border="0" />
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td class="Label" style="width: 180px;">Revenue Entry Type:
                                </td>
                                <td class="Data" style="width: 600px;">
                                    <asp:DropDownList ID="RevenueEntryTypeID" runat="server" Width="260px" helpMessage="The Revenue Entry Type">
                                    </asp:DropDownList>
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
                                    <asp:Button ID="Submit" runat="server" CssClass="button" Text="Submit" OnClick="Submit_Click" />
                                    <input type="button" value="Cancel" class="button" name="Cancel" onclick="window.location='rptbonuscodequery.aspx'" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
        </table>
        <br />
        <asp:Literal ID="SearchResultCriteria" EnableViewState="true" runat="server"></asp:Literal>
        <br />
        <table style="margin-left: 7px; width: 1040px;" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td>
                    <asp:Literal ID="SearchResultGrid" EnableViewState="true" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
    </form>
    <script>
        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
            m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-47186728-17', 'atriacom.com');
        ga('send', 'pageview');
    </script>
</body>
</html>