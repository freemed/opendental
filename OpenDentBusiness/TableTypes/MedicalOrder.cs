using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness {
  ///<summary></summary>
  [Serializable]
  public class MedicalOrder:TableBase {
    ///<summary>Primary key.</summary>
    [CrudColumn(IsPriKey=true)]
    public long MedicalOrderNum;
    ///<summary>Enum:MedicalOrderType Laboratory=0,Radiology=1,Medication=2.</summary>
    public MedicalOrderType MedOrderType;
    ///<summary>FK to patient.PatNum</summary>
    public long PatNum;
    ///<summary></summary>
    [CrudColumn(SpecialType=CrudSpecialColType.DateT)]
    public DateTime DateTimeOrder;
		///<summary>User will be required to type entire order out from scratch.</summary>
		public string Description;
		///<summary>If this is true, then this order will show in list of all pending orders to let user attach lab results to a patient.  This flag gets cleared when lab result is attached to a patient.</summary>
		public bool IsLabPending;
		///<summary>EHR requires Active/Discontinued status. 0=Active, 1=Discontinued.</summary>
		public bool IsDiscontinued;

    ///<summary></summary>
    public MedicalOrder Copy() {
      return (MedicalOrder)this.MemberwiseClone();
    }
  }

  public enum MedicalOrderType {
    ///<summary>0- Laboratory</summary>
    Laboratory,
    ///<summary>1- Radiology</summary>
    Radiology,
    ///<summary>2- Medication</summary>
    Medication
  }
}