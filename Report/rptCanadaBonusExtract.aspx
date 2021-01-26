<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptCanadaBonusExtract.aspx.cs" Inherits="BonusPlan.rptCanadaBonusExtract" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <%=objSecurity.Title%>
    </title>
    <link href="../../../../../lib/style/Portal.css" type="text/css" rel="STYLESHEET" />
    <link href="../../../../lib/style/CalendarControl.css" type="text/css" rel="styleSheet" />
    <script src="../../../lib/ControlValidator.js" type="text/javascript"></script>
    <script src="lib/DataUpload.js" type="text/javascript"></script>
    <script src="../../../../../../../../../lib/jquery-min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../../../lib/CalendarControl.js"></script>
</head>
<body>
    <form id="DataExtractForm" runat="server">
        <asp:Literal ID="SecurityMenu" EnableViewState="false" runat="server"></asp:Literal>
        <table style="margin-left: 7px; padding: 0px; border-collapse: collapse; border-spacing: 0; width: 750px">
            <tr>
                <td class="Atria_Title" style="text-align: left">
                    <% =objSecurity.Title %>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <% =objSecurity.Body %>
                </td>
            </tr>
        </table>
        <fieldset style="margin-left: 7px; width: 575px;">
            <legend>Data Extract</legend>
            <br />
            <table style="margin-left: 7px; padding: 0px; border-collapse: collapse; border-spacing: 0;">
                <tr>
                    <td class="Label">Month/Year:</td>
                    <td class="Data">
                        <asp:DropDownList ID="MonthList" runat="server"></asp:DropDownList>
                        <asp:DropDownList ID="YearList" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td>
                        <table style="padding: 0px; border-collapse: collapse; border-spacing: 0;">
                            <tr>
                                <td>
                                    <asp:Button ID="Submit" runat="server" CssClass="button" Text="Submit" OnClick="Submit_Click" />
                                    <input type="button" value="Cancel" class="button" name="Cancel" onclick="window.location = '../Report.aspx'" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
            </table>
        </fieldset>
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