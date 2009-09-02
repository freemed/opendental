using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormRequestEdit:Form {
		public int RequestId;
		public bool IsAdminMode;
		//private Request ReqCur;
		//public bool IsNew;//might be redundant, since RequestId will be zero anyway.
		//public table for discussion
		private ODDataTable tableObj;
		private Color colorDisabled;
		private int myPointsUsed=0;
		//private int DiscussIdCur;//only has a value during discuss edit.

		public FormRequestEdit() {
			InitializeComponent();
			Lan.F(this);
			colorDisabled=Color.FromArgb(230, 229, 233);
		}

		private void FormRequestEdit_Load(object sender,EventArgs e) {
			if(IsAdminMode){
				textDescription.BackColor=Color.White;
				textDescription.ReadOnly=false;
				textDetail.BackColor=Color.White;
				textDetail.ReadOnly=false;
				checkIsMine.Visible=false;
				labelSubmitter.Visible=true;
				textSubmitter.Visible=true;
				textSubmitter.BackColor=colorDisabled;
				textDifficulty.BackColor=Color.White;
				textDifficulty.ReadOnly=false;
				groupMyVotes.Visible=false;
				butJordan.Visible=true;
				labelReqId.Visible=true;
				textRequestId.Visible=true;
				textRequestId.Text=RequestId.ToString();
				labelAdmin.Visible=true;
				labelAdmin.Location=groupMyVotes.Location;
				labelAdmin.Size=groupMyVotes.Size;
			}
			else{
				if(RequestId==0){//new
					//allow them to edit their description and detail
					textDescription.BackColor=Color.White;
					textDescription.ReadOnly=false;
					textDetail.BackColor=Color.White;
					textDetail.ReadOnly=false;
					butDelete.Visible=true;
					groupMyVotes.Visible=false;
				}
				else{
					//later on, it will test to see if isMine, and will then allow editing.
					textDescription.BackColor=colorDisabled;
					textDetail.BackColor=colorDisabled;
				}
				textDifficulty.BackColor=colorDisabled;
				comboApproval.Visible=false;
			}
			if(RequestId==0){
				checkIsMine.Checked=true;
				textDifficulty.Text="5";
				labelDiscuss.Visible=false;
				butAddDiscuss.Visible=false;
				textNote.Visible=false;
				//gridMain.Height=butDelete.Top-gridMain.Top-4;
				gridMain.Visible=false;
			}
			textApproval.BackColor=colorDisabled;
			comboApproval.Items.Add("New");
			comboApproval.Items.Add("NeedsClarification");
			comboApproval.Items.Add("Redundant");
			comboApproval.Items.Add("TooBroad");
			comboApproval.Items.Add("NotARequest");
			comboApproval.Items.Add("AlreadyDone");
			comboApproval.Items.Add("Obsolete");
			comboApproval.Items.Add("Approved");
			comboApproval.Items.Add("InProgress");
			comboApproval.Items.Add("Complete");
			comboApproval.SelectedIndex=0;
			textMyPoints.Text="0";
			textMyPledge.Text="0";
			if(RequestId!=0){
				GetOneFromServer();
				FillGrid();
			}
			textMyPointsRemain.BackColor=colorDisabled;
			textTotalPoints.BackColor=colorDisabled;
			textTotalCritical.BackColor=colorDisabled;
			textTotalPledged.BackColor=colorDisabled;
			textWeight.BackColor=colorDisabled;
		}

		private void GetOneFromServer(){
			//get a table with data
			Cursor=Cursors.WaitCursor;
			//prepare the xml document to send--------------------------------------------------------------------------------------
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;
			settings.IndentChars = ("    ");
			StringBuilder strbuild=new StringBuilder();
			using(XmlWriter writer=XmlWriter.Create(strbuild,settings)){
				writer.WriteStartElement("FeatureRequestGetOne");
				writer.WriteStartElement("RegistrationKey");
				writer.WriteString(PrefC.GetString("RegistrationKey"));
				writer.WriteEndElement();
				writer.WriteStartElement("RequestId");
				writer.WriteString(RequestId.ToString());
				writer.WriteEndElement();
				writer.WriteEndElement();
			}
			#if DEBUG
				OpenDental.localhost.Service1 updateService=new OpenDental.localhost.Service1();
			#else
				OpenDental.customerUpdates.Service1 updateService=new OpenDental.customerUpdates.Service1();
				updateService.Url=PrefC.GetString("UpdateServerAddress");
			#endif
			//Send the message and get the result-------------------------------------------------------------------------------------
			string result="";
			try {
				result=updateService.FeatureRequestGetOne(strbuild.ToString());
			}
			catch(Exception ex) {
				Cursor=Cursors.Default;
				MessageBox.Show("Error: "+ex.Message);
				return;
			}
			//textConnectionMessage.Text=Lan.g(this,"Connection successful.");
			//Application.DoEvents();
			Cursor=Cursors.Default;
			//MessageBox.Show(result);
			XmlDocument doc=new XmlDocument();
			doc.LoadXml(result);
			//Process errors------------------------------------------------------------------------------------------------------------
			XmlNode node=doc.SelectSingleNode("//Error");
			if(node!=null) {
				//textConnectionMessage.Text=node.InnerText;
				MessageBox.Show(node.InnerText,"Error");
				DialogResult=DialogResult.Cancel;
				return;
			}
			//Process a valid return value------------------------------------------------------------------------------------------------
			node=doc.SelectSingleNode("//ResultTable");
			tableObj=new ODDataTable(node.InnerXml);
			ODDataRow row=tableObj.Rows[0];
			textDescription.Text=row["Description"];
			string detail=row["Detail"];
			detail=detail.Replace("\n","\r\n");
			textDetail.Text=detail;
			checkIsMine.Checked=PIn.PBool(row["isMine"]);
			textDifficulty.Text=row["Difficulty"];
			int approval=PIn.PInt32(row["Approval"]);
			if(IsAdminMode){
				textSubmitter.Text=row["submitter"];
			}
			comboApproval.SelectedIndex=approval;
			//textApproval gets set automatically due to comboApproval_SelectedIndexChanged.
			if(!IsAdminMode && PIn.PBool(row["isMine"])){//user editing their own request
				if((ApprovalEnum)approval==ApprovalEnum.New
					|| (ApprovalEnum)approval==ApprovalEnum.NeedsClarification
					|| (ApprovalEnum)approval==ApprovalEnum.NotARequest
					|| (ApprovalEnum)approval==ApprovalEnum.Redundant
					|| (ApprovalEnum)approval==ApprovalEnum.TooBroad)
					//so user not allowed to edit if Approved,AlreadyDone,Obsolete, or InProgress.
				{
					textDescription.BackColor=Color.White;
					textDescription.ReadOnly=false;
					textDetail.BackColor=Color.White;
					textDetail.ReadOnly=false;
					if((ApprovalEnum)approval!=ApprovalEnum.New){
						butResubmit.Visible=true;
					}
					butDelete.Visible=true;
				}
			}
			if((ApprovalEnum)approval!=ApprovalEnum.Approved){
				//only allowed to vote on Approved features.
				//All others should always have zero votes, except InProgress and Complete
				groupMyVotes.Visible=false;
			}
			if((ApprovalEnum)approval==ApprovalEnum.Approved
				|| (ApprovalEnum)approval==ApprovalEnum.InProgress
				|| (ApprovalEnum)approval==ApprovalEnum.Complete)
			{//even administrators should not be able to change things at this point
				textDescription.BackColor=colorDisabled;
				textDescription.ReadOnly=true;
				textDetail.BackColor=colorDisabled;
				textDetail.ReadOnly=true;
			}
			myPointsUsed=PIn.PInt32(row["myPointsUsed"]);
			//textMyPointsRemain.Text=;this will be filled automatically when myPoints changes
			textMyPoints.Text=row["myPoints"];
			RecalcMyPoints();
			checkIsCritical.Checked=PIn.PBool(row["IsCritical"]);
			textMyPledge.Text=row["myPledge"];
			textTotalPoints.Text=row["totalPoints"];
			textTotalCritical.Text=row["totalCritical"];
			textTotalPledged.Text=row["totalPledged"];
			textWeight.Text=row["Weight"];
		}

		private void textMyPoints_TextChanged(object sender,EventArgs e) {
			RecalcMyPoints();
		}

		private void RecalcMyPoints(){
			try{
				int mypoints=0;
				if(textMyPoints.Text!=""){
					mypoints=Convert.ToInt32(textMyPoints.Text);
				}
				textMyPointsRemain.Text=(100-myPointsUsed-mypoints).ToString();
			}
			catch{
				textMyPointsRemain.Text="";
			}
		}

		private void butJordan_Click(object sender,EventArgs e) {
			textDescription.BackColor=Color.White;
			textDescription.ReadOnly=false;
			textDetail.BackColor=Color.White;
			textDetail.ReadOnly=false;
		}

		private void comboApproval_SelectedIndexChanged(object sender,EventArgs e) {
			int approval=comboApproval.SelectedIndex;
			switch((ApprovalEnum)approval){
				case ApprovalEnum.New:
					textApproval.Text="New. Awaiting review and approval.";
					break;
				case ApprovalEnum.NeedsClarification:
					textApproval.Text="Needs Clarification.";
					break;
				case ApprovalEnum.Redundant:
					textApproval.Text="Redundant. An identical request already exists.";
					break;
				case ApprovalEnum.TooBroad:
					textApproval.Text="Too broad. A request must be limited to one issue.";
					break;
				case ApprovalEnum.NotARequest:
					textApproval.Text="Not a request. Most likely a training issue.";
					break;
				case ApprovalEnum.AlreadyDone:
					textApproval.Text="Already done. Feature already exists in the software.";
					break;
				case ApprovalEnum.Obsolete:
					textApproval.Text="Obsolete. No longer applies to current version.";
					break;
				case ApprovalEnum.Approved:
					textApproval.Text="Approved. Cannot be edited by user.";
					break;
				case ApprovalEnum.InProgress:
					textApproval.Text="In Progress. Feature currently being programmed.";
					break;
				case ApprovalEnum.Complete:
					textApproval.Text="Complete. Feature has been implemented.";
					break;
			}
		}

		private void butResubmit_Click(object sender,EventArgs e) {
			//only visible if NeedsClarification,NotARequest,Redundant,or TooBroad
			if(!MsgBox.Show(this,true,"Only continue if you have revised the original request to comply better with submission guidelines.")){
				return;
			}
			comboApproval.SelectedIndex=0;
			butResubmit.Visible=false;
		}

		private void checkIsCritical_Click(object sender,EventArgs e) {
			if(checkIsCritical.Checked){
				if(!MsgBox.Show(this,true,"Are you sure this is really critical?  To qualify as critical, there would be no possible workarounds.  The missing feature would probably be seriously impacting the financial status of the office.  It would be serious enough that you might be considering using another software.")){
					checkIsCritical.Checked=false;
					return;
				}
			}
		}

		private void butAddDiscuss_Click(object sender,EventArgs e) {
			//button is not even visible if New
			if(textNote.Text==""){
				MsgBox.Show(this,"Please enter some text first.");
				return;
			}
			if(!SaveDiscuss()){
				return;
			}
			textNote.Text="";
			FillGrid();
		}

		///<summary>Never happens with a new request.</summary>
		private void FillGrid(){
			Cursor=Cursors.WaitCursor;
			//prepare the xml document to send--------------------------------------------------------------------------------------
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;
			settings.IndentChars = ("    ");
			StringBuilder strbuild=new StringBuilder();
			using(XmlWriter writer=XmlWriter.Create(strbuild,settings)){
				writer.WriteStartElement("FeatureRequestDiscussGetList");
				writer.WriteStartElement("RegistrationKey");
				writer.WriteString(PrefC.GetString("RegistrationKey"));
				writer.WriteEndElement();
				writer.WriteStartElement("RequestId");
				writer.WriteString(RequestId.ToString());
				writer.WriteEndElement();
				writer.WriteEndElement();
			}
			#if DEBUG
				OpenDental.localhost.Service1 updateService=new OpenDental.localhost.Service1();
			#else
				OpenDental.customerUpdates.Service1 updateService=new OpenDental.customerUpdates.Service1();
				updateService.Url=PrefC.GetString("UpdateServerAddress");
			#endif
			//Send the message and get the result-------------------------------------------------------------------------------------
			string result="";
			try {
				result=updateService.FeatureRequestDiscussGetList(strbuild.ToString());
			}
			catch(Exception ex) {
				Cursor=Cursors.Default;
				MessageBox.Show("Error: "+ex.Message);
				return;
			}
			//textConnectionMessage.Text=Lan.g(this,"Connection successful.");
			//Application.DoEvents();
			Cursor=Cursors.Default;
			//MessageBox.Show(result);
			XmlDocument doc=new XmlDocument();
			doc.LoadXml(result);
			//Process errors------------------------------------------------------------------------------------------------------------
			XmlNode node=doc.SelectSingleNode("//Error");
			if(node!=null) {
				//textConnectionMessage.Text=node.InnerText;
				MessageBox.Show(node.InnerText,"Error");
				return;
			}
			//Process a valid return value------------------------------------------------------------------------------------------------
			node=doc.SelectSingleNode("//ResultTable");
			ODDataTable table=new ODDataTable(node.InnerXml);
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableRequestDiscuss","Date"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRequestDiscuss","Note"),200);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<table.Rows.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(table.Rows[i]["dateTime"]);
				row.Cells.Add(table.Rows[i]["Note"]);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		///<summary></summary>
		private bool SaveDiscuss(){//bool doDelete) {
			//prepare the xml document to send--------------------------------------------------------------------------------------
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;
			settings.IndentChars = ("    ");
			StringBuilder strbuild=new StringBuilder();
			using(XmlWriter writer=XmlWriter.Create(strbuild,settings)){
				writer.WriteStartElement("FeatureRequestDiscussSubmit");
				//regkey
				writer.WriteStartElement("RegistrationKey");
				writer.WriteString(PrefC.GetString("RegistrationKey"));
				writer.WriteEndElement();
				//DiscussId
				//writer.WriteStartElement("DiscussId");
				//writer.WriteString(DiscussIdCur.ToString());//this will be zero for a new entry. We currently only support new entries
				//writer.WriteEndElement();
				//RequestId
				writer.WriteStartElement("RequestId");
				writer.WriteString(RequestId.ToString());
				writer.WriteEndElement();
				//can't pass patnum.  Determined on the server side.
				//date will also be figured on the server side.
				//Note
				writer.WriteStartElement("Note");
				writer.WriteString(textNote.Text);
				writer.WriteEndElement();
				/*if(doDelete){
					//delete
					writer.WriteStartElement("Delete");
					writer.WriteString("true");
					writer.WriteEndElement();
				}*/
			}
			Cursor=Cursors.WaitCursor;
			#if DEBUG
				OpenDental.localhost.Service1 updateService=new OpenDental.localhost.Service1();
			#else
				OpenDental.customerUpdates.Service1 updateService=new OpenDental.customerUpdates.Service1();
				updateService.Url=PrefC.GetString("UpdateServerAddress");
			#endif
			//Send the message and get the result-------------------------------------------------------------------------------------
			string result="";
			try {
				result=updateService.FeatureRequestDiscussSubmit(strbuild.ToString());
			}
			catch(Exception ex) {
				Cursor=Cursors.Default;
				MessageBox.Show("Error: "+ex.Message);
				return false;
			}
			//textConnectionMessage.Text=Lan.g(this,"Connection successful.");
			//Application.DoEvents();
			Cursor=Cursors.Default;
			//MessageBox.Show(result);
			XmlDocument doc=new XmlDocument();
			doc.LoadXml(result);
			//Process errors------------------------------------------------------------------------------------------------------------
			XmlNode node=doc.SelectSingleNode("//Error");
			if(node!=null) {
				//textConnectionMessage.Text=node.InnerText;
				MessageBox.Show(node.InnerText,"Error");
				return false;
			}
			return true;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			//only visible if New,NeedsClarification,NotARequest,Redundant,or TooBroad
			if(!MsgBox.Show(this,true,"Delete this entire request?")){
				return;
			}
			if(!SaveChangesToDb(true)){
				return;
			}
			DialogResult=DialogResult.OK;
		}

		///<summary>Only called when user clicks Delete or OK.  Not called repeatedly when adding discussions.</summary>
		private bool SaveChangesToDb(bool doDelete) {
			#region validation
			//validate---------------------------------------------------------------------------------------------------------
			int difficulty=0;
			int myPoints=0;
			double myPledge=0;
			if(!doDelete){
				if(textDescription.Text==""){
					MsgBox.Show(this,"Description cannot be blank.");
					return false;
				}
				//if(IsAdminMode){
				//had to do this for everyone, because initial diff setting gets ignored otherwise.
				try{
					difficulty=int.Parse(textDifficulty.Text);
				}
				catch{
					MsgBox.Show(this,"Difficulty is invalid.");
					return false;
				}
				if(difficulty<0 || difficulty>10){
					MsgBox.Show(this,"Difficulty is invalid.");
					return false;
				}
				//}
				//else{
				if(!IsAdminMode){
					try{
						myPoints=PIn.PInt32(textMyPoints.Text);//handles "" gracefully
					}
					catch{
						MsgBox.Show(this,"Points is invalid.");
						return false;
					}
					if(difficulty<0 || difficulty>100){
						MsgBox.Show(this,"Points is invalid.");
						return false;
					}
					//still need to validate that they have enough points.
					if(textMyPledge.Text==""){
						myPledge=0;
					}
					else{
						try{
							myPledge=double.Parse(textMyPledge.Text);
						}
						catch{
							MsgBox.Show(this,"Pledge is invalid.");
							return false;
						}
					}
					if(myPledge<0){
						MsgBox.Show(this,"Pledge is invalid.");
						return false;
					}
				}
				double myPointsRemain=PIn.PDouble(textMyPointsRemain.Text);
				if(myPointsRemain<0){
					MsgBox.Show(this,"You have gone over your allotted 100 points.");
					return false;
				}
			}
			//end of validation------------------------------------------------------------------------------------------------
			#endregion validation
			//if user has made no changes, then exit out-------------------------------------------------------------------------
			bool changesMade=false;
			if(doDelete){
				changesMade=true;
			}
			if(tableObj==null || tableObj.Rows.Count==0){//new
				changesMade=true;
			}
			else{
				ODDataRow row=tableObj.Rows[0];
				if(textDescription.Text!=row["Description"]){
					changesMade=true;
				}
				if(textDetail.Text!=row["Detail"]){
					changesMade=true;
				}
				if(textDifficulty.Text!=row["Difficulty"]){
					changesMade=true;
				}
				int approval=PIn.PInt32(row["Approval"]);
				if(comboApproval.SelectedIndex!=approval){
					changesMade=true;
				}
				if(groupMyVotes.Visible){
					if(textMyPoints.Text!=row["myPoints"]
						|| checkIsCritical.Checked!=PIn.PBool(row["IsCritical"])
						|| textMyPledge.Text!=row["myPledge"])
					{
						changesMade=true;
					}
				}
			}
			if(!changesMade){
				//temporarily show me which ones shortcutted out
				//MessageBox.Show("no changes made");
				return true;
			}
			Cursor=Cursors.WaitCursor;
			//prepare the xml document to send--------------------------------------------------------------------------------------
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;
			settings.IndentChars = ("    ");
			StringBuilder strbuild=new StringBuilder();
			using(XmlWriter writer=XmlWriter.Create(strbuild,settings)){
				writer.WriteStartElement("FeatureRequestSubmitChanges");
				//regkey
				writer.WriteStartElement("RegistrationKey");
				writer.WriteString(PrefC.GetString("RegistrationKey"));
				writer.WriteEndElement();
				//requestId
				writer.WriteStartElement("RequestId");
				writer.WriteString(RequestId.ToString());//this will be zero for a new request.
				writer.WriteEndElement();
				if(doDelete){
					//delete
					writer.WriteStartElement("Delete");
					writer.WriteString("true");//all the other elements will be ignored.
					writer.WriteEndElement();
				}
				else{
					if(!textDescription.ReadOnly){
						//description
						writer.WriteStartElement("Description");
						writer.WriteString(textDescription.Text);
						writer.WriteEndElement();
					}
					if(!textDetail.ReadOnly){
						//detail
						writer.WriteStartElement("Detail");
						writer.WriteString(textDetail.Text);
						writer.WriteEndElement();
					}
					if(IsAdminMode
						|| RequestId==0)//This allows the initial difficulty of 5 to get saved.
					{
						//difficulty
						writer.WriteStartElement("Difficulty");
						writer.WriteString(difficulty.ToString());
						writer.WriteEndElement();
					}
					//approval
					writer.WriteStartElement("Approval");
					writer.WriteString(comboApproval.SelectedIndex.ToString());
					writer.WriteEndElement();
					if(!IsAdminMode){
						//mypoints
						writer.WriteStartElement("MyPoints");
						writer.WriteString(myPoints.ToString());
						writer.WriteEndElement();
						//iscritical
						writer.WriteStartElement("IsCritical");
						if(checkIsCritical.Checked){
							writer.WriteString("1");
						}
						else{
							writer.WriteString("0");
						}
						writer.WriteEndElement();
						//mypledge
						writer.WriteStartElement("MyPledge");
						writer.WriteString(myPledge.ToString("f2"));
						writer.WriteEndElement();
					}
				}
				writer.WriteEndElement();
			}
			#if DEBUG
				OpenDental.localhost.Service1 updateService=new OpenDental.localhost.Service1();
			#else
				OpenDental.customerUpdates.Service1 updateService=new OpenDental.customerUpdates.Service1();
				updateService.Url=PrefC.GetString("UpdateServerAddress");
			#endif
			//Send the message and get the result-------------------------------------------------------------------------------------
			string result="";
			try {
				result=updateService.FeatureRequestSubmitChanges(strbuild.ToString());
			}
			catch(Exception ex) {
				Cursor=Cursors.Default;
				MessageBox.Show("Error: "+ex.Message);
				return false;
			}
			//textConnectionMessage.Text=Lan.g(this,"Connection successful.");
			//Application.DoEvents();
			Cursor=Cursors.Default;
			//MessageBox.Show(result);
			XmlDocument doc=new XmlDocument();
			doc.LoadXml(result);
			//Process errors------------------------------------------------------------------------------------------------------------
			XmlNode node=doc.SelectSingleNode("//Error");
			if(node!=null) {
				//textConnectionMessage.Text=node.InnerText;
				MessageBox.Show(node.InnerText,"Error");
				return false;
			}
			return true;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(textNote.Text!=""){
				MsgBox.Show(this,"You need to save your note first.");
				return;
			}
			if(!SaveChangesToDb(false)){
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		
	

		
		

		

		
	}

	/*
	///<summary>This object is used to organize all the datafields in FormRequestEdit.  It is used both for admin mode and for regular mode.</summary>
	public class Request{
		///<summary>Once approval is changed to Approved, this cannot be edited by submitter.</summary>
		public string Description;
		///<summary>Once approval is changed to Approved, this cannot be edited by submitter.</summary>
		public string Detail;
		///<summary></summary>
		public DateTime DateTSubmitted;
		///<summary>On the server, this is a PatNum.  Here on the client, it's true false.  This value is never sent to the server.  It's inferred on the server end.</summary>
		public bool IsMine;
		///<summary>Only set by Jordan.</summary>
		public int Difficulty;
		///<summary>Only set by admins.  In non-admin mode, this converts to a wordy description.</summary>
		public ApprovalEnum Approval;
		///<summary>Ignored when in admin mode.</summary>
		public int MyPoints;
		///<summary>Ignored when in admin mode.</summary>
		public bool IsCritical;
		///<summary>Ignored when in admin mode.</summary>
		public double MyPledge;
		///<summary>This is the points remaining with the assumption that zero points are consumed by this request.  Then, the points for the current request are subtracted from the amount before display.</summary>
		public int MyPointsRemain;
		///<summary>Just informational.  Nobody can edit.</summary>
		public string TotalPoints;
		///<summary>Just informational.  Nobody can edit.</summary>
		public string TotalCritical;
		///<summary>Just informational.  Nobody can edit.</summary>
		public string TotalPledged;
	}*/

	public enum ApprovalEnum{
		New,
		NeedsClarification,
		Redundant,
		TooBroad,
		NotARequest,
		AlreadyDone,
		Obsolete,
		Approved,
		InProgress,
		Complete
	}

}