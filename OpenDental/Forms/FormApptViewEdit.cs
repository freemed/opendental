using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormApptViewEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butDelete;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox listOps;
		private System.Windows.Forms.ListBox listProv;
		private System.Windows.Forms.Label label2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textDescription;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ListView listViewDisplay;
		private System.Windows.Forms.ListView listViewAvailable;
		private System.Windows.Forms.Label label5;
		private OpenDental.UI.Button butDown;
		private OpenDental.UI.Button butUp;
		private OpenDental.UI.Button butLeft;
		private OpenDental.UI.Button butRight;
		///<summary></summary>
		public bool IsNew;
		///<summary>A collection of strings of all available element descriptions.</summary>
		private ArrayList allElements;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textRowsPerIncr;
		///<summary>A local list of ApptViewItems which are displayed in list on left.  Not updated to db until the form is closed.</summary>
		private ArrayList displayedElements;
		///<summary>Set this value before opening the form.</summary>
		public ApptView ApptViewCur;

		///<summary></summary>
		public FormApptViewEdit()
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
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "Row 1"},-1,System.Drawing.Color.Red,System.Drawing.Color.Empty,null);
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("row2");
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "Row 1"},-1,System.Drawing.Color.Red,System.Drawing.Color.Empty,null);
			System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("row2");
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormApptViewEdit));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.listOps = new System.Windows.Forms.ListBox();
			this.listProv = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textDescription = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.listViewDisplay = new System.Windows.Forms.ListView();
			this.listViewAvailable = new System.Windows.Forms.ListView();
			this.label5 = new System.Windows.Forms.Label();
			this.butDown = new OpenDental.UI.Button();
			this.butUp = new OpenDental.UI.Button();
			this.butLeft = new OpenDental.UI.Button();
			this.butRight = new OpenDental.UI.Button();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.label6 = new System.Windows.Forms.Label();
			this.textRowsPerIncr = new System.Windows.Forms.TextBox();
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
			this.butCancel.Location = new System.Drawing.Point(556,537);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
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
			this.butOK.Location = new System.Drawing.Point(556,496);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(32,543);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(87,26);
			this.butDelete.TabIndex = 38;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(32,56);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(128,23);
			this.label1.TabIndex = 39;
			this.label1.Text = "View Operatories";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listOps
			// 
			this.listOps.Location = new System.Drawing.Point(32,84);
			this.listOps.Name = "listOps";
			this.listOps.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listOps.Size = new System.Drawing.Size(120,186);
			this.listOps.TabIndex = 40;
			// 
			// listProv
			// 
			this.listProv.Location = new System.Drawing.Point(32,303);
			this.listProv.Name = "listProv";
			this.listProv.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listProv.Size = new System.Drawing.Size(120,212);
			this.listProv.TabIndex = 42;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(32,275);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(128,23);
			this.label2.TabIndex = 41;
			this.label2.Text = "View Providers";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(31,1);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(187,18);
			this.label3.TabIndex = 43;
			this.label3.Text = "Description";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textDescription
			// 
			this.textDescription.Location = new System.Drawing.Point(33,23);
			this.textDescription.Name = "textDescription";
			this.textDescription.Size = new System.Drawing.Size(250,20);
			this.textDescription.TabIndex = 44;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(215,51);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(174,31);
			this.label4.TabIndex = 46;
			this.label4.Text = "Rows Displayed (double click to edit color)";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listViewDisplay
			// 
			this.listViewDisplay.FullRowSelect = true;
			this.listViewDisplay.HideSelection = false;
			this.listViewDisplay.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
			this.listViewDisplay.LabelWrap = false;
			this.listViewDisplay.Location = new System.Drawing.Point(217,88);
			this.listViewDisplay.MultiSelect = false;
			this.listViewDisplay.Name = "listViewDisplay";
			this.listViewDisplay.Size = new System.Drawing.Size(175,368);
			this.listViewDisplay.TabIndex = 47;
			this.listViewDisplay.UseCompatibleStateImageBehavior = false;
			this.listViewDisplay.View = System.Windows.Forms.View.List;
			this.listViewDisplay.DoubleClick += new System.EventHandler(this.listViewDisplay_DoubleClick);
			// 
			// listViewAvailable
			// 
			this.listViewAvailable.FullRowSelect = true;
			this.listViewAvailable.HideSelection = false;
			this.listViewAvailable.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem3,
            listViewItem4});
			this.listViewAvailable.LabelWrap = false;
			this.listViewAvailable.Location = new System.Drawing.Point(453,88);
			this.listViewAvailable.MultiSelect = false;
			this.listViewAvailable.Name = "listViewAvailable";
			this.listViewAvailable.Size = new System.Drawing.Size(175,368);
			this.listViewAvailable.TabIndex = 48;
			this.listViewAvailable.UseCompatibleStateImageBehavior = false;
			this.listViewAvailable.View = System.Windows.Forms.View.List;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(452,65);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(161,17);
			this.label5.TabIndex = 49;
			this.label5.Text = "Available Rows";
			this.label5.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butDown
			// 
			this.butDown.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDown.Autosize = true;
			this.butDown.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDown.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDown.CornerRadius = 4F;
			this.butDown.Image = global::OpenDental.Properties.Resources.down;
			this.butDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDown.Location = new System.Drawing.Point(311,469);
			this.butDown.Name = "butDown";
			this.butDown.Size = new System.Drawing.Size(82,26);
			this.butDown.TabIndex = 50;
			this.butDown.Text = "&Down";
			this.butDown.Click += new System.EventHandler(this.butDown_Click);
			// 
			// butUp
			// 
			this.butUp.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butUp.Autosize = true;
			this.butUp.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butUp.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butUp.CornerRadius = 4F;
			this.butUp.Image = global::OpenDental.Properties.Resources.up;
			this.butUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butUp.Location = new System.Drawing.Point(217,469);
			this.butUp.Name = "butUp";
			this.butUp.Size = new System.Drawing.Size(82,26);
			this.butUp.TabIndex = 51;
			this.butUp.Text = "&Up";
			this.butUp.Click += new System.EventHandler(this.butUp_Click);
			// 
			// butLeft
			// 
			this.butLeft.AdjustImageLocation = new System.Drawing.Point(-1,0);
			this.butLeft.Autosize = true;
			this.butLeft.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLeft.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLeft.CornerRadius = 4F;
			this.butLeft.Image = global::OpenDental.Properties.Resources.Left;
			this.butLeft.Location = new System.Drawing.Point(404,197);
			this.butLeft.Name = "butLeft";
			this.butLeft.Size = new System.Drawing.Size(35,26);
			this.butLeft.TabIndex = 52;
			this.butLeft.Click += new System.EventHandler(this.butLeft_Click);
			// 
			// butRight
			// 
			this.butRight.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRight.Autosize = true;
			this.butRight.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRight.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRight.CornerRadius = 4F;
			this.butRight.Image = global::OpenDental.Properties.Resources.Right;
			this.butRight.Location = new System.Drawing.Point(404,237);
			this.butRight.Name = "butRight";
			this.butRight.Size = new System.Drawing.Size(35,26);
			this.butRight.TabIndex = 53;
			this.butRight.Click += new System.EventHandler(this.butRight_Click);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(336,1);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(246,18);
			this.label6.TabIndex = 54;
			this.label6.Text = "Rows Per Time Increment (usually 1)";
			this.label6.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textRowsPerIncr
			// 
			this.textRowsPerIncr.Location = new System.Drawing.Point(337,23);
			this.textRowsPerIncr.Name = "textRowsPerIncr";
			this.textRowsPerIncr.Size = new System.Drawing.Size(56,20);
			this.textRowsPerIncr.TabIndex = 55;
			this.textRowsPerIncr.Validating += new System.ComponentModel.CancelEventHandler(this.textRowsPerIncr_Validating);
			// 
			// FormApptViewEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(673,592);
			this.Controls.Add(this.textRowsPerIncr);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.butRight);
			this.Controls.Add(this.butLeft);
			this.Controls.Add(this.butDown);
			this.Controls.Add(this.butUp);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.listViewAvailable);
			this.Controls.Add(this.listViewDisplay);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textDescription);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.listProv);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.listOps);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormApptViewEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Appointment View Edit";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormApptViewEdit_Closing);
			this.Load += new System.EventHandler(this.FormApptViewEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormApptViewEdit_Load(object sender, System.EventArgs e) {
			textDescription.Text=ApptViewCur.Description;
			if(ApptViewCur.RowsPerIncr==0)
				textRowsPerIncr.Text="1";
			else
				textRowsPerIncr.Text=ApptViewCur.RowsPerIncr.ToString();
			ApptViewItems.GetForCurView(ApptViewCur);
			for(int i=0;i<Operatories.ListShort.Length;i++){
				listOps.Items.Add(Operatories.ListShort[i].OpName);
				if(ApptViewItems.OpIsInView(Operatories.ListShort[i].OperatoryNum)){
					listOps.SetSelected(i,true);
				}
			}
			for(int i=0;i<Providers.List.Length;i++){
				listProv.Items.Add
					(Providers.List[i].Abbr+" - "+Providers.List[i].LName+", "+Providers.List[i].FName);
				if(ApptViewItems.ProvIsInView(Providers.List[i].ProvNum)){
					listProv.SetSelected(i,true);
				}
			}
			allElements=new ArrayList();
			allElements.Add("AddrNote");
			allElements.Add("Age");
			allElements.Add("ChartNumAndName");
			allElements.Add("ChartNumber");
			allElements.Add("HmPhone");
			allElements.Add("Lab");
			allElements.Add("MedUrgNote");
			allElements.Add("PremedFlag");
			allElements.Add("Note");
			allElements.Add("PatientName");
			allElements.Add("PatNum");
			allElements.Add("PatNumAndName");
			allElements.Add("Procs");
			//allElements.Add("ProcDescript");
			allElements.Add("Production");
			allElements.Add("Provider");
			allElements.Add("WirelessPhone");
			allElements.Add("WkPhone");
			displayedElements=new ArrayList();
			for(int i=0;i<ApptViewItems.ApptRows.Length;i++){
				displayedElements.Add(ApptViewItems.ApptRows[i]);
			}
			FillElements();
		}

		///<summary>Fills the two lists based on displayedElements. No database transactions are performed here.</summary>
		private void FillElements(){
			ListViewItem item;
			//Fill rows displayed
			listViewDisplay.Items.Clear();
			for(int i=0;i<displayedElements.Count;i++){
				item=new ListViewItem(((ApptViewItem)displayedElements[i]).ElementDesc);
				item.ForeColor=((ApptViewItem)displayedElements[i]).ElementColor;
				listViewDisplay.Items.Add(item);
			}
			//then fill rows available
			listViewAvailable.Items.Clear();
			for(int i=0;i<allElements.Count;i++){
				if(!elementIsDisplayed((string)allElements[i])){
					item=new ListViewItem((string)allElements[i]);
					listViewAvailable.Items.Add(item);
				}
			}
		}

		///<summary>Called from FillElements. Used to determine whether a given element is already displayed. If not, then it is displayed in the available rows on the right.</summary>
		private bool elementIsDisplayed(string elementDesc){
			for(int i=0;i<displayedElements.Count;i++){
				if(((ApptViewItem)displayedElements[i]).ElementDesc==elementDesc){
					return true;
				}
			}
			return false;
		}

		private void butLeft_Click(object sender, System.EventArgs e) {
			if(listViewAvailable.SelectedIndices.Count==0){
				return;
			}
			//the item order is not used until saving to db.
			ApptViewItem item=new ApptViewItem(listViewAvailable.SelectedItems[0].Text,0,Color.Black);
			if(listViewDisplay.SelectedItems.Count==1){//insert
				displayedElements.Insert(listViewDisplay.SelectedItems[0].Index,item);
			}
			else{//add to end
				displayedElements.Add(item);
			}
			FillElements();
		}

		private void butRight_Click(object sender, System.EventArgs e) {
			if(listViewDisplay.SelectedIndices.Count==0){
				return;
			}
			displayedElements.RemoveAt(listViewDisplay.SelectedIndices[0]);
			FillElements();
		}

		private void butUp_Click(object sender, System.EventArgs e) {
			if(listViewDisplay.SelectedIndices.Count==0){
				return;
			}
			int originalI=listViewDisplay.SelectedIndices[0];
			if(originalI==0){
				return;//can't move up any more
			}
			ApptViewItem item=(ApptViewItem)displayedElements[originalI];
			displayedElements.RemoveAt(originalI);
			displayedElements.Insert(originalI-1,item);
			FillElements();
			listViewDisplay.Items[originalI-1].Selected=true;
		}

		private void butDown_Click(object sender, System.EventArgs e) {
			if(listViewDisplay.SelectedIndices.Count==0){
				return;
			}
			int originalI=listViewDisplay.SelectedIndices[0];
			if(originalI==displayedElements.Count-1){
				return;//can't move down any more
			}
			ApptViewItem item=(ApptViewItem)displayedElements[originalI];
			displayedElements.RemoveAt(originalI);
			displayedElements.Insert(originalI+1,item);
			FillElements();
			listViewDisplay.Items[originalI+1].Selected=true;
		}

		private void listViewDisplay_DoubleClick(object sender, System.EventArgs e) {
			if(listViewDisplay.SelectedIndices.Count==0){
				return;
			}
			int originalI=listViewDisplay.SelectedIndices[0];
			ApptViewItem item=(ApptViewItem)displayedElements[originalI];
			colorDialog1=new ColorDialog();
			colorDialog1.Color=item.ElementColor;
			if(colorDialog1.ShowDialog()!=DialogResult.OK){
				return;
			}
			item.ElementColor=colorDialog1.Color;
			displayedElements.RemoveAt(originalI);
			displayedElements.Insert(originalI,item);
			FillElements();
		}

		private void textRowsPerIncr_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
			try{
				Convert.ToInt32(textRowsPerIncr.Text);
			}
			catch{
				MessageBox.Show(Lan.g(this,"Must be a number between 1 and 3."));
				e.Cancel=true;
				return;
			}
			if(PIn.PInt(textRowsPerIncr.Text)<1 || PIn.PInt(textRowsPerIncr.Text)>3){
				MessageBox.Show(Lan.g(this,"Must be a number between 1 and 3."));
				e.Cancel=true;
			}
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			//this does mess up the item orders a little, but missing numbers don't actually hurt anything.
			if(MessageBox.Show(Lan.g(this,"Delete this category?"),"",MessageBoxButtons.OKCancel)
				!=DialogResult.OK){
				return;
			}
			ApptViewItems.DeleteAllForView(ApptViewCur);
			ApptViews.Delete(ApptViewCur);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(listOps.SelectedIndices.Count==0 || listProv.SelectedIndices.Count==0){
				MessageBox.Show(Lan.g(this,"At least one operatory and one provider must be selected."));
				return;
			}
			if(textDescription.Text==""){
				MessageBox.Show(Lan.g(this,"A description must be entered."));
				return;
			}
			if(displayedElements.Count==0){
				MessageBox.Show(Lan.g(this,"At least one row type must be displayed."));
				return;
			}
			ApptViewItems.DeleteAllForView(ApptViewCur);//start with a clean slate
			for(int i=0;i<Operatories.ListShort.Length;i++){
				if(listOps.SelectedIndices.Contains(i)){
					ApptViewItem ApptViewItemCur=new ApptViewItem();
					ApptViewItemCur.ApptViewNum=ApptViewCur.ApptViewNum;
					ApptViewItemCur.OpNum=Operatories.ListShort[i].OperatoryNum;
					ApptViewItems.Insert(ApptViewItemCur);
				}
			}
			for(int i=0;i<Providers.List.Length;i++){
				if(listProv.SelectedIndices.Contains(i)){
					ApptViewItem ApptViewItemCur=new ApptViewItem();
					ApptViewItemCur.ApptViewNum=ApptViewCur.ApptViewNum;
					ApptViewItemCur.ProvNum=Providers.List[i].ProvNum;
					ApptViewItems.Insert(ApptViewItemCur);
				}
			}
			for(int i=0;i<displayedElements.Count;i++){
				ApptViewItem ApptViewItemCur=(ApptViewItem)displayedElements[i];
				ApptViewItemCur.ApptViewNum=ApptViewCur.ApptViewNum;
				//elementDesc and elementColor already handled.
				ApptViewItemCur.ElementOrder=i;
				ApptViewItems.Insert(ApptViewItemCur);
			}
			ApptViewCur.Description=textDescription.Text;
			ApptViewCur.RowsPerIncr=PIn.PInt(textRowsPerIncr.Text);
			ApptViews.Update(ApptViewCur);//same whether isnew or not
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;;
		}

		private void FormApptViewEdit_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(DialogResult==DialogResult.OK)
				return;
			if(IsNew){
				ApptViewItems.DeleteAllForView(ApptViewCur);
				ApptViews.Delete(ApptViewCur);
			}
		}

		

		

		


	}
}





















