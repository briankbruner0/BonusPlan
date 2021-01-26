<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApprovalAdministration.aspx.cs" Inherits="BonusPlan.ApprovalAdministration" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <%=objSecurity.Title%>
    </title>

    <link href="../../../../lib/style/Portal.css" type="text/css" rel="Stylesheet" />

    <script type="text/javascript" src="../../../../lib/jquery-min.js"></script>
    <script src="../../../../lib/jquery.tablesorter.js" type="text/javascript"></script>

    <script src="../../../../lib/jquery.textarearesizer.compressed.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $("#ApprovalGUIDGrid").tablesorter({
                debug: false
            });

            $("#ApprovalWorkflowGrid").tablesorter({
                debug: false
            });
            /***** Plugin Initialization *****/
            $('textarea.resizable:not(.processed)').TextAreaResizer();

        });

        function ValidatePage() {
            var errors = "";

            if ($("#JobCategoryID").val().length == 0) {
                errors += "A Job Role is required.\n";
            }

            if ($("#Sort").val().length == 0) {
                errors += "A Sort value is required.\n";
            }

//            if ($("#CriteriaValue").val().length == 0) {
//                errors += "A Criteria Value is required.\n";
//            }

            if ($("#AdministrationNote").val().length == 0) {
                errors += "An Override Note value is required.\n";
            }

            if (errors.length > 0) {
                alert(errors);
                return false;
            }
            else {
                return true;
            }

        }
    </script>
    <style type="text/css">
        .button {
            height: 26px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="PaymentHiddenID" runat="server" />
        <asp:HiddenField ID="PaymentToApprovalHiddenID" runat="server" />
        <asp:HiddenField ID="CommunityNumberHidden" runat="server" />
        <asp:HiddenField ID="BonusPlanHiddenID" runat="server" />
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
        <table style="width: 750px;" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td>
                    <asp:Literal ID="AdministrationInformationBlock" runat="server" EnableViewState="false"></asp:Literal>
                </td>
            </tr>
        </table>
        <table style="width: 750px; margin-left: 7px;" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td>
                    <fieldset>
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="Label">ID:
                                </td>
                                <td class="Data">
                                    <asp:Literal ID="PaymentToApprovalID" runat="server" EnableViewState="false"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td class="LabelRequired">Job Role:
                                </td>
                                <td class="Data">
                                    <asp:DropDownList ID="JobCategoryID" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="Label">Job Role Alternate:
                                </td>
                                <td class="Data">
                                    <asp:DropDownList ID="JobCategoryIDAlternate" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="Label">User Alternate:
                                </td>
                                <td class="Data">
                                    <asp:TextBox ID="UsernameAlternate" Width="216px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="LabelRequired">Sort:
                                </td>
                                <td class="Data">
                                    <asp:TextBox ID="Sort" MaxLength="5" Width="50px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <%-- <tr>
                            <td class="LabelRequired">
                               Criteria Value:
                            </td>
                            <td class="Data">
                                <asp:Textbox ID="CriteriaValue" MaxLength="5" Width="50px" runat="server"></asp:Textbox>
                            </td>
                        </tr>--%>
                            <tr>
                                <td class="Label">Approve:
                                </td>
                                <td class="Data">
                                    <asp:CheckBox ID="ApproveFlg" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="Label">Deny:
                                </td>
                                <td class="Data">
                                    <asp:CheckBox ID="DenyFlg" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="Label">Exception:
                                </td>
                                <td class="Data">
                                    <asp:CheckBox ID="ExceptionFlgCB" runat="server" />
                                    <asp:Literal ID="Exception" runat="server" EnableViewState="false"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td class="Label">Exception Note:
                                </td>
                                <td class="Data">
                                    <asp:TextBox ID="ExceptionNote" runat="server" CssClass="resizable" TextMode="MultiLine" Width="215px" Rows="3"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="LabelRequired">Override:
                                </td>
                                <td class="Data">
                                    <asp:Literal ID="OverrideFlg" runat="server" EnableViewState="false"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td class="LabelRequired">Override Note:
                                </td>
                                <td class="Data">
                                    <asp:TextBox ID="AdministrationNote" TextMode="MultiLine" CssClass="resizable" Width="215px" Rows="3" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <%--<tr>
                            <td class="Label">
                               Attachment:
                            </td>
                            <td class="Data">
                                <asp:FileUpload ID="AttachmentID" runat="server" />
                            </td>
                        </tr>--%>
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
                                <td colspan="2">
                                    <asp:Button runat="server" ID="Submit" Text="Submit" CssClass="button"
                                        OnClientClick="return ValidatePage();" OnClick="Submit_Click" />
                                    <asp:Button runat="server" ID="Cancel" Text="Cancel" CssClass="button"
                                        OnClick="Cancel_Click" />
                                    <asp:Button runat="server" ID="Delete" Text="Delete" CssClass="button" Visible="false"
                                        OnClick="Delete_Click" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px; margin-left: 7px;">
            <tr>
                <td>
                    <asp:Literal ID="ApprovalManagementGrid" runat="server" EnableViewState="false"></asp:Literal>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <br />
    </form>
    <!--#include file="include/GoogleAnalytic.html"-->
</body>
</html>