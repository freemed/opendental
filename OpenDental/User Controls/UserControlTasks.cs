using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental {
	public partial class UserControlTasks:UserControl {
		///<summary>List of all TastLists that are to be displayed in the main window. Combine with TasksList.</summary>
		private TaskList[] TaskListsList;
		///<summary>List of all Tasks that are to be displayed in the main window.  Combine with TaskListsList.</summary>
		private Task[] TasksList;
		///<summary>An arraylist of TaskLists beginning from the trunk and adding on branches.  If the count is 0, then we are in the trunk of one of the five categories.  The last TaskList in the TreeHistory is the one that is open in the main window.</summary>
		private ArrayList TreeHistory;
		///<summary>A TaskList that is on the 'clipboard' waiting to be pasted.  Will be null if nothing has been copied yet.</summary>
		private TaskList ClipTaskList;
		///<summary>A Task that is on the 'clipboard' waiting to be pasted.  Will be null if nothing has been copied yet.</summary>
		private Task ClipTask;
		///<summary>If there is an item on our 'clipboard', this tracks whether it was cut.</summary>
		private bool WasCut;
		///<summary>The index of the last clicked item in the main list.</summary>
		private int clickedI;
		///<summary>After closing, if this is not zero, then it will jump to the object specified in GotoKeyNum.</summary>
		public TaskObjectType GotoType;
		///<summary>After closing, if this is not zero, then it will jump to the specified patient.</summary>
		public int GotoKeyNum;
		///<summary></summary>
		[Category("Property Changed"),Description("Event raised when user wants to go to a patient or related object.")]
		public event EventHandler GoToChanged=null;

		public UserControlTasks() {
			InitializeComponent();
			this.listMain.ContextMenu = this.menuEdit;
			//Lan.F(this);
			for(int i=0;i<menuEdit.MenuItems.Count;i++) {
				Lan.C(this,menuEdit.MenuItems[i]);
			}
		}

		protected void OnGoToChanged() {
			if(GoToChanged!=null) {
				GoToChanged(this,new EventArgs());
			}
		}

		public void InitializeOnStartup(){
			LayoutToolBar();
			if(Tasks.LastOpenList==null) {//first time openning
				TreeHistory=new ArrayList();
				cal.SelectionStart=DateTime.Today;
			}
			else {//reopening
				tabContr.SelectedIndex=Tasks.LastOpenGroup;
				TreeHistory=new ArrayList();
				for(int i=0;i<Tasks.LastOpenList.Count;i++) {
					TreeHistory.Add(((TaskList)Tasks.LastOpenList[i]).Copy());
				}
				cal.SelectionStart=Tasks.LastOpenDate;
			}
			FillTree();
			FillMain();
			SetMenusEnabled();
		}

		private void UserControlTasks_Load(object sender,System.EventArgs e) {
			
		}

		///<summary></summary>
		public void LayoutToolBar() {
			ToolBarMain.Buttons.Clear();
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Add TaskList"),0,"","AddList"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Add Task"),1,"","AddTask"));
			//ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Exit"),-1,"","Exit"));
			ToolBarMain.Invalidate();
		}

		private void FillTree() {
			tree.Nodes.Clear();
			TreeNode node;
			//TreeNode lastNode=null;
			for(int i=0;i<TreeHistory.Count;i++) {
				node=new TreeNode(((TaskList)TreeHistory[i]).Descript);
				node.Tag=((TaskList)TreeHistory[i]).TaskListNum;
				if(tree.SelectedNode==null) {
					tree.Nodes.Add(node);
				}
				else {
					tree.SelectedNode.Nodes.Add(node);
				}
				tree.SelectedNode=node;
			}
			//remember this position for the next time we open tasks
			Tasks.LastOpenList=new ArrayList();
			for(int i=0;i<TreeHistory.Count;i++) {
				Tasks.LastOpenList.Add(((TaskList)TreeHistory[i]).Copy());
			}
			Tasks.LastOpenGroup=tabContr.SelectedIndex;
			Tasks.LastOpenDate=cal.SelectionStart;
			//layout
			if(tabContr.SelectedIndex==0) {//main
				tree.Top=tabContr.Bottom;
			}
			else if(tabContr.SelectedIndex==1) {//repeating
				tree.Top=tabContr.Bottom;
			}
			else {//by date
				tree.Top=cal.Bottom+1;
			}
			tree.Height=TreeHistory.Count*tree.ItemHeight+8;
			tree.Refresh();
			listMain.Top=tree.Bottom;
			listMain.Height=this.ClientSize.Height-listMain.Top-3;
		}

		private void FillMain() {
			int parent;
			DateTime date;
			if(TreeHistory.Count>0) {//not on main trunk
				parent=((TaskList)TreeHistory[TreeHistory.Count-1]).TaskListNum;
				date=DateTime.MinValue;
			}
			else {//one of the main trunks
				parent=0;
				date=cal.SelectionStart;
			}
			RefreshMainLists(parent,date);
			if(TreeHistory.Count==0//main trunk
				&& (tabContr.SelectedIndex==2	|| tabContr.SelectedIndex==3 || tabContr.SelectedIndex==4))//any of the dated groups
			{
				//clear any lists which are derived from a repeating list and which do not have any itmes checked off
				bool changeMade=false;
				for(int i=0;i<TaskListsList.Length;i++) {
					if(TaskListsList[i].FromNum==0) {//ignore because not derived from a repeating list
						continue;
					}
					if(!AnyAreMarkedComplete(TaskListsList[i])) {
						DeleteEntireList(TaskListsList[i]);
						changeMade=true;
					}
				}
				//clear any tasks which are derived from a repeating tast and which are not checked off
				for(int i=0;i<TasksList.Length;i++) {
					if(TasksList[i].FromNum==0) {
						continue;
					}
					if(!TasksList[i].TaskStatus) {
						Tasks.Delete(TasksList[i]);
						changeMade=true;
					}
				}
				if(changeMade) {
					RefreshMainLists(parent,date);
				}
				//now add back any repeating lists and tasks that meet the criteria
				//Get lists of all repeating lists and tasks of one type.  We will pick items from these two lists.
				TaskList[] repeatingLists=new TaskList[0];
				Task[] repeatingTasks=new Task[0];
				switch(tabContr.SelectedIndex) {
					case 2:
						repeatingLists=TaskLists.Refresh(-1,DateTime.MinValue,TaskDateType.Day,true);
						repeatingTasks=Tasks.Refresh(-1,DateTime.MinValue,TaskDateType.Day,true);
						break;
					case 3:
						repeatingLists=TaskLists.Refresh(-1,DateTime.MinValue,TaskDateType.Week,true);
						repeatingTasks=Tasks.Refresh(-1,DateTime.MinValue,TaskDateType.Week,true);
						break;
					case 4:
						repeatingLists=TaskLists.Refresh(-1,DateTime.MinValue,TaskDateType.Month,true);
						repeatingTasks=Tasks.Refresh(-1,DateTime.MinValue,TaskDateType.Month,true);
						break;
				}
				//loop through list and add back any that meet criteria.
				changeMade=false;
				bool alreadyExists;
				for(int i=0;i<repeatingLists.Length;i++) {
					//if already exists, skip
					alreadyExists=false;
					for(int j=0;j<TaskListsList.Length;j++) {//loop through Main list
						if(TaskListsList[j].FromNum==repeatingLists[i].TaskListNum) {
							alreadyExists=true;
							break;
						}
					}
					if(alreadyExists) {
						continue;
					}
					//otherwise, duplicate the list
					repeatingLists[i].DateTL=date;
					repeatingLists[i].FromNum=repeatingLists[i].TaskListNum;
					repeatingLists[i].IsRepeating=false;
					repeatingLists[i].Parent=0;
					repeatingLists[i].ObjectType=0;//user will have to set explicitly
					DuplicateExistingList(repeatingLists[i],true);
					changeMade=true;
				}
				for(int i=0;i<repeatingTasks.Length;i++) {
					//if already exists, skip
					alreadyExists=false;
					for(int j=0;j<TasksList.Length;j++) {//loop through Main list
						if(TasksList[j].FromNum==repeatingTasks[i].TaskNum) {
							alreadyExists=true;
							break;
						}
					}
					if(alreadyExists) {
						continue;
					}
					//otherwise, duplicate the task
					repeatingTasks[i].DateTask=date;
					repeatingTasks[i].FromNum=repeatingTasks[i].TaskNum;
					repeatingTasks[i].IsRepeating=false;
					repeatingTasks[i].TaskListNum=0;
					Tasks.InsertOrUpdate(repeatingTasks[i],true);
					changeMade=true;
				}
				if(changeMade) {
					RefreshMainLists(parent,date);
				}
			}//if main trunk on dated group
			listMain.Items.Clear();
			ListViewItem item;
			string dateStr="";
			for(int i=0;i<TaskListsList.Length;i++) {
				dateStr="";
				if(TaskListsList[i].DateTL.Year>1880
					&& tabContr.SelectedIndex==0)//main
				{
					//dateStr=TaskListsList[i].DateTL.ToShortDateString()+" - ";
					if(TaskListsList[i].DateType==TaskDateType.Day) {
						dateStr=TaskListsList[i].DateTL.ToShortDateString()+" - ";
					}
					else if(TaskListsList[i].DateType==TaskDateType.Week) {
						dateStr=Lan.g(this,"Week of")+" "+TaskListsList[i].DateTL.ToShortDateString()+" - ";
					}
					else if(TaskListsList[i].DateType==TaskDateType.Month) {
						dateStr=TaskListsList[i].DateTL.ToString("MMMM")+" - ";
					}
				}
				item=new ListViewItem(dateStr+TaskListsList[i].Descript,0);
				item.ToolTipText=item.Text;
				listMain.Items.Add(item);
			}
			string objDesc="";
			for(int i=0;i<TasksList.Length;i++) {
				//checked=1, unchecked=2
				dateStr="";
				if(tabContr.SelectedIndex==0) {//main
					if(TasksList[i].DateTask.Year>1880) {
						if(TasksList[i].DateType==TaskDateType.Day) {
							dateStr=TasksList[i].DateTask.ToShortDateString()+" - ";
						}
						else if(TasksList[i].DateType==TaskDateType.Week) {
							dateStr=Lan.g(this,"Week of")+" "+TasksList[i].DateTask.ToShortDateString()+" - ";
						}
						else if(TasksList[i].DateType==TaskDateType.Month) {
							dateStr=TasksList[i].DateTask.ToString("MMMM")+" - ";
						}
					}
					else if(TasksList[i].DateTimeEntry.Year>1880) {
						dateStr=TasksList[i].DateTimeEntry.ToShortDateString()+" - ";
					}
				}
				objDesc="";
				if(TasksList[i].ObjectType==TaskObjectType.Patient) {
					if(TasksList[i].KeyNum!=0) {
						objDesc=Patients.GetPat(TasksList[i].KeyNum).GetNameLF()+" - ";
					}
				}
				else if(TasksList[i].ObjectType==TaskObjectType.Appointment) {
					if(TasksList[i].KeyNum!=0) {
						Appointment AptCur=Appointments.GetOneApt(TasksList[i].KeyNum);
						if(AptCur!=null) {
							objDesc=Patients.GetPat(AptCur.PatNum).GetNameLF()
								+"  "+AptCur.AptDateTime.ToString()
								+"  "+AptCur.ProcDescript
								+"  "+AptCur.Note
								+" - ";
						}
					}
				}
				if(TasksList[i].TaskStatus) {//complete
					item=new ListViewItem(dateStr+objDesc+TasksList[i].Descript,1);
				}
				else {
					item=new ListViewItem(dateStr+objDesc+TasksList[i].Descript,2);
				}
				item.ToolTipText=item.Text;
				listMain.Items.Add(item);
			}
		}

		///<summary>A recursive function that checks every child in a list IsFromRepeating.  If any are marked complete, then it returns true, signifying that this list should be immune from being deleted since it's already in use.</summary>
		private bool AnyAreMarkedComplete(TaskList list) {
			//get all children:
			TaskList[] childLists=TaskLists.Refresh(list.TaskListNum,DateTime.MinValue,TaskDateType.None,false);
			Task[] childTasks=Tasks.Refresh(list.TaskListNum,DateTime.MinValue,TaskDateType.None,false);
			for(int i=0;i<childLists.Length;i++) {
				if(AnyAreMarkedComplete(childLists[i])) {
					return true;
				}
			}
			for(int i=0;i<childTasks.Length;i++) {
				if(childTasks[i].TaskStatus) {
					return true;
				}
			}
			return false;
		}

		private void RefreshMainLists(int parent,DateTime date) {
			if(this.DesignMode){
				TaskListsList=new TaskList[0];
				TasksList=new Task[0];
				return;
			}
			if(tabContr.SelectedIndex==0) {//main
				TaskListsList=TaskLists.Refresh(parent,DateTime.MinValue,TaskDateType.None,false);
				TasksList=Tasks.Refresh(parent,DateTime.MinValue,TaskDateType.None,false);
			}
			else if(tabContr.SelectedIndex==1) {//repeating
				TaskListsList=TaskLists.Refresh(parent,DateTime.MinValue,TaskDateType.None,true);
				TasksList=Tasks.Refresh(parent,DateTime.MinValue,TaskDateType.None,true);
			}
			else if(tabContr.SelectedIndex==2) {//date
				TaskListsList=TaskLists.Refresh(parent,date,TaskDateType.Day,false);
				TasksList=Tasks.Refresh(parent,date,TaskDateType.Day,false);
			}
			else if(tabContr.SelectedIndex==3) {//week
				TaskListsList=TaskLists.Refresh(parent,date,TaskDateType.Week,false);
				TasksList=Tasks.Refresh(parent,date,TaskDateType.Week,false);
			}
			else if(tabContr.SelectedIndex==4) {//month
				TaskListsList=TaskLists.Refresh(parent,date,TaskDateType.Month,false);
				TasksList=Tasks.Refresh(parent,date,TaskDateType.Month,false);
			}
		}

		private void tabContr_Click(object sender,System.EventArgs e) {
			TreeHistory=new ArrayList();//clear the tree no matter which tab clicked.
			FillTree();
			FillMain();
		}

		private void cal_DateSelected(object sender,System.Windows.Forms.DateRangeEventArgs e) {
			TreeHistory=new ArrayList();//clear the tree
			FillTree();
			FillMain();
		}

		private void ToolBarMain_ButtonClick(object sender,OpenDental.UI.ODToolBarButtonClickEventArgs e) {
			//if(e.Button.Tag.GetType()==typeof(string)){
			//standard predefined button
			switch(e.Button.Tag.ToString()) {
				case "AddList":
					OnAddList_Click();
					break;
				case "AddTask":
					OnAddTask_Click();
					break;
				//case "Exit":
				//	Close();
				//	break;
			}
		}

		private void OnAddList_Click() {
			TaskList cur=new TaskList();
			//if this is a child of any other taskList
			if(TreeHistory.Count>0) {
				cur.Parent=((TaskList)TreeHistory[TreeHistory.Count-1]).TaskListNum;
			}
			else {
				cur.Parent=0;
				if(tabContr.SelectedIndex==2) {//by date
					cur.DateTL=cal.SelectionStart;
					cur.DateType=TaskDateType.Day;
				}
				else if(tabContr.SelectedIndex==3) {//by week
					cur.DateTL=cal.SelectionStart;
					cur.DateType=TaskDateType.Week;
				}
				else if(tabContr.SelectedIndex==4) {//by month
					cur.DateTL=cal.SelectionStart;
					cur.DateType=TaskDateType.Month;
				}
			}
			if(tabContr.SelectedIndex==1) {//repeating
				cur.IsRepeating=true;
			}
			FormTaskListEdit FormT=new FormTaskListEdit(cur);
			FormT.IsNew=true;
			FormT.ShowDialog();
			FillMain();
		}

		private void OnAddTask_Click() {
			Task cur=new Task();
			//if this is a child of any taskList
			if(TreeHistory.Count>0) {
				cur.TaskListNum=((TaskList)TreeHistory[TreeHistory.Count-1]).TaskListNum;
			}
			else {
				cur.TaskListNum=0;
				if(tabContr.SelectedIndex==2) {//by date
					cur.DateTask=cal.SelectionStart;
					cur.DateType=TaskDateType.Day;
				}
				else if(tabContr.SelectedIndex==3) {//by week
					cur.DateTask=cal.SelectionStart;
					cur.DateType=TaskDateType.Week;
				}
				else if(tabContr.SelectedIndex==4) {//by month
					cur.DateTask=cal.SelectionStart;
					cur.DateType=TaskDateType.Month;
				}
			}
			if(tabContr.SelectedIndex==1) {//repeating
				cur.IsRepeating=true;
			}
			FormTaskEdit FormT=new FormTaskEdit(cur);
			FormT.IsNew=true;
			FormT.ShowDialog();
			if(FormT.GotoType!=TaskObjectType.None) {
				GotoType=FormT.GotoType;
				GotoKeyNum=FormT.GotoKeyNum;
				OnGoToChanged();
				//DialogResult=DialogResult.OK;
				return;
			}
			FillMain();
		}

		private void OnEdit_Click() {
			if(clickedI < TaskListsList.Length) {//is list
				FormTaskListEdit FormT=new FormTaskListEdit(TaskListsList[clickedI]);
				FormT.ShowDialog();
			}
			else {//task
				FormTaskEdit FormT
					=new FormTaskEdit(TasksList[clickedI-TaskListsList.Length]);
				FormT.ShowDialog();
				if(FormT.GotoType!=TaskObjectType.None) {
					GotoType=FormT.GotoType;
					GotoKeyNum=FormT.GotoKeyNum;
					OnGoToChanged();
					//DialogResult=DialogResult.OK;
					return;
				}
			}
			FillMain();
		}

		private void OnCut_Click() {
			if(clickedI < TaskListsList.Length) {//is list
				ClipTaskList=TaskListsList[clickedI].Copy();
				ClipTask=null;
			}
			else {//task
				ClipTaskList=null;
				ClipTask=TasksList[clickedI-TaskListsList.Length].Copy();
			}
			WasCut=true;
		}

		private void OnCopy_Click() {
			if(clickedI < TaskListsList.Length) {//is list
				ClipTaskList=TaskListsList[clickedI].Copy();
				ClipTask=null;
			}
			else {//task
				ClipTaskList=null;
				ClipTask=TasksList[clickedI-TaskListsList.Length].Copy();
			}
			WasCut=false;
		}

		private void OnPaste_Click() {
			if(ClipTaskList!=null) {//a taskList is on the clipboard
				TaskList newTL=ClipTaskList.Copy();
				if(TreeHistory.Count>0) {//not on main trunk
					newTL.Parent=((TaskList)TreeHistory[TreeHistory.Count-1]).TaskListNum;
					switch(tabContr.SelectedIndex) {
						case 0://main
							//even though usually only trunks are dated, we will leave the date alone in main
							//category since user may wish to preserve it. All other children get date cleared.
							break;
						case 1://repeating
							newTL.DateTL=DateTime.MinValue;//never a date
							//leave dateType alone, since that affects how it repeats
							break;
						case 2://day
						case 3://week
						case 4://month
							newTL.DateTL=DateTime.MinValue;//children do not get dated
							newTL.DateType=TaskDateType.None;//this doesn't matter either for children
							break;
					}
				}
				else {//one of the main trunks
					newTL.Parent=0;
					switch(tabContr.SelectedIndex) {
						case 0://main
							newTL.DateTL=DateTime.MinValue;
							newTL.DateType=TaskDateType.None;
							break;
						case 1://repeating
							newTL.DateTL=DateTime.MinValue;//never a date
							//newTL.DateType=TaskDateType.None;//leave alone
							break;
						case 2://day
							newTL.DateTL=cal.SelectionStart;
							newTL.DateType=TaskDateType.Day;
							break;
						case 3://week
							newTL.DateTL=cal.SelectionStart;
							newTL.DateType=TaskDateType.Week;
							break;
						case 4://month
							newTL.DateTL=cal.SelectionStart;
							newTL.DateType=TaskDateType.Month;
							break;
					}
				}
				if(tabContr.SelectedIndex==1) {//repeating
					newTL.IsRepeating=true;
				}
				else {
					newTL.IsRepeating=false;
				}
				newTL.FromNum=0;//always
				if(tabContr.SelectedIndex==0) {
					DuplicateExistingList(newTL,true);
				}
				else {
					DuplicateExistingList(newTL,false);
				}
			}
			if(ClipTask!=null) {//a task is on the clipboard
				Task newT=ClipTask.Copy();
				if(TreeHistory.Count>0) {//not on main trunk
					newT.TaskListNum=((TaskList)TreeHistory[TreeHistory.Count-1]).TaskListNum;
					switch(tabContr.SelectedIndex) {
						case 0://main
							//even though usually only trunks are dated, we will leave the date alone in main
							//category since user may wish to preserve it. All other children get date cleared.
							break;
						case 1://repeating
							newT.DateTask=DateTime.MinValue;//never a date
							//leave dateType alone, since that affects how it repeats
							break;
						case 2://day
						case 3://week
						case 4://month
							newT.DateTask=DateTime.MinValue;//children do not get dated
							newT.DateType=TaskDateType.None;//this doesn't matter either for children
							break;
					}
				}
				else {//one of the main trunks
					newT.TaskListNum=0;
					switch(tabContr.SelectedIndex) {
						case 0://main
							newT.DateTask=DateTime.MinValue;
							newT.DateType=TaskDateType.None;
							break;
						case 1://repeating
							newT.DateTask=DateTime.MinValue;//never a date
							//newTL.DateType=TaskDateType.None;//leave alone
							break;
						case 2://day
							newT.DateTask=cal.SelectionStart;
							newT.DateType=TaskDateType.Day;
							break;
						case 3://week
							newT.DateTask=cal.SelectionStart;
							newT.DateType=TaskDateType.Week;
							break;
						case 4://month
							newT.DateTask=cal.SelectionStart;
							newT.DateType=TaskDateType.Month;
							break;
					}
				}
				if(tabContr.SelectedIndex==1) {//repeating
					newT.IsRepeating=true;
				}
				else {
					newT.IsRepeating=false;
				}
				newT.FromNum=0;//always
				Tasks.InsertOrUpdate(newT,true);
			}
			if(WasCut) {
				if(ClipTaskList!=null) {
					DeleteEntireList(ClipTaskList);
				}
				else if(ClipTask!=null) {
					Tasks.Delete(ClipTask);
				}
			}
			FillMain();
		}

		private void OnGoto_Click() {
			//not even allowed to get to this point unless a valid task
			Task task=TasksList[clickedI-TaskListsList.Length];
			GotoType=task.ObjectType;
			GotoKeyNum=task.KeyNum;
			OnGoToChanged();
			//DialogResult=DialogResult.OK;
		}

		///<summary>A recursive function that duplicates an entire existing TaskList.  For the initial loop, make changes to the original taskList before passing it in.  That way, Date and type are only set in initial loop.  All children preserve original dates and types.  The isRepeating value will be applied in all loops.  Also, make sure to change the parent num to the new one before calling this function.  The taskListNum will always change, because we are inserting new record into database.</summary>
		private void DuplicateExistingList(TaskList newList,bool isInMain) {
			//get all children:
			TaskList[] childLists=TaskLists.Refresh(newList.TaskListNum,DateTime.MinValue,TaskDateType.None,newList.IsRepeating);
			Task[] childTasks=Tasks.Refresh(newList.TaskListNum,DateTime.MinValue,TaskDateType.None,newList.IsRepeating);
			TaskLists.InsertOrUpdate(newList,true);
			//now we have a new taskListNum to work with
			for(int i=0;i<childLists.Length;i++) {
				childLists[i].Parent=newList.TaskListNum;
				if(newList.IsRepeating) {
					childLists[i].IsRepeating=true;
					childLists[i].DateTL=DateTime.MinValue;//never a date
				}
				else {
					childLists[i].IsRepeating=false;
				}
				childLists[i].FromNum=0;
				if(!isInMain) {
					childLists[i].DateTL=DateTime.MinValue;
					childLists[i].DateType=TaskDateType.None;
				}
				DuplicateExistingList(childLists[i],isInMain);
			}
			for(int i=0;i<childTasks.Length;i++) {
				childTasks[i].TaskListNum=newList.TaskListNum;
				if(newList.IsRepeating) {
					childTasks[i].IsRepeating=true;
					childTasks[i].DateTask=DateTime.MinValue;//never a date
				}
				else {
					childTasks[i].IsRepeating=false;
				}
				childTasks[i].FromNum=0;
				if(!isInMain) {
					childTasks[i].DateTask=DateTime.MinValue;
					childTasks[i].DateType=TaskDateType.None;
				}
				Tasks.InsertOrUpdate(childTasks[i],true);
			}
		}

		private void OnDelete_Click() {
			if(clickedI < TaskListsList.Length) {//is list
				if(!MsgBox.Show(this,true,"Delete list including all sublists and tasks?")) {
					return;
				}
				DeleteEntireList(TaskListsList[clickedI]);
			}
			else {//Is task
				if(!MsgBox.Show(this,true,"Delete?")) {
					return;
				}
				Tasks.Delete(TasksList[clickedI-TaskListsList.Length]);
			}
			FillMain();
		}

		///<summary>A recursive function that deletes the specified list and all children.</summary>
		private void DeleteEntireList(TaskList list) {
			//get all children:
			TaskList[] childLists=TaskLists.Refresh(list.TaskListNum,DateTime.MinValue,TaskDateType.None,list.IsRepeating);
			Task[] childTasks=Tasks.Refresh(list.TaskListNum,DateTime.MinValue,TaskDateType.None,list.IsRepeating);
			for(int i=0;i<childLists.Length;i++) {
				DeleteEntireList(childLists[i]);
			}
			for(int i=0;i<childTasks.Length;i++) {
				Tasks.Delete(childTasks[i]);
			}
			try {
				TaskLists.Delete(list);
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
			}
		}

		private void listMain_Click(object sender,System.EventArgs e) {
			/*if(listMain.SelectedIndices.Count==0){
				return;
			}
			if(clickedI < TaskListsList.Length){//is list
				TreeHistory.Add(TaskListsList[listMain.SelectedIndices[0]]);
				FillTree();
				FillMain();
			}*/
		}

		private void listMain_DoubleClick(object sender,System.EventArgs e) {
			if(clickedI==-1) {
				return;
			}
			if(clickedI >= TaskListsList.Length) {//is task
				FormTaskEdit FormT=new FormTaskEdit(TasksList[clickedI-TaskListsList.Length]);
				FormT.ShowDialog();
				if(FormT.GotoType!=TaskObjectType.None) {
					GotoType=FormT.GotoType;
					GotoKeyNum=FormT.GotoKeyNum;
					OnGoToChanged();
					//DialogResult=DialogResult.OK;
					return;
				}
			}
			FillMain();
		}

		private void listMain_MouseDown(object sender,System.Windows.Forms.MouseEventArgs e) {
			ListViewItem ClickedItem=listMain.GetItemAt(e.X,e.Y);
			if(ClickedItem==null) {
				return;
			}
			clickedI=ClickedItem.Index;
			if(e.Button!=MouseButtons.Left) {
				return;
			}
			if(clickedI < TaskListsList.Length) {//is list
				TreeHistory.Add(TaskListsList[clickedI]);
				FillTree();
				FillMain();
				return;
			}
			//check tasks off
			if(e.X>16) {
				return;
			}
			Task task=TasksList[clickedI-TaskListsList.Length].Copy();
			task.TaskStatus= !task.TaskStatus;
			try {
				Tasks.InsertOrUpdate(task,false);
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
				return;
			}
			FillMain();
		}

		private void menuEdit_Popup(object sender,System.EventArgs e) {
			SetMenusEnabled();
		}

		private void SetMenusEnabled() {
			if(listMain.SelectedIndices.Count==0) {
				menuItemEdit.Enabled=false;
				menuItemCut.Enabled=false;
				menuItemCopy.Enabled=false;
				menuItemDelete.Enabled=false;
			}
			else {
				menuItemEdit.Enabled=true;
				menuItemCut.Enabled=true;
				menuItemCopy.Enabled=true;
				menuItemDelete.Enabled=true;
			}
			if(ClipTaskList==null && ClipTask==null) {
				menuItemPaste.Enabled=false;
			}
			else {//there is an item on our clipboard
				menuItemPaste.Enabled=true;
			}
			if(listMain.SelectedIndices.Count>0
				&& clickedI >= TaskListsList.Length)//is task
			{
				Task task=TasksList[clickedI-TaskListsList.Length];
				if(task.ObjectType==TaskObjectType.None) {
					menuItemGoto.Enabled=false;
				}
				else {
					menuItemGoto.Enabled=true;
				}
			}
			else {
				menuItemGoto.Enabled=false;//not a task
			}
		}

		private void menuItemEdit_Click(object sender,System.EventArgs e) {
			OnEdit_Click();
		}

		private void menuItemCut_Click(object sender,System.EventArgs e) {
			OnCut_Click();
		}

		private void menuItemCopy_Click(object sender,System.EventArgs e) {
			OnCopy_Click();
		}

		private void menuItemPaste_Click(object sender,System.EventArgs e) {
			OnPaste_Click();
		}

		private void menuItemDelete_Click(object sender,System.EventArgs e) {
			OnDelete_Click();
		}

		private void menuItemGoto_Click(object sender,System.EventArgs e) {
			OnGoto_Click();
		}

		private void listMain_SelectedIndexChanged(object sender,System.EventArgs e) {
			SetMenusEnabled();
		}

		private void tree_MouseDown(object sender,System.Windows.Forms.MouseEventArgs e) {
			for(int i=TreeHistory.Count-1;i>0;i--) {
				if(((TaskList)TreeHistory[i]).TaskListNum==(int)tree.GetNodeAt(e.X,e.Y).Tag) {
					break;//don't remove the node click on or any higher node
				}
				TreeHistory.RemoveAt(i);
			}
			FillTree();
			FillMain();
		}

		




	}
}
