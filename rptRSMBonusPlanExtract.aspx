<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptRSMBonusPlanExtract.aspx.cs" Inherits="BonusPlan.rptRSMBonusExtract" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%=objSecurity.Title%>
    </title>
    <link href="../../../../../lib/style/Portal.css" type="text/css" rel="STYLESHEET" />
    <link href="../../../../lib/style/CalendarControl.css" type="text/css" rel="styleSheet" />
    <script src="../../../lib/ControlValidator.js" type="text/javascript"></script>
    <script src="lib/DataUpload.js" type="text/javascript"></script>
    <script src="../../../../../../../../../lib/jquery-min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../../../lib/CalendarControl.js"></script>

    <script type="text/javascript">
        function ValidatePage()
        {
            var error = "";

            var $EffectiveDT = $("#EffectiveDT");
            if ($.trim($EffectiveDT.val()).length == 0) {
                error += "Please enter an Effective Date.\n";
                $EffectiveDT.css('backgroundColor', 'Pink');
            }
            else {
                if (!$.trim($EffectiveDT.val()).isDate()) {
                    error += "Please enter a valid date for Effective Date.\n";
                    $EffectiveDT.css('backgroundColor', 'Pink');
                }
                else {
                    $EffectiveDT.css('backgroundColor', 'White');
                }
            }
            if (error.length > 0) {
                alert(error);
            }
            return error.length == 0;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Literal ID="SecurityMenu" EnableViewState="false" runat="server"></asp:Literal>
        <table cellspacing="0" cellpadding="0" border="0" style="margin-left: 7px;" width="750px">
            <tr>
                <td class="Atria_Title" align="left">
                    <% =objSecurity.Title %>
                </td>
            </tr>
            <tr>
                <td align="left">&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <% =objSecurity.Body %>
                </td>
            </tr>
        </table>
        <fieldset style="margin-left: 7px; width: 700px;">
            <legend>Data Extract</legend>
            <asp:Label ID="ErrorMessage" Font-Bold="true" ForeColor="Red" runat="server" EnableViewState="false"></asp:Label>
            <br />
            <table cellspacing="0" cellpadding="0" border="0" style="margin-left: 7px; width: 693px;">

                <tr>
                    <td class="Label">Country:</td>
                    <td class="Data">
                        <asp:RadioButton ID="USA" runat="server" GroupName="Country" Text="USA" Checked="true" /><br />
                        <asp:RadioButton ID="CAN" runat="server" GroupName="Country" Text="Canada" /><br />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="Label">Effective Date:</td>
                    <td class="Data" style="vertical-align: middle;">
                        <asp:TextBox ID="EffectiveDT" runat="server" Width="65px"></asp:TextBox>
                        <a href="Javascript:showCalendarControl(document.getElementById('EffectiveDT'));void(0);">
                            <img alt="Calendar" src="../../../../images/calendar_icon.gif" onmouseover="style.cursor='hand';"
                                border="0" style="vertical-align: top; padding-top: 1px; padding-bottom: 1px;" /></a>
                    </td>
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
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td width="25%">
                                    <asp:Button ID="Submit" runat="server" Text="Submit" CssClass="button" OnClientClick="return ValidatePage();"
                                        OnClick="Submit_Click"></asp:Button>&nbsp;
                                </td>
                                <td width="25%">
                                    <input type="button" value="Cancel" name="Cancel" class="button" onclick="javascript: window.location = 'rptRSMBonusExtract.aspx?'" />&nbsp;
                                </td>
                                <td width="25%">&nbsp;
                                </td>
                                <td width="25%">&nbsp;
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
        <br />
        <br />
    </form>
</body>
</html>