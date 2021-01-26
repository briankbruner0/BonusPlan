//  Approval Panel
//  A component which displays the current approvals
function DashboardApprovalPanel(ContainerID){
	
	var self = this;
	this.ContainerID = ContainerID; // ID of container to render menu bar
	this.CommunityNumber = null;
	this.OperationClusterID = null;
	this.EffectiveDT = null;
	this.Username = null;
	this.BonusPlanID = null;
	
	this.CommunityList = null; // Populated by data provided by BonusPlanAJAX.svc
	this.ApprovalList = null;  // Populated by data provided by BonusPlanAJAX.svc
	this.ApprovalDetailList = null;	 // Populated by data provided by BonusPlanAJAX.svc
	
	// Gets the data necessary for the approvals
	this.ApprovalByUsernameGet = function(Username)
	{
		// Update attributes
		this.Username = Username;
		
		// Load spinner
		$("#" + this.ContainerID + " > .panel > .panel-body").html("<img src='../../../../images/progressSnapIn.gif' alt='Loading' />&nbsp;Please wait downloading...");
		
		// Set up AJAX request
		var objRevenue = new Object();
		objRevenue.Username = this.Username;
	
		var ajaxMethodURL = location.protocol + '//' + location.host + '/Application/BonusPlan/AJAXData/BonusPlanAJAX.svc/ApprovalByUsernameGet';

		var approval = $.ajax({
			type: 'POST',
			url: ajaxMethodURL,
			cache: false,
			data: JSON.stringify(objRevenue),
			contentType: 'application/json; charset=utf-8',
			dataType: 'json',
			success: function(msg){
				try
				{
					var response = JSON.parse(msg);
					this.ApprovalList = response.Approval;
					this.CommunityList = response.Community;
					this.Render();
				}
				catch(err){}
			}.bind(self),
			error: function (XMLHttpRequest, textStatus, errorThrown)
			{
				alert('Error :' + XMLHttpRequest.responseText);
			}
		});
		
		// Require the detail list too
		var approvalDetail = this.ApprovalByUsernameDetailGet();
		
		$.when(approval, approvalDetail).done( function(){
				this.Render();
			}.bind(self) )
	}
	
	// Gets the approval detail data
	this.ApprovalByUsernameDetailGet = function()
	{	
		// Set up AJAX request
		var objDashboard = new Object();
		objDashboard.Username = this.Username;
	
		var ajaxMethodURL = location.protocol + '//' + location.host + '/Application/BonusPlan/AJAXData/BonusPlanAJAX.svc/ApprovalByUsernameDetailGet';

		return $.ajax({
			type: 'POST',
			url: ajaxMethodURL,
			cache: false,
			data: JSON.stringify(objDashboard),
			contentType: 'application/json; charset=utf-8',
			dataType: 'json',
			success: function(msg){
				try
				{
					this.ApprovalDetailList = JSON.parse(msg);
				}
				catch(err){}
			}.bind(self),
			error: function (XMLHttpRequest, textStatus, errorThrown)
			{
				alert('Error :' + XMLHttpRequest.responseText);
			}
		});
	}
	
	// Approves a payment
	window.ApprovePayment = function()
	{	
		// Set up AJAX request
		var objPayment = new Object();
        objPayment.PaymentToApprovalID = $("#ApprovalDetailModalPaymentToApprovalID").val();
        objPayment.CommunityNumber = $("#CommunityNumber").val();
		objPayment.UserName = $("#Username").val();
	
		var ajaxMethodURL = location.protocol + '//' + location.host + '/Application/BonusPlan/AJAXData/Approval.aspx/ApprovePayment';

		return $.ajax({
			type: 'POST',
			url: ajaxMethodURL,
			cache: false,
			data: JSON.stringify(objPayment),
			contentType: 'application/json; charset=utf-8',
			dataType: 'json',
			success: function(msg){
				try
				{
					$("#ApprovalDetailModal").modal('hide');
					this.ApprovalByUsernameGet(this.Username);
				}
				catch(err){}
			}.bind(self),
			error: function (XMLHttpRequest, textStatus, errorThrown)
			{
				alert('Error :' + XMLHttpRequest.responseText);
			}
		});
	}.bind(self)

	// Creates an exception for the approval
	window.CreateException = function()
	{
		if ($("#ApprovalExceptionNote").val().length > 0) {
			if (confirm('Are you sure you want to Create an Exception for this Payment?')) {
				//ajax exception note and exception flag
				var paymentException = new Object();
				paymentException.ExceptionNote = $("#ApprovalExceptionNote").val();
				paymentException.PaymentToApprovalID = $("#ApprovalDetailModalPaymentToApprovalID").val();;
				paymentException.CommunityNumber = $("#CommunityNumber").val();
				paymentException.UserName = $("#Username").val();

				var ajaxMethodURL = location.protocol + "//" + location.host + "/Application/BonusPlan/AJAXData/Approval.aspx/CreateException";

				$.ajax({
					type: "POST",
					url: ajaxMethodURL,
					data: JSON.stringify(paymentException),
					contentType: "application/json; charset=utf-8",
					dataType: "json",
					success: function (msg) {
						$("#ApprovalDetailModal").modal('hide');
						this.ApprovalByUsernameGet(this.Username);

						ResetModal();

					}.bind(self)
				});

			}
			else {
				return false;
			}
		}
		else {
			alert("Please provide a reason for the exception.\n");
		}
	}.bind(self)

	window.ResetModal = function () { 	    
	    $('#ApprovalExceptionControl').css("display", "none");
	    $('#ApprovalApproveControl').css("display", "inline");
	
	    $("#ApprovalExceptionNote").val("");
	}.bind(self);
	
	// Loads the approval detail modal
	window.ApprovalDetailModalOpen = function(ApprovalGUID)
	{
		$("#ApprovalDetailModal").modal('show');

		// Get the approval object
		var Approval = null;
		this.ApprovalList.forEach(function(approval){
			if (approval.ApprovalGUID == ApprovalGUID)
			{
				Approval = approval;
				this.ModalDataToFormBind(Approval);
			}
		}.bind(self));
		
	}.bind(self)
	
	// Begin the approval exception path
	window.ApprovalExceptionOpen = function(ApprovalGUID)
	{
		$("#ApprovalDetailModal").modal('show');

		// Get the approval object
		var Approval = null;
		this.ApprovalList.forEach(function(approval){
			if (approval.ApprovalGUID == ApprovalGUID)
			{
				Approval = approval;
				this.ModalDataToFormBind(Approval);
			}
		}.bind(self));	
	}.bind(self)
	
	// Binds approval Detail to Modal Window
	this.ModalDataToFormBind = function(Approval)
	{
		if (!Approval)
		{
			return;
		}		

		// Bind header information
		$("#ApprovalDetailModalPaymentToApprovalID").val(Approval.PaymentToApprovalID);
		$("#ApprovalDetailModalEmployeeName").html(Approval.UserName);
		$("#ApprovalDetailModalAmount").html(Approval.Amount);
		$("#ApprovalDetailModalApprover").html(Approval.Approver);
		$("#ApprovalDetailModalEmployeeID").html(Approval.EmployeeID);
		$("#ApprovalDetailModalCommunityName").html(Approval.CommunityName);

		// Generate approval detail table body
		var html = "";
		
		this.ApprovalDetailList.forEach(function(detail){
			
			if (detail.ApprovalGUID == Approval.ApprovalGUID)
			{
				html += "<tr>";
				html += "	<td class='characterShort'>" + detail.CustomerID + "</td>";
				html += "	<td class='characterShort'>" + detail.Resident + "</td>";
				html += "	<td class='dateShort'>" + detail.MoveInDt + "</td>";
				html += "	<td class='characterShort'>" + detail.Note + "</td>";
				html += "	<td class='numericShort'>" + detail.Amount + "</td>";
				html += "</tr>";
			}
		});
		
		$("#ApprovalDetailModalTable>tbody").html(html);
		
		// Generate revenue detail table body footer
		var footerHtml = "";
		
		footerHtml += "<tr>";
		footerHtml += "	<td class='numericShort modalFooter' colspan='5'>" + Approval.Amount + "</td>";
		footerHtml += "</tr>"; 
		
		$("#ApprovalDetailModalTable>tfoot").html(footerHtml);
	}
	
	// Renders the Approval Panel HTML at ContainerID
	this.Render = function()
	{
		// Don't render if the data isn't loaded
		if (this.ApprovalList === null)
		{ return;}
		
	    // If there are no Approvals, don't show the panel at all
		if (this.ApprovalList.length == 0)
		{
			$("#" + this.ContainerID).html("");
			return;
		}
		
		var html = "";
		var CommunityID = "";
		
		html += "<div class='panel panel-default'>\n";
		html += "	<div class='panel-heading'>Approvals</div>\n";
		html += "		<div class='panel-body'>\n";
		
		if (this.ApprovalList.length == 0)
		{
			html += "No approvals.";
		}
		else
		{
			this.CommunityList.forEach(function(Community)
			{
				CommunityID = Community.CommunityID;
								
				html += "		<table class='table table-striped table-condensed tablesorter'>\n";
				html += "			<thead>\n";
				html += "				<tr>\n";
				html += "					<th colspan='3' class='characterLong'>" + Community.CommunityName + "</td>\n";
				html += "				</tr>\n";	
				html += "			</thead>\n";
				html += "			<tbody>\n";
				
				this.ApprovalList.forEach(function(approval)
				{		
					if (approval.CommunityID == CommunityID)
					{
						html += "<tr>\n";
						html += "	<td class='characterShort'>" + approval.UserName.replace(/\./g, '.&#x200b;') + "</td>\n";  // Use a zero width space at the dot in the username to enable word break
						html += "	<td class='numericShort'>" + approval.Amount + "</td>\n";
						html += "	<td class='iconLong'><input class='btn btn-primary btn-xs' type='button' value='View' onClick=\"ApprovalDetailModalOpen('" + approval.ApprovalGUID + "')\"></td>\n";
						html += "</tr>\n";	
					}	
				}, this);
				
				html += "			</tbody>\n";
				html += "		</table>\n";
			}, this);
		}
		
		html += "		</div>\n";
		html += "	</div>\n";
		html += "</div>\n";

		// Set the container with the generated HTML
		$("#" + this.ContainerID).html(html);
	}		
}