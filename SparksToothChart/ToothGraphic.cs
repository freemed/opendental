using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using OpenDentBusiness;

namespace SparksToothChart {
	public class ToothGraphic {
		private string toothID;
		///<summary>second dim is always 3.</summary>
		public float[][] Vertices;
		///<summary>second dim is always 3.</summary>
		public float[][] Normals;
		///<summary>Collection of type ToothGroup.</summary>
		public ArrayList Groups;
		private bool visible;
		private static float[] DefaultOrthoXpos;
		///<summary>The rotation about the Y axis in degrees. Performed before any other rotation or translation.  Positive numbers are clockwise as viewed from occlusal.</summary>
		public float Rotate;
		///<summary>Rotation to the buccal or lingual about the X axis in degrees.  Positive numbers are to buccal. Happens 2nd, after rotateY.  Not common. Usually 0.</summary>
		public float TipB;
		///<summary>Rotation about the Z axis in degrees.  Aka tipping.  Positive numbers are mesial.  Happens last after all other rotations.</summary>
		public float TipM;
		///<summary>Shifting mesially or distally in millimeters.  Mesial is always positive, distal is negative.  Happens after all three rotations.</summary>
		public float ShiftM;
		///<summary>Shifting vertically in millimeters.  Aka supereruption.  Happens after all three rotations.  A positive number is super eruption, whether on the maxillary or mandibular.</summary>
		public float ShiftO;
		///<summary>Shifting buccolingually in millimeters.  Rare.  Usually 0.</summary>
		public float ShiftB;
		///<summary>This gets set to true if this tooth has an RCT.  The purpose of this flag is to cause the root to be painted transparently on top of the RCT.</summary>
		public bool IsRCT;
		///<summary>Set true to hide the number of the tooth from displaying.  Visible should also be set to false in this case.</summary>
		private bool hideNumber;
		///<summary>The X indicated that an extraction is planned.</summary>
		public bool DrawBigX;
		///<summary>If a big X is to be drawn, this will contain the color.</summary>
		public Color colorX;
		///<summary>If RCT, then this will contain the color.</summary>
		public Color colorRCT;
		///<summary>True if this tooth is set to primary.  This can only be true for the 32 perm teeth.  A primary tooth would never have this value set.</summary>
		public bool ShowPrimary;
		///<summary>This gets set to true if tooth has a BU or a post.</summary>
		public bool IsBU;
		///<summary>If BU, then this will contain the color.</summary>
		public Color colorBU;
		///<summary>This gets set to true if tooth has an implant.</summary>
		public bool IsImplant;
		///<summary>If implant, then this will contain the color.</summary>
		public Color colorImplant;
		///<summary>This will be true if the tooth is a crown or retainer.  The reason it's needed is so that crowns will show on missing teeth with implants.</summary>
		public bool IsCrown;
		///<summary>True if the tooth is a bridge pontic, or a tooth on a denture or RPD.  This makes the crown visible from the F view, but not the occlusal.  It also makes the root invisible.</summary>
		public bool IsPontic;
		///<summary>This gets set to true if tooth has a sealant.</summary>
		public bool IsSealant;
		///<summary>If sealant, then this will contain the color.</summary>
		public Color colorSealant;

		///<summary>Only called from GraphicalToothChart.ResetTeeth or from GraphicalToothChartControl.ResetTeeth when program first loads.  Constructor requires passing in the toothID.  Exception will be thrown if not one of the following: 1-32 or A-T.  Always loads graphics data from local resources even if in simplemode.</summary>
		public ToothGraphic(string tooth_id) {
			if(tooth_id!="implant" && !IsValidToothID(tooth_id)) {
				///<summary>This will only happen if bugs in program</summary>
				throw new ApplicationException("Invalid tooth ID");
			}
			toothID=tooth_id;
			//if(!simplemode){
				ImportObj();
			//}
			SetDefaultColors();
		}

		#region properties

		///<summary>ToothID is set when creating a tooth object. It can never be changed.
		///Valid values are 1-32 or A-T.</summary>
		public string ToothID {
			get {
				return toothID;
			}
			set {
				//
			}
		}

		///<summary>Set false if teeth are missing.  All primary teeth initially set to false.</summary>
		public bool Visible {
			get {
				return visible;
			}
			set {
				visible=value;
			}
		}

		///<summary>Set true to hide the number of the tooth from displaying.  Visible should also be set to false in this case.</summary>
		public bool HideNumber {
			get {
				return hideNumber;
			}
			set {
				hideNumber=value;
			}
		}

		#endregion properties

		#region Public Methods
		///<summary>Resets this tooth graphic to original location, visiblity, and no restorations.  If primary tooth, then Visible=false.  </summary>
		public void Reset() {
			if(IsPrimary(ToothID)) {
				visible=false;
			}
			else {
				visible=true;
			}
			TipM=0;
			TipB=0;
			Rotate=0;
			ShiftB=0;
			ShiftM=0;
			ShiftO=0;
			SetDefaultColors();
			IsRCT=false;
			hideNumber=false;
			DrawBigX=false;
			ShowPrimary=false;
			IsBU=false;
			IsImplant=false;
			IsCrown=false;
			IsPontic=false;
			IsSealant=false;
		}

		public void SetSurfaceColors(string surfaces,Color color){
			for(int i=0;i<surfaces.Length;i++){
				if(surfaces[i]=='M'){
					SetGroupColor(ToothGroupType.M,color);
				}
				else if(surfaces[i]=='O') {
					SetGroupColor(ToothGroupType.O,color);
				}
				else if(surfaces[i]=='D') {
					SetGroupColor(ToothGroupType.D,color);
				}
				else if(surfaces[i]=='B') {
					SetGroupColor(ToothGroupType.B,color);
				}
				else if(surfaces[i]=='L') {
					SetGroupColor(ToothGroupType.L,color);
				}
				else if(surfaces[i]=='V') {
					SetGroupColor(ToothGroupType.V,color);
				}
				else if(surfaces[i]=='I') {
					SetGroupColor(ToothGroupType.I,color);
				}
				else if(surfaces[i]=='F') {
					SetGroupColor(ToothGroupType.F,color);
				}
			}
		}

