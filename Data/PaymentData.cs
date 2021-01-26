using AtriaEM;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BonusPlan
{
    public class PaymentData : PaymentModel
    {
        public void PaymentInsert()
        {
            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand command = new SqlCommand("BonusPlan.PaymentInsert", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 600;

                    command.Parameters.Add("@EmployeeID", SqlDbType.Int);
                    command.Parameters["@EmployeeID"].Value = EmployeeID.NullIfEmpty();

                    command.Parameters.Add("@CommunityNumber", SqlDbType.VarChar);
                    command.Parameters["@CommunityNumber"].Value = CommunityNumber.NullIfEmpty();

                    command.Parameters.Add("@Amount", SqlDbType.VarChar);
                    command.Parameters["@Amount"].Value = Amount.NullIfEmpty();

                    command.Parameters.Add("@BonusPlanID", SqlDbType.Int);
                    command.Parameters["@BonusPlanID"].Value = BonusPlanID.NullIfEmpty();

                    command.Parameters.Add("@Note", SqlDbType.VarChar);
                    command.Parameters["@Note"].Value = Note.NullIfEmpty();

                    command.Parameters.Add("@CustomerID", SqlDbType.Int);
                    command.Parameters["@CustomerID"].Value = CustomerID;

                    command.Parameters.Add("@CreateBy", SqlDbType.VarChar);
                    command.Parameters["@CreateBy"].Value = Audit.CreateBy;

                    Connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        
        public DataSet BonusPlanGet()
        {
            DataSet dsBonusPlan = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.BonusPlanGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@ActiveFlg", SqlDbType.Int);
                    Command.Parameters["@ActiveFlg"].Value = Audit.ActiveFlg.NullIfEmpty();

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(dsBonusPlan);
                    }
                }
            }

            return dsBonusPlan;
        }
        
        public DataSet acCustomerSearch(string query)
        {
            DataSet dsResident = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.acCustomerSearch", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@query", SqlDbType.VarChar);
                    Command.Parameters["@query"].Value = query;

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(dsResident);
                    }
                }
            }

            return dsResident;
        }
    }
}