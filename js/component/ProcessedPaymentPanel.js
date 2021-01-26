//  Processed Payment Panel
//  A component which displays the pending payments
//  for a given user and community
function ProcessedPaymentPanel(ContainerID){
	
	var self = this;
	this.ContainerID = ContainerID; // ID of container to render menu bar
	this.CommunityNumber = null;
	this.EffectiveDT = null;
	this.Username = null;
	
	this.PaymentList = null;  // Populated by data provided by BonusPlanAJAX.svc
	
	// Listen for monthChange events generated from the MenuBar
	$(document).on("monthChange", function (event, month)
    {
		this.EffectiveDT = month;
		this.ProcessedPaymentGet(this.CommunityNumber, this.EffectiveDT, this.Username);
    }.bind(self));
	
	// Gets the data necessary for the Move In Revenue
	this.ProcessedPaymentGet = function(CommunityNumber, EffectiveDT, Username)
	{
		// Update attributes
		this.CommunityNumber = CommunityNumber;
		this.EffectiveDT = EffectiveDT;
		this.Username = Username;
		
		// Load spinner
		$("#" + this.ContainerID + " > .panel > .panel-body > table").html("<img src='../../../../images/progressSnapIn.gif' alt='Loading' />&nbsp;Please wait downloading...");
		
		// Set up AJAX request
		var objRevenue = new Object();
		objRevenue.CommunityNumber = this.CommunityNumber;
		objRevenue.EffectiveDT = this.EffectiveDT;
		objRevenue.Username = this.Username;
	
		var ajaxMethodURL = location.protocol + '//' + location.host + '/Application/BonusPlan/AJAXData/BonusPlanAJAX.svc/ProcessedPaymentGet';

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
					this.PaymentList = JSON.parse(msg);
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
	window.ProcessedPaymentDetailModalOpen = function(PayrollID)
	{
		$("#ProcessedPaymentDetailModal").modal('show');
		
		// Get the corresponding detail object
		this.PaymentList.forEach(function(payment){

				if (payment.PayrollID == PayrollID)
				{
					this.ModalDataToFormBind(payment);
				}
				
		}.bind(self));
	}.bind(self)
	
	// Binds Revenue Detail to Modal Window
	this.ModalDataToFormBind = function(payment)
	{
		if (!payment)
		{
			return;
		}
		
		// Generate revenue detail table body
		var html = "";
		
		payment.Detail.forEach(function(detail){
			
			html += "<tr>";
			html += "	<td>" + detail.CustomerID + "</td>";
			html += "	<td>" + detail.Resident + "</td>";
			html += "	<td>" + detail.MoveInDt + "</td>";
			html += "	<td>" + payment.PaymentDt + "</td>";
			html += "	<td class='numericShort'>" + detail.Amount + "</td>";
			html += "</tr>";
		});
		
		$("#ProcessPaymentDetailModalTable>tbody").html(html);
		
		// Generate footer payment total
		$("#ProcessPaymentDetailModalTable>tfoot>tr>.pendingFooter").html(payment.Amount);
	}
	
	// Renders the Revenue Panel HTML at ContainerID
	this.Render = function()
	{
		// Don't render if the data isn't loaded
		if (this.PaymentList === null)
		{ return;}
		
		var html = "";
		
		html += "<div class='panel panel-default'>\n";	
        html += "	<div class='panel-heading'>Processed</div>\n";	
        html += "	<div class='panel-body'>\n";		
        html += "		<table class='table table-striped table-condensed tablesorter' id='snpProcessed'>\n";	
        html += "		    <thead>\n";	
        html += "		        <tr>\n";	
        html += "		            <th class='headerSort'>*Date</th>\n";	
        html += "		            <th class='numericShort headerSort'>Amount</th>\n";	
        html += "		            <th class='iconLong headerSort'>&nbsp;</th>\n";	
        html += "		        </tr>\n";	
        html += "		    </thead>\n";	
        html += "		    <tbody>\n";	
		
		if (this.PaymentList.length == 0)
		{
			html += "	    		<tr>\n";	
			html += "	    			<td colspan='3'>No data to display.</td>\n";	
			html += "	 			</tr>\n";
			html += "	  		</tbody>\n";
			html += "		</table>\n";	
			html += "	</div>\n";	
			html += "</div>\n";	
			
			// Set the container with the generated HTML
			$("#" + this.ContainerID).html(html);
			return;
		}
		
		this.PaymentList.forEach(function(payment){
			
			html += "	                <tr>\n";	
			html += "	                    <td>" + payment.PaymentDt + "</td>\n";	
			html += "	                    <td class='numericShort'>" + payment.Amount +"</td>\n";	
			html += "	                    <td class='iconLong'><input class='btn btn-primary btn-xs' type='button' value='View' onClick=\"ProcessedPaymentDetailModalOpen('" + payment.PayrollID + "')\"></td>\n";	
			html += "	                </tr>\n";
		});
		
        html += "	            </tbody>\n";	
        html += "	            <tfoot>\n";	
        html += "	                <tr>\n";	
        html += "	                    <td class='disclaimer' colspan='3'>*Processing Date is Approximately 7 Days Prior to the Pay Stub Payroll Date</td>\n";	
        html += "	                </tr>\n";	
        html += "	            </tfoot>\n";	
        html += "	        </table>\n";	
        html += "	    </span>\n";	
        html += "	</div>\n";	
        html += "</div>\n";	

		// Set the container with the generated HTML
		$("#" + this.ContainerID).html(html);
	}		
}