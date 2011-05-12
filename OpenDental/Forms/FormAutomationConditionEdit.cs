using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormAutomationConditionEdit:Form {
		public bool IsNew;
		public AutomationCondition ConditionCur;

		public FormAutomationConditionEdit() {
			InitializeComponent();
			Lan.F(this);

		}

		private void FormAutomationConditionEdit_Load(object sender,EventArgs e) {
			for(int i=0;i<Enum.GetNames(typeof(AutoCondField)).Length;i++) {
				listCompareField.Items.Add(Enum.GetNames(typeof(AutoCondField))[i]);
				listCompareField.SelectedIndex=0;
			}
			for(int i=0;i<Enum.GetNames(typeof(AutoCondComparison)).Length;i++) {
				listComparison.Items.Add(Enum.GetNames(typeof(AutoCondComparison))[i]);
				listComparison.SelectedIndex=0;
			}
			if(!IsNew) {
				textCompareString.Text=ConditionCur.CompareString;
				listCompareField.SelectedIndex=(int)ConditionCur.CompareField;
				listComparison.SelectedIndex=(int)ConditionCur.Comparison;
			}
		}

		///<summary>Logic might get more complex as fields and comparisons are added so a seperate function was made.</summary>
		private bool ReasonableLogic() {
			AutoCondComparison comp=(AutoCondComparison)listComparison.SelectedIndex;
			AutoCondField cond=(AutoCondField)listCompareField.SelectedIndex;
			//So far Age is only thing that allows GreaterThan or LessThan.
			if(cond!=AutoCondField.Age) {
				if(comp==AutoCondComparison.GreaterThan || comp==AutoCondComparison.LessThan) {
					return false;
				}
			}
			return true;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(IsNew) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(!MsgBox.Show(this,true,"Delete this condition?")) {
				return;
			}
			try {
				AutomationConditions.Delete(ConditionCur.AutomationConditionNum);
				DialogResult=DialogResult.OK;
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(textCompareString.Text.Trim()=="") {
				MsgBox.Show(this,"Text not allowed to be blank.");
				return;
			}
			if(!ReasonableLogic()) {
				MsgBox.Show(this,"Comparison does not make sense with chosen field.");
				return;
			}
			if(((AutoCondField)listCompareField.SelectedIndex==AutoCondField.Gender
				&& !(textCompareString.Text.ToLower()=="m" || textCompareString.Text.ToLower()=="f"))) 
			{
				MsgBox.Show(this,"Allowed gender values are M or F.");
				return;
			}
			ConditionCur.CompareString=textCompareString.Text;
			ConditionCur.CompareField=(AutoCondField)listCompareField.SelectedIndex;
			ConditionCur.Comparison=(AutoCondComparison)listComparison.SelectedIndex;
			if(IsNew) {
				AutomationConditions.Insert(ConditionCur);
			}
			else {
				AutomationConditions.Update(ConditionCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}


	}
}