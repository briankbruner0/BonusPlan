using AtriaEM;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BonusPlan
{
    public class MoveInRevenueStatusBO : MoveInRevenueStatusDTO
    {
        public DataSet MoveInRevenueStatusGet()
        {
            DataSet dsRevenue = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.MoveInRevenueStatusGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@ActiveFlg", SqlDbType.Int);
                    Command.Parameters["@ActiveFlg"].Value = Audit.ActiveFlg.NullIfEmpty();

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(dsRevenue);
                    }
                }
            }

            return dsRevenue;
        }

        public DataSet MoveInRevenueStatusDetailGet()
        {
            DataSet dsRevenue = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.MoveInRevenueStatusDetailGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@MoveInRevenueStatusID", SqlDbType.Int);
                    Command.Parameters["@MoveInRevenueStatusID"].Value = MoveInRevenueStatusID;

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(dsRevenue);

                        if (dsRevenue.HasRows())
                        {
                            MoveInRevenueStatusID = dsRevenue.Tables[0].Rows[0]["MoveInRevenueStatusID"].ToString();
                            MoveInRevenueStatus = dsRevenue.Tables[0].Rows[0]["MoveInRevenueStatus"].ToString();
                            Sort = dsRevenue.Tables[0].Rows[0]["Sort"].ToString();
                            Audit.CreateBy = dsRevenue.Tables[0].Rows[0]["CreateBy"].ToString();
                            Audit.CreateDt = dsRevenue.Tables[0].Rows[0]["CreateDt"].ToString();
                            Audit.ModifyBy = dsRevenue.Tables[0].Rows[0]["ModifyBy"].ToString();
                            Audit.ModifyDt = dsRevenue.Tables[0].Rows[0]["ModifyDt"].ToString();
                            Audit.ActiveFlg = dsRevenue.Tables[0].Rows[0]["ActiveFlg"].ToString();
                        }
                    }
                }
            }

            return dsRevenue;
        }

        public void MoveInRevenueStatusInsert()
        {
            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.MoveInRevenueStatusInsert", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@MoveInRevenueStatus", SqlDbType.VarChar);
                    Command.Parameters["@MoveInRevenueStatus"].Value = MoveInRevenueStatus;

                    Command.Parameters.Add("@Sort", SqlDbType.Int);
                    Command.Parameters["@Sort"].Value = Sort;

                    Command.Parameters.Add("@CreateBy", SqlDbType.VarChar);
                    Command.Parameters["@CreateBy"].Value = Audit.CreateBy;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(Command);

                    Connection.Open();
                    Command.ExecuteNonQuery();
                }
            }
        }

        public void MoveInRevenueStatusUpdate()
        {
            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.MoveInRevenueStatusUpdate", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@MoveInRevenueStatusID", SqlDbType.Int);
                    Command.Parameters["@MoveInRevenueStatusID"].Value = MoveInRevenueStatusID;

                    Command.Parameters.Add("@MoveInRevenueStatus", SqlDbType.VarChar);
                    Command.Parameters["@MoveInRevenueStatus"].Value = MoveInRevenueStatus;

                    Command.Parameters.Add("@Sort", SqlDbType.Int);
                    Command.Parameters["@Sort"].Value = Sort;

                    Command.Parameters.Add("@ModifyBy", SqlDbType.VarChar);
                    Command.Parameters["@ModifyBy"].Value = Audit.ModifyBy;

                    Command.Parameters.Add("@ActiveFlg", SqlDbType.Int);
                    Command.Parameters["@ActiveFlg"].Value = Audit.ActiveFlg;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(Command);

                    Connection.Open();
                    Command.ExecuteNonQuery();
                }
            }
        }

        public string MoveInRevenueStatusGrid()
        {
            StringBuilder sbRevenueStatus = new StringBuilder();
            DataSet dsRevenueStatus = MoveInRevenueStatusGet();

            sbRevenueStatus.AppendLine("<table id='MoveInRevenueStatusGrid' class='tablesorter' cellpadding='0' cellspacing='0' border='0' style='width: 740px;'>");
            sbRevenueStatus.AppendLine("  <thead>");
            sbRevenueStatus.AppendLine("    <tr>");
            sbRevenueStatus.AppendLine("        <th class=\"columnHeader\" style=\"width:20px;\">&nbsp;</td>");
            sbRevenueStatus.AppendLine("        <th class=\"columnHeader\" style=\"width:20px;\">ID</td>");
            sbRevenueStatus.AppendLine("        <th class=\"columnHeader\" style=\"width:450px;\">Revenue</td>");
            sbRevenueStatus.AppendLine("        <th class=\"columnHeader\" style=\"width:50px;\">Sort</td>");
            sbRevenueStatus.AppendLine("        <th class=\"columnHeader\" style=\"width:50px;\">Active</td>");
            sbRevenueStatus.AppendLine("    </tr>");
            sbRevenueStatus.AppendLine("  </thead>");
            sbRevenueStatus.AppendLine("  <tbody>");

            if (dsRevenueStatus.HasRows())
            {
                Dictionary<string, string> revenueStatusFields = new Dictionary<string, string>();
                revenueStatusFields.Add("Revenue Status ID", "MoveInRevenueStatusID");
                revenueStatusFields.Add("Revenue Status", "MoveInRevenueStatus");
                revenueStatusFields.Add("Sort", "Sort");
                revenueStatusFields.Add("Create By", "CreateBy");
                revenueStatusFields.Add("Create Date", "CreateDt");
                revenueStatusFields.Add("Modify By", "ModifyBy");
                revenueStatusFields.Add("Modify Date", "ModifyDt");
                revenueStatusFields.Add("Active", "ActiveFlgImg");

                foreach (DataRow row in dsRevenueStatus.Tables[0].Rows)
                {
                    sbRevenueStatus.AppendLine("<tr>");
                    sbRevenueStatus.AppendLine(AtriaBase.UI.Popup.InformationIconGet(row, revenueStatusFields));
                    sbRevenueStatus.AppendFormat("<td>{0}</td>", row["MoveInRevenueStatusID"]);
                    sbRevenueStatus.AppendFormat("<td><a href='MoveInRevenueStatusProfile.aspx?MoveInRevenueStatusID={0}'>{1}</a></td>", row["MoveInRevenueStatusID"], row["MoveInRevenueStatus"]);
                    sbRevenueStatus.AppendFormat("<td>{0}</td>", row["Sort"]);
                    sbRevenueStatus.AppendFormat("<td><img src='{0}' /></td>", row["ActiveFlg"].ToString() == "1" ? "../../../../images/icon_check.gif" : "../../../../images/icon_check_open.gif");
                    sbRevenueStatus.AppendLine("</tr>");
                }
            }
            else
            {
                sbRevenueStatus.AppendLine("<tr><td colspan='5'>No data to display</td></tr>");
            }

            sbRevenueStatus.AppendLine("  </tbody>");
            sbRevenueStatus.AppendLine("</table>");

            return sbRevenueStatus.ToString();
        }
    }
}