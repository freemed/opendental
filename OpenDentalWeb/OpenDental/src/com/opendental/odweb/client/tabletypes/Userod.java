package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class Userod {
		/** Primary key. */
		public int UserNum;
		/** . */
		public String UserName;
		/** The password hash, not the actual password.  If no password has been entered, then this will be blank. */
		public String Password;
		/** FK to usergroup.UserGroupNum.  Every user belongs to exactly one user group.  The usergroup determines the permissions. */
		public int UserGroupNum;
		/** FK to employee.EmployeeNum. Cannot be used if provnum is used. Used for timecards to block access by other users. */
		public int EmployeeNum;
		/** FK to clinic.ClinicNum.  Has two purposes.  Firstly, it causes new patients to default to this clinic when entered by this user.  Also, if ClinicIsRestricted is set to be true, then it does not allow this user to have access to other clinics. If 0, then user had access to all clinics regardless of ClinicIsRestricted. */
		public int ClinicNum;
		/** FK to provider.ProvNum.  Cannot be used if EmployeeNum is used. */
		public int ProvNum;
		/** Set true to hide user from login list. */
		public boolean IsHidden;
		/** FK to tasklist.TaskListNum.  0 if no inbox setup yet.  It is assumed that the TaskList is in the main trunk, but this is not strictly enforced.  User can't delete an attached TaskList, but they could move it. */
		public int TaskListInBox;
		/**  Defaults to 3 (regular user) unless specified. Helps populates the Anesthetist, Surgeon, Assistant and Circulator dropdowns properly on FormAnestheticRecord///  */
		public int AnesthProvType;
		/** If set to true, the hide popups button will start out pressed for this user. */
		public boolean DefaultHidePopups;
		/** Gets set to true if strong passwords are turned on, and this user changes their password to a strong password.  We don't store actual passwords, so this flag is the only way to tell. */
		public boolean PasswordIsStrong;
		/** Only used when userod.ClinicNum is set to not be zero.  Prevents user from having access to other clinics. */
		public boolean ClinicIsRestricted;

		/** Deep copy of object. */
		public Userod Copy() {
			Userod userod=new Userod();
			userod.UserNum=this.UserNum;
			userod.UserName=this.UserName;
			userod.Password=this.Password;
			userod.UserGroupNum=this.UserGroupNum;
			userod.EmployeeNum=this.EmployeeNum;
			userod.ClinicNum=this.ClinicNum;
			userod.ProvNum=this.ProvNum;
			userod.IsHidden=this.IsHidden;
			userod.TaskListInBox=this.TaskListInBox;
			userod.AnesthProvType=this.AnesthProvType;
			userod.DefaultHidePopups=this.DefaultHidePopups;
			userod.PasswordIsStrong=this.PasswordIsStrong;
			userod.ClinicIsRestricted=this.ClinicIsRestricted;
			return userod;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Userod>");
			sb.append("<UserNum>").append(UserNum).append("</UserNum>");
			sb.append("<UserName>").append(Serializing.EscapeForXml(UserName)).append("</UserName>");
			sb.append("<Password>").append(Serializing.EscapeForXml(Password)).append("</Password>");
			sb.append("<UserGroupNum>").append(UserGroupNum).append("</UserGroupNum>");
			sb.append("<EmployeeNum>").append(EmployeeNum).append("</EmployeeNum>");
			sb.append("<ClinicNum>").append(ClinicNum).append("</ClinicNum>");
			sb.append("<ProvNum>").append(ProvNum).append("</ProvNum>");
			sb.append("<IsHidden>").append((IsHidden)?1:0).append("</IsHidden>");
			sb.append("<TaskListInBox>").append(TaskListInBox).append("</TaskListInBox>");
			sb.append("<AnesthProvType>").append(AnesthProvType).append("</AnesthProvType>");
			sb.append("<DefaultHidePopups>").append((DefaultHidePopups)?1:0).append("</DefaultHidePopups>");
			sb.append("<PasswordIsStrong>").append((PasswordIsStrong)?1:0).append("</PasswordIsStrong>");
			sb.append("<ClinicIsRestricted>").append((ClinicIsRestricted)?1:0).append("</ClinicIsRestricted>");
			sb.append("</Userod>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"UserNum")!=null) {
					UserNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"UserNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"UserName")!=null) {
					UserName=Serializing.GetXmlNodeValue(doc,"UserName");
				}
				if(Serializing.GetXmlNodeValue(doc,"Password")!=null) {
					Password=Serializing.GetXmlNodeValue(doc,"Password");
				}
				if(Serializing.GetXmlNodeValue(doc,"UserGroupNum")!=null) {
					UserGroupNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"UserGroupNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"EmployeeNum")!=null) {
					EmployeeNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"EmployeeNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ClinicNum")!=null) {
					ClinicNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ClinicNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ProvNum")!=null) {
					ProvNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ProvNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"IsHidden")!=null) {
					IsHidden=(Serializing.GetXmlNodeValue(doc,"IsHidden")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"TaskListInBox")!=null) {
					TaskListInBox=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"TaskListInBox"));
				}
				if(Serializing.GetXmlNodeValue(doc,"AnesthProvType")!=null) {
					AnesthProvType=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"AnesthProvType"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DefaultHidePopups")!=null) {
					DefaultHidePopups=(Serializing.GetXmlNodeValue(doc,"DefaultHidePopups")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"PasswordIsStrong")!=null) {
					PasswordIsStrong=(Serializing.GetXmlNodeValue(doc,"PasswordIsStrong")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"ClinicIsRestricted")!=null) {
					ClinicIsRestricted=(Serializing.GetXmlNodeValue(doc,"ClinicIsRestricted")=="0")?false:true;
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
