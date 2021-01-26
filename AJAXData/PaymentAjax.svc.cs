using Newtonsoft.Json;
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
    public class PaymentAjax
    {
        #region Payment search panel

        // Used for getting the processed bonus payments
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string PaymentDetailGet(string PaymentID)
        {
            DataSet ds = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.PaymentDetailGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@PaymentID", SqlDbType.VarChar);
                    Command.Parameters["@PaymentID"].Value = PaymentID;

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(ds);
                    }
                }
            }

            return JsonConvert.SerializeObject(ds.Tables[0]);
        }

        // Used for getting the pending bonus payments
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string PaymentDetailUpdate(string Username, string PaymentID, string Amount, string ProcessedDate)
        {
            int rowsAffected = 0;

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.PaymentUpdate", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@ModifyBy", SqlDbType.VarChar);
                    Command.Parameters["@ModifyBy"].Value = Username;

                    Command.Parameters.Add("@PaymentID", SqlDbType.Decimal);
                    Command.Parameters["@PaymentID"].Value = PaymentID;

                    Command.Parameters.Add("@Amount", SqlDbType.VarChar);
                    Command.Parameters["@Amount"].Value = Amount;

                    Command.Parameters.Add("@ProcessedDate", SqlDbType.VarChar);
                    Command.Parameters["@ProcessedDate"].Value = ProcessedDate;

                    Connection.Open();
                    rowsAffected = Command.ExecuteNonQuery();
                }
            }

            return rowsAffected.ToString();
        }

        #endregion Payment search panel

        #region Add a Payment Panel

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string PaymentInsert(PaymentData PaymentModel)
        {
            PaymentModel.PaymentInsert();

            return string.Empty;
        }
        
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string PaymentBonusPlanGet(PaymentData PaymentModel)
        {
            DataSet ds = PaymentModel.BonusPlanGet();

            return JsonConvert.SerializeObject(ds);
        }
        
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string acCustomerSearch(string query)
        {
            PaymentData PaymentData = new PaymentData();

            DataSet ds = PaymentData.acCustomerSearch(query);

            return JsonConvert.SerializeObject(ds);
        }

        #endregion Add a Payment Panel
    }
}