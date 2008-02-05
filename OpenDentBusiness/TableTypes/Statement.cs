using System;
using System.Collections;
using OpenDental.DataAccess;

namespace OpenDentBusiness{

	///<summary>Represents one statement for one family.  Usually already sent, but could still be waiting to send.</summary>
	[DataObject("statement")]
	public class Statement : DataObjectBase {
		[DataField("StatementNum", PrimaryKey=true, AutoNumber=true)]
		private int statementNum;
		bool statementNumChanged;
		/// <summary>Primary key.</summary>
		public int StatementNum {
			get { return statementNum; }
			set { if(statementNum!=value){statementNum = value; MarkDirty(); statementNumChanged = true; }}
		}
		public bool StatementNumChanged {
			get { return statementNumChanged; }
		}

		[DataField("PatNum")]
		private int patNum;
		bool patNumChanged;
		/// <summary>FK to patient.PatNum. Typically the guarantor unless the family has been rearranged.</summary>
		public int PatNum {
			get { return patNum; }
			set { if(patNum!=value){patNum = value; MarkDirty(); patNumChanged = true;} }
		}
		public bool PatNumChanged {
			get { return patNumChanged; }
		}

		[DataField("DateSent")]
		private DateTime dateSent;
		bool dateSentChanged;
		/// <summary>This will always be a valid and reasonable date regardless of whether it's actually been sent yet.</summary>
		public DateTime DateSent {
			get { return dateSent; }
			set { if(dateSent!=value){dateSent = value; MarkDirty(); dateSentChanged = true; }}
		}
		public bool DateSentChanged {
			get { return dateSentChanged; }
		}

		[DataField("DateRangeFrom")]
		private DateTime dateRangeFrom;
		bool dateRangeFromChanged;
		/// <summary>Typically 45 days before dateSent</summary>
		public DateTime DateRangeFrom {
			get { return dateRangeFrom; }
			set { if(dateRangeFrom!=value){dateRangeFrom = value; MarkDirty(); dateRangeFromChanged = true; }}
		}
		public bool DateRangeFromChanged {
			get { return dateRangeFromChanged; }
		}

		[DataField("DateRangeTo")]
		private DateTime dateRangeTo;
		bool dateRangeToChanged;
		/// <summary>Any date >= year 2200 is considered max val.</summary>
		public DateTime DateRangeTo {
			get { return dateRangeTo; }
			set { if(dateRangeTo!=value){dateRangeTo = value; MarkDirty(); dateRangeToChanged = true; }}
		}
		public bool DateRangeToChanged {
			get { return dateRangeToChanged; }
		}

		[DataField("Note")]
		private string note;
		bool noteChanged;
		/// <summary>Can include line breaks.  This ordinary note will be in the standard font.</summary>
		public string Note {
			get { return note; }
			set { if(note!=value){note = value; MarkDirty(); noteChanged = true; }}
		}
		public bool NoteChanged {
			get { return noteChanged; }
		}

		[DataField("NoteBold")]
		private string noteBold;
		bool noteBoldChanged;
		/// <summary>More important notes may go here.  Font will be bold.  Color and size of text will be customizable in setup.</summary>
		public string NoteBold {
			get { return noteBold; }
			set { if(noteBold!=value){noteBold = value; MarkDirty(); noteBoldChanged = true; }}
		}
		public bool NoteBoldChanged {
			get { return noteBoldChanged; }
		}

		[DataField("Mode_")]
		private StatementMode mode_;
		bool mode_Changed;
		/// <summary>Enum:StatementMode Mail, InPerson, Email.</summary>
		public StatementMode Mode_ {
			get { return mode_; }
			set { if(mode_!=value){mode_ = value; MarkDirty(); mode_Changed = true; }}
		}
		public bool Mode_Changed {
			get { return mode_Changed; }
		}

		[DataField("HidePayment")]
		private bool hidePayment;
		bool hidePaymentChanged;
		/// <summary>Set true to hide the credit card section, and the please pay box.</summary>
		public bool HidePayment {
			get { return hidePayment; }
			set { if(hidePayment!=value){hidePayment = value; MarkDirty(); hidePaymentChanged = true; }}
		}
		public bool HidePaymentChanged {
			get { return hidePaymentChanged; }
		}

		[DataField("SinglePatient")]
		private bool singlePatient;
		bool singlePatientChanged;
		/// <summary>One patient on statement instead of entire family.</summary>
		public bool SinglePatient {
			get { return singlePatient; }
			set { if(singlePatient!=value){singlePatient = value; MarkDirty(); singlePatientChanged = true; }}
		}
		public bool SinglePatientChanged {
			get { return singlePatientChanged; }
		}

		[DataField("Intermingled")]
		private bool intermingled;
		bool intermingledChanged;
		/// <summary>If entire family, then this determines whether they are all intermingled into one big grid, or whether they are all listed in separate grids.</summary>
		public bool Intermingled {
			get { return intermingled; }
			set { if(intermingled!=value){intermingled = value; MarkDirty(); intermingledChanged = true; }}
		}
		public bool IntermingledChanged {
			get { return intermingledChanged; }
		}

		[DataField("IsSent")]
		private bool isSent;
		bool isSentChanged;
		/// <summary>True</summary>
		public bool IsSent {
			get { return isSent; }
			set { if(isSent!=value){isSent = value; MarkDirty(); isSentChanged = true; }}
		}
		public bool IsSentChanged {
			get { return isSentChanged; }
		}
		
		public Statement Copy(){
			return (Statement)Clone();
		}	
	}
}
