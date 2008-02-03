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
		/// <summary>Any date >= year 2200 is </summary>
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
		/// <summary>Can include line breaks.  For now, entire note will be one single font.</summary>
		public string Note {
			get { return note; }
			set { if(note!=value){note = value; MarkDirty(); noteChanged = true; }}
		}
		public bool NoteChanged {
			get { return noteChanged; }
		}

		[DataField("NoteFontSize")]
		private int noteFontSize;
		bool noteFontSizeChanged;
		/// <summary>Typical is around 10.</summary>
		public int NoteFontSize {
			get { return noteFontSize; }
			set { if(noteFontSize!=value){noteFontSize = value; MarkDirty(); noteFontSizeChanged = true; }}
		}
		public bool NoteFontSizeChanged {
			get { return noteFontSizeChanged; }
		}

		[DataField("NoteFontColor")]
		private int noteFontColor;
		bool noteFontColorChanged;
		/// <summary>For now, the entire note must be the same color.  There is no bold flag.</summary>
		public int NoteFontColor {
			get { return noteFontColor; }
			set { if(noteFontColor!=value){noteFontColor = value; MarkDirty(); noteFontColorChanged = true; }}
		}
		public bool NoteFontColorChanged {
			get { return noteFontColorChanged; }
		}

		[DataField("Mode_")]
		private StatementMode mode_;
		bool mode_Changed;
		/// <summary>Enum:StatementMode Unsent, Email, Mail, InPerson.</summary>
		public StatementMode Mode_ {
			get { return mode_; }
			set { if(mode_!=value){mode_ = value; MarkDirty(); mode_Changed = true; }}
		}
		public bool Mode_Changed {
			get { return mode_Changed; }
		}
		
		public Statement Copy(){
			return (Statement)Clone();
		}	
	}
}
