using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental.Bridges{
	/// <summary></summary>
	public class Sirona{
		private static string iniFile;

		/// <summary></summary>
		public Sirona(){
			
		}

		[DllImport("kernel32")]//this is the windows function for writing to ini files.
    private static extern long WritePrivateProfileString(string section,string key,string val
			,string filePath);

		[DllImport("kernel32")]//this is the windows function for reading from ini files.
    private static extern int GetPrivateProfileString(string section,string key,string def
			,StringBuilder retVal,int size,string filePath);

		private static string ReadValue(string section,string key){
			StringBuilder strBuild=new StringBuilder(255);
			int i=GetPrivateProfileString(section,key,"",strBuild,255,iniFile);
				return strBuild.ToString();
			}

		///<summary>Sends data for Patient to a mailbox file and launches the program.</summary>
		public static void SendData(Program ProgramCur, Patient pat){
			ArrayList ForProgram=ProgramProperties.GetForProgram(ProgramCur.ProgramNum);
			if(pat!=null){
				//read file C:\sidexis\sifiledb.ini
				iniFile=Path.GetDirectoryName(ProgramCur.Path)+"\\sifiledb.ini";
				if(!File.Exists(iniFile)){
					MessageBox.Show(iniFile+" could not be found. Is Sidexis installed properly?");
					return;
				}
				//read FromStation0 | File to determine location of comm file (sendBox) (siomin.sdx)
				//example:
				//[FromStation0]
				//File=F:\PDATA\siomin.sdx  //only one sendBox on entire network.
				string sendBox=ReadValue("FromStation0","File");
				//read Multistations | GetRequest (=1) to determine if station can take xrays.
				//but we don't care at this point, so ignore
				//set OfficeManagement | OffManConnected = 1 to make sidexis ready to accept a message.
				WritePrivateProfileString("OfficeManagement","OffManConnected","1",iniFile);
				//line formats: first two bytes are the length of line including first two bytes and \r\n
				//each field is terminated by null (byte 0).
				//Append U token to siomin.sdx file
				StringBuilder line=new StringBuilder();
				FileStream fs=new FileStream(sendBox,FileMode.OpenOrCreate);
				using(BinaryWriter bw=new BinaryWriter(fs)){
					char nTerm=(char)0;//Convert.ToChar(0);
					line.Append("U");//U signifies Update patient in sidexis. Gets ignored if new patient.
					line.Append(nTerm);
					line.Append(pat.LName);
					line.Append(nTerm);
					line.Append(pat.FName);
					line.Append(nTerm);
					line.Append(pat.Birthdate.ToString("dd.MM.yyyy"));
					line.Append(nTerm);
					//leave initial patient id blank. This updates sidexis to patNums used in Open Dental
					line.Append(nTerm);
					line.Append(pat.LName);
					line.Append(nTerm);
					line.Append(pat.FName);
					line.Append(nTerm);
					line.Append(pat.Birthdate.ToString("dd.MM.yyyy"));
					line.Append(nTerm);
					//Patient id:
					ProgramProperty PPCur=ProgramProperties.GetCur(ForProgram, "Enter 0 to use PatientNum, or 1 to use ChartNum");;
					if(PPCur.PropertyValue=="0"){
						line.Append(pat.PatNum.ToString());
					}
					else{
						line.Append(pat.ChartNumber);
					}
					line.Append(nTerm);
					if(pat.Gender==PatientGender.Female)
						line.Append("F");
					else
						line.Append("M");
					line.Append(nTerm);
					line.Append(Providers.GetAbbr(Patients.GetProvNum(pat)));
					line.Append(nTerm);
					line.Append("OpenDental");
					line.Append(nTerm);
					line.Append("Sidexis");
					line.Append(nTerm);
					line.Append("\r\n");
					bw.Write(IntToByteArray(line.Length+2));//the 2 accounts for these two chars.
					bw.Write(StrBuildToBytes(line));
					//Append N token to siomin.sdx file
					//N signifies create New patient in sidexis. If patient already exists,
					//then it simply updates any old data.
					line=new StringBuilder();
					line.Append("N");
					line.Append(nTerm);
					line.Append(pat.LName);
					line.Append(nTerm);
					line.Append(pat.FName);
					line.Append(nTerm);
					line.Append(pat.Birthdate.ToString("dd.MM.yyyy"));
					line.Append(nTerm);
					//Patient id:
					if(PPCur.PropertyValue=="0"){
						line.Append(pat.PatNum.ToString());
					}
					else{
						line.Append(pat.ChartNumber);
					}
					line.Append(nTerm);
					if(pat.Gender==PatientGender.Female)
						line.Append("F");
					else
						line.Append("M");
					line.Append(nTerm);
					line.Append(Providers.GetAbbr(Patients.GetProvNum(pat)));
					line.Append(nTerm);
					line.Append("OpenDental");
					line.Append(nTerm);
					line.Append("Sidexis");
					line.Append(nTerm);
					line.Append("\r\n");
					bw.Write(IntToByteArray(line.Length+2));
					bw.Write(StrBuildToBytes(line));
					//Append A token to siomin.sdx file
					//A signifies Autoselect patient. 
					line=new StringBuilder();
					line.Append("A");
					line.Append(nTerm);
					line.Append(pat.LName);
					line.Append(nTerm);
					line.Append(pat.FName);
					line.Append(nTerm);
					line.Append(pat.Birthdate.ToString("dd.MM.yyyy"));
					line.Append(nTerm);
					if(PPCur.PropertyValue=="0"){
						line.Append(pat.PatNum.ToString());
					}
					else{
						line.Append(pat.ChartNumber);
					}
					line.Append(nTerm);
					line.Append(SystemInformation.ComputerName);
					line.Append(nTerm);
					line.Append(DateTime.Now.ToString("dd.MM.yyyy"));
					line.Append(nTerm);
					line.Append(DateTime.Now.ToString("HH.mm.ss"));
					line.Append(nTerm);
					line.Append("OpenDental");
					line.Append(nTerm);
					line.Append("Sidexis");
					line.Append(nTerm);
					line.Append("0");//0=no image selection
					line.Append(nTerm);
					line.Append("\r\n");
					bw.Write(IntToByteArray(line.Length+2));
					bw.Write(StrBuildToBytes(line));
				}
				fs.Close();
			}//if patient is loaded
			//Start Sidexis.exe whether patient loaded or not.
			try{
				Process.Start(ProgramCur.Path);
			}
			catch{
				MessageBox.Show(ProgramCur.Path+" is not available.");
			}
		}

		///<summary>this is my way of converting to hex. I don't like their file format at all. Always returns an array of 2 chars.  Used when measuring length of line.</summary>
		private static byte[] IntToByteArray(int toConvert){
			byte[] retVal=new byte[2];
			retVal[0]=(byte)Math.IEEERemainder(toConvert,256);
			retVal[1]=(byte)(toConvert/256);//rounds down automatically
			return retVal;
		}

		private static byte[] StrBuildToBytes(StringBuilder strBuild){
			byte[] retVal=new byte[strBuild.Length];
			for(int i=0;i<retVal.Length;i++){
				switch(strBuild[i]){
					default:
						retVal[i]=(byte)strBuild[i];
						break;
					case '\u00C7'://C,
						retVal[i]=128;
						break;
					case '\u00FC'://u"
						retVal[i]=129;
						break;
					case '\u00E9'://e'
						retVal[i]=130;
						break;
					case '\u00E2'://a^
						retVal[i]=131;
						break;
					case '\u00E4'://a"
						retVal[i]=132;
						break;
					case '\u00E0'://a`
						retVal[i]=133;
						break;
					case '\u00E5'://ao
						retVal[i]=134;
						break;
					case '\u00E7'://c,
						retVal[i]=135;
						break;
					case '\u00EA'://e^
						retVal[i]=136;
						break;
					case '\u00EB'://e"
						retVal[i]=137;
						break;
					case '\u00E8'://e`
						retVal[i]=138;
						break;
					case '\u00EF'://i"
						retVal[i]=139;
						break;
					case '\u00EE'://i^
						retVal[i]=140;
						break;
					case '\u00EC'://i`
						retVal[i]=141;
						break;
					case '\u00C4'://A"
						retVal[i]=142;
						break;
					case '\u00C5'://Ao
						retVal[i]=143;
						break;
					case '\u00C9'://E'
						retVal[i]=144;
						break;
					case '\u00E6'://ae
						retVal[i]=145;
						break;
					case '\u00C6'://AE
						retVal[i]=146;
						break;
					case '\u00F4'://o^
						retVal[i]=147;
						break;
					case '\u00F6'://o"
						retVal[i]=148;
						break;
					case '\u00F2'://o`
						retVal[i]=149;
						break;
					case '\u00FB'://u^
						retVal[i]=150;
						break;
					case '\u00F9'://u`
						retVal[i]=151;
						break;
					case '\u00FF'://y"
						retVal[i]=152;
						break;
					case '\u00D6'://O"
						retVal[i]=153;
						break;
					case '\u00DC'://U"
						retVal[i]=154;
						break;
					//skipped 155 through 159
					case '\u00E1'://a'
						retVal[i]=160;
						break;
					case '\u00ED'://i'
						retVal[i]=161;
						break;
					case '\u00F3'://o'
						retVal[i]=162;
						break;
					case '\u00FA'://u'
						retVal[i]=163;
						break;
					case '\u00F1'://n~
						retVal[i]=164;
						break;
					case '\u00D1'://N~
						retVal[i]=165;
						break;
				}
			}
			return retVal;
		}

	}
}










