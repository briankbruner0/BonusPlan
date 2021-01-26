//  Bonus Plan To Community Panel
//  Used for editing and viewing bonus plan to community data
function BonusPlanToCommunityPanel(ContainerID){
	
	var self = this;
	this.ContainerID = ContainerID; // ID of container to render menu bar
	this.BonusPlanID = null;
	
	this.ConfiguredCommunityList = null;  // Populated by data provided by BonusPlanAJAX.svc
		
	// Gets the configured communities for this Bonus Plan
	this.BonusPlanToCommunityGet = function(BonusPlanID)
	{
		this.BonusPlanID = BonusPlanID;
		
		var objBonusPlan = new Object();
		objBonusPlan.BonusPlanID = this.BonusPlanID;
		
		// Load spinner
		$("#" + this.ContainerID).html("<img src='../../../../images/progressSnapIn.gif' alt='Loading' />&nbsp;Please wait downloading...");
		
		// Set up AJAX request
		var objBonusPlan = new Object();
		objBonusPlan.BonusPlanID = BonusPlanID;
	
		var ajaxMethodURL = location.protocol + '//' + location.host + '/Application/BonusPlan/AJAXData/BonusPlanAJAX.svc/BonusPlanToCommunityGet';

		var configuredCommunity = $.ajax({
			type: 'POST',
			url: ajaxMethodURL,
			cache: false,
			data: JSON.stringify(objBonusPlan),
			contentType: 'application/json; charset=utf-8',
			dataType: 'json',
			success: function(msg){
				try
				{
					this.ConfiguredCommunityList = JSON.parse(msg);
				}
				catch(err){}
			}.bind(self),
			error: function (XMLHttpRequest, textStatus, errorThrown)
			{
				alert('Error :' + XMLHttpRequest.responseText);
			}
		});
		
		// Get the list of all communities if necessary
		if (CommunityList === null)
		{
			var community = CommunityUserAccessGet();
			$.when(community, configuredCommunity).done(function(){
				this.Render();
				this.RenderTable();
			}.bind(self) )
		}
		else
		{
			$.when(community).done( function(){
				this.Render();
				this.RenderTable();
			}.bind(self) )
		}
	}
	
	// Loads a spinner into the community table
	this.TableSpinnerGet = function()
	{
		$("#CommunityGrid>tbody").html("<tr><td colspan='4'><img src='/images/progressSnapIn.gif' alt='Loading'/>&nbsp;Please wait downloading...</td></tr>");
	}
	
	// Adds or edits a community that is already on the community grid
    window.EditCommunity = function(CommunityNumber)
	{
		var CommunityConfigured = false;
		var CommunityBeginDt = null;
		var CommunityEndDt = null;
		
		// Look to see if this community is already configured
		this.ConfiguredCommunityList.forEach(function (community)
		{
			if (community.CommunityNumber == CommunityNumber)
			{
				CommunityConfigured = true;
				CommunityBeginDt = Date.parse(community.BeginDt) ? community.BeginDt : "";
				CommunityEndDt = Date.parse(community.EndDt) ? community.EndDt : "";
			}
		});

		if (CommunityConfigured) // Edit existing configuration
		{
			$("#BonusPlanCommunity").val(CommunityNumber);
			$("#CommunityBeginDt").val(CommunityBeginDt);
			$("#CommunityEndDt").val(CommunityEndDt);
			$("#AddCommunityButton").prop("value", "Edit");
		}
		else // Add new configuration
		{
			$("#CommunityBeginDt").val("");
			$("#CommunityEndDt").val("");
			$("#AddCommunityButton").prop("value", "Add");
		}
	}.bind(self)

	// Validates community being added or edited
	window.VerifyCommunity = function()
	{
		var error = "";

		if ($("#BonusPlanCommunity").val() == "")
		{
			error += "Please select a community.\n"
		}

		if ($("#BonusPlanEffectiveDt").val() == "")
		{
			error += "Please select an effective date.\n";
		}

		if (error.length > 0)
		{
			alert(error);
			return false;
		}

		this.BonusPlanToCommunityInsert();
		
	}.bind(self)
	
	// Inserts a community and returns an updated list of configured communities
    this.BonusPlanToCommunityInsert = function()
	{
		var objBonusPlan = new Object();
		objBonusPlan.BonusPlanID = this.BonusPlanID;
		objBonusPlan.CommunityNumber = $("#BonusPlanCommunity").val();
		objBonusPlan.BeginDt = $("#CommunityBeginDt").val();
		objBonusPlan.EndDt = $("#CommunityEndDt").val();
		objBonusPlan.Username = $("#Username").val();

		var ajaxMethodURL = location.protocol + '//' + location.host + '/Application/BonusPlan/AJAXData/BonusPlanAJAX.svc/BonusPlanToCommunityInsert';

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
				this.ConfiguredCommunityList = JSON.parse(msg);
				this.RenderTable();
			}.bind(self),
			error: function (XMLHttpRequest, textStatus, errorThrown)
			{
				alert('Error :' + XMLHttpRequest.responseText);
			}
		});
	}
	
	// Deletes a community from the configuration
	window.DeleteCommunity = function(Community, BonusPlanToCommunityID)
	{
		if (confirm("Are you sure you want to delete " + Community + "?"))
		{
			this.BonusPlanToCommunityDelete(BonusPlanToCommunityID);
		}
	}.bind(self)
		
	// Deletes a community from the configuration and returns an updated list of configured communities
    this.BonusPlanToCommunityDelete = function(BonusPlanToCommunityID)
	{
		var objBonusPlan = new Object();
		objBonusPlan.BonusPlanToCommunityID = BonusPlanToCommunityID;
		objBonusPlan.BonusPlanID = this.BonusPlanID;

		var ajaxMethodURL = location.protocol + '//' + location.host + '/Application/BonusPlan/AJAXData/BonusPlanAJAX.svc/BonusPlanToCommunityDelete';

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
				this.ConfiguredCommunityList = JSON.parse(msg);
				this.RenderTable();
			}.bind(self),
			error: function (XMLHttpRequest, textStatus, errorThrown)
			{
				alert('Error :' + XMLHttpRequest.responseText);
			}
		});
	}
	
	// Renders the Bonus Plan to Community HTML at ContainerID
	this.Render = function()
	{
		var html = "";
		
		html += "<div class='form-group row'>\n";
        html += "    <label class='col-xs-12 col-sm-2 col-md-2 col-lg-2 control-label col-form-label' for='BonusPlanCommunity'>Community:</label>\n";
        html += "    <div class='col-xs-12 col-sm-6 col-md-6 col-lg-6'>\n";
		html += "        <select id='BonusPlanCommunity' class='form-control' onchange='EditCommunity(this.value);'></select>\n";
        html += "    </div>\n";
        html += "</div>\n";
        html += "<div class='form-group row'>\n";
        html += "    <label class='col-xs-12 col-sm-2 col-md-2 col-lg-2 control-label col-form-label' for='CommunityBeginDt'>Begin Date:</label>\n";
        html += "    <div class=' col-xs-12 col-sm-4 col-md-4 col-lg-4'>\n";
        html += "        <div class='input-group date'>\n";
        html += "            <input type='text' id='CommunityBeginDt' data-date-format='mm/dd/yyyy' placeholder='MM/DD/YYYY' class='datepicker form-control' runat='server' />\n";
        html += "            <label for='CommunityBeginDt' class='input-group-addon btn'>\n";
        html += "                <span class='glyphicon glyphicon-calendar' style='font-size: 16px;' title='calendar'></span>\n";
        html += "            </label>\n";
        html += "        </div>\n";
        html += "    </div>\n";
		html += "</div>\n";
		html += "<div class='form-group row'>\n";
		html += "    <label class='col-xs-12 col-sm-2 col-md-2 col-lg-2 control-label col-form-label' for='CommunityEndDt'>End Date:</label>\n";
        html += "    <div class=' col-xs-12 col-sm-4 col-md-4 col-lg-4'>\n";
        html += "        <div class='input-group date'>\n";
        html += "            <input type='text' id='CommunityEndDt' data-date-format='mm/dd/yyyy' placeholder='MM/DD/YYYY' class='datepicker form-control' runat='server' />\n";
        html += "            <label for='CommunityEndDt' class='input-group-addon btn'>\n";
        html += "                <span class='glyphicon glyphicon-calendar' style='font-size: 16px;' title='calendar'></span>\n";
        html += "            </label>\n";
        html += "        </div>\n";
        html += "    </div>\n";
        html += "</div>\n";
        html += "<div class='row ControlButtonRow'>\n";
        html += "    <div class=' col-xs-2 col-sm-2 col-md-2 col-lg-2'></div>\n";
        html += "    <div class=' col-xs-12 col-sm-6 col-md-6 col-lg-6'>\n";
        html += "        <input id='AddCommunityButton' type='button' value='Add' class='btn btn-primary pull-right ' onclick='VerifyCommunity();' />\n";
        html += "    </div>\n";
        html += "</div>\n";
        html += "<div class='row'>\n";
        html += "    <div class='col-xs-6 col-sm-6 col-md-6 col-lg-6'>\n";
        html += "        <h4 id='CommunityTitle'>Communities Configured</h4>\n";
        html += "    </div>\n";
        html += "    <div class='col-xs-6 col-sm-6 col-md-6 col-lg-6'>\n";
        html += "        <div class='input-group'>\n";
        html += "            <input class='form-control input-sm pull-right' id='CommunityFilter' type='search' placeholder='To filter your results, enter your search criteria here' />\n";
        html += "            <span class='input-group-addon'><span class='glyphicon glyphicon-search' title='search'></span></span>\n";
        html += "        </div>\n";
        html += "    </div>\n";
        html += "    <div class='col-xs-12 col-sm-12 col-md-12 col-lg-12'>\n";
        html += "        <div class='table-responsive'>\n";
        html += "            <table class='tablesorter table-striped table table-condensed' id='CommunityGrid'>\n";
        html += "                <thead>\n";
        html += "                    <tr class='TableRowBorderTop'>\n";
        html += "                        <th class='headerSort'>Community</th>\n";
        html += "                        <th class='headerSort'>Begin Dt</th>\n";
		html += "                        <th class='headerSort'>End Dt</th>\n";
        html += "                        <th class='headerSort'>&nbsp;</th>\n";
        html += "                        <th class='headerSort'>&nbsp;</th>\n";
        html += "                    </tr>\n";
        html += "                </thead>\n";
        html += "                <tbody>\n";
        html += "                    <tr>\n";
        html += "                        <td colspan='5'>\n";
        html += "                            <img src='/images/progressSnapIn.gif' alt='Loading' />&nbsp;Please wait downloading...</td>\n";
        html += "                    </tr>\n";
        html += "                </tbody>\n";
        html += "            </table>\n";
        html += "        </div>\n";
        html += "    </div>\n";
        html += "</div>\n";

		// Set the container with the generated HTML
		$("#" + this.ContainerID).html(html);
		
		// Set up table filter
		$.tablefilter({
                inputElement: "#CommunityFilter",
                tableElement: "#CommunityGrid"
            });
		
		//INVOKE CALENDAR CONTROL FOR Start and End Dates
		$('.datepicker').datepicker({
			clearBtn: true,
			autoclose: true,
			todayHighlight: true,
		});
		
		// Load the community list with available communities
		var communityHtml = "<option selected='selected' value=''>Select One...</option>\n";
		
		CommunityList.forEach(function(community)
		{
			communityHtml += "<option value='" + community.CommunityNumber + "'>" + community.CommunityName + "</option>\n";
		});
		
		$("#BonusPlanCommunity").html(communityHtml); 
	}

	// Renders the table data at #CommunityGrid
	this.RenderTable = function()
	{
		var html = "";
		
		if (this.ConfiguredCommunityList.length == 0)
		{
			html += "<tr>\n";
			html += "    <td colspan='5'>No data to display.</td>\n";
			html += "</tr>\n";
		}
		else
		{
			this.ConfiguredCommunityList.forEach(function (community)
			{
				html += "<tr>";
				html += "  <td>" + community.Community + "</td>";
				html += "  <td>" + community.BeginDt + "</td>";
				html += "  <td>" + community.EndDt + "</td>";
				html += "  <td><input class='btn btn-primary btn-xs' type='button' value='Edit' onClick=\"EditCommunity('" + community.CommunityNumber + "');\"></input></td>";
				html += "  <td><i class=\"fa fa-2x fa-trash-o\" title=\"delete\"  aria-hidden=\"true\" onClick=\"DeleteCommunity('" + community.Community + "', '" + community.BonusPlanToCommunityID + "')\"></i></td>";
				html += "</tr>";
			});
		}
		
		$("#CommunityGrid>tbody").html(html);

		$("#CommunityGrid").tablesorter({
			debug: false,
			cssHeader: "headerSort",
			cssDesc: "headerSortDown",
			cssAsc: "headerSortUp"
		});
	}
}