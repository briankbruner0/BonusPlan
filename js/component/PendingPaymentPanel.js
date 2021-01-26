//  Pending Payment Panel
//  A component which displays the pending payments
//  for a given user and community
function PendingPaymentPanel(ContainerID){
	
	var self = this;
	this.ContainerID = ContainerID; // ID of container to render menu bar
	this.CommunityNumber = null;
	this.EffectiveDT = null;
	this.Username = null;
	
	this.PaymentList = null;  // Populated by data provided by BonusPlanAJAX.svc
	
	// Listen for monthChange events generated from the MenuBar
	$(document).on("monthChange", function (event, month)
    {
		this.MonthDidChange(event, month);
    }.bind(self));
	
	// Gets the data necessary for the Move In Revenue
	this.PendingPaymentGet = function(CommunityNumber, EffectiveDT, Username)
	{
		// Update attributes
		this.CommunityNumber = CommunityNumber;
		this.EffectiveDT = EffectiveDT;
		this.Username = Username;
		
		// Load spinner
		$("#" + this.ContainerID + " > .panel > .panel-body > table ").html("<img src='../../../../images/progressSnapIn.gif' alt='Loading' />&nbsp;Please wait downloading...");
		
		// Set up AJAX request
		var objRevenue = new Object();
		objRevenue.CommunityNumber = this.CommunityNumber;
		objRevenue.EffectiveDT = this.EffectiveDT;
		objRevenue.Username = this.Username;
	
		var ajaxMethodURL = location.protocol + '//' + location.host + '/Application/BonusPlan/AJAXData/BonusPlanAJAX.svc/PendingPaymentGet';

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
	
	// Responds to month change event
	this.MonthDidChange = function(event, month)
	{
		//alert("revenue captured month change");
		this.EffectiveDT = month;
		this.PendingPaymentGet(this.CommunityNumber, this.EffectiveDT, this.Username);
	}
	
	// Renders the Revenue Panel HTML at ContainerID
	this.Render = function()
	{	
		var html = "";
		
		html += "<div class='panel panel-default'>\n";	
        html += "   <div class='panel-heading'>Pending</div>\n";
        html += "   <div class='panel-body'>\n";
        //html += "       <table class='table table-striped table-condensed tablesorter' id='snpPending'>\n";

        //html += "       <thead>\n";
        //html += "           <tr>\n";
        //html += "               <th colspan='2'>Waiting for Approval</th>\n";
        //html += "           </tr>\n";
        //html += "       </thead>\n";

        //html += "       <tbody>\n";

        ////global variable to hold payment total
        //var paymentTotal = "";

        //if (this.PaymentList.length > 0) {
            
        //    this.PaymentList.forEach(function (payment) {

        //        if (payment.PaymentStatusID == "2") {
        //            paymentTotal = payment.Total;

        //            html += "   <tr>\n";
        //            html += "       <td>" + payment.Resident + "</td>\n";
        //            html += "       <td class='numericShort'>" + payment.Amount + "</td>\n";
        //            html += "   </tr>\n";
        //        }
        //    });            
        //} 
        //else {
        //    html += "	        <tr>\n";
        //    html += "	        	<td colspan='2'>No data to display.</td>\n";
        //    html += "	        </tr>\n";

        //    //reset payment total variable
        //    paymentTotal = "";
        //}

        //html += "       </tbody>\n";

        //if (paymentTotal.length > 0) {
        //    html += "       <tfoot>\n";
        //    html += "           <tr>\n";
        //    html += "               <td colspan='2' style='text-align:right;'>" + paymentTotal + "</td>\n";
        //    html += "           </tr>\n";
        //    html += "       </tfoot>\n";

        //    //reset payment total variable
        //    paymentTotal = "";
        //}
        //html += "       </table>\n";
        

		html += "       <table class='table table-striped table-condensed tablesorter' id='snpPending'>\n";
		html += "       <thead>\n";
		html += "           <tr>\n";
		html += "               <th colspan='2'>Approved (waiting for processing)</th>\n";
		html += "           </tr>\n";
		html += "       </thead>\n";
		html += "       <tbody>\n";

		if (this.PaymentList.length > 0) {

		    this.PaymentList.forEach(function (payment) {

		        if (payment.PaymentStatusID == "3") {
		            paymentTotal = payment.Total;

		            html += "      <tr>\n";
		            html += "          <td>" + payment.Resident + "</td>\n";
		            html += "          <td class='numericShort'>" + payment.Amount + "</td>\n";
		            html += "      </tr>\n";
		        }
		    });		    
		}
		else {
		    html += "	        <tr>\n";
		    html += "	        	<td colspan='2'>No data to display.</td>\n";
		    html += "	        </tr>\n";

		    //reset payment total variable
		    paymentTotal = "";
		}

		html += "       </tbody>\n";
		html += "       <tfoot>\n";

		if (paymentTotal.length > 0) {
		    html += "           <tr>\n";
		    html += "               <td colspan='2' style='text-align:right;'>" + paymentTotal + "</td>\n";
		    html += "           </tr>\n";

		    //reset payment total variable
		    paymentTotal = "";
		}
        html += "           <tr>\n";
        html += "               <td class='disclaimer' style=\"border-top: 1px solid white;\" colspan='2'>*Pending Payments have met the 30 day stay requirement. </td>\n";
        html += "           </tr>\n";
        html += "           <tr>\n";
        html += "               <td class='disclaimer' style=\"border-top: 1px solid white;\" colspan='2'>**MP = Manual Payment Assigned to a Resident </td>\n";
        html += "           </tr>\n";
        html += "       </tfoot>\n";
        html += "       </table>\n";
        html += "	</div>\n";
        html += "</div>\n";

		// Set the container with the generated HTML
		$("#" + this.ContainerID).html(html);
	}		
}