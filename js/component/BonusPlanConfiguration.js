//  Bonus Plan Configuration
//  A panel that displays config information about all bonus plans in the system
function BonusPlanConfiguration(ContainerID){
	
	var self = this;
	this.ContainerID = ContainerID; // ID of container to render menu bar
	this.ActiveFlg = "1";
	
	this.BonusPlanList = null;  // Populated by data provided by BonusPlanAJAX.svc
	
	// Gets the data necessary for the Bonus Plan Configuration
	this.BonusPlanConfigurationGet = function()
	{	
		// Load spinner
		$("#" + this.ContainerID + " > .panel > .panel-body").html("<img src='../../../../images/progressSnapIn.gif' alt='Loading' />&nbsp;Please wait downloading...");
		
		// Set up AJAX request
		var objBonusPlan = new Object();
		objBonusPlan.ActiveFlg = this.ActiveFlg;
	
		var ajaxMethodURL = location.protocol + '//' + location.host + '/Application/BonusPlan/AJAXData/BonusPlanAJAX.svc/BonusPlanConfigurationGet';

		$.ajax({
			type: 'POST',
			url: ajaxMethodURL,
			cache: false,
			data: JSON.stringify(objBonusPlan),
			contentType: 'application/json; charset=utf-8',
			dataType: 'json',
			success: function(msg){
				try
				{
					this.BonusPlanList = JSON.parse(msg);
					this.Render();
				}
				catch(err){}
			}.bind(self),
			error: function (XMLHttpRequest, textStatus, errorThrown)
			{
				alert('Error :' + XMLHttpRequest.responseText);
			}
		});
	}
	
	// Renders the Revenue Panel HTML at ContainerID
	this.Render = function()
	{
		// Don't render if the data isn't loaded
		if (this.BonusPlanList === null)
		{ return;}
		
		var html = "";
		
		html += "<div class='panel panel-default'>\n";
		html += "	<div class='panel-body'>\n";
		
		html += "		<div class='row'>\n";
		html += "			<div class='col-xs-6 col-sm-6 col-md-6 col-lg-6 pull-right'>\n";
        html += "  			    <div class='input-group'>\n";
        html += "  			        <input class='form-control input-sm pull-right' id='BonusPlanConfigurationFilter' type='search' placeholder='To filter your results, enter your search criteria here'>\n";
        html += "  			        <span class='input-group-addon'><span title='search' class='glyphicon glyphicon-search'></span></span>\n";
        html += "  			    </div>\n";
        html += "  			</div>\n";
		html += "  		</div>\n";
		
		html += "		<table class='table table-striped table-condensed tablesorter' id='BonusPlanConfigurationGrid'>\n";
		html += "			<thead>\n";
		
		html += "				<tr>\n";
		html += "					<th class='numericShort'>ID</th>\n";
		html += "					<th class='characterShort'>Name</th>\n";
		html += "	                <th class='numericShort'>Community Count</th>\n";
		html += "	                <th class='numericShort'>Ledger Entry Count</th>\n";
		html += "	                <th class='numericShort'>Job Role Count</th>\n";
		html += "	                <th class='numericShort'>Employee Count</th>\n";
		html += "	                <th class='numericShort'>Override Employee Count</th>\n";
		html += "	                <th class='numericShort'>&nbsp;</th>\n";
		html += "				</tr>\n";
		html += "			</thead>\n";
		html += "		<tbody>\n";
		
		if (this.BonusPlanList.length == 0)
		{
			html += "<tr><td colspan='8'>No data to display</td></tr>\n";
			html += "			</tbody>\n";
		}
		else
		{
			this.BonusPlanList.forEach(function(bonusPlan){
				
				html += "<tr>\n";
				
				html += "<td class='numericShort'>" + bonusPlan.BonusPlanID + "</td>\n";
				html += "<td class='characterShort'>" + bonusPlan.BonusPlan + "</td>\n";
				html += "<td class='numericShort'>" + bonusPlan.CommunityCount + "</td>\n";
				html += "<td class='numericShort'>" + bonusPlan.RevenueEntryCount + "</td>\n";
				html += "<td class='numericShort'>" + bonusPlan.JobRoleCount + "</td>\n";
				html += "<td class='numericShort'>" + bonusPlan.EmployeeCount + "</td>\n";
				html += "<td class='numericShort'>" + bonusPlan.OverrideEmployeeCount + "</td>\n";
				html += "<td class='iconLong'><a class='btn btn-primary btn-xs' href='BonusPlanProfile.aspx?BonusPlanID=" + bonusPlan.BonusPlanID + "'>View</a></td>\n";
				
				html += "</tr>\n";
			});
			
			html += "			</tbody>\n";
		}
		
		html += "		</table>\n";
		
		html += "	</div>\n";
		html += "</div>\n";

		// Set the container with the generated HTML
		$("#" + this.ContainerID).html(html);
		
		$("#BonusPlanConfigurationGrid").tablesorter({
			debug: false,
			headers: {
				7: { sorter: false }
			},
			cssHeader: "headerSort",
			cssDesc: "headerSortDown",
			cssAsc: "headerSortUp"
		});
			
		$.tablefilter({
			inputElement: "#BonusPlanConfigurationFilter",
			tableElement: "#BonusPlanConfigurationGrid"
		});
	}		
}