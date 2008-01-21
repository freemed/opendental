using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Globalization;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;

namespace OpenDental{
///<summary></summary>
	public class FormRpStatement : System.Windows.Forms.Form{
		private System.Windows.Forms.PrintPreviewControl printPreviewControl2;
		private OpenDental.UI.Button butPrint;
		private OpenDental.UI.Button butClose;
		private System.Windows.Forms.Panel panelZoom;
		private OpenDental.UI.Button butFwd;
		private OpenDental.UI.Button butBack;
		private System.Windows.Forms.Label labelTotPages;
		private OpenDental.UI.Button butZoomIn;
		private OpenDental.UI.Button butFullPage;
		private System.ComponentModel.Container components = null;
		private System.Drawing.Printing.PrintDocument pd2;
		private int totalPages;
		private bool HidePayment;
		private string[] Notes;
		///<summary>Holds the data for all the statements.  Each item in the collection is one statement.</summary>
		private List<FamilyStatementData> FamilyStatementDataList;
		//private Family FamCur;
		private bool SubtotalsOnly;
		///<summary>First dim is for the family. Second dim is family members</summary>
		private int[][] PatNums;
		///<summary>The guarantor for the statement that is currently printing.</summary>
		private Patient PatGuar;
		//<summary>Remove the detail from the itemized grid and make the statement simple</summary>
		//private bool  SimpleStatement;

