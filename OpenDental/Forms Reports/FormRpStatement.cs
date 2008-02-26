using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.Imaging;
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
		///<summary>Holds the data for one statement.</summary>
		private DataSet dataSet;
		private Statement Stmt;
		private IImageStore imageStore;

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
			this.butPrint.Visible = false;
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
			this.butZoomIn.Visible = false;
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
			this.Text = "Statement Preview";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.FormRpStatement_Load);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.FormRpStatement_Layout);
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

		/*
		///<summary>Used from FormBilling to print all statements for all the supplied patNums.</summary>
		public void LoadAndPrint(int[] guarNums,string generalNote){
			int[][] patNums=new int[guarNums.Length][];
			Family famCur;
			ArrayList numsFam;
			string[] notes=new string[guarNums.Length];
			Dunning[] dunList=Dunnings.Refresh();
			int ageAccount=0;
			YN insIsPending=YN.Unknown;
			Dunning dunning;
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
				dunning=Dunnings.GetDunning(dunList,famCur.List[0].BillingType,ageAccount,insIsPending);
				if(dunning!=null){
					notes[i]+=dunning.DunMessage;
					//also messageBold needs to be addressed.
				}
				if(notes[i]!="" && generalNote!=""){
					notes[i]+="\r\n\r\n";//Space two lines apart
				}
				notes[i]+=generalNote;
			}
			//PrintStatements(patNums,DateTime.Today.AddDays(-45),DateTime.Today,true,false,false,false,notes,true,"");
			//PrintStatements();
		}*/

		///<summary>Creates a new pdf, attaches it to a new doc, and attaches that to the statement.  If it cannot create a pdf, for example if no AtoZ folders, then it will simply result in a docnum of zero, so no attached doc.</summary>
		public void CreateStatementPdf(Statement stmt){
			Stmt=stmt;
			dataSet=AccountModule.GetStatement(stmt.PatNum,stmt.SinglePatient,stmt.DateRangeFrom,stmt.DateRangeTo,
				stmt.Intermingled);
			if(ImageStore.UpdatePatient == null){
				ImageStore.UpdatePatient = new FileStore.UpdatePatientDelegate(Patients.Update);
			}
			Patient pat=Patients.GetPat(stmt.PatNum);
			imageStore = ImageStore.GetImageStore(pat);
			//Save to a temp pdf--------------------------------------------------------------------------
			string tempPath=CodeBase.ODFileUtils.CombinePaths(Path.GetTempPath(),pat.PatNum.ToString()+".pdf");
			PrintDocument pd=new PrintDocument();
			pd.DefaultPageSettings.Margins=new Margins(40,40,40,60);
			if(CultureInfo.CurrentCulture.Name.EndsWith("CH")) {//CH is for switzerland. eg de-CH
				//leave a big margin on the bottom for the routing slip
				pd.DefaultPageSettings.Margins=new Margins(40,40,40,440);//4.4" from bottom
			}
			//pd.OriginAtMargins=true;
			if(pd.DefaultPageSettings.PaperSize.Height==0) {
				pd.DefaultPageSettings.PaperSize=new PaperSize("default",850,1100);
			}
			MigraDoc.DocumentObjectModel.Document doc=CreateDocument(pd);
			MigraDoc.Rendering.PdfDocumentRenderer pdfRenderer=new MigraDoc.Rendering.PdfDocumentRenderer(true,PdfFontEmbedding.Always);
			pdfRenderer.Document=doc;
			pdfRenderer.RenderDocument();
			pdfRenderer.PdfDocument.Save(tempPath);
			//get the category-----------------------------------------------------------------------------
			int category=0;
			for(int i=0;i<DefB.Short[(int)DefCat.ImageCats].Length;i++){
				if(Regex.IsMatch(DefB.Short[(int)DefCat.ImageCats][i].ItemValue,@"S")){
					category=DefB.Short[(int)DefCat.ImageCats][i].DefNum;
					break;
				}
			}
			if(category==0){
				category=DefB.Short[(int)DefCat.ImageCats][0].DefNum;//put it in the first category.
			}
			//create doc--------------------------------------------------------------------------------------
			OpenDentBusiness.Document docc=null;
			try {
				docc=imageStore.Import(tempPath,category);
			} 
			catch {
				MsgBox.Show(this,"Error saving document.");
				//this.Cursor=Cursors.Default;
				return;
			}
			docc.ImgType=ImageType.Document;
			docc.Description=Lan.g(this,"Statement");
			docc.DateCreated=stmt.DateSent;
			Documents.Update(docc);
			stmt.DocNum=docc.DocNum;
			Statements.WriteObject(stmt);
		}

		///<summary>Prints one statement to a specified printer which is passed in as a PrintDocument field.  Used when printer selection happens before a batch</summary>
		public void PrintStatement(Statement stmt,PrintDocument pd){
			PrintStatement(stmt,false,pd);
		}

		///<summary>Prints one statement.  Does not generate pdf or print from existing pdf.</summary>
		public void PrintStatement(Statement stmt,bool previewOnly){
			PrintDocument pd=new PrintDocument();
			if(!Printers.SetPrinter(pd,PrintSituation.Statement)){
				return;
			}
			PrintStatement(stmt,previewOnly,pd);
		}

		///<summary>Prints one statement.  Does not generate pdf or print from existing pdf.</summary>
		public void PrintStatement(Statement stmt,bool previewOnly,PrintDocument pd){
			Stmt=stmt;
			dataSet=AccountModule.GetStatement(stmt.PatNum,stmt.SinglePatient,stmt.DateRangeFrom,stmt.DateRangeTo,
				stmt.Intermingled);
			pd.DefaultPageSettings.Margins=new Margins(40,40,40,60);
			if(CultureInfo.CurrentCulture.Name.EndsWith("CH")) {//CH is for switzerland. eg de-CH
				//leave a big margin on the bottom for the routing slip
				pd.DefaultPageSettings.Margins=new Margins(40,40,40,440);//4.4" from bottom
			}
			//pd.OriginAtMargins=true;
			if(pd.DefaultPageSettings.PaperSize.Height==0) {
				pd.DefaultPageSettings.PaperSize=new PaperSize("default",850,1100);
			}
			MigraDoc.DocumentObjectModel.Document doc=CreateDocument(pd);
			MigraDoc.Rendering.Printing.MigraDocPrintDocument printdoc=new MigraDoc.Rendering.Printing.MigraDocPrintDocument();
			MigraDoc.Rendering.DocumentRenderer renderer=new MigraDoc.Rendering.DocumentRenderer(doc);
			renderer.PrepareDocument();
			totalPages=renderer.FormattedDocument.PageCount;
			labelTotPages.Text="1 / "+totalPages.ToString();
			printdoc.Renderer=renderer;
			printdoc.PrinterSettings=pd.PrinterSettings;
			if(previewOnly){
				printPreviewControl2.Document=printdoc;
			}
			else{
				try{
					printdoc.Print();
				}
				catch{
					MessageBox.Show(Lan.g(this,"Printer not available"));
				}
			}
		}

		/*
		/// <summary>Gets one FamilyStatementData for a single family.</summary>
		private FamilyStatementData AssembleStatement(int[] famPatNums,DateTime fromDate,DateTime toDate,bool includeClaims,bool nextAppt){
			FamilyStatementData retVal=new FamilyStatementData();
			retVal.GuarNum=famPatNums[0];
			PatStatementAbout patAbout;
			PatStatementData patData;
			for(int i=0;i<famPatNums.Length;i++){
				patAbout=new PatStatementAbout();
				patAbout.PatNum=famPatNums[i];
			//contrAccount.RefreshModuleData(famPatNums[i]);
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
		}*/

		/*
		private void GetPatGuar(int patNum){
			if(PatGuar!=null
				&& patNum==PatGuar.PatNum){//if PatGuar is already set
				return;
			}
			//but if the guarantor is not on the list of patients in the fam to print, it will also refresh
      Family FamCur=Patients.GetFamily(patNum);
			PatGuar=FamCur.List[0].Copy();
		}*/

		///<summary>Supply pd so that we know the paper size and margins.</summary>
		private MigraDoc.DocumentObjectModel.Document CreateDocument(PrintDocument pd){
			MigraDoc.DocumentObjectModel.Document doc= new MigraDoc.DocumentObjectModel.Document();
			doc.DefaultPageSetup.PageWidth=Unit.FromInch((double)pd.DefaultPageSettings.PaperSize.Width/100);
			doc.DefaultPageSetup.PageHeight=Unit.FromInch((double)pd.DefaultPageSettings.PaperSize.Height/100);
			doc.DefaultPageSetup.TopMargin=Unit.FromInch((double)pd.DefaultPageSettings.Margins.Top/100);
			doc.DefaultPageSetup.LeftMargin=Unit.FromInch((double)pd.DefaultPageSettings.Margins.Left/100);
			doc.DefaultPageSetup.RightMargin=Unit.FromInch((double)pd.DefaultPageSettings.Margins.Right/100);
			doc.DefaultPageSetup.BottomMargin=Unit.FromInch((double)pd.DefaultPageSettings.Margins.Bottom/100);
			MigraDoc.DocumentObjectModel.Section section=doc.AddSection();//so that Swiss will have different footer for each patient.
			string text;
			MigraDoc.DocumentObjectModel.Font font;
			//GetPatGuar(PatNums[famIndex][0]);
			Family fam=Patients.GetFamily(Stmt.PatNum);
			Patient PatGuar=fam.List[0];//.Copy();
			//HEADING------------------------------------------------------------------------------
			#region Heading
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
			#endregion
			//Practice Address----------------------------------------------------------------------
			#region Practice Address
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
			#endregion
			//AMOUNT ENCLOSED------------------------------------------------------------------------------------------------------
			#region Amount Enclosed
			Table table;
			Column col;
			Row row;
			Cell cell;
			frame=MigraDocHelper.CreateContainer(section,450,110,330,29);
			if(!Stmt.HidePayment) {
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
			#endregion
			//Credit Card Info--------------------------------------------------------------------------------------------------------
			#region Credit Card Info
			if(!Stmt.HidePayment) {
				if(PrefB.GetBool("StatementShowCreditCard")) {
					float yPos=60;
					font=MigraDocHelper.CreateFont(7,true);
					text=Lan.g(this,"CREDIT CARD TYPE");
					MigraDocHelper.DrawString(frame,text,font,0,yPos);
					float rowHeight=26;
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
					text=Lan.g(this,"3 DIGIT CSV");
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
			#endregion
			//Patient's Billing Address---------------------------------------------------------------------------------------------
			#region Patient Billing Address
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
			if(!Stmt.HidePayment) {
				MigraDocHelper.DrawLine(frame,System.Drawing.Color.LightGray,0,0,850,0);
				text=Lan.g(this,"PLEASE DETACH AND RETURN THE UPPER PORTION WITH YOUR PAYMENT");
				font=MigraDocHelper.CreateFont(6,true,System.Drawing.Color.Gray);
				par=frame.AddParagraph();
				par.Format.Alignment=ParagraphAlignment.Center;
				par.Format.Font=font;
				par.AddText(text);
			}
			#endregion
			//Aging-----------------------------------------------------------------------------------
			#region Aging
			MigraDocHelper.InsertSpacer(section,275);
			frame = MigraDocHelper.CreateContainer(section, 55, 390, 250, 29);
			if (!Stmt.HidePayment)
			{
				table = MigraDocHelper.DrawTable(frame, 0, 0, 29);
				col = table.AddColumn(Unit.FromInch(1.1));
				col = table.AddColumn(Unit.FromInch(1.1));
				col = table.AddColumn(Unit.FromInch(1.1));
				col = table.AddColumn(Unit.FromInch(1.1));
				row = table.AddRow();
				row.Format.Alignment = ParagraphAlignment.Center;
				row.Borders.Color = Colors.Black;
				row.Shading.Color = Colors.LightGray;
				row.TopPadding = Unit.FromInch(0);
				row.BottomPadding = Unit.FromInch(0);
				font = MigraDocHelper.CreateFont(8, true);
				cell = row.Cells[0];
				par = cell.AddParagraph();
				par.AddFormattedText(Lan.g(this, "0-30"), font);
				cell = row.Cells[1];
				par = cell.AddParagraph();
				par.AddFormattedText(Lan.g(this, "31-60"), font);
				cell = row.Cells[2];
				par = cell.AddParagraph();
				par.AddFormattedText(Lan.g(this, "61-90"), font);
				cell = row.Cells[3];
				par = cell.AddParagraph();
				par.AddFormattedText(Lan.g(this, "over 90"), font);
				row = table.AddRow();
				row.Format.Alignment = ParagraphAlignment.Center;
				row.Borders.Left.Color = Colors.Gray;
				row.Borders.Bottom.Color = Colors.Gray;
				row.Borders.Right.Color = Colors.Gray;
				font = MigraDocHelper.CreateFont(9);
				text= PatGuar.Bal_0_30.ToString("F");
				cell = row.Cells[0];
				par = cell.AddParagraph();
				par.AddFormattedText(text, font);
				text = PatGuar.Bal_31_60.ToString("F");
				cell = row.Cells[1];
				par = cell.AddParagraph();
				par.AddFormattedText(text, font);
				text = PatGuar.Bal_61_90.ToString("F");
				cell = row.Cells[2];
				par = cell.AddParagraph();
				par.AddFormattedText(text, font);
				text = PatGuar.BalOver90.ToString("F");
				cell = row.Cells[3];
				par = cell.AddParagraph();
				par.AddFormattedText(text, font);
			}
			/*
			ODGridColumn gcol;
			ODGridRow grow;
			if(!Stmt.HidePayment) {
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
				if(PrefB.GetBool("BalancesDontSubtractIns")) {//less common
					gcol=new ODGridColumn(Lan.g(this,"Balance"),70,HorizontalAlignment.Center);
					gridAging.Columns.Add(gcol);
					gcol=new ODGridColumn(Lan.g(this,"InsPending"),70,HorizontalAlignment.Center);
					gridAging.Columns.Add(gcol);
					gcol=new ODGridColumn(Lan.g(this,"AfterIns"),70,HorizontalAlignment.Center);
					gridAging.Columns.Add(gcol);
				}
				else{//more common
					gcol=new ODGridColumn(Lan.g(this,"Total"),70,HorizontalAlignment.Center);
					gridAging.Columns.Add(gcol);
					gcol=new ODGridColumn(Lan.g(this,"- InsEst"),70,HorizontalAlignment.Center);
					gridAging.Columns.Add(gcol);
					gcol=new ODGridColumn(Lan.g(this,"= Balance"),70,HorizontalAlignment.Center);
					gridAging.Columns.Add(gcol);
				}
				gridAging.Rows.Clear();
				//Annual max--------------------------
				grow=new ODGridRow();
				grow.Cells.Add(PatGuar.Bal_0_30.ToString("F"));
				grow.Cells.Add(PatGuar.Bal_31_60.ToString("F"));
				grow.Cells.Add(PatGuar.Bal_61_90.ToString("F"));
				grow.Cells.Add(PatGuar.BalOver90.ToString("F"));
				grow.Cells.Add(PatGuar.BalTotal.ToString("F"));
				grow.Cells.Add(PatGuar.InsEst.ToString("F"));
				grow.Cells.Add((PatGuar.BalTotal-PatGuar.InsEst).ToString("F"));
				gridAging.Rows.Add(grow);
				gridAging.EndUpdate();
				MigraDocHelper.DrawGrid(section,gridAging);
				gridAging.Dispose();
			*/
			#endregion
			//Floating Balance, Ins info-------------------------------------------------------------------
			#region FloatingBalance
			
			frame = MigraDocHelper.CreateContainer(section,455,380,250,200);					
			//draw backgrounds first so the text always makes it on top (seems to mess it up if I do it in the same context as the text)
			//so unless someone can figure out how to combine them, we will just have to run two separate if structures
			if(PrefB.GetBool("StatementSummaryShowInsInfo")){
				if(PatGuar.HasIns=="I"){
					if (PrefB.GetBool("BalancesDontSubtractIns")){
						MigraDocHelper.FillRectangle(frame,System.Drawing.Color.LightGray,57,1,280,16);
					}
					else{
						MigraDocHelper.FillRectangle(frame,System.Drawing.Color.LightGray,57,36,280,18);
					}
				}
				else{
					MigraDocHelper.FillRectangle(frame,System.Drawing.Color.LightGray,57,1,280,16);			
				}
			}
			else{
				MigraDocHelper.FillRectangle(frame,System.Drawing.Color.LightGray,57,18,280,18);
			}
			
			//These are the lables for the floating blance info (left frame of the two)
			frame = MigraDocHelper.CreateContainer(section,455,380,250,200);		
			par = frame.AddParagraph();
			parformat = new ParagraphFormat();
			parformat.Alignment = ParagraphAlignment.Right;
			par.Format = parformat;
			font = MigraDocHelper.CreateFont(11, true);
			if(PrefB.GetBool("StatementSummaryShowInsInfo"))//Show balance with ins estimate and est balance
			{
				text = Lan.g(this,"Current account balance:");
				par.AddFormattedText(text,font);
				par.AddLineBreak();
				if(PatGuar.HasIns=="I"){//Pt has ins
					if (PrefB.GetBool("BalancesDontSubtractIns")){
						font = MigraDocHelper.CreateFont(11,false);
						text = Lan.g(this,"Insurance pending:");
						par.AddFormattedText(text,font);
						par.AddLineBreak();
						text = Lan.g(this,"Estimate after insurance:");
					} 
					else{
						text = Lan.g(this,"Insurance pending:");
						par.AddFormattedText(text,font);
						par.AddLineBreak();
						text = Lan.g(this,"What you owe now:");
					}
					par.AddFormattedText(text, font);
					par.AddLineBreak();
				}
				else{//pt does not have ins listed on account
					frame = MigraDocHelper.CreateContainer(section,550,400,350,200);
					par = frame.AddParagraph();
					font = MigraDocHelper.CreateFont(9,false,System.Drawing.Color.DarkGray);
					text = Lan.g(this,"No insurance associated with account.");
					par.AddFormattedText(text,font);
					frame = MigraDocHelper.CreateContainer(section,560,412,350,200);
					par = frame.AddParagraph();
					text = Lan.g(this,"Please contact us if this is incorrect.");
					par.AddFormattedText(text,font);
					
					font = MigraDocHelper.CreateFont(11,true,System.Drawing.Color.Black);
					
				}
			}
			else{//Just show balance, aligned to the middle
				font = MigraDocHelper.CreateFont(11,true);
				text = Lan.g(this,"Current account balance:");
				par.AddLineBreak();
				par.AddFormattedText(text,font);
				par.AddLineBreak();

			}
			
			//if payplan exists, may want to add something in here
			
			//this is for the amts on the floating balance (right frame of the two)
			frame = MigraDocHelper.CreateContainer(section, 708, 380, 100, 200);
			par = frame.AddParagraph();
			parformat = new ParagraphFormat();
			parformat.Alignment = ParagraphAlignment.Left;
			par.Format = parformat;
			font = MigraDocHelper.CreateFont(11, true);
			if(PrefB.GetBool("StatementSummaryShowInsInfo"))//Show balance with ins estimate and est balance
			{
				text = PatGuar.BalTotal.ToString("c");
				par.AddFormattedText(text, font);
				par.AddLineBreak();
				if(PatGuar.HasIns=="I"){
					if(PrefB.GetBool("BalancesDontSubtractIns")){
						font = MigraDocHelper.CreateFont(11,false);
					}
					text = PatGuar.InsEst.ToString("c");
					par.AddFormattedText(text, font);
					par.AddLineBreak();
					//same $, just different label on this line with "BalancesDontSubtractIns" and not bold
						text = (PatGuar.BalTotal - PatGuar.InsEst).ToString("c");
						par.AddFormattedText(text, font);
						par.AddLineBreak();
				}
				else{//nothing else needed if pt does not have ins
				
				}
			}
			else{//only show balance in the middle as no ins info is displayed
				par.AddLineBreak();
				text = PatGuar.BalTotal.ToString("c");
				par.AddFormattedText(text,font);
				par.AddLineBreak();
			}
			MigraDocHelper.InsertSpacer(section, 80);//spacer to put main grid in the right location
			//TextFrame frame;
			#endregion
			//Bold note-------------------------------------------------------------------------------
			if(Stmt.NoteBold!=""){
				MigraDocHelper.InsertSpacer(section,7);
				font=MigraDocHelper.CreateFont(10,true,System.Drawing.Color.DarkRed);
				par=section.AddParagraph();
				par.Format.Font=font;
				par.AddText(Stmt.NoteBold);
				MigraDocHelper.InsertSpacer(section,8);
			}
			//Body Tables-----------------------------------------------------------------------------------------------------------
			ODGridColumn gcol;
			ODGridRow grow;
			ODGrid gridPat = new ODGrid();
			this.Controls.Add(gridPat);
			gridPat.BeginUpdate();
			gridPat.Columns.Clear();
			gcol=new ODGridColumn(Lan.g(this,"Date"),73);
			gridPat.Columns.Add(gcol);
			gcol=new ODGridColumn(Lan.g(this,"Patient"),100);
			gridPat.Columns.Add(gcol);
			//prov
			gcol=new ODGridColumn(Lan.g(this,"Code"),45);
			gridPat.Columns.Add(gcol);
			gcol=new ODGridColumn(Lan.g(this,"Tooth"),42);
			gridPat.Columns.Add(gcol);
			gcol=new ODGridColumn(Lan.g(this,"Description"),270);
			gridPat.Columns.Add(gcol);
			gcol=new ODGridColumn(Lan.g(this,"Charges"),60,HorizontalAlignment.Right);
			gridPat.Columns.Add(gcol);
			gcol=new ODGridColumn(Lan.g(this,"Credits"),60,HorizontalAlignment.Right);
			gridPat.Columns.Add(gcol);
			gcol=new ODGridColumn(Lan.g(this,"Balance"),60,HorizontalAlignment.Right);
			gridPat.Columns.Add(gcol);
			gridPat.Width=gridPat.WidthAllColumns+20;
			gridPat.EndUpdate();
			//Loop through each table.  Could be one intermingled, or one for each patient-----------------------------------------
			DataTable tableAccount;
			string tablename;
			int patnum;
			for(int i=0;i<dataSet.Tables.Count;i++){
				tableAccount=dataSet.Tables[i];
				tablename=tableAccount.TableName;
				if(!tablename.StartsWith("account")){
					continue;
				}
				par=section.AddParagraph();
				par.Format.Font=MigraDocHelper.CreateFont(10,true);
				par.Format.SpaceBefore=Unit.FromInch(.05);
				par.Format.SpaceAfter=Unit.FromInch(.05);
				patnum=0;
				if(tablename!="account"){//account123 etc.
					patnum=PIn.PInt(tablename.Substring(7));
				}
				if(patnum!=0){
					par.AddText(fam.GetNameInFamFL(patnum));
				}
				//if(FamilyStatementDataList[famIndex].PatAboutList[i].ApptDescript!=""){
				//	par=section.AddParagraph();
				//	par.Format.Font=MigraDocHelper.CreateFont(9);//same as body font
				//	par.AddText(FamilyStatementDataList[famIndex].PatAboutList[i].ApptDescript);
				//}
				gridPat.BeginUpdate();
				gridPat.Rows.Clear();
				//lineData=FamilyStatementDataList[famIndex].PatDataList[i].PatData;
				for(int p=0;p<tableAccount.Rows.Count;p++){
					grow=new ODGridRow();
					grow.Cells.Add(tableAccount.Rows[p]["date"].ToString());
					grow.Cells.Add(tableAccount.Rows[p]["patient"].ToString());
					grow.Cells.Add(tableAccount.Rows[p]["ProcCode"].ToString());
					grow.Cells.Add(tableAccount.Rows[p]["tth"].ToString());
					grow.Cells.Add(tableAccount.Rows[p]["description"].ToString());
					grow.Cells.Add(tableAccount.Rows[p]["charges"].ToString());
					grow.Cells.Add(tableAccount.Rows[p]["credits"].ToString());
					grow.Cells.Add(tableAccount.Rows[p]["balance"].ToString());
					gridPat.Rows.Add(grow);
				}
				gridPat.EndUpdate();
				MigraDocHelper.DrawGrid(section,gridPat);
				//Total
				frame=MigraDocHelper.CreateContainer(section);
				font=MigraDocHelper.CreateFont(9,true);
				float totalPos=((float)(doc.DefaultPageSetup.PageWidth.Inch//-doc.DefaultPageSetup.LeftMargin.Inch
					//-doc.DefaultPageSetup.RightMargin.Inch)
					)*100f)/2f+(float)gridPat.WidthAllColumns/2f+7;
				RectangleF rectF=new RectangleF(0,0,totalPos,16);
				if(patnum!=0){
					MigraDocHelper.DrawString(frame," ",
						//I decided this was unnecessary:
						//dataSet.Tables["patient"].Rows[fam.GetIndex(patnum)]["balance"].ToString(),
						font,rectF,ParagraphAlignment.Right);
					//MigraDocHelper.DrawString(frame,FamilyStatementDataList[famIndex].PatAboutList[i].Balance.ToString("F"),font,rectF,
					//	ParagraphAlignment.Right);
				}
			}
			gridPat.Dispose();
			//Future appointments---------------------------------------------------------------------------------------------
			font=MigraDocHelper.CreateFont(9);
			DataTable tableAppt=dataSet.Tables["appts"];
			if(tableAppt.Rows.Count>0){
				par=section.AddParagraph();
				par.Format.Font=font;
				par.AddText(Lan.g(this,"Scheduled Appointments:"));
			}
			for(int i=0;i<tableAppt.Rows.Count;i++){
				par.AddLineBreak();
				par.AddText(tableAppt.Rows[i]["descript"].ToString());
			}
			if(tableAppt.Rows.Count>0){
				MigraDocHelper.InsertSpacer(section,10);
			}
			//Note------------------------------------------------------------------------------------------------------------
			font=MigraDocHelper.CreateFont(9);
			par=section.AddParagraph();
			par.Format.Font=font;
			par.AddText(Stmt.Note);
			//bold note
			if(Stmt.NoteBold!=""){
				MigraDocHelper.InsertSpacer(section,10);
				font=MigraDocHelper.CreateFont(10,true,System.Drawing.Color.DarkRed);
				par=section.AddParagraph();
				par.Format.Font=font;
				par.AddText(Stmt.NoteBold);
			}
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
			return doc;
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
