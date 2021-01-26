//  Menu Bar
//  A component which displays the current cluster/community information
//  and provides a month drop down 
function MoveInRevenuePanel(ContainerID){
	
	var self = this;
	this.ContainerID = ContainerID; // ID of container to render menu bar
	this.CommunityNumber = null;
	this.EffectiveDT = null;
	this.BonusPlanID = null;
	this.EmployeeList = null;
	this.RevenueData = null;  // Populated by data provided by BonusPlanAJAX.svc
	
	// Listen for monthChange events generated from the MenuBar
	$(document).on("monthChange", function (event, month)
    {
		this.MonthDidChange(event, month);
    }.bind(self));
	
	// Gets the data necessary for the Move In Revenue
	this.MoveInRevenueActivityGet = function(CommunityNumber, EffectiveDT, BonusPlanID)
	{
		// Update attributes
		this.CommunityNumber = CommunityNumber;
		this.EffectiveDT = EffectiveDT;
		this.BonusPlanID = BonusPlanID;
		
		// Load spinner
		$("#" + this.ContainerID + " > .panel > .panel-body").html("<img src='../../../../images/progressSnapIn.gif' alt='Loading' />&nbsp;Please wait downloading...");
		
		// Set up AJAX request
		var objRevenue = new Object();
		objRevenue.CommunityNumber = this.CommunityNumber;
		objRevenue.EffectiveDT = this.EffectiveDT;
		objRevenue.BonusPlanID = this.BonusPlanID;
	
		var ajaxMethodURL = location.protocol + '//' + location.host + '/Application/BonusPlan/AJAXData/BonusPlanAJAX.svc/MoveInRevenueActivityByCommunityGet';

		$.ajax({
			type: 'POST',
			url: ajaxMethodURL,
			cache: false,
			data: JSON.stringify(objRevenue),
			contentType: 'application/json; charset=utf-8',
			dataType: 'json',
			success: function(msg){
				try
				{
					var json = JSON.parse(msg);
					
					this.RevenueData = json.Revenue;
					this.EmployeeList = json.EmployeeList;
					
					// alert any listening objects that the move in data is ready
					$(document).trigger('moveInDataReady', [this.RevenueData]);
					
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
	
	// Responds to month change event
	this.MonthDidChange = function(event, month)
	{
		//alert("revenue captured month change");
		this.EffectiveDT = month;
		this.MoveInRevenueActivityGet(this.CommunityNumber, this.EffectiveDT, this.BonusPlanID);
	}
	
	// Renders the Revenue Panel HTML at ContainerID
	this.Render = function()
	{
		// Don't render if the data isn't loaded
	    if (this.RevenueData === null)
		{ return;}
		
		var html = "";
		
		html += "<div class='panel panel-default'>\n";
		html += "	<div class='panel-body'>\n";
		html += "		<div class='table-responsive'>\n";
		
		if (this.EmployeeList.length == 0) {
		    html += "		<table class='table table-striped table-condensed tablesorter' id='BonusPlanRevenue'>\n";
		    html += "			<thead>\n";
		    html += "		<tr>\n";
		    html += "	        <th class='numericShort'>Room</th>\n";
		    html += "	        <th class='iconShort'>RA</th>\n";
		    html += "	        <th class='numericShort'>Stay</th>\n";
		    html += "	        <th class='characterLong'>Name</th>\n";
		    html += "	        <th class='characterLong'>Date</th>\n";
		    html += "	        <th class='numericShort'>Revenue</th>\n";
		    html += "	        <th class='numericShort'>Bonus</th>\n";
		    html += "	        <th class='numericShort'>Paid</th>\n";
		    html += "		</tr>\n";
		    html += "	</thead>\n";
		    html += "	<tbody>\n";
		    html += "       <tr><td colspan='8'>No data to display</td></tr>\n";
		    html += "   </tbody>\n";
		    html += "</table>\n";
		}

		this.EmployeeList.forEach(function (employee)
		{
		    html += "		<table class='table table-striped table-condensed tablesorter BonusPlanRevenue'>\n";
		    html += "			<thead>\n";

		    // Display the employee name
		    html += "				<tr>\n";
		    html += "					<th colspan='8'>" + employee.EmployeeName + "</th>\n";
		    html += "				</tr>\n";

		    html += "				<tr>\n";
		    html += "					<th class='numericShort'>Room</th>\n";
		    html += "	                <th class='iconShort'>RA</th>\n";
		    html += "	                <th class='numericShort'>Stay</th>\n";
		    html += "	                <th class='characterLong'>Name</th>\n";
		    html += "	                <th class='characterLong'>Date</th>\n";
		    html += "	                <th class='numericShort'>Revenue</th>\n";
		    html += "	                <th class='numericShort'>Bonus</th>\n";
		    html += "	                <th class='numericShort'>Paid</th>\n";
		    html += "				</tr>\n";
		    html += "			</thead>\n";
		    html += "<tbody>\n";

		    var employeeID = employee.EmployeeID;

            //check for existence of data
		    if (this.RevenueData.length > 0) {

		        this.RevenueData.forEach(function (revenue) {
		            if (revenue.EmployeeID == employeeID) {

		                html += "<tr>\n";
		                html += "<td class='numericShort'>" + revenue.RoomNumber + "</td>\n";

		                // Residency Agreement
		                if (revenue.ResidencyAgreementFlg == "1") {
		                    html += "<td class='iconShort'><span class=\"glyphicon glyphicon-check\"></span></td>\n";
		                }
		                else {
		                    html += "<td class='iconShort'><span class=\"glyphicon glyphicon-unchecked\"></span></td>\n";
		                }

		                html += "<td class='numericShort'>" + revenue.DayOfResidence + "</td>\n";
		                html += "<td class='characterLong'>" + revenue.ResidentName + "</td>\n";

		                // Format move in dates as MoveInDT - MoveOutDT if both are available.  If no MoveOutDT is available, just show MoveInDT
		                html += "<td class='characterLong'>" + revenue.MoveInDT

		                if (revenue.MoveOutDT) // Add label if there was a move out
		                {
		                    html += " - " + revenue.MoveOutDT;
		                }
		                html += "</td>\n";

		                html += "<td class='numericShort'>" + revenue.AmountAtMaturity + "</td>\n";
		                html += "<td class='numericShort'>" + revenue.BonusPayment + "</td>\n";

		                // Paid Flag
		                if (revenue.PaidFlg == "1") {
		                    html += "<td class='iconShort'><span class=\"glyphicon glyphicon-check\" title=\"" + revenue.PaidDt + "\"></span></td>\n";
		                }
		                else {
		                    html += "<td class='iconShort'><span class=\"glyphicon glyphicon-unchecked\"></span></td>\n";
		                }

		                html += "</tr>\n";
		            }

		            //// Generate table footer
		            //html += "			<tfoot>\n";
		            //html += "				<tr>\n";
		            //html += "			    	<td class='modalFooter' colspan='5'>&nbsp;</td>\n";
		            //html += "			    	<td class='numericShort modalFooter'>" + revenue.EmployeeData.AmountAtMaturity + "</td>\n";
		            //html += "			    	<td class='numericShort modalFooter'>" + revenue.EmployeeData.PotentialBonus + "</td>\n";
		            //html += "			    	<td class='iconLong' colspan='2'>&nbsp;</td>\n";
		            //html += "			 	</tr>\n";
		            //html += "			</tfoot>\n";

		        });
		    }

		    html += "			</tbody>\n";
		    html += "		</table>\n";

		}, this);
		
		html += "		</div>\n";
		html += "	</div>\n";
		html += "</div>\n";

		// Set the container with the generated HTML
		$("#" + this.ContainerID).html(html);
		
		$(".BonusPlanRevenue").tablesorter({
                debug: false,
                cssHeader: "headerSort",
                cssDesc: "headerSortDown",
                cssAsc: "headerSortUp"
            });
	}		
}