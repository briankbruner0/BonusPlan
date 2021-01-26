/********************************************************************
-- ATRIA SENIOR LIVING GROUP CONFIDENTIAL.  For authorized use only.
-- Except for as expressly authorized by Atria Senior Living,
-- do not disclose, copy, reproduce, distribute, or modify.
-- TM 2012 Atria Senior Living, LLC
*********************************************************************
PURPOSE:		The Business Object for RevenueEntryType
AUTHOR:			Tony.Thoman
DATE:			12/16/2012
NOTES:
CHANGE CONTROL:
********************************************************************/

using AtriaEM;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BonusPlan
{
    public class RevenueEntryTypeBO : RevenueEntryTypeDTO
    {
        #region SQL Fetch/Manipulation Methods

        public DataSet RevenueEntryTypeGet()
        {
            DataSet DataSet = new DataSet();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand command = new SqlCommand("BonusPlan.RevenueEntryTypeGet", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@ActiveFlg", SqlDbType.Int);
                    command.Parameters["@ActiveFlg"].Value = Audit.ActiveFlg.NullIfEmpty();

                    connection.Open();

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter())
                    {
                        DataAdapter.SelectCommand = command;
                        DataAdapter.Fill(DataSet);
                    }
                }
            }

            return DataSet;
        }

        public DataSet RevenueEntrySearch()
        {
            DataSet DataSet = new DataSet();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand command = new SqlCommand("BonusPlan.RevenueEntrySearch", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@CreateDT", SqlDbType.DateTime);
                    command.Parameters["@CreateDT"].Value = Audit.CreateDt.NullIfEmpty();

                    command.Parameters.Add("@RevenueEntryTypeID", SqlDbType.Int);
                    command.Parameters["@RevenueEntryTypeID"].Value = RevenueEntryTypeID.NullIfEmpty();

                    connection.Open();

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter())
                    {
                        DataAdapter.SelectCommand = command;
                        DataAdapter.Fill(DataSet);
                    }
                }
            }

            return DataSet;
        }

        public void RevenueEntryTypeDetailGet()
        {
            DataSet dsRevenueEntryType = new DataSet();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand command = new SqlCommand("BonusPlan.RevenueEntryTypeDetailGet", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@RevenueEntryTypeID", SqlDbType.Int);
                    command.Parameters["@RevenueEntryTypeID"].Value = RevenueEntryTypeID;

                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        dataAdapter.Fill(dsRevenueEntryType);
                        if (dsRevenueEntryType.HasRows())
                        {
                            RevenueEntryType = dsRevenueEntryType.Tables[0].Rows[0]["RevenueEntryType"].ToString();
                            RevenueEntryTypeID = dsRevenueEntryType.Tables[0].Rows[0]["RevenueEntryTypeID"].ToString();
                            Audit.CreateBy = dsRevenueEntryType.Tables[0].Rows[0]["CreateBy"].ToString();
                            Audit.CreateDt = dsRevenueEntryType.Tables[0].Rows[0]["CreateDt"].ToString();
                            Audit.ModifyBy = dsRevenueEntryType.Tables[0].Rows[0]["ModifyBy"].ToString();
                            Audit.ModifyDt = dsRevenueEntryType.Tables[0].Rows[0]["ModifyDt"].ToString();
                            Audit.ActiveFlg = dsRevenueEntryType.Tables[0].Rows[0]["ActiveFlg"].ToString();
                        }
                    }
                }
            }
        }

        public void RevenueEntryTypeInsert()
        {
            DataSet dsRevenueEntryType = new DataSet();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand command = new SqlCommand("BonusPlan.RevenueEntryTypeInsert", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@RevenueEntryType", SqlDbType.VarChar);
                    command.Parameters["@RevenueEntryType"].Value = RevenueEntryType;

                    command.Parameters.Add("@UserName", SqlDbType.VarChar);
                    command.Parameters["@UserName"].Value = Audit.CreateBy;

                    command.Parameters.Add("@ActiveFlg", SqlDbType.Int);
                    command.Parameters["@ActiveFlg"].Value = Audit.ActiveFlg;

                    command.Parameters.Add("@RevenueEntryTypeID", SqlDbType.Int);
                    command.Parameters["@RevenueEntryTypeID"].Direction = ParameterDirection.Output;

                    connection.Open();
                    command.ExecuteNonQuery();

                    RevenueEntryTypeID = command.Parameters["@RevenueEntryTypeID"].Value.ToString();
                }
            }
        }

        public void RevenueEntryTypeUpdate()
        {
            DataSet dsRevenueEntryType = new DataSet();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand command = new SqlCommand("BonusPlan.RevenueEntryTypeUpdate", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@RevenueEntryTypeID", SqlDbType.Int);
                    command.Parameters["@RevenueEntryTypeID"].Value = RevenueEntryTypeID;

                    command.Parameters.Add("@RevenueEntryType", SqlDbType.VarChar);
                    command.Parameters["@RevenueEntryType"].Value = RevenueEntryType;

                    command.Parameters.Add("@UserName", SqlDbType.VarChar);
                    command.Parameters["@UserName"].Value = Audit.ModifyBy;

                    command.Parameters.Add("@ActiveFlg", SqlDbType.Int);
                    command.Parameters["@ActiveFlg"].Value = Audit.ActiveFlg;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public DataSet RevenueEntryTypeToLedgerEntryGet()
        {
            DataSet dsLedgerEntryType = new DataSet();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand command = new SqlCommand("BonusPlan.LedgerEntryToRevenueEntryTypeGet", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@RevenueEntryTypeID", SqlDbType.Int);
                    command.Parameters["@RevenueEntryTypeID"].Value = RevenueEntryTypeID.NullIfEmpty();

                    connection.Open();

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter())
                    {
                        DataAdapter.SelectCommand = command;
                        DataAdapter.Fill(dsLedgerEntryType);
                    }
                }
            }
            return dsLedgerEntryType;
        }

        #endregion SQL Fetch/Manipulation Methods

        #region Grid

        public string RevenueEntryTypeGrid()
        {
            StringBuilder sbHTMLOutput = new StringBuilder();

            sbHTMLOutput.AppendLine("\t<table id='RevenueEntryTypeGrid' class='tablesorter' cellpadding='0' cellspacing='0' border='0' style='width: 650px;'>");
            sbHTMLOutput.AppendLine("\t\t<thead>");
            sbHTMLOutput.AppendLine("\t\t\t<tr>");
            sbHTMLOutput.AppendLine("\t\t\t\t<th class=\"columnHeader\" style=\"width:25px;\">&nbsp;</td>");
            sbHTMLOutput.AppendLine("\t\t\t\t<th class=\"columnHeader\" style=\"width:25px;\">ID</td>");
            sbHTMLOutput.AppendLine("\t\t\t\t<th class=\"columnHeader\" style=\"width:575px;\">Revenue Entry Type</td>");
            sbHTMLOutput.AppendLine("\t\t\t\t<th class=\"columnHeader\" style=\"width:25px;\">Active</td>");
            sbHTMLOutput.AppendLine("\t\t\t</tr>");
            sbHTMLOutput.AppendLine("\t\t</thead>");
            sbHTMLOutput.AppendLine("\t\t\t<tbody>");

            DataSet dsCategory = RevenueEntryTypeGet();

            if (dsCategory.HasRows())
            {
                Dictionary<string, string> categoryFields = new Dictionary<string, string>();
                categoryFields.Add("RevenueEntryTypeID", "RevenueEntryTypeID");
                categoryFields.Add("Revenue Entry Type", "RevenueEntryType");
                categoryFields.Add("Create By", "CreateBy");
                categoryFields.Add("Create DT", "CreateDT");
                categoryFields.Add("Modify By", "ModifyBy");
                categoryFields.Add("Modify DT", "ModifyDT");

                foreach (DataRow drCategory in dsCategory.Tables[0].Rows)
                {
                    sbHTMLOutput.AppendFormat("\t\t\t<tr onmouseover=\"this.style.cursor='hand';this.style.color='White';this.style.backgroundColor='6699FF';\" onMouseOut=\"this.style.cursor='';this.style.backgroundColor='White';\" onclick=\"window.location.href='RevenueEntryTypeProfile.aspx?RevenueEntryTypeID={0}'\">", drCategory["RevenueEntryTypeID"].ToString());
                    sbHTMLOutput.AppendLine(AtriaBase.UI.Popup.InformationIconGet(drCategory, categoryFields));
                    sbHTMLOutput.AppendLine("\t\t\t\t<td class='columnData' style='text-align:right;' >" + drCategory["RevenueEntryTypeID"].WhenNullOrEmpty("&nbsp;") + "</td>");
                    sbHTMLOutput.AppendLine("\t\t\t\t<td class='columnData'>" + drCategory["RevenueEntryType"].WhenNullOrEmpty("&nbsp;") + "</td>");
                    sbHTMLOutput.AppendLine("\t\t\t\t<td class=\"columnData\" style='text-align:center;' >" + ((drCategory["ActiveFlg"].WhenNullOrEmpty("0").Equals("0")) ? "<img src='../../../../../../../images/icon_check_open.gif'>" : "<img src='../../../../../../../images/icon_check.gif'>") + "</td>");
                    sbHTMLOutput.AppendLine("\t\t\t\t\t</tr>");
                }
            }
            else
            {
                sbHTMLOutput.AppendLine("\t\t\t<tr>");
                sbHTMLOutput.AppendLine("\t\t\t\t<td colspan='4'>");
                sbHTMLOutput.AppendLine("\t\t\t\tNo Data to Display.");
                sbHTMLOutput.AppendLine("\t\t\t\t</td>");
                sbHTMLOutput.AppendLine("\t\t\t</tr>");
            }

            sbHTMLOutput.AppendLine("\t\t\t</tbody>");
            sbHTMLOutput.AppendLine("\t\t</table>");
            sbHTMLOutput.AppendLine("\t\t<div style='min-height: 300px;'>");
            sbHTMLOutput.AppendLine("\t\t</div>");

            return sbHTMLOutput.ToString();
        }

        public string SearchResultGrid()
        {
            StringBuilder sbHTMLOutput = new StringBuilder();

            sbHTMLOutput.AppendLine("\t<table id='SearchResultGrid' class='tablesorter' cellpadding='0' cellspacing='0' border='0' width='1040px'>");
            sbHTMLOutput.AppendLine("\t\t<thead>");
            sbHTMLOutput.AppendLine("\t\t\t<tr>");
            sbHTMLOutput.AppendLine("\t\t\t\t<th class=\"columnHeader\" style=\"width:20px;\">&nbsp;</td>");
            sbHTMLOutput.AppendLine("\t\t\t\t<th class=\"columnHeader\" style=\"width:250px;\">Bonus Plan</td>");
            sbHTMLOutput.AppendLine("\t\t\t\t<th class=\"columnHeader\" style=\"width:135px;\">Create Date</td>");
            sbHTMLOutput.AppendLine("\t\t\t\t<th class=\"columnHeader\" style=\"width:135px;\">Effective Date</td>");
            sbHTMLOutput.AppendLine("\t\t\t\t<th class=\"columnHeader\" style=\"width:250px;\">Event Type</td>");
            sbHTMLOutput.AppendLine("\t\t\t\t<th class=\"columnHeader\" style=\"width:250px;\">Ledger Entry</td>");
            sbHTMLOutput.AppendLine("\t\t\t</tr>");
            sbHTMLOutput.AppendLine("\t\t</thead>");
            sbHTMLOutput.AppendLine("\t\t\t<tbody>");

            DataSet dsSearch = RevenueEntrySearch();

            if (dsSearch.HasRows())
            {
                Dictionary<string, string> revenueEntryFields = new Dictionary<string, string>();
                revenueEntryFields.Add("Bonus Plan", "BonusPlan");
                revenueEntryFields.Add("Create Date", "CreateDT");
                revenueEntryFields.Add("Effective Date", "EffectiveDT");
                revenueEntryFields.Add("Revenue Entry Type", "RevenueEntryType");
                revenueEntryFields.Add("Ledger Entry", "LedgerEntry");

                foreach (DataRow dr in dsSearch.Tables[0].Rows)
                {
                    sbHTMLOutput.AppendFormat("\t\t\t<tr>");
                    sbHTMLOutput.AppendLine(AtriaBase.UI.Popup.InformationIconGet(dr, revenueEntryFields));
                    sbHTMLOutput.AppendLine("\t\t\t\t<td>" + dr["BonusPlan"].WhenNullOrEmpty("&nbsp;") + "</td>");
                    sbHTMLOutput.AppendLine("\t\t\t\t<td style=\"text-align:right;\">" + dr["CreateDT"].WhenNullOrEmpty("&nbsp;") + "</td>");
                    sbHTMLOutput.AppendLine("\t\t\t\t<td style=\"text-align:right;\">" + dr["EffectiveDT"].WhenNullOrEmpty("&nbsp;") + "</td>");
                    sbHTMLOutput.AppendLine("\t\t\t\t<td>" + dr["RevenueEntryType"].WhenNullOrEmpty("&nbsp;") + "</td>");
                    sbHTMLOutput.AppendLine("\t\t\t\t<td>" + dr["LedgerEntry"].WhenNullOrEmpty("&nbsp;") + "</td>");

                    sbHTMLOutput.AppendLine("\t\t\t</tr>");
                }
            }
            else
            {
                sbHTMLOutput.AppendLine("\t\t\t<tr>");
                sbHTMLOutput.AppendLine("\t\t\t\t<td colspan='6'>");
                sbHTMLOutput.AppendLine("\t\t\t\tNo Data to Display.");
                sbHTMLOutput.AppendLine("\t\t\t\t</td>");
                sbHTMLOutput.AppendLine("\t\t\t</tr>");
            }

            sbHTMLOutput.AppendLine("\t\t\t</tbody>");
            sbHTMLOutput.AppendLine("\t\t</table>");
            sbHTMLOutput.AppendLine("\t\t<div style='min-height: 300px;'>");
            sbHTMLOutput.AppendLine("\t\t</div>");

            return sbHTMLOutput.ToString();
        }

        #endregion Grid
    }
}