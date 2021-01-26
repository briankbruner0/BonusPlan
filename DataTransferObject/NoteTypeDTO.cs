using Atria.Framework;
using AtriaEM;

namespace BonusPlan
{
    public class NoteTypeDTO
    {
        private string noteTypeID;
        private string noteType;
        private AuditDto audit;

        public string NoteTypeID
        {
            get { return noteTypeID.WhenNullOrEmpty(string.Empty); }
            set { noteTypeID = value; }
        }

        public string NoteType
        {
            get { return noteType.WhenNullOrEmpty(string.Empty); }
            set { noteType = value; }
        }

        public AuditDto Audit
        {
            get
            {
                if (audit == null)
                {
                    audit = new AuditDto();
                }
                return audit;
            }
            set
            {
                audit = value;
            }
        }
    }
}