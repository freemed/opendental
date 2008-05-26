using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental.ReportingOld2
{
	///<summary>There is one ReportObject for each element of an ODReport that gets printed on the page.  There are many different kinds of reportObjects</summary>
	public class ReportObject{
		private string sectionName;
		private Point location;
		private Size size;
		private string name;
		private ReportObjectKind objectKind;
		private Font font;
		private ContentAlignment textAlign;
		private Color foreColor;
		private string staticText;
		private string formatString;
		private bool suppressIfDuplicate;
		private string endSectionName;
		private Point locationLowerRight;
		private float lineThickness;
		private FieldDefKind fieldKind;
		private FieldValueType valueType;
		private SpecialFieldType specialType;
		private SummaryOperation operation;
		private string summarizedField;
		private string dataField;
		

#region Properties
		///<summary>The name of the section to which this object is attached.  For lines and boxes that span multiple sections, this is the section in which the upper part of the object resides.</summary>
		public string SectionName{
			get{
				return sectionName;
			}
			set{
				sectionName=value;
			}
		}
		///<summary>Location within the section. Frequently, y=0</summary>
		public Point Location{
			get{
				return location;
			}
			set{
				location=value;
			}
		}
		///<summary></summary>
		public Size Size{
			get{
				return size;
			}
			set{
				size=value;
			}
		}
		///<summary>The unique name of the ReportObject.</summary>
		public string Name{
			get{
				return name;
			}
			set{
				name=value;
			}
		}
		///<summary>For instance, FieldObject, or TextObject.</summary>
		public ReportObjectKind ObjectKind{
			get{
				return objectKind;
			}
			set{
				objectKind=value;
			}
		}
		///<summary></summary>
		public Font Font{
			get{
				return font;
			}
			set{
				font=value;
			}
		}
		///<summary>Horizontal alignment of the text.</summary>
		public ContentAlignment TextAlign{
			get{
				return textAlign;
			}
			set{
				textAlign=value;
			}
		}
		///<summary>Can be used for text color or for line color.</summary>
		public Color ForeColor{
			get{
				return foreColor;
			}
			set{
				foreColor=value;
			}
		}
		///<summary>The text to display for a TextObject.  Will later include XML formatting markup.</summary>
		public string StaticText{
			get{
				return staticText;
			}
			set{
				staticText=value;
			}
		}
		///<summary>For a FieldObject, a C# format string that specifies how to print dates, times, numbers, and currency based on the country or on a custom format.</summary>
		///<remarks>There are a LOT of options for this string.  Look in C# help under Standard Numeric Format Strings, Custom Numeric Format Strings, Standard DateTime Format Strings, Custom DateTime Format Strings, and Enumeration Format Strings.  Once users are allowed to edit reports, we will assemble a help page with all of the common options. The best options are "n" for number, and "d" for date.</remarks>
		public string FormatString{
			get{
				return formatString;
			}
			set{
				formatString=value;
			}
		}
		///<summary>Suppresses this field if the field for the previous record was the same.</summary>
		public bool SuppressIfDuplicate{
			get{
				return suppressIfDuplicate;
			}
			set{
				suppressIfDuplicate=value;
			}
		}
		///<summary>For graphics, the name of the Section to which the lower part of the object extends.  This will normally be the same as the sectionName unless the object spans multiple sections.  The object will then be drawn across all sections in between.</summary>
		public string EndSectionName{
			get{
				return endSectionName;
			}
			set{
				endSectionName=value;
			}
		}
		///<summary>The position of the lower right corner of the box or line in the coordinates of the endSection.</summary>
		public Point LocationLowerRight{
			get{
				return locationLowerRight;
			}
			set{
				locationLowerRight=value;
			}
		}
		///<summary></summary>
		public float LineThickness{
			get{
				return lineThickness;
			}
			set{
				lineThickness=value;
			}
		}
		///<summary>The kind of field, like FormulaField, SummaryField, or DataTableField.</summary>
		public FieldDefKind FieldKind{
			get{
				return fieldKind;
			}
			set{
				fieldKind=value;
			}
		}
		///<summary>The value type of field, like string or datetime.</summary>
		public FieldValueType ValueType{
			get{
				return valueType;
			}
			set{
				valueType=value;
			}
		}
		///<summary>For FieldKind=FieldDefKind.SpecialField, this is the type.  eg. pagenumber</summary>
		public SpecialFieldType SpecialType{
			get{
				return specialType;
			}
			set{
				specialType=value;
			}
		}
		///<summary>For FieldKind=FieldDefKind.SummaryField, the summary operation type.</summary>
		public SummaryOperation Operation{
			get{
				return operation;
			}
			set{
				operation=value;
			}
		}
		///<summary>For FieldKind=FieldDefKind.SummaryField, the name of the dataField that is being summarized.  This might later be changed to refer to a ReportObject name instead (or maybe not).</summary>
		public string SummarizedField{
			get{
				return summarizedField;
			}
			set{
				summarizedField=value;
			}
		}
		///<summary>For objectKind=ReportObjectKind.FieldObject, the name of the dataField column.</summary>
		public string DataField{
			get{
				return dataField;
			}
			set{
				dataField=value;
			}
		}
#endregion

		///<summary>Default constructor.</summary>
		public ReportObject(){

		}

		///<summary>Overload for TextObject.</summary>
		public ReportObject(string thisSectionName,Point thisLocation,Size thisSize,string thisStaticText,Font thisFont,ContentAlignment thisTextAlign){
			sectionName=thisSectionName;
			location=thisLocation;
			size=thisSize;
			staticText=thisStaticText;
			font=thisFont;
			textAlign=thisTextAlign;
			foreColor=Color.Black;
			objectKind=ReportObjectKind.TextObject;
		}

		///<summary>Overload for DataTableField ReportObject</summary>
		public ReportObject(string thisSectionName,Point thisLocation,Size thisSize
			,string thisDataField,FieldValueType thisValueType
			,Font thisFont,ContentAlignment thisTextAlign,string thisFormatString){
			sectionName=thisSectionName;
			location=thisLocation;
			size=thisSize;
			font=thisFont;
			textAlign=thisTextAlign;
			formatString=thisFormatString;
			fieldKind=FieldDefKind.DataTableField;
			dataField=thisDataField;
			valueType=thisValueType;
			//defaults:
			foreColor=Color.Black;
			objectKind=ReportObjectKind.FieldObject;
		}

		///<summary>Overload for SummaryField ReportObject</summary>
		public ReportObject(string thisSectionName,Point thisLocation,Size thisSize
			,SummaryOperation thisOperation,string thisSummarizedField
			,Font thisFont,ContentAlignment thisTextAlign,string thisFormatString){
			sectionName=thisSectionName;
			location=thisLocation;
			size=thisSize;
			font=thisFont;
			textAlign=thisTextAlign;
			formatString=thisFormatString;
			fieldKind=FieldDefKind.SummaryField;
			valueType=FieldValueType.Number;
			operation=thisOperation;
			summarizedField=thisSummarizedField;
			//defaults:
			foreColor=Color.Black;
			objectKind=ReportObjectKind.FieldObject;
		}

		///<summary>Overload for SpecialField ReportObject</summary>
		public ReportObject(string thisSectionName,Point thisLocation,Size thisSize
			,FieldValueType thisValueType,SpecialFieldType thisSpecialType
			,Font thisFont,ContentAlignment thisTextAlign,string thisFormatString){
			sectionName=thisSectionName;
			location=thisLocation;
			size=thisSize;
			font=thisFont;
			textAlign=thisTextAlign;
			formatString=thisFormatString;
			fieldKind=FieldDefKind.SpecialField;
			valueType=thisValueType;
			specialType=thisSpecialType;
			//defaults:
			foreColor=Color.Black;
			objectKind=ReportObjectKind.FieldObject;
		}



		///<summary>Converts contentAlignment into a combination of StringAlignments.  More arguments will later be added for other formatting options.  This method is called by FormReport when drawing text for a reportObject.</summary>
		///<param name="contentAlignment"></param>
		///<returns></returns>
		public static StringFormat GetStringFormat(ContentAlignment contentAlignment){
			if(!Enum.IsDefined(typeof(ContentAlignment),(int)contentAlignment))
				throw new System.ComponentModel.InvalidEnumArgumentException(
					"contentAlignment",(int)contentAlignment,typeof(ContentAlignment));
			StringFormat stringFormat = new StringFormat();
			switch (contentAlignment){
				case ContentAlignment.MiddleCenter:
					stringFormat.LineAlignment = StringAlignment.Center;
					stringFormat.Alignment = StringAlignment.Center;
					break;
				case ContentAlignment.MiddleLeft:
					stringFormat.LineAlignment = StringAlignment.Center;
					stringFormat.Alignment = StringAlignment.Near;
					break;
				case ContentAlignment.MiddleRight:
					stringFormat.LineAlignment = StringAlignment.Center;
					stringFormat.Alignment = StringAlignment.Far;
					break;
				case ContentAlignment.TopCenter:
					stringFormat.LineAlignment = StringAlignment.Near;
					stringFormat.Alignment = StringAlignment.Center;
					break;
				case ContentAlignment.TopLeft:
					stringFormat.LineAlignment = StringAlignment.Near;
					stringFormat.Alignment = StringAlignment.Near;
					break;
				case ContentAlignment.TopRight:
					stringFormat.LineAlignment = StringAlignment.Near;
					stringFormat.Alignment = StringAlignment.Far;
					break;
				case ContentAlignment.BottomCenter:
					stringFormat.LineAlignment = StringAlignment.Far;
					stringFormat.Alignment = StringAlignment.Center;
					break;
				case ContentAlignment.BottomLeft:
					stringFormat.LineAlignment = StringAlignment.Far;
					stringFormat.Alignment = StringAlignment.Near;
					break;
				case ContentAlignment.BottomRight:
					stringFormat.LineAlignment = StringAlignment.Far;
					stringFormat.Alignment = StringAlignment.Far;
					break;
			}
			return stringFormat;
		}

		///<summary>Once a dataTable has been set, this method can be run to get the summary value of this field.  It will still need to be formatted. It loops through all records to get this value.  This will be changed soon to refer to the ReportObject rather than the dataTable field when summarizing.</summary>
		public double GetSummaryValue(DataTable dataTable,int col){
			//if(SummarizedField.FieldKind!=FieldDefKind.DataTableField){
			//	return 0;
			//}
			double retVal=0;
			for(int i=0;i<dataTable.Rows.Count;i++){
				if(Operation==SummaryOperation.Sum){
					retVal+=PIn.PDouble(dataTable.Rows[i][col].ToString());
						//PIn.PDouble(Report.ReportTable.Rows[i][Report.DataFields.IndexOf(fieldObject.DataSource.Name)].ToString())
				}
				else if(Operation==SummaryOperation.Count){
					retVal++;
				}
			}
			return retVal;
		}


	}

	///<summary>Specifies the field kind in the FieldKind property of the ReportObject class.</summary>
	public enum FieldDefKind{
		///<summary></summary>
		DataTableField,
		///<summary></summary>
		FormulaField,
		///<summary></summary>
		SpecialField,
		///<summary></summary>
		SummaryField
		//RunningTotalField
		//GroupNameField
	}

	///<summary>Used in the Kind field of each ReportObject to provide a quick way to tell what kind of reportObject.</summary>
	public enum ReportObjectKind{
		//BlobFieldObject Object is a blob field. 
		///<summary>Object is a box.</summary>
		BoxObject,
		//ChartObject Object is a chart. 
		//CrossTabObject Object is a cross tab. 
		///<summary>Object is a field object.</summary>
		FieldObject,
		///<summary>Object is a line. </summary>
		LineObject,
		//PictureObject Object is a picture. 
		//SubreportObject Object is a subreport.
		///<summary>Object is a text object. </summary>
		TextObject
	}

	///<summary>Specifies the special field type in the SpecialType property of the ReportObject class.</summary>
	public enum SpecialFieldType{
		///<summary>Field returns "Page [current page number] of [total page count]" formula. Not functional yet.</summary>
		PageNofM,
		///<summary>Field returns the current page number.</summary>
		PageNumber,
		///<summary>Field returns the current date.</summary>
		PrintDate
	}

	///<summary></summary>
	public enum SummaryOperation{
		//Average Summary returns the average of a field.
		///<summary>Summary counts the number of values, from the field.</summary>
		Count,
		//DistinctCount Summary returns the number of none repeating values, from the field. 
		//Maximum Summary returns the largest value from the field. 
		//Median Summary returns the middle value in a sequence of numeric values. 
		//Minimum Summary returns the smallest value from the field. 
		//Percentage Summary returns as a percentage of the grand total summary. 
		///<summary>Summary returns the total of all the values for the field.</summary>
		Sum
	}

	



}