		public void SetGroupColor(ToothGroupType groupType,Color paintColor){
			for(int i=0;i<Groups.Count;i++){
				if(((ToothGroup)Groups[i]).GroupType!=groupType){
					continue;
				}
				((ToothGroup)Groups[i]).PaintColor=paintColor;
			}
			//if type not found, then no action is taken
		}

		///<summary>Used in constructor and also in Reset.  Also sets visibility of all groups to true except RCT visible false.</summary>
		private void SetDefaultColors(){
			for(int i=0;i<Groups.Count;i++){
				if(((ToothGroup)Groups[i]).GroupType==ToothGroupType.Cementum){
					((ToothGroup)Groups[i]).PaintColor=Color.FromArgb(255,243,234,176);//230,214,143);
				}
				else{//enamel
					((ToothGroup)Groups[i]).PaintColor=Color.FromArgb(255,255,253,209);
				}
				if(((ToothGroup)Groups[i]).GroupType==ToothGroupType.Canals
					|| ((ToothGroup)Groups[i]).GroupType==ToothGroupType.Buildup)
				{
					((ToothGroup)Groups[i]).Visible=false;
				}
				else{
					((ToothGroup)Groups[i]).Visible=true;
				}
			}
		}

		public void SetGroupVisibility(ToothGroupType groupType,bool setVisible) {
			for(int i=0;i<Groups.Count;i++) {
				if(((ToothGroup)Groups[i]).GroupType!=groupType) {
					continue;
				}
				((ToothGroup)Groups[i]).Visible=setVisible;
			}
		}

		///<summary>This is only used once for the initial fill of display lists.</summary>
		public ToothGroup GetGroupForDisplayList(int index){
			ToothGroupType groupType=(ToothGroupType)index;//this can be enhanced later, but this is simple for now.
			for(int i=0;i<Groups.Count;i++) {
				if(((ToothGroup)Groups[i]).GroupType==groupType) {
					return (ToothGroup)Groups[i];
				}
			}
			return null;
		}

		///<summary>This is used when calling display lists.</summary>
		public int GetIndexForDisplayList(ToothGroup group) {
			int toothInt=Tooth.ToOrdinal(toothID);
			//this can be enhanced later, but it's very simple for now.
			return (toothInt*10)+(int)group.GroupType;
		}

		#endregion Public Methods

		#region static functions

		///<summary>Strict validator for toothID. Will not handle any international tooth nums since all internal toothID's are in US format.  True if 1-32 or A-T.  Since supernumerary not currently handled, 51-82 and AS-TS return false.  Any invalid values also return false.</summary>
		public static bool IsValidToothID(string tooth_id) {
			if(tooth_id==null || tooth_id=="")
				return false;
			if(Regex.IsMatch(tooth_id,"^[A-T]$"))
				return true;
			//if(Regex.IsMatch(tooth_id,"^[A-T]S$"))//supernumerary
			//	return true;
			if(!Regex.IsMatch(tooth_id,@"^[1-9]\d?$")) {//matches 1 or 2 digits, leading 0 not allowed
				return false;
			}
			int intToothId=Convert.ToInt32(tooth_id);
			if(intToothId<=32 && intToothId>=1)
				return true;
			//if(intToothId>=51 && intToothId<=82)//supernumerary
			//	return true;
			return false;
		}

		///<summary>The tooth_id should be validated before coming here, but it won't crash if invalid.  Primary or perm are ok.  Empty and null are also ok.  Result is always 1-32 or -1 if invalid tooth_id.  Supernumerary not allowed</summary>
		public static int IdToInt(string tooth_id) {
			if(!IsValidToothID(tooth_id)) {
				return -1;
			}
			try {
				if(IsPrimary(tooth_id)) {
					return Convert.ToInt32(PriToPerm(tooth_id));
				}
				else {
					return Convert.ToInt32(tooth_id);
				}
			}
			catch {
				return -1;
			}
		}

		///<summary></summary>
		public static string PermToPri(string tooth_id) {
			switch(tooth_id) {
				default: return "";
				case "4": return "A";
				case "5": return "B";
				case "6": return "C";
				case "7": return "D";
				case "8": return "E";
				case "9": return "F";
				case "10": return "G";
				case "11": return "H";
				case "12": return "I";
				case "13": return "J";
				case "20": return "K";
				case "21": return "L";
				case "22": return "M";
				case "23": return "N";
				case "24": return "O";
				case "25": return "P";
				case "26": return "Q";
				case "27": return "R";
				case "28": return "S";
				case "29": return "T";
			}
		}

		///<summary></summary>
		public static string PriToPerm(string tooth_id) {
			switch(tooth_id) {
				default: return "";
				case "A": return "4";
				case "B": return "5";
				case "C": return "6";
				case "D": return "7";
				case "E": return "8";
				case "F": return "9";
				case "G": return "10";
				case "H": return "11";
				case "I": return "12";
				case "J": return "13";
				case "K": return "20";
				case "L": return "21";
				case "M": return "22";
				case "N": return "23";
				case "O": return "24";
				case "P": return "25";
				case "Q": return "26";
				case "R": return "27";
				case "S": return "28";
				case "T": return "29";
			}
		}

		///<summary>Returns true if A-T or AS-TS.  Otherwise, returns false.</summary>
		public static bool IsPrimary(string tooth_id) {
			if(Regex.IsMatch(tooth_id,"^[A-T]$")) {
				return true;
			}
			if(Regex.IsMatch(tooth_id,"^[A-T]S$")) {
				return true;
			}
			return false;
		}

