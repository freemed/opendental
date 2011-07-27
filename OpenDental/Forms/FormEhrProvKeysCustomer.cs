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
	public partial class FormEhrProvKeysCustomer:Form {
		private List<EhrProvKey> listKeys;
		private List<EhrQuarterlyKey> listKeysQuart;
		public long Guarantor;

		public FormEhrProvKeysCustomer() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormEhrProvKeysCustomer_Load(object sender,EventArgs e) {
			FillGrid();
			FillGridQ();
		}

		private void FillGrid(){
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"LName"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"FName"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Key"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Charge"),60,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"FTE"),35,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Notes"),100);
			gridMain.Columns.Add(col);
			listKeys=EhrProvKeys.RefreshForFam(Guarantor);
			gridMain.Rows.Clear();
			ODGridRow row;
			decimal feeTotal=0;
			decimal fee=0;
			for(int i=0;i<listKeys.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(listKeys[i].LName);
				row.Cells.Add(listKeys[i].FName);
				row.Cells.Add(listKeys[i].ProvKey);
				fee=(decimal)(60f*listKeys[i].FullTimeEquiv);
				feeTotal+=fee;
				row.Cells.Add(fee.ToString("c"));
				row.Cells.Add(listKeys[i].FullTimeEquiv.ToString());
				row.Cells.Add(listKeys[i].Notes);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			textCharge.Text=feeTotal.ToString("c");
		}

		private void gridMain_CellDoubleClick(object sender,UI.ODGridClickEventArgs e) {
			FormEhrProvKeyEditCust formK=new FormEhrProvKeyEditCust();
			formK.KeyCur=listKeys[e.Row];
			formK.ShowDialog();
			FillGrid();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormEhrProvKeyEditCust formK=new FormEhrProvKeyEditCust();
			formK.KeyCur=new EhrProvKey();
			formK.KeyCur.PatNum=Guarantor;
			formK.KeyCur.FullTimeEquiv=1;
			formK.KeyCur.IsNew=true;
			formK.ShowDialog();
			FillGrid();
		}

		private void FillGridQ(){
			gridQ.BeginUpdate();
			gridQ.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Year"),40);
			gridQ.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Quarter"),50);
			gridQ.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Key"),100);
			gridQ.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Notes"),100);
			gridQ.Columns.Add(col);
			listKeysQuart=EhrQuarterlyKeys.Refresh(Guarantor);
			gridQ.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<listKeysQuart.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(listKeysQuart[i].YearValue.ToString());
				row.Cells.Add(listKeysQuart[i].QuarterValue.ToString());
				row.Cells.Add(listKeysQuart[i].KeyValue);
				row.Cells.Add(listKeysQuart[i].Notes);
				gridQ.Rows.Add(row);
			}
			gridQ.EndUpdate();
		}

		private void gridQ_CellDoubleClick(object sender,UI.ODGridClickEventArgs e) {
			FormEhrQuarterlyKeyEditCust formK=new FormEhrQuarterlyKeyEditCust();
			formK.KeyCur=listKeysQuart[e.Row];
			formK.ShowDialog();
			FillGridQ();
		}

		private void butAddQuarterly_Click(object sender,EventArgs e) {
			FormEhrQuarterlyKeyEditCust formK=new FormEhrQuarterlyKeyEditCust();
			formK.KeyCur=new EhrQuarterlyKey();
			formK.KeyCur.PatNum=Guarantor;
			if(listKeysQuart.Count==0){
				formK.KeyCur.YearValue=DateTime.Today.Year-2000;
				int quarter=1;
				if(DateTime.Today.Month>=4 && DateTime.Today.Month<=6){
					quarter=2;
				}
				if(DateTime.Today.Month>=7 && DateTime.Today.Month<=9){
					quarter=3;
				}
				if(DateTime.Today.Month>=10){
					quarter=4;
				}
				formK.KeyCur.QuarterValue=quarter;
			}
			else{
				formK.KeyCur.PracticeName=listKeysQuart[listKeysQuart.Count-1].PracticeName;
				formK.KeyCur.YearValue=listKeysQuart[listKeysQuart.Count-1].YearValue;
				formK.KeyCur.QuarterValue=listKeysQuart[listKeysQuart.Count-1].QuarterValue+1;
				if(formK.KeyCur.QuarterValue==5){
					formK.KeyCur.QuarterValue=1;
					formK.KeyCur.YearValue++;
				}
				int monthOfQuarter=1;
				if(formK.KeyCur.QuarterValue==2){
					monthOfQuarter=4;
				}
				if(formK.KeyCur.QuarterValue==3){
					monthOfQuarter=7;
				}
				if(formK.KeyCur.QuarterValue==4){
					monthOfQuarter=10;
				}
				DateTime firstDayOfQuarter=new DateTime(2000+formK.KeyCur.YearValue,monthOfQuarter,1);
				DateTime earliestReleaseDate=firstDayOfQuarter.AddMonths(-1);
				if(DateTime.Today<earliestReleaseDate){
					MessageBox.Show("Warning!  Quarterly keys cannot be released more than one month in advance.");
				}
			}
			formK.KeyCur.IsNew=true;
			formK.ShowDialog();
			FillGridQ();
		}

		private void butSave_Click(object sender,EventArgs e) {
			long defNum=DefC.GetList(DefCat.ImageCats)[0].DefNum;
			Bitmap bitmap=new Bitmap(this.Width,this.Height);
			this.DrawToBitmap(bitmap,new Rectangle(0,0,this.Width,this.Height));
			Patient guar=Patients.GetPat(Guarantor);
			try {
				ImageStore.Import(bitmap,defNum,ImageType.Photo,guar);
			}
			catch(Exception ex) {
				MessageBox.Show("Unable to save file: "+ex.Message);
				return;
			}
			MsgBox.Show(this,"Saved.");
		}

		private void butClose_Click(object sender,EventArgs e) {
			Close();
		}

		

	

	

		

		
	}
}