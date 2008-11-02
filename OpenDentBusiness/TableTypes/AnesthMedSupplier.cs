using System;
using System.Collections;
using OpenDental.DataAccess;

namespace OpenDentBusiness{
	///<summary>An individual anesthetic medication supplier.</summary>
	[DataObject("anesthmedsuppliers")]
	public class AnesthMedSupplier : DataObjectBase{

		[DataField("SupplierIDNum",PrimaryKey=true,AutoNumber=true)]
		private int supplierIDNum;
		private bool supplierIDNumChanged;
		///<summary>Primary key.</summary>
		public int SupplierIDNum{
			get{return SupplierIDNum;}
            set {if (supplierIDNum!=value){supplierIDNum=value; MarkDirty(); supplierIDNumChanged = true;}}
		}
		public bool SupplierIDNumChanged{
			get{return supplierIDNumChanged;}
		}

		[DataField("SupplierName")]
		private string supplierName;
		private bool supplierNameChanged;
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

        [DataField("PhoneExt")]
        private string phoneExt;
        private bool phoneExtChanged;
        ///<summary>Includes all punctuation.</summary>
        public string PhoneExt
        {
            get {return phoneExt;}
            set {if(phoneExt!=value){ phoneExt = value; MarkDirty(); phoneExtChanged = true;}}
        }
        public bool PhoneExtChanged{
            get {return phoneExtChanged;}
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

		[DataField("Addr1")]
		private string addr1;
		private bool addr1Changed;
		///<summary></summary>
		public string Addr1{
			get{return addr1;}
			set{if(addr1!=value){addr1=value;MarkDirty();addr1Changed=true;}}
		}
		public bool Addr1Changed{
			get{return addr1Changed;}
		}

		[DataField("Addr2")]
		private string addr2;
		private bool addr2Changed;
		///<summary>Optional.</summary>
		public string Addr2{
			get{return addr2;}
			set{if(addr2!=value){addr2=value;MarkDirty();addr2Changed=true;}}
		}
		public bool Addr2Changed{
			get{return addr2Changed;}
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

        /*[DataField("Country")]
        private string country;
        private bool countryChanged;
        ///<summary></summary>
        public string Country
        {
            get { return country; }
            set { if (country != value) { country = value; MarkDirty(); countryChanged = true; } }
        }
        public bool CountryChanged
        {
            get { return countryChanged; }
        }*/

        [DataField("Contact")]
        private string contact;
        private bool contactChanged;
        ///<summary>Includes all punctuation.</summary>
        public string Contact
        {
            get {return contact;}
            set {if(contact!=value){contact=value; MarkDirty();contactChanged=true;}}
        }
        public bool ContactChanged{
            get {return contactChanged;}
        }

        [DataField("WebSite")]
        private string webSite;
        private bool webSiteChanged;
        ///<summary></summary>
        public string WebSite{
            get {return webSite;}
            set {if (webSite!=value){webSite=value;MarkDirty();webSiteChanged=true;}}
        }
        public bool WebSiteChanged{
            get { return webSiteChanged; }
        }

		[DataField("Notes")]
		private string notes;
        private bool notesChanged;
		public string Notes{
			get{return notes;}
			set{if(notes!=value){notes=value;MarkDirty();notesChanged=true;}}
		}
		public bool NotesChanged{
			get{return notesChanged;}
		}
		
		//public AnesthMedSupplier Copy(){
			//return (AnesthMedSupplier)Clone();
		//}	
	}
}