		///<summary>The actual specific width of the tooth in mm.  Used to lay out the teeth on the screen.  Value taken from Wheelers Dental Anatomy.  Used in the orthographic projection.  Also used when building the teeth in the first place.  The perspective projection will need to adjust these numbers because of the curvature of the arch.  Only the widths of permanent teeth are used for layout.  The primary teeth widths are informational only.</summary>
		public static float GetWidth(string tooth_id) {
			switch(tooth_id) {
				case "1":
				case "16":
					return 8.5f;
				case "2":
				case "15":
					return 9f;
				case "3":
				case "14":
					return 10f;
				case "4":
				case "5":
				case "13":
				case "12":
					return 7f;
				case "6":
				case "11":
					return 7.5f;
				case "7":
				case "10":
					return 6.5f;
				case "8":
				case "9":
					return 8.5f;
				case "17":
				case "32":
					return 10f;
				case "18":
				case "31":
					return 10.5f;
				case "19":
				case "30":
					return 11f;
				case "20":
				case "21":
				case "29":
				case "28":
					return 7f;
				case "22":
				case "27":
					return 7f;
				case "23":
				case "26":
					return 5f;//5.5f;
				case "24":
				case "25":
					return 5f;
				default:
					throw new ApplicationException(tooth_id);
				//return 0;
			}
		}

		///<summary></summary>
		public static float GetWidth(int tooth_num) {
			return GetWidth(tooth_num.ToString());
		}

		///<summary>The x position of the center of the given tooth, with midline being 0.  Calculated once, then used to quickly calculate mouse positions and tooth positions.  All values are in mm.</summary>
		public static float GetDefaultOrthoXpos(int tooth_num) {
			if(DefaultOrthoXpos==null) {
				DefaultOrthoXpos=new float[33];//0-32, 0 not used
				float spacing=0;//the distance between each adjacent tooth.
				DefaultOrthoXpos[8]=-spacing/2f-GetWidth(8)/2f;
				DefaultOrthoXpos[7]=DefaultOrthoXpos[8]-spacing-GetWidth(8)/2f-GetWidth(7)/2f;
				DefaultOrthoXpos[6]=DefaultOrthoXpos[7]-spacing-GetWidth(7)/2f-GetWidth(6)/2f;
				DefaultOrthoXpos[5]=DefaultOrthoXpos[6]-spacing-GetWidth(6)/2f-GetWidth(5)/2f;
				DefaultOrthoXpos[4]=DefaultOrthoXpos[5]-spacing-GetWidth(5)/2f-GetWidth(4)/2f;
				DefaultOrthoXpos[3]=DefaultOrthoXpos[4]-spacing-GetWidth(4)/2f-GetWidth(3)/2f;
				DefaultOrthoXpos[2]=DefaultOrthoXpos[3]-spacing-GetWidth(3)/2f-GetWidth(2)/2f;
				DefaultOrthoXpos[1]=DefaultOrthoXpos[2]-spacing-GetWidth(2)/2f-GetWidth(1)/2f;
				DefaultOrthoXpos[9]=-DefaultOrthoXpos[8];
				DefaultOrthoXpos[10]=-DefaultOrthoXpos[7];
				DefaultOrthoXpos[11]=-DefaultOrthoXpos[6];
				DefaultOrthoXpos[12]=-DefaultOrthoXpos[5];
				DefaultOrthoXpos[13]=-DefaultOrthoXpos[4];
				DefaultOrthoXpos[14]=-DefaultOrthoXpos[3];
				DefaultOrthoXpos[15]=-DefaultOrthoXpos[2];
				DefaultOrthoXpos[16]=-DefaultOrthoXpos[1];
				DefaultOrthoXpos[24]=spacing/2f+GetWidth(24)/2f;
				DefaultOrthoXpos[23]=DefaultOrthoXpos[24]+spacing+GetWidth(24)/2f+GetWidth(23)/2f;
				DefaultOrthoXpos[22]=DefaultOrthoXpos[23]+spacing+GetWidth(23)/2f+GetWidth(22)/2f;
				DefaultOrthoXpos[21]=DefaultOrthoXpos[22]+spacing+GetWidth(22)/2f+GetWidth(21)/2f;
				DefaultOrthoXpos[20]=DefaultOrthoXpos[21]+spacing+GetWidth(21)/2f+GetWidth(20)/2f;
				DefaultOrthoXpos[19]=DefaultOrthoXpos[20]+spacing+GetWidth(20)/2f+GetWidth(19)/2f;
				DefaultOrthoXpos[18]=DefaultOrthoXpos[19]+spacing+GetWidth(19)/2f+GetWidth(18)/2f;
				DefaultOrthoXpos[17]=DefaultOrthoXpos[18]+spacing+GetWidth(18)/2f+GetWidth(17)/2f;
				DefaultOrthoXpos[25]=-DefaultOrthoXpos[24];
				DefaultOrthoXpos[26]=-DefaultOrthoXpos[23];
				DefaultOrthoXpos[27]=-DefaultOrthoXpos[22];
				DefaultOrthoXpos[28]=-DefaultOrthoXpos[21];
				DefaultOrthoXpos[29]=-DefaultOrthoXpos[20];
				DefaultOrthoXpos[30]=-DefaultOrthoXpos[19];
				DefaultOrthoXpos[31]=-DefaultOrthoXpos[18];
				DefaultOrthoXpos[32]=-DefaultOrthoXpos[17];
			}
			if(tooth_num<1 || tooth_num>32) {
				throw new ApplicationException("Invalid tooth_num: "+tooth_num.ToString());//just for debugging
			}
			return DefaultOrthoXpos[tooth_num];
		}

