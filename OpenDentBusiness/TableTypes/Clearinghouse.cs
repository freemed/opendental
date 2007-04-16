using System;
using System.Collections;


namespace OpenDentBusiness{
	
	///<summary>Since we can send e-claims to multiple clearinghouses, this table keeps track of each clearinghouse.  Will eventually be used for individual carriers as well if they accept </summary>
	public class Clearinghouse{
		///<summary>Primary key.</summary>
		public int ClearinghouseNum;
		///<summary>Description of this clearinghouse</summary>
		public string Description;
		///<summary>The path to export the X12 file to. \ is now optional.</summary>
		public string ExportPath;
		///<summary>Set to true if this is the default clearinghouse to which you want most of your e-claims sent.</summary>
		public bool IsDefault;
		///<summary>A list of all payors which should have claims sent to this clearinghouse. Comma delimited with no spaces.  Not necessary if IsDefault.</summary>
		public string Payors;
		///<summary>Enum:ElectronicClaimFormat The format of the file that gets sent electronically.</summary>
		public ElectronicClaimFormat Eformat;
		///<summary>Sender ID Qualifier. Usually ZZ, sometimes 30. Seven other values are allowed as specified in X12 document, but probably never used.</summary>
		public string ISA05;
		///<summary>Used in ISA06, GS02, 1000A NM1, and 1000A PER.  If blank, then 810624427 is used to indicate Open Dental.</summary>
		public string SenderTIN;
		///<summary>Receiver ID Qualifier.  Usually ZZ, sometimes 30. Seven other values are allowed as specified in X12 document, but probably never used.</summary>
		public string ISA07;
		///<summary>Receiver ID. Also used in GS03. Provided by clearinghouse. Examples: BCBSGA or 0135WCH00(webMD)</summary>
		public string ISA08;
		///<summary>"P" for Production or "T" for Test.</summary>
		public string ISA15;
		///<summary>Password is usually combined with the login ID for user validation.</summary>
		public string Password;
		///<summary>The path that all incoming response files will be saved to. \ is now optional.</summary>
		public string ResponsePath;
		///<summary>Enum:EclaimsCommBridge  One of the included hard-coded communications briges.  Or none to just create the claim files without uploading.</summary>
		public EclaimsCommBridge CommBridge;
		///<summary>If applicable, this is the name of the client program to launch.  It is even used by the hard-coded comm bridges, because the user may have changed the installation directory or exe name.</summary>
		public string ClientProgram;
		///<summary>Each clearinghouse increments their batch numbers by one each time a claim file is sent.  User never sees this number.  Maxes out at 999, then loops back to 1.  This field is skipped during all update and retreival except if specifically looking for this one field.  Defaults to 0 for brand new clearinghouses, which causes the first batch to go out as #1.</summary>
		public int LastBatchNumber;
		///<summary>1,2,3,or 4. The port that the modem is connected to if applicable. Always uses 9600 baud and standard settings. Will crash if port or modem not valid.</summary>
		public int ModemPort;
		///<summary>A clearinghouse usually has a login ID that is used with the password in order to access the remote server.  This value is not usualy used within the actual claim.</summary>
		public string LoginID;
		///<summary>Used in 1000A NM1 and 1000A PER.  But if SenderTIN is blank, then OPEN DENTAL SOFTWARE is used instead.</summary>
		public string SenderName;
		///<summary>Used in 1000A PER.  But if SenderTIN is blank, then 8776861248 is used instead.  10 digit phone is required by WebMD and is universally assumed, so for now, this must be either blank or 10 digits.</summary>
		public string SenderTelephone;
		///<summary>Usually the same as ISA08, but at least one clearinghouse uses a different number here.</summary>
		public string GS03;

		/*//<summary>Returns a copy of the clearinghouse.</summary>
    public ClaimForm Copy(){
			ClaimForm cf=new ClaimForm();
			cf.ClaimFormNum=ClaimFormNum;
			cf.Description=Description;
			cf.IsHidden=IsHidden;
			cf.FontName=FontName;
			cf.FontSize=FontSize;
			cf.UniqueID=UniqueID;
			cf.PrintImages=PrintImages;
			cf.OffsetX=OffsetX;
			cf.OffsetY=OffsetY;
			return cf;
		}*/



	}

	



}









