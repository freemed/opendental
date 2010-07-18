using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormApptQuickAdd:Form {
		///<summary>If form closes with OK, this contains selected code nums.  All codeNums will already have been checked for validity.</summary>
		public List<long> SelectedCodeNums;
		public Point ParentFormLocation;

		///<summary>Security handled in calling form.</summary>
		public FormApptQuickAdd() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormApptQuickAdd_Load(object sender,EventArgs e) {
			for(int i=0;i<DefC.Short[(int)DefCat.ApptProcsQuickAdd].Length;i++) {
				listQuickAdd.Items.Add(DefC.Short[(int)DefCat.ApptProcsQuickAdd][i].ItemName);
			}
			this.Location=new Point(this.ParentFormLocation.X+75,this.ParentFormLocation.Y+25);
		}

		private void FormApptQuickAdd_Shown(object sender,EventArgs e) {
			
		}

		private void listQuickAdd_DoubleClick(object sender,EventArgs e) {
			if(listQuickAdd.SelectedIndices.Count==0) {
				return;
			}
			SelectedCodeNums=new List<long>();
			string[] procCodeStrArray=DefC.Short[(int)DefCat.ApptProcsQuickAdd][listQuickAdd.SelectedIndices[0]].ItemValue.Split(',');
			long codeNum;
			for(int i=0;i<procCodeStrArray.Length;i++) {
				codeNum=ProcedureCodes.GetCodeNum(procCodeStrArray[i]);
				if(codeNum==0){
					MessageBox.Show(Lan.g(this,"Definition contains invalid code: ")+procCodeStrArray[i]);
					return;
				}
				SelectedCodeNums.Add(codeNum);
			}
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(listQuickAdd.SelectedIndices.Count==0) {
				MsgBox.Show(this,"Please select items first.");
				return;
			}
			SelectedCodeNums=new List<long>();
			string[] procCodeStrArray;
			long codeNum;
			for(int s=0;s<listQuickAdd.SelectedIndices.Count;s++) {
				procCodeStrArray=DefC.Short[(int)DefCat.ApptProcsQuickAdd][listQuickAdd.SelectedIndices[s]].ItemValue.Split(',');
				for(int i=0;i<procCodeStrArray.Length;i++) {
					codeNum=ProcedureCodes.GetCodeNum(procCodeStrArray[i]);
					if(codeNum==0) {
						MessageBox.Show(Lan.g(this,"Definition contains invalid code: ")+procCodeStrArray[i]);
						return;
					}
					SelectedCodeNums.Add(codeNum);
				}
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		
	}
}