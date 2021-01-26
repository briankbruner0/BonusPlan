//  Bonus Plan Detail Panel
//  Used for editing and viewing basic bonus plan information
function BonusPlanDetailPanel(ContainerID){
	
	var self = this;
	this.ContainerID = ContainerID; // ID of container to render menu bar
	
	this.BonusPlan = null;  // Populated by data provided by BonusPlanAJAX.svc
	
	// Gets the data necessary for the Bonus Plan Detail
	this.BonusPlanDetailGet = function(BonusPlanID)
	{
		// Load spinner
		$("#" + this.ContainerID).html("<img src='../../../../images/progressSnapIn.gif' alt='Loading' />&nbsp;Please wait downloading...");
		
		// Set up AJAX request
		var objBonusPlan = new Object();
		objBonusPlan.BonusPlanID = BonusPlanID;
	
		var ajaxMethodURL = location.protocol + '//' + location.host + '/Application/BonusPlan/AJAXData/BonusPlanAJAX.svc/BonusPlanDetailGet';

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
					this.BonusPlan = JSON.parse(msg)[0];
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
	
	// Updates bonus plan information
	this.BonusPlanUpdate = function()
	{
		// Load spinner
		$("#" + this.ContainerID).html("<img src='../../../../images/progressSnapIn.gif' alt='Loading' />&nbsp;Please wait downloading...");
		
		// Set up AJAX request
		var objBonusPlan = new Object();
		objBonusPlan.BonusPlanID = this.BonusPlan.BonusPlanID;
		objBonusPlan.BonusPlan = this.BonusPlan.BonusPlan;
		objBonusPlan.TypeOfEarning = this.BonusPlan.TypeOfEarning;
		objBonusPlan.PaymentProcessFlg = this.BonusPlan.PaymentProcessFlg;
		objBonusPlan.ExcludeLookBackFlg = this.BonusPlan.ExcludeLookBackFlg;
		objBonusPlan.EmailFlg = this.BonusPlan.EmailFlg;
		objBonusPlan.ActiveFlg = this.BonusPlan.ActiveFlg;
		objBonusPlan.Username = $("#Username").val();
	
		var ajaxMethodURL = location.protocol + '//' + location.host + '/Application/BonusPlan/AJAXData/BonusPlanAJAX.svc/BonusPlanDetailUpdate';

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
					this.BonusPlan = JSON.parse(msg)[0];
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
	
	// Validates the Bonus Plan information and updates
	window.BonusPlanValidate = function()
	{
		var error = "";
		
		if ($("#BonusPlan").val() == "")
		{
			error += "Please provide a Bonus Plan name.\n";
		}
		
		if ($("#TypeOfEarning").val() == "")
		{
			error += "Please provide a Type of Earning.\n";
		}
		
		if (error.length > 0)
		{
			alert(error);
			return;
		}
		else
		{
			this.FormToDataBind();
			this.BonusPlanUpdate();
		}	
		
	}.bind(self)
	
	// Deletes the bonus plan
	window.BonusPlanDelete = function()
	{
		if (confirm("Are you sure you want to delete this Bonus Plan?"))
		{
			this.BonusPlan.ActiveFlg = "0";
			this.BonusPlanUpdate();
		}	
	}.bind(self)
	
	// Binds form data to this object
	this.FormToDataBind = function()
	{
		this.BonusPlan.BonusPlan = $("#BonusPlan").val();
		this.BonusPlan.TypeOfEarning = $("#TypeOfEarning").val();
		this.BonusPlan.PaymentProcessFlg = $("#PaymentProcessFlg").prop("checked") ? "1" : "0";
		this.BonusPlan.ExcludeLookBackFlg = $("#ExcludeLookBackFlg").prop("checked") ? "1" : "0";
		this.BonusPlan.EmailFlg = $("#EmailFlg").prop("checked") ? "1" : "0";
		
		// If the record is already active, it is displayed as a font awesome and we can't inactivate it
		if ($("i#ActiveFlg").length)
		{
			this.BonusPlan.ActiveFlg = "1";
		}
		else  // it is displayed as a checkbox and may be on or off
		{
			this.BonusPlan.ActiveFlg = $("#ActiveFlg").prop("checked") ? "1" : "0";
		}
	}
	
	// Renders the Bonus Plan Detail HTML at ContainerID
	this.Render = function()
	{
		// Don't render if the data isn't loaded
		if (this.BonusPlan === null)
		{ return;}
		
		var html = "";
		
		html += "<div class='col-xs-12 col-sm-12 col-md-8 col-lg-8'>\n";
        html += "   <div class='form-group row'>\n";
        html += "       <label class='col-xs-4 col-sm-4 col-md-4 col-lg-4 control-label col-form-label'>Bonus Plan ID:</label>\n";
        html += "       <div class='col-xs-8 col-sm-8 col-md-8 col-lg-8 form-control-static'>\n";
        html += "           " + this.BonusPlan.BonusPlanID + "\n";
        html += "       </div>\n";
        html += "   </div>\n";
        html += "   <div class='form-group row'>\n";
        html += "       <label class='col-xs-4 col-sm-4 col-md-4 col-lg-4 control-label col-form-label LabelRequired' for='BonusPlan'>Bonus Plan:</label>\n";
        html += "       <div class='col-xs-8 col-sm-8 col-md-8 col-lg-8'>\n";
        html += "           <input name='BonusPlan' class='form-control' id='BonusPlan' style='width: 200px;' type='text' value='" + this.BonusPlan.BonusPlan + "'>\n";
        html += "       </div>\n";
        html += "   </div>\n";
        html += "   <div class='form-group row'>\n";
        html += "       <label class='col-xs-4 col-sm-4 col-md-4 col-lg-4 control-label col-form-label LabelRequired' for='TypeOfEarning'>Type of Earning:</label>\n";
        html += "       <div class='col-xs-8 col-sm-8 col-md-8 col-lg-8'>\n";
        html += "           <input name='TypeOfEarning' class='form-control' id='TypeOfEarning' style='width: 50px;' type='text' maxlength='5' value='" + this.BonusPlan.TypeOfEarning + "'>\n";
        html += "       </div>\n";
        html += "   </div>\n";
		html += "   <div class='form-group row'>\n";
        html += "       <label class='col-xs-4 col-sm-4 col-md-4 col-lg-4 control-label col-form-label' for='PaymentProcessFlg'>Payment Process:</label>\n";
        html += "       <div class='col-xs-8 col-sm-8 col-md-8 col-lg-8'>\n";
        html += "           <div class='form-control-static'>\n";
		
		if (this.BonusPlan.PaymentProcessFlg == "1")
		{
			html += "               <input name='PaymentProcessFlg' id='PaymentProcessFlg' type='checkbox' checked='checked'>\n";
		}
		else
		{
			html += "               <input name='PaymentProcessFlg' id='PaymentProcessFlg' type='checkbox'>\n";
		}
		
        html += "           </div>\n";
        html += "       </div>\n";
        html += "   </div>\n";
        html += "   <div class='form-group row'>\n";
        html += "       <label class='col-xs-4 col-sm-4 col-md-4 col-lg-4 control-label col-form-label' for='ExcludeLookBackFlg'>Exclude Lookback:</label>\n";
        html += "       <div class='col-xs-8 col-sm-8 col-md-8 col-lg-8'>\n";
        html += "           <div class='form-control-static'>\n";
		
		if (this.BonusPlan.ExcludeLookBackFlg == "1")
		{
			html += "               <input name='ExcludeLookBackFlg' id='ExcludeLookBackFlg' type='checkbox' checked='checked'>\n";
		}
		else
		{
			html += "               <input name='ExcludeLookBackFlg' id='ExcludeLookBackFlg' type='checkbox'>\n";
		}
		
        html += "           </div>\n";
        html += "       </div>\n";
        html += "   </div>\n";
        html += "   <div class='form-group row'>\n";
        html += "       <label class='col-xs-4 col-sm-4 col-md-4 col-lg-4 control-label col-form-label' for='EmailFlg'>Send Email Alert:</label>\n";
        html += "       <div class='col-xs-8 col-sm-8 col-md-8 col-lg-8'>\n";
        html += "           <div class='form-control-static'>\n";
		
		if (this.BonusPlan.EmailFlg == "1")
		{
			html += "               <input name='EmailFlg' id='EmailFlg' type='checkbox' checked='checked'>\n";
		}
		else
		{
			html += "               <input name='EmailFlg' id='EmailFlg' type='checkbox'>\n";
		}
		
        html += "           </div>\n";
        html += "       </div>\n";
        html += "   </div>     \n";    
        
		
		html += "	<div class='row'>";
		html += "		<div class='col-xs-0 col-sm-2 col-md-2 col-lg-2'></div>\n";
		html += "		<div class='col-xs-8 col-sm-8 col-md-8 col-lg-8 btn-group-sm'>\n";
        html += "			<input id='BonusPlanSubmitButton' type='button' value='Submit' class='btn btn-sm btn-primary pull-left ' onclick='BonusPlanValidate();' />\n";
		html += "			<a href='BonusPlanProfile.aspx?BonusPlanID="+ this.BonusPlan.BonusPlanID +"' id='BonusPlanCancelButton' class='btn btn-sm btn-default pull-left'>Cancel</a>\n";
		html += "		</div>\n";
		html += "	</div>\n";
		html += "	<br/>";
		
        html += "</div>\n";

		// Set the container with the generated HTML
		$("#" + this.ContainerID).html(html);
		
		// Set the system information tab
		html = "";
		
		html += "<div class='col-xs-12 col-sm-12 col-md-8 col-lg-8'>\n";
		html += "   <div class='form-group row'>\n";
        html += "       <label class='col-xs-4 col-sm-4 col-md-4 col-lg-4 control-label col-form-label'>Create By:</label>\n";
        html += "       <div class='col-xs-8 col-sm-8 col-md-8 col-lg-8'>\n";
        html += "           <div class='form-control-static'>\n";
        html += "               " + this.BonusPlan.CreateBy + "\n";
        html += "           </div>\n";
        html += "       </div>\n";
        html += "   </div>\n";
        html += "   <div class='form-group row'>\n";
        html += "       <label class='col-xs-4 col-sm-4 col-md-4 col-lg-4 control-label col-form-label'>Create Date:</label>\n";
        html += "       <div class='col-xs-8 col-sm-8 col-md-8 col-lg-8'>\n";
        html += "           <div class='form-control-static'>\n";
        html += "               " + this.BonusPlan.CreateDT + "\n";
        html += "           </div>\n";
        html += "       </div>\n";
        html += "   </div>\n";
        html += "   <div class='form-group row'>\n";
        html += "       <label class='col-xs-4 col-sm-4 col-md-4 col-lg-4 control-label col-form-label'>Modify By:</label>\n";
        html += "       <div class='col-xs-8 col-sm-8 col-md-8 col-lg-8'>\n";
        html += "           <div class='form-control-static'>\n";
        html += "               " + this.BonusPlan.ModifyBy + "\n";
        html += "           </div>\n";
        html += "       </div>\n";
        html += "   </div>\n";
        html += "   <div class='form-group row'>\n";
        html += "       <label class='col-xs-4 col-sm-4 col-md-4 col-lg-4 control-label col-form-label'>Modify Date:</label>\n";
        html += "       <div class='col-xs-8 col-sm-8 col-md-8 col-lg-8'>\n";
        html += "           <div class='form-control-static'>\n";
        html += "               " + this.BonusPlan.ModifyDT + "\n";
        html += "           </div>\n";
        html += "       </div>\n";
        html += "   </div>\n";
        html += "   <div class='form-group row'>\n";
        html += "       <label class='col-xs-4 col-sm-4 col-md-4 col-lg-4 control-label col-form-label'>Active:</label>\n";
        html += "       <div class='col-xs-8 col-sm-8 col-md-8 col-lg-8'>\n";     

		if (this.BonusPlan.ActiveFlg == "1")
		{
			html += "           <div class='form-control-static'><i class='fa fa-lg fa-check-square fa-fw' id='ActiveFlg'></i></div>\n";
		}
		else
		{
			html += "           <input name='ActiveFlg' id='ActiveFlg' type='checkbox'>\n";
		}
		
        html += "       </div>\n";
        html += "   </div>\n";
		html += "	<div class='row'>";
		html += "		<div class='col-xs-0 col-sm-2 col-md-2 col-lg-2'></div>\n";
		html += "		<div class='col-xs-8 col-sm-8 col-md-8 col-lg-8 btn-group-sm'>\n";
		
		// Display the submit button only if the record can be reactivated
		if (this.BonusPlan.ActiveFlg == "0")
		{
			html += "			<input id='SystemSubmitButton' type='button' value='Submit' class='btn btn-sm btn-primary pull-left ' onclick='BonusPlanValidate();' />\n";
		}
		
		// Display the delete button if we have the security access AND the record is active
		if ($("#DeleteFlg").val() == "1" && this.BonusPlan.ActiveFlg == "1")
		{
		        html += "			<input id='SystemDeleteButton' type='button' value='Delete' class='btn btn-sm btn-warning pull-left ' onclick='BonusPlanDelete();' />\n";
		}
		
		html += "			<a href='BonusPlanProfile.aspx?BonusPlanID="+ this.BonusPlan.BonusPlanID +"' id='BonusPlanCancelButton' class='btn btn-sm btn-default pull-left'>Cancel</a>\n";
		html += "		</div>\n";
		html += "	</div>\n";
		html += "	<br/>";
		html += "</div>\n";
		
		$("#SystemTab").html(html);
	}		
}