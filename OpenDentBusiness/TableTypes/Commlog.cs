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
		///<summary>Enum:CommItemType .</summary>
		public CommItemType CommType;
		///<summary>Note for this commlog entry.</summary>
		public string Note;
		///<summary>Enum:CommItemMode Phone, email, etc.</summary>
		public CommItemMode Mode_;
		///<summary>Enum:CommSentOrReceived Neither=0,Sent=1,Received=2.</summary>
		public CommSentOrReceived SentOrReceived;
		//<summary>FK to emailmessage.EmailMessageNum, if there is an associated email. Otherwise 0.</summary>
		//public int EmailMessageNum;

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
			return c;
		}

	}

	




}

















