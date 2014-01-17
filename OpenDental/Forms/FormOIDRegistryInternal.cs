using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormOIDRegistryInternal:Form {
		private List<OIDInternal> ListOIDInternal;
		///<summary>Used for refence to construct reccomended values.</summary>
		public string rootOIDString;

		public FormOIDRegistryInternal() {
			InitializeComponent();
		}

		private void FormReminders_Load(object sender,EventArgs e) {
			OIDInternals.InsertMissingValues();
			ListOIDInternal=OIDInternals.GetAll();
			ListOIDInternal.Sort(sortOIDsByIDType);
			rootOIDString=OIDInternals.GetForType(IdentifierType.Root).IDRoot;
			FillGrid();
		}

		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn("Type",100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Reccomended Value",200);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Actual Value",200);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<ListOIDInternal.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(ListOIDInternal[i].IDType.ToString());
				switch(ListOIDInternal[i].IDType) {
					case IdentifierType.Root:
						row.Cells.Add("See Manual");//TODO:maybe enhance this. 
						break;
					case IdentifierType.LabOrder:
						row.Cells.Add(rootOIDString+".1");
						break;
					case IdentifierType.Patient:
						row.Cells.Add(rootOIDString+".2");
						break;
					case IdentifierType.Provider:
						row.Cells.Add(rootOIDString+".3");
						break;
					default://should never happen
						row.Cells.Add("");
						break;
				}
				row.Cells.Add(ListOIDInternal[i].IDRoot);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			InputBox ipb=new InputBox("New OID");
			ipb.textResult.Text=ListOIDInternal[e.Row].IDRoot;
			ipb.ShowDialog();
			if(ipb.DialogResult!=DialogResult.OK) {
				return;
			}
			if(e.Row==0) {
				rootOIDString=ipb.textResult.Text;
				//if(MsgBox.Show(this,MsgBoxButtons.YesNo,"Would you like to use default values for OIDs based of of this root OID?")) {
				//	for(int i=0;i<ListOIDInternal.Count;i++) {
				//		switch(ListOIDInternal[i].IDType) {
				//			case IdentifierType.Root:

				//				break;
				//			case IdentifierType.LabOrder:
				//				row.Cells.Add(rootOIDString+".1");
				//				break;
				//			case IdentifierType.Patient:
				//				row.Cells.Add(rootOIDString+".2");
				//				break;
				//			case IdentifierType.Provider:
				//				row.Cells.Add(rootOIDString+".3");
				//				break;
				//			default://should never happen
				//				row.Cells.Add("");
				//				break;
				//		}
				//		row.Cells.Add(ListOIDInternal[i].IDRoot);
				//		gridMain.Rows.Add(row);
				//	}
				//}
			}
			ListOIDInternal[e.Row].IDRoot=ipb.textResult.Text;
			FillGrid();
		}

		private int sortOIDsByIDType(OIDInternal a,OIDInternal b) {
			if(a.IDType>b.IDType) {
				return 1;
			}
			if(a.IDType<b.IDType) {
				return -1;
			}
			return 0;//should never happen.
		}

		private void butOk_Click(object sender,EventArgs e) {
			//TODO: Validate OIDs and provide warning s if they do not make sense. For instance, if the Actual values do not match the reccomended values.
			//Also if the other OIDs are not children of the root OID.  etc...
			for(int i=0;i<ListOIDInternal.Count;i++) {
				OIDInternals.Update(ListOIDInternal[i]);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}



	}
}
