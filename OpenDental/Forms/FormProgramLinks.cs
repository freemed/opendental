using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormProgramLinks : System.Windows.Forms.Form{
		private System.Windows.Forms.ListBox listProgram;
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.Button butClose;
		private OpenDental.UI.Button butAdd;// Required designer variable.
		private Programs Programs=new Programs();
		private Label label2;
		private Label label1;
		private bool changed;

		///<summary></summary>
		public FormProgramLinks(){
			InitializeComponent();// Required for Windows Form Designer support
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

		private void InitializeComponent(){
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProgramLinks));
			this.listProgram = new System.Windows.Forms.ListBox();
			this.butClose = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// listProgram
			// 
			this.listProgram.Items.AddRange(new object[] {
            ""});
			this.listProgram.Location = new System.Drawing.Point(17,41);
			this.listProgram.Name = "listProgram";
			this.listProgram.Size = new System.Drawing.Size(282,459);
			this.listProgram.TabIndex = 34;
			this.listProgram.DoubleClick += new System.EventHandler(this.listProgram_DoubleClick);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.Location = new System.Drawing.Point(344,518);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,26);
			this.butClose.TabIndex = 38;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(17,516);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(75,26);
			this.butAdd.TabIndex = 41;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(14,2);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(294,36);
			this.label2.TabIndex = 43;
			this.label2.Text = "Double click on one of the programs in the list below to enable it or change its " +
    "settings";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(97,512);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(240,32);
			this.label1.TabIndex = 44;
			this.label1.Text = "Do not Add unless you have a totally custom bridge which we don\'t support.";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// FormProgramLinks
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(447,554);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.listProgram);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormProgramLinks";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Program Links";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormProgramLinks_Closing);
			this.Load += new System.EventHandler(this.FormProgramLinks_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormProgramLinks_Load(object sender, System.EventArgs e) {
			FillList();
		}

		private void FillList(){
			Programs.Refresh();
			listProgram.Items.Clear();
			string itemName="";
			for(int i=0;i<Programs.List.Length;i++){
				itemName=Programs.List[i].ProgDesc;
				if(Programs.List[i].Enabled)
					itemName+="(enabled)";
				listProgram.Items.Add(itemName);
			}
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			FormProgramLinkEdit FormPE=new FormProgramLinkEdit();
			FormPE.IsNew=true;
			FormPE.ProgramCur=new Program();
			FormPE.ShowDialog();
			changed=true;//because we don't really know what they did, so assume changed.
			FillList();
		}

		private void listProgram_DoubleClick(object sender, System.EventArgs e) {
			if(listProgram.SelectedIndex==-1)
				return;
			FormProgramLinkEdit FormPE=new FormProgramLinkEdit();
			FormPE.ProgramCur=Programs.List[listProgram.SelectedIndex];
			FormPE.ShowDialog();
			changed=true;
			FillList();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void FormProgramLinks_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(changed){
				DataValid.SetInvalid(InvalidTypes.Programs | InvalidTypes.ToolBut);
			}
		}

	

		

		

		



		
	}
}
