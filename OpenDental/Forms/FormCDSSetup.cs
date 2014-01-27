using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.Forms;
using System.Xml;
using System.Threading;
using System.Net;
using System.IO;
using Ionic.Zip;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormCDSSetup:Form {
		private List<CDSPermission> _listCdsPermissions;

		public FormCDSSetup() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormCDSSetup_Load(object sender,EventArgs e) {
			_listCdsPermissions=CDSPermissions.GetAll();
			FillGrid();
		}

		private void gridMain_CellClick(object sender,ODGridClickEventArgs e) {
			if(e.Col<2) {
				return;//name and group name columns.
			}
			for(int i=0;i<_listCdsPermissions.Count;i++) {
				if(_listCdsPermissions[i].CDSPermissionNum!=(long)gridMain.Rows[e.Row].Tag) {
					continue;
				}
				switch(e.Col){
					case 0:
					case 1:
						//should never happen.
						break;
					case 2:
						_listCdsPermissions[i].ShowCDS=!_listCdsPermissions[i].ShowCDS;
						break;
					case 3:
						_listCdsPermissions[i].SetupCDS=!_listCdsPermissions[i].SetupCDS;
						break;
					case 4:
						_listCdsPermissions[i].AccessBibliography=!_listCdsPermissions[i].AccessBibliography;
						break;
					case 5:
						_listCdsPermissions[i].ProblemCDS=!_listCdsPermissions[i].ProblemCDS;
						break;
					case 6:
						_listCdsPermissions[i].MedicationCDS=!_listCdsPermissions[i].MedicationCDS;
						break;
					case 7:
						_listCdsPermissions[i].AllergyCDS=!_listCdsPermissions[i].AllergyCDS;
						break;
					case 8:
						_listCdsPermissions[i].DemographicCDS=!_listCdsPermissions[i].DemographicCDS;
						break;
					case 9:
						_listCdsPermissions[i].LabTestCDS=!_listCdsPermissions[i].LabTestCDS;
						break;
					case 10:
						_listCdsPermissions[i].VitalCDS=!_listCdsPermissions[i].VitalCDS;
						break;
					default:
						//should never happen.
						break;
				}
				break;
			}
			FillGrid();
		}

		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn("User Name",120);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Group Name",120);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Show CDS",80,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Edit CDS",80,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Source",80,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Problem",80,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Medication",80,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Allergy",80,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Demographic",80,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Labs",80,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Vitals",80,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			List<Userod> ListUsers=Userods.GetNotHidden();
			UserGroup[] ArrayGroups=UserGroups.List;
			//if(radioUser.Checked) {//by user
			for(int i=0;i<ListUsers.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(ListUsers[i].UserName);
				for(int g=0;g<ArrayGroups.Length;g++) {//group name.
					if(ListUsers[i].UserGroupNum!=ArrayGroups[g].UserGroupNum) {
						continue;
					}
					row.Cells.Add(ArrayGroups[g].Description);
					break;
				}
				for(int p=0;p<_listCdsPermissions.Count;p++) {
					if(ListUsers[i].UserNum!=_listCdsPermissions[p].UserNum) {
						continue;
					}
					row.Cells.Add((_listCdsPermissions[p].ShowCDS						?"X":""));//"X" if user has permission
					row.Cells.Add((_listCdsPermissions[p].SetupCDS					?"X":""));//"X" if user has permission
					row.Cells.Add((_listCdsPermissions[p].AccessBibliography?"X":""));//"X" if user has permission
					row.Cells.Add((_listCdsPermissions[p].ProblemCDS				?"X":""));//"X" if user has permission
					row.Cells.Add((_listCdsPermissions[p].MedicationCDS			?"X":""));//"X" if user has permission
					row.Cells.Add((_listCdsPermissions[p].AllergyCDS				?"X":""));//"X" if user has permission
					row.Cells.Add((_listCdsPermissions[p].DemographicCDS		?"X":""));//"X" if user has permission
					row.Cells.Add((_listCdsPermissions[p].LabTestCDS				?"X":""));//"X" if user has permission
					row.Cells.Add((_listCdsPermissions[p].VitalCDS					?"X":""));//"X" if user has permission
					row.Tag=_listCdsPermissions[p].CDSPermissionNum;//used to edit correct permission.
					break;
				}
				gridMain.Rows.Add(row);
			}
			//}
			//else {//by user group
			//	for(int g=0;g<ArrayGroups.Length;g++) {
			//		row=new ODGridRow();
			//		row.Cells.Add("");//No User Name
			//		row.Cells.Add(ArrayGroups[g].Description);
						//TODO: Later. No time now for group level permission editing.
			//		gridMain.Rows.Add(row);
			//	}
			//}
			gridMain.EndUpdate();
		}

		private void butOk_Click(object sender,EventArgs e) {
			for(int i=0;i<_listCdsPermissions.Count;i++) {
				//TODO:instead of updating all permissions. Update only the permissions neccesary.
				CDSPermissions.Update(_listCdsPermissions[i]);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}


		

	}
}