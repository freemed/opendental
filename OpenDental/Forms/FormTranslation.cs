using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Globalization;
using System.IO;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental{
///<summary></summary>
	public class FormTranslation : System.Windows.Forms.Form{
		private OpenDental.UI.Button butClose;
		private System.ComponentModel.Container components = null;
		private Language[] LanList;
		private string ClassType;
		private OpenDental.UI.ODGrid gridLan;
		private LanguageForeign[] ListForType;

		///<summary></summary>
		public FormTranslation(string classType){
			InitializeComponent();
			gridLan.Title=classType+" Words";
			
			//tbLan.Fields[2]=CultureInfo.CurrentCulture.Parent.DisplayName;
			//tbLan.Fields[3]=CultureInfo.CurrentCulture.Parent.DisplayName + " Comments";
			//no need to translate much here
			Lan.C("All", new System.Windows.Forms.Control[] {
				butClose,																											
			});
			ClassType=classType;
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

		private void InitializeComponent(){
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTranslation));
			this.butClose = new OpenDental.UI.Button();
			this.gridLan = new OpenDental.UI.ODGrid();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.Location = new System.Drawing.Point(847,671);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,26);
			this.butClose.TabIndex = 3;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// gridLan
			// 
			this.gridLan.HScrollVisible = false;
			this.gridLan.Location = new System.Drawing.Point(18,12);
			this.gridLan.Name = "gridLan";
			this.gridLan.ScrollValue = 0;
			this.gridLan.Size = new System.Drawing.Size(905,643);
			this.gridLan.TabIndex = 7;
			this.gridLan.Title = "Translations";
			this.gridLan.TranslationName = "TableLan";
			this.gridLan.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridLan_CellDoubleClick);
			// 
			// FormTranslation
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(958,708);
			this.Controls.Add(this.gridLan);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormTranslation";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Translation";
			this.Load += new System.EventHandler(this.FormLanguage_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormLanguage_Load(object sender, System.EventArgs e) {
			FillGrid();
		}

		private void FillGrid(){
			LanList=Lan.GetListForCat(ClassType);
			ListForType=LanguageForeigns.GetListForType(ClassType);
			LanguageForeigns.Refresh(CultureInfo.CurrentCulture);
			gridLan.BeginUpdate();
			gridLan.Columns.Clear();
			ODGridColumn column=new ODGridColumn("English",220);
			gridLan.Columns.Add(column);
			column=new ODGridColumn(CultureInfo.CurrentCulture.DisplayName,220);
			gridLan.Columns.Add(column);
			column=new ODGridColumn("Other "+CultureInfo.CurrentCulture.Parent.DisplayName+" Translation",220);
			gridLan.Columns.Add(column);
			column=new ODGridColumn(CultureInfo.CurrentCulture.DisplayName+" Comments",220);
			gridLan.Columns.Add(column);
			//gridLan.Columns[1].Heading=;
			//gridLan.Columns[2].Heading="Other "+CultureInfo.CurrentCulture.Parent.DisplayName+" Translation";
			//gridLan.Columns[3].Heading=CultureInfo.CurrentCulture.DisplayName+" Comments";
			gridLan.Rows.Clear();
			UI.ODGridRow row;
			LanguageForeign lanForeign;
			LanguageForeign lanForeignOther;
			for(int i=0;i<LanList.Length;i++){
				row=new OpenDental.UI.ODGridRow();
				row.Cells.Add(LanList[i].English);
				lanForeign=LanguageForeigns.GetForCulture(ListForType,LanList[i].English,CultureInfo.CurrentCulture.Name);
				lanForeignOther=LanguageForeigns.GetOther(ListForType,LanList[i].English,CultureInfo.CurrentCulture.Name);
				if(lanForeign==null){
					row.Cells.Add("");
				}
				else{
					row.Cells.Add(lanForeign.Translation);
				}
				if(lanForeignOther==null){
					row.Cells.Add("");
				}
				else{
					row.Cells.Add(lanForeignOther.Translation);
				}
				if(lanForeign==null){
					row.Cells.Add("");
				}
				else{
					row.Cells.Add(lanForeign.Comments);
				}
				gridLan.Rows.Add(row);
			}
			gridLan.EndUpdate();
		}

		private void gridLan_CellDoubleClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			Language LanCur=LanList[e.Row];
			LanguageForeign lanForeign=LanguageForeigns.GetForCulture(ListForType,LanCur.English,CultureInfo.CurrentCulture.Name);
			LanguageForeign lanForeignOther=LanguageForeigns.GetOther(ListForType,LanCur.English,CultureInfo.CurrentCulture.Name);
			string otherTrans="";
			if(lanForeignOther!=null){
				otherTrans=lanForeignOther.Translation;
			}
			FormTranslationEdit FormTE=new FormTranslationEdit(LanCur,lanForeign,otherTrans);
			FormTE.ShowDialog();
			FillGrid();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

	}
}





















