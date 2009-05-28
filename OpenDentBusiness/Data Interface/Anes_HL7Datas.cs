using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental;
using OpenDentBusiness.DataAccess;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;


namespace OpenDentBusiness {
	///<summary></summary>

	public class Anes_HL7Datas {

		
		///<summary>This is the connection that is used by the data adapter for all queries.</summary>
		private static MySqlConnection con;

		public static List<Anes_hl7data> CreateObjects(int anestheticRecordNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Anes_hl7data>>(MethodBase.GetCurrentMethod(),anestheticRecordNum);
			}
			string cmd="SELECT * FROM anes_hl7data ORDER BY DateLoaded ASC";
			return new List<Anes_hl7data>(DataObjectFactory<Anes_hl7data>.CreateObjects(cmd));
		}

		///<summary>Deletes anesthetic meds from the table anesthmedsgiven, and updates inventory accordingly </summary>
		public static void DeleteHL7Msg(string messageID) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),messageID);
				return;
			}
			string command = "DELETE FROM anes_hl7data WHERE MessageID='" + messageID + "'";
			Db.NonQ(command);
		}

		public static void WriteObject(Anes_hl7data hl7) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),hl7);
				return;
			}
			DataObjectFactory<Anes_hl7data>.WriteObject(hl7);
		}

		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string c="SELECT HL7Message FROM anes_hl7data";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),c);
			table.TableName="anes_hl7data";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			Anes_HL7DataC.Listt=new List<Anes_hl7data>();
			Anes_hl7data hl7Cur;
			for(int i=0;i<table.Rows.Count;i++) {
				hl7Cur=new Anes_hl7data();
				hl7Cur.IsNew = false;
				hl7Cur.MessageID =	PIn.PString(table.Rows[i][0].ToString());
				hl7Cur.VendorName	=	PIn.PString(table.Rows[i][1].ToString());
				hl7Cur.VendorVersion =	PIn.PString(table.Rows[i][2].ToString());
				hl7Cur.MsgControl =	PIn.PString(table.Rows[i][3].ToString());
				hl7Cur.PartnerAPP =	PIn.PString(table.Rows[i][4].ToString());
				hl7Cur.DateLoaded =	PIn.PDateT(table.Rows[i][5].ToString());
				hl7Cur.LastLoaded =	PIn.PDateT(table.Rows[i][6].ToString());
				hl7Cur.LoadCount =	PIn.PInt(table.Rows[i][7].ToString());
				hl7Cur.MsgType =	PIn.PString(table.Rows[i][8].ToString());
				hl7Cur.MsgEvent =	PIn.PString(table.Rows[i][9].ToString());
				hl7Cur.Outbound =	PIn.PInt(table.Rows[i][10].ToString());
				hl7Cur.Inbound =	PIn.PInt(table.Rows[i][11].ToString());
				hl7Cur.Processed =	PIn.PInt(table.Rows[i][12].ToString());
				hl7Cur.Warnings =	PIn.PInt(table.Rows[i][13].ToString());
				hl7Cur.Loaded =	PIn.PInt(table.Rows[i][14].ToString());
				hl7Cur.SchemaLoaded =	PIn.PInt(table.Rows[i][15].ToString());
				hl7Cur.StatusMessage =	PIn.PString(table.Rows[i][16].ToString());
				hl7Cur.ArchiveID =	PIn.PString(table.Rows[i][17].ToString());
				hl7Cur.HL7Format =	PIn.PInt(table.Rows[i][18].ToString());
				hl7Cur.SegmentCount =	PIn.PInt(table.Rows[i][19].ToString());
				hl7Cur.MessageSize =	PIn.PInt(table.Rows[i][20].ToString());
				hl7Cur.HL7Message =	PIn.PString(table.Rows[i][21].ToString());
				Anes_HL7DataC.Listt.Add(hl7Cur);
			}
		}

		public static string GetHL7List() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod());
			}
			MySqlCommand cmd = new MySqlCommand();
			con = new MySqlConnection(DataSettings.ConnectionString);
			cmd.Connection = con;
			if(con.State == ConnectionState.Open) {
				con.Close();
			}
			con.Open();
			cmd.CommandText = "SELECT * FROM anes_hl7data";
			string rawHL7 = Convert.ToString(cmd.ExecuteScalar());
			con.Close();
			return rawHL7;
		}

		//This method currently works for a Philips VM4 speaking HL7 v. 2.4. Other monitors may work depending on how they output their HL7 messages
		public static void ParseHL7Messages(int anestheticRecordNum,int patNum,string rawHL7,string messageID,string anesthDateTime,string anesthOpenTime,string anesthCloseTime) {
			//No need to check RemotingRole; no call to db.
			string[] hL7OBX; //an array of the raw HL7 message
			string[] hL7MSH; //an array of the MSH segment
			string[] hL7OBXSeg; //OBX segments
			string VSMName = "";
			string VSMSerNum = "";
			int patID = 0;
			int SpO2 = 0;
			int NBPs = 0;
			int NBPd = 0;
			int NBPm = 0;
			int HR = 0;
			int temp = 0;
			int EtCO2 = 0;
			string MessageID = messageID;
			string HL7Message=rawHL7;
			//split the rawHL7 message into OBX Segments
			hL7OBX = Regex.Split(rawHL7,@"OBX");
			string VSTimeStamp = "";
			for(int count=0;count < hL7OBX.Length;count++) {
				if(Regex.Match(rawHL7,@"Alarm").Success == true) {
					Anes_HL7Datas.DeleteHL7Msg(Convert.ToString(MessageID));//deletes message from anes_hl7data so we don't keep reimporting it
				}
				if(Regex.Match(rawHL7,@"Alarm").Success != true) //discard messages with alarm warnings
				{
					string subSegment = hL7OBX[count].ToString();
					if(count < hL7OBX.Length) {
						//parses VSTimeStamp from message header (MSH)
						if(count == 0) {
							hL7MSH = Regex.Split(subSegment,@"\|");
							for(int m=0;m < hL7MSH.Length;m++) {
								if(m == 2) //gets VSM name
											{
									VSMName = hL7MSH[m].ToString();
								}

								if(m==23) //gets pat ID num
											{
									patID = Convert.ToInt32(hL7MSH[m].ToString());
								}

								if(m ==42) //gets VSM serial number
											{
									VSMSerNum = hL7MSH[m].ToString();
								}
								if(m == 6) {
									VSTimeStamp = hL7MSH[m].ToString();
								}
							}
						}
						AnesthMeds.UpdateVSMData(anestheticRecordNum,VSMName,VSMSerNum);
						//parse the OBX segments for vital sign data
						for(int j=0;j < hL7OBX.Length;j++) {
							int SpO2Matched = 0;//Certain VS monitors append an extra VS parameter after the last OBX segment
							int NBPsMatched = 0;//which breaks the Regex(es). These variables are set to "1" once a match 
							int NBPdMatched = 0;//is made prevent data from one parameter from being written to another
							int NBPmMatched = 0;
							int HRMatched = 0;
							string subSeg = hL7OBX[j].ToString();
							hL7OBXSeg = Regex.Split(subSeg,@"\|");
							if(Regex.Match(hL7OBX[j],@"SpO2").Success == true) {
								for(int k=0;k < hL7OBXSeg.Length;k++) {
									if(k == 5)//5th part of the OBX segment contains the vital sign data
															{
										try {
											if(SpO2Matched == 0) {
												SpO2 = Convert.ToInt32(hL7OBXSeg[k].ToString());
												SpO2Matched = SpO2Matched + 1;
											}
										}
										catch {
											SpO2 = 0;//otherwise it's null
										}
									}
								}
							}
							if(Regex.Match(hL7OBX[j],@"NBPs").Success == true) {
								for(int k=0;k < hL7OBXSeg.Length;k++) {
									if(k == 5) {
										try {
											if(NBPsMatched == 0) {
												NBPs = Convert.ToInt32(hL7OBXSeg[k].ToString());
												NBPsMatched = NBPsMatched + 1;
											}
										}
										catch {
											NBPs = 0;
										}
									}
								}
							}

							if(Regex.Match(hL7OBX[j],@"NBPd").Success == true) {
								for(int k=0;k < hL7OBXSeg.Length;k++) {
									if(k == 5) {
										try {
											if(NBPdMatched == 0) {
												NBPd = Convert.ToInt32(hL7OBXSeg[k].ToString());
												NBPdMatched = NBPdMatched + 1;
											}
										}
										catch {
											NBPd = 0;
										}
									}
								}
							}
							if(Regex.Match(hL7OBX[j],@"NBPm").Success == true) {
								for(int k=0;k < hL7OBXSeg.Length;k++) {
									if(k == 5) {
										try {
											if(NBPmMatched == 0) {
												NBPm = Convert.ToInt32(hL7OBXSeg[k].ToString());
												NBPmMatched = NBPmMatched + 1;
											}
										}
										catch {
											NBPm = 0;
										}
									}
								}
							}
							if(Regex.Match(hL7OBX[j],@"HR").Success == true) {
								for(int k=0;k < hL7OBXSeg.Length;k++) {
									if(k == 5) {
										try {
											if(HRMatched == 0) {
												HR = Convert.ToInt32(hL7OBXSeg[k].ToString());
												HRMatched = HRMatched + 1;
											}
										}
										catch {
											HR = 0;
										}
									}
								}
							}
						}
					}
				}
				else return;
			}
			int checkblankVS = NBPs + NBPd + HR + SpO2 + temp + EtCO2;
			if(checkblankVS != 0) //filters out empty segments; inserts valid ones to db
				{
				if(patID == patNum) {
					InsertVitalSigns(anestheticRecordNum,patNum,VSMName,VSMSerNum,NBPs,NBPd,NBPm,HR,SpO2,temp,EtCO2,VSTimeStamp,MessageID,HL7Message,anesthDateTime,anesthOpenTime,anesthCloseTime);
				}
			}

			if(NBPs == 0)//filters out partial messages from the import db 
				{
				Anes_HL7Datas.DeleteHL7Msg(Convert.ToString(MessageID));
			}
			return;

		}

		public static void InsertVitalSigns(int anestheticRecordNum,int patNum,string VSMName,string VSMSerNum,int NBPs,int NBPd,int NBPm,int HR,int SpO2,int temp,int EtCO2,string VSTimeStamp,string MessageID,string HL7Message,string anesthDateTime,string anesthOpenTime,string anesthCloseTime) {
			//No need to check RemotingRole; no call to db.
			List<AnestheticVSData> listAnesthVSData = AnesthVSDatas.CreateObjects(anestheticRecordNum);
			AnesthVSDatas.RefreshCache(anestheticRecordNum);
			//These conversions are needed so dates and times can be compared as numeric strings. All times are compared as military times. Consequently, the vital sign monitor(s) should be set up to export times as military times or this section will break.
			string AnesthDateTime = anesthDateTime.ToString();
			string AnesDate = AnesthDateTime.Remove(8,6);
			Double ADate = Convert.ToDouble(AnesDate);//needs to be a Double for String.Format to work
			string ADT = String.Format("{0:0000-00-00}",ADate);
			string ADTNumString = String.Format("{0:00000000}",ADate);
			DateTime AnesthClose_Time = Convert.ToDateTime(ADT+" " +anesthCloseTime); //Need to add ADT to give yyyyMMdd since anesthCloseTime is saved as a partial string
			DateTime AnesthOpen_Time = Convert.ToDateTime(ADT+" " +anesthOpenTime);
			string AnesthCloseTime = AnesthClose_Time.ToString(ADTNumString+"HHmmss");
			string AnesthOpenTime = AnesthOpen_Time.ToString(ADTNumString+"HHmmss");
			string vSTS = VSTimeStamp.ToString();

			long vsTS = Convert.ToInt64(vSTS);
			long anesthOT = Convert.ToInt64(AnesthOpenTime);
			long anesthCT = Convert.ToInt64(AnesthCloseTime);

			for(int o=0;o < listAnesthVSData.Count;o++)//looks to see if a vs has already been written to the db
				{
				if(listAnesthVSData[o].VSTimeStamp == VSTimeStamp) {
					Anes_HL7Datas.DeleteHL7Msg(Convert.ToString(MessageID));//deletes messages from anes_hl7data 
					return;
				}
			}
			if(anesthCT >= vsTS)
				if(vsTS >= anesthOT)//filters out messages that don't belong with this Anesthetic Record.
						{

					if(NBPs !=0)//data with a BP of zero is usually a partial reading so we discard
								{
						AnesthVSDatas.InsertVSData(anestheticRecordNum,patNum,VSMName,VSMSerNum,NBPs,NBPd,NBPm,HR,SpO2,temp,EtCO2,VSTimeStamp,MessageID,HL7Message);
					}
					Anes_HL7Datas.DeleteHL7Msg(Convert.ToString(MessageID));//deletes messages from anes_hl7data 
				}

			return;

		}

	}
}










