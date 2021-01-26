//  Bonus Plan Job Code Panel
//  Used for editing and viewing configured job codes
function BonusPlanJobCodePanel(ContainerID){
	
	var self = this;
	this.ContainerID = ContainerID; // ID of container to render menu bar
	this.BonusPlanID = null;
	this.JobCodeList = null;
		
	// Gets the revenue entries for this Bonus Plan
	this.BonusPlanToJobCodeDetailGet = function(BonusPlanID)
	{
		this.BonusPlanID = BonusPlanID;
		
		var objBonusPlan = new Object();
		objBonusPlan.BonusPlanID = this.BonusPlanID;
		
		// Set up AJAX request
		var objBonusPlan = new Object();
		objBonusPlan.BonusPlanID = BonusPlanID;
	
		var ajaxMethodURL = location.protocol + '//' + location.host + '/Application/BonusPlan/AJAXData/BonusPlanAJAX.svc/BonusPlanToJobCodeDetailGet';

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
					this.JobCodeList = JSON.parse(msg);
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
	
	// Inserts a job code into the configuration and returns an updated list of configured job codes
    this.BonusPlanJobCodeInsert = function()
	{
		var objBonusPlan = new Object();
		objBonusPlan.bonusPlan = new Object();
		objBonusPlan.bonusPlan.BonusPlanID = this.BonusPlanID;
		objBonusPlan.bonusPlan.JobCodeID = $("#JobCodeID").val();
		objBonusPlan.bonusPlan.Percentage = $("#JobCodePercentage").val();
		objBonusPlan.bonusPlan.JobCodeMultiplier = $("#JobCodeMultiplier").val();
		objBonusPlan.bonusPlan.JobCodeCommissionBase = $("#JobCodeCommissionBase").val();
		objBonusPlan.bonusPlan.CommunityNumber = $("#JobCodeCommunity").val();
		objBonusPlan.bonusPlan.EffectiveDt = $("#JobCodeEffectiveDt").val();
		objBonusPlan.bonusPlan.Username = $("#Username").val();

		var ajaxMethodURL = location.protocol + '//' + location.host + '/Application/BonusPlan/AJAXData/BonusPlanAJAX.svc/BonusPlanToJobCodeInsert';

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
				this.JobCodeList = JSON.parse(msg);
				this.RenderTable();
			}.bind(self),
			error: function (XMLHttpRequest, textStatus, errorThrown)
			{
				alert('Error :' + XMLHttpRequest.responseText);
			}
		});
	}
	
	// Loads a spinner into the community table
	this.TableSpinnerGet = function()
	{
		$("#JobCodeGrid>tbody").html("<tr><td colspan='8'><img src='/images/progressSnapIn.gif' alt='Loading'/>&nbsp;Please wait downloading...</td></tr>");
	}
	
	// Deletes a job code entry from the configuration
	window.JobCodeDelete = function(BonusPlanToJobCodeID, JobTitle)
	{
		if (confirm("Are you sure you want to delete " + JobTitle + "?"))
		{
			this.BonusPlanJobCodeDelete(BonusPlanToJobCodeID);
		}
	}.bind(self)
		
	// Deletes a job code entry from the configuration and returns an updated list of job codes
    this.BonusPlanJobCodeDelete = function(BonusPlanToJobCodeID)
	{
		var objBonusPlan = new Object();
		objBonusPlan.BonusPlanID = this.BonusPlanID;
		objBonusPlan.BonusPlanToJobCodeID = BonusPlanToJobCodeID;

		var ajaxMethodURL = location.protocol + '//' + location.host + '/Application/BonusPlan/AJAXData/BonusPlanAJAX.svc/BonusPlanToJobCodeDelete';

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
				this.JobCodeList = JSON.parse(msg);
				this.RenderTable();
			}.bind(self),
			error: function (XMLHttpRequest, textStatus, errorThrown)
			{
				alert('Error :' + XMLHttpRequest.responseText);
			}
		});
	}
	
	// Recalculates the total percentage every time an input field is changed.
	window.JobCodeTotalPercentageGet = function()
    {
		var base = parseFloat($("#JobCodeCommissionBase").val());
		var multiplier = parseFloat($("#JobCodeMultiplier").val());
		var percentage = parseFloat($("#JobCodePercentage").val());
		
		if (isNaN(base) || isNaN(multiplier) || isNaN(percentage))
		{
			$("#JobCodeTotalPercentageLabel").text("--%");
		}
		else
		{
			var total = ((base * percentage)/100.00) * (multiplier/100.00);
			$("#JobCodeTotalPercentageLabel").text(total + "%");
		}
	}
	
	window.JobCodeValidate = function()
        {
            var error = "";

			if ($("#JobCodeID").val() == "")
			{
				error += "Please select a Job Code.\n";
			}
			
			var jobCodeCommissionBase = parseFloat($("#JobCodeCommissionBase").val());
			if (isNaN(jobCodeCommissionBase))
			{
				error += "Please provide a valid Base Commission.\n";
			}

			if (jobCodeCommissionBase > 100)
			{
				error += "Job Code Base Commission cannot be over 100%.\n";
			}

			if (jobCodeCommissionBase < 0)
			{
				error += "Job Code Base Commission cannot be less than 0%.\n";
			}

			var jobCodeMultiplier = parseFloat($("#JobCodeMultiplier").val());
			if (isNaN(jobCodeMultiplier))
			{
				error += "Please provide a valid Job Code Multiplier.\n";
			}

			if (jobCodeMultiplier < 0)
			{
				error += "Job Code Multiplier cannot be less than 0.\n";
			}

			var jobCodePercentage = parseFloat($("#JobCodePercentage").val());
			if (isNaN(jobCodePercentage))
			{
				error += "Please provide a valid Job Code Percentage.\n";
			}

			if (jobCodePercentage > 100)
			{
				error += "Job Code Percentage cannot be over 100%.\n";
			}

			if (jobCodePercentage < 0)
			{
				error += "Job Code Percentage cannot be less than 0%.\n";
            }
			
			if($("#JobCodeEffectiveDt").val() == "")
			{
				error += "Please enter an effective date.\n";
			}

            if (error.length > 0)
            {
                alert(error);
                return false;
            }
            else
            {
                this.BonusPlanJobCodeInsert();
            }
        }.bind(self)
	
	// Renders the Bonus Plan to Job Code HTML at ContainerID
	this.Render = function()
	{
		
	}

	// Renders the table data at #JobCodeGrid
	this.RenderTable = function()
	{
		var html = "";
		
		if (this.JobCodeList.length == 0)
		{
			html += "<tr>\n";
			html += "    <td colspan='8'>No data to display.</td>\n";
			html += "</tr>\n";
		}
		else
		{
			this.JobCodeList.forEach(function(jobCode){
				
				html += "<tr>\n";
				html += "	<td class='hidden-xs'>" + jobCode.BonusPlanToJobCodeID + "</td>\n";
				html += "	<td class='hidden-xs'>" + jobCode.JobCode + "</td>\n";
				html += "	<td>" + jobCode.JobTitle + "</td>\n";
				html += "	<td>" + jobCode.Community + "</td>\n";
				html += "	<td class='numericShort'>" + jobCode.CommissionBase + "</td>\n";
				html += "	<td class='numericShort'>" + jobCode.Multiplier + "</td>\n";
				html += "	<td class='numericShort'>" + jobCode.Percentage + "</td>\n";
				html += "	<td class='dateShort'>" + jobCode.EffectiveDT + "</td>\n";
				html += "   <td><i class=\"fa fa-2x fa-trash-o\" title=\"delete\"  aria-hidden=\"true\" onClick=\"JobCodeDelete('" + jobCode.BonusPlanToJobCodeID + "', '" + jobCode.JobTitle + "')\"></i></td>";
				html += "</tr>\n";
			});
		}

		// Set the container with the generated HTML
		$("#JobCodeGrid>tbody").html(html);
		
		// Set up table filter
		$.tablefilter({
                inputElement: "#JobCodeFilter",
                tableElement: "#JobCodeGrid"
            });

		$("#JobCodeGrid").tablesorter({
			debug: false,
			headers: {8:{sorter:false}},
			cssHeader: "headerSort",
			cssDesc: "headerSortDown",
			cssAsc: "headerSortUp"
		});
	}
}