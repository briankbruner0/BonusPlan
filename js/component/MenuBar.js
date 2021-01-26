//  Menu Bar
//  A component which displays the current cluster/community information
//  and provides a month drop down 
function MenuBar(MenuType, ContainerID, CommunityPath, ClusterPath) {
	
	var self = this;
	this.ContainerID = ContainerID; // ID of container to render menu bar
	this.MenuType = MenuType; // "Cluster" is being used for the cluster dashboard, otherwise "Community"
	this.CommunityPath = CommunityPath == null ? "Default.aspx" : CommunityPath; //11.08.2017 Amanda marburger providing the ability to use a unique path for the community navigation
	this.ClusterPath = ClusterPath == null ? "BonusPlanClusterDashboard.aspx" : ClusterPath; //11.08.2017 Amanda marburger providing the ability to use a unique path for the cluster navigation

	this.ClusterCommunityList = null;  // Populated by data provided by Cluster.js
	this.MenuBarMonthList = null;  // Populated by data provided by BonusPlan.js
	
	// Gets the data necessary for the Menu Bar by Cluster ID
	this.MenuBarByOperationClusterIDGet = function(OperationClusterID)
	{
		var cluster = CommunityByOperationClusterIDGet(OperationClusterID).then(function (result)
		{
			try
			{
				this.ClusterCommunityList = JSON.parse(result);
			}
			catch(err){}
		}.bind(self)); // binding to self so the callback has correct scope to object variables
		
		var menu = this.MenuBarMonthGet();
		
		$.when(cluster, menu).done(function(){
			this.Render();
		}.bind(self)); // binding to self so the callback has correct scope to object variables
	}
	
	// Gets the data necessary for the Menu Bar by Community Number
	this.MenuBarByCommunityNumberGet = function(CommunityNumber)
	{
		var cluster = ClusterByCommunityNumberGet(CommunityNumber).then(function (result)
		{
			try
			{
				this.ClusterCommunityList = JSON.parse(result);
			}
			catch(err){}
		}.bind(self)); // binding to self so the callback has correct scope to object variables
		
		var menu = this.MenuBarMonthGet();
		
		$.when(cluster, menu).done(function(){
			this.Render();
		}.bind(self)); // binding to self so the callback has correct scope to object variables
	}

	// Gets the list of months to display in the menu bar
	this.MenuBarMonthGet = function()
	{
		var ajaxMethodURL = location.protocol + '//' + location.host + '/Application/BonusPlan/AJAXData/BonusPlanAJAX.svc/MenuBarMonthGet';

		return $.ajax({
			type: 'POST',
			url: ajaxMethodURL,
			cache: false,
			data: '',
			contentType: 'application/json; charset=utf-8',
			dataType: 'json',
			success: function(msg)
			{
				try
				{
					this.MenuBarMonthList = JSON.parse(msg);
				}
				catch(err){}
			}.bind(self),
			error: function (XMLHttpRequest, textStatus, errorThrown)
			{
				alert('Error :' + XMLHttpRequest.responseText);
			}
		});
	}
	
	// Retrieves the cluster and community information for a given operation cluster ID
	var CommunityByOperationClusterIDGet = function(OperationClusterID)
	{
		var objCluster = new Object();
		objCluster.OperationClusterID = OperationClusterID;

		var ajaxMethodURL = location.protocol + '//' + location.host + '/Application/BonusPlan/AJAXData/BonusPlanAJAX.svc/CommunityByOperationClusterIDGet';

		return $.ajax({
			type: 'POST',
			url: ajaxMethodURL,
			cache: false,
			data: JSON.stringify(objCluster),
			contentType: 'application/json; charset=utf-8',
			dataType: 'json',
			error: function (XMLHttpRequest, textStatus, errorThrown)
			{
				alert('Error :' + XMLHttpRequest.responseText);
			}
		});
	}
	
	// Retrieves the cluster and community information for a given operation community number
	var ClusterByCommunityNumberGet = function(CommunityNumber)
	{
		var objCluster = new Object();
		objCluster.CommunityNumber = CommunityNumber;

		var ajaxMethodURL = location.protocol + '//' + location.host + '/Application/BonusPlan/AJAXData/BonusPlanAJAX.svc/ClusterByCommunityNumberGet';

		return $.ajax({
			type: 'POST',
			url: ajaxMethodURL,
			cache: false,
			data: JSON.stringify(objCluster),
			contentType: 'application/json; charset=utf-8',
			dataType: 'json',
			error: function (XMLHttpRequest, textStatus, errorThrown)
			{
				alert('Error :' + XMLHttpRequest.responseText);
			}
		});
	}

	// Updates the month on the drop down list
	// DisplayMonth is of the format "Oct 2016"
	// DataMonth is of the format "10/15/2016"
	window.MonthUpdate = function(DisplayMonth, DataMonth)
	{
		$("#SelectedMonth").html(DisplayMonth);
		
		$(document).trigger('monthChange', [DataMonth]);
	}
	
	// Renders the Menu Bar HTML at ContainerID using jQuery templates
	this.Render = function()
	{
		// Don't render if the data isn't loaded
		if (this.ClusterCommunityList === null || this.MenuBarMonthList ===  null)
		{ return;}
		
		var menuBarHTML = "";
		
		menuBarHTML += "<div class='panel panel-default'>\n";
		menuBarHTML += "	<div class='panel-body'>\n";
		menuBarHTML += "		<div class='row'>\n";
		menuBarHTML += "			<div class='col-xs-8 col-sm-9 col-md-9 col-lg-9'>\n";
		menuBarHTML += "				<span class='pull-left'>\n";
		
		// Generate the community & cluster information
		if (this.ClusterCommunityList.Cluster[0].OperationClusterID) // Display for a cluster
		{

		    //11.08.2017 Amanda Marburger - adjusted to use the unique path pass for the community navigation
		    menuBarHTML += "<a href='" + self.ClusterPath + "?OperationClusterID=" + this.ClusterCommunityList.Cluster[0].OperationClusterID + "' style='text-decoration:none; color: #777' id='clusterHome'>\n";
			
			if (this.MenuType == "Cluster")  // Display cluster in bold for the Cluster Dashboard
			{
			    menuBarHTML += "<span class='glyphicon glyphicon-home'></span> <span class='communityCluster navCurrentCommunity'>\n&nbsp;" + this.ClusterCommunityList.Cluster[0].OperationCluster + "</span>";
			}
			else // Display cluster normally for Community Dashboard
			{
			    menuBarHTML += "<span class='glyphicon glyphicon-home'></span> <span class='communityCluster'>\n&nbsp;" + this.ClusterCommunityList.Cluster[0].OperationCluster + "</span>";
			}
			
			this.ClusterCommunityList.Community.forEach(function(community){
				
			    menuBarHTML += "<span class='separator'>&#8226;</span>\n";
                //11.08.2017 Amanda Marburger - adjusted to use the unique path pass for the community navigation
			    menuBarHTML += "<a href='" + self.CommunityPath + "?CommunityNumber=" + community.CommunityNumber + "' style='text-decoration:none; color: #777'>";
				
				if (community.CurrentCommunityFlg == "1") // Bold this community if it's the current community
				{	
					menuBarHTML += "<span class='navCurrentCommunity communityCluster'>" + community.CommunityName + " (" + community.CommunityNumber + ")" + "</span>";
				}
				else
				{
					menuBarHTML += "<span class='communityCluster'>" + community.CommunityName + " (" + community.CommunityNumber + ")" + "</span>";
				}
			});
		}
		else  // Display for a single non-cluster community
		{
		    //11.08.2017 Amanda Marburger - adjusted to use the unique path pass for the community navigation
		    menuBarHTML += "<a href='" + self.CommunityPath + "?CommunityNumber=" + this.ClusterCommunityList.Community[0].CommunityNumber + "' style='text-decoration:none; color: #777' id='clusterHome'>";
			menuBarHTML += "<span class='glyphicon glyphicon-home navCurrentCommunity'></span> <span class='communityCluster'>&nbsp;" + this.ClusterCommunityList.Community[0].CommunityName + "</span>";
		}
		
		menuBarHTML += "			</a>\n";
		menuBarHTML += "		</span>\n";
		menuBarHTML += "	</div>\n";
	
		// Generate the month drop down
		menuBarHTML += "<div class='col-xs-4 col-sm-3 col-md-3 col-lg-3'>\n";
		menuBarHTML += "	<ul class='nav navbar-nav pull-right'>\n";
		menuBarHTML += "		<li class='dropdown select' id='DateFilter'>\n";
		menuBarHTML += "			<a href='#' class='dropdown-toggle bonusPlanDatePicker' data-toggle='dropdown'>\n";
		menuBarHTML += "				<span class='selected' id='SelectedMonth'>" + this.MenuBarMonthList[0].EffectiveDtDisplay + "</span>&nbsp;<span class='caret'></span>\n";
		menuBarHTML += "			</a>\n";
		menuBarHTML += "			<ul class='dropdown-menu option' role='menu'>\n"; 
		
		this.MenuBarMonthList.forEach(function(month){
			menuBarHTML += "<li data-id=" + month.EffectiveDt + " class='dashboard-view' onclick=\"MonthUpdate('" + month.EffectiveDtDisplay + "' , '" + month.EffectiveDt + "');\"><a href='#'>" + month.EffectiveDtDisplay + "</a></li>\n";
		});

		menuBarHTML += "			</ul>\n";
		menuBarHTML += "		</li>\n";
		menuBarHTML += "	</ul>\n";
		menuBarHTML += "</div>\n";
		menuBarHTML += "</div>\n";
		
		// Set the container with the generated HTML
		$("#" + this.ContainerID).html(menuBarHTML);
	}		
}

