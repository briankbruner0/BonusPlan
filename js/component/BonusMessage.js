//  Bonus Message
//  A component which displays a message to the user indicating
//  their potential bonus for the month.
function BonusMessage(ContainerID){
	
	var self = this;
	this.ContainerID = ContainerID; // ID of container to render menu bar
	this.FirstName = null;
	this.PotentialBonus = null;
	this.Month = null;
	this.BonusEligibleFlg = null;
	this.OperationCluster = null;
	this.Percentage = null;
	
	// Listen for monthChange events generated from the MenuBar
	$(document).on("monthChange", function (event, month)
    {
		$("#" + this.ContainerID + " > .panel > .panel-body").html("<img src='../../../../images/progressSnapIn.gif' alt='Loading' />&nbsp;Please wait downloading...");
	
    }.bind(self));
	
	// Listen for moveInDataReady events generated from the MoveInRevenuePanel
	$(document).on("moveInDataReady", function (event, CommunityList, Cluster)
    {
		var operationCluster = null;
		var potentialBonus = null;
		
		if (Cluster.length > 0)
		{
			operationCluster = Cluster[0].OperationCluster;
			potentialBonus = Cluster[0].PotentialBonus;
		}
		else
		{
			potentialBonus = CommunityList[0].PotentialBonus;
		}
		
		this.BonusMessageGet( CommunityList[0].FirstName,
						 potentialBonus,
						 CommunityList[0].Month,
						 CommunityList[0].BonusEligibleFlg,
						 operationCluster,
						 CommunityList[0].PercentageEffective);
    }.bind(self));
	
	// Gets the data necessary for the Move In Revenue
	this.BonusMessageGet = function(FirstName, PotentialBonus, Month, BonusEligibleFlg, OperationCluster, Percentage)
	{
		// Update attributes
		this.FirstName = FirstName;
		this.PotentialBonus = PotentialBonus;
		this.Month = Month;
		this.BonusEligibleFlg = BonusEligibleFlg;
		this.OperationCluster = OperationCluster;
		this.Percentage = Percentage;
		
		this.Render();
	}
	
	// Renders the Bonus Message at ContainerID
	this.Render = function()
	{
		var html = "";
		
		html += "<div class='panel panel-default'>\n";
        html +=	"	<div class='panel-body bonusMessage'>\n";
        html +=	"		<h4 class='pull-left'>\n";
		
		if (this.BonusEligibleFlg == "0")
		{
			if (this.OperationCluster)
			{
				html +=	"<span class='emphasis'>"+ this.FirstName + "</span>, you are not bonus eligible for the " + this.OperationCluster +" Cluster.</span>\n";
			}
			else
			{
				html +=	"<span class='emphasis'>"+ this.FirstName + "</span>, you are not bonus eligible for this community.</span>\n";
			}
		}
		else
		{
			if (this.OperationCluster)
			{
				html +=	"<span class='emphasis'>"+ this.FirstName + "</span>, you could receive a <span class='emphasis'>" + this.PotentialBonus +" bonus</span> for sales in the " + this.OperationCluster + " Cluster in the month of <span class='emphasis'>" + this.Month +"</span>.\n";
			}
			else
			{
				html +=	"<span class='emphasis'>"+ this.FirstName + "</span>, you could receive a <span class='emphasis'>" + this.PotentialBonus +" bonus</span> for sales made in the month of <span class='emphasis'>" + this.Month +"</span>.\n";
				html += "<br/><span id='BonusMessagePercentage'>You are bonused at " + this.Percentage +"% for this community.</span>\n";
			}
		}
		
        html +=	"		</h4>\n";              
        html +=	"	</div>\n";
        html += "</div>\n";

		// Set the container with the generated HTML
		$("#" + this.ContainerID).html(html);
	}		
}