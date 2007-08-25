using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary></summary>
	public class FormAllocate : System.Windows.Forms.Form{
		private OpenDental.UI.Button butClose;
		private OpenDental.UI.Button butReallocate;
		private System.Windows.Forms.Label label1;
		private Label label2;
		private OpenDental.UI.Button butRecalculate;
		/// <summary>Required designer variable.</summary>
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormAllocate()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAllocate));
			this.butClose = new OpenDental.UI.Button();
			this.butReallocate = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.butRecalculate = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.Location = new System.Drawing.Point(429,248);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,26);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butReallocate
			// 
			this.butReallocate.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butReallocate.Autosize = true;
			this.butReallocate.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butReallocate.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butReallocate.CornerRadius = 4F;
			this.butReallocate.Location = new System.Drawing.Point(17,57);
			this.butReallocate.Name = "butReallocate";
			this.butReallocate.Size = new System.Drawing.Size(85,26);
			this.butReallocate.TabIndex = 1;
			this.butReallocate.Text = "Reallocate";
			this.butReallocate.Click += new System.EventHandler(this.butReallocate_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(116,57);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(388,95);
			this.label1.TabIndex = 2;
			this.label1.Text = resources.GetString("label1.Text");
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(116,9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(388,37);
			this.label2.TabIndex = 4;
			this.label2.Text = "Recalculates all patient balances so that they will display properly after a conv" +
    "ersion.";
			// 
			// butRecalculate
			// 
			this.butRecalculate.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRecalculate.Autosize = true;
			this.butRecalculate.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRecalculate.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRecalculate.CornerRadius = 4F;
			this.butRecalculate.Location = new System.Drawing.Point(17,9);
			this.butRecalculate.Name = "butRecalculate";
			this.butRecalculate.Size = new System.Drawing.Size(85,26);
			this.butRecalculate.TabIndex = 3;
			this.butRecalculate.Text = "Recalculate";
			this.butRecalculate.Click += new System.EventHandler(this.butRecalculate_Click);
			// 
			// FormAllocate
			// 
			this.AcceptButton = this.butClose;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(545,296);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butRecalculate);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butReallocate);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormAllocate";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Reallocate Family Balances";
			this.Load += new System.EventHandler(this.FormAllocate_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormAllocate_Load(object sender, System.EventArgs e) {
		
		}

		private void butRecalculate_Click(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			int changed=RecalcAll();
			Cursor=Cursors.Default;
			MessageBox.Show(Lan.g(this,"Recalc done. Records changed: ")+changed.ToString());
		}
		
		private void butReallocate_Click(object sender, System.EventArgs e) {
			Cursor=Cursors.WaitCursor;
			int changed=RecalcAll();
			Cursor=Cursors.Default;
			MessageBox.Show(Lan.g(this,"Recalc done. Records changed: ")+changed.ToString()+"  "
				+Lan.g(this,"Reallocation will now begin."));
			Cursor=Cursors.WaitCursor;
			changed=ReallocateAll();
			Cursor=Cursors.Default;
			MessageBox.Show(Lan.g(this,"Reallocation done. Records changed: ")+changed.ToString());
		}

		private int RecalcAll(){
			string command="SELECT PatNum FROM patient";
			DataTable table=General.GetTable(command);
			int changed=0;
			bool result;
			for(int i=0;i<table.Rows.Count;i++) {
				result=ComputeBalances(PIn.PInt(table.Rows[i][0].ToString()));
				if(result){
					changed++;
				}
			}
			return changed;
		}

		///<summary>This will eventually replace Misc.ClassesShared.ComputeBalance.  Returns true if a change was made.</summary>
		public static bool ComputeBalances(int patNum){
			string command="SELECT (SELECT EstBalance FROM patient WHERE PatNum="+POut.PInt(patNum)+" GROUP BY PatNum) EstBalance, "
				+"IFNULL((SELECT SUM(ProcFee) FROM procedurelog WHERE PatNum="+POut.PInt(patNum)+" AND ProcStatus=2 GROUP BY PatNum),0)"//complete
				+"+IFNULL((SELECT SUM(InsPayAmt) FROM claimproc WHERE PatNum="+POut.PInt(patNum)
				+" AND (Status=1 OR Status=4 OR Status=5) GROUP BY PatNum),0) "//received,supplemental,capclaim"
				+"+IFNULL((SELECT SUM(AdjAmt) FROM adjustment WHERE PatNum="+POut.PInt(patNum)+" GROUP BY PatNum),0) "
				+"-IFNULL((SELECT SUM(SplitAmt) FROM paysplit WHERE PatNum="+POut.PInt(patNum)+" GROUP BY PatNum),0) CalcBalance, "
				+"IFNULL((SELECT SUM(InsPayEst) FROM claimproc WHERE PatNum="+POut.PInt(patNum)+" GROUP BY PatNum),0) Estimate ";
			DataTable table=General.GetTable(command);
			double calcBal=PIn.PDouble(table.Rows[0]["CalcBalance"].ToString());
			if(!PrefB.GetBool("BalancesDontSubtractIns")){//most common
				calcBal-=PIn.PDouble(table.Rows[0]["Estimate"].ToString());
			}
			double estBal=PIn.PDouble(table.Rows[0]["EstBalance"].ToString());
			if(calcBal!=estBal) {
				command="UPDATE patient SET EstBalance='"+POut.PDouble(calcBal)+"' WHERE PatNum="+POut.PInt(patNum);
				General.NonQ(command);
				return true;
			}
			return false;
		}

		private int ReallocateAll(){
			string command="SELECT DefNum FROM definition WHERE Category=1 AND ItemName='Reallocation'";
			DataTable table=General.GetTable(command);
			int defnum;
			if(table.Rows.Count==0){
				Def def=new Def();
				def.Category=DefCat.AdjTypes;
				def.ItemName="Reallocation";
				def.ItemValue="+";
				def.ItemOrder=DefB.Long[(int)DefCat.AdjTypes].Length;
				Defs.Insert(def);
				defnum=def.DefNum;
				DataValid.SetInvalid(InvalidTypes.Defs);
			}
			else{
				defnum=PIn.PInt(table.Rows[0][0].ToString());
			}
			//find all families where someone has a negative balance.
			command="SELECT DISTINCT Guarantor FROM patient WHERE EstBalance < 0";
			DataTable tableGuarantors=General.GetTable(command);
			int changed=0;
			//bool result;
			double[] familyBals;
			DataTable tablePatients;
			Adjustment adj;
			Double delta;
			for(int i=0;i<tableGuarantors.Rows.Count;i++) {
				command="SELECT PatNum,EstBalance FROM patient WHERE Guarantor="+tableGuarantors.Rows[i][0].ToString();
				tablePatients=General.GetTable(command);
				if(tablePatients.Rows.Count==1){//impossible to allocate
					continue;
				}
				familyBals=new double[tablePatients.Rows.Count];
				for(int p=0;p<tablePatients.Rows.Count;p++){
					familyBals[p]=PIn.PDouble(tablePatients.Rows[p]["EstBalance"].ToString());
				}
				for(int p=0;p<familyBals.Length;p++){
					if(familyBals[p]<0){//if a negative bal found
						for(int j=0;j<familyBals.Length;j++){//look for a positive bal to adjust
							if(j==p){
								continue;//skip same patient
							}
							if(familyBals[j]<=0){
								continue;//no neg bal
							}
							if(familyBals[j]>=-familyBals[p]){//if sufficient bal to zero out the neg
								familyBals[j]+=familyBals[p];//because p is neg
								familyBals[p]=0;
								break;//quit looking for a pos bal to adj.
							}
							else{//only enough bal to reduce the negative, not eliminate it
								familyBals[p]+=familyBals[j];//because p is neg
								familyBals[j]=0;
							}
						}
					}
				}
				//now, save any changes to db:
				for(int p=0;p<familyBals.Length;p++){
					if(familyBals[p]==PIn.PDouble(tablePatients.Rows[p]["EstBalance"].ToString())){
						continue;//same, so no change to db
					}
					adj=new Adjustment();
					adj.PatNum=PIn.PInt(tablePatients.Rows[p]["PatNum"].ToString());
					adj.AdjDate=DateTime.Today;
					adj.AdjNote="Automatic";
					adj.AdjType=defnum;
					adj.ProcDate=DateTime.Today;
					adj.ProvNum=PrefB.GetInt("PracticeDefaultProv");
					delta=familyBals[p]-PIn.PDouble(tablePatients.Rows[p]["EstBalance"].ToString());//works whether pos or neg
					adj.AdjAmt=delta;
					Adjustments.InsertOrUpdate(adj,true);
					command="UPDATE patient SET EstBalance=EstBalance+'"+POut.PDouble(delta)+"' WHERE PatNum="+tablePatients.Rows[p]["PatNum"].ToString();
					General.NonQ(command);
					changed++;
				}
			}
			return changed;
		}

		private bool Reallocate(int guarantor){
			//string 
			return true;
		}

		private void butClose_Click(object sender,System.EventArgs e) {
			Close();
		}

		

	}
}










