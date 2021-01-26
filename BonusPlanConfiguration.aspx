<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BonusPlanConfiguration.aspx.cs" Inherits="BonusPlan.BonusPlanConfiguration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!--#include file="include/hdrMain.html"-->

    <%--Bonus Plan Configuration Component--%>
    <script src="js/component/BonusPlanConfiguration.js"></script>

    <style>
        table.tablesorter thead tr .headerSort {
            white-space: normal;
        }
    </style>

    <script>

        $(document).ready(function ()
        {
            var configurationPanel = new BonusPlanConfiguration("BonusPlanConfigurationContainer");
            configurationPanel.BonusPlanConfigurationGet();
        });
    </script>
</head>

<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="BonusPlanHiddenID" runat="server" />
        <asp:HiddenField ID="LedgerEntryHiddenID" runat="server" />
        <div class="navbar navbar-default navbar-static" role="navigation">
            <asp:Literal ID="SecurityMenu" runat="server" EnableViewState="false"></asp:Literal>
        </div>

        <div class="container">
            <div class="col-lg-1"></div>
            <div class="col-lg-10">

                <asp:Literal ID="PageMessage" runat="server"></asp:Literal>
                <h1>
                    <asp:Literal ID="PageTitle" runat="server" EnableViewState="false"></asp:Literal></h1>
                <p>
                    <asp:Literal ID="PageBody" runat="server" EnableViewState="false"></asp:Literal>
                </p>
                <br />
                <div id="BonusPlanConfigurationContainer"></div>
            </div>
            <div class="col-lg-1"></div>
        </div>
    </form>
    <!--#include file="include/GoogleAnalytic.html"-->
</body>
</html>