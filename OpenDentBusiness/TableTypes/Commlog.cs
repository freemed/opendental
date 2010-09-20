using System;
using System.Collections;

namespace OpenDentBusiness{
	
	/// <summary>Tracks all forms of communications with patients, including emails, phonecalls, postcards, etc.</summary>
	[Serializable]
	public class Commlog:TableBase{
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long CommlogNum;
		///<summary>FK to patient.PatNum.</summary>
		public long PatNum;
		///<summary>Date and time of entry</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.DateT)]
		public DateTime CommDateTime;
		///<summary>FK to definition.DefNum. This will be 0 if IsStatementSent.  Used to be an enumeration in previous versions.</summary>
		public long CommType;
		///<summary>Note for this commlog entry.</summary>
		public string Note;
		///<summary>Enum:CommItemMode Phone, email, etc.</summary>
		public CommItemMode Mode_;
		///<summary>Enum:CommSentOrReceived Neither=0,Sent=1,Received=2.</summary>
		public CommSentOrReceived SentOrReceived;
		//<summary>FK to emailmessage.EmailMessageNum, if there is an associated email. Otherwise 0.</summary>
		//public long EmailMessageNum;
		///<Summary>No longer used.  Use the statement table instead.</Summary>
		public bool IsStatementSent;
		///<summary>FK to userod.UserNum.</summary>
		public long UserNum;
		///<summary>Signature.</summary>
		public string Signature;
		///<summary>True if signed using the Topaz signature pad, false otherwise.</summary>
		public bool SigIsTopaz;

		///<summary></summary>
		public Commlog Copy(){
			return (Commlog)this.MemberwiseClone();
		}

	}

	




}

















