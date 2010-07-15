using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormApptViewItemEdit:Form {
		public ApptViewItem ApptVItem;

		public FormApptViewItemEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormApptViewItemEdit_Load(object sender,EventArgs e) {
			textDesc.Text=ApptVItem.ElementDesc;
			panelColor.BackColor=ApptVItem.ElementColor;
			for(int i=0;i<Enum.GetNames(typeof(ApptViewAlignment)).Length;i++) {
				listAlignment.Items.Add(Enum.GetNames(typeof(ApptViewAlignment))[i]);
			}
			listAlignment.SelectedIndex=(int)ApptVItem.ElementAlignment;
		}

		private void butColor_Click(object sender,EventArgs e) {
			ColorDialog colorDialog1=new ColorDialog();
			colorDialog1.Color=panelColor.BackColor;
			if(colorDialog1.ShowDialog()!=DialogResult.OK) {
				return;
			}
			panelColor.BackColor=colorDialog1.Color;
		}

		private void butOK_Click(object sender,EventArgs e) {
			ApptVItem.ElementColor=panelColor.BackColor;
			ApptVItem.ElementAlignment=(ApptViewAlignment)listAlignment.SelectedIndex;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		
	}
}