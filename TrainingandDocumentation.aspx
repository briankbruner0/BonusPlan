<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrainingandDocumentation.aspx.cs" Inherits="BonusPlan.TrainingandDocumentation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>
        <%=objSecurity.Title%>
    </title>
    
    <link href="../../../lib/style/Portal.css" type="text/css" rel="STYLESHEET" />    
    <link href="../../../lib/style/simpleModal.css" type="text/css" rel="STYLESHEET" />
    <link href="../../../lib/style/CalendarControl.css" type="text/css" rel="StyleSheet">
    <script src="../../../../../../lib/CalendarControl.js" type="text/javascript"></script>
    <script src="../../../lib/jquery-min.js" type="text/javascript"></script>
    <script src="../../../../../lib/jquery.tablesorter.js" type="text/javascript"></script>
    <script src="../../../../lib/jquery.simplemodal.js" type="text/javascript"></script>
    <script src="../../../lib/ControlValidator.js" type="text/javascript"></script>   
</head>
<body>
    <form id="form1" runat="server">
    <asp:Literal ID="SecurityMenu" EnableViewState="false" runat="server"></asp:Literal>
        <table style="width: 750px; margin-left: 7px;" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td class="Atria_Title">
                    <asp:Literal ID="Title" runat="server" EnableViewState="false"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Literal ID="Body" runat="server" EnableViewState="false"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Literal ID="TrainingPathGrid" runat="server" EnableViewState="false"></asp:Literal>
                </td>
            </tr>
        </table>
    </form>
	<!--#include file="include/GoogleAnalytic.html"-->
</body>
</html>
