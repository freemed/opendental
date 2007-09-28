using System;
using System.Collections;

namespace OpenDentBusiness{
	
	/// <summary>Tracks all forms of communications with patients, including emails, phonecalls, postcards, etc.</summary>
	public class Commlog{
		///<summary>Primary key.</summary>
		public int CommlogNum;
		///<summary>FK to patient.PatNum.</summary>
		public int PatNum;
		///<summary>Date and time of entry</summary>
		public DateTime CommDateTime;
		///<summary>FK to definition.DefNum. This will be 0 if IsStatementSent.  Used to be an enumeration in previous versions.</summary>
		public int CommType;
		///<summary>Note for this commlog entry.</summary>
		public string Note;
		///<summary>Enum:CommItemMode Phone, email, etc.</summary>
		public CommItemMode Mode_;
		///<summary>Enum:CommSentOrReceived Neither=0,Sent=1,Received=2.</summary>
		public CommSentOrReceived SentOrReceived;
		//<summary>FK to emailmessage.EmailMessageNum, if there is an associated email. Otherwise 0.</summary>
		//public int EmailMessageNum;
		///<Summary>Replaces the old way of doing it where StatementSent was one of the types.</Summary>
		public bool IsStatementSent;

		///<summary></summary>
		public Commlog Copy(){
			Commlog c=new Commlog();
			c.CommlogNum=CommlogNum;
			c.PatNum=PatNum;
			c.CommDateTime=CommDateTime;
			c.CommType=CommType;
			c.Note=Note;
			c.Mode_=Mode_;
			c.SentOrReceived=SentOrReceived;
			c.IsStatementSent=IsStatementSent;
			return c;
		}

	}

	




}

