		///<summary></summary>
		public static bool IsMaxillary(string tooth_id) {
			if(!IsValidToothID(tooth_id))
				return false;
			int intTooth=IdToInt(tooth_id);
			if(intTooth>=1 && intTooth<=16)
				return true;
			return false;
		}

		///<summary></summary>
		public static bool IsAnterior(string tooth_id) {
			if(!IsValidToothID(tooth_id))
				return false;
			int intTooth=IdToInt(tooth_id);
			if(intTooth>=6 && intTooth<=11)
				return true;
			if(intTooth>=22 && intTooth<=27)
				return true;
			return false;
		}

		///<summary></summary>
		public static bool IsRight(string tooth_id) {
			if(!IsValidToothID(tooth_id))
				return false;
			int intTooth=IdToInt(tooth_id);
			if(intTooth>=1 && intTooth<=8)
				return true;
			if(intTooth>=25 && intTooth<=32)
				return true;
			return false;
		}

		///<summary>This is just a copy of the real function.  Because of that, it works a little bit differently.</summary>
		public static string ToInternat(string tooth_id) {
			if(tooth_id=="")
				return "";
			int intToothI=0;//the international tooth number we will find
			int intTooth=0;
			try {
				intTooth=IdToInt(tooth_id);//this gives us the american 1-32. Primary are 4-13,20-29
			}
			catch {
				return "";//for situations where no validation was performed
			}
			if(IsPrimary(tooth_id)) {
				if(intTooth>=4 && intTooth<=8) {//UR= 51-55
					intToothI=59-intTooth;
				}
				else if(intTooth>=9 && intTooth<=13) {//UL= 61-65
					intToothI=52+intTooth;
				}
				else if(intTooth>=20 && intTooth<=24) {//LL= 71-75
					intToothI=95-intTooth;
				}
				else if(intTooth>=25 && intTooth<=29) {//LR= 81-85
					intToothI=56+intTooth;
				}
			}
			else {//adult toothnum
				if(intTooth>=1 && intTooth<=8) {//UR= 11-18
					intToothI=19-intTooth;
				}
				else if(intTooth>=9 && intTooth<=16) {//UL= 21-28
					intToothI=12+intTooth;
				}
				else if(intTooth>=17 && intTooth<=24) {//LL= 31-38
					intToothI=55-intTooth;
				}
				else if(intTooth>=25 && intTooth<=32) {//LR= 41-48
					intToothI=16+intTooth;
				}
			}
			return intToothI.ToString();
		}
		#endregion static functions

