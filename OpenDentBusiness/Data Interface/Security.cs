using CodeBase;
using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Web;
using System.Windows.Forms;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Security{
		///<summary>The current user.  Might be null when first starting the program.  Otherwise, must contain valid user.</summary>
		private static Userod curUser;

		public static Userod CurUser {
			get {
				if(RemotingClient.RemotingRole==RemotingRole.ServerWeb) {
					throw new ApplicationException("Security.Userod not accessible from RemotingRole.ServerWeb.");
				}
				return curUser;
			}
			set {
				if(RemotingClient.RemotingRole==RemotingRole.ServerWeb) {
					throw new ApplicationException("Security.Userod not accessible from RemotingRole.ServerWeb.");
				}
				curUser=value;
			}
		}

		///<summary></summary>
		public Security(){
			//No need to check RemotingRole; no call to db.
		}

		///<summary>Checks to see if current user is authorized.  It also checks any date restrictions.  If not authorized, it gives a Message box saying so and returns false.</summary>
		public static bool IsAuthorized(Permissions perm){
			//No need to check RemotingRole; no call to db.
			return IsAuthorized(perm,DateTime.MinValue,false);
		}

		///<summary>Checks to see if current user is authorized.  It also checks any date restrictions.  If not authorized, it gives a Message box saying so and returns false.</summary>
		public static bool IsAuthorized(Permissions perm,DateTime date){
			//No need to check RemotingRole; no call to db.
			return IsAuthorized(perm,date,false);
		}

		///<summary>Checks to see if current user is authorized.  It also checks any date restrictions.  If not authorized, it gives a Message box saying so and returns false.</summary>
		public static bool IsAuthorized(Permissions perm,bool suppressMessage){
			//No need to check RemotingRole; no call to db.
			return IsAuthorized(perm,DateTime.MinValue,suppressMessage);
		}

		///<summary>Checks to see if current user is authorized.  It also checks any date restrictions.  If not authorized, it gives a Message box saying so and returns false.</summary>
		public static bool IsAuthorized(Permissions perm,DateTime date,bool suppressMessage){
			//No need to check RemotingRole; no call to db.
			if(Security.CurUser==null) {
				if(!suppressMessage) {
					MessageBox.Show(Lans.g("Security","Not authorized for")+"\r\n"+GroupPermissions.GetDesc(perm));
				}
				return false;
			}
			try {
				return IsAuthorized(perm,date,suppressMessage,curUser.UserGroupNum);
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
				return false;
			}
		}

		///<summary>Will throw an error if not authorized and message not suppressed.</summary>
		public static bool IsAuthorized(Permissions perm,DateTime date,bool suppressMessage,long userGroupNum) {
			//No need to check RemotingRole; no call to db.
			if(!GroupPermissions.HasPermission(userGroupNum,perm)){
				if(!suppressMessage){
					throw new Exception(Lans.g("Security","Not authorized for")+"\r\n"+GroupPermissions.GetDesc(perm));
				}
				return false;
			}
			if(perm==Permissions.AccountingCreate || perm==Permissions.AccountingEdit){
				if(date <= PrefC.GetDate(PrefName.AccountingLockDate)){
					if(!suppressMessage) {
						throw new Exception(Lans.g("Security","Locked by Administrator."));
					}
					return false;	
				}
			}
			if(  perm==Permissions.AdjustmentCreate
				|| perm==Permissions.AdjustmentEdit
				|| perm==Permissions.PaymentCreate
				|| perm==Permissions.PaymentEdit
				|| perm==Permissions.ProcComplCreate
				|| perm==Permissions.ProcComplEdit
				|| perm==Permissions.InsPayCreate
				|| perm==Permissions.InsPayEdit
				|| perm==Permissions.SheetEdit
				)
			{
				if(date.Year>1//if a valid date was passed in
					&& date <= PrefC.GetDate(PrefName.SecurityLockDate))//and that date is earlier than the lock
				{
					if(PrefC.GetBool(PrefName.SecurityLockIncludesAdmin)//if admins are locked out too
						|| !GroupPermissions.HasPermission(userGroupNum,Permissions.SecurityAdmin))//or is not an admin
					{
						if(!suppressMessage) {
							throw new Exception(Lans.g("Security","Locked by Administrator before ")+PrefC.GetDate(PrefName.SecurityLockDate).ToShortDateString());
						}
						return false;	
					}
				}
				if(date.Year>1//if a valid date was passed in
					&& PrefC.GetInt(PrefName.SecurityLockDays) > 0
					&& date <= DateTime.Today.AddDays(-PrefC.GetInt(PrefName.SecurityLockDays)))//and that date is earlier than the lock
				{
					if(PrefC.GetBool(PrefName.SecurityLockIncludesAdmin)//if admins are locked out too
						|| !GroupPermissions.HasPermission(userGroupNum,Permissions.SecurityAdmin))//or is not an admin
					{
						if(!suppressMessage) {
							throw new Exception(Lans.g("Security","Locked by Administrator before ")+PrefC.GetInt(PrefName.SecurityLockDays).ToString()+" days.");
						}
						return false;
					}
				}
			}
			if(!GroupPermissions.PermTakesDates(perm)){
				return true;
			}
			DateTime dateLimit=GetDateLimit(perm,userGroupNum);
			if(date>dateLimit){//authorized
				return true;
			}
			//Handling of min dates.  There might be others, but we have to handle them individually to avoid introduction of bugs.
			if(perm==Permissions.ClaimSentEdit//no date sent was entered before setting claim received
				|| perm==Permissions.ProcComplEdit//a completed procedure with a min date.
				|| perm==Permissions.InsPayEdit//a claim payment with no date.
				|| perm==Permissions.TreatPlanEdit
				|| perm==Permissions.AdjustmentEdit)
			{
				if(date.Year<1880	&& dateLimit.Year<1880) {
					return true;
				}
			}
			if(!suppressMessage){
				throw new Exception(Lans.g("Security","Not authorized for")+"\r\n"
					+GroupPermissions.GetDesc(perm)+"\r\n"+Lans.g("Security","Date limitation"));
			}
			return false;		
		}

		private static DateTime GetDateLimit(Permissions permType,long userGroupNum){
			//No need to check RemotingRole; no call to db.
			DateTime nowDate=MiscData.GetNowDateTime().Date;
			DateTime retVal=DateTime.MinValue;
			for(int i=0;i<GroupPermissionC.List.Length;i++){
				if(GroupPermissionC.List[i].UserGroupNum!=userGroupNum || GroupPermissionC.List[i].PermType!=permType){
					continue;
				}
				//this should only happen once.  One match.
				if(GroupPermissionC.List[i].NewerDate.Year>1880){
					retVal=GroupPermissionC.List[i].NewerDate;
				}
				if(GroupPermissionC.List[i].NewerDays==0){//do not restrict by days
					//do not change retVal
				}
				else if(nowDate.AddDays(-GroupPermissionC.List[i].NewerDays)>retVal){
					retVal=nowDate.AddDays(-GroupPermissionC.List[i].NewerDays);
				}
			}
			return retVal;
		}

		///<summary>Gets a module that the user has permission to use.  Tries the suggestedI first.  If a -1 is supplied, it tries to find any authorized module.  If no authorization for any module, it returns a -1, causing no module to be selected.</summary>
		public static int GetModule(int suggestI){
			//No need to check RemotingRole; no call to db.
			if(suggestI!=-1 && IsAuthorized(PermofModule(suggestI),DateTime.MinValue,true)){
				return suggestI;
			}
			for(int i=0;i<7;i++){
				if(IsAuthorized(PermofModule(i),DateTime.MinValue,true)){
					return i;
				}
			}
			return -1;
		}

		private static Permissions PermofModule(int i){
			//No need to check RemotingRole; no call to db.
			switch(i){
				case 0:
					return Permissions.AppointmentsModule;
				case 1:
					return Permissions.FamilyModule;
				case 2:
					return Permissions.AccountModule;
				case 3:
					return Permissions.TPModule;
				case 4:
					return Permissions.ChartModule;
				case 5:
					return Permissions.ImagesModule;
				case 6:
					return Permissions.ManageModule;
			}
			return Permissions.None;
		}

		//<summary>Obsolete</summary>
		//public static void ResetPassword(){
			//FIXME:UPDATE-MULTIPLE-TABLES
			/*string command="UPDATE userod,grouppermissions SET userod.Password='' "
				+"WHERE grouppermissions.UserGroupNum=userod.UserGroupNum "
				+"AND grouppermissions.PermType=24";
 			Db.NonQ(command);
			 */
			//Code updated to be compatible with Oracle as well as MySQL.
			/*
			string command="SELECT userod.UserNum FROM userod,grouppermissions "
				+"WHERE grouppermissions.UserGroupNum=userod.UserGroupNum "
				+"AND grouppermissions.PermType=24";
			DataTable table=Db.GetTable(command); 
			if(table.Rows.Count==0){
				throw new ApplicationException("No admin exists.");
			}
			command="UPDATE userod SET Password='' WHERE UserNum="+POut.PString(table.Rows[0][0].ToString());
			Db.NonQ(command);
		}*/

		///<summary>RemotingRole has not yet been set to ClientWeb, but it will if this succeeds.  Will throw an exception if server cannot validate username and password.  configPath will be empty from a workstation and filled from the server.</summary>
		public static Userod LogInWeb(string oduser,string odpasshash,string configPath,string clientVersionStr) {
			//Very unusual method.  Remoting role can't be checked, but is implied by the presence of a value in configPath.
			if(configPath != "") {//RemotingRole.ServerWeb
				Userods.LoadDatabaseInfoFromFile(ODFileUtils.CombinePaths(configPath,"OpenDentalServerConfig.xml"));
				//ODFileUtils.CombinePaths(
				//  ,"OpenDentalServerConfig.xml"));
				//Path.GetDirectoryName(Application.ExecutablePath),"OpenDentalServerConfig.xml"));
				//Application.StartupPath,"OpenDentalServerConfig.xml"));
				//Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),"OpenDentalServerConfig.xml"));
				//Environment.CurrentDirectory,"OpenDentalServerConfig.xml"));
				//Then, check username and password
				Userod user=Userods.CheckUserAndPassword(oduser,odpasshash);
				#if DEBUG
					if(oduser=="Admin"){
						user=Userods.GetUserByName("Admin");//without checking password.  Makes debugging faster.
					}
				#endif
				if(user==null) {
					throw new Exception("Invalid username or password.");
				}
				string command="SELECT ValueString FROM preference WHERE PrefName='ProgramVersion'";
				string dbVersionStr=Db.GetScalar(command);
				string serverVersionStr=Assembly.GetAssembly(typeof(Db)).GetName().Version.ToString(4);
				#if DEBUG
					command="SELECT ValueString FROM preference WHERE PrefName='DataBaseVersion'";//Using this during debug in the head makes it open fast with less fiddling.
					dbVersionStr=Db.GetScalar(command);
				#endif
				if(dbVersionStr!=serverVersionStr) {
					throw new Exception("Version mismatch.  Server:"+serverVersionStr+"  Database:"+dbVersionStr);
				}
				Version clientVersion=new Version(clientVersionStr);
				Version serverVersion=new Version(serverVersionStr);
				if(clientVersion > serverVersion){
					throw new Exception("Version mismatch.  Client:"+clientVersionStr+"  Server:"+serverVersionStr);
				}
				//if clientVersion == serverVersion, than we need do nothing.
				//if clientVersion < serverVersion, than an update will later be triggered.
				//Security.CurUser=user;//we're on the server, so this is meaningless
				return user;
				//return 0;//meaningless
			}
			else {
				//Because RemotingRole has not been set, and because CurUser has not been set,
				//this particular method is more verbose than most and does not use Meth.
				//It's not a good example of the standard way of doing things.
				DtoGetObject dto=new DtoGetObject();
				dto.Credentials=new Credentials();
				dto.Credentials.Username=oduser;
				dto.Credentials.PassHash=odpasshash;//Userods.EncryptPassword(password);
				dto.MethodName="Security.LogInWeb";
				dto.ObjectType=typeof(Userod).FullName;
				object[] parameters=new object[] { oduser,odpasshash,configPath,clientVersionStr };
				Type[] objTypes=new Type[] { typeof(string),typeof(string),typeof(string),typeof(string) };
				dto.Params=DtoObject.ConstructArray(parameters,objTypes);
				return RemotingClient.ProcessGetObject<Userod>(dto);//can throw exception
			}
		}

		
	}
}

























