using AtriaEM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Serialization;

namespace BonusPlan
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class BonusPlanAJAX
    {
        // Retrieves the bonus percentage for a given user and community
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string BonusPlanPercentageByUserGet(string Username, string BonusPlanID, string CommunityNumber)
        {
            DataSet ds = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.BonusPlanPercentageGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@CommunityNumber", SqlDbType.VarChar);
                    Command.Parameters["@CommunityNumber"].Value = CommunityNumber;

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

            var rsBonusPlan = (from community in ds.Tables[0].AsEnumerable()
                               select new
                               {
                                   CommunityNumber = community["CommunityNumber"],
                                   CommunityID = community["CommunityID"],
                                   Username = community["Username"],
                                   Percentage = community["Percentage"],
                                   ExpectedBonus = community["ExpectedBonus"],
                                   ClusterExpectedBonus = community["ClusterExpectedBonus"],
                                   PotentialBonus = community["PotentialBonus"],
                                   ClusterPotentialBonus = community["ClusterPotentialBonus"]
                               });

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            return jsSerializer.Serialize(rsBonusPlan);
        }

        // Retrieves a list of communities configured for a given bonus plan
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string BonusPlanToCommunityGet(string BonusPlanID)
        {
            BonusPlanBO objBonusPlan = new BonusPlanBO();
            objBonusPlan.BonusPlanID = BonusPlanID;

            DataSet ds = objBonusPlan.BonusPlanToCommunityGet();

            var rsCommunity = ds.Tables[0].AsEnumerable()
                               .GroupBy(c => c["CommunityNumber"])
                               .Select(g => g.OrderByDescending(d => d["BeginDt"]).FirstOrDefault())
                               .Where(b => string.IsNullOrEmpty(b["EndDt"].ToString()) || Convert.ToDateTime(b["EndDt"].ToString()) > DateTime.Now)
                               .Select(r =>
                                    new
                                    {
                                        BonusPlanToCommunityID = r["BonusPlanToCommunityID"],
                                        CommunityNumber = r["CommunityNumber"],
                                        Community = r["Community"],
                                        BeginDt = r["BeginDt"].ToString().WhenNullOrEmpty("--"),
                                        EndDt = r["EndDt"].ToString().WhenNullOrEmpty("--")
                                    });

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            return jsSerializer.Serialize(rsCommunity);
        }

        // Deletes a community configured for a bonus plan
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string BonusPlanToCommunityDelete(string BonusPlanToCommunityID, string BonusPlanID)
        {
            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.BonusPlanToCommunityDelete", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@BonusPlanToCommunityID", SqlDbType.Int);
                    Command.Parameters["@BonusPlanToCommunityID"].Value = BonusPlanToCommunityID;

                    Connection.Open();
                    Command.ExecuteNonQuery();
                }
            }

            return BonusPlanToCommunityGet(BonusPlanID);
        }

        // Adds a community to the bonus plan configuration
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string BonusPlanToCommunityInsert(string BonusPlanID, string CommunityNumber, string BeginDt, string EndDt, string Username)
        {
            BonusPlanBO objBonusPlan = new BonusPlanBO();
            objBonusPlan.BonusPlanID = BonusPlanID;
            objBonusPlan.CommunityNumber = CommunityNumber;
            objBonusPlan.BeginDt = BeginDt;
            objBonusPlan.EndDt = EndDt;
            objBonusPlan.Audit.UserName = Username;

            objBonusPlan.BonusPlanToCommunityInsert();

            return BonusPlanToCommunityGet(BonusPlanID);
        }

        // Gets the configured users for a given bonus plan
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string BonusPlanToUserCommunityGet(string BonusPlanID)
        {
            BonusPlanBO objBonusPlan = new BonusPlanBO();
            objBonusPlan.BonusPlanID = BonusPlanID;

            DataSet ds = objBonusPlan.BonusPlanToUserCommunityDetailGet();

            var rsUserCommunity = (from community in ds.Tables[0].AsEnumerable()
                                   select new
                                   {
                                       BonusPlanToUserCommunityID = community["BonusPlanToUserCommunityID"],
                                       EmployeeID = community["EmployeeID"],
                                       ADP_EmployeeID = community["ADP_EmployeeID"],
                                       UserName = community["UserName"],
                                       Community = community["Community"],
                                       Percentage = community["Percentage"],
                                       FlatRate = community["FlatRate"].ToString().WhenNullOrEmpty(""),
                                       BeginDT = community["BeginDT"],
                                       EndDT = community["EndDt"],
                                       Rollforwardflg = community["Rollforwardflg"]
                                   });

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            return jsSerializer.Serialize(rsUserCommunity);
        }

        // Removes a user from the bonus plan configuration (nb:  existing stored procedure is named with Update)
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string BonusPlanToUserCommunityUpdate(string BonusPlanToUserCommunityID, string BonusPlanID)
        {
            BonusPlanBO objBonusPlan = new BonusPlanBO();
            objBonusPlan.BonusPlanToUserCommunityID = BonusPlanToUserCommunityID;

            objBonusPlan.BonusPlanToUserCommunityUpdate();

            return BonusPlanToUserCommunityGet(BonusPlanID);
        }

        // Adds a user to a bonus plan configuration
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string BonusPlanToUserCommunityInsert(string BonusPlanID, string EmployeeID, string CommunityNumber, string Percentage, string BeginDt, string EndDt, string RollforwardFlg, string Username)
        {
            BonusPlanBO objBonusPlan = new BonusPlanBO();
            objBonusPlan.BonusPlanID = BonusPlanID;
            objBonusPlan.EmployeeID = EmployeeID;
            objBonusPlan.CommunityNumber = CommunityNumber;
            objBonusPlan.Percentage = Percentage;
            objBonusPlan.BeginDt = BeginDt;
            objBonusPlan.EndDt = EndDt;
            objBonusPlan.RollforwardFlg = RollforwardFlg;
            objBonusPlan.Audit.CreateBy = Username;

            objBonusPlan.BonusPlanToUserCommunityInsert();

            return BonusPlanToUserCommunityGet(BonusPlanID);
        }

        // Retrieves ledger entry types for a given entry type ID.
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string LedgerEntryTypeGet(string RevenueEntryTypeID)
        {
            RevenueEntryTypeBO objRevenue = new RevenueEntryTypeBO();
            objRevenue.RevenueEntryTypeID = RevenueEntryTypeID;

            DataSet ds = objRevenue.RevenueEntryTypeToLedgerEntryGet();

            var rsLedger = (from revenue in ds.Tables[0].AsEnumerable()
                            select new
                            {
                                LedgerEntryID = revenue["LedgerEntryID"],
                                LedgerEntry = revenue["LedgerEntry"]
                            });

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            return jsSerializer.Serialize(rsLedger);
        }

        // Used for autocompleting employee search in Bonus Plan Profile
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public List<BonusPlanBO> BonusPlanEmployeeSearch(string ADPEmpID)
        {
            DataSet employee;

            BonusPlanBO objBonusPlan = new BonusPlanBO();
            objBonusPlan.ADPEmployeeID = ADPEmpID;

            employee = objBonusPlan.EmployeeSearchGet();
            List<BonusPlanBO> objEmployee = new List<BonusPlanBO>();

            if (employee.HasRows())
            {
                foreach (DataRow row in employee.Tables[0].Rows)
                {
                    // 02/14/2018 Fritz Kern - Added Job Title, Community, and Photo Path so we can use those for potential employee bubbles. See AddPayment.aspx
                    objEmployee.Add(new BonusPlanBO
                    {
                        Employee = row["Employee"].ToString()
                        ,
                        EmployeeID = row["EmployeeID"].ToString()
                        ,
                        JobTitle = row["JobTitle"].ToString()
                        ,
                        Community = row["Community"].ToString()
                        ,
                        PhotoPath = row["PhotoPath"].ToString()
                    });
                }
            }

            return objEmployee;
        }

        #region Cluster

        // Returns all communities for a given country
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string CommunityByCountryGet(string CountryCode)
        {
            StringBuilder sbHTML = new StringBuilder();
            CommunityBO objCommunity = new CommunityBO();
            objCommunity.Country = CountryCode;

            DataSet ds = objCommunity.CommunityByCountryGet();

            foreach (var community in ds.Tables[0].AsEnumerable())
            {
                sbHTML.AppendFormat("<option value='{0}'>{1}</option>", community["CommunityID"], community["CommunityName"]);
            }

            return sbHTML.ToString();
        }

        // Used for getting cluster information
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string CommunityByOperationClusterIDGet(string OperationClusterID)
        {
            DataSet ds = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("ARM.CommunityByOperationClusterIDGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@OperationClusterID", SqlDbType.VarChar);
                    Command.Parameters["@OperationClusterID"].Value = OperationClusterID;

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(ds);
                    }
                }
            }

            var rsCommunity = (from community in ds.Tables[0].AsEnumerable()
                               select new
                               {
                                   CommunityNumber = community["CommunityNumber"],
                                   CommunityName = community["CommunityName"],
                                   CommunityID = community["CommunityID"]
                               });
            var rsCluster = (from community in ds.Tables[0].AsEnumerable()
                             select new
                             {
                                 OperationCluster = community["OperationCluster"],
                                 OperationClusterID = community["OperationClusterID"]
                             }).Distinct();

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            return jsSerializer.Serialize(new { Community = rsCommunity, Cluster = rsCluster });
        }

        // Used for getting cluster information
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string ClusterByCommunityNumberGet(string CommunityNumber)
        {
            DataSet ds = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("ARM.ClusterByCommunityNumberGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@CommunityNumber", SqlDbType.VarChar);
                    Command.Parameters["@CommunityNumber"].Value = CommunityNumber;

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(ds);
                    }
                }
            }

            var rsCommunity = (from community in ds.Tables[0].AsEnumerable()
                               select new
                               {
                                   CommunityNumber = community["CommunityNumber"],
                                   CommunityName = community["CommunityName"],
                                   CommunityID = community["CommunityID"],
                                   CurrentCommunityFlg = community["CurrentCommunityFlg"]
                               });
            var rsCluster = (from community in ds.Tables[0].AsEnumerable()
                             select new
                             {
                                 OperationCluster = community["OperationCluster"],
                                 OperationClusterID = community["OperationClusterID"]
                             }).Distinct();

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            return jsSerializer.Serialize(new { Community = rsCommunity, Cluster = rsCluster });
        }

        #endregion Cluster

        // Used for getting the menu bar month list
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string MenuBarMonthGet()
        {
            DataSet ds = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.MenuBarMonthGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(ds);
                    }
                }
            }

            var rsMonth = (from month in ds.Tables[0].AsEnumerable()
                           select new
                           {
                               DateName = month["DateName"],
                               MonthGroup = month["MonthGroup"],
                               EffectiveDtDisplay = DateTime.Parse(month["EffectiveDt"].ToString()).ToString("MMMM yyyy"),
                               EffectiveDt = DateTime.Parse(month["EffectiveDt"].ToString()).ToString("MM/dd/yyyy")
                           });

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            return jsSerializer.Serialize(rsMonth);
        }

        // Used for getting move in revenue for a given user, community/cluster and month
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string MoveInRevenueActivityGet(string CommunityNumber, string OperationClusterID, string EffectiveDT, string Username, string BonusPlanID)
        {

            DashboardBO objDashboard = new DashboardBO();
            objDashboard.CommunityNumber = CommunityNumber;
            objDashboard.OperationClusterID = OperationClusterID;
            objDashboard.EffectiveDT = EffectiveDT;
            objDashboard.Audit.UserName = Username;
            objDashboard.BonusPlanID = BonusPlanID;

            DataSet ds = objDashboard.MoveInRevenueActivityGet();

            var rsRevenueCommunity = (from community in ds.Tables[2].AsEnumerable()
                                      select new
                                      {
                                          CommunityID = community["CommunityID"],
                                          CommunityName = community["CommunityName"],
                                          CommunityNumber = community["CommunityNumber"],
                                          BonusEligibleFlg = community["BonusEligibleFlg"],
                                          PercentageEffective = community["PercentageEffective"],
                                          AmountAtMaturity = community["AmountAtMaturity"].ToString().MoneyFormat(),
                                          PotentialBonus = community["PotentialBonus"].ToString().MoneyFormat(),
                                          FirstName = community["FirstName"],
                                          Month = community["Month"],
                                          MoveInRevenue = (from revenue in ds.Tables[0].AsEnumerable()
                                                           where revenue["CommunityID"].ToString().Equals(community["CommunityID"].ToString())
                                                           select new
                                                           {
                                                               MoveInRevenueID = revenue["MoveInRevenueID"],
                                                               CustomerID = revenue["CustomerID"],
                                                               RoomNumber = revenue["RoomNumber"],
                                                               RoomType = revenue["RoomType"],
                                                               CareTypeCode = revenue["CareTypeCode"],
                                                               ResidentName = revenue["ResidentName"],
                                                               MoveInDT = revenue["MoveInDT"],
                                                               MoveOutDT = revenue["MoveOutDT"],
                                                               DayOfResidence = revenue["DayOfResidence"],
                                                               ResidencyAgreementFlg = revenue["ResidencyAgreementFlg"],
                                                               AmountAtMaturity = revenue["AmountAtMaturity"].ToString().MoneyFormat(),
                                                               PotentialBonus = revenue["PotentialBonus"].ToString().MoneyFormat(),
                                                               PaidFlg = revenue["PaidFlg"],
                                                               PaidDt = revenue["PaidDt"].ToString(),
                                                               Detail = (from detail in ds.Tables[1].AsEnumerable()
                                                                         where revenue["MoveInRevenueID"].ToString().Equals(detail["MoveInRevenueID"].ToString())
                                                                         select new
                                                                         {
                                                                             Description = detail["Description"],
                                                                             CareType = detail["CareType"],
                                                                             AmountAtMaturity = detail["AmountAtMaturity"].ToString().MoneyFormat(),
                                                                         })
                                                           })
                                      });

            var rsRevenueCluster = (from cluster in ds.Tables[3].AsEnumerable()
                                    select new
                                    {
                                        OperationClusterID = cluster["OperationClusterID"],
                                        OperationCluster = cluster["OperationCluster"],
                                        AmountAtMaturity = cluster["AmountAtMaturity"].ToString().MoneyFormat(),
                                        PotentialBonus = cluster["PotentialBonus"].ToString().MoneyFormat()
                                    });

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            return jsSerializer.Serialize(new { Community = rsRevenueCommunity, Cluster = rsRevenueCluster });
        }

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string MoveInRevenueActivityByCommunityGet(string CommunityNumber, string EffectiveDT, string BonusPlanID)
        {
            DashboardBO objDashboard = new DashboardBO();
            objDashboard.CommunityNumber = CommunityNumber;
            objDashboard.EffectiveDT = EffectiveDT;
            objDashboard.BonusPlanID = BonusPlanID;

            DataSet ds = objDashboard.MoveInRevenueActivityByCommunityGet();

            var rsEmployees = (from employee in ds.Tables[0].AsEnumerable()
                               orderby employee["EmployeeName"].ToString()
                               select new
                               {
                                   EmployeeName = employee["EmployeeName"].ToString(),
                                   EmployeeID = employee["EmployeeID"].ToString(),
                               }).Distinct();

            var rsReveneByEmployee = (from revenueByEmployee in ds.Tables[0].AsEnumerable()
                                      orderby revenueByEmployee["EmployeeName"].ToString()
                                      select new
                                      {
                                          EmployeeID = revenueByEmployee["EmployeeID"].ToString(),
                                          AmountAtMaturity = revenueByEmployee["AmountAtMaturity"].ToString().MoneyFormat(),
                                          BonusPayment = revenueByEmployee["BonusPayment"].ToString().MoneyFormat(),
                                          MoveInRevenueID = revenueByEmployee["MoveInRevenueID"],
                                          CustomerID = revenueByEmployee["CustomerID"],
                                          RoomNumber = revenueByEmployee["RoomNumber"],
                                          RoomType = revenueByEmployee["RoomType"],
                                          CareTypeCode = revenueByEmployee["CareTypeCode"],
                                          ResidentName = revenueByEmployee["ResidentName"],
                                          MoveInDT = revenueByEmployee["MoveInDT"],
                                          MoveOutDT = revenueByEmployee["MoveOutDT"],
                                          DayOfResidence = revenueByEmployee["DayOfResidence"],
                                          ResidencyAgreementFlg = revenueByEmployee["ResidencyAgreementFlg"],
                                          PaidFlg = revenueByEmployee["PaidFlg"],
                                          PaidDt = revenueByEmployee["PaidDt"].ToString()
                                      });

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            return jsSerializer.Serialize(new { Revenue = rsReveneByEmployee, EmployeeList = rsEmployees });
        }


        // Used for getting the pending bonus payments
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string PendingPaymentGet(string CommunityNumber, string EffectiveDT, string Username)
        {
            DashboardBO objDashboard = new DashboardBO();

            DataSet ds = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.PendingPaymentGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@CommunityNumber", SqlDbType.VarChar);
                    Command.Parameters["@CommunityNumber"].Value = CommunityNumber;

                    Command.Parameters.Add("@EffectiveDT", SqlDbType.VarChar);
                    Command.Parameters["@EffectiveDT"].Value = EffectiveDT;

                    Command.Parameters.Add("@Username", SqlDbType.VarChar);
                    Command.Parameters["@Username"].Value = Username;

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(ds);
                    }
                }
            }

            var rsPayment = (from payment in ds.Tables[0].AsEnumerable()
                             select new
                             {
                                 EmployeeID = payment["EmployeeID"],
                                 PaymentID = payment["PaymentID"],
                                 Amount = payment["Amount"].ToString().MoneyFormat(),
                                 Total = payment["Total"].ToString().MoneyFormat(),
                                 PaymentTypeID = payment["PaymentTypeID"],
                                 PaymentStatusID = payment["PaymentStatusID"],
                                 Resident = payment["Resident"],
                             });

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            return jsSerializer.Serialize(rsPayment);
        }

        // Used for getting the processed bonus payments
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string ProcessedPaymentGet(string CommunityNumber, string EffectiveDT, string Username)
        {
            DataSet ds = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.ProcessedPaymentGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@CommunityNumber", SqlDbType.VarChar);
                    Command.Parameters["@CommunityNumber"].Value = CommunityNumber;

                    Command.Parameters.Add("@EffectiveDT", SqlDbType.VarChar);
                    Command.Parameters["@EffectiveDT"].Value = EffectiveDT;

                    Command.Parameters.Add("@Username", SqlDbType.VarChar);
                    Command.Parameters["@Username"].Value = Username;

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(ds);
                    }
                }
            }

            var rsPayment = (from payment in ds.Tables[0].AsEnumerable()
                             select new
                             {
                                 EmployeeID = payment["EmployeeID"],
                                 PayrollID = payment["PayrollID"],
                                 PaymentDt = payment["PaymentDt"].ToString(),
                                 Amount = payment["Amount"].ToString().MoneyFormat(),
                                 Detail = (from detail in ds.Tables[1].AsEnumerable()
                                           where detail["PayrollID"].ToString().Equals(payment["PayrollID"].ToString())
                                           select new
                                           {
                                               PaymentID = detail["PaymentID"],
                                               PayrollID = detail["PayrollID"],
                                               CustomerID = detail["CustomerID"].WhenNullOrEmpty(""),
                                               Resident = detail["Resident"],
                                               MoveInDt = detail["MoveInDt"].WhenNullOrEmpty(""),
                                               Amount = detail["Amount"].ToString().MoneyFormat(),
                                           })
                             });

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            return jsSerializer.Serialize(rsPayment);
        }

        // Used for getting the bonus plan configuration
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string BonusPlanConfigurationGet(string ActiveFlg)
        {
            DataSet ds = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.BonusPlanConfigurationGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@ActiveFlg", SqlDbType.VarChar);
                    Command.Parameters["@ActiveFlg"].Value = ActiveFlg;

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(ds);
                    }
                }
            }

            var rsConfiguration = (from bonusPlan in ds.Tables[0].AsEnumerable()
                                   select new
                                   {
                                       BonusPlanID = bonusPlan["BonusPlanID"],
                                       BonusPlan = bonusPlan["BonusPlan"],
                                       CommunityCount = bonusPlan["CommunityCount"],
                                       RevenueEntryCount = bonusPlan["RevenueEntryCount"],
                                       JobRoleCount = bonusPlan["JobRoleCount"],
                                       EmployeeCount = bonusPlan["EmployeeCount"],
                                       OverrideEmployeeCount = bonusPlan["OverrideEmployeeCount"],
                                   });

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            return jsSerializer.Serialize(rsConfiguration);
        }

        // Used for getting the bonus plan detail used on the profile page
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string BonusPlanDetailGet(string BonusPlanID)
        {
            BonusPlanBO objBonusPlan = new BonusPlanBO();
            objBonusPlan.BonusPlanID = BonusPlanID;

            DataSet ds = objBonusPlan.BonusPlanDetailGet();

            var rsBonusPlan = (from bonusPlan in ds.Tables[0].AsEnumerable()
                               select new
                               {
                                   BonusPlanID = bonusPlan["BonusPlanID"],
                                   BonusPlan = bonusPlan["BonusPlan"],
                                   PaymentProcessFlg = bonusPlan["PaymentProcessFlg"],
                                   ExcludeLookBackFlg = bonusPlan["ExcludeLookBackFlg"],
                                   TypeOfEarning = bonusPlan["PS_Type_Of_Earning"],
                                   EmailFlg = bonusPlan["EmailFlg"],
                                   CreateDT = bonusPlan["CreateDT"],
                                   CreateBy = bonusPlan["CreateBy"],
                                   ModifyDT = bonusPlan["ModifyDT"],
                                   ModifyBy = bonusPlan["ModifyBy"],
                                   ActiveFlg = bonusPlan["ActiveFlg"],
                               });

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            return jsSerializer.Serialize(rsBonusPlan);
        }

        // Used for updating the bonus plan detail
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string BonusPlanDetailUpdate(string BonusPlanID, string BonusPlan, string TypeOfEarning, string PaymentProcessFlg, string ExcludeLookBackFlg, string EmailFlg, string ActiveFlg, string Username)
        {
            BonusPlanBO objBonusPlan = new BonusPlanBO();
            objBonusPlan.BonusPlanID = BonusPlanID;
            objBonusPlan.BonusPlan = BonusPlan;
            objBonusPlan.TypeOfEarning = TypeOfEarning;
            objBonusPlan.PaymentProcessFlg = PaymentProcessFlg;
            objBonusPlan.ExcludeLookBackFlg = ExcludeLookBackFlg;
            objBonusPlan.EmailFlg = EmailFlg;
            objBonusPlan.Audit.ModifyBy = Username;
            objBonusPlan.Audit.ActiveFlg = ActiveFlg;

            objBonusPlan.BonusPlanUpdate();

            return BonusPlanDetailGet(BonusPlanID);
        }

        // Gets the list of communities for a given user
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string CommunityUserAccessGet(string Username)
        {
            DataSet ds = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("Portal.CommunityUserAccessGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@Username", SqlDbType.VarChar);
                    Command.Parameters["@Username"].Value = Username;

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(ds);
                    }
                }
            }

            var rsCommunity = (from community in ds.Tables[0].AsEnumerable()
                               select new
                               {
                                   CommunityName = community["CommunityName"],
                                   CommunityNumber = community["CommunityNumber"],
                                   CommunityID = community["CommunityID"]
                               });

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            return jsSerializer.Serialize(rsCommunity);
        }

        // Gets the list of all revenue entry types available
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string RevenueEntryTypeGet()
        {
            RevenueEntryTypeBO objRevenueEntry = new RevenueEntryTypeBO();
            objRevenueEntry.Audit.ActiveFlg = "1";

            DataSet ds = objRevenueEntry.RevenueEntryTypeGet();

            var rsRevenueEntry = (from entry in ds.Tables[0].AsEnumerable()
                                  select new
                                  {
                                      RevenueEntryTypeID = entry["RevenueEntryTypeID"],
                                      RevenueEntryType = entry["RevenueEntryType"]
                                  });

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            return jsSerializer.Serialize(rsRevenueEntry);
        }

        // Gets the list of all revenue entry types available
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string BonusPlanToRevenueDetailGet(string BonusPlanID)
        {
            BonusPlanBO objBonusPlan = new BonusPlanBO();
            objBonusPlan.BonusPlanID = BonusPlanID;

            DataSet ds = objBonusPlan.BonusPlanToRevenueDetailGet();

            var rsRevenueEntry = ds.Tables[0].AsEnumerable()
                                  .GroupBy(g => g["LedgerEntryID"])
                                  .Select(g => g.OrderByDescending(d => d["EffectiveDT"]).FirstOrDefault())
                                  .Select(r =>
                                  new
                                  {
                                      RevenueEntryTypeToLedgerEntryID = r["RevenueEntryTypeToLedgerEntryID"],
                                      BonusPlanID = r["BonusPlanID"],
                                      RevenueEntryTypeID = r["RevenueEntryTypeID"],
                                      RevenueEntryType = r["RevenueEntryType"],
                                      LedgerEntryID = r["LedgerEntryID"],
                                      LedgerEntry = r["LedgerEntry"],
                                      Percentage = r["Percentage"],
                                      FlatRate = r["FlatRate"],
                                      EffectiveDT = r["EffectiveDT"],
                                      //ConfiguredFlg = entry["ConfiguredFlg"],
                                  });

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            return jsSerializer.Serialize(rsRevenueEntry);
        }

        // Adds a ledger entry to the configuration
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string LedgerEntryTypeInsert(string RevenueEntryTypeID, string LedgerEntryID, string BonusPlanID, string Username)
        {
            DataSet ds = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.LedgerEntryTypeInsert", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@RevenueEntryTypeID", SqlDbType.VarChar);
                    Command.Parameters["@RevenueEntryTypeID"].Value = RevenueEntryTypeID;

                    Command.Parameters.Add("@LedgerEntryID", SqlDbType.VarChar);
                    Command.Parameters["@LedgerEntryID"].Value = LedgerEntryID;

                    Command.Parameters.Add("@BonusPlanID", SqlDbType.VarChar);
                    Command.Parameters["@BonusPlanID"].Value = BonusPlanID;

                    Command.Parameters.Add("@Username", SqlDbType.VarChar);
                    Command.Parameters["@Username"].Value = Username;

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(ds);
                    }
                }
            }

            return BonusPlanToRevenueDetailGet(BonusPlanID);
        }

        // Deletes a ledger entry type from the configuration
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string LedgerEntryTypeDelete(string RevenueEntryTypeToLedgerEntryID, string BonusPlanID)
        {
            DataSet ds = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.LedgerEntryTypeDelete", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@RevenueEntryTypeToLedgerEntryID", SqlDbType.VarChar);
                    Command.Parameters["@RevenueEntryTypeToLedgerEntryID"].Value = RevenueEntryTypeToLedgerEntryID;

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(ds);
                    }
                }
            }

            return BonusPlanToRevenueDetailGet(BonusPlanID);
        }

        #region JobCode

        // Gets the list of all revenue entry types available
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string BonusPlanToJobCodeDetailGet(string BonusPlanID)
        {
            BonusPlanBO objBonusPlan = new BonusPlanBO();
            objBonusPlan.BonusPlanID = BonusPlanID;

            DataSet ds = objBonusPlan.BonusPlanToJobCodeDetailGet();

            var rsJobCode = ds.Tables[0].AsEnumerable()
                             .GroupBy(g => g["JobCodeID"])
                             .Select(g => g.OrderByDescending(d => d["EffectiveDT"]).FirstOrDefault())
                             .Select(r =>
                             new
                             {
                                 BonusPlanToJobCodeID = r["BonusPlanToJobCodeID"],
                                 BonusPlanID = r["BonusPlanID"],
                                 JobCodeID = r["JobCodeID"],
                                 JobCode = r["JobCode"],
                                 JobTitle = r["JobTitle"],
                                 CommissionBase = r["CommissionBase"].WhenNullOrEmpty(""),
                                 Multiplier = r["Multiplier"].WhenNullOrEmpty(""),
                                 Percentage = r["Percentage"].WhenNullOrEmpty(""),
                                 CreateDt = r["CreateDt"],
                                 CreateBy = r["CreateBy"],
                                 ActiveFlg = r["ActiveFlg"],
                                 Community = r["Community"].ToString().WhenNullOrEmpty("All"),
                                 EffectiveDT = r["EffectiveDT"],
                             });

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            return jsSerializer.Serialize(rsJobCode);
        }

        // Adds a ledger entry to the configuration
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //public string BonusPlanToJobCodeInsert(string BonusPlanID, string JobCodeID, string Percentage, string CommissionBase, string Multiplier, string CommunityNumber, string Username)
        public string BonusPlanToJobCodeInsert(BonusPlanDTO bonusPlan)

        {
            DataSet ds = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.BonusPlanToJobCodeInsert", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@BonusPlanID", SqlDbType.VarChar);
                    Command.Parameters["@BonusPlanID"].Value = bonusPlan.BonusPlanID;

                    Command.Parameters.Add("@JobCodeID", SqlDbType.VarChar);
                    Command.Parameters["@JobCodeID"].Value = bonusPlan.JobCodeID;

                    Command.Parameters.Add("@Percentage", SqlDbType.VarChar);
                    Command.Parameters["@Percentage"].Value = bonusPlan.Percentage;

                    Command.Parameters.Add("@CommissionBase", SqlDbType.VarChar);
                    Command.Parameters["@CommissionBase"].Value = bonusPlan.JobCodeCommissionBase;

                    Command.Parameters.Add("@Multiplier", SqlDbType.VarChar);
                    Command.Parameters["@Multiplier"].Value = bonusPlan.JobCodeMultiplier;

                    Command.Parameters.Add("@CommunityNumber", SqlDbType.VarChar);
                    Command.Parameters["@CommunityNumber"].Value = bonusPlan.CommunityNumber;

                    Command.Parameters.Add("@Username", SqlDbType.VarChar);
                    Command.Parameters["@Username"].Value = bonusPlan.Username;

                    Command.Parameters.Add("@EffectiveDt", SqlDbType.VarChar);
                    Command.Parameters["@EffectiveDt"].Value = bonusPlan.EffectiveDt.NullIfEmpty();

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(ds);
                    }
                }
            }

            return BonusPlanToJobCodeDetailGet(bonusPlan.BonusPlanID);
        }

        // Deletes a job code from the configuration
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string BonusPlanToJobCodeDelete(string BonusPlanToJobCodeID, string BonusPlanID)
        {
            DataSet ds = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.BonusPlanToJobCodeDelete", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@BonusPlanToJobCodeID", SqlDbType.VarChar);
                    Command.Parameters["@BonusPlanToJobCodeID"].Value = BonusPlanToJobCodeID;

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(ds);
                    }
                }
            }

            return BonusPlanToJobCodeDetailGet(BonusPlanID);
        }

        #endregion JobCode

        #region Approver

        // Gets the list of all approvals
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string BonusPlanToApprovalWorkflowGet(string BonusPlanID)
        {
            BonusPlanBO objBonusPlan = new BonusPlanBO();
            objBonusPlan.BonusPlanID = BonusPlanID;

            DataSet ds = objBonusPlan.BonusPlanToApprovalWorkflowGet();

            var rsApproval = (from approval in ds.Tables[0].AsEnumerable()
                              select new
                              {
                                  ApproverManagementID = approval["ApproverManagementID"],
                                  BonusPlanID = approval["BonusPlanID"],
                                  JobCategoryID = approval["JobCategoryID"],
                                  JobCategory = approval["JobCategory"],
                                  Sort = approval["Sort"],
                                  CriteriaValue = approval["CriteriaValue"],
                                  CreateDt = approval["CreateDt"],
                                  CreateBy = approval["CreateBy"],
                                  ActiveFlg = approval["ActiveFlg"]
                              });

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            return jsSerializer.Serialize(rsApproval);
        }

        // Adds an approver to the configuration
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string ApproverManagementInsert(string BonusPlanID, string JobCategoryID, string ApprovalSort, string ApprovalAmount, string Username)
        {
            DataSet ds = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.ApproverManagementInsert", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@BonusPlanID", SqlDbType.Int);
                    Command.Parameters["@BonusPlanID"].Value = BonusPlanID;

                    Command.Parameters.Add("@JobCategoryID", SqlDbType.Int);
                    Command.Parameters["@JobCategoryID"].Value = JobCategoryID;

                    Command.Parameters.Add("@ApprovalSort", SqlDbType.Int);
                    Command.Parameters["@ApprovalSort"].Value = ApprovalSort.NullIfEmpty();

                    Command.Parameters.Add("@ApprovalAmount", SqlDbType.VarChar);
                    Command.Parameters["@ApprovalAmount"].Value = ApprovalAmount.NullIfEmpty();

                    Command.Parameters.Add("@Username", SqlDbType.VarChar);
                    Command.Parameters["@Username"].Value = Username;

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(ds);
                    }
                }
            }

            return BonusPlanToApprovalWorkflowGet(BonusPlanID);
        }

        // Deletes a job code from the configuration
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string ApproverManagementDelete(string ApproverManagementID, string BonusPlanID)
        {
            DataSet ds = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.ApproverManagementDelete", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@ApproverManagementID", SqlDbType.VarChar);
                    Command.Parameters["@ApproverManagementID"].Value = ApproverManagementID;

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(ds);
                    }
                }
            }

            return BonusPlanToApprovalWorkflowGet(BonusPlanID);
        }

        #endregion Approver

        #region Approval

        // Gets the list of all approvals for this user
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string ApprovalByUsernameGet(string Username)
        {
            DashboardBO objDashboard = new DashboardBO();
            objDashboard.Audit.UserName = Username;

            DataSet ds = objDashboard.snpApprovalByUsernameGet();

            var rsApproval = (from approval in ds.Tables[0].AsEnumerable()
                              select new
                              {
                                  PaymentToApprovalID = approval["PaymentToApprovalID"],
                                  ApprovalGUID = approval["ApprovalGUID"],
                                  Amount = approval["Amount"].ToString().MoneyFormat(),
                                  UserName = approval["UserName"],
                                  Approver = approval["Approver"],
                                  EmployeeID = approval["EmployeeID"],
                                  ApproveFlg = approval["ApproveFlg"],
                                  DenyFlg = approval["DenyFlg"],
                                  Sort = approval["Sort"],
                                  ExceptionFlg = approval["ExceptionFLg"],
                                  CommunityID = approval["CommunityID"],
                                  CommunityName = approval["CommunityName"]
                              });

            var rsCommunity = (from approval in ds.Tables[0].AsEnumerable()
                               select new
                               {
                                   CommunityID = approval["CommunityID"],
                                   CommunityName = approval["CommunityName"]
                               }).Distinct();

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            return jsSerializer.Serialize(new { Approval = rsApproval, Community = rsCommunity });
        }

        // Gets the approval details for this user
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string ApprovalByUsernameDetailGet(string Username)
        {
            DashboardBO objDashboard = new DashboardBO();
            objDashboard.Audit.UserName = Username;

            DataSet ds = objDashboard.snpApprovalByUsernameDetailGet();

            var rsApproval = (from approval in ds.Tables[0].AsEnumerable()
                              select new
                              {
                                  ApprovalGUID = approval["ApprovalGUID"],
                                  Amount = approval["Amount"].ToString().MoneyFormat(),
                                  CommunityNumber = approval["CommunityNumber"],
                                  CustomerID = approval["CustomerID"].WhenNullOrEmpty("--"),
                                  MoveInDt = approval["MoveInDt"].WhenNullOrEmpty("--"),
                                  Resident = approval["Resident"].WhenNullOrEmpty("--"),
                                  Note = approval["Note"].WhenNullOrEmpty(""),
                                  CreateBy = approval["CreateBy"],
                                  CommunityID = approval["CommunityID"]
                              });

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            return jsSerializer.Serialize(rsApproval);
        }

        #endregion Approval
    }
}