		///<summary></summary>
		private void ImportObj() {
			byte[] buffer=null;
			if(toothID=="1" || toothID=="16") {
				buffer=Properties.Resources.tooth1;
			}
			else if(toothID=="2" || toothID=="15") {
				buffer=Properties.Resources.tooth2;
			}
			else if(toothID=="3" || toothID=="14") {
				//file+="3.obj";
				buffer=Properties.Resources.tooth3;
			}
			else if(toothID=="4" || toothID=="13") {
				buffer=Properties.Resources.tooth4;
			}
			else if(toothID=="5" || toothID=="12") {
				buffer=Properties.Resources.tooth5;
			}
			else if(toothID=="6" || toothID=="11") {
				buffer=Properties.Resources.tooth6;
			}
			else if(toothID=="7" ||toothID=="10") {
				buffer=Properties.Resources.tooth7;
			}
			else if(toothID=="8" || toothID=="9") {
				buffer=Properties.Resources.tooth8;
			}
			else if(toothID=="17" || toothID=="32") {
				buffer=Properties.Resources.tooth32;
			}
			else if(toothID=="18" || toothID=="31") {
				buffer=Properties.Resources.tooth31;
			}
			else if(toothID=="19" || toothID=="30") {
				buffer=Properties.Resources.tooth30;
			}
			else if(toothID=="20" || toothID=="29") {
				buffer=Properties.Resources.tooth29;
			}
			else if(toothID=="21" || toothID=="28") {
				buffer=Properties.Resources.tooth28;
			}
			else if(toothID=="22" || toothID=="27") {
				buffer=Properties.Resources.tooth27;
			}
			else if(toothID=="23" || toothID=="24" || toothID=="25" ||toothID=="26") {
				buffer=Properties.Resources.tooth25;
			}
			else if(toothID=="A" || toothID=="J") {
				buffer=Properties.Resources.toothA;
			}
			else if(toothID=="B" || toothID=="I") {
				buffer=Properties.Resources.toothB;
			}
			else if(toothID=="C" || toothID=="H") {
				buffer=Properties.Resources.toothC;
			}
			else if(toothID=="D" || toothID=="G") {
				buffer=Properties.Resources.toothD;
			}
			else if(toothID=="E" || toothID=="F") {
				buffer=Properties.Resources.toothE;
			}
			else if(toothID=="P" || toothID=="O" || toothID=="Q" || toothID=="N") {
				buffer=Properties.Resources.toothP;
			}
			else if(toothID=="R" || toothID=="M") {
				buffer=Properties.Resources.toothR;
			}
			else if(toothID=="S" || toothID=="L") {
				buffer=Properties.Resources.toothS;
			}
			else if(toothID=="T" || toothID=="K") {
				buffer=Properties.Resources.toothT;
			}
			else if(toothID=="implant"){
				buffer=Properties.Resources.implant;
			}
			bool flipHorizontally=false;
			if(toothID!="implant" && IdToInt(toothID)>=9 && IdToInt(toothID)<=24) {
				flipHorizontally=true;
			}
			ArrayList ALv=new ArrayList();//vertices
			ArrayList ALvn=new ArrayList();//vertex normals
			Groups=new ArrayList();//type=ToothGroup
			ArrayList ALf=new ArrayList();//faces always part of a group
			MemoryStream stream=new MemoryStream(buffer);
			using(StreamReader sr = new StreamReader(stream)){
				String line;
				float[] vertex;
				string[] items;
				string[] subitems;
				int[][] face;//dim 1=the vertex. dim 2 has length=2, with 1st vertex, and 2nd normal.
				ToothGroup group=null;
				while((line = sr.ReadLine()) != null) {
					if(line.StartsWith("#")//comment
						|| line.StartsWith("mtllib")//material library.  We build our own.
						|| line.StartsWith("usemtl")//use material
						|| line.StartsWith("o")) {//object. There's only one object 
						continue;
					}
					if(line.StartsWith("v ")) {//vertex
						items=line.Split(new char[] { ' ' });
						vertex=new float[3];
						if(flipHorizontally) {
							vertex[0]=-Convert.ToSingle(items[1],CultureInfo.InvariantCulture);
						}
						else {
							vertex[0]=Convert.ToSingle(items[1],CultureInfo.InvariantCulture);
						}
						vertex[1]=Convert.ToSingle(items[2],CultureInfo.InvariantCulture);
						vertex[2]=Convert.ToSingle(items[3],CultureInfo.InvariantCulture);
						ALv.Add(vertex);
						continue;
					}
					if(line.StartsWith("vn")) {//vertex normal
						items=line.Split(new char[] { ' ' });
						vertex=new float[3];
						if(flipHorizontally) {
							vertex[0]=-Convert.ToSingle(items[1],CultureInfo.InvariantCulture);
						}
						else {
							vertex[0]=Convert.ToSingle(items[1],CultureInfo.InvariantCulture);
						}
						vertex[1]=Convert.ToSingle(items[2],CultureInfo.InvariantCulture);
						vertex[2]=Convert.ToSingle(items[3],CultureInfo.InvariantCulture);
						ALvn.Add(vertex);
						continue;
					}
					if(line.StartsWith("g")) {//group
						if(group != null) {
							//move all faces into the existing group
							group.Faces=new int[ALf.Count][][];
							for(int i=0;i<group.Faces.GetLength(0);i++) {
								group.Faces[i]=new int[((int[][])ALf[i]).Length][];
								for(int j=0;j<group.Faces[i].Length;j++) {//loop through vertices for the face
									group.Faces[i][j]=new int[2];
									group.Faces[i][j][0]=((int[][])ALf[i])[j][0];//vertex
									group.Faces[i][j][1]=((int[][])ALf[i])[j][1];//normal
								}
							}
							//move the existing group into the AL
							Groups.Add(group);
						}
						//start a new group to which all subsequent faces will be attached.
						group=new ToothGroup();
						ALf=new ArrayList();//reset faces
						//group.PaintColor=Color.FromArgb(255,255,253,209);//default to enamel
						switch(line) {
							case "g cube1_Cementum":
								group.GroupType=ToothGroupType.Cementum;
								//group.PaintColor=Color.FromArgb(255,255,240,170);//ARGB
								break;
							/*default:
								if(toothID=="30"){
									group.GroupType=ToothGroupType.Enamel;
									group.PaintColor=Color.FromArgb(204,182,84);
								}
								else {
									group.GroupType=ToothGroupType.Enamel;
									group.PaintColor=Color.FromArgb(255,255,253,209);
								}
								break;
							case "g cube1_M":
							case "g cube1_O":
							case "g cube1_D":
								if(toothID=="3" || toothID=="14") {
									group.GroupType=ToothGroupType.Filling;
									group.PaintColor=Color.DarkRed;
								}
								else if(toothID=="30"){
									group.GroupType=ToothGroupType.Enamel;
									group.PaintColor=Color.FromArgb(204,182,84);
								}
								else {
									group.GroupType=ToothGroupType.Enamel;
									group.PaintColor=Color.FromArgb(255,255,253,209);
								}
								break;*/
							/*case "g cube1_V":
								if(toothID=="6"){
									group.GroupType=ToothGroupType.Filling;
									group.PaintColor=Color.MediumBlue;
								}
								else{
									group.GroupType=ToothGroupType.Enamel;
									group.PaintColor=Color.FromArgb(255,255,253,209);
								}
								break;*/
							case "g cube1_Enamel2":
								group.GroupType=ToothGroupType.Enamel;
								//group.PaintColor=Color.FromArgb(255,255,253,209);
								break;
							case "g cube1_M":
								group.GroupType=ToothGroupType.M;
								//group.PaintColor=Color.Purple;
								break;
							case "g cube1_D":
								group.GroupType=ToothGroupType.D;
								//group.PaintColor=Color.Yellow;
								break;
							case "g cube1_F":
								group.GroupType=ToothGroupType.F;
								//group.PaintColor=Color.Blue;
								break;
							case "g cube1_I":
								group.GroupType=ToothGroupType.I;
								//group.PaintColor=Color.Green;
								break;
							case "g cube1_L":
								group.GroupType=ToothGroupType.L;
								//group.PaintColor=Color.Blue;
								break;
							case "g cube1_V":
								group.GroupType=ToothGroupType.V;
								//group.PaintColor=Color.Green;
								break;
							case "g cube1_B":
								group.GroupType=ToothGroupType.B;
								//group.PaintColor=Color.Blue;
								break;
							case "g cube1_O":
								group.GroupType=ToothGroupType.O;
								//group.PaintColor=Color.Green;
								break;
							case "g cube2_Canals":
								group.GroupType=ToothGroupType.Canals;
								break;
							case "g cube2_Buildup":
								group.GroupType=ToothGroupType.Buildup;
								break;
							case "g cube1_Implant":
								group.GroupType=ToothGroupType.Implant;
								break;
							//buildup
						}
					}
					if(line.StartsWith("f")) {//face. Usually 4 vertices, but not always.
						items=line.Split(new char[] { ' ' });
						face=new int[items.Length-1][];
						//do we need to load these backwards for flipping, so they'll still be counterclockwise?
						//It seems to work anyway, but it's something to keep in mind for later.
						for(int i=0;i<face.GetLength(0);i++) {
							subitems=items[i+1].Split(new char[] { '/' });// eg: 9//9
							face[i]=new int[2];
							face[i][0]=Convert.ToInt32(subitems[0])-1;//vertex
							face[i][1]=Convert.ToInt32(subitems[2])-1;//normal
						}
						ALf.Add(face);
						continue;
					}
				}//while readline
				//For the very last group, move all faces into the group
				group.Faces=new int[ALf.Count][][];
				for(int i=0;i<group.Faces.GetLength(0);i++) {
					group.Faces[i]=new int[((int[][])ALf[i]).Length][];
					for(int j=0;j<group.Faces[i].Length;j++) {//loop through vertices for the face
						group.Faces[i][j]=new int[2];
						group.Faces[i][j][0]=((int[][])ALf[i])[j][0];//vertex
						group.Faces[i][j][1]=((int[][])ALf[i])[j][1];//normal
					}
				}
				//move the last group into the AL
				Groups.Add(group);
			}
			Vertices=new float[ALv.Count][];
			for(int i=0;i<Vertices.GetLength(0);i++) {
				Vertices[i]=(float[])ALv[i];
			}
			Normals=new float[ALvn.Count][];
			for(int i=0;i<Normals.GetLength(0);i++) {
				Normals[i]=(float[])ALvn[i];
			}
			//MessageBox.Show(Vertices[2,2].ToString());
		}

