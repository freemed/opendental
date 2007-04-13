/*using System;
using System.Collections;

namespace OpenDentBusiness{
	
	/// <summary>One batch of electronic transactions.  Typically, a group of claims or responses.  Stores a copy of what was sent for permanent record.</summary>
	public class Ebatch{
		///<summary>Primary key.</summary>
		public int EbatchNum;
		///<summary>The date and time of the batch.</summary>
		public DateTime DateTimeBatch;
		///<summary>FK to clearinghouse.ClearinghouseNum .  Can be 0 if no clearinghouse was involved.</summary>
		public int ClearinghouseNum;
		///<summary>Enum:CommSentOrReceived Neither=0,Sent=1,Received=2.</summary>
		public CommSentOrReceived SentOrReceived;
		///<summary></summary>
		public EbatchType Etype;

		///<summary></summary>
		public Ebatch Copy(){
			Ebatch e=new Ebatch();
			e.EbatchNum=EbatchNum;
			e.DateTimeBatch=DateTimeBatch;
			e.ClearinghouseNum=ClearinghouseNum;
			e.SentOrReceived=SentOrReceived;
			e.Etype=Etype;
			return e;
		}

	}

	




}
*/
















