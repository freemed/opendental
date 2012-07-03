using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormSupplyInventory:Form {
		private List<SupplyNeeded> listNeeded;
		private List<Supply> listSupply;
		private List<Supplier> listSupplier;
		private List<SupplyOrder> listOrder;
		private DataTable tableOrderItem;
		///<Summary>Will be true if the data displayed in gridSupplyMain was obtained with the find text empty.</Summary>
		private bool IsCleanRefresh;
		PrintDocument pd2;
		private int pagesPrinted;
		private bool headingPrinted;
		private int headingPrintH;

		public FormSupplyInventory() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormInventory_Load(object sender,EventArgs e) {
			FillGridNeeded();
			IsCleanRefresh=true;
		}

		private void FillGridNeeded(){
			listNeeded=SupplyNeededs.CreateObjects();
			gridNeeded.BeginUpdate();
			gridNeeded.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Date Added"),80);
			gridNeeded.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Description"),300);
			gridNeeded.Columns.Add(col);
			gridNeeded.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<listNeeded.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(listNeeded[i].DateAdded.ToShortDateString());
				row.Cells.Add(listNeeded[i].Description);
				gridNeeded.Rows.Add(row);
			}
			gridNeeded.EndUpdate();
		}

		private void gridNeeded_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormSupplyNeededEdit FormS=new FormSupplyNeededEdit();
			FormS.Supp=listNeeded[e.Row];
			FormS.ShowDialog();
			if(FormS.DialogResult==DialogResult.OK) {
				FillGridNeeded();
			}
		}

		private void butAddNeeded_Click(object sender,EventArgs e) {
			SupplyNeeded supp=new SupplyNeeded();
			supp.IsNew=true;
			supp.DateAdded=DateTime.Today;
			FormSupplyNeededEdit FormS=new FormSupplyNeededEdit();
			FormS.Supp=supp;
			FormS.ShowDialog();
			if(FormS.DialogResult==DialogResult.OK){
				FillGridNeeded();
			}
		}

		private void menuItemSuppliers_Click(object sender,EventArgs e) {
			FormSuppliers FormS=new FormSuppliers();
			FormS.ShowDialog();
		}

		private void menuItemCategories_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormDefinitions FormD=new FormDefinitions(DefCat.SupplyCats);
			FormD.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Definitions.");
		}

		private void butEquipment_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormEquipment form=new FormEquipment();
			form.ShowDialog();
		}

		private void butOrders_Click(object sender,EventArgs e) {
			FormSupplyOrders FormSO=new FormSupplyOrders();
			FormSO.ShowDialog();
		}

		private void butSupplies_Click(object sender,EventArgs e) {
			FormSupplies FormSup=new FormSupplies();
			FormSup.ShowDialog();
		}

		private void butPrint_Click(object sender,EventArgs e) {
			//if(tabControl.SelectedIndex==1 && gridOrder.GetSelectedIndex()==-1){
			//  MsgBox.Show(this,"Please select an order first.");
			//  return;
			//}
			//if(tabControl.SelectedIndex==0 && gridSupplyMain.Rows.Count==0){
			//  MsgBox.Show(this,"The list is empty.");
			//  return;
			//}
			//pagesPrinted=0;
			//headingPrinted=false;
			//pd2=new PrintDocument();
			//pd2.DefaultPageSettings.Margins=new Margins(50,50,40,30);
			//if(tabControl.SelectedIndex==0){//main list
			//  pd2.PrintPage += new PrintPageEventHandler(pd2_PrintPageMain);
			//}
			//else{//one order
			//  pd2.PrintPage += new PrintPageEventHandler(pd2_PrintPageOrder);
			//}
			//if(pd2.DefaultPageSettings.PrintableArea.Height==0) {
			//  pd2.DefaultPageSettings.PaperSize=new PaperSize("default",850,1100);
			//}
			//#if DEBUG
			//  FormRpPrintPreview pView = new FormRpPrintPreview();
			//  pView.printPreviewControl2.Document=pd2;
			//  pView.ShowDialog();
			//#else
			//  if(PrinterL.SetPrinter(pd2,PrintSituation.Default)) {
			//    try{
			//      pd2.Print();
			//    }
			//    catch{
			//      MsgBox.Show(this,"Printer not available");
			//    }
			//  }
			//#endif
		}

		//private void pd2_PrintPageMain(object sender,System.Drawing.Printing.PrintPageEventArgs e) {
		//  Rectangle bounds=e.MarginBounds;
		//  Graphics g=e.Graphics;
		//  string text;
		//  Font headingFont=new Font("Arial",13,FontStyle.Bold);
		//  Font subHeadingFont=new Font("Arial",10,FontStyle.Bold);
		//  Font mainFont=new Font("Arial",9);
		//  int yPos=bounds.Top;
		//  #region printHeading
		//  if(!headingPrinted) {
		//    text="Supply List";
		//    g.DrawString(text,headingFont,Brushes.Black,425-g.MeasureString(text,headingFont).Width/2,yPos);
		//    yPos+=(int)g.MeasureString(text,headingFont).Height;
		//    text=listSupplier[comboSupplier.SelectedIndex].Name;
		//    g.DrawString(text,subHeadingFont,Brushes.Black,425-g.MeasureString(text,subHeadingFont).Width/2,yPos);
		//    yPos+=(int)g.MeasureString(text,subHeadingFont).Height;
		//    text=listSupplier[comboSupplier.SelectedIndex].Phone;
		//    g.DrawString(text,subHeadingFont,Brushes.Black,425-g.MeasureString(text,subHeadingFont).Width/2,yPos);
		//    yPos+=(int)g.MeasureString(text,subHeadingFont).Height;
		//    text=listSupplier[comboSupplier.SelectedIndex].Note;
		//    g.DrawString(text,subHeadingFont,Brushes.Black,425-g.MeasureString(text,mainFont,bounds.Width).Width/2,yPos);
		//    yPos+=(int)g.MeasureString(text,mainFont,bounds.Width).Height+15;
		//    headingPrinted=true;
		//    headingPrintH=yPos;
		//  }
		//  #endregion
		//  yPos=gridSupplyMain.PrintPage(g,pagesPrinted,bounds,headingPrintH);
		//  pagesPrinted++;
		//  if(yPos==-1) {
		//    e.HasMorePages=true;
		//  }
		//  else {
		//    e.HasMorePages=false;
		//  }
		//  g.Dispose();
		//}

		//private void pd2_PrintPageOrder(object sender,System.Drawing.Printing.PrintPageEventArgs e) {
		//  Rectangle bounds=e.MarginBounds;
		//  Graphics g=e.Graphics;
		//  string text;
		//  Font headingFont=new Font("Arial",13,FontStyle.Bold);
		//  Font subHeadingFont=new Font("Arial",10,FontStyle.Bold);
		//  Font mainFont=new Font("Arial",9);
		//  int yPos=bounds.Top;
		//  #region printHeading
		//  if(!headingPrinted) {
		//    text="Order";
		//    g.DrawString(text,headingFont,Brushes.Black,425-g.MeasureString(text,headingFont).Width/2,yPos);
		//    yPos+=(int)g.MeasureString(text,headingFont).Height;
		//    if(listOrder[gridOrder.GetSelectedIndex()].DatePlaced.Year>2200) {
		//      text="Pending  "+listOrder[gridOrder.GetSelectedIndex()].AmountTotal.ToString("c");
		//    }
		//    else {
		//      text=listOrder[gridOrder.GetSelectedIndex()].DatePlaced.ToShortDateString()
		//        +"  "+listOrder[gridOrder.GetSelectedIndex()].AmountTotal.ToString("c");
		//    }
		//    g.DrawString(text,subHeadingFont,Brushes.Black,425-g.MeasureString(text,subHeadingFont).Width/2,yPos);
		//    yPos+=(int)g.MeasureString(text,subHeadingFont).Height;
		//    text=listSupplier[comboSupplier.SelectedIndex].Name;
		//    g.DrawString(text,subHeadingFont,Brushes.Black,425-g.MeasureString(text,subHeadingFont).Width/2,yPos);
		//    yPos+=(int)g.MeasureString(text,subHeadingFont).Height;
		//    text=listSupplier[comboSupplier.SelectedIndex].Phone;
		//    g.DrawString(text,subHeadingFont,Brushes.Black,425-g.MeasureString(text,subHeadingFont).Width/2,yPos);
		//    yPos+=(int)g.MeasureString(text,subHeadingFont).Height;
		//    text=listSupplier[comboSupplier.SelectedIndex].Note;
		//    g.DrawString(text,subHeadingFont,Brushes.Black,425-g.MeasureString(text,mainFont,bounds.Width).Width/2,yPos);
		//    yPos+=(int)g.MeasureString(text,mainFont,bounds.Width).Height+15;
		//    headingPrinted=true;
		//    headingPrintH=yPos;
		//  }
		//  #endregion
		//  yPos=gridOrderItem.PrintPage(g,pagesPrinted,bounds,headingPrintH);
		//  pagesPrinted++;
		//  if(yPos==-1) {
		//    e.HasMorePages=true;
		//  }
		//  else {
		//    e.HasMorePages=false;
		//  }
		//  g.Dispose();
		//}
		
		private void butClose_Click(object sender,EventArgs e) {
			Close();
		}

		

		

		

		

		

		

		
		

		

	

		

		

		

		

		

		

		

		
	}
}