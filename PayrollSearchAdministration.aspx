<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayrollSearchAdministration.aspx.cs" Inherits="BonusPlan.PayrollSearchAdministration" %>

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

    <!--jquery ui-->
    <link rel="stylesheet" href="../../../../../lib/style/jquery-ui.css">
    <script src="../../../../../lib/jquery.ui.core.js"></script>
    <script src="../../../../../lib/jquery.ui.widget.js"></script>
    <script src="../../../../../lib/jquery.ui.position.js"></script>
    <script src="../../../../../lib/jquery.ui.menu.js"></script>
    <script src="../../../../../lib/jquery.ui.autocomplete.js"></script>

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {

            $("#PayrollSearchGrid").tablesorter({
                debug: false,
                headers: { 0: { sorter: false} }
            });

            var url = function (request, response) {
                //object(s) to pass to web method
                var employeeSearch = new Object();
                employeeSearch.ADPEmpID = $("#" + this.element.attr("id")).val();

                //web method url
                var ajaxMethodURL = location.protocol + "//" + location.host + "/Application/BonusPlan/AJAXData/BonusPlanAJAX.svc/BonusPlanEmployeeSearch";

                //ajax call to web method which passes the object created above
                $.ajax({
                    type: "POST",
                    url: ajaxMethodURL,
                    data: JSON.stringify(employeeSearch),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        response($.map(msg, function (item) {
                            return {
                                label: item.Employee,
                                key: item.EmployeeID
                            }
                        }))
                    } //,
                    //                        error: function (response, status, errorMsg) {
                    //                            alert(response.responseText);
                    //                        }
                });
            }

            //on document ready initalize the autocomplete on the resolution team textbox
            $("#EmployeeID").autocomplete({
                source: url,
                minLength: 3,
                delay: 0,
                select: function (event, ui) {
                    $("#EmployeeHiddenID").val(ui.item.key);
                    $("#EmployeeID").val('');
                    //return false; //keeps value from being inserted into auto complete field
                },
                change: function (event, ui) { //clears data if no user was found
                    var data = $.data(this);
                    if (ui.item == null) {
                        $("#EmployeeHiddenID").val('');
                        $("#EmployeeName").html('');
                    }
                    //if (data.autocomplete.selectedItem == undefined) {
                    //    $("#EmployeeHiddenID").val('');
                    //    $("#EmployeeName").html('');
                    //}
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
                        <legend>Payroll Search</legend>
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="Label">ADP Employee ID:
                                </td>
                                <td class="Data">
                                    <asp:TextBox runat="server" ID="EmployeeID" Width="200px"></asp:TextBox>
                                    <asp:HiddenField ID="EmployeeHiddenID" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="Label">Payroll Date:
                                </td>
                                <td class="Data">
                                    <asp:DropDownList ID="PaymentDt" runat="server"></asp:DropDownList>
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
                                    <asp:Button runat="server" ID="SearchSubmit" Text="Search" CssClass="button"
                                        OnClick="SearchSubmit_Click" />
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
        <asp:Literal ID="PaymentSearchCriteriaGrid" EnableViewState="false" runat="server"></asp:Literal>
        <br />
        <table style="width: 750px; margin-left: 7px;" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td>
                    <asp:Literal ID="PayrollSearchGrid" runat="server" EnableViewState="false"></asp:Literal>
                </td>
            </tr>
        </table>
    </form>
    <!--#include file="include/GoogleAnalytic.html"-->
</body>
</html>