<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MoveInRevenueStatusProfile.aspx.cs" Inherits="BonusPlan.MoveInRevenueStatusProfile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <%=objSecurity.Title%>
    </title>

    <link href="../../../../../lib/style/Portal.css" type="text/css" rel="Stylesheet" />

    <script type="text/javascript" src="../../../lib/jquery-min.js"></script>
    <script type="text/javascript" src="../../../lib/JSON2.js"></script>
    <script src="../../../../lib/jquery.tablesorter.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {

            $("#MoveInRevenueStatusGrid").tablesorter({
                debug: false,
                headers: { 0: { sorter: false } }
            });
        });

        function ValidatePage() {
            var error = "";

            if ($("#MoveInRevenueStatus").val.length() == 0) {
                error += "A move in revenue status is required.\n";
            }

            if ($("#Sort").val.length() == 0) {
                error += "A sort is required.\n";
            }

            var integerRegEx = /^\d*$/;
            if (!integerRegEx.test($("#Sort").val()) && $("#Sort").val().length > 0) {
                errors += "Please provide a valid Sort Value.\n";
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
        <asp:HiddenField ID="MoveInRevenueStatusHiddenID" runat="server" />
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
                        <legend>Move In Revenue Status</legend>
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td class="Label">ID:
                                </td>
                                <td>
                                    <asp:Literal ID="MoveInRevenueStatusID" runat="server" EnableViewState="false"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td class="LabelRequired">Revenue Status:
                                </td>
                                <td>
                                    <asp:TextBox ID="MoveInRevenueStatus" Width="200px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="LabelRequired">Sort:
                                </td>
                                <td>
                                    <asp:TextBox ID="Sort" Width="50px" MaxLength="4" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Create By:
                                </td>
                                <td>
                                    <asp:Literal ID="CreateBy" runat="server" EnableViewState="false"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td>Create Date:
                                </td>
                                <td>
                                    <asp:Literal ID="CreateDt" runat="server" EnableViewState="false"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td>Modify By:
                                </td>
                                <td>
                                    <asp:Literal ID="ModifyBy" runat="server" EnableViewState="false"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td>Modify Date:
                                </td>
                                <td>
                                    <asp:Literal ID="ModifyDt" runat="server" EnableViewState="false"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td>Active:
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
                    <asp:Literal ID="MoveInRevenueStatusGrid" runat="server" EnableViewState="false"></asp:Literal>
                </td>
            </tr>
        </table>
    </form>
    <!--#include file="include/GoogleAnalytic.html"-->
</body>
</html>