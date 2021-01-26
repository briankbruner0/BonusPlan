//  Menu Bar
//  A component which displays the current cluster/community information
//  and provides a month drop down 
function MoveInRevenuePanel(ContainerID){
	
	var self = this;
	this.ContainerID = ContainerID; // ID of container to render menu bar
	this.CommunityNumber = null;
	this.OperationClusterID = null;
	this.EffectiveDT = null;
	this.Username = null;
	this.BonusPlanID = null;
	
	this.CommunityList = null;  // Populated by data provided by BonusPlanAJAX.svc
	this.Cluster = null;  // Populated by data provided by BonusPlanAJAX.svc
	
	// Listen for monthChange events generated from the MenuBar
	$(document).on("monthChange", function (event, month)
    {
		this.MonthDidChange(event, month);
    }.bind(self));
	
	// Gets the data necessary for the Move In Revenue
	this.MoveInRevenueActivityGet = function(CommunityNumber, OperationClusterID, EffectiveDT, Username, BonusPlanID)
	{
		// Update attributes
		this.CommunityNumber = CommunityNumber;
		this.OperationClusterID = OperationClusterID;
		this.EffectiveDT = EffectiveDT;
		this.Username = Username;
		this.BonusPlanID = BonusPlanID;
		
		// Load spinner
		$("#" + this.ContainerID + " > .panel > .panel-body").html("<img src='../../../../images/progressSnapIn.gif' alt='Loading' />&nbsp;Please wait downloading...");
		
		// Set up AJAX request
		var objRevenue = new Object();
		objRevenue.CommunityNumber = this.CommunityNumber;
		objRevenue.OperationClusterID = this.OperationClusterID;
		objRevenue.EffectiveDT = this.EffectiveDT;
		objRevenue.Username = this.Username;
		objRevenue.BonusPlanID = this.BonusPlanID;
	
		var ajaxMethodURL = location.protocol + '//' + location.host + '/Application/BonusPlan/AJAXData/BonusPlanAJAX.svc/MoveInRevenueActivityGet';

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
					
					this.CommunityList = json.Community;
					this.Cluster = json.Cluster;
					
					// alert any listening objects that the move in data is ready
					$(document).trigger('moveInDataReady', [this.CommunityList, this.Cluster]);
					
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

	// Loads the revenue detail modal
	window.RevenueDetailModalOpen = function(MoveInRevenueID)
	{
		$("#RevenueDetailModal").modal('show');
		
		// Get the corresponding detail object
		this.CommunityList.forEach(function(community){
			community.MoveInRevenue.forEach(function(revenue){
				if (revenue.MoveInRevenueID == MoveInRevenueID)
				{
					this.ModalDataToFormBind(revenue);
				}
			}.bind(self));
		}.bind(self));
		
	}.bind(self)
	
	// Responds to month change event
	this.MonthDidChange = function(event, month)
	{
		//alert("revenue captured month change");
		this.EffectiveDT = month;
		this.MoveInRevenueActivityGet(this.CommunityNumber, this.OperationClusterID, this.EffectiveDT, this.Username, this.BonusPlanID);
	}
	
	// Binds Revenue Detail to Modal Window
	this.ModalDataToFormBind = function(MoveInRevenue)
	{
		if (!MoveInRevenue)
		{
			return;
		}
		
		// Bind header information
		$("#RevenueDetailModalResidentName").html(MoveInRevenue.ResidentName);
		$("#RevenueDetailModalCustomerID").html(MoveInRevenue.CustomerID);
		$("#RevenueDetailModalRoomNumber").html(MoveInRevenue.RoomNumber);
		$("#RevenueDetailModalCareTypeCode").html(MoveInRevenue.CareTypeCode);
		$("#RevenueDetailModalCareTypeCode").removeClass();
		$("#RevenueDetailModalCareTypeCode").addClass('badge roomBadge-' + MoveInRevenue.CareTypeCode);
		$("#RevenueDetailModalRoomStyle").html(MoveInRevenue.RoomType);
		$("#RevenueDetailModalMoveInDT").html(MoveInRevenue.MoveInDT);
		$("#RevenueDetailModalMoveOutDT").html(MoveInRevenue.MoveOutDT);
		$("#RevenueDetailModalResidencyAgreementFlg > label").text(MoveInRevenue.ResidencyAgreementFlg == "1" ? "Yes" : "No");
		$("#RevenueDetailModalResidencyAgreementFlg > span").removeClass();
		$("#RevenueDetailModalResidencyAgreementFlg > span").addClass(MoveInRevenue.ResidencyAgreementFlg == "1" ? "glyphicon glyphicon-check" : "glyphicon glyphicon-unchecked");
		
		// Generate revenue detail table body
		var html = "";
		
		MoveInRevenue.Detail.forEach(function(detail){
			
			html += "<tr>";
			html += "	<td>" + detail.Description + "</td>";
			html += "	<td class='numericShort'>" + detail.AmountAtMaturity + "</td>";
			html += "</tr>";
		});
		
		$("#RevenueDetailModalRevenueTable>tbody").html(html);
		
		// Generate revenue detail table body footer
		var footerHtml = "";
		
		footerHtml += "<tr>";
		footerHtml += "	<td>&nbsp;</td>";
		footerHtml += "	<td class='numericShort modalFooter'>" + MoveInRevenue.AmountAtMaturity + "</td>";
		footerHtml += "</tr>"; 
		
		$("#RevenueDetailModalRevenueTable>tfoot").html(footerHtml);
	}
	
	// Renders the Revenue Panel HTML at ContainerID
	this.Render = function()
	{
		// Don't render if the data isn't loaded
		if (this.CommunityList === null)
		{ return;}
		
		var html = "";
		
		html += "<div class='panel panel-default'>\n";
		html += "	<div class='panel-body'>\n";
		html += "		<div class='table-responsive'>\n";
		
		this.CommunityList.forEach(function(community)
		{
			html += "		<table class='table table-striped table-condensed tablesorter' id='BonusPlanRevenue'>\n";
			html += "			<thead>\n";
			
			// Display the community name, but only as part of the cluster view
			if (this.OperationClusterID)
			{
				html += "				<tr>\n";
				html += "					<th colspan='9'>"+ community.CommunityName + " ("+ community.CommunityNumber +")</th>\n";
				html += "				</tr>\n";
			}
			
			html += "				<tr>\n";
			html += "					<th class='numericShort'>Room</th>\n";
			html += "	                <th class='iconShort'>RA</th>\n";
			html += "	                <th class='numericShort'>Stay</th>\n";
			html += "	                <th class='characterLong'>Name</th>\n";
			html += "	                <th class='characterLong'>Date</th>\n";
			html += "	                <th class='numericShort'>Revenue</th>\n";
			html += "	                <th class='numericShort'>Bonus</th>\n";
			html += "	                <th class='iconShort'>Paid</th>\n";
			html += "	                <th class='iconLong'>&nbsp;</th>\n";
			html += "				</tr>\n";
			html += "			</thead>\n";
			html += "		<tbody>\n";
			
			if (community.MoveInRevenue.length == 0)
			{
				html += "<tr><td colspan='10'>No data to display</td></tr>\n";
				html += "			</tbody>\n";
			}
			else
			{
				community.MoveInRevenue.forEach(function(revenue){
					
					html += "<tr>\n";
					
					html += "<td class='numericShort'>" + revenue.RoomNumber + "</td>\n";
					
					// Residency Agreement
					if (revenue.ResidencyAgreementFlg == "1")
					{
						html += "<td class='iconShort'><span class=\"glyphicon glyphicon-check\"></span></td>\n";
					}
					else
					{
						html += "<td class='iconShort'><span class=\"glyphicon glyphicon-unchecked\"></span></td>\n";
					}
					
					html += "<td class='numericShort'>" + revenue.DayOfResidence +"</td>\n";
					html += "<td class='characterLong'>" + revenue.ResidentName +"</td>\n";
					
					// Format move in dates as MoveInDT - MoveOutDT if both are available.  If no MoveOutDT is available, just show MoveInDT
					html += "<td class='characterLong'>" + revenue.MoveInDT
					
					if (revenue.MoveOutDT) // Add label if there was a move out
					{
						html += " - " + revenue.MoveOutDT;
					}
					html += "</td>\n";
					
					html += "<td class='numericShort'>" + revenue.AmountAtMaturity + "</td>\n";                                          
					html += "<td class='numericShort'>" + revenue.PotentialBonus + "</td>\n";
					
					// Paid Flag
					if (revenue.PaidFlg == "1")
					{
					    html += "<td class='iconShort'><span class=\"glyphicon glyphicon-check\" title=\"" + revenue.PaidDt + "\"></span></td>\n";
					}
					else
					{
						html += "<td class='iconShort'><span class=\"glyphicon glyphicon-unchecked\"></span></td>\n";
					}
					
					html += "<td class='iconLong'><input class='btn btn-primary btn-xs' type='button' value='View' onClick=\"RevenueDetailModalOpen('" + revenue.MoveInRevenueID + "')\"></td>\n";
					
					html += "</tr>\n";
				});
				
				html += "			</tbody>\n";
			
				// Generate table footer
				html += "			<tfoot>\n";
				html += "				<tr>\n";
				html += "			    	<td class='modalFooter' colspan='5'>&nbsp;</td>\n";
				html += "			    	<td class='numericShort modalFooter'>" +community.AmountAtMaturity + "</td>\n";
				html += "			    	<td class='numericShort modalFooter'>" + community.PotentialBonus + "</td>\n";
				html += "			    	<td class='iconLong' colspan='2'>&nbsp;</td>\n";
				html += "			 	</tr>\n";
				html += "			</tfoot>\n";
			}
			
			html += "		</table>\n";
		}, this);
		
		html += "		</div>\n";
		html += "	</div>\n";
		html += "</div>\n";

		// Set the container with the generated HTML
		$("#" + this.ContainerID).html(html);
		
		$("#BonusPlanRevenue").tablesorter({
                debug: false,
                headers: {
                    1: { sorter: false },
                    7: { sorter: false }
                },
                cssHeader: "headerSort",
                cssDesc: "headerSortDown",
                cssAsc: "headerSortUp"
            });
	}		
}