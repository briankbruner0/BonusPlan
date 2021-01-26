using AtriaEM;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BonusPlan
{
    public class CommunityBO : CommunityDTO
    {
        public DataSet CommunityGet()
        {
            DataSet dsCommunity = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("ARM.CommunityGet", Connection))
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

        public DataSet CommunityByCountryGet()
        {
            DataSet ds = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("ARM.CommunityByCountryGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@CountryCode", SqlDbType.VarChar);
                    Command.Parameters["@CountryCode"].Value = Country.NullIfEmpty();

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(ds);
                    }
                }
            }

            return ds;
        }

        /// <summary>
        /// Gets details for a Community and loads the object, returning the details in a DataSet.
        /// </summary>
        /// <returns>DataSet loaded with data for a particular Community.</returns>
        public DataSet CommunityDetailGet()
        {
            DataSet DataSet = new DataSet();

            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("ARM.CommunityDetailGet", connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@CommunityNumber", SqlDbType.VarChar);
                    Command.Parameters["@CommunityNumber"].Value = this.CommunityNumber.NullIfEmpty();

                    Command.Parameters.Add("@CommunityID", SqlDbType.VarChar);
                    Command.Parameters["@CommunityID"].Value = this.CommunityID.NullIfEmpty();

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter())
                    {
                        DataAdapter.SelectCommand = Command;
                        DataAdapter.Fill(DataSet);
                    }

                    if (DataSet.HasRows())
                    {
                        CommunityID = DataSet.Tables[0].Rows[0]["CommunityID"].ToString();
                        CommunityName = DataSet.Tables[0].Rows[0]["CommunityName"].ToString();
                        Address1 = DataSet.Tables[0].Rows[0]["Address1"].ToString();
                        Address2 = DataSet.Tables[0].Rows[0]["Address2"].ToString();
                        City = DataSet.Tables[0].Rows[0]["City"].ToString();
                        State = DataSet.Tables[0].Rows[0]["State"].ToString();
                        PostalCode = DataSet.Tables[0].Rows[0]["PostalCode"].ToString();
                        Country = DataSet.Tables[0].Rows[0]["Country"].ToString();
                        PhoneNumber1 = DataSet.Tables[0].Rows[0]["PhoneNumber1"].ToString();
                        FaxNumber = DataSet.Tables[0].Rows[0]["FaxNumber"].ToString();
                        Region = DataSet.Tables[0].Rows[0]["Region"].ToString();
                        Division = DataSet.Tables[0].Rows[0]["Division"].ToString();
                    }
                }
            }
            return DataSet;
        }

        /// <summary>
        /// This gets the community information block
        /// </summary>
        /// <param name="CommunityNumber"></param>
        /// <returns></returns>
        public string CommunityInformationSnapIn()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<table border=\"0\" cellspacing=\"0\" cellpadding=\"3\" style=\"width:325px;\">");
            sb.AppendLine("<tr>");
            sb.AppendLine("<td style=\"background-color:#f7f7f7;border:3px solid #efefef;\">");
            sb.AppendFormat("<div style=\"color: #4a6d94; padding-bottom: 0px; font-family: Helvetica, Arial, sans-serif; font-size: 12px; font-weight: bold;\">{0}</div>", CommunityName);

            sb.AppendFormat("<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width:100%;margin-bottom:5px;\">");
            sb.AppendFormat("<tbody>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td style=\"background-color:#FFFFFF;padding:10px;border-top:#e5e5e5 1px solid;border-bottom:#d2d2d2 1px solid;border-left:#dbdbdb 1px solid;border-right:#dbdbdb 1px solid;width: 100%;\"> ");
            sb.AppendFormat("<img src=\"../../../../../../../images/Community/{0}0.jpg\" width='120' style=\"padding:0px 7px 0px 0px;\" align='left' >", CommunityNumber);

            sb.AppendFormat("<div class=\"ContactInformation\" style=\"font-family:Helvetica, Arial, sans-serif;font-size:11px;line-height:14px;color:#999999;\">");

            if (Address1.WhenNullOrEmpty(string.Empty).Length > 0 && City.WhenNullOrEmpty(string.Empty).Length > 0)
            {
                sb.AppendLine(Address1 + "<br>");
                if (Address2.WhenNullOrEmpty(string.Empty).Length > 0)
                {
                    sb.AppendLine(Address2 + "<br>");
                }
                sb.AppendLine(City + ",");
                sb.AppendLine(State + " " + Country);
                sb.AppendLine(PostalCode + "<br>");
            }
            if (PhoneNumber1.WhenNullOrEmpty(string.Empty).Length > 0)
            {
                sb.AppendFormat("Phone: {0}<br />", PhoneNumber1);
            }
            if (FaxNumber.WhenNullOrEmpty(string.Empty).Length > 0)
            {
                sb.AppendFormat("Fax: {0}<br />", FaxNumber);
            }
            sb.AppendFormat("</div>");

            if (Region.WhenNullOrEmpty(string.Empty).Length > 0)
            {
                sb.AppendFormat("<br><div style=\"font-family:Helvetica, Arial, sans-serif;font-size:12px;font-weight:bold;color:#999999;\">Region: {0}</div>", Region.WhenNullOrEmpty("&nbsp;"));
            }
            if (Division.WhenNullOrEmpty(string.Empty).Length > 0)
            {
                sb.AppendFormat("<div style=\"font-family:Helvetica, Arial, sans-serif;font-size:12px;font-weight:bold;color:#999999;\">Division: {0}</div>", Division.WhenNullOrEmpty("&nbsp;"));
            }

            sb.AppendFormat("</td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");

            sb.AppendFormat("</td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");

            return sb.ToString();
        }
    }
}