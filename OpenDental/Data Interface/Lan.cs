using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary>Handles database commands for the language table in the database.</summary>
	public class Lan{
		///<summary>key=ClassType+English.  Value =Language object.</summary>
		public static Hashtable HList;
		///<summary></summary>
		private static Language[] List;
		///<summary>Used by g to keep track of whether any language items were inserted into db. If so a refresh gets done.</summary>
		private static bool itemInserted;

		///<summary>Refreshed automatically to always be kept current with all phrases, regardless of whether there are any entries in LanguageForeign table.</summary>
		public static void Refresh(){
			HList=new Hashtable();
			if(CultureInfo.CurrentCulture.Name=="en-US"){
				return;
			}
			string command="SELECT * from language";
			DataTable table=General.GetTable(command);
			List=new Language[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new Language();
				//List[i].EnglishCommentsOld= PIn.PString(table.Rows[i][0].ToString());
				List[i].ClassType      = PIn.PString(table.Rows[i][1].ToString());
				List[i].English        = PIn.PString(table.Rows[i][2].ToString());
				List[i].IsObsolete     = PIn.PBool  (table.Rows[i][3].ToString());
				if(!HList.ContainsKey(List[i].ClassType+List[i].English)){
					HList.Add(List[i].ClassType+List[i].English,List[i]);
				}
			}
			//MessageBox.Show(List.Length.ToString());
		}

		///<summary>Tries to insert, but ignores the insert if this row already exists. This prevents the previous frequent crashes.</summary>
		public static void Insert(Language Cur){
			//In Oracle, one must specify logging options for logging to occur, otherwise logging is not used.
			string ignoreClause="";
			if(DataConnection.DBtype==DatabaseType.MySql){
				ignoreClause="IGNORE";
			}
			string command = "INSERT "+ignoreClause+" INTO language (ClassType,English,EnglishComments,IsObsolete) "
				+"VALUES("
				+"'"+POut.PString(Cur.ClassType)+"', "
				+"'"+POut.PString(Cur.English)+"','',0)";
			//MessageBox.Show(command);
			General.NonQ(command);
		}

		/*
		///<summary>not used to update the english version of text.  Create new instead.</summary>
		public static void UpdateCur(){
			string command="UPDATE language SET "
				+"EnglishComments = '" +POut.PString(Cur.EnglishComments)+"'"
				+",IsObsolete = '"     +POut.PBool  (Cur.IsObsolete)+"'"
				+" WHERE ClassType = BINARY '"+POut.PString(Cur.ClassType)+"'"
				+" AND English = BINARY '"     +POut.PString(Cur.English)+"'";
			NonQ(false);
		}*/

		///<summary></summary>
		public static string[] GetListCat(){
			string command="SELECT Distinct ClassType FROM language ORDER BY ClassType ";
			DataTable table=General.GetTable(command);
			string[] ListCat=new string[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				ListCat[i]=PIn.PString(table.Rows[i][0].ToString());
			}
			return ListCat;
		}

		///<summary>Only used in translation tool to get list for one category</summary>
		public static Language[] GetListForCat(string classType){
			string command="SELECT * FROM language "
				+"WHERE ClassType = BINARY '"+POut.PString(classType)+"' ORDER BY English";
			DataTable table=General.GetTable(command);
			Language[] ListForCat=new Language[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				ListForCat[i]=new Language();
				//ListForCat[i].EnglishComments= PIn.PString(table.Rows[i][0].ToString());
				ListForCat[i].ClassType      = PIn.PString(table.Rows[i][1].ToString());
				ListForCat[i].English        = PIn.PString(table.Rows[i][2].ToString());
				ListForCat[i].IsObsolete     = PIn.PBool  (table.Rows[i][3].ToString());
			}
			return ListForCat;
		}

		private static string ConvertString(string classType,string text){
			if(CultureInfo.CurrentCulture.Name=="en-US"){
				return text;
			}
			if(text==""){
				return "";
			}
			if(HList==null) return text;
			itemInserted=false;
			//try{
			if(!HList.ContainsKey(classType+text)){
				Language Cur=new Language();
				Cur.ClassType=classType;
				Cur.English=text;
				//MessageBox.Show(Cur.ClassType+Cur.English);
				Insert(Cur);
				itemInserted=true;
				//Refresh();
				return text;
			}
			//}
			//catch{
			//	MessageBox.Show(classType+text);
			//}
			if(LanguageForeigns.HList.Contains(classType+text)){
				if(((LanguageForeign)LanguageForeigns.HList[classType+text]).Translation==""){
					//if translation is empty
					return text;//return the English version
				}
				return ((LanguageForeign)LanguageForeigns.HList[classType+text]).Translation;	
			}
			else{
				return text;
			}
		}

		//strings-----------------------------------------------
		///<summary>Converts a string to the current language.</summary>
		public static string g(string classType,string text){
			string retVal=ConvertString(classType,text);
			if(itemInserted)
				Refresh();
			return retVal;
		}

		///<summary>Converts a string to the current language.</summary>
		public static string g(System.Object sender,string text){
			string retVal=ConvertString(sender.GetType().Name,text);
			if(itemInserted)
				Refresh();
			return retVal;
		}

		//menuItems---------------------------------------------
		///<summary>C is for control. Translates the text of this control to another language.</summary>
		public static void C(string classType, System.Windows.Forms.MenuItem mi){
			mi.Text=ConvertString(classType,mi.Text);
			if(itemInserted)
				Refresh();
		}

		///<summary></summary>
		public static void C(System.Windows.Forms.Control sender, System.Windows.Forms.MenuItem mi){
			mi.Text=ConvertString(sender.GetType().Name,mi.Text);
			if(itemInserted)
				Refresh();
		}		

		//controls-----------------------------------------------
		///<summary></summary>
		public static void C(string classType, System.Windows.Forms.Control[] contr){
			for(int i=0;i<contr.Length;i++){
				contr[i].Text=ConvertString(classType,contr[i].Text);
			}
			if(itemInserted)
				Refresh();
		}

		///<summary></summary>
		public static void C(System.Windows.Forms.Control sender, System.Windows.Forms.Control[] contr){
			for(int i=0;i<contr.Length;i++){
				if(contr[i].GetType()==typeof(UI.ODGrid)){
					((UI.ODGrid)contr[i]).Title=ConvertString(((UI.ODGrid)contr[i]).TranslationName,((UI.ODGrid)contr[i]).Title);
					foreach(UI.ODGridColumn col in ((UI.ODGrid)contr[i]).Columns){
						col.Heading=ConvertString(((UI.ODGrid)contr[i]).TranslationName,col.Heading);
					}
					continue;
				}
				contr[i].Text=ConvertString(sender.GetType().Name,contr[i].Text);
			}
			if(itemInserted)
				Refresh();
		}

		//forms----------------------------------------------------------------------------------------
		///<summary>F is for Form. Translates the following controls on the entire form: title Text, labels, buttons, groupboxes, checkboxes, radiobuttons.  Can include a list of controls to exclude. Also puts all the correct controls into the All category (OK,Cancel,Close,Delete,etc).</summary>
		public static void F(System.Windows.Forms.Form sender){
			F(sender,new System.Windows.Forms.Control[] {});
		}

		///<summary>F is for Form. Translates the following controls on the entire form: title Text, labels, buttons, groupboxes, checkboxes, radiobuttons.  Can include a list of controls to exclude. Also puts all the correct controls into the All category (OK,Cancel,Close,Delete,etc).</summary>
		public static void F(Form sender,Control[] exclusions){
			if(CultureInfo.CurrentCulture.Name=="en-US"){
				return;
			}
			//first translate the main title Text on the form:
			if(!Contains(exclusions,sender)){
				sender.Text=ConvertString(sender.GetType().Name,sender.Text);
			}
			//then launch the recursive function for all child controls
			Fchildren(sender,sender,exclusions);
			if(itemInserted)
				Refresh();
		}

		///<summary>Called from F and also recursively. Translates all children of the given control except those in the exclusions list.</summary>
		private static void Fchildren(Form sender,Control parent,Control[] exclusions){
			foreach(Control contr in parent.Controls){
				if(contr.GetType()==typeof(UI.ODGrid)){
					((UI.ODGrid)contr).Title=ConvertString(((UI.ODGrid)contr).TranslationName,((UI.ODGrid)contr).Title);
					foreach(UI.ODGridColumn col in ((UI.ODGrid)contr).Columns){
						col.Heading=ConvertString(((UI.ODGrid)contr).TranslationName,col.Heading);
					}
					continue;
				}
				//any controls with children of their own.
				if(contr.HasChildren){
					Fchildren(sender,contr,exclusions);
				}
				//ignore any controls except the types we are interested in
				if(contr.GetType()!=typeof(TextBox)
					&& contr.GetType()!=typeof(Button)
					&& contr.GetType()!=typeof(OpenDental.UI.Button)
					&& contr.GetType()!=typeof(Label)
					&& contr.GetType()!=typeof(GroupBox)
					&& contr.GetType()!=typeof(CheckBox)
					&& contr.GetType()!=typeof(RadioButton))
				{
					continue;
				}
				if(contr.Text==""){
					continue;
				}
				if(!Contains(exclusions,contr)){
					if(contr.Text=="OK"
						|| contr.Text=="&OK"
						|| contr.Text=="Cancel"
						|| contr.Text=="&Cancel"
						|| contr.Text=="Close"
						|| contr.Text=="&Close"
						|| contr.Text=="Add"
						|| contr.Text=="&Add"
						|| contr.Text=="Delete"
						|| contr.Text=="&Delete"
						|| contr.Text=="Up"
						|| contr.Text=="&Up"
						|| contr.Text=="Down"
						|| contr.Text=="&Down"
						|| contr.Text=="Print"
						|| contr.Text=="&Print"
						//|| contr.Text==""
						){
						contr.Text=ConvertString("All",contr.Text);
					}
					else{
						contr.Text=ConvertString(sender.GetType().Name,contr.Text);
					}
				}
			}
		}

		///<summary>Returns true if the contrToFind exists in the supplied contrArray. Used to check the exclusion list of F.</summary>
		private static bool Contains(Control[] contrArray,Control contrToFind){
			for(int i=0;i<contrArray.Length;i++){
				if(contrArray[i]==contrToFind){
					return true;
				}
			}
			return false;
		}

		///<summary>Gets a short time format for displaying in appt and schedule along the sides. Create a clone of the current culture and pass it in. It will get altered. Returns a string format.</summary>
		public static string GetShortTimeFormat(CultureInfo ci){
			string hFormat="";
			ci.DateTimeFormat.AMDesignator=ci.DateTimeFormat.AMDesignator.ToLower();
			ci.DateTimeFormat.PMDesignator=ci.DateTimeFormat.PMDesignator.ToLower();
			string shortPattern=ci.DateTimeFormat.ShortTimePattern;
			if(shortPattern.IndexOf("hh")!=-1){//if hour is 01-12
				hFormat+="hh";
			}
			else if(shortPattern.IndexOf("h")!=-1){//or if hour is 1-12
				hFormat+="h";
			}
			else if(shortPattern.IndexOf("HH")!=-1){//or if hour is 00-23
				hFormat+="HH";
			}
			else{//hour is 0-23
				hFormat+="H";
			}
			if(shortPattern.IndexOf("t")!=-1){//if there is an am/pm designator
				hFormat+="tt";
			}
			else{//if no am/pm designator, then use :00
				hFormat+=":00";//time separator will actually change according to region
			}
			return hFormat;
		}

	}

	

	

}













