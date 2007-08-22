using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;
using CDT;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormClaimFormEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.VScrollBar vScrollBar1;
		private System.Windows.Forms.TextBox textDescription;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox checkIsHidden;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListBox listItems;
		private OpenDental.UI.Button butAdd;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox textXPos;
		private System.Windows.Forms.TextBox textYPos;
		private System.Windows.Forms.TextBox textWidth;
		private System.Windows.Forms.TextBox textHeight;
		private System.Windows.Forms.FontDialog fontDialog1;
		private OpenDental.UI.Button butFont;
		private OpenDental.UI.Button butPrint;
		///<summary></summary>
		public bool IsNew;
		//private bool shiftIsDown;
		private bool controlIsDown;
		private bool mouseIsDown;
		private PointF mouseDownLoc;
		private PointF[] oldItemLocs;
		private System.Drawing.Printing.PrintDocument pd2;
		private System.Windows.Forms.Label labelUniqueID;
		private System.Windows.Forms.TextBox textUniqueID;
		private System.Windows.Forms.Label labelWarning;
		private System.Windows.Forms.CheckBox checkPrintImages;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label8;
		private OpenDental.ValidNum textOffsetX;
		private OpenDental.ValidNum textOffsetY;
		private string[] displayStrings;
		///<summary></summary>
		public ClaimForm ClaimFormCur;

		///<summary></summary>
		public FormClaimFormEdit()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormClaimFormEdit));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.labelWarning = new System.Windows.Forms.Label();
			this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
			this.textDescription = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.checkIsHidden = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.listItems = new System.Windows.Forms.ListBox();
			this.butAdd = new OpenDental.UI.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.textXPos = new System.Windows.Forms.TextBox();
			this.textYPos = new System.Windows.Forms.TextBox();
			this.textWidth = new System.Windows.Forms.TextBox();
			this.textHeight = new System.Windows.Forms.TextBox();
			this.fontDialog1 = new System.Windows.Forms.FontDialog();
			this.butFont = new OpenDental.UI.Button();
			this.butPrint = new OpenDental.UI.Button();
			this.pd2 = new System.Drawing.Printing.PrintDocument();
			this.labelUniqueID = new System.Windows.Forms.Label();
			this.textUniqueID = new System.Windows.Forms.TextBox();
			this.checkPrintImages = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.textOffsetX = new OpenDental.ValidNum();
			this.textOffsetY = new OpenDental.ValidNum();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(892,667);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(892,637);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.White;
			this.panel2.Controls.Add(this.labelWarning);
			this.panel2.Location = new System.Drawing.Point(0,0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(850,1200);
			this.panel2.TabIndex = 2;
			this.panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseDown);
			this.panel2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseMove);
			this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
			this.panel2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseUp);
			// 
			// labelWarning
			// 
			this.labelWarning.Location = new System.Drawing.Point(13,3);
			this.labelWarning.Name = "labelWarning";
			this.labelWarning.Size = new System.Drawing.Size(782,28);
			this.labelWarning.TabIndex = 0;
			this.labelWarning.Text = "Warning.  This is not a user-added claim form.  Any changes you make might be los" +
    "t when you upgrade.  Add your own claim form if you want your changes to be pres" +
    "erved.";
			this.labelWarning.Visible = false;
			// 
			// vScrollBar1
			// 
			this.vScrollBar1.Location = new System.Drawing.Point(850,0);
			this.vScrollBar1.Name = "vScrollBar1";
			this.vScrollBar1.Size = new System.Drawing.Size(17,650);
			this.vScrollBar1.TabIndex = 3;
			this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
			// 
			// textDescription
			// 
			this.textDescription.Location = new System.Drawing.Point(867,18);
			this.textDescription.Name = "textDescription";
			this.textDescription.Size = new System.Drawing.Size(114,20);
			this.textDescription.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(868,3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(108,16);
			this.label1.TabIndex = 5;
			this.label1.Text = "Description";
			// 
			// checkIsHidden
			// 
			this.checkIsHidden.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIsHidden.Location = new System.Drawing.Point(869,41);
			this.checkIsHidden.Name = "checkIsHidden";
			this.checkIsHidden.Size = new System.Drawing.Size(109,16);
			this.checkIsHidden.TabIndex = 6;
			this.checkIsHidden.Text = "Is Hidden";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(870,162);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(108,15);
			this.label2.TabIndex = 7;
			this.label2.Text = "Items:";
			// 
			// listItems
			// 
			this.listItems.Location = new System.Drawing.Point(867,178);
			this.listItems.MultiColumn = true;
			this.listItems.Name = "listItems";
			this.listItems.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listItems.Size = new System.Drawing.Size(114,329);
			this.listItems.TabIndex = 8;
			this.listItems.DoubleClick += new System.EventHandler(this.listItems_DoubleClick);
			this.listItems.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listItems_MouseUp);
			this.listItems.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listItems_MouseDown);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Location = new System.Drawing.Point(946,157);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(37,20);
			this.butAdd.TabIndex = 9;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label4.Location = new System.Drawing.Point(869,520);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(51,16);
			this.label4.TabIndex = 10;
			this.label4.Text = "X Pos";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label5.Location = new System.Drawing.Point(869,541);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(51,16);
			this.label5.TabIndex = 11;
			this.label5.Text = "Y Pos";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label6.Location = new System.Drawing.Point(869,562);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(51,16);
			this.label6.TabIndex = 12;
			this.label6.Text = "Width";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label7
			// 
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label7.Location = new System.Drawing.Point(869,583);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(51,16);
			this.label7.TabIndex = 13;
			this.label7.Text = "Height";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textXPos
			// 
			this.textXPos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.textXPos.Location = new System.Drawing.Point(921,517);
			this.textXPos.Name = "textXPos";
			this.textXPos.Size = new System.Drawing.Size(60,20);
			this.textXPos.TabIndex = 14;
			this.textXPos.Validated += new System.EventHandler(this.textXPos_Validated);
			// 
			// textYPos
			// 
			this.textYPos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.textYPos.Location = new System.Drawing.Point(921,537);
			this.textYPos.Name = "textYPos";
			this.textYPos.Size = new System.Drawing.Size(60,20);
			this.textYPos.TabIndex = 15;
			this.textYPos.Validated += new System.EventHandler(this.textYPos_Validated);
			// 
			// textWidth
			// 
			this.textWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.textWidth.Location = new System.Drawing.Point(921,557);
			this.textWidth.Name = "textWidth";
			this.textWidth.Size = new System.Drawing.Size(60,20);
			this.textWidth.TabIndex = 16;
			this.textWidth.Validated += new System.EventHandler(this.textWidth_Validated);
			// 
			// textHeight
			// 
			this.textHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.textHeight.Location = new System.Drawing.Point(921,577);
			this.textHeight.Name = "textHeight";
			this.textHeight.Size = new System.Drawing.Size(60,20);
			this.textHeight.TabIndex = 17;
			this.textHeight.Validated += new System.EventHandler(this.textHeight_Validated);
			// 
			// fontDialog1
			// 
			this.fontDialog1.MaxSize = 25;
			this.fontDialog1.MinSize = 5;
			this.fontDialog1.ShowEffects = false;
			// 
			// butFont
			// 
			this.butFont.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butFont.Autosize = true;
			this.butFont.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butFont.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butFont.CornerRadius = 4F;
			this.butFont.Location = new System.Drawing.Point(870,94);
			this.butFont.Name = "butFont";
			this.butFont.Size = new System.Drawing.Size(111,21);
			this.butFont.TabIndex = 20;
			this.butFont.Text = "&Font";
			this.butFont.Click += new System.EventHandler(this.butFont_Click);
			// 
			// butPrint
			// 
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butPrint.Autosize = true;
			this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.CornerRadius = 4F;
			this.butPrint.Location = new System.Drawing.Point(892,607);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(75,26);
			this.butPrint.TabIndex = 22;
			this.butPrint.Text = "&Print";
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// labelUniqueID
			// 
			this.labelUniqueID.Location = new System.Drawing.Point(870,58);
			this.labelUniqueID.Name = "labelUniqueID";
			this.labelUniqueID.Size = new System.Drawing.Size(59,15);
			this.labelUniqueID.TabIndex = 23;
			this.labelUniqueID.Text = "UniqueID";
			this.labelUniqueID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textUniqueID
			// 
			this.textUniqueID.Location = new System.Drawing.Point(928,55);
			this.textUniqueID.Name = "textUniqueID";
			this.textUniqueID.Size = new System.Drawing.Size(51,20);
			this.textUniqueID.TabIndex = 24;
			this.textUniqueID.Validating += new System.ComponentModel.CancelEventHandler(this.textUniqueID_Validating);
			// 
			// checkPrintImages
			// 
			this.checkPrintImages.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkPrintImages.Location = new System.Drawing.Point(869,76);
			this.checkPrintImages.Name = "checkPrintImages";
			this.checkPrintImages.Size = new System.Drawing.Size(109,16);
			this.checkPrintImages.TabIndex = 25;
			this.checkPrintImages.Text = "Print Images";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(872,120);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(58,15);
			this.label3.TabIndex = 26;
			this.label3.Text = "Offset X";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(872,139);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(58,15);
			this.label8.TabIndex = 28;
			this.label8.Text = "Offset Y";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textOffsetX
			// 
			this.textOffsetX.Location = new System.Drawing.Point(929,116);
			this.textOffsetX.MaxVal = 255;
			this.textOffsetX.MinVal = -9999;
			this.textOffsetX.Name = "textOffsetX";
			this.textOffsetX.Size = new System.Drawing.Size(50,20);
			this.textOffsetX.TabIndex = 30;
			// 
			// textOffsetY
			// 
			this.textOffsetY.Location = new System.Drawing.Point(929,136);
			this.textOffsetY.MaxVal = 255;
			this.textOffsetY.MinVal = -9999;
			this.textOffsetY.Name = "textOffsetY";
			this.textOffsetY.Size = new System.Drawing.Size(50,20);
			this.textOffsetY.TabIndex = 31;
			// 
			// FormClaimFormEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(992,700);
			this.Controls.Add(this.textOffsetY);
			this.Controls.Add(this.textOffsetX);
			this.Controls.Add(this.textUniqueID);
			this.Controls.Add(this.textDescription);
			this.Controls.Add(this.textHeight);
			this.Controls.Add(this.textWidth);
			this.Controls.Add(this.textYPos);
			this.Controls.Add(this.textXPos);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.butFont);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.checkPrintImages);
			this.Controls.Add(this.labelUniqueID);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.listItems);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.checkIsHidden);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.vScrollBar1);
			this.Controls.Add(this.panel2);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Name = "FormClaimFormEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.FormClaimFormEdit_Layout);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormClaimFormEdit_Closing);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormClaimFormEdit_KeyUp);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormClaimFormEdit_KeyDown);
			this.Load += new System.EventHandler(this.FormClaimFormEdit_Load);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormClaimFormEdit_Load(object sender, System.EventArgs e) {
			//must have already saved the claimform, because clicking ok, only updates it.

			FillForm();
			panel2.Invalidate();
		}

		private void FormClaimFormEdit_Layout(object sender, System.Windows.Forms.LayoutEventArgs e) {
			vScrollBar1.Height=this.ClientSize.Height;
			vScrollBar1.Minimum=0;
			vScrollBar1.Maximum=panel2.Height;//1200;
			vScrollBar1.LargeChange=ClientSize.Height;
			listItems.Height=textXPos.Location.Y-listItems.Location.Y-4;
			listItems.Width=this.ClientSize.Width-listItems.Location.X-3;
		}

		private void FillForm(){
			if(ClaimFormCur.UniqueID!=""){
				labelWarning.Visible=true;//stupid thing malfunctions if you try to display a MessageBox.
			}		
			textDescription.Text=ClaimFormCur.Description;
			checkIsHidden.Checked=ClaimFormCur.IsHidden;
			textUniqueID.Text=ClaimFormCur.UniqueID.ToString();
			checkPrintImages.Checked=ClaimFormCur.PrintImages;
			textOffsetX.Text=ClaimFormCur.OffsetX.ToString();
			textOffsetY.Text=ClaimFormCur.OffsetY.ToString();
			if(ClaimFormCur.FontName=="" || ClaimFormCur.FontSize==0){
				ClaimFormCur.FontName="Arial";
				ClaimFormCur.FontSize=8;
			}
			ClaimFormCur.Items=ClaimFormItems.GetListForForm(ClaimFormCur.ClaimFormNum);
			listItems.Items.Clear();
			for(int i=0;i<ClaimFormCur.Items.Length;i++){
				if(ClaimFormCur.Items[i].ImageFileName==""){//field
					listItems.Items.Add(ClaimFormCur.Items[i].FieldName);
				}
				else{//image
					listItems.Items.Add(ClaimFormCur.Items[i].ImageFileName);
				}
			}
		}

		private void FillItem(){
			if(listItems.SelectedIndices.Count==0){
				textXPos.Text="";
				textYPos.Text="";
				textWidth.Text="";
				textHeight.Text="";
			}
			else if(listItems.SelectedIndices.Count==1){
				//ClaimFormItems.Cur=ClaimFormItems.ListForForm[listItems.SelectedIndices[0]];
				textXPos.Text=ClaimFormCur.Items[listItems.SelectedIndices[0]].XPos.ToString();
				textYPos.Text=ClaimFormCur.Items[listItems.SelectedIndices[0]].YPos.ToString();
				textWidth.Text=ClaimFormCur.Items[listItems.SelectedIndices[0]].Width.ToString();
				textHeight.Text=ClaimFormCur.Items[listItems.SelectedIndices[0]].Height.ToString();
			}
			else{//2 or more selected
				//only shows a value if all are the same
				bool xSame=true;
				bool ySame=true;
				bool wSame=true;
				bool hSame=true;
				for(int i=1;i<listItems.SelectedIndices.Count;i++){//loop starts with second items to compare
					if(ClaimFormCur.Items[listItems.SelectedIndices[i]].XPos!=
						ClaimFormCur.Items[listItems.SelectedIndices[i-1]].XPos){
						xSame=false;
					}
					if(ClaimFormCur.Items[listItems.SelectedIndices[i]].YPos!=
						ClaimFormCur.Items[listItems.SelectedIndices[i-1]].YPos){
						ySame=false;
					}
					if(ClaimFormCur.Items[listItems.SelectedIndices[i]].Width!=
						ClaimFormCur.Items[listItems.SelectedIndices[i-1]].Width){
						wSame=false;
					}
					if(ClaimFormCur.Items[listItems.SelectedIndices[i]].Height!=
						ClaimFormCur.Items[listItems.SelectedIndices[i-1]].Height){
						hSame=false;
					}
				}
				if(xSame)
					textXPos.Text=ClaimFormCur.Items[listItems.SelectedIndices[0]].XPos.ToString();
				else
					textXPos.Text="";
				if(ySame)
					textYPos.Text=ClaimFormCur.Items[listItems.SelectedIndices[0]].YPos.ToString();
				else
					textYPos.Text="";
				if(wSame)
					textWidth.Text=ClaimFormCur.Items[listItems.SelectedIndices[0]].Width.ToString();
				else
					textWidth.Text="";
				if(hSame)
					textHeight.Text=ClaimFormCur.Items[listItems.SelectedIndices[0]].Height.ToString();
				else
					textHeight.Text="";
			}
		}//fill item

		private void panel2_Paint(object sender, System.Windows.Forms.PaintEventArgs e) {
			//could make this much faster if invalidated only certain areas, but no time
			FillDisplayStrings();
			Graphics grfx=e.Graphics;
			Color myColor;
			float xPosRect;
			float xPosText;
			for(int i=0;i<ClaimFormCur.Items.Length;i++){
				if(ClaimFormCur.Items[i].ImageFileName==""){//field
					if(listItems.SelectedIndices.Contains(i))
						myColor=Color.Red;
					else myColor=Color.Blue;
					xPosRect=ClaimFormCur.Items[i].XPos;
					xPosText=xPosRect;
					if(displayStrings[i]=="1234.00"){
						xPosRect-=ClaimFormCur.Items[i].Width;//this aligns it to the right
						xPosText-=grfx.MeasureString("1234.00"
							,new Font(ClaimFormCur.FontName,ClaimFormCur.FontSize)).Width;
					}
					grfx.DrawRectangle(new Pen(myColor)
						,xPosRect,ClaimFormCur.Items[i].YPos
						,ClaimFormCur.Items[i].Width,ClaimFormCur.Items[i].Height);
					grfx.DrawString(displayStrings[i]
						,new Font(ClaimFormCur.FontName,ClaimFormCur.FontSize)
						,new SolidBrush(myColor)
						,new RectangleF(xPosText,ClaimFormCur.Items[i].YPos
						,ClaimFormCur.Items[i].Width,ClaimFormCur.Items[i].Height));
				}
				else{//image
					Image thisImage;
					string extension;
					if(ClaimFormCur.Items[i].ImageFileName=="ADA2006.gif"){
						thisImage=CDT.Class1.GetADA2006();
						extension=".gif";
					}
					else{
						string fileName=ODFileUtils.CombinePaths(FormPath.GetPreferredImagePath(),ClaimFormCur.Items[i].ImageFileName);
						if(!File.Exists(fileName)){
							grfx.DrawString("IMAGE FILE NOT FOUND",new Font(FontFamily.GenericSansSerif,12,FontStyle.Bold)
								,Brushes.DarkRed,0,0);
							//MessageBox.Show("File not found.");
							continue;
						}
						thisImage=Image.FromFile(fileName);
						extension=Path.GetExtension(fileName);
					}
					if(extension==".jpg"){
						grfx.DrawImage(thisImage
							,ClaimFormCur.Items[i].XPos
							,ClaimFormCur.Items[i].YPos
							,(int)(thisImage.Width/thisImage.HorizontalResolution*100)
							,(int)(thisImage.Height/thisImage.VerticalResolution*100));
					}
					else if(extension==".gif"){
						grfx.DrawImage(thisImage
							,ClaimFormCur.Items[i].XPos
							,ClaimFormCur.Items[i].YPos
							,ClaimFormCur.Items[i].Width
							,ClaimFormCur.Items[i].Height);
					}
					else if(extension==".emf"){
						grfx.DrawImage(thisImage
							,ClaimFormCur.Items[i].XPos
							,ClaimFormCur.Items[i].YPos
							,thisImage.Width
							,thisImage.Height);
					}
				}
			}
		}

		private void FillDisplayStrings(){
			displayStrings=new string[ClaimFormCur.Items.Length];
			for(int i=0;i<ClaimFormCur.Items.Length;i++){
				switch(ClaimFormCur.Items[i].FieldName){
					default://image="", or most fields = name of field
						displayStrings[i]=ClaimFormCur.Items[i].FieldName;
						break;
					//bool
					case "IsPreAuth":
					case "IsStandardClaim":
					case "IsMedicaidClaim":
					case "OtherInsExists":
					case "OtherInsNotExists":
					case "OtherInsSubscrIsMale":
					case "OtherInsSubscrIsFemale":
					case "OtherInsRelatIsSelf":
					case "OtherInsRelatIsSpouse":
					case "OtherInsRelatIsChild":
					case "OtherInsRelatIsOther":
					case "SubscrIsMale":
					case "SubscrIsFemale":
					case "SubscrIsMarried":
					case "SubscrIsSingle":
					case "SubscrIsFTStudent":
					case "SubscrIsPTStudent":
					case "RelatIsSelf":
					case "RelatIsSpouse":
					case "RelatIsChild":
					case "RelatIsOther":
					case "IsFTStudent":
					case "IsPTStudent":
					case "IsStudent":
					case "PatientIsMale":
					case "PatientIsFemale":
					case "PatientIsMarried":
					case "PatientIsSingle":
					case "Miss1":
					case "Miss2":
					case "Miss3":
					case "Miss4":
					case "Miss5":
					case "Miss6":
					case "Miss7":
					case "Miss8":
					case "Miss9":
					case "Miss10":
					case "Miss11":
					case "Miss12":
					case "Miss13":
					case "Miss14":
					case "Miss15":
					case "Miss16":
					case "Miss17":
					case "Miss18":
					case "Miss19":
					case "Miss20":
					case "Miss21":
					case "Miss22":
					case "Miss23":
					case "Miss24":
					case "Miss25":
					case "Miss26":
					case "Miss27":
					case "Miss28":
					case "Miss29":
					case "Miss30":
					case "Miss31":
					case "Miss32":
					case "PlaceIsOffice":
					case "PlaceIsHospADA2002":
					case "PlaceIsExtCareFacilityADA2002":
					case "PlaceIsOtherADA2002":
					case "PlaceIsInpatHosp":
					case "PlaceIsOutpatHosp":
					case "PlaceIsAdultLivCareFac":
					case "PlaceIsSkilledNursFac":
					case "PlaceIsPatientsHome":
					case "PlaceIsOtherLocation":
					case "IsRadiographsAttached":
					case "RadiographsNotAttached":
					case "IsNotOrtho":
					case "IsOrtho":
					case "IsNotProsth":
					case "IsInitialProsth":
					case "IsNotReplacementProsth":
					case "IsReplacementProsth":
					case "IsOccupational":
					case "IsNotOccupational":
					case "IsAutoAccident":
					case "IsNotAutoAccident":
					case "IsOtherAccident":
					case "IsNotOtherAccident":
					case "IsNotAccident"://of either kind
					case "IsAccident":
					case "BillingDentistNumIsSSN":
					case "BillingDentistNumIsTIN":
						displayStrings[i]="X";
						break;
					//short strings custom
					case "PriInsST":
					case "OtherInsST":
						displayStrings[i]="ST";
						break;
					//date
					case "PatientDOB":
					case "SubscrDOB":
					case "OtherInsSubscrDOB":
					case "P1Date":
					case "P2Date":
					case "P3Date":
					case "P4Date":
					case "P5Date":
					case "P6Date":
					case "P7Date":
					case "P8Date":
					case "P9Date":
					case "P10Date":
					case "PatientReleaseDate":
					case "PatientAssignmentDate":
					case "DateOrthoPlaced":
					case "DatePriorProsthPlaced":
					case "AccidentDate":
					case "TreatingDentistSigDate":
						if(ClaimFormCur.Items[i].FormatString=="")
							displayStrings[i]="";//DateTime.Today.ToShortDateString();
						else
							displayStrings[i]=DateTime.Today.ToString(ClaimFormCur.Items[i].FormatString);
						break;
					case "P1Fee":
					case "P2Fee":
					case "P3Fee":
					case "P4Fee":
					case "P5Fee":
					case "P6Fee":
					case "P7Fee":
					case "P8Fee":
					case "P9Fee":
					case "P10Fee":
					case "P1Lab":
					case "P2Lab":
					case "P3Lab":
					case "P4Lab":
					case "P5Lab":
					case "P6Lab":
					case "P7Lab":
					case "P8Lab":
					case "P9Lab":
					case "P10Lab":
					case "P1FeeMinusLab":
					case "P2FeeMinusLab":
					case "P3FeeMinusLab":
					case "P4FeeMinusLab":
					case "P5FeeMinusLab":
					case "P6FeeMinusLab":
					case "P7FeeMinusLab":
					case "P8FeeMinusLab":
					case "P9FeeMinusLab":
					case "P10FeeMinusLab":
					case "MedInsAAmtDue":
					case "MedInsBAmtDue":
					case "MedInsCAmtDue":
					case "MedInsAPriorPmt":
					case "MedInsBPriorPmt":
					case "MedInsCPriorPmt":
					case "TotalFee":
					case "MedValAmount39a":
					case "MedValAmount39b":
					case "MedValAmount39c":
					case "MedValAmount39d":
					case "MedValAmount40a":
					case "MedValAmount40b":
					case "MedValAmount40c":
					case "MedValAmount40d":
					case "MedValAmount41a":
					case "MedValAmount41b":
					case "MedValAmount41c":
					case "MedValAmount41d":
						displayStrings[i]="1234.00";
						break;
					case "Remarks":
						displayStrings[i]="This is a test of the remarks section of the claim form.";
						break;
					case "FixedText":
						displayStrings[i]=ClaimFormCur.Items[i].FormatString;
						break;
				}//switch
			}//for
		}

		private void vScrollBar1_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e) {
			panel2.Location=new Point(0,-vScrollBar1.Value);
		}

		private void textUniqueID_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
			if(ClaimFormCur.UniqueID==textUniqueID.Text){
				return;
			}
			if(ClaimFormCur.UniqueID==""){
				//this is a user-created claimform
				MessageBox.Show(Lan.g(this,"Remember if you assign a Unique ID, that it should be your name followed by a number. For instance JoeDeveloper4. Claim forms released by us will be of the form OD#."));
			}
			else{
				//they are trying to change a claimform with an existing UniqueID
				if(MessageBox.Show(Lan.g(this,"Are you sure you want to change the Unique ID?  It might be better to make a copy of the claim form instead. If you change it, future upgrades will not be made to this form automatically. Continue anyway?")
					,"",MessageBoxButtons.OKCancel)!=DialogResult.OK)
				{
					textUniqueID.Text=ClaimFormCur.UniqueID;
					e.Cancel=true;
					return;
				}
				//allow the change
			}
		}

		private void listItems_DoubleClick(object sender, System.EventArgs e) {
			int index=listItems.SelectedIndices[0];
			FormClaimFormItemEdit FormCFIE=new FormClaimFormItemEdit();
			FormCFIE.CFIcur=ClaimFormCur.Items[index];
			FormCFIE.ShowDialog();
			ClaimFormItems.Refresh();
			FillForm();
			panel2.Invalidate();
			if(listItems.Items.Count > index)//in case the last item in the list was deleted
				listItems.SetSelected(index,true);
			FillItem();
		}

		private void textXPos_Validated(object sender, System.EventArgs e) {
			if(listItems.SelectedIndices.Count==0)
				return;
			if(listItems.SelectedIndices.Count > 1 && textXPos.Text=="")
				//blank means that the values for the selected items are not the same
				return;//so disregard unless you put in an actual value
			float xPos;
			try{xPos=Convert.ToSingle(textXPos.Text);}
			catch{xPos=0;}
			for(int i=0;i<listItems.SelectedIndices.Count;i++){
				ClaimFormCur.Items[listItems.SelectedIndices[i]].XPos=xPos;
				ClaimFormItems.Update(ClaimFormCur.Items[listItems.SelectedIndices[i]]);
			}
			ClaimFormItems.Refresh();
			ClaimFormCur.Items=ClaimFormItems.GetListForForm(ClaimFormCur.ClaimFormNum);
			FillItem();
			panel2.Invalidate();
		}

		private void textYPos_Validated(object sender, System.EventArgs e) {
			if(listItems.SelectedIndices.Count==0)
				return;
			if(listItems.SelectedIndices.Count > 1 && textYPos.Text=="")
				return;
			float yPos;
			try{yPos=Convert.ToSingle(textYPos.Text);}
			catch{yPos=0;}
			for(int i=0;i<listItems.SelectedIndices.Count;i++){
				ClaimFormCur.Items[listItems.SelectedIndices[i]].YPos=yPos;
				ClaimFormItems.Update(ClaimFormCur.Items[listItems.SelectedIndices[i]]);
			}
			ClaimFormItems.Refresh();
			ClaimFormCur.Items=ClaimFormItems.GetListForForm(ClaimFormCur.ClaimFormNum);
			FillItem();
			panel2.Invalidate();
		}

		private void textWidth_Validated(object sender, System.EventArgs e) {
			//MessageBox.Show("width");
			if(listItems.SelectedIndices.Count==0)
				return;
			if(listItems.SelectedIndices.Count > 1 && textWidth.Text=="")
				return;
			float width;
			try{width=Convert.ToSingle(textWidth.Text);}
			catch{width=0;}
			for(int i=0;i<listItems.SelectedIndices.Count;i++){
				ClaimFormCur.Items[listItems.SelectedIndices[i]].Width=width;
				ClaimFormItems.Update(ClaimFormCur.Items[listItems.SelectedIndices[i]]);
			}
			ClaimFormItems.Refresh();
			ClaimFormCur.Items=ClaimFormItems.GetListForForm(ClaimFormCur.ClaimFormNum);
			FillItem();
			panel2.Invalidate();
		}

		private void textHeight_Validated(object sender, System.EventArgs e) {
			if(listItems.SelectedIndices.Count==0)
				return;
			if(listItems.SelectedIndices.Count > 1 && textHeight.Text=="")
				return;
			float height;
			try{height=Convert.ToSingle(textHeight.Text);}
			catch{height=0;}
			for(int i=0;i<listItems.SelectedIndices.Count;i++){
				ClaimFormCur.Items[listItems.SelectedIndices[i]].Height=height;
				ClaimFormItems.Update(ClaimFormCur.Items[listItems.SelectedIndices[i]]);
			}
			ClaimFormItems.Refresh();
			ClaimFormCur.Items=ClaimFormItems.GetListForForm(ClaimFormCur.ClaimFormNum);
			FillItem();
			panel2.Invalidate();
		}

		private void listItems_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			
		}

		private void listItems_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			FillItem();
			panel2.Invalidate();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			FormClaimFormItemEdit FormCFIE=new FormClaimFormItemEdit();
			FormCFIE.CFIcur=new  ClaimFormItem();
			FormCFIE.CFIcur.ClaimFormNum=ClaimFormCur.ClaimFormNum;
			FormCFIE.CFIcur.YPos=540;
			FormCFIE.IsNew=true;
			FormCFIE.ShowDialog();
			if(FormCFIE.DialogResult!=DialogResult.OK){
				return;
			}
			//MessageBox.Show(ClaimFormItems.Cur.ClaimFormItemNum.ToString());
			//ClaimFormItems.Refresh();
			//ClaimFormCur.Items=ClaimFormItems.GetListForForm(ClaimFormCur.ClaimFormNum);
			//FillDisplayStrings();
			//set the width and height
			//Graphics grfx=panel2.CreateGraphics();
