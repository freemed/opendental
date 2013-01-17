package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class Def {
		/** Primary key. */
		public int DefNum;
		/** Enum:DefCat */
		public DefCat Category;
		/** Order that each item shows on various lists. 0-indexed. */
		public int ItemOrder;
		/** Each category is a little different.  This field is usually the common name of the item. */
		public String ItemName;
		/** This field can be used to store extra info about the item. */
		public String ItemValue;
		/** Some categories include a color option. */
		public int ItemColor;
		/** If hidden, the item will not show on any list, but can still be referenced. */
		public boolean IsHidden;

		/** Deep copy of object. */
		public Def deepCopy() {
			Def def=new Def();
			def.DefNum=this.DefNum;
			def.Category=this.Category;
			def.ItemOrder=this.ItemOrder;
			def.ItemName=this.ItemName;
			def.ItemValue=this.ItemValue;
			def.ItemColor=this.ItemColor;
			def.IsHidden=this.IsHidden;
			return def;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Def>");
			sb.append("<DefNum>").append(DefNum).append("</DefNum>");
			sb.append("<Category>").append(Category.ordinal()).append("</Category>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("<ItemName>").append(Serializing.escapeForXml(ItemName)).append("</ItemName>");
			sb.append("<ItemValue>").append(Serializing.escapeForXml(ItemValue)).append("</ItemValue>");
			sb.append("<ItemColor>").append(ItemColor).append("</ItemColor>");
			sb.append("<IsHidden>").append((IsHidden)?1:0).append("</IsHidden>");
			sb.append("</Def>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"DefNum")!=null) {
					DefNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"DefNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Category")!=null) {
					Category=DefCat.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"Category"))];
				}
				if(Serializing.getXmlNodeValue(doc,"ItemOrder")!=null) {
					ItemOrder=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ItemOrder"));
				}
				if(Serializing.getXmlNodeValue(doc,"ItemName")!=null) {
					ItemName=Serializing.getXmlNodeValue(doc,"ItemName");
				}
				if(Serializing.getXmlNodeValue(doc,"ItemValue")!=null) {
					ItemValue=Serializing.getXmlNodeValue(doc,"ItemValue");
				}
				if(Serializing.getXmlNodeValue(doc,"ItemColor")!=null) {
					ItemColor=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ItemColor"));
				}
				if(Serializing.getXmlNodeValue(doc,"IsHidden")!=null) {
					IsHidden=(Serializing.getXmlNodeValue(doc,"IsHidden")=="0")?false:true;
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing Def: "+e.getMessage());
			}
		}

		/** Definition Category. Go to the definition setup window in the program to see how each of these categories is used. */
		public enum DefCat {
			/** 0- Colors to display in Account module. */
			AccountColors,
			/** 1- Adjustment types. */
			AdjTypes,
			/** 2- Appointment confirmed types. */
			ApptConfirmed,
			/** 3- Procedure quick add list for appointments. */
			ApptProcsQuickAdd,
			/** 4- Billing types. */
			BillingTypes,
			/** 5- Not used. */
			ClaimFormats,
			/** 6- Not used. */
			DunningMessages,
			/** 7- Not used. */
			FeeSchedNamesOld,
			/** 8- Medical notes for quick paste. */
			MedicalNotes,
			/** 9- No longer used */
			OperatoriesOld,
			/** 10- Payment types. */
			PaymentTypes,
			/** 11- Procedure code categories. */
			ProcCodeCats,
			/** 12- Progress note colors. */
			ProgNoteColors,
			/** 13- Statuses for recall, unscheduled, and next appointments. */
			RecallUnschedStatus,
			/** 14- Service notes for quick paste. */
			ServiceNotes,
			/** 15- Discount types. */
			DiscountTypes,
			/** 16- Diagnosis types. */
			Diagnosis,
			/** 17- Colors to display in the Appointments module. */
			AppointmentColors,
			/** 18- Image categories. */
			ImageCats,
			/** 19- Quick add notes for the ApptPhoneNotes, which is getting phased out. */
			ApptPhoneNotes,
			/** 20- Treatment plan priority names. */
			TxPriorities,
			/** 21- Miscellaneous color options. */
			MiscColors,
			/** 22- Colors for the graphical tooth chart. */
			ChartGraphicColors,
			/** 23- Categories for the Contact list. */
			ContactCategories,
			/** 24- Categories for Letter Merge. */
			LetterMergeCats,
			/** 25- Types of Schedule Blockouts. */
			BlockoutTypes,
			/** 26- Categories of procedure buttons in Chart module */
			ProcButtonCats,
			/**  */
			CommLogTypes,
			/** 28- Categories of Supplies */
			SupplyCats,
			/** 29- Types of unearned income used in accrual accounting. */
			PaySplitUnearnedType,
			/** 30- Prognosis types. */
			Prognosis,
			/** 31- Custom Tracking, statuses such as 'review', 'hold', 'riskmanage', etc. */
			ClaimCustomTracking
		}


}
