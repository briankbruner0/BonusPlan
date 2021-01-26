<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentSearch.aspx.cs" Inherits="BonusPlan.PaymentSearch" EnableEventValidation="false" %>
<%-- Emmanuel.Ruvalcaba - 12/13/2017 - Disabled Event Validation to prevent errors for invalid postbacks or callback arguments --%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!--#include file="include/hdrMain.html"-->

    <link href="css/BonusPlan.css" rel="stylesheet" type="text/css" />

    <!-- 11/14/2017 Fritz Kern - Moved all the javascript into a seperate file. -->
    <script src="js/PaymentSearch.js" type="text/javascript"></script>
    
    <!-- 11/14/2017 Fritz Kern - Moved all the css into a seperate file. -->
    <link href="css/PaymentSearch.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="CommunityID" runat="server" />
        <asp:HiddenField ID="Username" runat="server" />

        <div class="navbar navbar-default navbar-static" role="navigation">
            <asp:Literal ID="SecurityMenu" EnableViewState="false" runat="server"></asp:Literal>
        </div>        

        <div class="container">
            <div class="col-lg-10">

                <div>
                    <asp:Literal ID="SecurityBody" runat="server" EnableViewState="false"></asp:Literal>
                </div>  

                <h1>
                    <asp:Literal ID="SecurityTitle" runat="server" EnableViewState="false"></asp:Literal>
                </h1>

                <div class="panel panel-default">
                    <div class="panel-heading">
                        Payment Search
                    </div>

                    <div class="panel-body">
                        
                        <div class="row">
                            <div class="col-md-10">
                                <asp:Label ID="ErrorMessage" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <label>Employee First Name:</label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox runat="server" ID="EmployeeFirstName" CssClass="form-control" placeholder="Employee First Name"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <label>Employee Last Name:</label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="EmployeeLastName" CssClass="form-control" placeholder="Employee Last Name" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <label>Employee ID:</label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="EmployeeID" CssClass="form-control" placeholder="Employee ID" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <label>Customer ID:</label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="CustomerID" CssClass="form-control" placeholder="Customer ID" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <label>Resident First Name:</label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="ResidentFirstName" CssClass="form-control" placeholder="Resident First Name" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <label>Resident Last Name:</label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="ResidentLastName" class="form-control" placeholder="Resident Last Name" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <label>Country:</label>
                            </div>
                            <div class="col-md-3">
                                <select name="Country" id="Country" class="form-control" runat="server">
                                    <option value="">Select All...</option>
                                    <option value="USA">USA</option>
                                    <option value="CAN">CAN</option>
                                </select>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <label>Community:</label>
                            </div>
                            <div class="col-md-8">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div>
                                            Available
                                        </div>
                                        <asp:ListBox ID="uiUnAssignedCommunityNumber" runat="server" Height="150px" Width="100%" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                    </div>
                                    <div class="col-md-6">
                                        <div>
                                            Selected
                                        </div>
                                        <asp:ListBox ID="uiAssignedCommunityNumber" runat="server" Height="150px" Width="100%" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12" style="text-align: center;">
                                        <a href="javascript: MoveAll('uiAssignedCommunityNumber', 'uiUnAssignedCommunityNumber');">
                                            <img src="../../../../images/iconMoveLeftAll.gif" border="0" alt="Move All"></a>
                                        <a href="javascript: MoveItem('uiAssignedCommunityNumber', 'uiUnAssignedCommunityNumber');">
                                            <img src="../../../../images/iconMoveLeft.gif" border="0" alt="Move One"></a>

                                        <a href="javascript: MoveItem('uiUnAssignedCommunityNumber', 'uiAssignedCommunityNumber');">
                                            <img src="../../../../images/iconMoveRight.gif" border="0" alt="Move One"></a>
                                        <a href="javascript: MoveAll('uiUnAssignedCommunityNumber', 'uiAssignedCommunityNumber');">
                                            <img src="../../../../images/iconMoveRightAll.gif" border="0" alt="Move All"></a>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <label>Payment Date:</label>
                            </div>
                            <div class="col-md-8">
                                <div class="row">
                                    <div class="col-md-4 col-xs-4">
                                        <div>
                                            From:
                                        </div>
                                        <div class="input-group date">
                                            <asp:TextBox ID="PaymentFromDT" CssClass="form-control" runat="server" helpMessage="Date the report starts"></asp:TextBox>
                                            <span class="input-group-addon "><i class="fa fa-calendar" aria-hidden="true"></i></span>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-xs-4">
                                        <div>
                                            To:
                                        </div>
                                        <div class="input-group date">
                                            <asp:TextBox ID="PaymentToDT" CssClass="form-control" runat="server" helpMessage="Date the reports ends"></asp:TextBox>
                                            <span class="input-group-addon "><i class="fa fa-calendar" aria-hidden="true"></i></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <label>Payroll Date:</label>
                            </div>
                            <div class="col-md-8">
                                <div class="row">
                                    <div class="col-md-4 col-xs-4">
                                        <div>
                                            From:
                                        </div>
                                        <div class="input-group date">
                                            <asp:TextBox ID="PayrollFromDT" CssClass="form-control" runat="server" helpMessage="Date the report starts"></asp:TextBox>
                                            <span class="input-group-addon "><i class="fa fa-calendar" aria-hidden="true"></i></span>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-xs-4">
                                        <div>
                                            To:
                                        </div>
                                        <div class="input-group date">
                                            <asp:TextBox ID="PayrollToDT" CssClass="form-control" runat="server" helpMessage="Date the reports ends"></asp:TextBox>
                                            <span class="input-group-addon "><i class="fa fa-calendar" aria-hidden="true"></i></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <label>Move In Date:</label>
                            </div>
                            <div class="col-md-8">
                                <div class="row">
                                    <div class="col-md-4 col-xs-4">
                                        <div>
                                            From:
                                        </div>
                                        <div class="input-group date">
                                            <asp:TextBox ID="MoveInFromDT" CssClass="form-control" runat="server" helpMessage="Date the report starts"></asp:TextBox>
                                            <span class="input-group-addon "><i class="fa fa-calendar" aria-hidden="true"></i></span>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-xs-4">
                                        <div>
                                            To:
                                        </div>
                                        <div class="input-group date">
                                            <asp:TextBox ID="MoveInToDT" CssClass="form-control" runat="server" helpMessage="Date the reports ends"></asp:TextBox>
                                            <span class="input-group-addon "><i class="fa fa-calendar" aria-hidden="true"></i></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <label>Output To:</label>
                            </div>
                            <div class="col-md-7">
                                <div class="btn-group" data-toggle="buttons">
                                    <span class="btn btn-xs active btn-default export">
                                        <asp:RadioButton ID="Screen" runat="server" GroupName="Export" Text="Screen" Checked="true" />
                                    </span>

                                    <span class="btn btn-xs btn-default export">
                                        <asp:RadioButton ID="Excel" runat="server" GroupName="Export" Text="Excel" />
                                    </span>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                &nbsp;
                            </div>
                            <div class="col-md-7">
                                <asp:Button runat="server" ID="Submit" Text="Submit" CssClass="btn btn-primary" OnClick="Submit_Click" OnClientClick="return ValidatePage();" />
                                <asp:Button ID="Cancel" runat="server" CssClass="btn btn-default" Text="Cancel" OnClick="Cancel_Click" />
                            </div>
                        </div>

                    </div>
                </div>

            </div>
        </div>
    </form>

    <div class="container">
        <div class="col-lg-12">
            <asp:Literal ID="PaymentSearchGrid" runat="server" EnableViewState="False"></asp:Literal>
        </div>
    </div>


    <!-- Start Edit Payment Modal -->
    <div id="PaymentEditModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="OutputModalClose" class="close" data-dismiss="modal">&times;</button>
                    <h4>Edit Payment</h4>
                </div>

                <div class="modal-body">
                    <div id="PaymentEditModalBody">
                        <input type="hidden" id="PaymentID" />
                        <div class="row">
                            <div class="col-md-3">
                                <label>Payment Amount:</label>
                            </div>

                            <div class="col-md-5">
                                <div class="input-group">
                                    <span class="input-group-addon">$</span>
                                    <input type="text" id="PaymentAmount" placeholder="Payment Amount" class="form-control" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <label>Processed Date:</label>
                            </div>

                            <div class="col-md-5">
                                <div class="input-group date">
                                    <input type="text" id="PaymentProcessedDate" placeholder="Processed Date" class="form-control" />
                                    <span class="input-group-addon"><i class="fa fa-calendar" aria-hidden="true"></i></span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="modal-footer">
                    <input type="button" id="PaymentEditCancel" data-dismiss="modal" class="btn btn-default pull-right btn-sm" style="margin: 3px;" value="Cancel" />
                    <input type="button" id="PaymentEditSubmit" class="btn btn-primary pull-right btn-sm" style="margin: 3px;" value="Submit" />
                    <div id="loading" class="pull-right hidden" style="margin: 3px;">
                        <i class="fa fa-spinner fa-2x fa-spin" aria-hidden="true"></i>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- End Edit Payment Modal -->
    
    <!-- Start Overlay -->
    <div class="overlay" id="FilterOverlay" style="display: none;">
		<div class="Generate">
			<i class="fa-white fa fa-refresh fa-5x fa-spin" aria-hidden="true"></i>
		</div>
		<h3 class="GenerateMessage">
            Please wait while your data is being retrieved.
		</h3>
	</div>
    <!-- End Overlay -->

    <div style="height:100px;"></div>

    <!--#include file="include/GoogleAnalytic.html"-->
</body>
</html>