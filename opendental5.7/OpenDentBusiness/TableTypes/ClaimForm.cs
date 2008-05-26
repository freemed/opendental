using System;
using System.Collections;
using System.Xml.Serialization;

namespace OpenDentBusiness{
	///<summary>Stores the information for printing different types of claim forms.  Each claimform has many claimformitems attached to it, one for each field on the claimform.  This table has nothing to do with the actual claims.  It just describes how to print them.</summary>
	public class ClaimForm{
		///<summary>Primary key.</summary>
		[XmlIgnore]
		public int ClaimFormNum;
		///<summary>eg. ADA2002 or CA Medicaid</summary>
		public string Description;
		///<summary>If true, then it will not be displayed in various claim form lists as a choice.</summary>
		[XmlIgnore]
		public bool IsHidden;
		///<summary>Valid font name for all text on the form.</summary>
		public string FontName="";
		///<summary>Font size for all text on the form.</summary>
		public float FontSize;
		///<summary>For instance OD12 or JoeDeveloper9.  If you are a developer releasing claimforms, then this should be your name or company followed by a unique number.  This will later make it easier for you to maintain your claimforms for your customers.  All claimforms that we release will be of the form OD##.  Forms that the user creates will have this field blank, protecting them from being changed by us.  So far, we have built the following claimforms: ADA2002=OD1, Denti-Cal=OD2, ADA2000=OD3, HCFA1500=OD4, HCFA1500preprinted=OD5, Canadian=OD6, Belgian=OD7, ADA2006=OD8, 1500=OD9</summary>
		public string UniqueID="";
		///<summary>Set to false to not print images.  This removes the background for printing on premade forms.</summary>
		public bool PrintImages;
		///<summary>Shifts all items by x/100th's of an inch to compensate for printer, typically less than 1/4 inch.</summary>
		[XmlIgnore]
		public int OffsetX;
		///<summary>Shifts all items by y/100th's of an inch to compensate for printer, typically less than 1/4 inch.</summary>
		[XmlIgnore]
		public int OffsetY;
		///<summary>This is not a database column.  It is an array of all claimformItems that are attached to this ClaimForm.</summary>
		public ClaimFormItem[] Items;

		///<summary>Returns a copy of the claimform including the Items.  Only used in FormClaimForms.butCopy_Click.</summary>
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
			cf.Items=(ClaimFormItem[])Items.Clone();
			//Items.CopyTo(cf.Items,0);
			return cf;
		}

		public int ClaimFormKey {
			get {
				return ClaimFormNum;
			}
		}
		public string ClaimFormDescription {
			get {
					return Description;
			}
		}

	}

	



}