		///<summary>dim 1=lines. dim 2 is points, (was always two). dim 3 is coordinates, always 3</summary>
		public float[][,] GetRctLines() {
			if(toothID=="1") {
				return new float[][,] {
					new float[,] { 
					{ -.7f,9.6f,1.6f }, 
					{ .6f,8,1.6f },
					{ 1.2f,5.8f,1.6f },
					{ .8f,0,.9f } },
					new float[,] { 
					{ -1.8f,9.5f,1.6f }, 
					{ -1.6f,8,1.6f },
					{ -1.6f,5.8f,1.6f },
					{ -.9f,0,.9f } }
				};
			}
			if(toothID=="16") {
				return new float[][,] {
					new float[,] { 
					{ .7f,9.6f,1.6f }, 
					{ -.6f,8,1.6f },
					{ -1.2f,5.8f,1.6f },
					{ -.8f,0,.9f } },
					new float[,] { 
					{ 1.8f,9.5f,1.6f }, 
					{ 1.6f,8,1.6f },
					{ 1.6f,5.8f,1.6f },
					{ .9f,0,.9f } }
				};
			}
			if(toothID=="2") {
				return new float[][,] {
					new float[,] { 
					{ .3f,10.6f,3.4f }, 
					{ 1.4f,8,3.2f },
					{ 1.7f,5,1.9f },
					{ .9f,0,1f } },
					new float[,] { 
					{ -1.8f,10.5f,3.4f }, 
					{ -2,7,3.2f },
					{ -1.7f,4,1.9f },
					{ -1,0,1.1f } },
					new float[,] { 
					{ -2,11.5f,-3.7f }, 
					{ -.6f,6.3f,-4 },
					{ 0,0,-2.3f } } 
				};
			}
			if(toothID=="15") {
				return new float[][,] {
					new float[,] { 
					{ -.3f,10.6f,3.4f }, 
					{ -1.4f,8,3.2f },
					{ -1.7f,5,1.9f },
					{ -.9f,0,1f } },
					new float[,] { 
					{ 1.8f,10.5f,3.4f }, 
					{ 2,7,3.2f },
					{ 1.7f,4,1.9f },
					{ 1,0,1.1f } },
					new float[,] { 
					{ 2,11.5f,-3.7f }, 
					{ .6f,6.3f,-4 },
					{ 0,0,-2.3f } } 
				};
			}
			if(toothID=="3") {
				return new float[][,] {
					new float[,] { 
					{ 1.4f,11.5f,3.4f }, 
					{ 2.2f,9.4f,3.2f },
					{ 2.4f,6.7f,3.2f },
					{ 1.2f,0,1.1f } },
					new float[,] { 
					{ -2.7f,11.5f,3.4f }, 
					{ -2.9f,9,3.2f },
					{ -2.6f,5,3.2f },
					{ -1.2f,0,1.1f } },
					new float[,] { 
					{ 0,12.5f,-4.3f }, 
					{ 0,9.4f,-4.3f },
					{ 0,0,-2.2f } } 
				};
			}
			if(toothID=="14") {
				return new float[][,] {
					new float[,] { 
					{ -1.4f,11.5f,3.4f }, 
					{ -2.2f,9.4f,3.2f },
					{ -2.4f,6.7f,3.2f },
					{ -1.2f,0,1.1f } },
					new float[,] { 
					{ 2.7f,11.5f,3.4f }, 
					{ 2.9f,9,3.2f },
					{ 2.6f,5,3.2f },
					{ 1.2f,0,1.1f } },
					new float[,] { 
					{ 0,12.5f,-4.3f }, 
					{ 0,9.4f,-4.3f },
					{ 0,0,-2.2f } }  
				};
			}
			if(toothID=="4") {
				return new float[][,] {
					new float[,] { 
					{ 0,13.5f,1.2f }, 
					{ -.2f,10,.6f },
					{ 0,0,0 } } 
				};
			}
			if(toothID=="13") {
				return new float[][,] {
					new float[,] { 
					{ 0,13.5f,1.2f }, 
					{ .2f,10,.6f },
					{ 0,0,0 } } 
				};
			}
			if(toothID=="5") {
				return new float[][,] {
					new float[,] { 
					{ -1.1f,13.5f,1.6f }, 
					{ 0,6,1.6f },
					{ 0,0,1 } } 
				};
			}
			if(toothID=="12") {
				return new float[][,] {
					new float[,] { 
					{ 1.1f,13.5f,1.6f }, 
					{ 0,6,1.6f },
					{ 0,0,1 } } 
				};
			}
			if(toothID=="6") {
				return new float[][,] {
					new float[,] { 
					{ -.4f,16.5f,0 }, 
					{ 0,11,0 },
					{ 0,0,0 } } };
			}
			if(toothID=="11") {
				return new float[][,] {
					new float[,] { 
					{ .4f,16.5f,0 }, 
					{ 0,11,0 },
					{ 0,0,0 } } };
			}
			if(toothID=="7") {
				return new float[][,] {
					new float[,] { 
					{ -.8f,12.5f,.6f }, 
					{ -.3f,10,0 },
					{ 0,0,0 } } };
			}
			if(toothID=="10") {
				return new float[][,] {
					new float[,] { 
					{ .8f,12.5f,.6f }, 
					{ .3f,10,0 },
					{ 0,0,0 } } };
			}
			if(toothID=="8"){
				return new float[][,] {
					new float[,] { 
					{ 0,12.6f,0 }, 
					{ 0,0,0 } } };
			}
			if(toothID=="9") {
				return new float[][,] {
					new float[,] { 
					{ 0,12.6f,0 }, 
					{ 0,0,0 } } };
			}
			if(toothID=="25" || toothID=="26") {
				return new float[][,] {
					new float[,] { 
					{ -.5f,-12,0 },
					{ -.2f,-9,0 },
					{ 0,0,0 } } };
			}
			if(toothID=="24" || toothID=="23") {
				return new float[][,] {
					new float[,] { 
					{ .5f,-12,0 }, 
					{ .2f,-9,0 },
					{ 0,0,0 } } };
			}
			if(toothID=="27") {
				return new float[][,] {
					new float[,] { 
					{ -.5f,-15.5f,0 },
					{ -.1f,-13,0 },
					{ 0,0,0 } } };
			}
			if(toothID=="22") {
				return new float[][,] {
					new float[,] { 
					{ .5f,-15.5f,0 },
					{ .1f,-13,0 },
					{ 0,0,0 } } };
			}
			if(toothID=="28") {
				return new float[][,] {
					new float[,] { 
					{ -.2f,-13.5f,0 },
					{ 0,0,0 } } };
			}
			if(toothID=="21") {
				return new float[][,] {
					new float[,] { 
					{ .2f,-13.5f,0 },
					{ 0,0,0 } } };
			}
			if(toothID=="29") {
				return new float[][,] {
					new float[,] { 
					{ -.3f,-14,0 },
					{ 0,-12,0 },
					{ 0,0,0 } } };
			}
			if(toothID=="20") {
				return new float[][,] {
					new float[,] { 
					{ .3f,-14,0 },
					{ 0,-12,0 },
					{ 0,0,0 } } };
			}
			if(toothID=="30") {
				return new float[][,] {
					new float[,] { 
					{ .9f,-13.5f,0 }, 
					{ 2.2f,-10,0 },
					{ 2.6f,-7,0 },
					{ 1.7f,0,0 } },
					new float[,] { 
					{ -4.3f,-13.5f,0 }, 
					{ -4f,-9,0 },
					{ -3.3f,-5,0 },
					{ -1.7f,0,0 } } 
				};
			}
			if(toothID=="19") {
				return new float[][,] {
					new float[,] { 
					{ -.9f,-13.5f,0 }, 
					{ -2.2f,-10,0 },
					{ -2.6f,-7,0 },
					{ -1.7f,0,0 } },
					new float[,] { 
					{ 4.3f,-13.5f,0 }, 
					{ 4f,-9,0 },
					{ 3.3f,-5,0 },
					{ 1.7f,0,0 } } 
				};
			}
			if(toothID=="31") {
				return new float[][,] {
					new float[,] { 
					{ 0,-12.5f,0 }, 
					{ 1.8f,-7.5f,0 },
					{ 2.2f,-4,0 },
					{ 1.7f,0,0 } },
					new float[,] { 
					{ -3.4f,-12.5f,0 }, 
					{ -3.4f,-8,0 },
					{ -3f,-5,0 },
					{ -1.7f,0,0 } } 
				};
			}
			if(toothID=="18") {
				return new float[][,] {
					new float[,] { 
					{ 0,-12.5f,0 }, 
					{ -1.8f,-7.5f,0 },
					{ -2.2f,-4,0 },
					{ -1.7f,0,0 } },
					new float[,] { 
					{ 3.4f,-12.5f,0 }, 
					{ 3.4f,-8,0 },
					{ 3f,-5,0 },
					{ 1.7f,0,0 } } 
				};
			}
			if(toothID=="32") {
				return new float[][,] {
					new float[,] { 
					{ -.7f,-10.5f,0 }, 
					{ .8f,-7.5f,0 },
					{ 1.7f,-4,0 },
					{ 1.6f,0,0 } },
					new float[,] { 
					{ -3.2f,-10.5f,0 }, 
					{ -3.4f,-8,0 },
					{ -3f,-5,0 },
					{ -1.7f,0,0 } } 
				};
			}
			if(toothID=="17") {
				return new float[][,] {
					new float[,] { 
					{ .7f,-10.5f,0 }, 
					{ -.8f,-7.5f,0 },
					{ -1.7f,-4,0 },
					{ -1.6f,0,0 } },
					new float[,] { 
					{ 3.2f,-10.5f,0 }, 
					{ 3.4f,-8,0 },
					{ 3f,-5,0 },
					{ 1.7f,0,0 } } 
				};
			}
			return new float[0][,];
		}

