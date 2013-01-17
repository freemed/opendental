package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class SheetField {
		/** Primary key. */
		public int SheetFieldNum;
		/** FK to sheet.SheetNum. */
		public int SheetNum;
		/** Enum:SheetFieldType  OutputText, InputField, StaticText,Parameter(only used for SheetField, not SheetFieldDef),Image,Drawing,Line,Rectangle,CheckBox,SigBox,PatImage. */
		public SheetFieldType FieldType;
		/** Mostly for OutputText and InputField types.  Each sheet typically has a main datatable type.  For OutputText types, FieldName is usually the string representation of the database column for the main table.  For other tables, it can be of the form table.Column.  There may also be extra fields available that are not strictly pulled from the database.  Extra fields will start with lowercase to indicate that they are not pure database fields.  The list of available fields for each type in SheetFieldsAvailable.  Users can pick from that list.  Likewise, InputField types are internally tied to actions to persist the data.  So they are also hard coded and are available in SheetFieldsAvailable.  For static images, this is the full file name including extension, but without path.  Static images paths are reconstructed by looking in the AtoZ folder, SheetImages folder.  For a PatImage, this now contains the long FK to the document.  A join must be done to that table to find the filename.   When a SheetField has a fieldType of Parameter, then the FieldName stores the name of the parameter. */
		public String FieldName;
		/** For OutputText, this value is set before printing.  This is the data obtained from the database and ready to print.  For StaticText, this is copied from the sheetFieldDef, but in-line fields like [this] will have been filled.  For an archived sheet retrieved from the database (all SheetField rows), this value will have been saved and will not be filled again automatically.  For a parameter fieldtype, this will store the value of the parameter. For a Drawing fieldtype, this will be the point data for the lines.  The format would look similar to this: 45,68;48,70;49,72;0,0;55,88;etc.  It's simply a sequence of points, separated by semicolons.  For CheckBox, it will either be an X or empty.  For SigBox, the first char will be 0 or 1 to indicate SigIsTopaz, and all subsequent chars will be the Signature itself.   For Pat Image, this contains image size and image position info.  Like this: "X=0,Y=20,W=100,H=60".  This is initially generated automatically to fit the object.  It can later be changed by the user to "zoom and pan" within the confines of the object. */
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
		/** Tab stop order for all fields. Only checkboxes and input fields can have values other than 0. */
		public int TabOrder;
		/** Allows reporting on misc fields. */
		public String ReportableName;

		/** Deep copy of object. */
		public SheetField deepCopy() {
			SheetField sheetfield=new SheetField();
			sheetfield.SheetFieldNum=this.SheetFieldNum;
			sheetfield.SheetNum=this.SheetNum;
			sheetfield.FieldType=this.FieldType;
			sheetfield.FieldName=this.FieldName;
			sheetfield.FieldValue=this.FieldValue;
			sheetfield.FontSize=this.FontSize;
			sheetfield.FontName=this.FontName;
			sheetfield.FontIsBold=this.FontIsBold;
			sheetfield.XPos=this.XPos;
			sheetfield.YPos=this.YPos;
			sheetfield.Width=this.Width;
			sheetfield.Height=this.Height;
			sheetfield.GrowthBehavior=this.GrowthBehavior;
			sheetfield.RadioButtonValue=this.RadioButtonValue;
			sheetfield.RadioButtonGroup=this.RadioButtonGroup;
			sheetfield.IsRequired=this.IsRequired;
			sheetfield.TabOrder=this.TabOrder;
			sheetfield.ReportableName=this.ReportableName;
			return sheetfield;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<SheetField>");
			sb.append("<SheetFieldNum>").append(SheetFieldNum).append("</SheetFieldNum>");
			sb.append("<SheetNum>").append(SheetNum).append("</SheetNum>");
			sb.append("<FieldType>").append(FieldType.ordinal()).append("</FieldType>");
			sb.append("<FieldName>").append(Serializing.escapeForXml(FieldName)).append("</FieldName>");
			sb.append("<FieldValue>").append(Serializing.escapeForXml(FieldValue)).append("</FieldValue>");
			sb.append("<FontSize>").append(FontSize).append("</FontSize>");
			sb.append("<FontName>").append(Serializing.escapeForXml(FontName)).append("</FontName>");
			sb.append("<FontIsBold>").append((FontIsBold)?1:0).append("</FontIsBold>");
			sb.append("<XPos>").append(XPos).append("</XPos>");
			sb.append("<YPos>").append(YPos).append("</YPos>");
			sb.append("<Width>").append(Width).append("</Width>");
			sb.append("<Height>").append(Height).append("</Height>");
			sb.append("<GrowthBehavior>").append(GrowthBehavior.ordinal()).append("</GrowthBehavior>");
			sb.append("<RadioButtonValue>").append(Serializing.escapeForXml(RadioButtonValue)).append("</RadioButtonValue>");
			sb.append("<RadioButtonGroup>").append(Serializing.escapeForXml(RadioButtonGroup)).append("</RadioButtonGroup>");
			sb.append("<IsRequired>").append((IsRequired)?1:0).append("</IsRequired>");
			sb.append("<TabOrder>").append(TabOrder).append("</TabOrder>");
			sb.append("<ReportableName>").append(Serializing.escapeForXml(ReportableName)).append("</ReportableName>");
			sb.append("</SheetField>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"SheetFieldNum")!=null) {
					SheetFieldNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"SheetFieldNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"SheetNum")!=null) {
					SheetNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"SheetNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"FieldType")!=null) {
					FieldType=SheetFieldType.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"FieldType"))];
				}
				if(Serializing.getXmlNodeValue(doc,"FieldName")!=null) {
					FieldName=Serializing.getXmlNodeValue(doc,"FieldName");
				}
				if(Serializing.getXmlNodeValue(doc,"FieldValue")!=null) {
					FieldValue=Serializing.getXmlNodeValue(doc,"FieldValue");
				}
				if(Serializing.getXmlNodeValue(doc,"FontSize")!=null) {
					FontSize=Float.valueOf(Serializing.getXmlNodeValue(doc,"FontSize"));
				}
				if(Serializing.getXmlNodeValue(doc,"FontName")!=null) {
					FontName=Serializing.getXmlNodeValue(doc,"FontName");
				}
				if(Serializing.getXmlNodeValue(doc,"FontIsBold")!=null) {
					FontIsBold=(Serializing.getXmlNodeValue(doc,"FontIsBold")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"XPos")!=null) {
					XPos=Integer.valueOf(Serializing.getXmlNodeValue(doc,"XPos"));
				}
				if(Serializing.getXmlNodeValue(doc,"YPos")!=null) {
					YPos=Integer.valueOf(Serializing.getXmlNodeValue(doc,"YPos"));
				}
				if(Serializing.getXmlNodeValue(doc,"Width")!=null) {
					Width=Integer.valueOf(Serializing.getXmlNodeValue(doc,"Width"));
				}
				if(Serializing.getXmlNodeValue(doc,"Height")!=null) {
					Height=Integer.valueOf(Serializing.getXmlNodeValue(doc,"Height"));
				}
				if(Serializing.getXmlNodeValue(doc,"GrowthBehavior")!=null) {
					GrowthBehavior=GrowthBehaviorEnum.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"GrowthBehavior"))];
				}
				if(Serializing.getXmlNodeValue(doc,"RadioButtonValue")!=null) {
					RadioButtonValue=Serializing.getXmlNodeValue(doc,"RadioButtonValue");
				}
				if(Serializing.getXmlNodeValue(doc,"RadioButtonGroup")!=null) {
					RadioButtonGroup=Serializing.getXmlNodeValue(doc,"RadioButtonGroup");
				}
				if(Serializing.getXmlNodeValue(doc,"IsRequired")!=null) {
					IsRequired=(Serializing.getXmlNodeValue(doc,"IsRequired")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"TabOrder")!=null) {
					TabOrder=Integer.valueOf(Serializing.getXmlNodeValue(doc,"TabOrder"));
				}
				if(Serializing.getXmlNodeValue(doc,"ReportableName")!=null) {
					ReportableName=Serializing.getXmlNodeValue(doc,"ReportableName");
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing SheetField: "+e.getMessage());
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
