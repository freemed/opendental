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
	public partial class FormMedicationReconcile:Form {
		public Patient PatCur;
		private Bitmap BitmapOriginal;

		public FormMedicationReconcile() {
			InitializeComponent();
			Lan.F(this);
		}

		private void BasicTemplate_Load(object sender,EventArgs e) {
			FillMeds();
		}

		private void FillMeds() {
			Medications.Refresh();
			MedicationPats.Refresh(PatCur.PatNum);
			gridMeds.BeginUpdate();
			gridMeds.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableMedications","Medication"),140);
			gridMeds.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableMedications","Notes for Patient"),225);
			gridMeds.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableMedications","Disc"),10,HorizontalAlignment.Center);
			gridMeds.Columns.Add(col);
			gridMeds.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<MedicationPats.List.Length;i++) {
				if(!checkDiscontinued.Checked && MedicationPats.List[i].IsDiscontinued) {
					continue;
				}
				row=new ODGridRow();
				Medication generic=Medications.GetGeneric(MedicationPats.List[i].MedicationNum);
				string medName=Medications.GetMedication(MedicationPats.List[i].MedicationNum).MedName;
				if(generic.MedicationNum!=MedicationPats.List[i].MedicationNum) {//not generic
					medName+=" ("+generic.MedName+")";
				}
				row.Cells.Add(medName);
				row.Cells.Add(MedicationPats.List[i].PatNote);
				row.Cells.Add(MedicationPats.List[i].IsDiscontinued?"X":"");
				gridMeds.Rows.Add(row);
			}
			gridMeds.EndUpdate();
		}

		private void resizePictBox() {
			if(pictBox.BackgroundImage!=null) {
				pictBox.BackgroundImage.Dispose();
			}
			int width;
			int height;
			float ratio;
			//Resize the image at the width of the pictBox, then only resize to the height if it doesn't fit.
			width=pictBox.Width-4;
			ratio=(float)width/BitmapOriginal.Width;
			height=(int)(BitmapOriginal.Height*ratio);
			if(height>pictBox.Height) {
				height=pictBox.Height-4;
				ratio=(float)height/BitmapOriginal.Height;
				width=(int)(BitmapOriginal.Width*ratio);
			}
			Bitmap newBitmap=new Bitmap(width,height);
			Graphics g=Graphics.FromImage(newBitmap);
			g.DrawImage(BitmapOriginal,0,0,width,height);
			g.Dispose();
			if(pictBox.BackgroundImage!=null) {
				pictBox.BackgroundImage.Dispose();
			}
			pictBox.BackgroundImage=newBitmap;
		}

		private void FormMedicationReconcile_ResizeEnd(object sender,EventArgs e) {
			resizePictBox();
		}

		private void checkDiscontinued_MouseUp(object sender,MouseEventArgs e) {
			FillMeds();
		}

		private void checkDiscontinued_KeyUp(object sender,KeyEventArgs e) {
			FillMeds();
		}

		private void gridMeds_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormMedPat FormMP=new FormMedPat();
			FormMP.MedicationPatCur=MedicationPats.List[e.Row];
			FormMP.ShowDialog();
			FillMeds();
		}

		private void FormMedicationReconcile_Resize(object sender,EventArgs e) {
			resizePictBox();
		}

		private void butPickRxListImage_Click(object sender,EventArgs e) {	
			if(!PrefC.UsingAtoZfolder) {
				MsgBox.Show(this,"This option is not supported with images stored in the database.");
				return;
			}
			FormImageSelect formIS=new FormImageSelect();
			formIS.PatNum=PatCur.PatNum;
			formIS.ShowDialog();
			if(formIS.DialogResult!=DialogResult.OK) {
				return;
			}		
			string patFolder=ImageStore.GetPatientFolder(PatCur);
			Document doc=Documents.GetByNum(formIS.SelectedDocNum);
			textDocDateDesc.Text=doc.DateTStamp.ToShortDateString()+" - "+doc.Description.ToString();
			if(BitmapOriginal!=null) {
				BitmapOriginal.Dispose();
			}
			BitmapOriginal=ImageStore.OpenImage(doc,patFolder);
			Bitmap bitmap=ImageHelper.ApplyDocumentSettingsToImage(doc,BitmapOriginal,ApplyImageSettings.ALL);
			pictBox.BackgroundImage=bitmap;
			resizePictBox();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			//select medication from list.  Additional meds can be added to the list from within that dlg
			FormMedications FormM=new FormMedications();
			FormM.SelectMode=true;
			FormM.ShowDialog();
			if(FormM.DialogResult!=DialogResult.OK) {
				return;
			}
			MedicationPat MedicationPatCur=new MedicationPat();
			MedicationPatCur.PatNum=PatCur.PatNum;
			MedicationPatCur.MedicationNum=FormM.MedicationNum;
			FormMedPat FormMP=new FormMedPat();
			FormMP.MedicationPatCur=MedicationPatCur;
			FormMP.IsNew=true;
			FormMP.ShowDialog();
			if(FormMP.DialogResult!=DialogResult.OK) {
				return;
			}
			FillMeds();
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}


		
	}
}