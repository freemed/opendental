using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public class InsPlanL {
		///<summary>The list of planNums passed in will also include this plan.  If calling from the Trojan ProcessPlansUpdate (ALLPLANS.TXT on startup), then thisPlanNum will be 0.  isForAll will be true if called from the list of ins plans.  In this case, thisPlanNum will be 0.  noteOld must contain the note as it was when the user first opened the form.  We will compare against it to make sure it didn't change.  Will return false if user cancelled.</summary>
		public static bool SynchronizePlanNote(List<long> planNums,long thisPlanNum,string note,string noteOld, bool isForAll){
			if(planNums.Count==1 && isForAll){//If there is only one plan when calling from the plan list.  Because we do not have access to the planNum.
				InsPlans.UpdateNoteForPlans(planNums,note);
				return true;
			}
			if(planNums.Count<=1){//if no similar plans
				//note has already been saved
				return true;
			}
			//Get a list of all distinct notes, not including this one
			string[] notesSimilar=InsPlans.GetNotesForPlans(planNums,thisPlanNum);//excludes thisPlanNum (0 if IsForAll)
			bool curNoteInSimilar=false;
			for(int i=0;i<notesSimilar.Length;i++){
				if(notesSimilar[i]==note){
					curNoteInSimilar=true;
				}
			}
			string[] notesAll;//this has all distinct notes INCLUDING PlanCur.PlanNote
			if(curNoteInSimilar){//the curNote is alread in notesSimilar
				notesAll=new string[notesSimilar.Length];
				notesSimilar.CopyTo(notesAll,0);
			}
			else{
				notesAll=new string[notesSimilar.Length+1];
				notesSimilar.CopyTo(notesAll,1);
				notesAll[0]=note;//curNote will be at position 0
			}
			if(notesSimilar.Length==0){
				//probably because there are not even any other similar plans
				//note has already been saved
			}
			else if(notesSimilar.Length==1 && notesSimilar[0]==note){//all notes are already the same
				//note has already been saved
			}
			else if(notesSimilar.Length==1 && notesSimilar[0]==noteOld){//notes were all the same until user just changed it
				//this also handles 'deleting' a note
				InsPlans.UpdateNoteForPlans(planNums,note);
			}
			//this note is different than the other notes
			//js 5/7/10 Regardless of why it's different, give the user a choice.
			//could be: if(IsNewPlan){//but it's a new plan, so user simply isn't aware of difference
			//or there could be a variety of notes for some reason.  Possibly a former bug, an improper conversion, or a Trojan import.
			else{//must give user a choice of notes
				FormNotePick FormN=new FormNotePick(notesAll);
				FormN.ShowDialog();
				if(FormN.DialogResult!=DialogResult.OK){
					return false;//the note was already saved before calling this method.  But this allows user to stay in the form to reconsider.
				}
				InsPlans.UpdateNoteForPlans(planNums,FormN.SelectedNote);
			}
			return true;
		}














	}
}
