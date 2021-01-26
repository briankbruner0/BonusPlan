<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentSchedule.aspx.cs" Inherits="BonusPlan.PaymentSchedule" %>

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
            $("#PaymentScheduleGrid").tablesorter({
                debug: false,
                headers: { 0: { sorter: false } }
            });
        });

        function ValidatePage() {
            var error = "";

            if ($("#BonusPlanID").val().length == 0) {
                error += "A Bonus Plan is required.\n";
            }

            if ($("#EmailAlertDt").val().length == 0) {
                error += "An email alert date is required.\n";
            }

            if ($("#ProcessDt").val().length == 0) {
                error += "A process date is required.\n";
            }

            if ($("#PayrollDt").val().length == 0) {
                error += "A payroll date is required.\n";
            }

            //Regular expression for date check
            var dateRegEx = /^(0[1-9]|1[012])[\/\-](0[1-9]|[12][0-9]|3[01])[\/\-](19|20)[0-9][0-9]$/;
            if (!dateRegEx.test($("#EmailAlertDt").val())) {
                error += 'Email Alert Date Format Is Incorrect.\n';
            }

            if (!dateRegEx.test($("#ProcessDt").val())) {
                error += 'Process Date Format Is Incorrect.\n';
            }

            if (!dateRegEx.test($("#PayrollDt").val())) {
                error += 'Payroll Date Format Is Incorrect.\n';
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
        <asp:HiddenField ID="PaymentScheduleHiddenID" runat="server" />
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
                        <legend>Payment Schedule</legend>
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td class="Label">ID:
                                </td>
                                <td>
                                    <asp:Literal ID="PaymentScheduleID" runat="server" EnableViewState="false"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td class="LabelRequired" style="width: 200px">Bonus Plan:
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="BonusPlanID"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="LabelRequired" style="width: 200px">Approval Notification Date:
                                </td>
                                <td>
                                    <asp:TextBox ID="EmailAlertDt" runat="server"></asp:TextBox>
                                    <a href="Javascript:showCalendarControl(document.getElementById('EmailAlertDt'));void(0);">
                                        <img alt="Calender" src="../../../../images/calendar_icon.gif" name="btnEmailDate"
                                            id="Img2" onmouseover="style.cursor='hand';" border="0" />
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td class="LabelRequired">Final Notification Date:
                                </td>
                                <td>
                                    <asp:TextBox ID="FinalNotificationDt" runat="server"></asp:TextBox>
                                    <a href="Javascript:showCalendarControl(document.getElementById('FinalNotificationDt'));void(0);">
                                        <img alt="Calender" src="../../../../images/calendar_icon.gif" name="btnFinalNotificationDate"
                                            id="Img3" onmouseover="style.cursor='hand';" border="0" />
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td class="LabelRequired">Process Date:
                                </td>
                                <td>
                                    <asp:TextBox ID="ProcessDt" runat="server"></asp:TextBox>
                                    <a href="Javascript:showCalendarControl(document.getElementById('ProcessDt'));void(0);">
                                        <img alt="Calender" src="../../../../images/calendar_icon.gif" name="btnProcessDate"
                                            id="btnFromDate" onmouseover="style.cursor='hand';" border="0" />
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td class="LabelRequired">Payroll Date:
                                </td>
                                <td>
                                    <asp:TextBox ID="PayrollDt" runat="server"></asp:TextBox>
                                    <a href="Javascript:showCalendarControl(document.getElementById('PayrollDt'));void(0);">
                                        <img alt="Calender" src="../../../../images/calendar_icon.gif" name="btnPayrollDate"
                                            id="Img1" onmouseover="style.cursor='hand';" border="0" />
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td class="Label">Create By:
                                </td>
                                <td>
                                    <asp:Literal ID="CreateBy" runat="server" EnableViewState="false"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td class="Label">Create Date:
                                </td>
                                <td>
                                    <asp:Literal ID="CreateDt" runat="server" EnableViewState="false"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td class="Label">Modify By:
                                </td>
                                <td>
                                    <asp:Literal ID="ModifyBy" runat="server" EnableViewState="false"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td class="Label">Modify Date:
                                </td>
                                <td>
                                    <asp:Literal ID="ModifyDt" runat="server" EnableViewState="false"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td class="Label">Active:
                                </td>
                                <td>
                                    <asp:CheckBox ID="ActiveFlgCB" runat="server" Visible="false" />
                                    <asp:Literal ID="ActiveFlg" runat="server" EnableViewState="false"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button runat="server" ID="Submit" Text="Submit" CssClass="button"
                                        OnClick="Submit_Click" OnClientClick="return ValidatePage();" />
                                    <asp:Button runat="server" ID="Cancel" Text="Cancel" CssClass="button"
                                        OnClick="Cancel_Click" />
                                    <asp:Button runat="server" ID="Delete" Text="Delete" Visible="false"
                                        CssClass="button" OnClick="Delete_Click" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
        </table>
        <br />
        <table style="width: 750px; margin-left: 7px;" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td>
                    <asp:Literal ID="PaymentScheduleGrid" runat="server" EnableViewState="false"></asp:Literal>
                </td>
            </tr>
        </table>
    </form>
    <!--#include file="include/GoogleAnalytic.html"-->
</body>
</html>