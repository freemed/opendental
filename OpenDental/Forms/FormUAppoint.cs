using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary> </summary>
	public class FormUAppoint:System.Windows.Forms.Form {
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.CheckBox checkEnabled;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textProgName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textProgDesc;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textPath;// Required designer variable.
		/// <summary>This Program link is new.</summary>
		public bool IsNew;
		public Program ProgramCur;
		private TextBox textUsername;
		private Label label4;
		private TextBox textPassword;
		private Label label5;
		private TextBox textWorkstationName;
		private Label label6;
		private TextBox textIntervalSeconds;
		private Label label7;
		private TextBox textDateTimeLastUploaded;
		private Label label8;
		private TextBox textSynchStatus;
		private Label label9;
		private OpenDental.UI.Button butStart;
		private Label label10;
		private TextBox textNote;
		private List<ProgramProperty> PropertyList;

		///<summary></summary>
		public FormUAppoint() {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUAppoint));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.checkEnabled = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textProgName = new System.Windows.Forms.TextBox();
			this.textProgDesc = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textPath = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textUsername = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textPassword = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textWorkstationName = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textIntervalSeconds = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textDateTimeLastUploaded = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.textSynchStatus = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.butStart = new OpenDental.UI.Button();
			this.label10 = new System.Windows.Forms.Label();
			this.textNote = new System.Windows.Forms.TextBox();
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
			this.butCancel.Location = new System.Drawing.Point(590,379);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
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
			this.butOK.Location = new System.Drawing.Point(590,345);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// checkEnabled
			// 
			this.checkEnabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkEnabled.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkEnabled.Location = new System.Drawing.Point(161,60);
			this.checkEnabled.Name = "checkEnabled";
			this.checkEnabled.Size = new System.Drawing.Size(98,18);
			this.checkEnabled.TabIndex = 41;
			this.checkEnabled.Text = "Enabled";
			this.checkEnabled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(58,10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(187,18);
			this.label1.TabIndex = 44;
			this.label1.Text = "Internal Name";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textProgName
			// 
			this.textProgName.Location = new System.Drawing.Point(246,9);
			this.textProgName.Name = "textProgName";
			this.textProgName.ReadOnly = true;
			this.textProgName.Size = new System.Drawing.Size(275,20);
			this.textProgName.TabIndex = 45;
			// 
			// textProgDesc
			// 
			this.textProgDesc.Location = new System.Drawing.Point(246,34);
			this.textProgDesc.Name = "textProgDesc";
			this.textProgDesc.Size = new System.Drawing.Size(275,20);
			this.textProgDesc.TabIndex = 47;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(57,35);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(187,18);
			this.label2.TabIndex = 46;
			this.label2.Text = "Description";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textPath
			// 
			this.textPath.Location = new System.Drawing.Point(246,81);
			this.textPath.Name = "textPath";
			this.textPath.Size = new System.Drawing.Size(275,20);
			this.textPath.TabIndex = 49;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(13,83);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(231,18);
			this.label3.TabIndex = 48;
			this.label3.Text = "URL of UAppoint server";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textUsername
			// 
			this.textUsername.Location = new System.Drawing.Point(246,107);
			this.textUsername.Name = "textUsername";
			this.textUsername.Size = new System.Drawing.Size(169,20);
			this.textUsername.TabIndex = 51;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(13,109);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(231,18);
			this.label4.TabIndex = 50;
			this.label4.Text = "Username";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textPassword
			// 
			this.textPassword.Location = new System.Drawing.Point(246,133);
			this.textPassword.Name = "textPassword";
			this.textPassword.Size = new System.Drawing.Size(251,20);
			this.textPassword.TabIndex = 53;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(13,135);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(231,18);
			this.label5.TabIndex = 52;
			this.label5.Text = "Password";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textWorkstationName
			// 
			this.textWorkstationName.Location = new System.Drawing.Point(246,159);
			this.textWorkstationName.Name = "textWorkstationName";
			this.textWorkstationName.Size = new System.Drawing.Size(169,20);
			this.textWorkstationName.TabIndex = 55;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(13,161);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(231,18);
			this.label6.TabIndex = 54;
			this.label6.Text = "Name of workstation used to synch";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textIntervalSeconds
			// 
			this.textIntervalSeconds.Location = new System.Drawing.Point(246,185);
			this.textIntervalSeconds.Name = "textIntervalSeconds";
			this.textIntervalSeconds.Size = new System.Drawing.Size(37,20);
			this.textIntervalSeconds.TabIndex = 57;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(13,187);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(231,18);
			this.label7.TabIndex = 56;
			this.label7.Text = "Synch interval in seconds";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDateTimeLastUploaded
			// 
			this.textDateTimeLastUploaded.Location = new System.Drawing.Point(246,211);
			this.textDateTimeLastUploaded.Name = "textDateTimeLastUploaded";
			this.textDateTimeLastUploaded.ReadOnly = true;
			this.textDateTimeLastUploaded.Size = new System.Drawing.Size(169,20);
			this.textDateTimeLastUploaded.TabIndex = 59;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(13,213);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(231,18);
			this.label8.TabIndex = 58;
			this.label8.Text = "DateTime last uploaded";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textSynchStatus
			// 
			this.textSynchStatus.Location = new System.Drawing.Point(246,237);
			this.textSynchStatus.Multiline = true;
			this.textSynchStatus.Name = "textSynchStatus";
			this.textSynchStatus.ReadOnly = true;
			this.textSynchStatus.Size = new System.Drawing.Size(275,44);
			this.textSynchStatus.TabIndex = 61;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(58,237);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(187,18);
			this.label9.TabIndex = 60;
			this.label9.Text = "Synch Status";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butStart
			// 
			this.butStart.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butStart.Autosize = true;
			this.butStart.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butStart.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butStart.CornerRadius = 4F;
			this.butStart.Location = new System.Drawing.Point(180,259);
			this.butStart.Name = "butStart";
			this.butStart.Size = new System.Drawing.Size(62,22);
			this.butStart.TabIndex = 62;
			this.butStart.Text = "Start";
			this.butStart.Click += new System.EventHandler(this.butStart_Click);
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(83,290);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(162,17);
			this.label10.TabIndex = 64;
			this.label10.Text = "Notes";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textNote
			// 
			this.textNote.Location = new System.Drawing.Point(246,287);
			this.textNote.MaxLength = 255;
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.Size = new System.Drawing.Size(275,80);
			this.textNote.TabIndex = 63;
			// 
			// FormUAppoint
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(677,415);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.butStart);
			this.Controls.Add(this.textSynchStatus);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.textDateTimeLastUploaded);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.textIntervalSeconds);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.textWorkstationName);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.textPassword);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textUsername);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textPath);
			this.Controls.Add(this.textProgDesc);
			this.Controls.Add(this.textProgName);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.checkEnabled);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormUAppoint";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "UAppoint Setup";
			this.Load += new System.EventHandler(this.FormUAppoint_Load);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormProgramLinkEdit_Closing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormUAppoint_Load(object sender, System.EventArgs e) {
			FillForm();
		}

		private void FillForm(){
			ProgramProperties.Refresh();
			textProgName.Text=ProgramCur.ProgName;
			textProgDesc.Text=ProgramCur.ProgDesc;
			checkEnabled.Checked=ProgramCur.Enabled;
			textPath.Text=ProgramCur.Path;
			PropertyList=ProgramProperties.GetListForProgram(ProgramCur.ProgramNum);
			textUsername.Text=GetProp("Username");
			textPassword.Text=GetProp("Password");
			textWorkstationName.Text=GetProp("WorkstationName");
			textIntervalSeconds.Text=GetProp("IntervalSeconds");
			DateTime datet=PIn.PDateT(GetProp("DateTimeLastUploaded"));
			if(datet.Year>1880){
				textDateTimeLastUploaded.Text=datet.ToShortDateString()+"  "+datet.ToShortTimeString();
			}
			textSynchStatus.Text=GetProp("SynchStatus");
			textNote.Text=ProgramCur.Note;
		}

		private string GetProp(string desc){
			for(int i=0;i<PropertyList.Count;i++){
				if(PropertyList[i].PropertyDesc==desc){
					return PropertyList[i].PropertyValue;
				}
			}
			throw new ApplicationException("Property not found: "+desc);
		}

		private void butStart_Click(object sender,EventArgs e) {
			if(!SaveToDb()){
				return;
			}
			string serverName=ProgramCur.Path;
			HttpWebRequest webReq=(HttpWebRequest)WebRequest.Create(serverName);
			string postData="<PracticeClient user=\""+GetProp("Username")+"\" "
				+" pass-md5=\""+GetProp("Password")+"\">"
				+"<patient action=\"insert\" id=\"1101\" "
        +"name-first=\"Markk\" name-last=\"Jeffcoat\" />"
				/*+"<appointment action=\"insert\" id=\"221\" patient-id=\"1101\" "
                     +"provider-id=\"DDS1\" operatory-id=\"OP01\""
                     +"start=\"Jan 5, 2007 11:45:00 AM\" length=\"45\""
                     +"procedure-code=\"D0110\""
                     +"description=\"New Patient Exam\" />"*/
				+" </PracticeClient>";
			webReq.KeepAlive=false;
			webReq.Method="POST";
			webReq.ContentType="application/x-www-form-urlencoded";
			webReq.ContentLength=postData.Length;
			ASCIIEncoding encoding=new ASCIIEncoding();
			byte[] bytes=encoding.GetBytes(postData);
			Stream streamOut=webReq.GetRequestStream();
			streamOut.Write(bytes,0,bytes.Length);
			streamOut.Close();
			WebResponse response=webReq.GetResponse();
			//Process the response:
			StreamReader readStream=new StreamReader(response.GetResponseStream(),Encoding.ASCII);
			string str=readStream.ReadToEnd();
			readStream.Close();
			MessageBox.Show(str);
		}

		private bool SaveToDb(){
			if(textProgDesc.Text==""){
				MessageBox.Show("Description may not be blank.");
				return false;
			}
			if(checkEnabled.Checked && textPath.Text==""){
				MessageBox.Show("URL may not be blank.");
				return false;
			}
			//check for valid url?
			if(checkEnabled.Checked && textUsername.Text==""){
				MessageBox.Show("Username may not be blank.");
				return false;
			}
			if(checkEnabled.Checked && textPassword.Text==""){
				MessageBox.Show("Password may not be blank.");
				return false;
			}
			if(checkEnabled.Checked && textWorkstationName.Text==""){
				MessageBox.Show("Workstation name may not be blank.");
				return false;
			}
			if(checkEnabled.Checked && Environment.MachineName!=textWorkstationName.Text.ToUpper()){
				MessageBox.Show("This workstation is: "+Environment.MachineName+".  The workstation entered does not match.\r\n"
					+"UAppoint setup should only be performed from the workstation responsible for synch.");
				return false;
			}
			int intervalSec=0;
			try{
				intervalSec=PIn.PInt(textIntervalSeconds.Text);//"" is handled fine here
			}
			catch{
				MessageBox.Show("Please fix the interval in seconds.");
				return false;
			}
			if(checkEnabled.Checked && intervalSec<1){
				MessageBox.Show("Interval in seconds must be greater than zero.");
				return false;
			}
			/*DateTime datetime=DateTime.MinValue;
			if(textDateTimeLastUploaded.Text!=""){
				try{
					datetime=DateTime.Parse(textDateTimeLastUploaded.Text);
				}
				catch{
					MessageBox.Show("Please fix the DateTime last uploaded.");
					return false;
				}
			}*/
			ProgramCur.ProgDesc=textProgDesc.Text;
			ProgramCur.Enabled=checkEnabled.Checked;
			ProgramCur.Path=textPath.Text;
			ProgramCur.Note=textNote.Text;
			Programs.Update(ProgramCur);
			ProgramProperties.SetProperty(ProgramCur.ProgramNum,"Username",textUsername.Text);
			ProgramProperties.SetProperty(ProgramCur.ProgramNum,"Password",textPassword.Text);
			ProgramProperties.SetProperty(ProgramCur.ProgramNum,"WorkstationName",textWorkstationName.Text.ToUpper());
			ProgramProperties.SetProperty(ProgramCur.ProgramNum,"IntervalSeconds",intervalSec.ToString());
			//ProgramProperties.SetProperty(ProgramCur.ProgramNum,"DateTimeLastUploaded",POut.PDateT(datetime));
			DataValid.SetInvalid(InvalidType.Programs);
			return true;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(!SaveToDb()){
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormProgramLinkEdit_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			
		}

	

		

		

		
		


	}
}





