		///<summary></summary>
		public FormRpStatement(){
			InitializeComponent();
			Lan.F(this, new Control[] 
				{//exclude:
					labelTotPages
				});
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing )	{
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRpStatement));
			this.printPreviewControl2 = new System.Windows.Forms.PrintPreviewControl();
			this.butPrint = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.panelZoom = new System.Windows.Forms.Panel();
			this.butFwd = new OpenDental.UI.Button();
			this.butBack = new OpenDental.UI.Button();
			this.labelTotPages = new System.Windows.Forms.Label();
			this.butZoomIn = new OpenDental.UI.Button();
			this.butFullPage = new OpenDental.UI.Button();
			this.pd2 = new System.Drawing.Printing.PrintDocument();
			this.panelZoom.SuspendLayout();
			this.SuspendLayout();
			// 
			// printPreviewControl2
			// 
			this.printPreviewControl2.AutoZoom = false;
			this.printPreviewControl2.Location = new System.Drawing.Point(-116,-7);
			this.printPreviewControl2.Name = "printPreviewControl2";
			this.printPreviewControl2.Size = new System.Drawing.Size(842,538);
			this.printPreviewControl2.TabIndex = 6;
			this.printPreviewControl2.Zoom = 1;
			// 
			// butPrint
			// 
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPrint.Autosize = true;
			this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.CornerRadius = 4F;
			this.butPrint.Image = global::OpenDental.Properties.Resources.butPrintSmall;
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Location = new System.Drawing.Point(340,5);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(85,27);
			this.butPrint.TabIndex = 8;
			this.butPrint.Text = "          Print";
			this.butPrint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.Location = new System.Drawing.Point(434,5);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,27);
			this.butClose.TabIndex = 7;
			this.butClose.Text = "Close";
			// 
			// panelZoom
			// 
			this.panelZoom.Controls.Add(this.butFwd);
			this.panelZoom.Controls.Add(this.butBack);
			this.panelZoom.Controls.Add(this.labelTotPages);
			this.panelZoom.Controls.Add(this.butZoomIn);
			this.panelZoom.Controls.Add(this.butFullPage);
			this.panelZoom.Controls.Add(this.butClose);
			this.panelZoom.Controls.Add(this.butPrint);
			this.panelZoom.Location = new System.Drawing.Point(35,419);
			this.panelZoom.Name = "panelZoom";
			this.panelZoom.Size = new System.Drawing.Size(524,37);
			this.panelZoom.TabIndex = 12;
			// 
			// butFwd
			// 
			this.butFwd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butFwd.Autosize = true;
			this.butFwd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butFwd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butFwd.CornerRadius = 4F;
			this.butFwd.Image = global::OpenDental.Properties.Resources.Right;
			this.butFwd.Location = new System.Drawing.Point(195,7);
			this.butFwd.Name = "butFwd";
			this.butFwd.Size = new System.Drawing.Size(18,22);
			this.butFwd.TabIndex = 13;
			this.butFwd.Click += new System.EventHandler(this.butFwd_Click);
			// 
			// butBack
			// 
			this.butBack.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butBack.Autosize = true;
			this.butBack.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBack.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBack.CornerRadius = 4F;
			this.butBack.Image = global::OpenDental.Properties.Resources.Left;
			this.butBack.Location = new System.Drawing.Point(123,7);
			this.butBack.Name = "butBack";
			this.butBack.Size = new System.Drawing.Size(18,22);
			this.butBack.TabIndex = 12;
			this.butBack.Click += new System.EventHandler(this.butBack_Click);
			// 
			// labelTotPages
			// 
			this.labelTotPages.Font = new System.Drawing.Font("Microsoft Sans Serif",9F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelTotPages.Location = new System.Drawing.Point(143,9);
			this.labelTotPages.Name = "labelTotPages";
			this.labelTotPages.Size = new System.Drawing.Size(47,18);
			this.labelTotPages.TabIndex = 11;
			this.labelTotPages.Text = "1 / 2";
			this.labelTotPages.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// butZoomIn
			// 
			this.butZoomIn.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butZoomIn.Autosize = true;
			this.butZoomIn.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butZoomIn.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butZoomIn.CornerRadius = 4F;
			this.butZoomIn.Image = global::OpenDental.Properties.Resources.butZoomIn;
			this.butZoomIn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butZoomIn.Location = new System.Drawing.Point(9,5);
			this.butZoomIn.Name = "butZoomIn";
			this.butZoomIn.Size = new System.Drawing.Size(94,27);
			this.butZoomIn.TabIndex = 10;
			this.butZoomIn.Text = "       Zoom In";
			this.butZoomIn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butZoomIn.Click += new System.EventHandler(this.butZoomIn_Click);
			// 
			// butFullPage
			// 
			this.butFullPage.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butFullPage.Autosize = true;
			this.butFullPage.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butFullPage.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butFullPage.CornerRadius = 4F;
			this.butFullPage.Location = new System.Drawing.Point(9,5);
			this.butFullPage.Name = "butFullPage";
			this.butFullPage.Size = new System.Drawing.Size(75,27);
			this.butFullPage.TabIndex = 9;
			this.butFullPage.Text = "Full Page";
			this.butFullPage.Visible = false;
			this.butFullPage.Click += new System.EventHandler(this.butFullPage_Click);
			// 
			// FormRpStatement
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(787,610);
			this.Controls.Add(this.panelZoom);
			this.Controls.Add(this.printPreviewControl2);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormRpStatement";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Statement";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.FormRpStatement_Layout);
			this.Load += new System.EventHandler(this.FormRpStatement_Load);
			this.panelZoom.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		
		private void FormRpStatement_Layout(object sender, System.Windows.Forms.LayoutEventArgs e) {
			printPreviewControl2.Location=new Point(0,0);
			printPreviewControl2.Height=ClientSize.Height-39;
			printPreviewControl2.Width=ClientSize.Width;	
			panelZoom.Location=new Point(ClientSize.Width-620,ClientSize.Height-38);
		}

		private void FormRpStatement_Load(object sender, System.EventArgs e) {
			//this only happens during debugging
			labelTotPages.Text="1 / "+totalPages.ToString();
			if(PrefB.GetBool("FuchsOptionsOn")) {
				butFullPage.Visible = true;
				butZoomIn.Visible = false;
				printPreviewControl2.Zoom = 1;
			}
			else{
				printPreviewControl2.Zoom = ((double)printPreviewControl2.ClientSize.Height
				/ (double)pd2.DefaultPageSettings.PaperSize.Height);
			}
		}

		///<summary>Used from FormBilling to print all statements for all the supplied patNums.  But commlog entries are done afterward, only when user confirms that they printed properly.</summary>
		public void LoadAndPrint(int[] guarNums,string generalNote){
			//this will be moved later when we make each statement an actual object. Right now, it's functional, but inefficient:
			int[][] patNums=new int[guarNums.Length][];
			Family famCur;
			ArrayList numsFam;
			string[] notes=new string[guarNums.Length];
			Dunning[] dunList=Dunnings.Refresh();
			int ageAccount=0;
			YN insIsPending=YN.Unknown;
			for(int i=0;i<guarNums.Length;i++){//loop through each family
				famCur=Patients.GetFamily(guarNums[i]);
				numsFam=new ArrayList();
				for(int j=0;j<famCur.List.Length;j++){
					if(j==0//because a non-patient might be the guarantor
						|| famCur.List[j].PatStatus==PatientStatus.Patient)
					{
						numsFam.Add(famCur.List[j].PatNum);
					}
				}
				patNums[i]=new int[numsFam.Count];
				for(int j=0;j<numsFam.Count;j++){
					patNums[i][j]=(int)numsFam[j]; 
				}
				if(famCur.List[0].BalOver90>0){
					ageAccount=90;
				}
				else if(famCur.List[0].Bal_61_90>0){
					ageAccount=60;
				}
				else if(famCur.List[0].Bal_31_60>0){
					ageAccount=30;
				}
				else{
					ageAccount=0;
				}
				if(famCur.List[0].InsEst>0){
					insIsPending=YN.Yes;
				}
				else{
					insIsPending=YN.No;
				}
				notes[i]=Dunnings.GetMessage(dunList,famCur.List[0].BillingType,ageAccount,insIsPending);
				if(notes[i]!="" && generalNote!=""){
					notes[i]+="\r\n\r\n";//Space two lines apart
				}
				notes[i]+=generalNote;
			}
			PrintStatements(patNums,DateTime.Today.AddDays(-45),DateTime.Today,true,false,false,false,notes,true,"");
		}

		///<summary>This is called from ContrAccount about 3 times and also from FormRpStatement as part of the billing process.  This is what you call to print statements, either one or many.  For the patNum parameter, the first dim is for the family. Second dim is family members. The note array must have one element for every statement, so same number as dim one of patNums.  IsBill distinguishes bills sent by mail from statements handed to the patient.  SimpleStatement removes the detail of the itemized grid.  Instead of printing, if pdfFullFileName is specified (including full path), then it saves as pdf to that file.</summary>
		public void PrintStatements(int[][] patNums,DateTime fromDate,DateTime toDate,bool includeClaims, bool subtotalsOnly,bool hidePayment,bool nextAppt,string[] notes,bool isBill,string pdfFullFileName){
			//these 5 variables are needed by the printing logic. The rest are not.
			PatNums=(int[][])patNums.Clone();
			Notes=(string[])notes.Clone();
			SubtotalsOnly=subtotalsOnly;
			HidePayment=hidePayment;
			//SimpleStatement=simpleStatement;
			PrintDocument pd=new PrintDocument();
			if(pdfFullFileName==""){
				if(!Printers.SetPrinter(pd,PrintSituation.Statement)){
					return;
				}
				pd.PrintPage+=new PrintPageEventHandler(this.pd2_PrintPage);
			}
			pd.DefaultPageSettings.Margins=new Margins(40,40,40,60);
			if(CultureInfo.CurrentCulture.Name.EndsWith("CH")) {//CH is for switzerland. eg de-CH
				//leave a big margin on the bottom for the routing slip
				pd.DefaultPageSettings.Margins=new Margins(40,40,40,440);//4.4" from bottom
			}
			//pd.OriginAtMargins=true;
			if(pd.DefaultPageSettings.PaperSize.Height==0) {
				pd.DefaultPageSettings.PaperSize=new PaperSize("default",850,1100);
			}
			ContrAccount contrAccount=new ContrAccount();
			//StatementA=new string[patNums.GetLength(0)][,];
			FamilyStatementDataList=new List<FamilyStatementData>();
			Commlog commlog;
			FamilyStatementData famData;
			for(int i=0;i<patNums.GetLength(0);i++){//loop through each family
				famData=AssembleStatement(contrAccount,patNums[i],fromDate,toDate,includeClaims,nextAppt);
				FamilyStatementDataList.Add(famData);
				//This has all been moved externally for multiple billing statements
				if(patNums.GetLength(0)==1){
					commlog=new Commlog();
					commlog.CommDateTime=DateTime.Now;
					commlog.CommType=0;
					commlog.Note=Notes[i];
					commlog.SentOrReceived=CommSentOrReceived.Sent;
					if(isBill){
						commlog.Mode_=CommItemMode.Mail;
					}
					else{
						commlog.Mode_=CommItemMode.None;//.InPerson;
					}
					commlog.IsStatementSent=true;
					commlog.PatNum=patNums[i][0];//uaually the guarantor
					//there is no dialog here because it is just a simple entry
					Commlogs.Insert(commlog);
				}
			}
			//linesPrinted=0;
			//isFirstLineOnPage=false;
			//notePrinted=false;
			//pagesPrinted=0;
			MigraDoc.DocumentObjectModel.Document doc=CreateDocument(pd);
			if(pdfFullFileName==""){//print
				MigraDoc.Rendering.Printing.MigraDocPrintDocument printdoc=new MigraDoc.Rendering.Printing.MigraDocPrintDocument();
				MigraDoc.Rendering.DocumentRenderer renderer=new MigraDoc.Rendering.DocumentRenderer(doc);
				renderer.PrepareDocument();
				totalPages=renderer.FormattedDocument.PageCount;
				labelTotPages.Text="1 / "+totalPages.ToString();
				printdoc.Renderer=renderer;
				printdoc.PrinterSettings=pd.PrinterSettings;
				#if DEBUG
					printPreviewControl2.Document=printdoc;
				#else
					try{
						printdoc.Print();
					}
					catch{
						MessageBox.Show(Lan.g(this,"Printer not available"));
					}
				#endif
			}
			else{
				MigraDoc.Rendering.PdfDocumentRenderer pdfRenderer=new MigraDoc.Rendering.PdfDocumentRenderer(true,PdfFontEmbedding.Always);
				pdfRenderer.Document=doc;
				pdfRenderer.RenderDocument();
				pdfRenderer.PdfDocument.Save(pdfFullFileName);
			}
		}

		/// <summary>Gets one FamilyStatementData for a single family.</summary>
		private FamilyStatementData AssembleStatement(ContrAccount contrAccount,int[] famPatNums,DateTime fromDate,DateTime toDate,bool includeClaims,bool nextAppt){
			FamilyStatementData retVal=new FamilyStatementData();
			retVal.GuarNum=famPatNums[0];
			PatStatementAbout patAbout;
			PatStatementData patData;
			for(int i=0;i<famPatNums.Length;i++){
				patAbout=new PatStatementAbout();
				patAbout.PatNum=famPatNums[i];
			//	contrAccount.RefreshModuleData(famPatNums[i]);
				patAbout.PatName=contrAccount.PatCur.GetNameLF();
				patAbout.ApptDescript="";
				if(nextAppt){
					Appointment[] allAppts=Appointments.GetForPat(contrAccount.PatCur.PatNum);
					for(int a=0;a<allAppts.Length;a++){
						if(allAppts[a].AptDateTime.Date <= DateTime.Today 
							|| allAppts[a].AptStatus == ApptStatus.PtNote 
							|| allAppts[a].AptStatus == ApptStatus.PtNoteCompleted) 
						{
							continue;//ignore old appts.
						}
						patAbout.ApptDescript+=":  "+Lan.g(this,"Your next appointment is on")+" "+allAppts[a].AptDateTime.ToString();
						break;//so that only one appointment will be displayed
					}
				}
				if(SubtotalsOnly) {
					patAbout.Balance=contrAccount.SubTotal;
				}
				else {
					patAbout.Balance=contrAccount.PatCur.EstBalance;
				}
				retVal.PatAboutList.Add(patAbout);
				patData=new PatStatementData();
				patData.PatNum=famPatNums[i];
			//	contrAccount.FillAcctLineList(fromDate,toDate,includeClaims,SubtotalsOnly);
			//	patData.PatData=contrAccount.AcctLineList;
				retVal.PatDataList.Add(patData);
			}
			return retVal;
		}

		private void GetPatGuar(int patNum){
			if(PatGuar!=null
				&& patNum==PatGuar.PatNum){//if PatGuar is already set
				return;
			}
			//but if the guarantor is not on the list of patients in the fam to print, it will also refresh
      Family FamCur=Patients.GetFamily(patNum);
			PatGuar=FamCur.List[0].Copy();
		}

		///<summary>Supply pd so that we know the paper size and margins.</summary>
		private MigraDoc.DocumentObjectModel.Document CreateDocument(PrintDocument pd){
			MigraDoc.DocumentObjectModel.Document doc= new MigraDoc.DocumentObjectModel.Document();
			doc.DefaultPageSetup.PageWidth=Unit.FromInch((double)pd.DefaultPageSettings.PaperSize.Width/100);
			doc.DefaultPageSetup.PageHeight=Unit.FromInch((double)pd.DefaultPageSettings.PaperSize.Height/100);
			doc.DefaultPageSetup.TopMargin=Unit.FromInch((double)pd.DefaultPageSettings.Margins.Top/100);
			doc.DefaultPageSetup.LeftMargin=Unit.FromInch((double)pd.DefaultPageSettings.Margins.Left/100);
			doc.DefaultPageSetup.RightMargin=Unit.FromInch((double)pd.DefaultPageSettings.Margins.Right/100);
			doc.DefaultPageSetup.BottomMargin=Unit.FromInch((double)pd.DefaultPageSettings.Margins.Bottom/100);
			for(int i=0;i<PatNums.Length;i++){
				AddOneFamily(i,doc);
			}
			return doc;
		}

		private void AddOneFamily(int famIndex//,MigraDoc.DocumentObjectModel.Section section
			,MigraDoc.DocumentObjectModel.Document doc)
		{
			MigraDoc.DocumentObjectModel.Section section=doc.AddSection();//so that Swiss will have different footer for each patient.
			string text;
			MigraDoc.DocumentObjectModel.Font font;
			GetPatGuar(PatNums[famIndex][0]);
			//HEADING------------------------------------------------------------------------------
			Paragraph par=section.AddParagraph();
			ParagraphFormat parformat=new ParagraphFormat();
			parformat.Alignment=ParagraphAlignment.Center;
			par.Format=parformat;
			font=MigraDocHelper.CreateFont(14,true);
			text=Lan.g(this,"STATEMENT");
			par.AddFormattedText(text,font);
			text=DateTime.Today.ToShortDateString();
			font=MigraDocHelper.CreateFont(10);
			par.AddLineBreak();
			par.AddFormattedText(text,font);
			text=Lan.g(this,"Account Number")+" ";
			if(PrefB.GetBool("StatementAccountsUseChartNumber")) {
				text+=PatGuar.ChartNumber;
			}
			else {
				text+=PatGuar.PatNum;
			}
			par.AddLineBreak();
			par.AddFormattedText(text,font);
			TextFrame frame;
			//Practice Address----------------------------------------------------------------------
			if(PrefB.GetBool("StatementShowReturnAddress")) {
				font=MigraDocHelper.CreateFont(10);
				frame=section.AddTextFrame();
				frame.RelativeVertical=RelativeVertical.Page;
				frame.RelativeHorizontal=RelativeHorizontal.Page;
				frame.MarginLeft=Unit.Zero;
				frame.MarginTop=Unit.Zero;
				frame.Top=TopPosition.Parse("0.5 in");
				frame.Left=LeftPosition.Parse("0.3 in");
				frame.Width=Unit.FromInch(3);
				if(!PrefB.GetBool("EasyNoClinics") && Clinics.List.Length>0 //if using clinics
						&& Clinics.GetClinic(PatGuar.ClinicNum)!=null)//and this patient assigned to a clinic
					{
					Clinic clinic=Clinics.GetClinic(PatGuar.ClinicNum);
					par=frame.AddParagraph();
					par.Format.Font=font;
					par.AddText(clinic.Description);
					par.AddLineBreak();
					par.AddText(clinic.Address);
					par.AddLineBreak();
					if(clinic.Address2!="") {
						par.AddText(clinic.Address2);
						par.AddLineBreak();
					}
					if(CultureInfo.CurrentCulture.Name.EndsWith("CH")) {//CH is for switzerland. eg de-CH
						par.AddText(clinic.Zip+" "+clinic.City);
					}
					else {
						par.AddText(clinic.City+", "+clinic.State+" "+clinic.Zip);
					}
					par.AddLineBreak();
					text=clinic.Phone;
					if(text.Length==10){
						text="("+text.Substring(0,3)+")"+text.Substring(3,3)+"-"+text.Substring(6);
					}
					par.AddText(text);
					//Nintendo ES, BrainAge
					par.AddLineBreak();
				}
				else {
					par=frame.AddParagraph();
					par.Format.Font=font;
					par.AddText(PrefB.GetString("PracticeTitle"));
					par.AddLineBreak();
					par.AddText(PrefB.GetString("PracticeAddress"));
					par.AddLineBreak();
					if(PrefB.GetString("PracticeAddress2")!="") {
						par.AddText(PrefB.GetString("PracticeAddress2"));
						par.AddLineBreak();
					}
					if(CultureInfo.CurrentCulture.Name.EndsWith("CH")) {//CH is for switzerland. eg de-CH
						par.AddText(PrefB.GetString("PracticeZip")+" "+PrefB.GetString("PracticeCity"));
					}
					else {
						par.AddText(PrefB.GetString("PracticeCity")+", "+PrefB.GetString("PracticeST")+" "+PrefB.GetString("PracticeZip"));
					}
					par.AddLineBreak();
					text=PrefB.GetString("PracticePhone");
					if(text.Length==10){
						text="("+text.Substring(0,3)+")"+text.Substring(3,3)+"-"+text.Substring(6);
					}
					par.AddText(text);
					par.AddLineBreak();
				}
			}
			//AMOUNT ENCLOSED------------------------------------------------------------------------------------------------------
			Table table;
			Column col;
			Row row;
			Cell cell;
			frame=MigraDocHelper.CreateContainer(section,450,110,330,29);
			if(!HidePayment && !SubtotalsOnly) {
				table=MigraDocHelper.DrawTable(frame,0,0,29);
				col=table.AddColumn(Unit.FromInch(1.1));
				col=table.AddColumn(Unit.FromInch(1.1));
				col=table.AddColumn(Unit.FromInch(1.1));				
				row=table.AddRow();
				row.Format.Alignment=ParagraphAlignment.Center;
				row.Borders.Color=Colors.Black;
				row.Shading.Color=Colors.LightGray;
				row.TopPadding=Unit.FromInch(0);
				row.BottomPadding=Unit.FromInch(0);
				font=MigraDocHelper.CreateFont(8,true);
				cell=row.Cells[0];
				par=cell.AddParagraph();
				par.AddFormattedText(Lan.g(this,"Amount Due"),font);
				cell=row.Cells[1];
				par=cell.AddParagraph();
				par.AddFormattedText(Lan.g(this,"Date Due"),font);
				cell=row.Cells[2];
				par=cell.AddParagraph();
				par.AddFormattedText(Lan.g(this,"Amount Enclosed"),font);
				row=table.AddRow();
				row.Format.Alignment=ParagraphAlignment.Center;
				row.Borders.Left.Color=Colors.Gray;
				row.Borders.Bottom.Color=Colors.Gray;
				row.Borders.Right.Color=Colors.Gray;
				font=MigraDocHelper.CreateFont(9);
				if(PrefB.GetBool("BalancesDontSubtractIns")) {
					text=PatGuar.BalTotal.ToString("F");
				}
				else {//this is more typical
					text=(PatGuar.BalTotal-PatGuar.InsEst).ToString("F");
				}
				cell=row.Cells[0];
				par=cell.AddParagraph();
				par.AddFormattedText(text,font);
				if(PrefB.GetInt("StatementsCalcDueDate")==-1) {
					text=Lan.g(this,"Upon Receipt");
				}
				else {
					text=DateTime.Today.AddDays(PrefB.GetInt("StatementsCalcDueDate")).ToShortDateString();
				}
				cell=row.Cells[1];
				par=cell.AddParagraph();
				par.AddFormattedText(text,font);
			}
			//Credit Card Info--------------------------------------------------------------------------------------------------------
			if(!HidePayment) {
				if(PrefB.GetBool("StatementShowCreditCard")) {
					float yPos=65;
					font=MigraDocHelper.CreateFont(7,true);
					text=Lan.g(this,"CREDIT CARD TYPE");
					MigraDocHelper.DrawString(frame,text,font,0,yPos);
					float rowHeight=30;
					System.Drawing.Font wfont=new System.Drawing.Font("Arial",7,FontStyle.Bold);
					Graphics g=this.CreateGraphics();//just to measure strings
					MigraDocHelper.DrawLine(frame,System.Drawing.Color.Black,g.MeasureString(text,wfont).Width,
						yPos+wfont.GetHeight(g),326,yPos+wfont.GetHeight(g));
					yPos+=rowHeight;
					text=Lan.g(this,"#");
					MigraDocHelper.DrawString(frame,text,font,0,yPos);
					MigraDocHelper.DrawLine(frame,System.Drawing.Color.Black,g.MeasureString(text,wfont).Width,
						yPos+wfont.GetHeight(g),326,yPos+wfont.GetHeight(g));
					yPos+=rowHeight;
					text=Lan.g(this,"EXPIRES");
					MigraDocHelper.DrawString(frame,text,font,0,yPos);
					MigraDocHelper.DrawLine(frame,System.Drawing.Color.Black,g.MeasureString(text,wfont).Width,
						yPos+wfont.GetHeight(g),326,yPos+wfont.GetHeight(g));
					yPos+=rowHeight;
					text=Lan.g(this,"AMOUNT APPROVED");
					MigraDocHelper.DrawString(frame,text,font,0,yPos);
					MigraDocHelper.DrawLine(frame,System.Drawing.Color.Black,g.MeasureString(text,wfont).Width,
						yPos+wfont.GetHeight(g),326,yPos+wfont.GetHeight(g));
					yPos+=rowHeight;
					text=Lan.g(this,"NAME");
					MigraDocHelper.DrawString(frame,text,font,0,yPos);
					MigraDocHelper.DrawLine(frame,System.Drawing.Color.Black,g.MeasureString(text,wfont).Width,
						yPos+wfont.GetHeight(g),326,yPos+wfont.GetHeight(g));
					yPos+=rowHeight;
					text=Lan.g(this,"SIGNATURE");
					MigraDocHelper.DrawString(frame,text,font,0,yPos);
					MigraDocHelper.DrawLine(frame,System.Drawing.Color.Black,g.MeasureString(text,wfont).Width,
						yPos+wfont.GetHeight(g),326,yPos+wfont.GetHeight(g));
					yPos-=rowHeight;
					text=Lan.g(this,"(As it appears on card)");
					wfont=new System.Drawing.Font("Arial",5);
					font=MigraDocHelper.CreateFont(5);
					MigraDocHelper.DrawString(frame,text,font,625-g.MeasureString(text,wfont).Width/2+5,yPos+13);
				}
			}
			//Patient's Billing Address---------------------------------------------------------------------------------------------	
			font=MigraDocHelper.CreateFont(11);
			frame=MigraDocHelper.CreateContainer(section,62.5f+12.5f,225+1,300,200);
			par=frame.AddParagraph();
			par.Format.Font=font;
			par.AddText(PatGuar.GetNameFLFormal());
			par.AddLineBreak();
			par.AddText(PatGuar.Address);
			par.AddLineBreak();
			if(PatGuar.Address2!="") {
				par.AddText(PatGuar.Address2);
				par.AddLineBreak();
			}
			if(CultureInfo.CurrentCulture.Name.EndsWith("CH")) {//CH is for switzerland. eg de-CH
				par.AddText(PatGuar.Zip+" "+PatGuar.City);
			}
			else {
				par.AddText(PatGuar.City+", "+PatGuar.State+" "+PatGuar.Zip);
			}
			//perforated line------------------------------------------------------------------------------------------------------
			//yPos=350;//3.62 inches from top, 1/3 page down
			frame=MigraDocHelper.CreateContainer(section,0,350,850,30);
			if(!HidePayment) {
				MigraDocHelper.DrawLine(frame,System.Drawing.Color.LightGray,0,0,850,0);
				text=Lan.g(this,"PLEASE DETACH AND RETURN THE UPPER PORTION WITH YOUR PAYMENT");
				font=MigraDocHelper.CreateFont(6,true,System.Drawing.Color.Gray);
				par=frame.AddParagraph();
				par.Format.Alignment=ParagraphAlignment.Center;
				par.Format.Font=font;
				par.AddText(text);
			}
			//Aging-----------------------------------------------------------------------------------
			MigraDocHelper.InsertSpacer(section,275);
			ODGridColumn gcol;
			ODGridRow grow;
			if(!HidePayment && !SubtotalsOnly ) {
				ODGrid gridAging=new ODGrid();
				this.Controls.Add(gridAging);
				gridAging.BeginUpdate();
				gridAging.Columns.Clear();
				gcol=new ODGridColumn(Lan.g(this,"0-30"),70,HorizontalAlignment.Center);
				gridAging.Columns.Add(gcol);
				gcol=new ODGridColumn(Lan.g(this,"31-60"),70,HorizontalAlignment.Center);
				gridAging.Columns.Add(gcol);
				gcol=new ODGridColumn(Lan.g(this,"61-90"),70,HorizontalAlignment.Center);
				gridAging.Columns.Add(gcol);
				gcol=new ODGridColumn(Lan.g(this,"over 90"),70,HorizontalAlignment.Center);
				gridAging.Columns.Add(gcol);
				gcol=new ODGridColumn(Lan.g(this,"Total"),70,HorizontalAlignment.Center);
				gridAging.Columns.Add(gcol);
				if(!PrefB.GetBool("BalancesDontSubtractIns")) {//this typically happens
					gcol=new ODGridColumn(Lan.g(this,"- InsEst"),70,HorizontalAlignment.Center);
					gridAging.Columns.Add(gcol);
				}
				gcol=new ODGridColumn(Lan.g(this,"= Balance"),70,HorizontalAlignment.Center);
				gridAging.Columns.Add(gcol);
				gridAging.Rows.Clear();
				//Annual max--------------------------
				grow=new ODGridRow();
				grow.Cells.Add(PatGuar.Bal_0_30.ToString("F"));
				grow.Cells.Add(PatGuar.Bal_31_60.ToString("F"));
				grow.Cells.Add(PatGuar.Bal_61_90.ToString("F"));
				grow.Cells.Add(PatGuar.BalOver90.ToString("F"));
				grow.Cells.Add(PatGuar.BalTotal.ToString("F"));
				if(PrefB.GetBool("BalancesDontSubtractIns")) {
					grow.Cells.Add(PatGuar.BalTotal.ToString("F"));
				}
				else {//this is more typical
					grow.Cells.Add(PatGuar.InsEst.ToString("F"));
					grow.Cells.Add((PatGuar.BalTotal-PatGuar.InsEst).ToString("F"));
				}
				gridAging.Rows.Add(grow);
				gridAging.EndUpdate();
				MigraDocHelper.DrawGrid(section,gridAging);
				gridAging.Dispose();
			}
			/*else if(SimpleStatement) {
				text=Lan.g(this,"Account Balance: ")+PatGuar.BalTotal.ToString("c");
				font=MigraDocHelper.CreateFont(12,true);
				par=section.AddParagraph(text);
				par.Format.Font=font;
				par.Format.Alignment=ParagraphAlignment.Center;
			}*/
			//Body Tables-----------------------------------------------------------------------------------------------------------
			ODGrid gridPat=new ODGrid();
			this.Controls.Add(gridPat);
			gridPat.BeginUpdate();
			gridPat.Columns.Clear();
			/*if(SimpleStatement) {
				gcol=new ODGridColumn(Lan.g(this,"Date"),73);
				gridPat.Columns.Add(gcol);
				gcol=new ODGridColumn(Lan.g(this,"Code"),45);
				gridPat.Columns.Add(gcol);
				gcol=new ODGridColumn(Lan.g(this,"Tooth"),42);
				gridPat.Columns.Add(gcol);
				gcol=new ODGridColumn(Lan.g(this,"Description"),235);
				gridPat.Columns.Add(gcol);
				gcol=new ODGridColumn(Lan.g(this,"Fee"),50,HorizontalAlignment.Right);
				gridPat.Columns.Add(gcol);
				gcol=new ODGridColumn(Lan.g(this,""),50,HorizontalAlignment.Right);
				gridPat.Columns.Add(gcol);
				gcol=new ODGridColumn(Lan.g(this,"Ins"),50,HorizontalAlignment.Right);
				gridPat.Columns.Add(gcol);
				gcol=new ODGridColumn(Lan.g(this,""),50,HorizontalAlignment.Right);
				gridPat.Columns.Add(gcol);
				gcol=new ODGridColumn(Lan.g(this,"Adj"),50,HorizontalAlignment.Right);
				gridPat.Columns.Add(gcol);
				gcol=new ODGridColumn(Lan.g(this,"Paid"),50,HorizontalAlignment.Right);
				gridPat.Columns.Add(gcol);
				gcol=new ODGridColumn(Lan.g(this,""),50,HorizontalAlignment.Right);
				gridPat.Columns.Add(gcol);
			}
			else{*/
				gcol=new ODGridColumn(Lan.g(this,"Date"),73);
				gridPat.Columns.Add(gcol);
				gcol=new ODGridColumn(Lan.g(this,"Code"),45);
				gridPat.Columns.Add(gcol);
				gcol=new ODGridColumn(Lan.g(this,"Tooth"),42);
				gridPat.Columns.Add(gcol);
				gcol=new ODGridColumn(Lan.g(this,"Description"),235);
				gridPat.Columns.Add(gcol);
				gcol=new ODGridColumn(Lan.g(this,"Fee"),50,HorizontalAlignment.Right);
				gridPat.Columns.Add(gcol);
				gcol=new ODGridColumn(Lan.g(this,"Ins Est"),50,HorizontalAlignment.Right);
				gridPat.Columns.Add(gcol);
				gcol=new ODGridColumn(Lan.g(this,"Ins Pd"),50,HorizontalAlignment.Right);
				gridPat.Columns.Add(gcol);
				gcol=new ODGridColumn(Lan.g(this,"Patient"),50,HorizontalAlignment.Right);
				gridPat.Columns.Add(gcol);
				gcol=new ODGridColumn(Lan.g(this,"Adj"),50,HorizontalAlignment.Right);
				gridPat.Columns.Add(gcol);
				gcol=new ODGridColumn(Lan.g(this,"Paid"),50,HorizontalAlignment.Right);
				gridPat.Columns.Add(gcol);
				gcol=new ODGridColumn(Lan.g(this,"Balance"),50,HorizontalAlignment.Right);
				gridPat.Columns.Add(gcol);
			//}
			gridPat.Width=gridPat.WidthAllColumns+20;
			gridPat.EndUpdate();
			//Loop through each patient-----------------------------------------------------------------------------------------------
			List<AcctLine> lineData;
			for(int i=0;i<FamilyStatementDataList[famIndex].PatAboutList.Count;i++){
				par=section.AddParagraph();
				par.Format.Font=MigraDocHelper.CreateFont(10,true);
				par.Format.SpaceBefore=Unit.FromInch(.05);
				par.Format.SpaceAfter=Unit.FromInch(.05);
				par.AddText(FamilyStatementDataList[famIndex].PatAboutList[i].PatName);
				if(FamilyStatementDataList[famIndex].PatAboutList[i].ApptDescript!=""){
					par=section.AddParagraph();
					par.Format.Font=MigraDocHelper.CreateFont(9);//same as body font
					par.AddText(FamilyStatementDataList[famIndex].PatAboutList[i].ApptDescript);
				}
				gridPat.BeginUpdate();
				gridPat.Rows.Clear();
				lineData=FamilyStatementDataList[famIndex].PatDataList[i].PatData;
				for(int p=0;p<lineData.Count;p++){
					grow=new ODGridRow();
					grow.Cells.Add(lineData[p].Date);
					grow.Cells.Add(lineData[p].Code);
					grow.Cells.Add(lineData[p].Tooth);
					grow.Cells.Add(lineData[p].Description);
					grow.Cells.Add(lineData[p].Fee);
					grow.Cells.Add(lineData[p].InsEst);
					grow.Cells.Add(lineData[p].InsPay);
					grow.Cells.Add(lineData[p].Patient);
					grow.Cells.Add(lineData[p].Adj);
					grow.Cells.Add(lineData[p].Paid);
					grow.Cells.Add(lineData[p].Balance);
					gridPat.Rows.Add(grow);
				}
				gridPat.EndUpdate();
				MigraDocHelper.DrawGrid(section,gridPat);
				//Total
				//if(!SimpleStatement) {
					frame=MigraDocHelper.CreateContainer(section);
					font=MigraDocHelper.CreateFont(9,true);
					float totalPos=((float)(doc.DefaultPageSetup.PageWidth.Inch//-doc.DefaultPageSetup.LeftMargin.Inch
						//-doc.DefaultPageSetup.RightMargin.Inch)
						)*100f)/2f+(float)gridPat.WidthAllColumns/2f;
					RectangleF rectF=new RectangleF(0,0,totalPos,16);
					MigraDocHelper.DrawString(frame,FamilyStatementDataList[famIndex].PatAboutList[i].Balance.ToString("F"),font,rectF,
						ParagraphAlignment.Right);
				//}
			}
			gridPat.Dispose();
			//Note------------------------------------------------------------------------------------------------------------
			//frame=MigraDocHelper.CreateContainer(section);
			font=MigraDocHelper.CreateFont(9);
			//MigraDocHelper.DrawString(frame,,font,0,0);
			par=section.AddParagraph();
			par.Format.Font=font;
			par.AddText(Notes[famIndex]);
			#region SwissBanking
			if(CultureInfo.CurrentCulture.Name.EndsWith("CH")){//CH is for switzerland. eg de-CH
				//&& pagesPrinted==0)//only on the first page
			//{
				//float yred=744;//768;//660 for testing
				//Red line (just temp)
				//g.DrawLine(Pens.Red,0,yred,826,yred);
				MigraDoc.DocumentObjectModel.Font swfont=MigraDocHelper.CreateFont(10);
					//new Font(FontFamily.GenericSansSerif,10);
				//Bank Address---------------------------------------------------------
				HeaderFooter footer=section.Footers.Primary;
				footer.Format.Borders.Color=Colors.Black;
				//footer.AddParagraph(PrefB.GetString("BankAddress"));
				frame=footer.AddTextFrame();
				frame.RelativeVertical=RelativeVertical.Line;
				frame.RelativeHorizontal=RelativeHorizontal.Page;
				frame.MarginLeft=Unit.Zero;
				frame.MarginTop=Unit.Zero;
				frame.Top=TopPosition.Parse("0 in");
				frame.Left=LeftPosition.Parse("0 in");
				frame.Width=Unit.FromInch(8.3);
				frame.Height=300;
				//RectangleF=new RectangleF(0,0,
				MigraDocHelper.DrawString(frame,PrefB.GetString("BankAddress"),swfont,30,30);
				MigraDocHelper.DrawString(frame,PrefB.GetString("BankAddress"),swfont,246,30);
				//Office Name and Address----------------------------------------------
				text=PrefB.GetString("PracticeTitle")+"\r\n"
					+PrefB.GetString("PracticeAddress")+"\r\n";
				if(PrefB.GetString("PracticeAddress2")!="") {
					text+=PrefB.GetString("PracticeAddress2")+"\r\n";
				}
				text+=PrefB.GetString("PracticeZip")+" "+PrefB.GetString("PracticeCity");
				MigraDocHelper.DrawString(frame,text,swfont,30,89);
				MigraDocHelper.DrawString(frame,text,swfont,246,89);
				//Bank account number--------------------------------------------------
				string origBankNum=PrefB.GetString("PracticeBankNumber");//must be exactly 9 digits. 2+6+1.
				//the 6 digit portion might have 2 leading 0's which would not go into the dashed bank num.
				string dashedBankNum="?";
				//examples: 01-200027-2
				//          01-4587-1  (from 010045871)
				if(origBankNum.Length==9) {
					dashedBankNum=origBankNum.Substring(0,2)+"-"
						+origBankNum.Substring(2,6).TrimStart(new char[] { '0' })+"-"
						+origBankNum.Substring(8,1);
				}
				swfont=MigraDocHelper.CreateFont(9,true);
					//new Font(FontFamily.GenericSansSerif,9,FontStyle.Bold);
				MigraDocHelper.DrawString(frame,dashedBankNum,swfont,95,169);
				MigraDocHelper.DrawString(frame,dashedBankNum,swfont,340,169);
				//Amount------------------------------------------------------------
				double amountdue=PatGuar.BalTotal-PatGuar.InsEst;
				text=amountdue.ToString("F2");
				text=text.Substring(0,text.Length-3);
				swfont=MigraDocHelper.CreateFont(10);
				MigraDocHelper.DrawString(frame,text,swfont,new RectangleF(50,205,100,25),ParagraphAlignment.Right);
				MigraDocHelper.DrawString(frame,text,swfont,new RectangleF(290,205,100,25),ParagraphAlignment.Right);
				text=amountdue.ToString("F2");//eg 92.00
				text=text.Substring(text.Length-2,2);//eg 00
				MigraDocHelper.DrawString(frame,text,swfont,185,205);
				MigraDocHelper.DrawString(frame,text,swfont,425,205);
				//Patient Address-----------------------------------------------------
				string patAddress=PatGuar.FName+" "+PatGuar.LName+"\r\n"
					+PatGuar.Address+"\r\n";
				if(PatGuar.Address2!="") {
					patAddress+=PatGuar.Address2+"\r\n";
				}
				patAddress+=PatGuar.Zip+" "+PatGuar.City;
				MigraDocHelper.DrawString(frame,text,swfont,495,218);//middle left
				MigraDocHelper.DrawString(frame,text,swfont,30,263);//Lower left
				//Compute Reference#------------------------------------------------------
				//Reference# has exactly 27 digits
				//First 6 numbers are what we are calling the BankRouting number.
				//Next 20 numbers represent the invoice #.
				//27th number is the checksum
				string referenceNum=PrefB.GetString("BankRouting");//6 digits
				if(referenceNum.Length!=6) {
					referenceNum="000000";
				}
				referenceNum+=PatGuar.PatNum.ToString().PadLeft(12,'0')
					//"000000000000"//12 0's
					+DateTime.Today.ToString("yyyyMMdd");//+8=20
				//for testing:
				//referenceNum+="09090271100000067534";
				//"00000000000000037112";
				referenceNum+=Modulo10(referenceNum).ToString();
				//at this point, the referenceNum will always be exactly 27 digits long.
				string spacedRefNum=referenceNum.Substring(0,2)+" "+referenceNum.Substring(2,5)+" "+referenceNum.Substring(7,5)
					+" "+referenceNum.Substring(12,5)+" "+referenceNum.Substring(17,5)+" "+referenceNum.Substring(22,5);
				//text=spacedRefNum.Substring(0,15)+"\r\n"+spacedRefNum.Substring(16)+"\r\n";
				//reference# at lower left above address.  Small
				swfont=MigraDocHelper.CreateFont(7);
				MigraDocHelper.DrawString(frame,spacedRefNum,swfont,30,243);
				//Reference# at upper right---------------------------------------------------------------
				swfont=MigraDocHelper.CreateFont(10);
				MigraDocHelper.DrawString(frame,spacedRefNum,swfont,490,140);
				//Big long number at the lower right--------------------------------------------------
				/*The very long number on the bottom has this format:
				>13 numbers > 27 numbers + 9 numbers >
				>Example: 0100000254306>904483000000000000000371126+ 010045871>
				>
				>The first group of 13 numbers would begin with either 01 or only have 
				>042 without any other following numbers.  01 would be used if there is 
				>a specific amount, and 042 would be used if there is not a specific 
				>amount billed. So in the example, the billed amount is 254.30.  It has 
				>01 followed by leading zeros to fill in the balance of the digits 
				>required.  The last digit is a checksum done by the program.  If the 
				>amount would be 1,254.30 then the example should read 0100001254306.
				>
				>There is a > separator, then the reference number made up previously.
				>
				>Then a + separator, followed by the bank account number.  Previously, 
				>the number printed without the zeros, but in this case it has the zeros 
				>and not the dashes.*/
				swfont=new MigraDoc.DocumentObjectModel.Font("OCR-B 10 BT",12);
				text="01"+amountdue.ToString("F2").Replace(".","").PadLeft(10,'0');
				text+=Modulo10(text).ToString()+">"
					+referenceNum+"+ "+origBankNum+">";
				MigraDocHelper.DrawString(frame,text,swfont,255,345);
			}
			#endregion SwissBanking
			//return doc;
		}

		private void pd2_PrintPage(object sender, PrintPageEventArgs ev){//raised for each page to be printed
			/*
			Graphics g=ev.Graphics;
			float yPos=0;
			float xPos=0;
			float width=0;
			float height=0;
			float rowHeight=0;
			System.Drawing.Font font;
			Pen pen=new Pen(System.Drawing.Color.Black);
			Brush brush=Brushes.Black;
			string text;
			GetPatGuar(PatNums[famsPrinted][0]);
			#region Page 1 header
			if(pagesPrinted==0) {
				//HEADING------------------------------------------------------------------------------
				yPos=30;
				text=Lan.g(this,"STATEMENT");
				font=new System.Drawing.Font("Arial",14,FontStyle.Bold);
				g.DrawString(text,font,brush,425-g.MeasureString(text,font).Width/2,yPos);
				text=DateTime.Today.ToShortDateString();
				font=new System.Drawing.Font("Arial",10);
				yPos+=22;
				g.DrawString(text,font,brush,425-g.MeasureString(text,font).Width/2,yPos);
				yPos+=18;
				text=Lan.g(this,"Account Number")+" ";
				if(PrefB.GetBool("StatementAccountsUseChartNumber")) {
					text+=PatGuar.ChartNumber;
				}
				else {
					text+=PatGuar.PatNum;
				}
				g.DrawString(text,font,brush,425-g.MeasureString(text,font).Width/2,yPos);
				//Practice Address----------------------------------------------------------------------
				if(PrefB.GetBool("StatementShowReturnAddress")) {
					font=new System.Drawing.Font("Arial",10);
					yPos=50;
					xPos=30;
					if(!PrefB.GetBool("EasyNoClinics") && Clinics.List.Length>0 //if using clinics
						&& Clinics.GetClinic(PatGuar.ClinicNum)!=null)//and this patient assigned to a clinic
					{
						Clinic clinic=Clinics.GetClinic(PatGuar.ClinicNum);
						g.DrawString(clinic.Description,font,brush,xPos,yPos);
						yPos+=18;
						g.DrawString(clinic.Address,font,brush,xPos,yPos);
						yPos+=18;
						if(clinic.Address2!="") {
							g.DrawString(clinic.Address2,font,brush,xPos,yPos);
							yPos+=18;
						}
						if(CultureInfo.CurrentCulture.Name.EndsWith("CH")) {//CH is for switzerland. eg de-CH
							g.DrawString(clinic.Zip+" "+clinic.City,font,brush,xPos,yPos);
						}
						else {
							g.DrawString(clinic.City+", "+clinic.State+" "+clinic.Zip,font,brush,xPos,yPos);
						}
						yPos+=18;
						text=clinic.Phone;
						if(text.Length==10)
							text="("+text.Substring(0,3)+")"+text.Substring(3,3)+"-"+text.Substring(6);
						g.DrawString(text,font,brush,xPos,yPos);
					}
					else {
						g.DrawString(PrefB.GetString("PracticeTitle"),font,brush,xPos,yPos);
						yPos+=18;
						g.DrawString(PrefB.GetString("PracticeAddress"),font,brush,xPos,yPos);
						yPos+=18;
						if(PrefB.GetString("PracticeAddress2")!="") {
							g.DrawString(PrefB.GetString("PracticeAddress2"),font,brush,xPos,yPos);
							yPos+=18;
						}
						if(CultureInfo.CurrentCulture.Name.EndsWith("CH")) {//CH is for switzerland. eg de-CH
							g.DrawString(PrefB.GetString("PracticeZip")+" "+PrefB.GetString("PracticeCity"),font,brush,xPos,yPos);
						}
						else {
							g.DrawString(PrefB.GetString("PracticeCity")+", "+PrefB.GetString("PracticeST")+" "
								+PrefB.GetString("PracticeZip"),font,brush,xPos,yPos);
						}
						yPos+=18;
						text=PrefB.GetString("PracticePhone");
						if(text.Length==10)
							text="("+text.Substring(0,3)+")"+text.Substring(3,3)+"-"+text.Substring(6);
						g.DrawString(text,font,brush,xPos,yPos);
					}
					yPos+=18;
				}
				//AMOUNT ENCLOSED------------------------------------------------------------------------
				if(!HidePayment && !SubtotalsOnly) {
					yPos=110;
					xPos=450;
					width=110;//width of an individual cell
					height=14;
					float hCell=20;
					brush=Brushes.LightGray;
					pen=new Pen(System.Drawing.Color.Black);
					Pen peng=new Pen(System.Drawing.Color.Gray);
					for(int i=0;i<3;i++) {
						g.FillRectangle(brush,xPos+width*i,yPos,width,height);
						g.DrawRectangle(pen,xPos+width*i,yPos,width,height);
						g.DrawLine(peng,xPos+width*i,yPos+height+hCell,xPos+width*(i+1),yPos+height+hCell);//horiz
						g.DrawLine(peng,xPos+width*i,yPos+height,xPos+width*i,yPos+height+hCell);//vert
					}
					g.DrawLine(peng,xPos+width*3,yPos+height,xPos+width*3,yPos+height+hCell);//vert
					font=new System.Drawing.Font("Arial",8,FontStyle.Bold);
					brush=Brushes.Black;
					text=Lan.g(this,"Amount Due");
					g.DrawString(text,font,brush,xPos+width/2-g.MeasureString(text,font).Width/2,yPos);
					xPos+=width;
					text=Lan.g(this,"Date Due");
					g.DrawString(text,font,brush,xPos+width/2-g.MeasureString(text,font).Width/2,yPos);
					xPos+=width;
					text=Lan.g(this,"Amount Enclosed");
					g.DrawString(text,font,brush,xPos+width/2-g.MeasureString(text,font).Width/2,yPos);
					xPos=450;
					yPos+=height+3;
					font=new System.Drawing.Font("Arial",9);
					if(PrefB.GetBool("BalancesDontSubtractIns")) {
						text=PatGuar.BalTotal.ToString("F");
					}
					else {//this is more typical
						text=(PatGuar.BalTotal-PatGuar.InsEst).ToString("F");
					}
					g.DrawString(text,font,brush,xPos+width/2-g.MeasureString(text,font).Width/2,yPos);
					xPos+=width;
					if(PrefB.GetInt("StatementsCalcDueDate")==-1) {
						text=Lan.g(this,"Upon Receipt");
					}
					else {
						text=DateTime.Today.AddDays(PrefB.GetInt("StatementsCalcDueDate")).ToShortDateString();
					}
					g.DrawString(text,font,brush,xPos+width/2-g.MeasureString(text,font).Width/2,yPos);
				}
				//Patient's Billing Address--------------------------------------------------------	
				font=new System.Drawing.Font("Arial",11);
				brush=Brushes.Black;
				yPos=225 + 1;//+10
				xPos=62.5F+12.5F;
				//We do not show the preferred name on statements, just the actual name.
				g.DrawString(PatGuar.GetNameFLFormal(),font,brush,xPos,yPos);
				yPos+=19;
				g.DrawString(PatGuar.Address,font,brush,xPos,yPos);
				yPos+=19;
				if(PatGuar.Address2!="") {
					g.DrawString(PatGuar.Address2,font,brush,xPos,yPos);
					yPos+=19;
				}
				if(CultureInfo.CurrentCulture.Name.EndsWith("CH")) {//CH is for switzerland. eg de-CH
					g.DrawString(PatGuar.Zip+" "+PatGuar.City,font,brush,xPos,yPos);
				}
				else {
					g.DrawString(PatGuar.City+", "+PatGuar.State+" "+PatGuar.Zip,font,brush,xPos,yPos);
				}
				//Credit Card Info-----------------------------------------------------------------------
				if(!HidePayment) {
					if(PrefB.GetBool("StatementShowCreditCard")) {
						xPos=450;
						yPos=175;
						font=new System.Drawing.Font("Arial",7,FontStyle.Bold);
						brush=Brushes.Black;
						pen=new Pen(System.Drawing.Color.Black);
						text=Lan.g(this,"CREDIT CARD TYPE");
						rowHeight=30;
						g.DrawString(text,font,brush,xPos,yPos);
						g.DrawLine(pen,xPos+(g.MeasureString(text,font)).Width,
							yPos+font.GetHeight(g),776,yPos+font.GetHeight(g));
						yPos+=rowHeight;
						text=Lan.g(this,"#");
						g.DrawString(text,font,brush,xPos,yPos);
						g.DrawLine(pen,xPos+(g.MeasureString(text,font)).Width,
							yPos+font.GetHeight(g),776,yPos+font.GetHeight(g));
						yPos+=rowHeight;
						text=Lan.g(this,"EXPIRES");
						g.DrawString(text,font,brush,xPos,yPos);
						g.DrawLine(pen,xPos+(g.MeasureString(text,font)).Width,
							yPos+font.GetHeight(g),776,yPos+font.GetHeight(g));
						yPos+=rowHeight;
						text=Lan.g(this,"AMOUNT APPROVED");
						g.DrawString(text,font,brush,xPos,yPos);
						g.DrawLine(pen,xPos+(g.MeasureString(text,font)).Width,
							yPos+font.GetHeight(g),776,yPos+font.GetHeight(g));
						yPos+=rowHeight;
						text=Lan.g(this,"NAME");
						g.DrawString(text,font,brush,xPos,yPos);
						g.DrawLine(pen,xPos+(g.MeasureString(text,font)).Width,
							yPos+font.GetHeight(g),776,yPos+font.GetHeight(g));
						yPos+=rowHeight;
						text=Lan.g(this,"SIGNATURE");
						g.DrawString(text,font,brush,xPos,yPos);
						g.DrawLine(pen,xPos+(g.MeasureString(text,font)).Width,
							yPos+font.GetHeight(g),776,yPos+font.GetHeight(g));
						yPos-=rowHeight;
						text=Lan.g(this,"(As it appears on card)");
						font=new System.Drawing.Font("Arial",5);
						g.DrawString(text,font,brush,625-g.MeasureString(text,font).Width/2+5,yPos+13);
					}
				}
				//perforated line------------------------------------------------------------------
				if(!HidePayment) {
					pen=new Pen(Brushes.Black);
					pen.Width=(float).125;
					pen.DashStyle=System.Drawing.Drawing2D.DashStyle.Dot;
					yPos=350;//3.62 inches from top, 1/3 page down
					g.DrawLine(pen,0,yPos,ev.PageBounds.Width,yPos);
					text=Lan.g(this,"PLEASE DETACH AND RETURN THE UPPER PORTION WITH YOUR PAYMENT");
					yPos+=5;
					font=new System.Drawing.Font("Arial",6,FontStyle.Bold);
					xPos=425-g.MeasureString(text,font).Width/2;
					brush=Brushes.Gray;
					g.DrawString(text,font,brush,xPos,yPos);
				}
				//Aging-----------------------------------------------------------------------------------
				if(!HidePayment && !SubtotalsOnly && !SimpleStatement) {
					yPos=350+25;
					xPos=160;
					width=70;//width of an individual cell
					height=14;
					float hCell=20;
					brush=Brushes.LightGray;
					pen=new Pen(System.Drawing.Color.Black);
					Pen peng=new Pen(System.Drawing.Color.Gray);
					for(int i=0;i<7;i++) {
						g.FillRectangle(brush,xPos+width*i,yPos,width,height);
						g.DrawRectangle(pen,xPos+width*i,yPos,width,height);
						g.DrawLine(peng,xPos+width*i,yPos+height+hCell,xPos+width*(i+1),yPos+height+hCell);//horiz
						g.DrawLine(peng,xPos+width*i,yPos+height,xPos+width*i,yPos+height+hCell);//vert
					}
					g.DrawLine(peng,xPos+width*7,yPos+height,xPos+width*7,yPos+height+hCell);//vert
					font=new System.Drawing.Font("Arial",8,FontStyle.Bold);
					brush=Brushes.Black;
					text=Lan.g(this,"0-30");
					g.DrawString(text,font,brush,xPos+width/2-g.MeasureString(text,font).Width/2,yPos);
					xPos+=width;
					text=Lan.g(this,"31-60");
					g.DrawString(text,font,brush,xPos+width/2-g.MeasureString(text,font).Width/2,yPos);
					xPos+=width;
					text=Lan.g(this,"61-90");
					g.DrawString(text,font,brush,xPos+width/2-g.MeasureString(text,font).Width/2,yPos);
					xPos+=width;
					text=Lan.g(this,"over 90");
					g.DrawString(text,font,brush,xPos+width/2-g.MeasureString(text,font).Width/2,yPos);
					xPos+=width;
					text=Lan.g(this,"Total");
					g.DrawString(text,font,brush,xPos+width/2-g.MeasureString(text,font).Width/2,yPos);
					xPos+=width;
					if(!PrefB.GetBool("BalancesDontSubtractIns")) {//this typically happens
						text=Lan.g(this,"- InsEst");
						g.DrawString(text,font,brush,xPos+width/2-g.MeasureString(text,font).Width/2,yPos);
					}
					xPos+=width;
					text=Lan.g(this,"= Balance");
					g.DrawString(text,font,brush,xPos+width/2-g.MeasureString(text,font).Width/2,yPos);
					xPos=160;
					font=new System.Drawing.Font("Arial",9);
					yPos+=height+3;
					text=PatGuar.Bal_0_30.ToString("F");
					g.DrawString(text,font,brush,xPos+width/2-g.MeasureString(text,font).Width/2,yPos);
					xPos+=width;
					text=PatGuar.Bal_31_60.ToString("F");
					g.DrawString(text,font,brush,xPos+width/2-g.MeasureString(text,font).Width/2,yPos);
					xPos+=width;
					text=PatGuar.Bal_61_90.ToString("F");
					g.DrawString(text,font,brush,xPos+width/2-g.MeasureString(text,font).Width/2,yPos);
					xPos+=width;
					text=PatGuar.BalOver90.ToString("F");
					g.DrawString(text,font,brush,xPos+width/2-g.MeasureString(text,font).Width/2,yPos);
					xPos+=width;
					text=PatGuar.BalTotal.ToString("F");
					g.DrawString(text,font,brush,xPos+width/2-g.MeasureString(text,font).Width/2,yPos);
					xPos+=width;
					if(PrefB.GetBool("BalancesDontSubtractIns")) {
						xPos+=width;
						text=PatGuar.BalTotal.ToString("F");
						g.DrawString(text,font,brush,xPos+width/2-g.MeasureString(text,font).Width/2,yPos);
					}
					else {//this is more typical
						text=PatGuar.InsEst.ToString("F");
						g.DrawString(text,font,brush,xPos+width/2-g.MeasureString(text,font).Width/2,yPos);
						xPos+=width;
						text=(PatGuar.BalTotal-PatGuar.InsEst).ToString("F");
						g.DrawString(text,font,brush,xPos+width/2-g.MeasureString(text,font).Width/2,yPos);
					}
				}
				else if(SimpleStatement) {
					text = "Account Balance: $" + PatGuar.BalTotal.ToString("F");
					font = new System.Drawing.Font("Arial",18,FontStyle.Bold);
					brush = Brushes.Black;
					yPos=350+25;
					xPos = 425 - g.MeasureString(text,font).Width / 2;
					g.DrawString(text,font,brush,xPos,yPos);


				}
				yPos=350+68;
				//yPos=770;//change this value to test multiple pages
			}//if(pagesPrinted==0){
			else {
				yPos=35;
			}
			#endregion
			#region Body Tables
			//an if is not needed here because the while loop will handle it
			//Body Tables----------------------------------------------------------------------------
			int[] colPos=new int[12];
			HorizontalAlignment[] colAlign=new HorizontalAlignment[11];
			string[] ColCaption=new string[11];
			if(SimpleStatement) {
				ColCaption[0]=Lan.g(this,"Date");
				ColCaption[1]=Lan.g(this,"Code");
				ColCaption[2]=Lan.g(this,"Tooth");
				ColCaption[3]=Lan.g(this,"Description");
				ColCaption[4]=Lan.g(this,"Fee");
				ColCaption[5]=Lan.g(this,"");
				ColCaption[6]=Lan.g(this,"Ins");
				ColCaption[7]=Lan.g(this,"");
				ColCaption[8]=Lan.g(this,"Adj");
				ColCaption[9]=Lan.g(this,"Paid");
				ColCaption[10]=Lan.g(this,"");

			}
			else {
				ColCaption[0]=Lan.g(this,"Date");
				ColCaption[1]=Lan.g(this,"Code");
				ColCaption[2]=Lan.g(this,"Tooth");
				ColCaption[3]=Lan.g(this,"Description");
				ColCaption[4]=Lan.g(this,"Fee");
				ColCaption[5]=Lan.g(this,"Ins Est");
				ColCaption[6]=Lan.g(this,"Ins Pd");
				ColCaption[7]=Lan.g(this,"Patient");
				ColCaption[8]=Lan.g(this,"Adj");
				ColCaption[9]=Lan.g(this,"Paid");
				ColCaption[10]=Lan.g(this,"Balance");
			}
			colPos[0]=30;
			colAlign[0]=HorizontalAlignment.Left;//date
			colPos[1]=103;
			colAlign[1]=HorizontalAlignment.Left;//code
			colPos[2]=148;
			colAlign[2]=HorizontalAlignment.Left;//tooth
			colPos[3]=190;
			colAlign[3]=HorizontalAlignment.Left;//description
			colPos[4]=425;
			colAlign[4]=HorizontalAlignment.Right;//fee
			colPos[5]=475;
			colAlign[5]=HorizontalAlignment.Right;//insest
			colPos[6]=525;
			colAlign[6]=HorizontalAlignment.Right;//inspay
			colPos[7]=575;
			colAlign[7]=HorizontalAlignment.Right;//patient
			colPos[8]=625;
			colAlign[8]=HorizontalAlignment.Right;//adj
			colPos[9]=675;
			colAlign[9]=HorizontalAlignment.Right;//paid
			colPos[10]=725;
			colAlign[10]=HorizontalAlignment.Right;//balance
			colPos[11]=780;//+1  //col 11 is for formatting codes
			isFirstLineOnPage=true;
			while(yPos<ev.MarginBounds.Bottom
				//ev.MarginBounds.Top+ev.MarginBounds.Height
				&& linesPrinted<StatementA[famsPrinted].GetLength(1)) {
				if(StatementA[famsPrinted][11,linesPrinted]=="PatName") {
					//Patient Name-------------------------------------------------------------------------
					//if(there is not room for at least a few rows){
					//break
					//}
					g.DrawString(StatementA[famsPrinted][3,linesPrinted],NameFont,Brushes.Black,colPos[0],yPos);
					yPos+=NameFont.GetHeight(g)+7;
					//Heading Box and Lines----------------------------------------------------------------       
					rowHeight=TotalFont.GetHeight(g)+3;
					g.FillRectangle(Brushes.LightGray,colPos[0],yPos,colPos[11]-colPos[0],rowHeight);
					g.DrawRectangle(new Pen(Color.Black),colPos[0],yPos,colPos[11]-colPos[0],rowHeight);
					for(int i=1;i<11;i++)
						g.DrawLine(new Pen(Color.Black),colPos[i],yPos,colPos[i],yPos+rowHeight);
					//Column Titles
					for(int i=0;i<ColCaption.Length;i++) {
						if(colAlign[i]==HorizontalAlignment.Right) {
							g.DrawString(Lan.g(this,ColCaption[i]),TotalFont,Brushes.Black,new RectangleF(
								colPos[i+1]-g.MeasureString(ColCaption[i],TotalFont).Width-1,yPos+1
								,colPos[i+1]-colPos[i]+8,TotalFont.GetHeight(g)));
						}
						else
							g.DrawString(Lan.g(this,ColCaption[i]),TotalFont,Brushes.Black,colPos[i],yPos+1);
					}
					yPos+=rowHeight;
				}
				else if(StatementA[famsPrinted][11,linesPrinted]=="PatTotal") {
					//Totals--------------------------------------------------------------------------------
					if(!SimpleStatement) {
						for(int iCol=3;iCol<11;iCol++) {
							g.DrawString(StatementA[famsPrinted][iCol,linesPrinted]
								,TotalFont,Brushes.Black,new RectangleF(
								colPos[iCol+1]
								-g.MeasureString(StatementA[famsPrinted][iCol,linesPrinted],TotalFont).Width-1,yPos
								,colPos[iCol+1]-colPos[iCol]+8,TotalFont.GetHeight(g)));
						}
					}
					yPos+=TotalFont.GetHeight(g);
				}
				else {
					//Body data--------------------------------------------------------------------------------
					if(isFirstLineOnPage) {
						//g.DrawLine(new Pen(Color.Gray),colPos[0],yPos,colPos[11],yPos);
					}
					//description column determines height of row
					rowHeight=g.MeasureString(StatementA[famsPrinted][3,linesPrinted],bodyFont,colPos[3+1]-colPos[3]+6).Height;
					//bodyFont.GetHeight(g);
					for(int i=0;i<11;i++) {
						//left line for this cell
						g.DrawLine(new Pen(Color.Gray),colPos[i],yPos,colPos[i],yPos+rowHeight);
						if(i==10) {//if this is the right column, then also draw line for right side of cell
							g.DrawLine(new Pen(Color.Gray),colPos[i+1],yPos,colPos[i+1],yPos+rowHeight);
						}
						//bottom line for this cell
						g.DrawLine(new Pen(Color.LightGray),colPos[i],yPos+rowHeight,colPos[i+1],yPos+rowHeight);
						//if new date, then print dark line above
						if(linesPrinted>0 && StatementA[famsPrinted][0,linesPrinted] != StatementA[famsPrinted][0,linesPrinted-1]) {
							g.DrawLine(new Pen(Color.Black,1.5f),colPos[i],yPos,colPos[i+1],yPos);
						}
						if(colAlign[i]==HorizontalAlignment.Right) {
							g.DrawString(StatementA[famsPrinted][i,linesPrinted]
								,bodyFont,Brushes.Black,new RectangleF(
								colPos[i+1]-g.MeasureString(StatementA[famsPrinted][i,linesPrinted],bodyFont).Width+1,//x
								yPos,//y
								colPos[i+1]-colPos[i]+8,//w
								rowHeight));//h
						}
						else {
							g.DrawString(StatementA[famsPrinted][i,linesPrinted]
								,bodyFont,Brushes.Black,new RectangleF(
								colPos[i],yPos
								,colPos[i+1]-colPos[i]+6,rowHeight));
						}
						if(StatementA[famsPrinted][11,linesPrinted+1]=="PatTotal") {
							g.DrawLine(new Pen(Color.Gray),colPos[i],yPos+rowHeight,colPos[11],yPos+rowHeight);
						}
					}
					yPos+=rowHeight;
				}
				isFirstLineOnPage=false;
				linesPrinted++;
				//if(linesPrinted<StatementA[famsPrinted].GetLength(1)
				//	&& StatementA[famsPrinted][11,linesPrinted]=="GrandTotal")
				//{
				//	linesPrinted++;
				//}
			}//end while lines
			#endregion
			#region Note
			//Note----------------------------------------------------------------------------------------
			if(!notePrinted && //if note has not printed
				linesPrinted==StatementA[famsPrinted].GetLength(1))//and all table data already printed
			{
				if(Notes[famsPrinted]=="") {
					notePrinted=true;
				}
				else {
					float noteHeight=g.MeasureString(Notes[famsPrinted],bodyFont,colPos[11]-colPos[0]).Height;
					if(noteHeight<ev.MarginBounds.Bottom-yPos) {//if there is room
						g.DrawString(Notes[famsPrinted],bodyFont,Brushes.Black,new RectangleF(colPos[0],yPos
							,colPos[11]-colPos[0],noteHeight));
						notePrinted=true;
					}
					//otherwise, pagesPrinted will increment and 
				}
			}
			#endregion
			#region SwissBanking
			if(CultureInfo.CurrentCulture.Name.EndsWith("CH")//CH is for switzerland. eg de-CH
				&& pagesPrinted==0)//only on the first page
			{
				float yred=744;//768;//660 for testing
				//Red line (just temp)
				//g.DrawLine(Pens.Red,0,yred,826,yred);
				Font swfont=new Font(FontFamily.GenericSansSerif,10);
				//Bank Address---------------------------------------------------------
				g.DrawString(PrefB.GetString("BankAddress"),swfont,Brushes.Black,30,yred+30);
				g.DrawString(PrefB.GetString("BankAddress"),swfont,Brushes.Black,246,yred+30);
				//Office Name and Address----------------------------------------------
				text=PrefB.GetString("PracticeTitle")+"\r\n"
					+PrefB.GetString("PracticeAddress")+"\r\n";
				if(PrefB.GetString("PracticeAddress2")!="") {
					text+=PrefB.GetString("PracticeAddress2")+"\r\n";
				}
				text+=PrefB.GetString("PracticeZip")+" "+PrefB.GetString("PracticeCity");
				g.DrawString(text,swfont,Brushes.Black,30,yred+89);
				g.DrawString(text,swfont,Brushes.Black,246,yred+89);
				//Bank account number--------------------------------------------------
				string origBankNum=PrefB.GetString("PracticeBankNumber");//must be exactly 9 digits. 2+6+1.
				//the 6 digit portion might have 2 leading 0's which would not go into the dashed bank num.
				string dashedBankNum="?";
				//examples: 01-200027-2
				//          01-4587-1  (from 010045871)
				if(origBankNum.Length==9) {
					dashedBankNum=origBankNum.Substring(0,2)+"-"
						+origBankNum.Substring(2,6).TrimStart(new char[] { '0' })+"-"
						+origBankNum.Substring(8,1);
				}
				swfont=new Font(FontFamily.GenericSansSerif,9,FontStyle.Bold);
				g.DrawString(dashedBankNum,swfont,Brushes.Black,95,yred+169);
				g.DrawString(dashedBankNum,swfont,Brushes.Black,340,yred+169);
				//Amount------------------------------------------------------------
				double amountdue=PatGuar.BalTotal-PatGuar.InsEst;
				text=amountdue.ToString("F2");
				text=text.Substring(0,text.Length-3);
				StringFormat format=new StringFormat();
				format.LineAlignment=StringAlignment.Near;
				format.Alignment=StringAlignment.Far;
				swfont=new Font(FontFamily.GenericSansSerif,10);
				g.DrawString(text,swfont,Brushes.Black,new Rectangle(50,(int)yred+205,100,25),format);
				g.DrawString(text,swfont,Brushes.Black,new Rectangle(290,(int)yred+205,100,25),format);
				text=amountdue.ToString("F2");//eg 92.00
				text=text.Substring(text.Length-2,2);//eg 00
				g.DrawString(text,swfont,Brushes.Black,185,yred+205);
				g.DrawString(text,swfont,Brushes.Black,425,yred+205);
				//Patient Address-----------------------------------------------------
				string patAddress=PatGuar.FName+" "+PatGuar.LName+"\r\n"
					+PatGuar.Address+"\r\n";
				if(PatGuar.Address2!="") {
					patAddress+=PatGuar.Address2+"\r\n";
				}
				patAddress+=PatGuar.Zip+" "+PatGuar.City;
				g.DrawString(patAddress,swfont,Brushes.Black,495,yred+218);//middle left
				g.DrawString(patAddress,swfont,Brushes.Black,30,yred+263);//Lower left
				//Compute Reference#------------------------------------------------------
				//Reference# has exactly 27 digits
				//First 6 numbers are what we are calling the BankRouting number.
				//Next 20 numbers represent the invoice #.
				//27th number is the checksum
				string referenceNum=PrefB.GetString("BankRouting");//6 digits
				if(referenceNum.Length!=6) {
					referenceNum="000000";
				}
				referenceNum+=PatGuar.PatNum.ToString().PadLeft(12,'0')
					//"000000000000"//12 0's
					+DateTime.Today.ToString("yyyyMMdd");//+8=20
				//for testing:
				//referenceNum+="09090271100000067534";
				//"00000000000000037112";
				referenceNum+=Modulo10(referenceNum).ToString();
				//at this point, the referenceNum will always be exactly 27 digits long.
				string spacedRefNum=referenceNum.Substring(0,2)+" "+referenceNum.Substring(2,5)+" "+referenceNum.Substring(7,5)
					+" "+referenceNum.Substring(12,5)+" "+referenceNum.Substring(17,5)+" "+referenceNum.Substring(22,5);
				//text=spacedRefNum.Substring(0,15)+"\r\n"+spacedRefNum.Substring(16)+"\r\n";
				//reference# at lower left above address.  Small
				swfont=new Font(FontFamily.GenericSansSerif,7);
				g.DrawString(spacedRefNum,swfont,Brushes.Black,30,yred+243);
				//Reference# at upper right---------------------------------------------------------------
				swfont=new Font(FontFamily.GenericSansSerif,10);
				g.DrawString(spacedRefNum,swfont,Brushes.Black,490,yred+140);
				//Big long number at the lower right--------------------------------------------------
				/The very long number on the bottom has this format:
				>13 numbers > 27 numbers + 9 numbers >
				>Example: 0100000254306>904483000000000000000371126+ 010045871>
				>
				>The first group of 13 numbers would begin with either 01 or only have 
				>042 without any other following numbers.  01 would be used if there is 
				>a specific amount, and 042 would be used if there is not a specific 
				>amount billed. So in the example, the billed amount is 254.30.  It has 
				>01 followed by leading zeros to fill in the balance of the digits 
				>required.  The last digit is a checksum done by the program.  If the 
				>amount would be 1,254.30 then the example should read 0100001254306.
				>
				>There is a > separator, then the reference number made up previously.
				>
				>Then a + separator, followed by the bank account number.  Previously, 
				>the number printed without the zeros, but in this case it has the zeros 
				>and not the dashes.
				swfont=new Font("OCR-B 10 BT",12);
				text="01"+amountdue.ToString("F2").Replace(".","").PadLeft(10,'0');
				text+=Modulo10(text).ToString()+">"
					+referenceNum+"+ "+origBankNum+">";
				g.DrawString(text,swfont,Brushes.Black,255,yred+345);
			}
			#endregion SwissBanking
			if(linesPrinted<StatementA[famsPrinted].GetLength(1)) {//if this family is not done printing
				ev.HasMorePages=true;
				pagesPrinted++;
				totalPages++;
			}
			else {//family is done printing
				pagesPrinted=0;
				linesPrinted=0;
				notePrinted=false;
				totalPages++;
				famsPrinted++;
				if(famsPrinted<StatementA.GetLength(0)) {//if more families to print
					ev.HasMorePages=true;
				}
				else {//completely done
					ev.HasMorePages=false;
					labelTotPages.Text="1 / "+totalPages.ToString();
					famsPrinted=0;
					pagesPrinted=0;
				}
			}*/
		}

		///<summary>data may only contain numbers between 0 und 9</summary>
		private int Modulo10(string strNumber){
			//try{
				int[] intTable={0,9,4,6,8,2,7,1,3,5};
				int intTransfer=0;
				for(int intIndex=0;intIndex<strNumber.Length;intIndex++){
					int digit=Convert.ToInt32(strNumber.Substring(intIndex,1));
					int modulus=(intTransfer+digit) % 10;
					intTransfer=intTable[modulus];
				}
				return (10-intTransfer) % 10;
			//}
			//catch{
			//	return 0;
			//}
    }


		private void butZoomIn_Click(object sender, System.EventArgs e) {
			butFullPage.Visible=true;
			butZoomIn.Visible=false;
			printPreviewControl2.Zoom=1;		
		}

		private void butBack_Click(object sender, System.EventArgs e) {
			if(printPreviewControl2.StartPage==0) 
				return;
			printPreviewControl2.StartPage--;
			labelTotPages.Text=(printPreviewControl2.StartPage+1).ToString()
				+" / "+totalPages.ToString();	
		}

		private void butFwd_Click(object sender, System.EventArgs e) {
			if(printPreviewControl2.StartPage==totalPages-1) return;
			printPreviewControl2.StartPage++;
			labelTotPages.Text=(printPreviewControl2.StartPage+1).ToString()
				+" / "+totalPages.ToString();		
		}

		private void butPrint_Click(object sender, System.EventArgs e) {
			//just for debugging
			/*PrintReport(false);
			DialogResult=DialogResult.Cancel;*/			
		}

		private void butFullPage_Click(object sender, System.EventArgs e) {
			butFullPage.Visible=false;
			butZoomIn.Visible=true;
			printPreviewControl2.Zoom=((double)printPreviewControl2.ClientSize.Height
				/(double)pd2.DefaultPageSettings.PaperSize.Height);	
		}
	}
}
