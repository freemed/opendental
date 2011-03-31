using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Allergies{
		///<summary></summary>
		public static List<Allergy> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Allergy>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM allergy WHERE PatNum = "+POut.Long(patNum);
			return Crud.AllergyCrud.SelectMany(command);
		}

		///<summary>Gets one Allergy from the db.</summary>
		public static Allergy GetOne(long allergyNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<Allergy>(MethodBase.GetCurrentMethod(),allergyNum);
			}
			return Crud.AllergyCrud.SelectOne(allergyNum);
		}

		///<summary></summary>
		public static long Insert(Allergy allergy){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				allergy.AllergyNum=Meth.GetLong(MethodBase.GetCurrentMethod(),allergy);
				return allergy.AllergyNum;
			}
			return Crud.AllergyCrud.Insert(allergy);
		}

		///<summary></summary>
		public static void Update(Allergy allergy){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),allergy);
				return;
			}
			Crud.AllergyCrud.Update(allergy);
		}

		///<summary></summary>
		public static void Delete(long allergyNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),allergyNum);
				return;
			}
			string command= "DELETE FROM allergy WHERE AllergyNum = "+POut.Long(allergyNum);
			Db.NonQ(command);
		}

		///<summary>Gets all allergies for patient whether active or not.</summary>
		public static List<Allergy> GetAll(long patNum,bool showInactive) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Allergy>>(MethodBase.GetCurrentMethod(),patNum,showInactive);
			}
			string command="SELECT * FROM allergy WHERE PatNum = "+POut.Long(patNum)
				+" AND StatusIsActive<>0";
			if(showInactive) {
				command="SELECT * FROM allergy WHERE PatNum = "+POut.Long(patNum);
			}
			return Crud.AllergyCrud.SelectMany(command);
		}

		public static List<long> GetChangedSinceAllergyNums(DateTime changedSince) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<long>>(MethodBase.GetCurrentMethod(),changedSince);
			}
			string command="SELECT AllergyNum FROM allergy WHERE DateTStamp > "+POut.DateT(changedSince);
			DataTable dt=Db.GetTable(command);
			List<long> allergynums = new List<long>(dt.Rows.Count);
			for(int i=0;i<dt.Rows.Count;i++) {
				allergynums.Add(PIn.Long(dt.Rows[i]["AllergyNum"].ToString()));
			}
			return allergynums;
		}

		///<summary>Used along with GetChangedSinceAllergyNums</summary>
		public static List<Allergy> GetMultAllergies(List<long> allergyNums) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Allergy>>(MethodBase.GetCurrentMethod(),allergyNums);
			}
			string strAllergyNums="";
			DataTable table;
			if(allergyNums.Count>0) {
				for(int i=0;i<allergyNums.Count;i++) {
					if(i>0) {
						strAllergyNums+="OR ";
					}
					strAllergyNums+="AllergyNum='"+allergyNums[i].ToString()+"' ";
				}
				string command="SELECT * FROM allergy WHERE "+strAllergyNums;
				table=Db.GetTable(command);
			}
			else {
				table=new DataTable();
			}
			Allergy[] multAllergies=Crud.AllergyCrud.TableToList(table).ToArray();
			List<Allergy> allergyList=new List<Allergy>(multAllergies);
			return allergyList;
		}
		

	}
}