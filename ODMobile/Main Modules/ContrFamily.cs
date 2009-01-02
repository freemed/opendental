using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OpenDentMobile.UI;

namespace OpenDentMobile {
	public partial class ContrFamily:UserControl {
		private Patient PatCur;

		public ContrFamily() {
			InitializeComponent();
		}

		///<summary></summary>
		public void ModuleSelected(int patNum){
			RefreshModuleData(patNum);
			RefreshModuleScreen();
		}

		private void RefreshModuleData(int patNum){
			if(patNum==0){
				PatCur=null;
				//FamCur=null;
				return;
			}
			//FamCur=Patients.GetFamily(patNum);
			PatCur=Patients.GetPat(patNum);
				//FamCur.GetPatient(patNum);
		}

		private void RefreshModuleScreen(){
			FillPatientData();
			//FillFamilyData();
		} 

		private void FillPatientData(){
			if(PatCur==null){
				gridPat.BeginUpdate();
				gridPat.Rows.Clear();
				gridPat.Columns.Clear();
				gridPat.EndUpdate();
				return;
			}
			gridPat.BeginUpdate();
			gridPat.Columns.Clear();
			ODGridColumn col=new ODGridColumn("",90);
			gridPat.Columns.Add(col);
			col=new ODGridColumn("",150);
			gridPat.Columns.Add(col);
			gridPat.Rows.Clear();
			ODGridRow row;
			row=new ODGridRow();
			row.Cells.Add("PatNum");
			row.Cells.Add(PatCur.PatNum.ToString());
			gridPat.Rows.Add(row);
			row=new ODGridRow();
			row.Cells.Add("LName");
			row.Cells.Add(PatCur.LName);
			gridPat.Rows.Add(row);
			row=new ODGridRow();
			row.Cells.Add("FName");
			row.Cells.Add(PatCur.FName);
			gridPat.Rows.Add(row);
			row=new ODGridRow();
			row.Cells.Add("Preferred");
			row.Cells.Add(PatCur.Preferred);
			gridPat.Rows.Add(row);
			row=new ODGridRow();
			row.Cells.Add("PatStatus");
			row.Cells.Add(PatCur.PatStatus.ToString());
			gridPat.Rows.Add(row);
			row=new ODGridRow();
			row.Cells.Add("Gender");
			row.Cells.Add(PatCur.Gender.ToString());
			gridPat.Rows.Add(row);
			row=new ODGridRow();
			row.Cells.Add("Position");
			row.Cells.Add(PatCur.Position.ToString());
			gridPat.Rows.Add(row);
			row=new ODGridRow();
			row.Cells.Add("Birthdate");
			if(PatCur.Birthdate.Year<1880){
				row.Cells.Add("");
			}
			else{
				row.Cells.Add(PatCur.Birthdate.ToShortDateString());
			}
			gridPat.Rows.Add(row);
			row=new ODGridRow();
			row.Cells.Add("Address");
			string addr=PatCur.Address;
			if(PatCur.Address2!=""){
				addr+="\r\n"+PatCur.Address2;
			}
			row.Cells.Add(addr);
			gridPat.Rows.Add(row);
			row=new ODGridRow();
			row.Cells.Add("City");
			row.Cells.Add(PatCur.City);
			gridPat.Rows.Add(row);
			row=new ODGridRow();
			row.Cells.Add("State");
			row.Cells.Add(PatCur.State);
			gridPat.Rows.Add(row);
			row=new ODGridRow();
			row.Cells.Add("Home Phone");
			row.Cells.Add(PatCur.HmPhone);
			gridPat.Rows.Add(row);
			row=new ODGridRow();
			row.Cells.Add("Work Phone");
			row.Cells.Add(PatCur.WkPhone);
			gridPat.Rows.Add(row);
			row=new ODGridRow();
			row.Cells.Add("Wireless");
			row.Cells.Add(PatCur.WirelessPhone);
			gridPat.Rows.Add(row);
			row=new ODGridRow();
			row.Cells.Add("CreditType");
			row.Cells.Add(PatCur.CreditType);
			gridPat.Rows.Add(row);
			row=new ODGridRow();
			row.Cells.Add("FamFinUrgNote");
			row.Cells.Add(PatCur.FamFinUrgNote);
			gridPat.Rows.Add(row);
			row=new ODGridRow();
			row.Cells.Add("MedUrgNote");
			row.Cells.Add(PatCur.MedUrgNote);
			gridPat.Rows.Add(row);
			row=new ODGridRow();
			row.Cells.Add("PrimaryInsurance");
			row.Cells.Add(PatCur.PrimaryInsurance);
			gridPat.Rows.Add(row);
			gridPat.EndUpdate();
			//gridPat.Height=gridPat.
		}


	}
}