		///<summary>dim 1=points, dim 2 is coordinates, always 3</summary>
		public float[,] GetSealantLine() {
			if(IsMaxillary(toothID)){
				return new float[,] {
				{ 1.5f,-4f,1.5f }, 
				{ .75f,-4f,2.25f }, 
				{ -.75f,-4f,2.25f }, 
				{ -1.5f,-4f,1.5f }, 
				{ -1.5f,-4f,.75f }, 
				{ 1.5f,-4f,-.75f }, 
				{ 1.5f,-4f,-1.5f }, 
				{ .75f,-4f,-2.25f }, 
				{ -.75f,-4f,-2.25f }, 
				{ -1.5f,-4f,-1.5f } };
			}
			else{
				return new float[,] {
				{ -1.5f,4f,1.5f }, 
				{ -.75f,4f,2.25f }, 
				{ .75f,4f,2.25f }, 
				{ 1.5f,4f,1.5f }, 
				{ 1.5f,4f,.75f }, 
				{ -1.5f,4f,-.75f }, 
				{ -1.5f,4f,-1.5f }, 
				{ -.75f,4f,-2.25f }, 
				{ .75f,4f,-2.25f }, 
				{ 1.5f,4f,-1.5f } };
			}
		}

		///<summary>dim 1=vertices. dim 2 is coordinates, always 3</summary>
		public float[,] GetBUpoly() {
			if(toothID=="1") {
				return new float[,] { 
					{ -1.5f,0,0 }, 
					{ -1.5f,2.3f,0 },
					{ 0,1.5f,0},
					{ 1.4f,2.3f,0 },
					{ 1.4f,0,0 } 
				};
			}
			if(toothID=="16") {
				return new float[,] { 
					{ 1.5f,0,0 }, 
					{ 1.5f,2.3f,0 },
					{ 0,1.5f,0},
					{ -1.4f,2.3f,0 },
					{ -1.4f,0,0 } 
				};
			}
			if(toothID=="2") {
				return new float[,] { 
					{ -1.8f,0,0 }, 
					{ -1.8f,2.3f,0 },
					{ 0,1.5f,0},
					{ 1.6f,2.3f,0 },
					{ 1.6f,0,0 } 
				};
			}
			if(toothID=="15") {
				return new float[,] { 
					{ 1.8f,0,0 }, 
					{ 1.8f,2.3f,0 },
					{ 0,1.5f,0},
					{ -1.6f,2.3f,0 },
					{ -1.6f,0,0 } 
				};
			}
			if(toothID=="3") {
				return new float[,] { 
					{ -2.3f,0,0 }, 
					{ -2.3f,2.6f,0 },
					{ 0,1.7f,0},
					{ 2.1f,2.6f,0 },
					{ 2.1f,0,0 } 
				};
			}
			if(toothID=="14") {
				return new float[,] { 
					{ 2.3f,0,0 }, 
					{ 2.3f,2.6f,0 },
					{ 0,1.7f,0},
					{ -2.1f,2.6f,0 },
					{ -2.1f,0,0 } 
				};
			}
			if(toothID=="4"
				|| toothID=="5"
				|| toothID=="6"
				|| toothID=="7"
				|| toothID=="8"
				|| toothID=="9"
				|| toothID=="10"
				|| toothID=="11"
				|| toothID=="12"
				|| toothID=="13"
				) {
				return new float[,] { 
					{ -.8f,0,0 }, 
					{ -.8f,3.5f,0 },
					{ .8f,3.5f,0 },
					{ .8f,0,0 } 
				};
			}
			if(toothID=="23"
				|| toothID=="24"
				|| toothID=="25"
				|| toothID=="26"
				) {
				return new float[,] { 
					{ -.7f,0,0 }, 
					{ -.7f,-3.5f,0 },
					{ .7f,-3.5f,0 },
					{ .7f,0,0 } 
				};
			}
			if(toothID=="20"
				|| toothID=="21"
				|| toothID=="22"
				|| toothID=="27"
				|| toothID=="28"
				|| toothID=="29"
				) {
				return new float[,] { 
					{ -.8f,0,0 }, 
					{ -.8f,-3.5f,0 },
					{ .8f,-3.5f,0 },
					{ .8f,0,0 } 
				};
			}
			if(toothID=="30") {
				return new float[,] { 
					{ -2.8f,0,0 }, 
					{ -2.8f,-2.4f,0 },
					{ 0,-1.5f,0},
					{ 2.3f,-2.4f,0 },
					{ 2.3f,0,0 } 
				};
			}
			if(toothID=="19") {
				return new float[,] { 
					{ 2.8f,0,0 }, 
					{ 2.8f,-2.4f,0 },
					{ 0,-1.5f,0},
					{ -2.3f,-2.4f,0 },
					{ -2.3f,0,0 } 
				};
			}
			if(toothID=="31") {
				return new float[,] { 
					{ -2.6f,0,0 }, 
					{ -2.6f,-2.1f,0 },
					{ 0,-1.5f,0},
					{ 2.3f,-2.1f,0 },
					{ 2.3f,0,0 } 
				};
			}
			if(toothID=="18") {
				return new float[,] { 
					{ 2.6f,0,0 }, 
					{ 2.6f,-2.1f,0 },
					{ 0,-1.5f,0},
					{ -2.3f,-2.1f,0 },
					{ -2.3f,0,0 } 
				};
			}
			if(toothID=="32") {
				return new float[,] { 
					{ -2.6f,0,0 }, 
					{ -2.6f,-2.1f,0 },
					{ 0,-1.5f,0},
					{ 2.1f,-2.1f,0 },
					{ 2.1f,0,0 } 
				};
			}
			if(toothID=="17") {
				return new float[,] { 
					{ 2.6f,0,0 }, 
					{ 2.6f,-2.1f,0 },
					{ 0,-1.5f,0},
					{ -2.1f,-2.1f,0 },
					{ -2.1f,0,0 } 
				};
			}
			return new float[0,0];
		}

	}
}
