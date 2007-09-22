using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental.ReportingOld2
{


	/// <summary>This class is loosely modeled after CrystalReports.ReportDocument, but with less inheritence and heirarchy.</summary>
	public class ReportOld2{
		private ArrayList dataFields;
		private SectionCollection sections;
		private ReportObjectCollection reportObjects;
		private ParameterFieldCollection parameterFields;
		//private Margins reportMargins; //Never set anywhere, so it is not needed!
		private bool isLandscape;
		private string query;
		private DataTable reportTable;
		private string reportName;
		private string description;
		private string authorID;
		private int letterOrder;
		///<summary>This is simply used to measure strings for alignment purposes.</summary>
		private Graphics grfx;

		#region Properties
		///<summary>Collection of strings representing available datatable field names. For now, only one table is allowed, so each string will represent a column.</summary>
		public ArrayList DataFields{
			get{
				return dataFields;
			}
			set{
				dataFields=value;
			}
		}
		///<summary>Collection of Sections.</summary>
		public SectionCollection Sections{
			get{
				return sections;
			}
			set{
				sections=value;
			}
		}
		///<summary>A collection of ReportObjects</summary>
		public ReportObjectCollection ReportObjects{
			get{
				return reportObjects;
			}
			set{
				reportObjects=value;
			}
		}
		///<summary>Collection of ParameterFields that are available for the query.</summary>
		public ParameterFieldCollection ParameterFields{
			get{
				return parameterFields;
			}
			set{
				parameterFields=value;
			}
		}
		///<summary>margins will be null unless set by user. When printing, if margins are null, the defaults will depend on the page orientation.</summary>
		public Margins ReportMargins{
			get{
				//return reportMargins; //reportMargins is always null!
				return null;
			}
		}
		///<summary></summary>
		public bool IsLandscape{
			get{
				return isLandscape;
			}
			set{
				isLandscape=value;
			}
		}
		///<summary>The query will get altered before it is actually used to retrieve. Any parameters will be replaced with user entered data without saving those changes.</summary>
		public string Query{
			get{
				return query;
			}
			set{
				query=value;
			}
		}
		///<summary>The datatable that is returned from the database.</summary>
		public DataTable ReportTable{
			get{
				return reportTable;
			}
			set{
				reportTable=value;
			}
		}
		///<summary>The name to display in the menu.</summary>
		public string ReportName{
			get{
				return reportName;
			}
			set{
				reportName=value;
			}
		}
		///<summary>Gives the user a description and some guidelines about what they can expect from this report.</summary>
		public string Description{
			get{
				return description;
			}
			set{
				description=value;
			}
		}
		///<summary>For instance OD12 or JoeDeveloper9.  If you are a developer releasing reports, then this should be your name or company followed by a unique number.  This will later make it easier to maintain your reports for your customers.  All reports that we release will be of the form OD##.  Reports that the user creates will have this field blank.</summary>
		public string AuthorID{
			get{
				return authorID;
			}
			set{
				authorID=value;
			}
		}
		///<summary>The 1-based order to show in the Letter menu, or 0 to not show in that menu.</summary>
		public int LetterOrder{
			get{
				return letterOrder;
			}
			set{
				letterOrder=value;
			}
		}
		
		#endregion

		///<summary>When a new Report is created, the only section that is added is the details. This makes the logic a little more complicated, but it will minimize calls to the database for unused sections. This also makes the act of adding groups more natural.</summary>
		public ReportOld2(){
			//ReportMargins=new Margins(50,50,30,30);//this should work for almost all printers.
			sections=new SectionCollection();
			//sections.Add(new Section(AreaSectionKind.ReportHeader,"Report Header",0));
			//sections.Add(new Section(AreaSectionKind.PageHeader,"Page Header",0));
			//sections.Add("Group Header");
			sections.Add(new Section(AreaSectionKind.Detail,0));
			//sections.Add("Group Footer");
			//sections.Add(new Section(AreaSectionKind.PageFooter,"Page Footer",0));
			//sections.Add(new Section(AreaSectionKind.ReportFooter,"Report Footer",0));
			reportObjects=new ReportObjectCollection();
			dataFields=new ArrayList();
			parameterFields=new ParameterFieldCollection();
			grfx=Graphics.FromImage(new Bitmap(1,1));//I'm still trying to find a better way to do this
		}

		/// <summary>Adds a ReportObject large, centered, and bold, to the top of the Report Header Section.  Should only be done once, and done before any subTitles.</summary>
		/// <param name="title">The text of the title.</param>
		public void AddTitle(string title){
			//FormReport FormR=new FormReport();
			//this is just to get a graphics object. There must be a better way.
			//Graphics grfx=FormR.CreateGraphics();
			Font font=new Font("Tahoma",17,FontStyle.Bold);
			Size size=new Size((int)(grfx.MeasureString(title,font).Width/grfx.DpiX*100+2)
				,(int)(grfx.MeasureString(title,font).Height/grfx.DpiY*100+2));
			int xPos;
			if(isLandscape)
				xPos=1100/2;
			else
				xPos=850/2;
			//if(reportMargins==null){	//Crashes MONO, but reportMargins would always null since it is never set, 
																	//so this check is not needed.
				if(IsLandscape)
          xPos-=50;
				else
					xPos-=30;
			//}
			//else{
			//	xPos-=reportMargins.Left;//to make it look centered
			//}
			xPos-=(int)(size.Width/2);
			reportObjects.Add(
				new ReportObject("Report Header",new Point(xPos,0),size,title,font,ContentAlignment.MiddleCenter));
			if(sections["Report Header"]==null){
				sections.Add(new Section(AreaSectionKind.ReportHeader,0));
			}
			//this it the only place a white buffer is added to a header.
			sections["Report Header"].Height=(int)size.Height+20;
			//grfx.Dispose();
			//FormR.Dispose();
		}

		/// <summary>Adds a ReportObject, centered and bold, at the bottom of the Report Header Section.  Should only be done after AddTitle.  You can add as many subtitles as you want.</summary>
		/// <param name="subTitle">The text of the subtitle.</param>
		public void AddSubTitle(string subTitle){
			//FormReport FormR=new FormReport();
			//Graphics grfx=FormR.CreateGraphics();
			Font font=new Font("Tahoma",10,FontStyle.Bold);
			Size size=new Size((int)(grfx.MeasureString(subTitle,font).Width/grfx.DpiX*100+2)
				,(int)(grfx.MeasureString(subTitle,font).Height/grfx.DpiY*100+2));
			int xPos;
			if(isLandscape)
				xPos=1100/2;
			else
				xPos=850/2;
			//if(reportMargins==null){	//Crashes MONO, but reportMargins would always null since it is never set, 
																	//so this check is not needed.
				if(isLandscape)
          xPos-=50;
				else
					xPos-=30;
			//}
			//else{
			//	xPos-=reportMargins.Left;//to make it look centered
			//}
			xPos-=(int)(size.Width/2);
			if(sections["Report Header"]==null){
				sections.Add(new Section(AreaSectionKind.ReportHeader,0));	
			}
			//find the yPos+Height of the last reportObject in the Report Header section
			int yPos=0;
			foreach(ReportObject reportObject in reportObjects){
				if(reportObject.SectionName!="Report Header") continue;
				if(reportObject.Location.Y+reportObject.Size.Height > yPos){
					yPos=reportObject.Location.Y+reportObject.Size.Height;
				}
			}
			reportObjects.Add(
				new ReportObject("Report Header",new Point(xPos,yPos+5),size,subTitle,font,ContentAlignment.MiddleCenter));
			sections["Report Header"].Height+=(int)size.Height+5;
			//grfx.Dispose();
			//FormR.Dispose();
		}

		/// <summary>Adds all the objects necessary for a typical column, including the textObject for column header and the fieldObject for the data.  Does not add lines or shading. If the column is type Double, then the alignment is set right and a total field is added. Also, default formatstrings are set for dates and doubles.</summary>
		/// <param name="dataField">The name of the column title as well as the dataField to add.</param>
		/// <param name="width"></param>
		/// <param name="valueType"></param>
		public void AddColumn(string dataField,int width,FieldValueType valueType){
			dataFields.Add(dataField);
			//FormReport FormR=new FormReport();
			//Graphics grfx=FormR.CreateGraphics();
			Font font;
			Size size;
			ContentAlignment textAlign;
			if(valueType==FieldValueType.Number){
				textAlign=ContentAlignment.MiddleRight;
			}
			else{
				textAlign=ContentAlignment.MiddleLeft;
			}
			string formatString="";
			if(valueType==FieldValueType.Number){
				formatString="n";
			}
			if(valueType==FieldValueType.Date){
				formatString="d";
			}
			if(sections["Page Header"]==null){
				sections.Add(new Section(AreaSectionKind.PageHeader,0));	
			}
			//add textobject for column header
			font=new Font("Tahoma",8,FontStyle.Bold);
			size=new Size((int)(grfx.MeasureString(dataField,font).Width/grfx.DpiX*100+2)
				,(int)(grfx.MeasureString(dataField,font).Height/grfx.DpiY*100+2));
			if(sections["Page Header"].Height==0){
				sections["Page Header"].Height=size.Height;
			}
			int xPos=0;
			//find next available xPos
			foreach(ReportObject reportObject in reportObjects){
				if(reportObject.SectionName!="Page Header") continue;
				if(reportObject.Location.X+reportObject.Size.Width > xPos){
					xPos=reportObject.Location.X+reportObject.Size.Width;
				}
			}
			ReportObjects.Add(new ReportObject("Page Header"
				,new Point(xPos,0),new Size(width,size.Height),dataField,font,textAlign));
			//add fieldObject for rows in details section
			font=new Font("Tahoma",9);
			size=new Size((int)(grfx.MeasureString(dataField,font).Width/grfx.DpiX*100+2)
				,(int)(grfx.MeasureString(dataField,font).Height/grfx.DpiY*100+2));
			if(sections["Detail"].Height==0){
				sections["Detail"].Height=size.Height;
			}
			reportObjects.Add(new ReportObject("Detail"
				,new Point(xPos,0),new Size(width,size.Height)
				,dataField,valueType
				//,new FieldDef(dataField,valueType)
				,font,textAlign,formatString));
			//add fieldObject for total in ReportFooter
			if(valueType==FieldValueType.Number){
				font=new Font("Tahoma",9,FontStyle.Bold);
				//use same size as already set for otherFieldObjects above
				if(sections["Report Footer"]==null){
					sections.Add(new Section(AreaSectionKind.ReportFooter,0));	
				}
				if(sections["Report Footer"].Height==0){
					sections["Report Footer"].Height=size.Height;
				}
				reportObjects.Add(new ReportObject("Report Footer"
					,new Point(xPos,0),new Size(width,size.Height)
					,SummaryOperation.Sum,dataField
					//,new FieldDef("Sum"+dataField,SummaryOperation.Sum
					//,GetLastRO(ReportObjectKind.FieldObject).DataSource)
					,font,textAlign,formatString));
			}
			//tidy up
			//grfx.Dispose();
			//FormR.Dispose();
			return;
		}

		/// <summary>Gets the last reportObect of a particular kind. Used immediately after entering an Object to alter its properties.</summary>
		/// <param name="objectKind"></param>
		/// <returns></returns>
		public ReportObject GetLastRO(ReportObjectKind objectKind){
			//ReportObject ro=null;
			for(int i=reportObjects.Count-1;i>=0;i--){//search from the end backwards
				if(reportObjects[i].ObjectKind==objectKind){
					return ReportObjects[i];
				}
			}
			MessageBox.Show("end of loop");
			return null;
		}

		/// <summary>Put a pagenumber object on lower left of page footer section.</summary>
		public void AddPageNum(){
			//FormReport FormR=new FormReport();
			//Graphics grfx=FormR.CreateGraphics();
			//add page number
			Font font=new Font("Tahoma",9);
			Size size=new Size(150,(int)(grfx.MeasureString("anytext",font).Height/grfx.DpiY*100+2));
			if(sections["Page Footer"]==null){
				sections.Add(new Section(AreaSectionKind.PageFooter,0));	
			}
			//Section section=Sections.GetOfKind(AreaSectionKind.PageFooter);
			if(sections["Page Footer"].Height==0){
				sections["Page Footer"].Height=size.Height;
			}
			reportObjects.Add(new ReportObject("Page Footer"
				,new Point(0,0),size
				,FieldValueType.String,SpecialFieldType.PageNumber
				//,new FieldDef("PageNum",FieldValueType.String
				//,SpecialVarType.PageNumber)
				,font,ContentAlignment.MiddleLeft,""));
			//grfx.Dispose();
			//FormR.Dispose();
		}
		
		/*public void AddParameter(string name,ParameterValueKind valueKind){
			ParameterFields.Add(new ParameterFieldDefinition(name,valueKind));
		}*/

		///<summary>Adds a parameterField which will be used in the query to represent user-entered data.</summary>
		///<param name="myName">The unique formula name of the parameter.</param>
		///<param name="myValueType">The data type that this parameter stores.</param>
		///<param name="myDefaultValue">The default value of the parameter</param>
		///<param name="myPromptingText">The text to prompt the user with.</param>
		///<param name="mySnippet">The SQL snippet that this parameter represents.</param>
		public void AddParameter(string myName,FieldValueType myValueType
			,object myDefaultValue,string myPromptingText,string mySnippet){
			parameterFields.Add(new ParameterField(myName,myValueType,myDefaultValue,myPromptingText,mySnippet));
		}

		/// <summary>Overload for ValueKind enum.</summary>
		public void AddParameter(string myName,FieldValueType myValueType
			,ArrayList myDefaultValues,string myPromptingText,string mySnippet,EnumType myEnumerationType){
			parameterFields.Add(new ParameterField(myName,myValueType,myDefaultValues,myPromptingText,mySnippet,myEnumerationType));
		}

		/// <summary>Overload for ValueKind defCat.</summary>
		public void AddParameter(string myName,FieldValueType myValueType
			,ArrayList myDefaultValues,string myPromptingText,string mySnippet,DefCat myDefCategory){
			parameterFields.Add(new ParameterField(myName,myValueType,myDefaultValues,myPromptingText,mySnippet,myDefCategory));
		}

		/// <summary>Overload for ValueKind defCat.</summary>
		public void AddParameter(string myName,FieldValueType myValueType
			,ArrayList myDefaultValues,string myPromptingText,string mySnippet,ReportFKType myReportFKType){
			parameterFields.Add(new ParameterField(myName,myValueType,myDefaultValues,myPromptingText,mySnippet,myReportFKType));
		}

		///<summary>Submits the Query to the database and fills ReportTable with the results.  Returns false if the user clicks Cancel on the Parameters dialog.</summary>
		public bool SubmitQuery(){
			string outputQuery=Query;			
			if(parameterFields.Count>0){//djc only display parameter dialog if parameters were specified
				//display a dialog for user to enter parameters
				FormParameterInput FormPI=new FormParameterInput();
				for(int i=0;i<parameterFields.Count;i++){
					FormPI.AddInputItem(parameterFields[i].PromptingText,parameterFields[i].ValueType
						,parameterFields[i].DefaultValues,parameterFields[i].EnumerationType
						,parameterFields[i].DefCategory,parameterFields[i].FKeyType);
				}
				FormPI.ShowDialog();
				if(FormPI.DialogResult!=DialogResult.OK){
					return false;
				}
				for(int i=0;i<parameterFields.Count;i++){
					parameterFields[i].CurrentValues=FormPI.GetCurrentValues(i);
					parameterFields[i].ApplyParamValues();
				}
				//the outputQuery will get altered without affecting the original Query.
				string replacement="";//the replacement value to put into the outputQuery for each match
				//first replace all parameters with values:
				MatchCollection mc;
				Regex regex=new Regex(@"\?\w+");//? followed by one or more text characters
				mc=regex.Matches(outputQuery);
				//loop through each occurance of "?etc"
				for(int i=0;i<mc.Count;i++){
					replacement=parameterFields[mc[i].Value.Substring(1)].OutputValue;
					regex=new Regex(@"\"+mc[i].Value);
					outputQuery=regex.Replace(outputQuery,replacement);
				}
				//then, submit the query
			}
			//MessageBox.Show(outputQuery);
			reportTable=General.GetTable(outputQuery);
				//ODReportData.SubmitQuery(outputQuery);
			return true;
		}

		///<summary>If the specified section exists, then this returns its height. Otherwise it returns 0.</summary>
		public int GetSectionHeight(string sectionName){
			if(!sections.Contains(sectionName)){
				return 0;
			}
			return sections[sectionName].Height;
		}

		/*
		/// <summary>Add Simple. This is used when there is only a single page in the report and all elements are added to the report header.  Height is set to one row, and all width is set to full page width of 850. There are no other sections to the report.</summary>
		public void AddSimp(string text,int xPos,int yPos,Font font){
			FormReport FormR=new FormReport();
			Graphics grfx=FormR.CreateGraphics();
			Size size=grfx.MeasureString(text,font);
			Section section=Sections.GetOfKind(AreaSectionKind.ReportHeader);
			if(section.Height<1100)
				section.Height=1100;
			ReportObjects.Add(new TextObject(Sections.IndexOf(section),new Point(xPos,yPos)
				,new Size(850,size.Height+2),text,font,ContentAlignment.MiddleLeft));
			grfx.Dispose();
			FormR.Dispose();
		}

		public void AddSimp(string text,int xPos,int yPos){
			AddSimp(text,xPos,yPos,new Font("Tahoma",9));
		}*/

		

	}
}











