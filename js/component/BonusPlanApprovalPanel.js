//  Bonus Plan Approval Panel
//  Used for editing and viewing approvals
function BonusPlanApprovalPanel(ContainerID){
	
	var self = this;
	this.ContainerID = ContainerID; // ID of container to render menu bar
	this.BonusPlanID = null;
	this.ApproverList = null;
		
	// Gets the approval entries for this Bonus Plan
	this.BonusPlanToApprovalWorkflowGet = function(BonusPlanID)
	{
		this.BonusPlanID = BonusPlanID;
		
		var objBonusPlan = new Object();
		objBonusPlan.BonusPlanID = this.BonusPlanID;
		
		// Set up AJAX request
		var objBonusPlan = new Object();
		objBonusPlan.BonusPlanID = BonusPlanID;
	
		var ajaxMethodURL = location.protocol + '//' + location.host + '/Application/BonusPlan/AJAXData/BonusPlanAJAX.svc/BonusPlanToApprovalWorkflowGet';

		$.ajax({
			type: 'POST',
			url: ajaxMethodURL,
			cache: false,
			data: JSON.stringify(objBonusPlan),
			contentType: 'application/json; charset=utf-8',
			dataType: 'json',
			beforeSend: this.TableSpinnerGet(),
			success: function(msg){
				try
				{
					this.ApproverList = JSON.parse(msg);
					this.RenderTable();
				}
				catch(err){}
			}.bind(self),
			error: function (XMLHttpRequest, textStatus, errorThrown)
			{
				alert('Error :' + XMLHttpRequest.responseText);
			}
		});
	}
	
	// Inserts an approver into the configuration and returns an updated list of configured approvers
    this.ApproverManagementInsert = function()
	{
		var objBonusPlan = new Object();
		objBonusPlan.BonusPlanID = this.BonusPlanID;
		objBonusPlan.JobCategoryID = $("#JobCategoryID").val();
		objBonusPlan.ApprovalSort = $("#ApprovalSort").val();
		objBonusPlan.ApprovalAmount = $("#ApprovalAmount").val();
		objBonusPlan.Username = $("#Username").val();

		var ajaxMethodURL = location.protocol + '//' + location.host + '/Application/BonusPlan/AJAXData/BonusPlanAJAX.svc/ApproverManagementInsert';

		$.ajax({
			type: 'POST',
			url: ajaxMethodURL,
			cache: false,
			data: JSON.stringify(objBonusPlan),
			contentType: 'application/json; charset=utf-8',
			dataType: 'json',
			beforeSend: this.TableSpinnerGet(),
			success: function (msg)
			{
				this.ApproverList = JSON.parse(msg);
				this.RenderTable();
			}.bind(self),
			error: function (XMLHttpRequest, textStatus, errorThrown)
			{
				alert('Error :' + XMLHttpRequest.responseText);
			}
		});
	}
	
	// Loads a spinner into the table
	this.TableSpinnerGet = function()
	{
		$("#ApprovalWorkflowGrid>tbody").html("<tr><td colspan='8'><img src='/images/progressSnapIn.gif' alt='Loading'/>&nbsp;Please wait downloading...</td></tr>");
	}
	
	// Deletes an approver entry from the configuration
	window.ApproverDelete = function(ApproverManagementID, JobCategory)
	{
		if (confirm("Are you sure you want to delete " + JobCategory + "?"))
		{
			this.ApproverManagementDelete(ApproverManagementID);
		}
	}.bind(self)
		
	// Deletes an approval entry from the configuration and returns an updated list of approvals
    this.ApproverManagementDelete = function(ApproverManagementID)
	{
		var objBonusPlan = new Object();
		objBonusPlan.BonusPlanID = this.BonusPlanID;
		objBonusPlan.ApproverManagementID = ApproverManagementID;

		var ajaxMethodURL = location.protocol + '//' + location.host + '/Application/BonusPlan/AJAXData/BonusPlanAJAX.svc/ApproverManagementDelete';

		$.ajax({
			type: 'POST',
			url: ajaxMethodURL,
			cache: false,
			data: JSON.stringify(objBonusPlan),
			contentType: 'application/json; charset=utf-8',
			dataType: 'json',
			beforeSend: this.TableSpinnerGet(),
			success: function (msg)
			{
				this.ApproverList = JSON.parse(msg);
				this.RenderTable();
			}.bind(self),
			error: function (XMLHttpRequest, textStatus, errorThrown)
			{
				alert('Error :' + XMLHttpRequest.responseText);
			}
		});
	}
	
	// Validates approver and proceeds with insertion if valid
	window.ApproverValidate = function()
        {
            var error = "";

			if ($("#JobCategoryID").val() == "")
			{
				error += "Please select a Job Category.\n";
			}

            if (error.length > 0)
            {
                alert(error);
                return false;
            }
            else
            {
                this.ApproverManagementInsert();
            }
        }.bind(self)
	
	// Renders the Bonus Plan to Job Code HTML at ContainerID
	this.Render = function()
	{
		
	}

	// Renders the table data at #ApprovalWorkflowGrid
	this.RenderTable = function()
	{
		var html = "";
		
		if (this.ApproverList.length == 0)
		{
			html += "<tr>\n";
			html += "    <td colspan='5'>No data to display.</td>\n";
			html += "</tr>\n";
		}
		else
		{
			this.ApproverList.forEach(function(approval){
				
				html += "<tr>\n";
				html += "	<td>" + approval.ApproverManagementID + "</td>\n";
				html += "	<td>" + approval.JobCategory + "</td>\n";
				html += "	<td>" + approval.Sort + "</td>\n";
				html += "	<td>" + approval.CriteriaValue + "</td>\n";
				html += "   <td><i class=\"fa fa-2x fa-trash-o\" title=\"delete\"  aria-hidden=\"true\" onClick=\"ApproverDelete('" + approval.ApproverManagementID + "', '" + approval.JobCategory + "')\"></i></td>";
				html += "</tr>\n";
			});
		}

		// Set the container with the generated HTML
		$("#ApprovalWorkflowGrid>tbody").html(html);
		
		$("#ApprovalWorkflowGrid").tablesorter({
			debug: false,
			cssHeader: "headerSort",
			cssDesc: "headerSortDown",
			cssAsc: "headerSortUp"
		});
	}
}