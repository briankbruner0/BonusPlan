using AtriaEM;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BonusPlan
{
    public class NoteTypeBO : NoteTypeDTO
    {
        public DataSet NoteTypeGet()
        {
            DataSet dsNoteType = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.NoteTypeGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@ActiveFlg", SqlDbType.Int);
                    Command.Parameters["@ActiveFlg"].Value = Audit.ActiveFlg.NullIfEmpty();

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(dsNoteType);
                    }
                }
            }

            return dsNoteType;
        }
    }
}