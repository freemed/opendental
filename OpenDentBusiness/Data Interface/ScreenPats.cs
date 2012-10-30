using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class ScreenPats{
		///<summary></summary>
		public static long Insert(ScreenPat screenPat){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				screenPat.ScreenPatNum=Meth.GetLong(MethodBase.GetCurrentMethod(),screenPat);
				return screenPat.ScreenPatNum;
			}
			return Crud.ScreenPatCrud.Insert(screenPat);
		}

		/// <summary></summary>
		public static List<ScreenPat> GetForScreenGroup(long screenGroupNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<List<ScreenPat>>(MethodBase.GetCurrentMethod(),screenGroupNum);
			}
			string command="SELECT * FROM screenpat WHERE ScreenPatNum IN (SELECT ScreenPatNum FROM screengroup WHERE ScreenGroupNum="+POut.Long(screenGroupNum)+")";
			return Crud.ScreenPatCrud.SelectMany(command);
		}

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<ScreenPat> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ScreenPat>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM screenpat WHERE PatNum = "+POut.Long(patNum);
			return Crud.ScreenPatCrud.SelectMany(command);
		}

		///<summary>Gets one ScreenPat from the db.</summary>
		public static ScreenPat GetOne(long screenPatNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<ScreenPat>(MethodBase.GetCurrentMethod(),screenPatNum);
			}
			return Crud.ScreenPatCrud.SelectOne(screenPatNum);
		}


		///<summary></summary>
		public static void Update(ScreenPat screenPat){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),screenPat);
				return;
			}
			Crud.ScreenPatCrud.Update(screenPat);
		}

		///<summary></summary>
		public static void Delete(long screenPatNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),screenPatNum);
				return;
			}
			string command= "DELETE FROM screenpat WHERE ScreenPatNum = "+POut.Long(screenPatNum);
			Db.NonQ(command);
		}
		*/




	}
}