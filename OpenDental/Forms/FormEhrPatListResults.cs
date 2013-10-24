using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;
using System.Drawing.Printing;

namespace OpenDental {
	public partial class FormEhrPatListResults:Form {
		private List<EhrPatListElement> elementList;
		private DataTable table;
		private bool headingPrinted;
		private int pagesPrinted;

		public FormEhrPatListResults(List<EhrPatListElement> ElementList) {
			InitializeComponent();
			elementList=ElementList;
		}

		private void FormPatListResults_Load(object sender,EventArgs e) {
			FillGrid(true);
		}

		private void FillGrid(bool isAsc) {
			table=EhrPatListElements.GetListOrderBy(elementList,isAsc);
			int colWidth=0;
			Graphics g=CreateGraphics();
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn("PatNum",60,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Full Name",110);
			gridMain.Columns.Add(col);
			for(int i=0;i<elementList.Count;i++) {
				if(elementList[i].Restriction==EhrRestrictionType.Birthdate) {
					col=new ODGridColumn("Birthdate",80,HorizontalAlignment.Center);
					gridMain.Columns.Add(col);
				}
				else if(elementList[i].Restriction==EhrRestrictionType.Gender) {
					col=new ODGridColumn("Gender",70,HorizontalAlignment.Center);
					gridMain.Columns.Add(col);
				}
				else if(elementList[i].Restriction==EhrRestrictionType.Problem) {
					col=new ODGridColumn("Disease",160,HorizontalAlignment.Center);
					gridMain.Columns.Add(col);
				}
				else {
					colWidth=System.Convert.ToInt32(g.MeasureString(elementList[i].CompareString,this.Font).Width);
					colWidth=colWidth+(colWidth/10);//Add 10%
					if(colWidth<90) {
						colWidth=90;//Minimum of 90 width.
					}
					col=new ODGridColumn(elementList[i].CompareString,colWidth,HorizontalAlignment.Center);
					gridMain.Columns.Add(col);
				}
			}
			gridMain.Rows.Clear();
			ODGridRow row;
			string icd9Desc;
			for(int i=0;i<table.Rows.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(table.Rows[i]["PatNum"].ToString());
				row.Cells.Add(table.Rows[i]["LName"].ToString()+", "+table.Rows[i]["FName"].ToString());
				//Add 3 to j to compensate for PatNum, LName and FName.
				for(int j=0;j<elementList.Count;j++) {
					if(elementList[j].Restriction==EhrRestrictionType.Problem) {
						ICD9 icd;
						try {
							icd=ICD9s.GetOne(PIn.Long(table.Rows[i][j+3].ToString()));
							icd9Desc="("+icd.ICD9Code+")-"+icd.Description;
							row.Cells.Add(icd9Desc);
						}
						catch {//Graceful fail just in case.
							row.Cells.Add("X");
						}
						continue;
					}
					if(elementList[j].Restriction==EhrRestrictionType.Medication)	{
						row.Cells.Add("X");
						continue;
					}
					if(elementList[j].Restriction==EhrRestrictionType.Birthdate) {
						row.Cells.Add(PIn.Date(table.Rows[i][j+3].ToString()).ToShortDateString());
						continue;
					}
					if(elementList[j].Restriction==EhrRestrictionType.Gender)	{
						switch(table.Rows[i][j+3].ToString()) {
							case "0":
								row.Cells.Add("Male");
								break;
							case "1":
								row.Cells.Add("Female");
								break;
							default:
								row.Cells.Add("Unknown");
								break;
						}
						continue;
					}
					row.Cells.Add(table.Rows[i][j+3].ToString());
				}
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			g.Dispose();
		}

		private void radioAsc_CheckedChanged(object sender,EventArgs e) {
			FillGrid(true);
		}

		private void radioDesc_CheckedChanged(object sender,EventArgs e) {
			FillGrid(false);
		}

		private void butPrint_Click(object sender,EventArgs e) {
			PrintDocument pd=new PrintDocument();
			pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
			pd.DefaultPageSettings.Margins=new Margins(25,25,40,40);
			//pd.OriginAtMargins=true;
			if(pd.DefaultPageSettings.PrintableArea.Height==0) {
				pd.DefaultPageSettings.PaperSize=new PaperSize("default",850,1100);
			}
			#if DEBUG
				//pd.Print();
					MessageBox.Show("Printed");
			#else
				PrintDialog dialog=new PrintDialog();
				dialog.UseEXDialog=true;
				DialogResult result=dialog.ShowDialog();
				if(result==DialogResult.OK) {
					try {
						pd.PrinterSettings=dialog.PrinterSettings;
						pd.Print();
						//Create audit log entry for printing.  PatNum can be 0.
						SecurityLogs.MakeLogEntry(Permissions.Printing,0,"Patient List Printed");
					}
					catch(Exception ex) {
						MessageBox.Show(ex.Message);
					}
				}
			#endif
		}

		private void pd_PrintPage(object sender,System.Drawing.Printing.PrintPageEventArgs e) {
			Rectangle bounds=e.MarginBounds;
			//new Rectangle(50,40,800,1035);//Some printers can handle up to 1042
			Graphics g=e.Graphics;
			string text;
			Font headingFont=new Font("Arial",13,FontStyle.Bold);
			Font subHeadingFont=new Font("Arial",10,FontStyle.Bold);
			int yPos=bounds.Top;
			int center=bounds.X+bounds.Width/2;
			#region printHeading
			if(!headingPrinted) {
				text="Patient List";
				g.DrawString(text,headingFont,Brushes.Black,center-g.MeasureString(text,headingFont).Width/2,yPos);
				yPos+=(int)g.MeasureString(text,headingFont).Height;
				headingPrinted=true;
				//headingPrintH=yPos;
			}
			#endregion
			yPos=gridMain.PrintPage(g,pagesPrinted,bounds,yPos);
			pagesPrinted++;
			if(yPos==-1) {
				e.HasMorePages=true;
			}
			else {
				e.HasMorePages=false;
			}
			g.Dispose();
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}
