package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		public Userod deepCopy() {
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
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Userod>");
			sb.append("<UserNum>").append(UserNum).append("</UserNum>");
			sb.append("<UserName>").append(Serializing.escapeForXml(UserName)).append("</UserName>");
			sb.append("<Password>").append(Serializing.escapeForXml(Password)).append("</Password>");
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

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"UserNum")!=null) {
					UserNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"UserNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"UserName")!=null) {
					UserName=Serializing.getXmlNodeValue(doc,"UserName");
				}
				if(Serializing.getXmlNodeValue(doc,"Password")!=null) {
					Password=Serializing.getXmlNodeValue(doc,"Password");
				}
				if(Serializing.getXmlNodeValue(doc,"UserGroupNum")!=null) {
					UserGroupNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"UserGroupNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"EmployeeNum")!=null) {
					EmployeeNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"EmployeeNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ClinicNum")!=null) {
					ClinicNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ClinicNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ProvNum")!=null) {
					ProvNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProvNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"IsHidden")!=null) {
					IsHidden=(Serializing.getXmlNodeValue(doc,"IsHidden")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"TaskListInBox")!=null) {
					TaskListInBox=Integer.valueOf(Serializing.getXmlNodeValue(doc,"TaskListInBox"));
				}
				if(Serializing.getXmlNodeValue(doc,"AnesthProvType")!=null) {
					AnesthProvType=Integer.valueOf(Serializing.getXmlNodeValue(doc,"AnesthProvType"));
				}
				if(Serializing.getXmlNodeValue(doc,"DefaultHidePopups")!=null) {
					DefaultHidePopups=(Serializing.getXmlNodeValue(doc,"DefaultHidePopups")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"PasswordIsStrong")!=null) {
					PasswordIsStrong=(Serializing.getXmlNodeValue(doc,"PasswordIsStrong")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"ClinicIsRestricted")!=null) {
					ClinicIsRestricted=(Serializing.getXmlNodeValue(doc,"ClinicIsRestricted")=="0")?false:true;
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing Userod: "+e.getMessage());
			}
		}


}
