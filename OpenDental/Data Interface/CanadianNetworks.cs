using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class CanadianNetworks{
		private static List<CanadianNetwork> listt;

		public static List<CanadianNetwork> Listt{
			get{
				if(listt==null){
					Refresh();
				}
				return listt;
			}
		}
	
		///<summary></summary>
		public static void Refresh(){
			string command="SELECT * FROM canadiannetwork";
			DataTable table=General.GetTable(command);
			listt=new List<CanadianNetwork>();
			CanadianNetwork network;
			for(int i=0;i<table.Rows.Count;i++){
				network=new CanadianNetwork();
				network.CanadianNetworkNum=PIn.PInt   (table.Rows[i][0].ToString());
				network.Abbrev            =PIn.PString(table.Rows[i][1].ToString());
				network.Descript          =PIn.PString(table.Rows[i][2].ToString());
				listt.Add(network);
			}
		}

		///<summary></summary>
		public static void Insert(CanadianNetwork network) {
			if(PrefB.RandomKeys) {
				network.CanadianNetworkNum=MiscData.GetKey("canadiannetwork","CanadianNetworkNum");
			}
			string command="INSERT INTO canadiannetwork (";
			if(PrefB.RandomKeys) {
				command+="CanadianNetworkNum,";
			}
			command+="Abbrev, Descript) VALUES(";
			if(PrefB.RandomKeys) {
				command+="'"+POut.PInt(network.CanadianNetworkNum)+"', ";
			}
			command+=
				 "'"+POut.PString(network.Abbrev)+"', "
				+"'"+POut.PString(network.Descript)+"')";
			if(PrefB.RandomKeys) {
				General.NonQ(command);
			}
			else {
				network.CanadianNetworkNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void Update(CanadianNetwork Cur){
			string command="UPDATE canadiannetwork SET "
				+ "Abbrev = '"+POut.PString(Cur.Abbrev)+"' "
				+ ",Descript='"+POut.PString(Cur.Descript)+"' "
				+"WHERE CanadianNetworkNum = '"+POut.PInt(Cur.CanadianNetworkNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(int networkNum){

		}

		///<summary></summary>
		public static CanadianNetwork GetNetwork(int networkNum){
			if(listt==null) {
				Refresh();
			}
			for(int i=0;i<listt.Count;i++){
				if(listt[i].CanadianNetworkNum==networkNum){
					return listt[i];
				}
			}
			return null;
		}


	


		 
		 
	}
}













