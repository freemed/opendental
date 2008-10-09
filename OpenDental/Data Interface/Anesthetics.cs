using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental
{
	///<summary></summary>
	public class Anesthetics
	{

			///<summary>Gets one commlog item from database.</summary>
			/*public static AnestheticData GetOne(int AnestheticDataNum)
			{
				string command =
					"SELECT * FROM anestheticdata"
					+ " WHERE AnestheticDataNum = " + POut.PInt(AnestheticDataNum);
				AnestheticData[] anestheticDataList = RefreshAndFill(command);
				if (anestheticDataList.Length == 0)
				{
					return null;
				}
				return anestheticDataList[0];
			}

			private static AnestheticData[] RefreshAndFill(string command)
			{

				AnestheticData[] List = new AnestheticData();
				for (int i = 0; i < List.Length; i++)
				{
					List[i] = new AnestheticData();
					List[i].AnestheticDataNum = PIn.PInt(anesthOpen.ToString());
					
				}
				return List;
			}*/

			///<summary></summary>
			/*public static void Insert(AnestheticData anestheticDataNum)
			{
				if (PrefC.RandomKeys)
				{
					anestheticDataNum.AnestheticDataNum = MiscData.GetKey("anestheticData", "AnestheticDataNum");
				}
				string command = "INSERT INTO anestheticData (";
				if (PrefC.RandomKeys)
				{
					command += "AnestheticDataNum,";
				}
				command += "AnesthOpen) VALUES(";
				if (PrefC.RandomKeys)
				{
					command += "'" + POut.PInt(AnestheticData.anestheticDataNum) + "', ";
				}
				command +=
					 "'" + POut.PInt(AnestheticData.AnestheticDataNum) + "', "
					+ POut.PDateT(AnestheticData.AnesthOpen) + ", ";
					/*+ "'" + POut.PInt(comm.CommType) + "', "
					+ "'" + POut.PString(comm.Note) + "', "
					+ "'" + POut.PInt((int)comm.Mode_) + "', "
					+ "'" + POut.PInt((int)comm.SentOrReceived) + "', "
					//+"'"+POut.PBool  (comm.IsStatementSent)+"', "
					+ "'" + POut.PInt(comm.UserNum) + "')";
				if (PrefC.RandomKeys)
				{
					General.NonQ(command);
				}
				else
				{
					AnestheticData.AnestheticDataNum = General.NonQ(command, true);
				}
			}*/

			///<summary></summary>
			/*public static void Update(Commlog comm)
			{
				string command = "UPDATE commlog SET "
					+ "PatNum = '" + POut.PInt(comm.PatNum) + "', "
					+ "CommDateTime= " + POut.PDateT(comm.CommDateTime) + ", "
					+ "CommType = '" + POut.PInt(comm.CommType) + "', "
					+ "Note = '" + POut.PString(comm.Note) + "', "
					+ "Mode_ = '" + POut.PInt((int)comm.Mode_) + "', "
					+ "SentOrReceived = '" + POut.PInt((int)comm.SentOrReceived) + "', "
					//+"IsStatementSent = '"+POut.PBool  (comm.IsStatementSent)+"', "
					+ "UserNum = '" + POut.PInt(comm.UserNum) + "' "
					+ "WHERE commlognum = '" + POut.PInt(comm.CommlogNum) + "'";
				General.NonQ(command);
			}

			///<summary></summary>
			public static void Delete(Commlog comm)
			{
				string command = "DELETE FROM commlog WHERE CommLogNum = '" + POut.PInt(comm.CommlogNum) + "'";
				General.NonQ(command);
			}*/
	}

}



















