﻿using System;
using System.Collections;
using OpenDental.DataAccess;

namespace OpenDentBusiness{
	///<summary>An individual anesthetic medication supplier.</summary>
	[DataObject("anesthmedsupplier")]
	public class AnesthMedSupplier : DataObjectBase{
		[DataField("AnesthMedSupplierNum",PrimaryKey=true,AutoNumber=true)]
		private int anesthmedsupplierNum;
		private bool anesthmedsupplierNumChanged;
		///<summary>Primary key.</summary>
		public int AnesthMedSupplierNum{
			get{return AnesthMedSupplierNum;}
			set{if(anesthmedsupplierNum!=value){anesthmedsupplierNum=value;MarkDirty();anesthmedsupplierNumChanged=true;}}
		}
		public bool AnesthMedSupplierNumChanged{
			get{return anesthmedsupplierNumChanged;}
		}

		[DataField("SupplierID")]
		private string supplierID;
		private bool supplierIDChanged;
		///<summary>The Supplier's unique ID.</summary>
		public string SupplierID{
			get{return SupplierID;}
			set { if (supplierID != value) { supplierID = value; MarkDirty(); supplierIDChanged = true; } }
		}
		public bool SupplierIDChanged{
			get{return supplierIDChanged;}
		}

		[DataField("SupplierName")]
		private string supplierName;
		private bool supplierNameChanged;
		///<summary>For now, it can just be a common description.  Later, it might have to be an official designation.</summary>
		public string SupplierName{
			get{return supplierName;}
			set{if(supplierName!=value){supplierName=value;MarkDirty();supplierNameChanged=true;}}
		}
		public bool SupplierNameChanged{
			get{return supplierNameChanged;}
		}

		[DataField("Phone")]
		private string phone;
		private bool phoneChanged;
		///<summary>Includes all punctuation.</summary>
		public string Phone{
			get{return phone;}
			set{if(phone!=value){phone=value;MarkDirty();phoneChanged=true;}}
		}
		public bool PhoneChanged{
			get{return phoneChanged;}
		}

		[DataField("Fax")]
		private string fax;
		private bool faxChanged;
		///<summary>Includes all punctuation.</summary>
		public string Fax{
			get{return fax;}
			set{if(fax!=value){fax=value;MarkDirty();faxChanged=true;}}
		}
		public bool FaxChanged{
			get{return faxChanged;}
		}

		[DataField("Address")]
		private string address;
		private bool addressChanged;
		///<summary></summary>
		public string Address{
			get{return address;}
			set{if(address!=value){address=value;MarkDirty();addressChanged=true;}}
		}
		public bool AddressChanged{
			get{return addressChanged;}
		}

		[DataField("Address2")]
		private string address2;
		private bool address2Changed;
		///<summary>Optional.</summary>
		public string Address2{
			get{return address2;}
			set{if(address2!=value){address2=value;MarkDirty();address2Changed=true;}}
		}
		public bool Address2Changed{
			get{return address2Changed;}
		}

		[DataField("City")]
		private string city;
		private bool cityChanged;
		///<summary></summary>
		public string City{
			get{return city;}
			set{if(city!=value){city=value;MarkDirty();cityChanged=true;}}
		}
		public bool CityChanged{
			get{return cityChanged;}
		}

		[DataField("State")]
		private string state;
		private bool stateChanged;
		///<summary>Two char, uppercase.</summary>
		public string State{
			get{return state;}
			set{if(state!=value){state=value;MarkDirty();stateChanged=true;}}
		}
		public bool StateChanged{
			get{return stateChanged;}
		}

		[DataField("Zip")]
		private string zip;
		private bool zipChanged;
		///<summary></summary>
		public string Zip{
			get{return zip;}
			set{if(zip!=value){zip=value;MarkDirty();zipChanged=true;}}
		}
		public bool ZipChanged{
			get{return zipChanged;}
		}

		[DataField("Note")]
		private string note;
		private bool noteChanged;
		///<summary>A freeform note for any info that is needed about the Supplier, such as hours.</summary>
		public string Note{
			get{return note;}
			set{if(note!=value){note=value;MarkDirty();noteChanged=true;}}
		}
		public bool NoteChanged{
			get{return noteChanged;}
		}
		
		public AnesthMedSupplier Copy(){
			return (AnesthMedSupplier)Clone();
		}	
	}
}

