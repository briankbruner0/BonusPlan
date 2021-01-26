using AtriaEM;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Web.Script.Serialization;

namespace BonusPlan
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class IncentivizerAJAX
    {
        // Retrieves the quarterly and annual goals, percentages and bonuses used by Incentivizer
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string IncentivizerGoalGet(string Username, string BonusPlanID, string CommunityNumber, string OperationClusterID, string EffectiveDT)
        {
            DataSet ds = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.IncentivizerGoalGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@CommunityNumber", SqlDbType.VarChar);
                    Command.Parameters["@CommunityNumber"].Value = CommunityNumber.NullIfEmpty();

                    Command.Parameters.Add("@OperationClusterID", SqlDbType.VarChar);
                    Command.Parameters["@OperationClusterID"].Value = OperationClusterID.NullIfEmpty();

                    Command.Parameters.Add("@Username", SqlDbType.VarChar);
                    Command.Parameters["@Username"].Value = Username;

                    Command.Parameters.Add("@BonusPlanID", SqlDbType.VarChar);
                    Command.Parameters["@BonusPlanID"].Value = BonusPlanID;

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(ds);
                    }
                }
            }

            var rsCommunityGoal = (from community in ds.Tables[0].AsEnumerable()
                                   select new
                                   {
                                       CommunityNumber = community["CommunityNumber"],
                                       CommunityID = community["CommunityID"],
                                       Username = community["Username"],
                                       Percentage = community["Percentage"],
                                       PresidentClubGoal = community["PresidentClubGoal"],
                                       PresidentClubPercentage = community["PresidentClubPercentage"],
                                       PresidentClubBonus = community["PresidentClubBonus"],
                                       PinnacleAwardGoal = community["PinnacleAwardGoal"],
                                       PinnacleAwardPercentage = community["PinnacleAwardPercentage"],
                                       PinnacleAwardBonus = community["PinnacleAwardBonus"],
                                   });

            var rsClusterGoal = (from cluster in ds.Tables[1].AsEnumerable()
                                 select new
                                 {
                                     OperationClusterID = cluster["OperationClusterID"],
                                     OperationCluster = cluster["OperationCluster"],
                                     FiscalQuarter = cluster["FiscalQuarter"],
                                     Username = cluster["Username"],
                                     Percentage = cluster["Percentage"],
                                     PresidentClubGoal = cluster["PresidentClubGoal"],
                                     PresidentClubBonus = cluster["PresidentClubBonus"],
                                     PinnacleAwardGoal = cluster["PinnacleAwardGoal"],
                                     PinnacleAwardBonus = cluster["PinnacleAwardBonus"],
                                 });

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            return jsSerializer.Serialize(new { Community = rsCommunityGoal, Cluster = rsClusterGoal });
        }

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string IncentivizerByBonusPlanGet(string CommunityNumber, string Username)
        {
            DataSet ds = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.IncentivizerByBonusPlanGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@CommunityNumber", SqlDbType.VarChar);
                    Command.Parameters["@CommunityNumber"].Value = CommunityNumber;

                    Command.Parameters.Add("@Username", SqlDbType.VarChar);
                    Command.Parameters["@Username"].Value = Username;

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(ds);
                    }
                }
            }

            var rsCommunityGoal = (from community in ds.Tables[0].AsEnumerable()
                                   select new
                                   {
                                       CommunityNumber = community["CommunityNumber"],
                                       CommunityName = community["CommunityName"],
                                       Username = community["Username"],
                                       EffectiveDt = community["EffectiveDt"],
                                       BonusPlanID = community["BonusPlanID"],
                                       OperationClusterID = community["OperationClusterID"],
                                       TierID = community["TierID"],
                                       CommunityGoal = community["CommunityGoal"],
                                       CommunityTier1Goal = community["CommunityTier1Goal"],
                                       CommunityTier2Goal = community["CommunityTier2Goal"],
                                       CommunityTier3Goal = community["CommunityTier3Goal"],
                                       CommunityRevenue = community["CommunityRevenue"],
                                       BonusAmount = community["BonusAmount"],
                                       ProjectedRevenue = community["ProjectedRevenue"],
                                       GoalRemaining = community["GoalRemaining"],
                                       GoalOverage = community["GoalOverage"]
                                   });

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            return jsSerializer.Serialize(new { Community = rsCommunityGoal });
        }

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string IncentivizerClusterByBonusPlanGet(string OperationClusterID, string Username)
        {
            DataSet ds = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.IncentivizerClusterByBonusPlanGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@OperationClusterID", SqlDbType.VarChar);
                    Command.Parameters["@OperationClusterID"].Value = OperationClusterID;

                    Command.Parameters.Add("@Username", SqlDbType.VarChar);
                    Command.Parameters["@Username"].Value = Username;

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(ds);
                    }
                }
            }

            var rsCommunityGoal = (from community in ds.Tables[0].AsEnumerable()
                                   select new
                                   {
                                       CommunityNumber = community["CommunityNumber"],
                                       CommunityName = community["CommunityName"],
                                       CommunityID = community["CommunityID"],
                                       Username = community["Username"],
                                       EffectiveDt = community["EffectiveDt"],
                                       BonusPlanID = community["BonusPlanID"],
                                       TierID = community["TierID"],
                                       CommunityGoal = community["CommunityGoal"],
                                       CommunityTier1Goal = community["CommunityTier1Goal"],
                                       CommunityTier2Goal = community["CommunityTier2Goal"],
                                       CommunityTier3Goal = community["CommunityTier3Goal"],
                                       CommunityRevenue = community["CommunityRevenue"],
                                       BonusAmount = community["BonusAmount"],
                                       ProjectedRevenue = community["ProjectedRevenue"],
                                       GoalRemaining = community["GoalRemaining"],
                                       GoalOverage = community["GoalOverage"]
                                   });

            var rsClusterGoal = (from cluster in ds.Tables[1].AsEnumerable()
                                 select new
                                 {
                                     OperationClusterID = cluster["OperationClusterID"],
                                     OperationCluster = cluster["OperationCluster"],
                                     Username = cluster["Username"],
                                     EffectiveDt = cluster["EffectiveDt"],
                                     BonusPlanID = cluster["BonusPlanID"],
                                     ClusterTierID = cluster["ClusterTierID"],
                                     ClusterGoal = cluster["ClusterGoal"],
                                     ClusterTier1Goal = cluster["ClusterTier1Goal"],
                                     ClusterTier2Goal = cluster["ClusterTier2Goal"],
                                     ClusterTier3Goal = cluster["ClusterTier3Goal"],
                                     ClusterRevenue = cluster["ClusterRevenue"],
                                     ClusterBonusAmount = cluster["ClusterBonusAmount"],
                                     ClusterProjectedRevenue = cluster["ClusterProjectedRevenue"],
                                     ClusterGoalRemaining = cluster["ClusterGoalRemaining"],
                                     ClusterGoalOverage = cluster["ClusterGoalOverage"]
                                 });

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            return jsSerializer.Serialize(new { Community = rsCommunityGoal, Cluster = rsClusterGoal });
        }
    }
}