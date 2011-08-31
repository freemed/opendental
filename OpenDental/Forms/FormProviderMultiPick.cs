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
	public partial class FormProvidersMultiPick:Form {
		public List<Provider> SelectedProviders;

		public FormProvidersMultiPick() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormProvidersMultiPick_Load(object sender,EventArgs e) {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableProviders","Abbrev"),90);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProviders","Last Name"),90);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProviders","First Name"),90);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<ProviderC.ListShort.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(ProviderC.ListShort[i].Abbr);
				row.Cells.Add(ProviderC.ListShort[i].LName);
				row.Cells.Add(ProviderC.ListShort[i].FName);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			for(int i=0;i<ProviderC.ListShort.Count;i++) {
				if(SelectedProviders.Contains(ProviderC.ListShort[i])) {
					gridMain.SetSelected(i,true);
				}
			}
		}


		private void butProvDentist_Click(object sender,EventArgs e) {
			SelectedProviders=new List<Provider>();
			for(int i=0;i<ProviderC.ListShort.Count;i++) {
				if(!ProviderC.ListShort[i].IsSecondary) {
					SelectedProviders.Add(ProviderC.ListShort[i]);
					gridMain.SetSelected(i,true);
				}
				else {
					gridMain.SetSelected(i,false);
				}
			}
		}

		private void butProvHygenist_Click(object sender,EventArgs e) {
			SelectedProviders=new List<Provider>();
			for(int i=0;i<ProviderC.ListShort.Count;i++) {
				if(ProviderC.ListShort[i].IsSecondary) {
					SelectedProviders.Add(ProviderC.ListShort[i]);
					gridMain.SetSelected(i,true);
				}
				else {
					gridMain.SetSelected(i,false);
				}
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			SelectedProviders=new List<Provider>();
			for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
				SelectedProviders.Add(ProviderC.ListShort[gridMain.SelectedIndices[i]]);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		
	}
}