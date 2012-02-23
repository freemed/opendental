using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Xml.Serialization;

namespace OpenDentBusiness {
	///<summary>The info in the definition table is used by other tables extensively.  Almost every table in the database links to definition.  Almost all links to this table will be to a DefNum.  Using the DefNum, you can find any of the other fields of interest, usually the ItemName.  Make sure to look at the Defs class to see how the definitions are used.  Loaded into memory ahead of time for speed.  Some types of info such as operatories started out life in this table, but then got moved to their own table when more complexity was needed.</summary>
	[Serializable]
	[CrudTable(TableName="definition")]
	public class Def:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long DefNum;
		///<summary>Enum:DefCat</summary>
		public DefCat Category;
		///<summary>Order that each item shows on various lists. 0-indexed.</summary>
		public int ItemOrder;
		///<summary>Each category is a little different.  This field is usually the common name of the item.</summary>
		public string ItemName;
		///<summary>This field can be used to store extra info about the item.</summary>
		public string ItemValue;
		///<summary>Some categories include a color option.</summary>
		[XmlIgnore]
		public Color ItemColor;
		///<summary>If hidden, the item will not show on any list, but can still be referenced.</summary>
		public bool IsHidden;

		///<summary>Used only for serialization purposes</summary>
		[XmlElement("ItemColor",typeof(int))]
		public int ItemColorXml {
			get {
				return ItemColor.ToArgb();
			}
			set {
				ItemColor = Color.FromArgb(value);
			}
		}

		///<summary>Returns a copy of the def.</summary>
		public Def Copy() {
			return (Def)MemberwiseClone();
		}

		/*
		public Def(){

		}

		public Def(long defNum,DefCat category,int itemOrder,string itemName,string itemValue,Color itemColor,bool isHidden){
			DefNum=defNum;
			Category=category;
			ItemOrder=itemOrder;
			ItemName=itemName;
			ItemValue=itemValue;
			ItemColor=itemColor;
			IsHidden=isHidden;
		}*/

	}

	///<summary>Definition Category. Go to the definition setup window in the program to see how each of these categories is used.</summary>
	public enum DefCat {
		///<summary>0- Colors to display in Account module.</summary>
		AccountColors,
		///<summary>1- Adjustment types.</summary>
		AdjTypes,
		///<summary>2- Appointment confirmed types.</summary>
		ApptConfirmed,
		///<summary>3- Procedure quick add list for appointments.</summary>
		ApptProcsQuickAdd,
		///<summary>4- Billing types.</summary>
		BillingTypes,
		///<summary>5- Not used.</summary>
		ClaimFormats,
		///<summary>6- Not used.</summary>
		DunningMessages,
		///<summary>7- Not used.</summary>
		FeeSchedNamesOld,
		///<summary>8- Medical notes for quick paste.</summary>
		MedicalNotes,
		///<summary>9- No longer used</summary>
		OperatoriesOld,
		///<summary>10- Payment types.</summary>
		PaymentTypes,
		///<summary>11- Procedure code categories.</summary>
		ProcCodeCats,
		///<summary>12- Progress note colors.</summary>
		ProgNoteColors,
		///<summary>13- Statuses for recall, unscheduled, and next appointments.</summary>
		RecallUnschedStatus,
		///<summary>14- Service notes for quick paste.</summary>
		ServiceNotes,
		///<summary>15- Discount types.</summary>
		DiscountTypes,
		///<summary>16- Diagnosis types.</summary>
		Diagnosis,
		///<summary>17- Colors to display in the Appointments module.</summary>
		AppointmentColors,
		///<summary>18- Image categories.</summary>
		ImageCats,
		///<summary>19- Quick add notes for the ApptPhoneNotes, which is getting phased out.</summary>
		ApptPhoneNotes,
		///<summary>20- Treatment plan priority names.</summary>
		TxPriorities,
		///<summary>21- Miscellaneous color options.</summary>
		MiscColors,
		///<summary>22- Colors for the graphical tooth chart.</summary>
		ChartGraphicColors,
		///<summary>23- Categories for the Contact list.</summary>
		ContactCategories,
		///<summary>24- Categories for Letter Merge.</summary>
		LetterMergeCats,
		///<summary>25- Types of Schedule Blockouts.</summary>
		BlockoutTypes,
		///<summary>26- Categories of procedure buttons in Chart module</summary>
		ProcButtonCats,
		///<Summary>27- Types of commlog entries.</Summary>
		CommLogTypes,
		///<summary>28- Categories of Supplies</summary>
		SupplyCats,
		///<summary>29- Types of unearned income used in accrual accounting.</summary>
		PaySplitUnearnedType,
		///<summary>30- Prognosis types.</summary>
		Prognosis
	}

	



}
