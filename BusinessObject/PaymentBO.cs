using AtriaEM;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BonusPlan
{
    public class PaymentBO : PaymentDTO
    {
        public DataSet PaymentCommunityGet()
        {
            DataSet dsCommunity = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.PaymentCommunityGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(dsCommunity);
                    }
                }
            }

            return dsCommunity;
        }

        public void PaymentInsert()
        {
            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.PaymentInsert", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@EmployeeID", SqlDbType.Int);
                    Command.Parameters["@EmployeeID"].Value = EmployeeID.NullIfEmpty();

                    Command.Parameters.Add("@CommunityNumber", SqlDbType.VarChar);
                    Command.Parameters["@CommunityNumber"].Value = CommunityNumber.NullIfEmpty();

                    Command.Parameters.Add("@Amount", SqlDbType.VarChar);
                    Command.Parameters["@Amount"].Value = Amount.NullIfEmpty();

                    Command.Parameters.Add("@BonusPlanID", SqlDbType.Int);
                    Command.Parameters["@BonusPlanID"].Value = BonusPlanID.NullIfEmpty();

                    Command.Parameters.Add("@Note", SqlDbType.VarChar);
                    Command.Parameters["@Note"].Value = Note.NullIfEmpty();

                    Command.Parameters.Add("@CreateBy", SqlDbType.VarChar);
                    Command.Parameters["@CreateBy"].Value = Audit.CreateBy.NullIfEmpty();

                    Connection.Open();
                    Command.ExecuteNonQuery();
                }
            }
        }
    }
}