using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDental.ReportingOld2;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormFormPatEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		//private Question[] QuestionList;
		private QuestionDef[] QuestionDefList;
		private ContrMultInput multInput;
		public FormPat FormPatCur;
		private ODGrid gridMain;
		private OpenDental.UI.Button butDelete;
		public bool IsNew;

		///<summary></summary>
		public FormFormPatEdit()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
			//PatNum=patNum;
			//FormPatCur=formPatCur.Copy();
			multInput.IsQuestionnaire=true;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFormPatEdit));
			this.gridMain = new OpenDental.UI.ODGrid();
			this.multInput = new OpenDental.UI.ContrMultInput();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(12,12);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(757,607);
			this.gridMain.TabIndex = 3;
			this.gridMain.Title = "Questions";
			this.gridMain.TranslationName = "TableQuestions";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// multInput
			// 
			this.multInput.Location = new System.Drawing.Point(12,12);
			this.multInput.Name = "multInput";
			this.multInput.Size = new System.Drawing.Size(757,640);
			this.multInput.TabIndex = 2;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(795,585);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(795,626);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(12,625);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(88,26);
			this.butDelete.TabIndex = 4;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// FormFormPatEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(893,671);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.multInput);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormFormPatEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Questionnaire";
			this.Shown += new System.EventHandler(this.FormFormPatEdit_Shown);
			this.Load += new System.EventHandler(this.FormFormPatEdit_Load);
			this.ResumeLayout(false);

		}
		#endregion
		
		private void FormFormPatEdit_Load(object sender,EventArgs e) {
			QuestionDefList=QuestionDefs.Refresh();
			if(IsNew){
				gridMain.Visible=false;
				butDelete.Visible=false;
				//only gets filled once on startup, and not saved until OK.
				for(int i=0;i<QuestionDefList.Length;i++) {
					if(QuestionDefList[i].QuestType==QuestionType.FreeformText) {
						multInput.AddInputItem(QuestionDefList[i].Description,FieldValueType.String,"");
					}
					else if(QuestionDefList[i].QuestType==QuestionType.YesNoUnknown) {
						multInput.AddInputItem(QuestionDefList[i].Description,FieldValueType.YesNoUnknown,YN.Unknown);
					}
				}
			}
			else {
				butOK.Visible=false;
				butCancel.Text=Lan.g(this,"Close");
				multInput.Visible=false;
				//Gets filled repeatedly.  Saved each time user double clicks on a row.  Only the answer can be edited.
				FillGrid();
			}
			/*QuestionDefList=QuestionDefs.Refresh();
			QuestionList=Questions.Refresh(PatNum);
			if(QuestionList.Length==0){
				IsNew=true;
				gridMain.Visible=false;
				butDelete.Visible=false;
				//only gets filled once on startup, and not saved until OK.
				for(int i=0;i<QuestionDefList.Length;i++){
					if(QuestionDefList[i].QuestType==QuestionType.FreeformText){
						multInput.AddInputItem(QuestionDefList[i].Description,FieldValueType.String,"");
					}
					else if(QuestionDefList[i].QuestType==QuestionType.YesNoUnknown) {
						multInput.AddInputItem(QuestionDefList[i].Description,FieldValueType.YesNoUnknown,YN.Unknown);
					}
				}
			}
			else{
				IsNew=false;
				butOK.Visible=false;
				butCancel.Text=Lan.g(this,"Close");
				multInput.Visible=false;
				//Gets filled repeatedly.  Saved each time user double clicks on a row.  Only the answer can be edited.
				FillGrid();
			}*/
		}

		private void FormFormPatEdit_Shown(object sender,EventArgs e) {
			if(IsNew){
				if(QuestionDefList.Length==0){
					MsgBox.Show(this,"Go to Setup | Questionniare to setup questions for all patients.");
				}
			}
		}

		private void FillGrid(){
			//QuestionList=Questions.Refresh(PatNum);
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableQuestions","Question"),370);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableQuestions","Answer"),370);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<FormPatCur.QuestionList.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(FormPatCur.QuestionList[i].Description);
				row.Cells.Add(FormPatCur.QuestionList[i].Answer);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			//only visible if editing existing quesionnaire.
			InputBox input=new InputBox(FormPatCur.QuestionList[e.Row].Description);
			input.textResult.Text=FormPatCur.QuestionList[e.Row].Answer;
			input.ShowDialog();
			if(input.DialogResult==DialogResult.OK){
				FormPatCur.QuestionList[e.Row].Answer=input.textResult.Text;
				Questions.Update(FormPatCur.QuestionList[e.Row]);
			}
			FillGrid();
		}

		private void butDelete_Click(object sender,EventArgs e) {
			//only visible if editing existing quesionnaire.
			if(!MsgBox.Show(this,true,"Delete this questionnaire?")){
				return;
			}
			FormPats.Delete(FormPatCur.FormPatNum);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(QuestionDefList.Length==0){
				MsgBox.Show(this,"No questions to save.");
				return;
			}
			//only visible if IsNew
			FormPats.Insert(FormPatCur);
			Question quest;
			ArrayList ALval;
			for(int i=0;i<QuestionDefList.Length;i++){
				quest=new Question();
				quest.PatNum=FormPatCur.PatNum;
				quest.ItemOrder=QuestionDefList[i].ItemOrder;
				quest.Description=QuestionDefList[i].Description;
				if(QuestionDefList[i].QuestType==QuestionType.FreeformText){
					ALval=multInput.GetCurrentValues(i);
					if(ALval.Count>0){
						quest.Answer=ALval[0].ToString();
					}
					//else it will just be blank
				}
				else if(QuestionDefList[i].QuestType==QuestionType.YesNoUnknown){
					quest.Answer=Lan.g("enumYN",multInput.GetCurrentValues(i)[0].ToString());
				}
				quest.FormPatNum=FormPatCur.FormPatNum;
				Questions.Insert(quest);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			if(IsNew){
				DialogResult=DialogResult.Cancel;
			}
			else{
				Close();
			}
		}

		

		

	

		


	}
}





















