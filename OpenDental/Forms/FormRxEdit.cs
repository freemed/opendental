using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Drawing.Printing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormRxEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.TextBox textSig;
		private System.Windows.Forms.TextBox textDisp;
		private System.Windows.Forms.TextBox textRefills;
		private System.Windows.Forms.TextBox textDrug;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private OpenDental.ValidDate textDate;
		private System.ComponentModel.Container components = null;// Required designer variable.
		private System.Windows.Forms.ListBox listProv;
		private System.Windows.Forms.Label label7;
		private System.Drawing.Printing.PrintDocument pd2;
		///<summary></summary>
		public bool IsNew;
		private OpenDental.UI.Button butPrint;
		private OpenDental.UI.Button butDelete;
		private OpenDental.ODtextBox textNotes;
		///<summary></summary>
    public FormRpPrintPreview pView = new FormRpPrintPreview();
		private Patient PatCur;
		//private User user;
		private RxPat RxPatCur;

		///<summary></summary>
		public FormRxEdit(Patient patCur,RxPat rxPatCur){
			//){//
			InitializeComponent();
			RxPatCur=rxPatCur;
			PatCur=patCur;
			Lan.F(this);
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if(components != null){
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRxEdit));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.textSig = new System.Windows.Forms.TextBox();
			this.textDisp = new System.Windows.Forms.TextBox();
			this.textRefills = new System.Windows.Forms.TextBox();
			this.textDrug = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textDate = new OpenDental.ValidDate();
			this.listProv = new System.Windows.Forms.ListBox();
			this.label7 = new System.Windows.Forms.Label();
			this.pd2 = new System.Drawing.Printing.PrintDocument();
			this.butPrint = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.textNotes = new OpenDental.ODtextBox();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(618,424);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 9;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(618,384);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 8;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// textSig
			// 
			this.textSig.AcceptsReturn = true;
			this.textSig.Location = new System.Drawing.Point(110,104);
			this.textSig.Multiline = true;
			this.textSig.Name = "textSig";
			this.textSig.Size = new System.Drawing.Size(254,44);
			this.textSig.TabIndex = 2;
			// 
			// textDisp
			// 
			this.textDisp.Location = new System.Drawing.Point(110,160);
			this.textDisp.Name = "textDisp";
			this.textDisp.Size = new System.Drawing.Size(112,20);
			this.textDisp.TabIndex = 3;
			// 
			// textRefills
			// 
			this.textRefills.Location = new System.Drawing.Point(110,194);
			this.textRefills.Name = "textRefills";
			this.textRefills.Size = new System.Drawing.Size(114,20);
			this.textRefills.TabIndex = 4;
			// 
			// textDrug
			// 
			this.textDrug.Location = new System.Drawing.Point(110,72);
			this.textDrug.Name = "textDrug";
			this.textDrug.Size = new System.Drawing.Size(254,20);
			this.textDrug.TabIndex = 1;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(17,108);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(89,14);
			this.label6.TabIndex = 17;
			this.label6.Text = "Sig";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(7,164);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(99,14);
			this.label5.TabIndex = 16;
			this.label5.Text = "Disp";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(7,198);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(99,14);
			this.label4.TabIndex = 15;
			this.label4.Text = "Refills";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(11,232);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(95,14);
			this.label3.TabIndex = 14;
			this.label3.Text = "Notes";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(13,74);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(93,14);
			this.label1.TabIndex = 13;
			this.label1.Text = "Drug";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(3,44);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(105,14);
			this.label2.TabIndex = 25;
			this.label2.Text = "Date";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(110,40);
			this.textDate.Name = "textDate";
			this.textDate.Size = new System.Drawing.Size(100,20);
			this.textDate.TabIndex = 0;
			// 
			// listProv
			// 
			this.listProv.Location = new System.Drawing.Point(524,28);
			this.listProv.Name = "listProv";
			this.listProv.Size = new System.Drawing.Size(120,212);
			this.listProv.TabIndex = 6;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(522,10);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(103,14);
			this.label7.TabIndex = 28;
			this.label7.Text = "Provider";
			// 
			// pd2
			// 
			this.pd2.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.pd2_PrintPage);
			// 
			// butPrint
			// 
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butPrint.Autosize = true;
			this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.CornerRadius = 4F;
			this.butPrint.Image = global::OpenDental.Properties.Resources.butPrintSmall;
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Location = new System.Drawing.Point(482,424);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(88,26);
			this.butPrint.TabIndex = 29;
			this.butPrint.Text = "&Print";
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(38,424);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(88,26);
			this.butDelete.TabIndex = 30;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// textNotes
			// 
			this.textNotes.AcceptsReturn = true;
			this.textNotes.Location = new System.Drawing.Point(110,231);
			this.textNotes.Multiline = true;
			this.textNotes.Name = "textNotes";
			this.textNotes.QuickPasteType = OpenDentBusiness.QuickPasteType.Rx;
			this.textNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNotes.Size = new System.Drawing.Size(373,111);
			this.textNotes.TabIndex = 31;
			// 
			// FormRxEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(710,472);
			this.Controls.Add(this.textNotes);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.textSig);
			this.Controls.Add(this.textDisp);
			this.Controls.Add(this.textRefills);
			this.Controls.Add(this.textDrug);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.listProv);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRxEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Rx";
			this.Load += new System.EventHandler(this.FormRxEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormRxEdit_Load(object sender, System.EventArgs e) {
			//security is handled on the Rx button click in the Chart module
			for(int i=0;i<Providers.List.Length;i++){
				this.listProv.Items.Add(Providers.List[i].Abbr);
				if(Providers.List[i].ProvNum==RxPatCur.ProvNum)
					listProv.SelectedIndex=i;
			}
			if(listProv.SelectedIndex==-1){
				listProv.SelectedIndex=0;
			}
			textDate.Text=RxPatCur.RxDate.ToString("d");
			textDrug.Text=RxPatCur.Drug;
			textSig.Text=RxPatCur.Sig;
			textDisp.Text=RxPatCur.Disp;
			textRefills.Text=RxPatCur.Refills;
			textNotes.Text=RxPatCur.Notes;
		}

		private bool SaveRx(){
			if(  textDate.errorProvider1.GetError(textDate)!=""
				//|| textAmount.errorProvider1.GetError(textAmount)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return false;
			}
			if(listProv.SelectedIndex!=-1)
				RxPatCur.ProvNum=Providers.List[listProv.SelectedIndex].ProvNum;
			RxPatCur.RxDate=PIn.PDate(textDate.Text);
			RxPatCur.Drug=textDrug.Text;
			RxPatCur.Sig=textSig.Text;
			RxPatCur.Disp=textDisp.Text;
			RxPatCur.Refills=textRefills.Text;
			RxPatCur.Notes=textNotes.Text;
			if(IsNew){
				RxPats.Insert(RxPatCur);
				//SecurityLogs.MakeLogEntry("Prescription Create",RxPats.cmd.CommandText,user);
			}
			else{
				RxPats.Update(RxPatCur);
				//SecurityLogs.MakeLogEntry("Prescription Edit",RxPats.cmd.CommandText,user);
			}
			return true;
		}

		///<summary></summary>
		public void PrintReport(){
			pd2=new PrintDocument();
			pd2.PrintPage += new PrintPageEventHandler(this.pd2_PrintPage);
			pd2.DefaultPageSettings.Margins=new Margins(0,0,0,0);
			pd2.OriginAtMargins=true;
			if(!PrefB.GetBool("RxOrientVert")){
				pd2.DefaultPageSettings.Landscape=true;
			}
			#if DEBUG
				pd2.DefaultPageSettings.PaperSize=new PaperSize("default",850,1100);
				pView.printPreviewControl2.Document=pd2;
				pView.ShowDialog();
			#else
				if(!Printers.SetPrinter(pd2,PrintSituation.Rx)){
					return;
				}
				try{
					pd2.Print();
				}
				catch{
					MessageBox.Show(Lan.g(this,"Printer not available"));
				}
			#endif		
		}

		private void pd2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e) {
			Graphics g=e.Graphics;
			Pen penDashBorder=new Pen(Color.Black,(float)(.125));
			SolidBrush brush=new SolidBrush(Color.Black);
			penDashBorder.DashStyle=DashStyle.Dot;
			int x;
			int y;
			int xAdj=(int)(PrefB.GetDouble("RxAdjustRight")*100);
			int yAdj=(int)(PrefB.GetDouble("RxAdjustDown")*100);
			string text;
			Font font=new Font(FontFamily.GenericSansSerif,8);
			Font fontRX=new Font(FontFamily.GenericSerif,24);
			Font fontBold=new Font(FontFamily.GenericSansSerif,8,FontStyle.Bold);
			int fontH=(int)font.GetHeight(g)+3;
			Provider prov=Providers.GetProv(RxPatCur.ProvNum);
			if(PrefB.GetBool("RxOrientVert")){
				g.DrawLine(penDashBorder,0+xAdj,0+yAdj,425+xAdj,0+yAdj);
				g.DrawLine(penDashBorder,0+xAdj,0+yAdj,0+xAdj,550+yAdj); 
				g.DrawLine(penDashBorder,425+xAdj,0+yAdj,425+xAdj,550+yAdj);
				g.DrawLine(penDashBorder,0+xAdj,550+yAdj,425+xAdj,550+yAdj); 
			}
			else{//horizontal
				g.DrawLine(penDashBorder,0+xAdj,0+yAdj,550+xAdj,0+yAdj);
				g.DrawLine(penDashBorder,0+xAdj,0+yAdj,0+xAdj,425+yAdj); 
				g.DrawLine(penDashBorder,550+xAdj,0+yAdj,550+xAdj,425+yAdj); 
				g.DrawLine(penDashBorder,0+xAdj,425+yAdj,550+xAdj,425+yAdj); 
			}
			//Dr--------------------------------------------------------------------------------------------------
			//Left Side
			x=50+xAdj;
			y=37+yAdj;
			text=prov.FName+" "+prov.MI+" "+prov.LName+", "+prov.Suffix;
			g.DrawString(text,fontBold,brush,x,y);
			y+=fontH;
			text=PrefB.GetString("PracticeAddress");
			g.DrawString(text,font,brush,x,y);
			y+=fontH;
			text=PrefB.GetString("PracticeAddress2");
			if(text!=""){
				g.DrawString(text,font,brush,x,y);
				y+=fontH; 
			}
			text=PrefB.GetString("PracticeCity")+", "+PrefB.GetString("PracticeST")+" "+PrefB.GetString("PracticeZip");
			g.DrawString(text,font,brush,x,y);
			y=100+yAdj;
			if(PrefB.GetBool("RxOrientVert")){
				g.DrawLine(Pens.Black,25+xAdj,y,400+xAdj,y);
			}
			else{
				g.DrawLine(Pens.Black,25+xAdj,y,525+xAdj,y);
			}
			//Right Side
			x=280+xAdj;
			y=38+yAdj;
			text=PrefB.GetString("PracticePhone");
			if(text.Length==10) {
				text="("+text.Substring(0,3)+")"+text.Substring(3,3)+"-"+text.Substring(6);
			}
			g.DrawString(text,font,brush,x,y);
			y+=fontH;
			text=RxPatCur.RxDate.ToShortDateString();
			g.DrawString(text,font,brush,x,y);
			y+=fontH;
			text=Lan.g(this,"DEA#: ")+prov.DEANum;
			g.DrawString(text,font,brush,x,y);
			//Patient---------------------------------------------------------------------------------------------------
			//Upper Left
			x=90+xAdj;
			y=105+yAdj;
			text=PatCur.GetNameFL();
			g.DrawString(text,fontBold,brush,x,y);
			y+=fontH;
			text=Lan.g(this,"DOB: ")+PatCur.Birthdate.ToShortDateString();
			g.DrawString(text,fontBold,brush,x,y);
			y+=fontH;
			text=PatCur.HmPhone;
			g.DrawString(text,font,brush,x,y);
			y+=fontH;
			//x=280+xAdj;
			//y=120+yAdj;
			text=PatCur.Address;
			g.DrawString(text,font,brush,x,y);
			y+=fontH;
			text=PatCur.Address2;
			if(text!=""){
				g.DrawString(text,font,brush,x,y);
				y+=fontH; 
			}
			text=PatCur.City+", "+PatCur.State+" "+PatCur.Zip;
			g.DrawString(text,font,brush,x,y);
			y+=fontH;
			//RX-----------------------------------------------------------------------------------------------------
			y=190+yAdj;
			x=40+xAdj;
			g.DrawString(Lan.g(this,"Rx"),fontRX,brush,x,y);
			y=205+yAdj;
			x=90+xAdj;
			g.DrawString(RxPatCur.Drug,fontBold,brush,x,y);
			y+=(int)(fontH*1.5);
			g.DrawString(Lan.g(this,"Disp:")+"  "+RxPatCur.Disp,font,brush,x,y);
			y+=(int)(fontH*1.5);
			g.DrawString(Lan.g(this,"Sig:")+"  "+RxPatCur.Sig,font,brush,new RectangleF(x,y,325,fontH*2));
			y+=(int)(fontH*2.5);
			g.DrawString(Lan.g(this,"Refills:")+"  "+RxPatCur.Refills,font,brush,x,y);
			//Generic Subst----------------------------------------------------------------------------------------------
			if(PrefB.GetInt("RxGeneric")==2){//two signature lines
				text=Lan.g(this,"Generic Substitution Permitted");
				if(PrefB.GetBool("RxOrientVert")) {
					y=380+yAdj;
					g.DrawLine(Pens.Black,90+xAdj,y,325+xAdj,y);
					x=207+xAdj-(int)(g.MeasureString(text,font).Width/2);
				}
				else{
					y=360+yAdj;
					g.DrawLine(Pens.Black,50+xAdj,y,260+xAdj,y);
					x=145+xAdj-(int)(g.MeasureString(text,font).Width/2);
				}
				y+=4;
				g.DrawString(text,font,brush,x,y);
			}
			else{//check boxes
				x=50+xAdj;
				y=343+yAdj;
				g.DrawRectangle(Pens.Black,x,y,12,12);
				x+=17;
				text=Lan.g(this,"Dispense as Written");
				g.DrawString(text,font,brush,x,y);
				x-=17;
				y+=25;
				g.DrawRectangle(Pens.Black,x,y,12,12);
				if(PrefB.GetInt("RxGeneric")==0){//generic checked
					g.DrawLine(Pens.Black,x,y,x+12,y+12);
					g.DrawLine(Pens.Black,x+12,y,x,y+12);	
				}
				x+=17;
				text=Lan.g(this,"Generic Substitution Permitted");
				g.DrawString(text,font,brush,x,y);
			}
			//Signature Line--------------------------------------------------------------------------------------------
			if(PrefB.GetInt("RxGeneric")==2){//two signature lines
				text=Lan.g(this,"Dispense as Written");
			}
			else{
				text=Lan.g(this,"Signature of Prescriber");
			}
			if(PrefB.GetBool("RxOrientVert")) {
				y=460+yAdj;
				g.DrawLine(Pens.Black,90+xAdj,y,325+xAdj,y);
				x=207+xAdj-(int)(g.MeasureString(text,font).Width/2);
			}
			else {
				y=360+yAdj;
				g.DrawLine(Pens.Black,300+xAdj,y,530+xAdj,y);
				x=412+xAdj-(int)(g.MeasureString(text,font).Width/2);
			}
			y+=4;
			g.DrawString(text,font,brush,x,y);
			g.Dispose();
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(IsNew){
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(MessageBox.Show(Lan.g(this,"Delete Prescription?"),"",MessageBoxButtons.OKCancel)
				!=DialogResult.OK){
				return;
			}
			RxPats.Delete(RxPatCur.RxNum);
			DialogResult=DialogResult.OK;	
		}

		private void butPrint_Click(object sender, System.EventArgs e) {
			if(SaveRx()){
				PrintReport();
				DialogResult=DialogResult.OK;
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(SaveRx())
				DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

	}
}
