//  Bonus Plan Ledger Entry Panel
//  Used for editing and viewing configured ledger entries
function BonusPlanLedgerEntryPanel(ContainerID){
	
	var self = this;
	this.ContainerID = ContainerID; // ID of container to render menu bar
	this.BonusPlanID = null;
	this.RevenueEntryTypeList = null;
	this.LedgerEntryList = null;
	this.ConfiguredLedgerEntryList = null
		
	// Gets the revenue entries for this Bonus Plan
	this.BonusPlanToRevenueDetailGet = function(BonusPlanID)
	{
		this.BonusPlanID = BonusPlanID;
		
		var objBonusPlan = new Object();
		objBonusPlan.BonusPlanID = this.BonusPlanID;
		
		// Load spinner
		$("#" + this.ContainerID).html("<img src='../../../../images/progressSnapIn.gif' alt='Loading' />&nbsp;Please wait downloading...");
		
		// Set up AJAX request
		var objBonusPlan = new Object();
		objBonusPlan.BonusPlanID = BonusPlanID;
	
		var ajaxMethodURL = location.protocol + '//' + location.host + '/Application/BonusPlan/AJAXData/BonusPlanAJAX.svc/BonusPlanToRevenueDetailGet';

		var ledgerEntry = $.ajax({
			type: 'POST',
			url: ajaxMethodURL,
			cache: false,
			data: JSON.stringify(objBonusPlan),
			contentType: 'application/json; charset=utf-8',
			dataType: 'json',
			success: function(msg){
				try
				{
					this.ConfiguredLedgerEntryList = JSON.parse(msg);
				}
				catch(err){}
			}.bind(self),
			error: function (XMLHttpRequest, textStatus, errorThrown)
			{
				alert('Error :' + XMLHttpRequest.responseText);
			}
		});
		
		// Get the list of all revenue entry types if necessary
		if (this.RevenueEntryTypeList === null)
		{
			var revenueEntry = this.RevenueEntryTypeGet();
			$.when(revenueEntry, ledgerEntry).done(function(){
				this.Render();
				this.RenderRevenueTable();
			}.bind(self) )
		}
		else
		{
			$.when(ledgerEntry).done( function(){
				this.Render();
				this.RenderRevenueTable();
			}.bind(self) )
		}
	}
	
	// Retrieves all available revenue entry types
	this.RevenueEntryTypeGet = function()
	{
		var objRevenueEntry = new Object();

		var ajaxMethodURL = location.protocol + '//' + location.host + '/Application/BonusPlan/AJAXData/BonusPlanAJAX.svc/RevenueEntryTypeGet';

		return $.ajax({
			type: 'POST',
			url: ajaxMethodURL,
			cache: false,
			data: JSON.stringify(objRevenueEntry),
			contentType: 'application/json; charset=utf-8',
			dataType: 'json',
			success: function (msg)
			{
				this.RevenueEntryTypeList = JSON.parse(msg)
			}.bind(self),
			error: function (XMLHttpRequest, textStatus, errorThrown)
			{
				alert('Error :' + XMLHttpRequest.responseText);
			}
		});
	}
	
	// Retrieves all available ledger entry types
	LedgerEntryTypeGet = function()
	{
		if ($("#RevenueEntryTypeID").val().length > 0)
		{
			var objRevenueEntry = new Object();
			objRevenueEntry.RevenueEntryTypeID = $("#RevenueEntryTypeID").val();

			var ajaxMethodURL = location.protocol + '//' + location.host + '/Application/BonusPlan/AJAXData/BonusPlanAJAX.svc/LedgerEntryTypeGet';

			$.ajax({
				type: 'POST',
				url: ajaxMethodURL,
				cache: false,
				data: JSON.stringify(objRevenueEntry),
				contentType: 'application/json; charset=utf-8',
				dataType: 'json',
				beforeSend:	function(){$("#LedgerEntryGrid>tbody").html("<img src='../../../../images/progressSnapIn.gif' alt='Loading' />&nbsp;Please wait downloading...");},
				success: function (msg)
				{
					this.LedgerEntryList = JSON.parse(msg);
					this.RenderLedgerTable();
				}.bind(self),
				error: function (XMLHttpRequest, textStatus, errorThrown)
				{
					alert('Error :' + XMLHttpRequest.responseText);
				}
			});
		}
	}.bind(self)
	
	// Inserts a ledger entry from the configuration
	window.LedgerEntryInsert = function(LedgerEntryID)
	{
		this.BonusPlanLedgerEntryInsert(LedgerEntryID);
	}.bind(self)
	
	// Inserts a ledger entry into the configuration and returns an updated list of ledger entries
    this.BonusPlanLedgerEntryInsert = function(LedgerEntryID)
	{
		var objBonusPlan = new Object();
		objBonusPlan.BonusPlanID = this.BonusPlanID;
		objBonusPlan.LedgerEntryID = LedgerEntryID;
		objBonusPlan.RevenueEntryTypeID = $("#RevenueEntryTypeID").val();
		objBonusPlan.Username = $("#Username").val();

		var ajaxMethodURL = location.protocol + '//' + location.host + '/Application/BonusPlan/AJAXData/BonusPlanAJAX.svc/LedgerEntryTypeInsert';

		$.ajax({
			type: 'POST',
			url: ajaxMethodURL,
			cache: false,
			data: JSON.stringify(objBonusPlan),
			contentType: 'application/json; charset=utf-8',
			dataType: 'json',
			beforeSend: function(){$("#RevenueAccountGrid>tbody").html("<img src='../../../../images/progressSnapIn.gif' alt='Loading' />&nbsp;Please wait downloading...");},
			success: function (msg)
			{
				this.ConfiguredLedgerEntryList = JSON.parse(msg);
				this.RenderRevenueTable();
				this.RenderLedgerTable();
			}.bind(self),
			error: function (XMLHttpRequest, textStatus, errorThrown)
			{
				alert('Error :' + XMLHttpRequest.responseText);
			}
		});
	}
	
	// Deletes a ledger entry from the configuration
	window.LedgerEntryDelete = function(RevenueEntryTypeToLedgerEntryID, LedgerEntry)
	{
		if (confirm("Are you sure you want to delete " + LedgerEntry + "?"))
		{
			this.BonusPlanLedgerEntryDelete(RevenueEntryTypeToLedgerEntryID);
		}
	}.bind(self)
		
	// Deletes a ledger entry from the configuration and returns an updated list of ledger entries
    this.BonusPlanLedgerEntryDelete = function(RevenueEntryTypeToLedgerEntryID)
	{
		var objBonusPlan = new Object();
		objBonusPlan.BonusPlanID = this.BonusPlanID;
		objBonusPlan.RevenueEntryTypeToLedgerEntryID = RevenueEntryTypeToLedgerEntryID;

		var ajaxMethodURL = location.protocol + '//' + location.host + '/Application/BonusPlan/AJAXData/BonusPlanAJAX.svc/LedgerEntryTypeDelete';

		$.ajax({
			type: 'POST',
			url: ajaxMethodURL,
			cache: false,
			data: JSON.stringify(objBonusPlan),
			contentType: 'application/json; charset=utf-8',
			dataType: 'json',
			beforeSend: function(){$("#RevenueAccountGrid>tbody").html("<img src='../../../../images/progressSnapIn.gif' alt='Loading' />&nbsp;Please wait downloading...");},
			success: function (msg)
			{
				this.ConfiguredLedgerEntryList = JSON.parse(msg);
				this.RenderRevenueTable();
				this.RenderLedgerTable();
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
        html += "    <label class='col-xs-3 col-sm-2 col-md-2 col-lg-2 control-label col-form-label' for='RevenueEntryTypeID'>Entry Type:</label>\n";
        html += "    <div class='col-xs-9 col-sm-4 col-md-4 col-lg-4'>\n";
		html += "    <select id='RevenueEntryTypeID' class='form-control' onchange='LedgerEntryTypeGet()'></select>\n";
        html += "    </div>\n";
        html += "</div>\n";
        html += "<div class='row' style='margin-top: 10px;'>\n";
        html += "	<div class='col-xs-12 col-sm-6 col-md-6 col-lg-6'>\n";
		html += " 		<div class='row'>\n";
		html += "			<div class='col-xs-6 col-sm-6 col-md-6 col-lg-6'>\n";
        html += "				<h4 id='RevenueAccountTitle'>Ledger Entries</h4>\n";
		html += "			</div>\n";
		html += " 			<div class='col-xs-6 col-sm-6 col-md-6 col-lg-6'>\n";
        html += "				<div class='input-group'>\n";
        html += "            		<input class='form-control input-sm pull-right' id='LedgerEntryFilter' type='search' placeholder='To filter your results, enter your search criteria here' />\n";
        html += "            		<span class='input-group-addon'><span class='glyphicon glyphicon-search' title='search'></span></span>\n";
        html += "        		</div>\n";
        html += "    		</div>\n";
		html += "			<div class='col-xs-12 col-sm-12 col-md-12 col-lg-12'>\n";
		html += "				<div class='table-responsive'>\n";
		html += "					<table class='tablesorter table-striped table table-condensed' id='LedgerEntryGrid'>\n";
		html += "						<thead>\n";
		html += "							<tr class='TableRowBorderTop'>\n";
		html += "								<th>ID</th>\n";
		html += "								<th>Ledger Entry</th>\n";
		html += "								<th>&nbsp;</th>\n";
		html += "							</tr>\n";
		html += "						</thead>\n";
		html += "						<tbody></tbody>\n";
		html += "       			</table>\n";
		html += "       		</div>\n";
        html += "       	</div>\n";
		html += "		</div>\n";
        html += "  	</div>\n";
		html += "	<div class='col-xs-12 col-sm-6 col-md-6 col-lg-6'>\n";
		html += " 		<div class='row'>\n";
		html += "			<div class='col-xs-6 col-sm-6 col-md-6 col-lg-6'>\n";
        html += "				<h4 id='RevenueAccountTitle'>Revenue Accounts</h4>\n";
		html += "			</div>\n";
		html += " 			<div class='col-xs-6 col-sm-6 col-md-6 col-lg-6'>\n";
        html += "				<div class='input-group'>\n";
        html += "            		<input class='form-control input-sm pull-right' id='RevenueAccountFilter' type='search' placeholder='To filter your results, enter your search criteria here' />\n";
        html += "            		<span class='input-group-addon'><span class='glyphicon glyphicon-search' title='search'></span></span>\n";
        html += "        		</div>\n";
        html += "    		</div>\n";
		html += "			<div class='col-xs-12 col-sm-12 col-md-12 col-lg-12'>\n";
		html += "				<div class='table-responsive'>\n";
		html += "					<table class='tablesorter table-striped table table-condensed' id='RevenueAccountGrid'>\n";
		html += "						<thead>\n";
		html += "							<tr class='TableRowBorderTop'>\n";
		html += "								<th>ID</th>\n";
		html += "								<th>Event Type</th>\n";
		html += "								<th>Ledger Entry</th>\n";
		html += "								<th>&nbsp;</th>\n";
		html += "							</tr>\n";
		html += "						</thead>\n";
		html += "						<tbody></tbody>\n";
		html += "       			</table>\n";
		html += "       		</div>\n";
        html += "       	</div>\n";
		html += "		</div>\n";
        html += "  	</div>\n";
		html += "</div>\n";

		// Set the container with the generated HTML
		$("#" + this.ContainerID).html(html);
		
		// Set up table filter
		$.tablefilter({
                inputElement: "#RevenueAccountFilter",
                tableElement: "#RevenueAccountGrid"
            });
			
		$.tablefilter({
			inputElement: "#LedgerEntryFilter",
			tableElement: "#LedgerEntryGrid"
		});
		
		//INVOKE CALENDAR CONTROL FOR Start and End Dates
		$('.datepicker').datepicker({
			clearBtn: true,
			autoclose: true,
			todayHighlight: true,
		});
		
		// Load the revenue entry list with available items
		var revenueHtml = "<option selected='selected' value=''>Select One...</option>\n";
		
		this.RevenueEntryTypeList.forEach(function(revenue)
		{
			revenueHtml += "<option value='" + revenue.RevenueEntryTypeID + "'>" + revenue.RevenueEntryType + "</option>\n";
		});
		
		$("#RevenueEntryTypeID").html(revenueHtml); 
	}

	// Renders the table data at #RevenueAccountGrid
	this.RenderRevenueTable = function()
	{
		var html = "";
		
		if (this.ConfiguredLedgerEntryList.length == 0)
		{
			html += "<tr>\n";
			html += "    <td colspan='4'>No data to display.</td>\n";
			html += "</tr>\n";
		}
		else
		{
			this.ConfiguredLedgerEntryList.forEach(function (ledger)
			{
				html += "<tr>";
				html += "  <td>" + ledger.LedgerEntryID + "</td>";
				html += "  <td>" + ledger.RevenueEntryType + "</td>";
				html += "  <td>" + ledger.LedgerEntry + "</td>";
				html += "  <td><i class=\"fa fa-2x fa-trash-o\" title=\"delete\"  aria-hidden=\"true\" onClick=\"LedgerEntryDelete('" + ledger.RevenueEntryTypeToLedgerEntryID + "', '" + ledger.LedgerEntry + "')\"></i></td>";
				html += "</tr>";
			});
		}
		
		$("#RevenueAccountGrid>tbody").html(html);

		$("#RevenueAccountGrid").tablesorter({
			debug: false,
			headers: {3:{sorter:false}},
			cssHeader: "headerSort",
			cssDesc: "headerSortDown",
			cssAsc: "headerSortUp"
		});
	}
	
	// Renders data at #LedgerEntryGrid
	this.RenderLedgerTable = function()
	{
		var html = "";
		var isLedgerEntryConfigured = false;
		
		if (this.LedgerEntryList.length == 0)
		{
			html += "<tr>\n";
			html += "    <td colspan='5'>No data to display.</td>\n";
			html += "</tr>\n";
		}
		else
		{
			this.LedgerEntryList.forEach(function (ledger)
			{
				html += "<tr>";
				html += "  <td>" + ledger.LedgerEntryID + "</td>";
				html += "  <td>" + ledger.LedgerEntry + "</td>";
				
				isLedgerEntryConfigured = false;
				
				// If the ledger entry is already configured, display the Add button
				this.ConfiguredLedgerEntryList.forEach(function(configuredLedgerEntry){
					if (configuredLedgerEntry.RevenueEntryTypeID == $("#RevenueEntryTypeID").val() &&
						configuredLedgerEntry.LedgerEntryID == ledger.LedgerEntryID)
						{
							isLedgerEntryConfigured = true;
						}
				}.bind(this));
				
				if (isLedgerEntryConfigured)
				{
					html += "  <td><i class=\"fa fa-2x fa-check\" title=\"LedgerConfigured\" aria-hidden=\"true\"></i></td>";
				}
				else
				{
					html += "  <td><input class='btn btn-primary btn-xs' type='button' value='Add' onClick=\"LedgerEntryInsert('" + ledger.LedgerEntryID + "');\"></input></td>";
				}
				
				html += "</tr>";
			}.bind(self));
		}
		
		$("#LedgerEntryGrid>tbody").html(html);

		$("#LedgerEntryGrid").tablesorter({
			debug: false,
			cssHeader: "headerSort",
			cssDesc: "headerSortDown",
			cssAsc: "headerSortUp",
			textExtraction: function(s){
				if ($(s).find('input').length > 0) return "button";
				if ($(s).find('i').length > 0) return "check";
				return $(s).text();
			}
		});
	}
}