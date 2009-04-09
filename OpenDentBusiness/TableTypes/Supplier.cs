using System;
using System.Collections;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{

	///<summary>A company that provides supplies for the office, typically dental supplies.</summary>
	[DataObject("supplier")]
	public class Supplier : DataObjectBase {
		[DataField("SupplierNum", PrimaryKey=true, AutoNumber=true)]
		private int supplierNum;
		bool supplierNumChanged;
		/// <summary>Primary key.</summary>
		public int SupplierNum {
			get { return supplierNum; }
			set { supplierNum = value; MarkDirty(); supplierNumChanged = true; }
		}
		public bool SupplierNumChanged {
			get { return supplierNumChanged; }
		}

		[DataField("Name")]
		private string name;
		bool nameChanged;
		/// <summary>.</summary>
		public string Name {
			get { return name; }
			set { name = value; MarkDirty(); nameChanged = true; }
		}
		public bool NameChanged {
			get { return nameChanged; }
		}

		[DataField("Phone")]
		private string phone;
		bool phoneChanged;
		/// <summary>.</summary>
		public string Phone {
			get { return phone; }
			set { phone = value; MarkDirty(); phoneChanged = true; }
		}
		public bool PhoneChanged {
			get { return phoneChanged; }
		}

		[DataField("CustomerId")]
		private string customerId;
		bool customerIdChanged;
		/// <summary>The customer ID that this office uses for transactions with the supplier</summary>
		public string CustomerId {
			get { return customerId; }
			set { customerId = value; MarkDirty(); customerIdChanged = true; }
		}
		public bool CustomerIdChanged {
			get { return customerIdChanged; }
		}

		[DataField("Website")]
		private string website;
		bool websiteChanged;
		/// <summary>Full address to website.  We might make it clickable.</summary>
		public string Website {
			get { return website; }
			set { website = value; MarkDirty(); websiteChanged = true; }
		}
		public bool WebsiteChanged {
			get { return websiteChanged; }
		}

		[DataField("UserName")]
		private string userName;
		bool userNameChanged;
		/// <summary>The username used to log in to the supplier website.</summary>
		public string UserName {
			get { return userName; }
			set { userName = value; MarkDirty(); userNameChanged = true; }
		}
		public bool UserNameChanged {
			get { return userNameChanged; }
		}

		[DataField("Password")]
		private string password;
		bool passwordChanged;
		/// <summary>The password to log in to the supplier website.  Not encrypted or hidden in any way.</summary>
		public string Password {
			get { return password; }
			set { password = value; MarkDirty(); passwordChanged = true; }
		}
		public bool PasswordChanged {
			get { return passwordChanged; }
		}

		[DataField("Note")]
		private string note;
		bool noteChanged;
		/// <summary>Any note regarding supplier.  Could hold address, CC info, etc.</summary>
		public string Note {
			get { return note; }
			set { note = value; MarkDirty(); noteChanged = true; }
		}
		public bool NoteChanged {
			get { return noteChanged; }
		}

		

			
	}

	

}









