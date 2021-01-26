<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MoveInRevenueSearch.aspx.cs" Inherits="BonusPlan.MoveInRevenueSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <%=objSecurity.Title%>
    </title>

    <link href="../../../../../lib/style/Portal.css" type="text/css" rel="Stylesheet" />
    <link type="text/css" rel="StyleSheet" href="../../../../../lib/Style/CalendarControl.css" />

    <script type="text/javascript" src="../../../lib/jquery-min.js"></script>
    <script type="text/javascript" src="../../../lib/JSON2.js"></script>
    <script type="text/javascript" src="../../../../../lib/CalendarControl.js"></script>
    <script src="../../../../lib/jquery.tablesorter.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {

            $("#RevenueSearchGrid").tablesorter({
                debug: false,
                headers: { 0: { sorter: false} }
            });
        });

        function ValidatePage() {
            var error = "";

            //Regular expression for date check
            var dateRegEx = /^(0?[1-9]|1[012])[\/\-](0?[1-9]|[12][0-9]|3[01])[\/\-](19|20)[0-9][0-9]$/;

            //Begin Date
            var dtBegin = $("#BeginDt").val();

            if (dtBegin.length == 0) {
                error += 'From Date is required.\n';
            }
            else {
                //Validate Proper Date Format
                if (!dateRegEx.test(dtBegin)) {
                    error += 'From Date Format Is Incorrect.\n';
                }
            }

            //Due Date End
            var dtEnd = $("#EndDt").val();

            if (dtEnd.length == 0) {
                error += 'To Date To is required.\n';
            }
            else {
                //Validate Proper Date Format
                if (!dateRegEx.test(dtEnd)) {
                    error += 'To Date To Format Is Incorrect.\n';
                }
            }

            //Validate Due Date To Date is not the same as Due Date From Date
            var beginDT = Date.parse(dtBegin);
            var endDT = Date.parse(dtEnd);

            if (dtBegin > dtEnd) {
                error += 'To Date must be later than the From Date.\n';
            }

            //Validate that the To & From dates are not more than 6 months apart
            //First subtract the beginDT val from the endDT giving you the date diff in milliseconds
            //Then divide that result by (24*60*60*100) or the number of milliseconds in 1 day
            //If the result is > 180 days then flag the date values.
            if (((endDT - beginDT)/(24*60*60*1000)) > 180) {
                error += 'To Date cannot be more than 6 months greater than the From Date.\n';
            }

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
                        <legend>Revenue Search</legend>
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan='2'>
                                    <asp:Label ID="ErrorMessage" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Label">Customer ID:
                                </td>
                                <td class="Data">
                                    <asp:TextBox ID="CustomerID" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="Label">Resident First Name:
                                </td>
                                <td class="Data">
                                    <asp:TextBox ID="FirstName" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="Label">Resident Last Name:
                                </td>
                                <td class="Data">
                                    <asp:TextBox ID="LastName" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="Label">Effective Date
                                </td>
                                <td>
                                    <table cellpadding="0" cellspacing="0" border="0" style="width: 300px;">
                                        <tr>
                                            <td class="Label">From
                                            </td>
                                            <td class="Label">To
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Data" style="width: 135px;">
                                                <input id="BeginDt" type="text" maxlength="10" name="BeginDt"
                                                    size="14" helpmessage="Date the search starts" runat="server" />
                                                <a href="javascript:showCalendarControl(document.getElementById('BeginDt'));void(0);">
                                                    <img alt="Calendar" src="../../../../images/calendar_icon.gif" name="txtBeginDt"
                                                        onmouseover="style.cursor='hand';" border="0" style="vertical-align: top; padding-top: 1px; padding-bottom: 1px;" /></a>
                                            </td>
                                            <td class="Data">
                                                <input id="EndDt" type="text" maxlength="10" name="EndDt"
                                                    size="14" helpmessage="Date the search ends" runat="server" />
                                                <a href="javascript:showCalendarControl(document.getElementById('EndDt'));void(0);">
                                                    <img alt="Calendar" src="../../../../images/calendar_icon.gif" name="txtEndDt"
                                                        onmouseover="style.cursor='hand';" border="0" style="vertical-align: top; padding-top: 1px; padding-bottom: 1px;" /></a>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="Label">Community:
                                </td>
                                <td class="Data">
                                    <asp:DropDownList ID="CommunityID" runat="server"></asp:DropDownList>
                                </td>
                            </tr>

                            <tr>
                                <td class="Label">Output to:&nbsp;
                                </td>
                                <td class="Data" style="width: 100px; height: 16px">
                                    <asp:RadioButton ID="Screen" runat="server" Text="Screen" GroupName="OutputTo" Checked="true" /><br />
                                    <asp:RadioButton ID="Excel" runat="server" Text="Excel" GroupName="OutputTo" /><br />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:Button runat="server" ID="Submit" Text="Submit" CssClass="button" OnClick="SearchSubmit_Click"
                                        OnClientClick="return ValidatePage();" />
                                    <asp:Button runat="server" ID="Cancel" Text="Cancel" CssClass="button"
                                        OnClick="Cancel_Click" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
        </table>
        <br />
        <asp:Literal ID="RevenueSearchCriteriaGrid" EnableViewState="false" runat="server"></asp:Literal>
        <br />
        <table style="width: 750px; margin-left: 7px;" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td>
                    <asp:Literal ID="RevenueSearchGrid" runat="server" EnableViewState="false"></asp:Literal>
                </td>
            </tr>
        </table>
    </form>
    <!--#include file="include/GoogleAnalytic.html"-->
</body>
</html>