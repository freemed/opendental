package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

public class SheetFieldDef {
		/** Primary key. */
		public int SheetFieldDefNum;
		/** FK to sheetdef.SheetDefNum. */
		public int SheetDefNum;
		/** Enum:SheetFieldType  OutputText, InputField, StaticText,Parameter(only used for SheetField, not SheetFieldDef),Image,Drawing,Line,Rectangle,CheckBox,SigBox,PatImage. */
		public SheetFieldType FieldType;
		/** Mostly for OutputText, InputField, and CheckBox types.  Each sheet typically has a main datatable type.  For OutputText types, FieldName is usually the string representation of the database column for the main table.  For other tables, it can be of the form table.Column.  There may also be extra fields available that are not strictly pulled from the database.  Extra fields will start with lowercase to indicate that they are not pure database fields.  The list of available fields for each type in SheetFieldsAvailable.  Users can pick from that list.  Likewise, InputField types are internally tied to actions to persist the data.  So they are also hard coded and are available in SheetFieldsAvailable.  For static images, this is the full file name including extension, but without path.  Static images paths are reconstructed by looking in the AtoZ folder, SheetImages folder.  For Pat Images, this is an long FK/DefNum to the default folder for the image.  The filename of a PatImage will later be stored in FieldValue. */
		public String FieldName;
		/** For StaticText, this text can include bracketed fields, like [nameLF].  For OutputText and InputField, this will be blank.  For CheckBoxes, either X or blank.  Even if the checkbox is set to behave like a radio button. */
		public String FieldValue;
		/** The fontSize for this field regardless of the default for the sheet.  The actual font must be saved with each sheetField. */
		public float FontSize;
		/** The fontName for this field regardless of the default for the sheet.  The actual font must be saved with each sheetField. */
		public String FontName;
		/** . */
		public boolean FontIsBold;
		/** In pixels. */
		public int XPos;
		/** In pixels. */
		public int YPos;
		/** The field will be constrained horizontally to this size.  Not allowed to be zero. */
		public int Width;
		/** The field will be constrained vertically to this size.  Not allowed to be 0.  It's not allowed to be zero so that it will be visible on the designer. */
		public int Height;
		/** Enum:GrowthBehaviorEnum */
		public GrowthBehaviorEnum GrowthBehavior;
		/** This is only used for checkboxes that you want to behave like radiobuttons.  Set the FieldName the same for each Checkbox in the group.  The FieldValue will likely be X for one of them and empty string for the others.  Each of them will have a different RadioButtonValue.  Whichever box has X, the RadioButtonValue for that box will be used when importing.  This field is not used for "misc" radiobutton groups. */
		public String RadioButtonValue;
		/** Name which identifies the group within which the radio button belongs. FieldName must be set to "misc" in order for the group to take effect. */
		public String RadioButtonGroup;
		/** Set to true if this field is required to have a value before the sheet is closed. */
		public boolean IsRequired;
		/** Tab stop order for all fields. One-based.  Only checkboxes and input fields can have values other than 0. */
		public int TabOrder;
		/** Allows reporting on misc fields. */
		public String ReportableName;

