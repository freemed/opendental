using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	/// <summary></summary>
	public partial class FormInnoDb:System.Windows.Forms.Form {

		/// <summary></summary>
		public FormInnoDb() {
			InitializeComponent();
			Lan.C(this,new System.Windows.Forms.Control[]{
				this.textBox1
			});
			Lan.F(this);
		}

		private void FormInnoDb_Load(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			StringBuilder strB=new StringBuilder();
			strB.Append('-',60);
			textBox1.Text=DateTime.Now.ToString()+strB.ToString()+"\r\n";
			Application.DoEvents();
			textBox1.Text+=Lans.g("FormInnoDb","Default Storage Engine: "+InnoDb.GetDefaultEngine().ToString()+"\r\n");
			Application.DoEvents();
			textBox1.Text+=InnoDb.GetEngineCount();
			Application.DoEvents();
			Cursor=Cursors.Default;
		}

		private void butToMyIsam_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"This will convert all tables in the database to the MyISAM storage engine.  This may take several minutes.\r\nContinue?")) {
				return;
			}
			if(InnoDb.GetDefaultEngine()=="InnoDB") {
				MsgBox.Show("FormInnoDB","You will need to change your default storage engine to MyISAM.  Replace the line in your my.ini file that says "+
					"\"default-storage-engine=InnoDB\" with \"default-storage-engine=MyISAM\".  Then restart the MySQL service within the service manager.");
				return;
			}
			try {
				MiscData.MakeABackup();
			}
			catch(Exception ex) {
				if(ex.Message!="") {
					MessageBox.Show(ex.Message);
				}
				MsgBox.Show("FormInnoDb","Backup failed. Your database has not been altered.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			textBox1.Text+=Lans.g("FormInnoDb","Default Storage Engine: "+InnoDb.GetDefaultEngine().ToString()+"\r\n");
			Application.DoEvents();
			int numchanged=InnoDb.ConvertTables("InnoDB","MyISAM");
			textBox1.Text+=Lan.g("FormInnoDb","Number of tables converted to MyIsam: ")+numchanged.ToString()+"\r\n";
			Application.DoEvents();
			textBox1.Text+=InnoDb.GetEngineCount();
			Application.DoEvents();
			Cursor=Cursors.Default;
		}

		private void butToInnoDb_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"This will convert all tables in the database to the InnoDB storage engine.  This may take several minutes.\r\nContinue?")) {
				return;
			}
			if(!InnoDb.IsInnodbAvail()) {
				MsgBox.Show("FormInnoDb","InnoDB storage engine is disabled.  In order for InnoDB tables to work you must remove the line in your my.ini file that says \"skip-innodb\""+
				" and remove the line \"default-storage-engine=MyISAM\" if present.  Then restart the MySQL service within the service manager.");
				return;
			}
			if(InnoDb.GetDefaultEngine()=="MyISAM") {
				MsgBox.Show("FormInnoDb","You will need to change your default storage engine to InnoDB.  Remove the line in your my.ini file that says "+
					"\"default-storage-engine=MyISAM\".  Then restart the MySQL service within the service manager.");
				return;
			}
			try {
				MiscData.MakeABackup();
			}
			catch(Exception ex) {
				if(ex.Message!="") {
					MessageBox.Show(ex.Message);
				}
				MsgBox.Show("FormInnoDb","Backup failed. Your database has not been altered.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			textBox1.Text+=Lans.g("FormInnoDb","Default Storage Engine: "+InnoDb.GetDefaultEngine().ToString()+"\r\n");
			Application.DoEvents();
			int numchanged=InnoDb.ConvertTables("MyISAM","InnoDB");
			textBox1.Text+=Lan.g("FormInnoDb","Number of tables converted to InnoDB: ")+numchanged.ToString()+"\r\n";
			Application.DoEvents();
			textBox1.Text+=InnoDb.GetEngineCount();
			Application.DoEvents();
			Cursor=Cursors.Default;
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}