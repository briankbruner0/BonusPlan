using AtriaEM;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BonusPlan
{
    public class MoveInRevenueBO : MoveInRevenueDTO
    {
        #region SQL

        public DataSet MoveInRevenueDetailGet()
        {
            DataSet dsRevenue = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.MoveInRevenueDetailGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@MoveInRevenueID", SqlDbType.Int);
                    Command.Parameters["@MoveInRevenueID"].Value = MoveInRevenueID.NullIfEmpty();

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(dsRevenue);

                        if (dsRevenue.HasRows())
                        {
                            CustomerID = dsRevenue.Tables[0].Rows[0]["CustomerID"].ToString();
                            MoveInDate = dsRevenue.Tables[0].Rows[0]["MoveInDt"].ToString();
                            MoveOutDate = dsRevenue.Tables[0].Rows[0]["MoveOutDt"].ToString();
                            CommunityID = dsRevenue.Tables[0].Rows[0]["CommunityID"].ToString();
                            Community = dsRevenue.Tables[0].Rows[0]["Community"].ToString();
                        }
                    }
                }
            }

            return dsRevenue;
        }

        public Boolean MoveInRevenueDeleteVerify()
        {
            DataSet dsRevenue = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.MoveInRevenueDeleteVerify", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@MoveInRevenueID", SqlDbType.Int);
                    Command.Parameters["@MoveInRevenueID"].Value = MoveInRevenueID.NullIfEmpty();

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(dsRevenue);
                    }
                }
            }

            if (dsRevenue.HasRows())
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void MoveInRevenueDelete()
        {
            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["WebApplication"]);

            using (connection)
            {
                connection.Open();

                SqlCommand Command = new SqlCommand("BonusPlan.MoveInRevenueDelete", connection);

                Command.Parameters.Add("@MoveInRevenueID", SqlDbType.Int);
                Command.Parameters["@MoveInRevenueID"].Value = MoveInRevenueID.NullIfEmpty();

                Command.CommandType = CommandType.StoredProcedure;
                Command.CommandTimeout = 600;
                Command.ExecuteNonQuery();
            }
        }

        #endregion SQL

        #region Grids

        public string MoveInRevenueDetailGrid()
        {
            StringBuilder sbRevenue = new StringBuilder();
            DataSet dsRevenue = MoveInRevenueDetailGet();

            sbRevenue.AppendLine("<table id='RevenueDetailGrid' border='0' class='tablesorter' cellpadding='0' cellspacing='0' style='width:750px;'>");
            sbRevenue.AppendLine("   <thead>");
            sbRevenue.AppendLine("      <tr>");
            //sbRevenue.AppendLine("         <th class='columnHeader' style='width:20px;'>&nbsp</th>");
            sbRevenue.AppendLine("         <th class='columnHeader' style='width:50px;'>ID</th>");
            sbRevenue.AppendLine("         <th class='columnHeader' style='width:125px;'>Ledger Entry Type</th>");
            sbRevenue.AppendLine("         <th class='columnHeader' style='width:150px;'>Amount At Create</th>");
            sbRevenue.AppendLine("         <th class='columnHeader' style='width:150px;'>Amount At Maturity</th>");
            sbRevenue.AppendLine("         <th class='columnHeader' style='width:150px;'>Amount At Lookback</th>");
            sbRevenue.AppendLine("      </tr>");
            sbRevenue.AppendLine("   </thead>");
            sbRevenue.AppendLine("   <tbody>");

            if (dsRevenue.HasRows())
            {
                //Dictionary<string, string> revenueFields = new Dictionary<string, string>();
                //revenueFields.Add("ID", "MoveInRevenueDetailID");
                //revenueFields.Add("Ledger Entry Type", "RevenueEntryType");
                //revenueFields.Add("Amout At Create", "AmountAtCreate");
                //revenueFields.Add("Amount At Maturity", "AmountAtMaturity");
                //revenueFields.Add("Amount At Lookback", "AmountAtLookback");

                foreach (DataRow row in dsRevenue.Tables[0].Rows)
                {
                    sbRevenue.AppendLine("   <tr>");
                    //sbRevenue.AppendLine(AtriaBase.UI.Popup.InformationIconGet(row, revenueFields));
                    sbRevenue.AppendFormat("   <td class='columnData' style='text-align:right'>{0}</td>", row["MoveInRevenueDetailID"].nbspIfNull(true));
                    sbRevenue.AppendFormat("   <td class='columnData'>{0}</td>", row["RevenueEntryType"].nbspIfNull(true));
                    sbRevenue.AppendFormat("   <td class='columnData' style='text-align:right'>{0}</td>", row["AmountAtCreate"].nbspIfNull(true));
                    sbRevenue.AppendFormat("   <td class='columnData' style='text-align:right'>{0}</td>", row["AmountAtMaturity"].nbspIfNull(true));
                    sbRevenue.AppendFormat("   <td class='columnData' style='text-align:right'>{0}</td>", row["AmountAtLookback"].nbspIfNull(true));

                    sbRevenue.AppendLine("   </tr>");
                }
            }
            else
            {
                sbRevenue.AppendLine("   <tr><td colspan='9'>No Data to Display</td></tr>");
            }

            sbRevenue.AppendLine("   </tbody>");
            sbRevenue.AppendLine("</table>");

            return sbRevenue.ToString();
        }

        #endregion Grids
    }
}