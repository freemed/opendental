using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Xml.Serialization;

namespace OpenDentBusiness {
	///<summary>The info in the definition table is used by other tables extensively.  Almost every table in the database links to definition.  Almost all links to this table will be to a DefNum.  Using the DefNum, you can find any of the other fields of interest, usually the ItemName.  Make sure to look at the Defs class to see how the definitions are used.  Loaded into memory ahead of time for speed.  Some types of info such as operatories started out life in this table, but then got moved to their own table when more complexity was needed.</summary>
	public class Def {
		///<summary>Primary key.</summary>
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
		}

	}

	

	



}