		/** Deep copy of object. */
		public SheetFieldDef Copy() {
			SheetFieldDef sheetfielddef=new SheetFieldDef();
			sheetfielddef.SheetFieldDefNum=this.SheetFieldDefNum;
			sheetfielddef.SheetDefNum=this.SheetDefNum;
			sheetfielddef.FieldType=this.FieldType;
			sheetfielddef.FieldName=this.FieldName;
			sheetfielddef.FieldValue=this.FieldValue;
			sheetfielddef.FontSize=this.FontSize;
			sheetfielddef.FontName=this.FontName;
			sheetfielddef.FontIsBold=this.FontIsBold;
			sheetfielddef.XPos=this.XPos;
			sheetfielddef.YPos=this.YPos;
			sheetfielddef.Width=this.Width;
			sheetfielddef.Height=this.Height;
			sheetfielddef.GrowthBehavior=this.GrowthBehavior;
			sheetfielddef.RadioButtonValue=this.RadioButtonValue;
			sheetfielddef.RadioButtonGroup=this.RadioButtonGroup;
			sheetfielddef.IsRequired=this.IsRequired;
			sheetfielddef.TabOrder=this.TabOrder;
			sheetfielddef.ReportableName=this.ReportableName;
			return sheetfielddef;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<SheetFieldDef>");
			sb.append("<SheetFieldDefNum>").append(SheetFieldDefNum).append("</SheetFieldDefNum>");
			sb.append("<SheetDefNum>").append(SheetDefNum).append("</SheetDefNum>");
			sb.append("<FieldType>").append(FieldType.ordinal()).append("</FieldType>");
			sb.append("<FieldName>").append(Serializing.EscapeForXml(FieldName)).append("</FieldName>");
			sb.append("<FieldValue>").append(Serializing.EscapeForXml(FieldValue)).append("</FieldValue>");
			sb.append("<FontSize>").append(FontSize).append("</FontSize>");
			sb.append("<FontName>").append(Serializing.EscapeForXml(FontName)).append("</FontName>");
			sb.append("<FontIsBold>").append((FontIsBold)?1:0).append("</FontIsBold>");
			sb.append("<XPos>").append(XPos).append("</XPos>");
			sb.append("<YPos>").append(YPos).append("</YPos>");
			sb.append("<Width>").append(Width).append("</Width>");
			sb.append("<Height>").append(Height).append("</Height>");
			sb.append("<GrowthBehavior>").append(GrowthBehavior.ordinal()).append("</GrowthBehavior>");
			sb.append("<RadioButtonValue>").append(Serializing.EscapeForXml(RadioButtonValue)).append("</RadioButtonValue>");
			sb.append("<RadioButtonGroup>").append(Serializing.EscapeForXml(RadioButtonGroup)).append("</RadioButtonGroup>");
			sb.append("<IsRequired>").append((IsRequired)?1:0).append("</IsRequired>");
			sb.append("<TabOrder>").append(TabOrder).append("</TabOrder>");
			sb.append("<ReportableName>").append(Serializing.EscapeForXml(ReportableName)).append("</ReportableName>");
			sb.append("</SheetFieldDef>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"SheetFieldDefNum")!=null) {
					SheetFieldDefNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"SheetFieldDefNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"SheetDefNum")!=null) {
					SheetDefNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"SheetDefNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"FieldType")!=null) {
					FieldType=SheetFieldType.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"FieldType"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"FieldName")!=null) {
					FieldName=Serializing.GetXmlNodeValue(doc,"FieldName");
				}
				if(Serializing.GetXmlNodeValue(doc,"FieldValue")!=null) {
					FieldValue=Serializing.GetXmlNodeValue(doc,"FieldValue");
				}
				if(Serializing.GetXmlNodeValue(doc,"FontSize")!=null) {
					FontSize=Float.valueOf(Serializing.GetXmlNodeValue(doc,"FontSize"));
				}
				if(Serializing.GetXmlNodeValue(doc,"FontName")!=null) {
					FontName=Serializing.GetXmlNodeValue(doc,"FontName");
				}
				if(Serializing.GetXmlNodeValue(doc,"FontIsBold")!=null) {
					FontIsBold=(Serializing.GetXmlNodeValue(doc,"FontIsBold")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"XPos")!=null) {
					XPos=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"XPos"));
				}
				if(Serializing.GetXmlNodeValue(doc,"YPos")!=null) {
					YPos=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"YPos"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Width")!=null) {
					Width=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"Width"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Height")!=null) {
					Height=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"Height"));
				}
				if(Serializing.GetXmlNodeValue(doc,"GrowthBehavior")!=null) {
					GrowthBehavior=GrowthBehaviorEnum.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"GrowthBehavior"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"RadioButtonValue")!=null) {
					RadioButtonValue=Serializing.GetXmlNodeValue(doc,"RadioButtonValue");
				}
				if(Serializing.GetXmlNodeValue(doc,"RadioButtonGroup")!=null) {
					RadioButtonGroup=Serializing.GetXmlNodeValue(doc,"RadioButtonGroup");
				}
				if(Serializing.GetXmlNodeValue(doc,"IsRequired")!=null) {
					IsRequired=(Serializing.GetXmlNodeValue(doc,"IsRequired")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"TabOrder")!=null) {
					TabOrder=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"TabOrder"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ReportableName")!=null) {
					ReportableName=Serializing.GetXmlNodeValue(doc,"ReportableName");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}

		/**  */
		public enum SheetFieldType {
			/**  */
			OutputText,
			/**  */
			InputField,
			/**  */
			StaticText,
			/** 3-Stores a parameter other than the PatNum.  Not meant to be seen on the sheet.  Only used for SheetField, not SheetFieldDef. */
			Parameter,
			/**  */
			Image,
			/** 5-One sequence of dots that makes a line.  Continuous without any breaks.  Each time the pen is picked up, it creates a new field row in the database. */
			Drawing,
			/**  */
			Line,
			/**  */
			Rectangle,
			/** 8-A clickable area on the screen.  It's a form of input, so treated similarly to an InputField.  The X will go from corner to corner of the rectangle specified.  It can also behave like a radio button */
			CheckBox,
			/** 9-A signature box, either Topaz pad or directly on the screen with stylus/mouse.  The signature is encrypted based an a hash of all other field values in the entire sheet, excluding other SigBoxes.  The order is critical. */
			SigBox,
			/**  */
			PatImage,
			/**  */
			Special
		}

		/** For sheetFields */
		public enum GrowthBehaviorEnum {
			/**  */
			None,
			/**  */
			DownLocal,
			/**  */
			DownGlobal
		}


}
