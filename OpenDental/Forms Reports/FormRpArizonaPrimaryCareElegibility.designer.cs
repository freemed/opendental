namespace OpenDental{
	partial class FormRpArizonaPrimaryCareElegibility {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources=new System.ComponentModel.ComponentResourceManager(typeof(FormRpArizonaPrimaryCareElegibility));
			this.textElegibilityFolder=new System.Windows.Forms.TextBox();
			this.label1=new System.Windows.Forms.Label();
			this.textLog=new System.Windows.Forms.TextBox();
			this.label2=new System.Windows.Forms.Label();
			this.butBrowse=new OpenDental.UI.Button();
			this.butFinished=new OpenDental.UI.Button();
			this.butCancel=new OpenDental.UI.Button();
			this.folderElegibilityPath=new System.Windows.Forms.FolderBrowserDialog();
			this.label3=new System.Windows.Forms.Label();
			this.textElegibilityFile=new System.Windows.Forms.TextBox();
			this.butCopy=new OpenDental.UI.Button();
			this.butRun=new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// textElegibilityFolder
			// 
			this.textElegibilityFolder.Location=new System.Drawing.Point(12,31);
			this.textElegibilityFolder.Name="textElegibilityFolder";
			this.textElegibilityFolder.Size=new System.Drawing.Size(607,20);
			this.textElegibilityFolder.TabIndex=4;
			this.textElegibilityFolder.Text="C:\\Temp";
			// 
			// label1
			// 
			this.label1.AutoSize=true;
			this.label1.Location=new System.Drawing.Point(12,12);
			this.label1.Name="label1";
			this.label1.Size=new System.Drawing.Size(77,13);
			this.label1.TabIndex=6;
			this.label1.Text="Save report to:";
			// 
			// textLog
			// 
			this.textLog.Location=new System.Drawing.Point(12,116);
			this.textLog.Multiline=true;
			this.textLog.Name="textLog";
			this.textLog.ReadOnly=true;
			this.textLog.ScrollBars=System.Windows.Forms.ScrollBars.Vertical;
			this.textLog.Size=new System.Drawing.Size(607,284);
			this.textLog.TabIndex=8;
			// 
			// label2
			// 
			this.label2.AutoSize=true;
			this.label2.Location=new System.Drawing.Point(13,99);
			this.label2.Name="label2";
			this.label2.Size=new System.Drawing.Size(60,13);
			this.label2.TabIndex=9;
			this.label2.Text="Report Log";
			// 
			// butBrowse
			// 
			this.butBrowse.AdjustImageLocation=new System.Drawing.Point(0,0);
			this.butBrowse.Anchor=((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Right)));
			this.butBrowse.Autosize=true;
			this.butBrowse.BtnShape=OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBrowse.BtnStyle=OpenDental.UI.enumType.XPStyle.Silver;
			this.butBrowse.CornerRadius=4F;
			this.butBrowse.Location=new System.Drawing.Point(625,28);
			this.butBrowse.Name="butBrowse";
			this.butBrowse.Size=new System.Drawing.Size(75,24);
			this.butBrowse.TabIndex=7;
			this.butBrowse.Text="Browse";
			this.butBrowse.Click+=new System.EventHandler(this.butBrowse_Click);
			// 
			// butFinished
			// 
			this.butFinished.AdjustImageLocation=new System.Drawing.Point(0,0);
			this.butFinished.Anchor=((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Right)));
			this.butFinished.Autosize=true;
			this.butFinished.BtnShape=OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butFinished.BtnStyle=OpenDental.UI.enumType.XPStyle.Silver;
			this.butFinished.CornerRadius=4F;
			this.butFinished.Location=new System.Drawing.Point(625,335);
			this.butFinished.Name="butFinished";
			this.butFinished.Size=new System.Drawing.Size(75,24);
			this.butFinished.TabIndex=3;
			this.butFinished.Text="&Finished";
			this.butFinished.Click+=new System.EventHandler(this.butFinished_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation=new System.Drawing.Point(0,0);
			this.butCancel.Anchor=((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize=true;
			this.butCancel.BtnShape=OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle=OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius=4F;
			this.butCancel.Location=new System.Drawing.Point(625,376);
			this.butCancel.Name="butCancel";
			this.butCancel.Size=new System.Drawing.Size(75,24);
			this.butCancel.TabIndex=2;
			this.butCancel.Text="&Cancel";
			this.butCancel.Click+=new System.EventHandler(this.butCancel_Click);
			// 
			// label3
			// 
			this.label3.AutoSize=true;
			this.label3.Location=new System.Drawing.Point(13,57);
			this.label3.Name="label3";
			this.label3.Size=new System.Drawing.Size(89,13);
			this.label3.TabIndex=10;
			this.label3.Text="Output File Name";
			// 
			// textElegibilityFile
			// 
			this.textElegibilityFile.Location=new System.Drawing.Point(12,74);
			this.textElegibilityFile.Name="textElegibilityFile";
			this.textElegibilityFile.ReadOnly=true;
			this.textElegibilityFile.Size=new System.Drawing.Size(164,20);
			this.textElegibilityFile.TabIndex=11;
			this.textElegibilityFile.Text="ApcElegibility.txt";
			// 
			// butCopy
			// 
			this.butCopy.AdjustImageLocation=new System.Drawing.Point(0,0);
			this.butCopy.Anchor=((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Right)));
			this.butCopy.Autosize=true;
			this.butCopy.BtnShape=OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCopy.BtnStyle=OpenDental.UI.enumType.XPStyle.Silver;
			this.butCopy.CornerRadius=4F;
			this.butCopy.Location=new System.Drawing.Point(625,245);
			this.butCopy.Name="butCopy";
			this.butCopy.Size=new System.Drawing.Size(83,24);
			this.butCopy.TabIndex=12;
			this.butCopy.Text="Copy Log Text";
			this.butCopy.Click+=new System.EventHandler(this.butCopy_Click);
			// 
			// butRun
			// 
			this.butRun.AdjustImageLocation=new System.Drawing.Point(0,0);
			this.butRun.Anchor=((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Right)));
			this.butRun.Autosize=true;
			this.butRun.BtnShape=OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRun.BtnStyle=OpenDental.UI.enumType.XPStyle.Silver;
			this.butRun.CornerRadius=4F;
			this.butRun.Location=new System.Drawing.Point(625,291);
			this.butRun.Name="butRun";
			this.butRun.Size=new System.Drawing.Size(75,24);
			this.butRun.TabIndex=13;
			this.butRun.Text="Run";
			this.butRun.Click+=new System.EventHandler(this.butRun_Click);
			// 
			// FormRpArizonaPrimaryCareElegibility
			// 
			this.AutoScaleMode=System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize=new System.Drawing.Size(725,427);
			this.Controls.Add(this.butRun);
			this.Controls.Add(this.butCopy);
			this.Controls.Add(this.textElegibilityFile);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textLog);
			this.Controls.Add(this.butBrowse);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textElegibilityFolder);
			this.Controls.Add(this.butFinished);
			this.Controls.Add(this.butCancel);
			this.Icon=((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name="FormRpArizonaPrimaryCareElegibility";
			this.StartPosition=System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text="Arizona Primary Care Elegibility File Report";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butFinished;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.TextBox textElegibilityFolder;
		private System.Windows.Forms.Label label1;
		private OpenDental.UI.Button butBrowse;
		private System.Windows.Forms.TextBox textLog;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.FolderBrowserDialog folderElegibilityPath;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textElegibilityFile;
		private OpenDental.UI.Button butCopy;
		private OpenDental.UI.Button butRun;
	}
}