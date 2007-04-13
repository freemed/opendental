using Microsoft.CSharp;
//using Microsoft.Vsa;
using System.CodeDom.Compiler;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental.ReportingOld2
{
	///<summary></summary>
	public class FormReportOld2 : System.Windows.Forms.Form{
		private System.Windows.Forms.Panel panel1;
		private System.ComponentModel.IContainer components;
		private OpenDental.UI.Button butClose;
		private OpenDental.UI.Button butPrint;
		private System.Drawing.Printing.PrintDocument pd2;
		private System.Windows.Forms.PrintDialog printDialog2;
		///<summary>The report to display.</summary>
		private ReportOld2 MyReport;
		private OpenDental.UI.Button butSetup;
		private System.Windows.Forms.PageSetupDialog setupDialog2;
		///<summary>The y position printed through so far in the current section.</summary>
		//private int printedThroughYPos; For now, assume all sections must remain together.
		private OpenDental.UI.Button button1;
		private System.Windows.Forms.Label labelTotPages;
		private OpenDental.UI.Button butBack;
		private OpenDental.UI.Button butFwd;
		///<summary>The name of the last section printed. It really only keeps track of whether the details section and the reportfooter have finished printing. This variable will be refined when groups are implemented.</summary>
		private string lastSectionPrinted;
		private int rowsPrinted;
		private int totalPages;
		private OpenDental.UI.ODToolBar ToolBarMain;
		private System.Windows.Forms.ImageList imageListMain;
		private System.Windows.Forms.PrintPreviewControl printPreviewControl2;
		private int pagesPrinted;

		///<summary></summary>
		public FormReportOld2(ReportOld2 myReport){
			InitializeComponent();// Required for Windows Form Designer support
			MyReport=myReport;
		}

		/// <summary>Clean up any resources being used.</summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReportOld2));
			this.butClose = new OpenDental.UI.Button();
			this.butPrint = new OpenDental.UI.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.button1 = new OpenDental.UI.Button();
			this.labelTotPages = new System.Windows.Forms.Label();
			this.butBack = new OpenDental.UI.Button();
			this.butFwd = new OpenDental.UI.Button();
			this.butSetup = new OpenDental.UI.Button();
			this.pd2 = new System.Drawing.Printing.PrintDocument();
			this.printDialog2 = new System.Windows.Forms.PrintDialog();
			this.setupDialog2 = new System.Windows.Forms.PageSetupDialog();
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.imageListMain = new System.Windows.Forms.ImageList(this.components);
			this.printPreviewControl2 = new System.Windows.Forms.PrintPreviewControl();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(239,2);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,23);
			this.butClose.TabIndex = 1;
			this.butClose.Text = "&Close";
			// 
			// butPrint
			// 
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPrint.Autosize = true;
			this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.CornerRadius = 4F;
			this.butPrint.Location = new System.Drawing.Point(1,2);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(75,23);
			this.butPrint.TabIndex = 2;
			this.butPrint.Text = "&Print";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.button1);
			this.panel1.Controls.Add(this.labelTotPages);
			this.panel1.Controls.Add(this.butBack);
			this.panel1.Controls.Add(this.butFwd);
			this.panel1.Controls.Add(this.butSetup);
			this.panel1.Controls.Add(this.butPrint);
			this.panel1.Controls.Add(this.butClose);
			this.panel1.Location = new System.Drawing.Point(-1,178);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(831,35);
			this.panel1.TabIndex = 4;
			this.panel1.Visible = false;
			// 
			// button1
			// 
			this.button1.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.button1.Autosize = true;
			this.button1.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.button1.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.button1.CornerRadius = 4F;
			this.button1.Location = new System.Drawing.Point(501,8);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75,23);
			this.button1.TabIndex = 4;
			this.button1.Text = "Test";
			this.button1.Visible = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// labelTotPages
			// 
			this.labelTotPages.Font = new System.Drawing.Font("Microsoft Sans Serif",9F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelTotPages.Location = new System.Drawing.Point(137,4);
			this.labelTotPages.Name = "labelTotPages";
			this.labelTotPages.Size = new System.Drawing.Size(54,18);
			this.labelTotPages.TabIndex = 19;
			this.labelTotPages.Text = "1 / 2";
			this.labelTotPages.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// butBack
			// 
			this.butBack.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butBack.Autosize = true;
			this.butBack.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBack.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBack.CornerRadius = 4F;
			this.butBack.Image = global::OpenDental.Properties.Resources.Left;
			this.butBack.Location = new System.Drawing.Point(115,1);
			this.butBack.Name = "butBack";
			this.butBack.Size = new System.Drawing.Size(18,23);
			this.butBack.TabIndex = 20;
			// 
			// butFwd
			// 
			this.butFwd.AdjustImageLocation = new System.Drawing.Point(1,0);
			this.butFwd.Autosize = true;
			this.butFwd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butFwd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butFwd.CornerRadius = 4F;
			this.butFwd.Image = global::OpenDental.Properties.Resources.Right;
			this.butFwd.Location = new System.Drawing.Point(193,1);
			this.butFwd.Name = "butFwd";
			this.butFwd.Size = new System.Drawing.Size(18,23);
			this.butFwd.TabIndex = 21;
			// 
			// butSetup
			// 
			this.butSetup.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSetup.Autosize = true;
			this.butSetup.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSetup.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSetup.CornerRadius = 4F;
			this.butSetup.Location = new System.Drawing.Point(590,2);
			this.butSetup.Name = "butSetup";
			this.butSetup.Size = new System.Drawing.Size(75,23);
			this.butSetup.TabIndex = 3;
			this.butSetup.Text = "&Setup";
			this.butSetup.Visible = false;
			this.butSetup.Click += new System.EventHandler(this.butSetup_Click);
			// 
			// ToolBarMain
			// 
			this.ToolBarMain.Dock = System.Windows.Forms.DockStyle.Top;
			this.ToolBarMain.ImageList = this.imageListMain;
			this.ToolBarMain.Location = new System.Drawing.Point(0,0);
			this.ToolBarMain.Name = "ToolBarMain";
			this.ToolBarMain.Size = new System.Drawing.Size(831,29);
			this.ToolBarMain.TabIndex = 5;
			this.ToolBarMain.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.ToolBarMain_ButtonClick);
			// 
			// imageListMain
			// 
			this.imageListMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMain.ImageStream")));
			this.imageListMain.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListMain.Images.SetKeyName(0,"");
			this.imageListMain.Images.SetKeyName(1,"");
			this.imageListMain.Images.SetKeyName(2,"");
			this.imageListMain.Images.SetKeyName(3,"");
			// 
			// printPreviewControl2
			// 
			this.printPreviewControl2.AutoZoom = false;
			this.printPreviewControl2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.printPreviewControl2.Location = new System.Drawing.Point(0,0);
			this.printPreviewControl2.Name = "printPreviewControl2";
			this.printPreviewControl2.Size = new System.Drawing.Size(831,570);
			this.printPreviewControl2.TabIndex = 6;
			// 
			// FormReportOld2
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(831,570);
			this.Controls.Add(this.ToolBarMain);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.printPreviewControl2);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormReportOld2";
			this.ShowInTaskbar = false;
			this.Text = "Report";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.FormReport_Layout);
			this.Load += new System.EventHandler(this.FormReport_Load);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormReport_Load(object sender, System.EventArgs e) {
			LayoutToolBar();
			ResetPd2();
			labelTotPages.Text="/ "+totalPages.ToString();
			if(MyReport.IsLandscape){
				printPreviewControl2.Zoom=((double)printPreviewControl2.ClientSize.Height
					/(double)pd2.DefaultPageSettings.PaperSize.Width);
			}
			else{
				printPreviewControl2.Zoom=((double)printPreviewControl2.ClientSize.Height
					/(double)pd2.DefaultPageSettings.PaperSize.Height);
			}
			printPreviewControl2.Document=pd2;
		}

		///<summary>Causes the toolbar to be laid out again.</summary>
		public void LayoutToolBar(){
			ToolBarMain.Buttons.Clear();
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Print"),0,"","Print"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton("",1,"Go Back One Page","Back"));
			ODToolBarButton button=new ODToolBarButton("",-1,"","PageNum");
			button.Style=ODToolBarButtonStyle.Label;
			ToolBarMain.Buttons.Add(button);
			ToolBarMain.Buttons.Add(new ODToolBarButton("",2,"Go Forward One Page","Fwd"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Export"),3,"","Export"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Close"),-1,"Close This Window","Close"));
			//ToolBarMain.Invalidate();
		}

		private void FormReport_Layout(object sender, System.Windows.Forms.LayoutEventArgs e) {
			printPreviewControl2.Location=new System.Drawing.Point(0,panel1.Height);
			printPreviewControl2.Height=ClientSize.Height-panel1.Height;
			printPreviewControl2.Width=ClientSize.Width;	
		}
		
		private void ResetPd2(){
			pd2=new PrintDocument();
			pd2.PrintPage += new PrintPageEventHandler(this.pd2_PrintPage);
			lastSectionPrinted="";
			rowsPrinted=0;
			pagesPrinted=0;
			if(MyReport.IsLandscape){
				pd2.DefaultPageSettings.Landscape=true;
			}
			pd2.DefaultPageSettings.Margins=new Margins(0,0,0,0);
			pd2.OriginAtMargins=true;//the actual margins are taken into consideration in the printpage event, and if the user specifies 0,0 for margins, then the report will reliably print on a preprinted form. Origin is ALWAYS the corner of the paper.
			if(pd2.DefaultPageSettings.PaperSize.Height==0) {
				pd2.DefaultPageSettings.PaperSize=new PaperSize("default",850,1100);
			}
		}

		private void ToolBarMain_ButtonClick(object sender, OpenDental.UI.ODToolBarButtonClickEventArgs e) {
			//MessageBox.Show(e.Button.Tag.ToString());
			switch(e.Button.Tag.ToString()){
					
				case "Print":
					OnPrint_Click();
					break;
				case "Back":
					OnBack_Click();
					break;
				case "Fwd":
					OnFwd_Click();
					break;
				case "Export":
					OnExport_Click();
					break;
				case "Close":
					OnClose_Click();
					break;
				}
		}

		///<summary></summary>
		private void PrintReport(){
			try{
				if(Printers.SetPrinter(pd2,PrintSituation.Default)){
					pd2.Print();
				}
			}
			catch{
				MessageBox.Show(Lan.g(this,"Printer not available"));
			}
		}

		///<summary>raised for each page to be printed.</summary>
		private void pd2_PrintPage(object sender, PrintPageEventArgs ev){
			//Note that the locations of the reportObjects are not absolute.  They depend entirely upon the margins.  When the report is initially created, it is pushed up against the upper and the left.
			Graphics grfx=ev.Graphics;
			//xPos and yPos represent the upper left of current section after margins are accounted for.
			//All reportObjects are then placed relative to this origin.
			Margins currentMargins=null;
			Size paperSize;
			if(MyReport.IsLandscape)
				paperSize=new Size(1100,850);
			else
				paperSize=new Size(850,1100);
			//if(MyReport.ReportMargins==null){ //Crashes MONO. That's ok, because MyReport.ReportMargins is
																					//always null anyway.
				if(MyReport.IsLandscape)
					currentMargins=new Margins(50,0,30,30);
				else
					currentMargins=new Margins(30,0,50,50);
			//}
			//else{
			//	currentMargins=MyReport.ReportMargins;
			//}
			int xPos=currentMargins.Left;
			int yPos=currentMargins.Top;
			int printableHeight=paperSize.Height-currentMargins.Top-currentMargins.Bottom;
			int yLimit=paperSize.Height-currentMargins.Bottom;//the largest yPos allowed
			//Now calculate and layout each section in sequence.
			Section section;
			//for(int sectionIndex=0;sectionIndex<Report.Sections.Count;sectionIndex++){
			while(true){//will break out if no more room on page
				//if no sections have been printed yet, print a report header.
				if(lastSectionPrinted==""){
					if(MyReport.Sections.Contains("Report Header")){
						section=MyReport.Sections["Report Header"];
						PrintSection(grfx,section,xPos,yPos);
						yPos+=section.Height;
						if(section.Height>printableHeight){//this can happen if the reportHeader takes up the full page
							//if there are no other sections to print
							if(MyReport.ReportTable==null){
								//this will keep the second page from printing:
								lastSectionPrinted="Report Footer";
							}
							break;
						}
					}
					else{//no report header
						//it will still be marked as printed on the next line
					}
					lastSectionPrinted="Report Header";
				}
				//If the size of pageheader+one detail+pagefooter is taller than page, then we might later display an error. But for now, they will all still be laid out, and whatever goes off the bottom edge will just not show.  This will not be an issue for normal reports:
				if(MyReport.GetSectionHeight("Page Header")
					+MyReport.GetSectionHeight("Detail")
					+MyReport.GetSectionHeight("Page Footer")
					>printableHeight){
					//nothing for now.
				}
				//If this is first page and not enough room to print reportheader+pageheader+detail+pagefooter.
				if(pagesPrinted==0
					&& MyReport.GetSectionHeight("Report Header")
					+MyReport.GetSectionHeight("Page Header")
					+MyReport.GetSectionHeight("Detail")
					+MyReport.GetSectionHeight("Page Footer")
					>printableHeight)
				{
					break;
				}
				//always print a page header if it exists
				if(MyReport.Sections.Contains("Page Header")){
					section=MyReport.Sections["Page Header"];
					PrintSection(grfx,section,xPos,yPos);
					yPos+=section.Height;
				}
				//calculate if there is room for all elements including the reportfooter on this page.
				int rowsRemaining=0;
				if(MyReport.ReportTable!=null){
					rowsRemaining=MyReport.ReportTable.Rows.Count-rowsPrinted;
				}
				int totalDetailsHeight=rowsRemaining*MyReport.GetSectionHeight("Detail");
				bool isRoomForReportFooter=true;
				if(yLimit-yPos
					-MyReport.GetSectionHeight("Report Footer")
					-MyReport.GetSectionHeight("Page Footer")
					-totalDetailsHeight < 0){
					isRoomForReportFooter=false;
				}
				//calculate how many rows of detail to print
				int rowsToPrint=rowsRemaining;
				section=MyReport.Sections["Detail"];
				if(!isRoomForReportFooter){
					int actualDetailsHeight=yLimit-yPos
						-MyReport.GetSectionHeight("Report Footer")
						-MyReport.GetSectionHeight("Page Footer");
					rowsToPrint=(int)(actualDetailsHeight
						/MyReport.GetSectionHeight("Detail"));
					if(rowsToPrint<1)
						rowsToPrint=1;//Always print at least one row.
				}
				//print the detail section
				PrintDetailsSection(grfx,section,xPos,yPos,rowsToPrint);
				if(rowsToPrint==rowsRemaining)//if all remaining rows were printed
					lastSectionPrinted="Detail";//mark this section as printed.
				yPos+=section.Height*rowsToPrint;
				//print the reportfooter section if there is room
				if(isRoomForReportFooter){
					if(MyReport.Sections.Contains("Report Footer")){
						section=MyReport.Sections["Report Footer"];
						PrintSection(grfx,section,xPos,yPos);
						yPos+=section.Height;
					}
					//mark the reportfooter as printed. This will prevent another loop.
					lastSectionPrinted="Report Footer";
				}
				//print the pagefooter
				if(MyReport.Sections.Contains("Page Footer")){
					section=MyReport.Sections["Page Footer"];
					if(isRoomForReportFooter){
						//for the last page, this moves the pagefooter to the bottom of the page.
						yPos=yLimit-section.Height;
					}
					PrintSection(grfx,section,xPos,yPos);
					yPos+=section.Height;
				}
				break;
			}//while			
			pagesPrinted++;
			//if the reportfooter has been printed, then there are no more pages.
			if(lastSectionPrinted=="Report Footer"){
				ev.HasMorePages=false;
				totalPages=pagesPrinted;
				ToolBarMain.Buttons["PageNum"].Text="1 / "+totalPages.ToString();
				ToolBarMain.Invalidate();
				//labelTotPages.Text="1 / "+totalPages.ToString();
			}
			else{
				ev.HasMorePages=true;
			}
		}

		///<summary>Prints one section other than details at the specified x and y position on the page.  The math to decide whether it will fit on the current page is done ahead of time. There is no mechanism for splitting a section across multiple pages.</summary>
		private void PrintSection(Graphics g,Section section,int xPos,int yPos){
			ReportObject textObject;
			ReportObject fieldObject;
			//LineObject lineObject;
			//BoxObject boxObject;
			StringFormat strFormat;//used each time text is drawn to handle alignment issues
			//string rawText="";//the raw text for a given field as taken from the database
			string displayText="";//The formatted text to print
			foreach(ReportObject reportObject in MyReport.ReportObjects){
				//todo later: check for lines and boxes that span multiple sections.
				if(reportObject.SectionName!=section.Name){
					continue;
				}
				if(reportObject.ObjectKind==ReportObjectKind.TextObject){
					textObject=reportObject;
					strFormat=ReportObject.GetStringFormat(textObject.TextAlign);
					RectangleF layoutRect=new RectangleF(xPos+textObject.Location.X
						,yPos+textObject.Location.Y
						,textObject.Size.Width,textObject.Size.Height);
					g.DrawString(textObject.StaticText,textObject.Font,Brushes.Black,layoutRect,strFormat);
				}
				else if(reportObject.ObjectKind==ReportObjectKind.FieldObject){
					fieldObject=reportObject;
					strFormat=ReportObject.GetStringFormat(fieldObject.TextAlign);
					RectangleF layoutRect=new RectangleF(xPos+fieldObject.Location.X
						,yPos+fieldObject.Location.Y
						,fieldObject.Size.Width,fieldObject.Size.Height);
					displayText="";
					if(fieldObject.FieldKind==FieldDefKind.SummaryField){
						displayText=fieldObject.GetSummaryValue
							(MyReport.ReportTable,MyReport.DataFields.IndexOf
							(fieldObject.SummarizedField))
							.ToString(fieldObject.FormatString);
					}
					else if(fieldObject.FieldKind==FieldDefKind.SpecialField){
						if(fieldObject.SpecialType==SpecialFieldType.PageNofM){//not functional yet
							//displayText=Lan.g(this,"Page")+" "+(pagesPrinted+1).ToString()
							//	+Lan.g(
						}
						else if(fieldObject.SpecialType==SpecialFieldType.PageNumber){
							displayText=Lan.g(this,"Page")+" "+(pagesPrinted+1).ToString();
						}
					}
					g.DrawString(displayText,fieldObject.Font,Brushes.Black,layoutRect,strFormat);
				}
				//incomplete: else if lines
				//incomplete: else if boxes.
			}//foreach reportObject
			//sectionsPrinted=sectionIndex+1;//mark current section as printed.
			//MessageBox.Show(pagesPrinted.ToString()+","+sectionsPrinted.ToString());
			//yPos+=section.Height;//set current yPos to the bottom of the section just printed.
		}

		///<summary>Prints some rows of the details section at the specified x and y position on the page.  The math to decide how many rows to print is done ahead of time.  The number of rows printed so far is kept global so that it can be used in calculating the layout of this section.</summary>
		private void PrintDetailsSection(Graphics g,Section section,int xPos,int yPos,int rowsToPrint){
			ReportObject textObject;
			ReportObject fieldObject;
			//LineObject lineObject;
			//BoxObject boxObject;
			StringFormat strFormat;//used each time text is drawn to handle alignment issues
			string rawText="";//the raw text for a given field as taken from the database
			string displayText="";//The formatted text to print
			string prevDisplayText="";//The formatted text of the previous row. Used to test suppress dupl.
			//loop through each row in the table
			for(int i=rowsPrinted;i<rowsPrinted+rowsToPrint;i++){
				foreach(ReportObject reportObject in MyReport.ReportObjects){
					//todo later: check for lines and boxes that span multiple sections.
					if(reportObject.SectionName!=section.Name){
						//skip any reportObjects that are not in this section
						continue;
					}
					if(reportObject.ObjectKind==ReportObjectKind.TextObject){
						//not typical to print textobject in details section, but allowed
						textObject=reportObject;
						strFormat=ReportObject.GetStringFormat(textObject.TextAlign);
						RectangleF layoutRect=new RectangleF(xPos+textObject.Location.X
							,yPos+textObject.Location.Y
							,textObject.Size.Width,textObject.Size.Height);
						g.DrawString(textObject.StaticText,textObject.Font
							,new SolidBrush(textObject.ForeColor),layoutRect,strFormat);
					}
					else if(reportObject.ObjectKind==ReportObjectKind.FieldObject){
						fieldObject=reportObject;
						strFormat=ReportObject.GetStringFormat(fieldObject.TextAlign);
						RectangleF layoutRect=new RectangleF(xPos+fieldObject.Location.X,yPos+fieldObject.Location.Y,fieldObject.Size.Width,fieldObject.Size.Height);
						if(fieldObject.FieldKind==FieldDefKind.DataTableField){
							rawText=MyReport.ReportTable.Rows
								[i][MyReport.DataFields.IndexOf(fieldObject.DataField)].ToString();
							displayText=rawText;
							if(fieldObject.ValueType==FieldValueType.Age){
								displayText=Shared.AgeToString(Shared.DateToAge(PIn.PDate(MyReport.ReportTable.Rows[i][MyReport.DataFields.IndexOf(fieldObject.DataField)].ToString())));//(fieldObject.FormatString);
							}
							else if(fieldObject.ValueType==FieldValueType.Boolean){
								displayText=PIn.PBool(MyReport.ReportTable.Rows[i][MyReport.DataFields.IndexOf(fieldObject.DataField)].ToString()).ToString();//(fieldObject.FormatString);
								if(i>0 && fieldObject.SuppressIfDuplicate){
									prevDisplayText=PIn.PBool(MyReport.ReportTable.Rows[i-1][MyReport.DataFields.IndexOf(fieldObject.DataField)].ToString()).ToString();
								}
							}
							else if(fieldObject.ValueType==FieldValueType.Date){
								displayText=PIn.PDateT(MyReport.ReportTable.Rows[i][MyReport.DataFields.IndexOf(fieldObject.DataField)].ToString()).ToString(fieldObject.FormatString);
								if(i>0 && fieldObject.SuppressIfDuplicate){
									prevDisplayText=PIn.PDateT(MyReport.ReportTable.Rows[i-1][MyReport.DataFields.IndexOf(fieldObject.DataField)].ToString()).ToString(fieldObject.FormatString);
								}
							}
							else if(fieldObject.ValueType==FieldValueType.Integer){
								displayText=PIn.PInt(MyReport.ReportTable.Rows[i][MyReport.DataFields.IndexOf(fieldObject.DataField)].ToString()).ToString(fieldObject.FormatString);
								if(i>0 && fieldObject.SuppressIfDuplicate){
									prevDisplayText=PIn.PInt(MyReport.ReportTable.Rows[i-1][MyReport.DataFields.IndexOf(fieldObject.DataField)].ToString()).ToString(fieldObject.FormatString);
								}
							}
							else if(fieldObject.ValueType==FieldValueType.Number){
								displayText=PIn.PDouble(MyReport.ReportTable.Rows[i][MyReport.DataFields.IndexOf(fieldObject.DataField)].ToString()).ToString(fieldObject.FormatString);
								if(i>0 && fieldObject.SuppressIfDuplicate){
									prevDisplayText=PIn.PDouble(MyReport.ReportTable.Rows[i-1][MyReport.DataFields.IndexOf(fieldObject.DataField)].ToString()).ToString(fieldObject.FormatString);
								}
							}
							else if(fieldObject.ValueType==FieldValueType.String){
								displayText=rawText;
								if(i>0 && fieldObject.SuppressIfDuplicate){
									prevDisplayText=MyReport.ReportTable.Rows[i-1][MyReport.DataFields.IndexOf(fieldObject.DataField)].ToString();
								}
							}
							//suppress if duplicate:
							if(i>0 && fieldObject.SuppressIfDuplicate && displayText==prevDisplayText){
								displayText="";
							}
						}
						else if(fieldObject.FieldKind==FieldDefKind.FormulaField){
							//can't do formulas yet
						}
						else if(fieldObject.FieldKind==FieldDefKind.SpecialField){
							
						}
						else if(fieldObject.FieldKind==FieldDefKind.SummaryField){
							
						}
						g.DrawString(displayText,fieldObject.Font
							,new SolidBrush(fieldObject.ForeColor),layoutRect,strFormat);
					}
					//incomplete: else if lines
					//incomplete: else if boxes.
				}//foreach reportObject
				yPos+=section.Height;
			}//for i rows
			rowsPrinted+=rowsToPrint;
		}

		private void butSetup_Click(object sender, System.EventArgs e) {
			setupDialog2.AllowMargins=true;
			setupDialog2.AllowOrientation=true;
			setupDialog2.AllowPaper=false;
			setupDialog2.AllowPrinter=false;
			setupDialog2.Document=pd2;
			setupDialog2.ShowDialog();
		}

		private void OnPrint_Click() {
			ResetPd2();
			PrintReport();
		}

		private void OnBack_Click(){
			if(printPreviewControl2.StartPage==0) return;
			printPreviewControl2.StartPage--;
			ToolBarMain.Buttons["PageNum"].Text=(printPreviewControl2.StartPage+1).ToString()
				+" / "+totalPages.ToString();
			ToolBarMain.Invalidate();
			//labelTotPages.Text=
		}

		private void OnFwd_Click(){
			if(printPreviewControl2.StartPage==totalPages-1) return;
			printPreviewControl2.StartPage++;
			ToolBarMain.Buttons["PageNum"].Text=(printPreviewControl2.StartPage+1).ToString()
				+" / "+totalPages.ToString();
			ToolBarMain.Invalidate();
		}

		private void OnExport_Click(){
			SaveFileDialog saveFileDialog2=new SaveFileDialog();
      saveFileDialog2.AddExtension=true;
			//saveFileDialog2.Title=Lan.g(this,"Select Folder to Save File To");
			saveFileDialog2.FileName=MyReport.ReportName+".txt";
			if(!Directory.Exists(PrefB.GetString("ExportPath"))){
				try{
					Directory.CreateDirectory(PrefB.GetString("ExportPath"));
					saveFileDialog2.InitialDirectory=PrefB.GetString("ExportPath");
				}
				catch{
					//initialDirectory will be blank
				}
			}
			else{
				saveFileDialog2.InitialDirectory=PrefB.GetString("ExportPath");
			}
			//saveFileDialog2.DefaultExt="txt";
			saveFileDialog2.Filter="Text files(*.txt)|*.txt|Excel Files(*.xls)|*.xls|All files(*.*)|*.*";
      saveFileDialog2.FilterIndex=0;
		  if(saveFileDialog2.ShowDialog()!=DialogResult.OK){
	   	  return;
			}
			try{
			  using(StreamWriter sw=new StreamWriter(saveFileDialog2.FileName,false)){
					String line="";  
					for(int i=0;i<MyReport.ReportTable.Columns.Count;i++){
						line+=MyReport.ReportTable.Columns[i].Caption;
						if(i<MyReport.ReportTable.Columns.Count-1){
							line+="\t";
						}
					}
					sw.WriteLine(line);
					string cell;
					for(int i=0;i<MyReport.ReportTable.Rows.Count;i++){
						line="";
						for(int j=0;j<MyReport.ReportTable.Columns.Count;j++){
							cell=MyReport.ReportTable.Rows[i][j].ToString();
							cell=cell.Replace("\r","");
							cell=cell.Replace("\n","");
							cell=cell.Replace("\t","");
							cell=cell.Replace("\"","");
							line+=cell;
							if(j<MyReport.ReportTable.Columns.Count-1){
								line+="\t";
							}
						}
						sw.WriteLine(line);
					}
				}//using
      }
      catch{
        MessageBox.Show(Lan.g(this,"File in use by another program.  Close and try again."));
				return;
			}
			MessageBox.Show(Lan.g(this,"File created successfully"));
		}

		private void OnClose_Click() {
			this.Close();
		}

		private void button1_Click(object sender, System.EventArgs e) {
			//ScriptEngine.FormulaCode = 
			/*string functionCode=
			@"using System.Windows.Forms;
				using System;
				public class Test{
					public static void Main(){
						MessageBox.Show(""This is a test"");
						Test2 two = new Test2();
						two.Stuff();
					}
				}
				public class Test2{
					public void Stuff(){

					}
				}";
			CodeDomProvider codeProvider=new CSharpCodeProvider();
			ICodeCompiler compiler = codeProvider.CreateCompiler();
			CompilerParameters compilerParams = new CompilerParameters();
			compilerParams.CompilerOptions = "/target:library /optimize";
			compilerParams.GenerateExecutable = false;
			compilerParams.GenerateInMemory = true;
			compilerParams.IncludeDebugInformation = false;
			compilerParams.ReferencedAssemblies.Add("mscorlib.dll");
			compilerParams.ReferencedAssemblies.Add("System.dll");
			compilerParams.ReferencedAssemblies.Add("System.Windows.Forms.dll");
			CompilerResults results = compiler.CompileAssemblyFromSource(
                             compilerParams,functionCode);
			if (results.Errors.Count > 0){
				MessageBox.Show(results.Errors[0].ErrorText);
				//foreach (CompilerError error in results.Errors)
				//	DotNetScriptEngine.LogAllErrMsgs("Compine Error:"+error.ErrorText); 
				return;
			}
			Assembly assembly = results.CompiledAssembly;	
			//Use reflection to call the Main function in the assembly
			ScriptEngine.RunScript(assembly, "Main");		
			*/

		}

		

		

		

		


	}
}
