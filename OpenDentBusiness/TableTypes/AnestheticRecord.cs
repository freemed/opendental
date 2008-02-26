
using System;

namespace OpenDentBusiness
{

    ///<summary>One anesthetic record for one patient on one date.</summary>
    public class AnestheticRecord
    {
        ///<summary>Primary key.</summary>
        public int AnestheticRecordNum;
        ///<summary>FK to patient.PatNum.</summary>
        public int PatNum;
        ///<summary></summary>
        public DateTime AnestheticDate;
        ///<summary>FK to provider.ProvNum.</summary>
        public int ProvNum;
    }





}

