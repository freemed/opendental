using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormRpProcCodes : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.ListBox listFeeSched;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.RadioButton radioCategories;
		private System.Windows.Forms.RadioButton radioCode;
		private System.ComponentModel.Container components = null;
		private Label label1;
		private FormQuery FormQuery2;

		///<summary></summary>
		public FormRpProcCodes(){
			InitializeComponent();
 			Lan.F(this);
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRpProcCodes));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.listFeeSched = new System.Windows.Forms.ListBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.radioCategories = new System.Windows.Forms.RadioButton();
			this.radioCode = new System.Windows.Forms.RadioButton();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(337,276);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 3;
			this.butCancel.Text = "&Cancel";
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(337,241);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 2;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// listFeeSched
			// 
			this.listFeeSched.Location = new System.Drawing.Point(33,41);
			this.listFeeSched.Name = "listFeeSched";
			this.listFeeSched.ScrollAlwaysVisible = true;
			this.listFeeSched.Size = new System.Drawing.Size(129,173);
			this.listFeeSched.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.radioCategories);
			this.panel1.Controls.Add(this.radioCode);
			this.panel1.Location = new System.Drawing.Point(206,25);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(104,60);
			this.panel1.TabIndex = 1;
			// 
			// radioCategories
			// 
			this.radioCategories.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioCategories.Location = new System.Drawing.Point(8,32);
			this.radioCategories.Name = "radioCategories";
			this.radioCategories.Size = new System.Drawing.Size(88,24);
			this.radioCategories.TabIndex = 1;
			this.radioCategories.Text = "Categories";
			// 
			// radioCode
			// 
			this.radioCode.Checked = true;
			this.radioCode.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioCode.Location = new System.Drawing.Point(8,8);
			this.radioCode.Name = "radioCode";
			this.radioCode.Size = new System.Drawing.Size(88,24);
			this.radioCode.TabIndex = 0;
			this.radioCode.TabStop = true;
			this.radioCode.Text = "Code";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(30,17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(132,22);
			this.label1.TabIndex = 4;
			this.label1.Text = "Fee Schedule";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// FormRpProcCodes
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(423,314);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.listFeeSched);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRpProcCodes";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Procedure Codes Report";
			this.Load += new System.EventHandler(this.FormRpProcCodes_Load);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		private void FormRpProcCodes_Load(object sender, System.EventArgs e) {
			for(int i=0;i<DefB.Short[(int)DefCat.FeeSchedNames].Length;i++){
				listFeeSched.Items.Add(DefB.Short[(int)DefCat.FeeSchedNames][i].ItemName);
			}		
			listFeeSched.SelectedIndex=0;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			int feeSched=DefB.Short[(int)DefCat.FeeSchedNames][listFeeSched.SelectedIndex].DefNum;	
      string catName="";  //string to hold current category name
			Fees fee=new Fees();
			Queries.CurReport=new ReportOld();
			
			Queries.CurReport.Query= "SELECT procedurecode.ProcCode,fee.Amount,'     ',procedurecode.Descript,"
			  +"procedurecode.AbbrDesc FROM procedurecode,fee "
				+"WHERE procedurecode.CodeNum=fee.CodeNum AND fee.FeeSched='"+feeSched.ToString()
         +"' ORDER BY procedurecode.ProcCode";
			FormQuery2=new FormQuery();
			FormQuery2.IsReport=true;
      if (radioCode.Checked==true)  {
			  FormQuery2.SubmitReportQuery();			      
				Queries.CurReport.Title="Procedure Codes";
				Queries.CurReport.SubTitle=new string[2];
				Queries.CurReport.SubTitle[0]=((Pref)PrefB.HList["PracticeTitle"]).ValueString;
				Queries.CurReport.SubTitle[1]=DefB.GetName(DefCat.FeeSchedNames,feeSched);
				Queries.CurReport.ColPos=new int[6];
				Queries.CurReport.ColCaption=new string[5];
				Queries.CurReport.ColAlign=new HorizontalAlignment[5];
				Queries.CurReport.ColPos[0]=60;
				Queries.CurReport.ColPos[1]=130;
				Queries.CurReport.ColPos[2]=200;
				Queries.CurReport.ColPos[3]=220;
				Queries.CurReport.ColPos[4]=420;
				Queries.CurReport.ColPos[5]=620;
				Queries.CurReport.ColCaption[0]="Code";
				Queries.CurReport.ColCaption[1]="Fee Amount";
				Queries.CurReport.ColCaption[2]=" ";//otherwise, the amount gets bunched up next to the description.
				Queries.CurReport.ColCaption[3]="Description";
				Queries.CurReport.ColCaption[4]="Abbr Description";
				//Queries.CurReport.ColCaption[3]="Fee Amount";
				Queries.CurReport.ColAlign[1]=HorizontalAlignment.Right;
				Queries.CurReport.Summary=new string[0];
				FormQuery2.ShowDialog();
				DialogResult=DialogResult.OK;		
      }
			else {//categories
			  Queries.SubmitTemp();//create TableTemp which is not actually used
	      ProcedureCode[] ProcList=ProcedureCodes.GetProcList();
				Queries.TableQ=new DataTable(null);
			  for(int i=0;i<5;i++){//add columns
				  Queries.TableQ.Columns.Add(new System.Data.DataColumn());//blank columns
			  }
				Queries.CurReport.ColTotal=new double[Queries.TableQ.Columns.Count];
        DataRow row=Queries.TableQ.NewRow();//add first row by hand to get value for temp
				row[0]=DefB.GetName(DefCat.ProcCodeCats,ProcList[0].ProcCat);
				catName=row[0].ToString();
				row[1]=ProcList[0].ProcCode;
				row[2]=ProcList[0].Descript;
				row[3]=ProcList[0].AbbrDesc;
			  row[4]=((double)Fees.GetAmount0(ProcList[0].CodeNum,feeSched)).ToString("F");
				Queries.CurReport.ColTotal[4]+=PIn.PDouble(row[4].ToString());
				Queries.TableQ.Rows.Add(row);
				for(int i=1;i<ProcList.Length;i++){//loop through data rows
					row=Queries.TableQ.NewRow();//create new row called 'row' based on structure of TableQ
					row[0]=DefB.GetName(DefCat.ProcCodeCats,ProcList[i].ProcCat);
					if(catName==row[0].ToString()){
            row[0]=""; 
					}
					else  {
						catName=row[0].ToString();
          }
					row[1]=ProcList[i].ProcCode.ToString();
					row[2]=ProcList[i].Descript;
					row[3]=ProcList[i].AbbrDesc.ToString();
					row[4]=((double)Fees.GetAmount0(ProcList[i].CodeNum,feeSched)).ToString("F");
  				//Queries.CurReport.ColTotal[4]+=PIn.PDouble(row[4].ToString());
					Queries.TableQ.Rows.Add(row);
				}
        Queries.CurReport.ColWidth=new int[Queries.TableQ.Columns.Count];
				Queries.CurReport.ColPos=new int[Queries.TableQ.Columns.Count+1];
				Queries.CurReport.ColPos[0]=0;
				Queries.CurReport.ColCaption=new string[Queries.TableQ.Columns.Count];
				Queries.CurReport.ColAlign=new HorizontalAlignment[Queries.TableQ.Columns.Count];
				FormQuery2.ResetGrid();//this is a method in FormQuery2;
				
				Queries.CurReport.Title="Procedure Codes";
				Queries.CurReport.SubTitle=new string[5];
				Queries.CurReport.SubTitle[0]=((Pref)PrefB.HList["PracticeTitle"]).ValueString;
				Queries.CurReport.SubTitle[1]=DefB.GetName(DefCat.FeeSchedNames,feeSched);
				Queries.CurReport.ColPos[0]=20;
				Queries.CurReport.ColPos[1]=120;
				Queries.CurReport.ColPos[2]=270;
				Queries.CurReport.ColPos[3]=470;
				Queries.CurReport.ColPos[4]=620;
				Queries.CurReport.ColPos[5]=770;
				Queries.CurReport.ColCaption[0]="Category";
				Queries.CurReport.ColCaption[1]="Code";
				Queries.CurReport.ColCaption[2]="Description";
				Queries.CurReport.ColCaption[3]="Abbr Description";
				Queries.CurReport.ColCaption[4]="Fee Amount";
				Queries.CurReport.ColAlign[4]=HorizontalAlignment.Right;
				Queries.CurReport.Summary=new string[5];
				FormQuery2.ShowDialog();
				DialogResult=DialogResult.OK;
			}
		}
	}
}