//yes, I know this is buggy, but it's not that important, just a little annoying.
			/*if(displayStrings[ClaimFormItems.ListForForm.Length-1]!="X"){
				ClaimFormItems.Cur.Width=(float)(int)grfx.MeasureString
					(displayStrings[ClaimFormItems.ListForForm.Length-1]
					,new Font(ClaimFormCur.FontName,ClaimFormCur.FontSize)).Width;
				ClaimFormItems.Cur.Height=(float)(int)grfx.MeasureString
					(displayStrings[ClaimFormItems.ListForForm.Length-1]
					,new Font(ClaimFormCur.FontName,ClaimFormCur.FontSize)).Height;
			}*/
			//grfx.Dispose();
			//ClaimFormItems.Cur.YPos=540;
			//ClaimFormItems.UpdateCur();
			ClaimFormItems.Refresh();
			FillForm();//also gets ListForForm
			listItems.ClearSelected();
			listItems.SetSelected(listItems.Items.Count-1,true);//selects last item in list
			panel2.Invalidate();//also Fills displayStrings
			FillItem();
		}

		private void butFont_Click(object sender, System.EventArgs e) {
			Font myFont=new Font(ClaimFormCur.FontName,ClaimFormCur.FontSize);
			fontDialog1.Font=myFont;
			if(fontDialog1.ShowDialog()!=DialogResult.OK){
				return;
			}
			if(fontDialog1.Font.Style!=FontStyle.Regular){
				MessageBox.Show(Lan.g(this,"Only regular font style allowed."));
			}
			ClaimFormCur.FontName=fontDialog1.Font.Name;
			ClaimFormCur.FontSize=fontDialog1.Font.Size;
			//MessageBox.Show(fontDialog1.Font.Size.ToString());
			panel2.Invalidate();
			//fontDialog1.Font
		}

		private void FormClaimFormEdit_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			e.Handled=true;
			if(e.KeyCode!=Keys.Up
				&& e.KeyCode!=Keys.Down
				&& e.KeyCode!=Keys.Left
				&& e.KeyCode!=Keys.Right
				&& e.KeyCode!=Keys.ShiftKey
				&& e.KeyCode!=Keys.ControlKey){
				return;
			}
			//if(e.Shift){
			//	shiftIsDown=true;
			//}
			if(e.Control){
				controlIsDown=true;
			}
			if(e.KeyCode==Keys.ShiftKey){
				return;
			}
			if(e.KeyCode==Keys.ControlKey){
				return;
			}
			//loop through all items selected and change them
			ClaimFormItem curItem;
			for(int i=0;i<listItems.SelectedIndices.Count;i++){
				//MessageBox.Show(i.ToString());
				curItem=ClaimFormCur.Items[listItems.SelectedIndices[i]];
				switch(e.KeyCode){
					case Keys.Up:
						if(e.Shift)
							curItem.YPos-=10;
						else
							curItem.YPos-=1;
						break;
					case Keys.Down:
						if(e.Shift)
							curItem.YPos+=10;
						else
							curItem.YPos+=1;
						break;
					case Keys.Left:
						if(e.Shift)
							curItem.XPos-=10;
						else
							curItem.XPos-=1;
						break;
					case Keys.Right:
						if(e.Shift)
							curItem.XPos+=10;
						else
							curItem.XPos+=1;
						break;
				}
				if(curItem.YPos<0)
					curItem.YPos=0;
				if(curItem.YPos>1100)
					curItem.YPos=1100;
				if(curItem.XPos<0)
					curItem.XPos=0;
				if(curItem.XPos>850)
					curItem.XPos=850;
				ClaimFormItems.Update(curItem);
			}
			ClaimFormItems.Refresh();
			ClaimFormCur.Items=ClaimFormItems.GetListForForm(ClaimFormCur.ClaimFormNum);
			FillItem();
			panel2.Invalidate();
		}

		private void FormClaimFormEdit_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e) {
			//shiftIsDown=false;
			controlIsDown=false;
		}

		private void panel2_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			mouseIsDown=true;
			mouseDownLoc=new Point(e.X,e.Y);
			//find the item and select it in the list
			float width;//of item
			float height;
			Graphics grfx=panel2.CreateGraphics();
			//start at the end of the list and work backwards until match
			for(int i=ClaimFormCur.Items.Length-1;i>0;i--){
				if(ClaimFormCur.Items[i].Width==0 || ClaimFormCur.Items[i].Height==0){
					width=grfx.MeasureString(displayStrings[i]
						,new Font(ClaimFormCur.FontName,ClaimFormCur.FontSize)).Width;
					height=grfx.MeasureString(displayStrings[i]
						,new Font(ClaimFormCur.FontName,ClaimFormCur.FontSize)).Height;
				}
				else{//a width and height are available, so use them
					width=ClaimFormCur.Items[i].Width;
					height=ClaimFormCur.Items[i].Height;
				}
				if(e.X>ClaimFormCur.Items[i].XPos
					&& e.X<ClaimFormCur.Items[i].XPos+width
					&& e.Y>ClaimFormCur.Items[i].YPos
					&& e.Y<ClaimFormCur.Items[i].YPos+height)
				{
					if(controlIsDown){
						if(listItems.SelectedIndices.Contains(i)){//if this item already selected
							listItems.SetSelected(i,false);//unselect it
						}
						else{//if not selected
							listItems.SetSelected(i,true);//select it
						}
					}
					else{//control not down
						if(listItems.SelectedIndices.Count>1//if multiple items already selected
							&& listItems.SelectedIndices.Contains(i)){//and this is one of them
							//don't do anything.  The user is getting ready to drag a group
						}
						else{
							listItems.ClearSelected();
							listItems.SetSelected(i,true);
						}
					}
					break;
				}
			}
			grfx.Dispose();
			FillItem();//also sets the oldItemLocs
			oldItemLocs=new PointF[listItems.SelectedIndices.Count];
			for(int i=0;i<listItems.SelectedIndices.Count;i++){//then a normal loop to set oldlocs for dragging
				oldItemLocs[i]=new PointF((float)ClaimFormCur.Items[listItems.SelectedIndices[i]].XPos
					,(float)ClaimFormCur.Items[listItems.SelectedIndices[i]].YPos);
			}
			panel2.Invalidate();
		}

		private void panel2_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(!mouseIsDown || listItems.SelectedIndices.Count==0){
				return;
			}
			for(int i=0;i<listItems.SelectedIndices.Count;i++){
				//ClaimFormItems.Cur=ClaimFormCur.Items[listItems.SelectedIndices[i]];
				ClaimFormCur.Items[listItems.SelectedIndices[i]].XPos=oldItemLocs[i].X+e.X-mouseDownLoc.X;
				ClaimFormCur.Items[listItems.SelectedIndices[i]].YPos=oldItemLocs[i].Y+e.Y-mouseDownLoc.Y;
				if(ClaimFormCur.Items[listItems.SelectedIndices[i]].YPos<0)
					ClaimFormCur.Items[listItems.SelectedIndices[i]].YPos=0;
				if(ClaimFormCur.Items[listItems.SelectedIndices[i]].YPos>1100)
					ClaimFormCur.Items[listItems.SelectedIndices[i]].YPos=1100;
				if(ClaimFormCur.Items[listItems.SelectedIndices[i]].XPos<0)
					ClaimFormCur.Items[listItems.SelectedIndices[i]].XPos=0;
				if(ClaimFormCur.Items[listItems.SelectedIndices[i]].XPos>850)
					ClaimFormCur.Items[listItems.SelectedIndices[i]].XPos=850;
				ClaimFormItems.Update(ClaimFormCur.Items[listItems.SelectedIndices[i]]);
			}
			ClaimFormItems.Refresh();
			ClaimFormCur.Items=ClaimFormItems.GetListForForm(ClaimFormCur.ClaimFormNum);
			FillItem();
			panel2.Invalidate();
		}

		private void panel2_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			mouseIsDown=false;
		}

		private void butPrint_Click(object sender, System.EventArgs e) {
			if(!UpdateCur())
				return;
			pd2=new PrintDocument();
			pd2.OriginAtMargins=true;
			pd2.DefaultPageSettings.Margins=new Margins(0,0,0,0);
			pd2.PrintPage+=new PrintPageEventHandler(this.pd2_PrintPage);
			if(!Printers.SetPrinter(pd2,PrintSituation.Default)){
				return;
			}
			try{
				pd2.Print();
			}
			catch{
				MessageBox.Show(Lan.g(this,"Printer not available."));
			}
		}

		private void pd2_PrintPage(object sender, PrintPageEventArgs ev){//raised for each page to be printed.
			Graphics grfx=ev.Graphics;
			Color myColor;
			float xPosRect;
			float xPosText;
			for(int i=0;i<ClaimFormCur.Items.Length;i++){
				if(ClaimFormCur.Items[i].ImageFileName==""){//field
					myColor=Color.Blue;
					xPosRect=ClaimFormCur.Items[i].XPos+ClaimFormCur.OffsetX;
					xPosText=xPosRect;
					if(displayStrings[i]=="1234.00"){
						xPosRect-=ClaimFormCur.Items[i].Width;//this aligns it to the right
						xPosText-=grfx.MeasureString("1234.00"
							,new Font(ClaimFormCur.FontName,ClaimFormCur.FontSize)).Width;
					}
					grfx.DrawRectangle(new Pen(myColor)
						,xPosRect,ClaimFormCur.Items[i].YPos+ClaimFormCur.OffsetY
						,ClaimFormCur.Items[i].Width,ClaimFormCur.Items[i].Height);
					grfx.DrawString(displayStrings[i]
						,new Font(ClaimFormCur.FontName,ClaimFormCur.FontSize)
						,new SolidBrush(myColor)
						,new RectangleF(xPosText,ClaimFormCur.Items[i].YPos+ClaimFormCur.OffsetY
						,ClaimFormCur.Items[i].Width,ClaimFormCur.Items[i].Height));
				}
				else{//image
					if(!ClaimFormCur.PrintImages){
						continue;
					}
					string fileName=ODFileUtils.CombinePaths(FormPath.GetPreferredImagePath(),ClaimFormCur.Items[i].ImageFileName);
					Image thisImage=null;
					if(ClaimFormCur.Items[i].ImageFileName=="ADA2006.gif"){
						thisImage=CDT.Class1.GetADA2006();
					}else{
						if(!File.Exists(fileName)){
							MessageBox.Show("File not found.");
							continue;
						}
						thisImage=Image.FromFile(fileName);
					}
					if(fileName.Substring(fileName.Length-3)=="jpg"){
						grfx.DrawImage(thisImage
							,ClaimFormCur.Items[i].XPos+ClaimFormCur.OffsetX
							,ClaimFormCur.Items[i].YPos+ClaimFormCur.OffsetY
							,(int)(thisImage.Width/thisImage.HorizontalResolution*100)
							,(int)(thisImage.Height/thisImage.VerticalResolution*100));
					}
					else if(fileName.Substring(fileName.Length-3)=="gif"){
						grfx.DrawImage(thisImage
							,ClaimFormCur.Items[i].XPos+ClaimFormCur.OffsetX
							,ClaimFormCur.Items[i].YPos+ClaimFormCur.OffsetY
							,ClaimFormCur.Items[i].Width
							,ClaimFormCur.Items[i].Height);
					}
					else if(fileName.Substring(fileName.Length-3)=="emf"){
						grfx.DrawImage(thisImage
							,ClaimFormCur.Items[i].XPos+ClaimFormCur.OffsetX
							,ClaimFormCur.Items[i].YPos+ClaimFormCur.OffsetY
							,thisImage.Width
							,thisImage.Height);
					}
				}
			}
			ev.HasMorePages=false;
		}

		private bool UpdateCur(){
			if(  textOffsetX.errorProvider1.GetError(textOffsetX)!=""
				|| textOffsetY.errorProvider1.GetError(textOffsetY)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return false;
			}
			ClaimFormCur.Description=textDescription.Text;
			ClaimFormCur.IsHidden=checkIsHidden.Checked;
			ClaimFormCur.UniqueID=textUniqueID.Text;
			ClaimFormCur.PrintImages=checkPrintImages.Checked;
			ClaimFormCur.OffsetX=PIn.PInt(textOffsetX.Text);
			ClaimFormCur.OffsetY=PIn.PInt(textOffsetY.Text);
			ClaimForms.Update(ClaimFormCur);
			return true;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			//MessageBox.Show(ClaimForms.Cur.ClaimFormNum.ToString());
			if(!UpdateCur())
				return;
			if(ClaimFormCur.Description==""){
				MessageBox.Show(Lan.g(this,"You must enter a description first."));
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormClaimFormEdit_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(DialogResult==DialogResult.OK)
				return;
			if(IsNew){
				ClaimForms.Delete(ClaimFormCur);
			}
		}

		

		

		


		

		

		

		

		

		

		

		

		

		

		

		


	}
}





















