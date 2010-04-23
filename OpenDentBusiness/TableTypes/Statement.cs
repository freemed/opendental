using System;
using System.Collections;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{

	///<summary>Represents one statement for one family.  Usually already sent, but could still be waiting to send.</summary>
	[Serializable()]
	public class Statement : TableBase {
		/// <summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long StatementNum;
		/// <summary>FK to patient.PatNum. Typically the guarantor.  Can also be the patient for walkout statements.</summary>
		public long PatNum;
		/// <summary>This will always be a valid and reasonable date regardless of whether it's actually been sent yet.</summary>
		public DateTime DateSent;
		/// <summary>Typically 45 days before dateSent</summary>
		public DateTime DateRangeFrom;
		/// <summary>Any date >= year 2200 is considered max val.  We generally try to automate this value to be the same date as the statement rather than the max val.  This is so that when payment plans are displayed, we can add approximately 10 days to effectively show the charge that will soon be due.  Adding the 10 days is not done until display time.</summary>
		public DateTime DateRangeTo;
		/// <summary>Can include line breaks.  This ordinary note will be in the standard font.</summary>
		public string Note;
		/// <summary>More important notes may go here.  Font will be bold.  Color and size of text will be customizable in setup.</summary>
		public string NoteBold;
		/// <summary>Enum:StatementMode Mail, InPerson, Email, Electronic.</summary>
		public StatementMode Mode_;
		/// <summary>Set true to hide the credit card section, and the please pay box.</summary>
		public bool HidePayment;
		/// <summary>One patient on statement instead of entire family.</summary>
		public bool SinglePatient;
		/// <summary>If entire family, then this determines whether they are all intermingled into one big grid, or whether they are all listed in separate grids.</summary>
		public bool Intermingled;
		/// <summary>True</summary>
		public bool IsSent;
		/// <summary>FK to document.DocNum when a pdf has been archived.</summary>
		public long DocNum;
		
		public Statement Copy(){
			return (Statement)this.MemberwiseClone();
		}	
	}
}
