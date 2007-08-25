using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class ProcButtons {
		///<summary></summary>
		public static ProcButton[] List;

		///<summary></summary>
		public static void Refresh() {
			string command="SELECT * FROM procbutton ORDER BY ItemOrder";
			DataTable table=General.GetTable(command);
			List=new ProcButton[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new ProcButton();
				List[i].ProcButtonNum=PIn.PInt(table.Rows[i][0].ToString());
				List[i].Description  =PIn.PString(table.Rows[i][1].ToString());
				List[i].ItemOrder    =PIn.PInt(table.Rows[i][2].ToString());
				List[i].Category     =PIn.PInt(table.Rows[i][3].ToString());
				List[i].ButtonImage  =PIn.PBitmap(table.Rows[i][4].ToString());
			}
		}

		///<summary>must have already checked procCode for nonduplicate.</summary>
		public static void Insert(ProcButton but) {
			string command= "INSERT INTO procbutton (Description,ItemOrder,Category,ButtonImage) VALUES("
				+"'"+POut.PString(but.Description)+"', "
				+"'"+POut.PInt   (but.ItemOrder)+"', "
				+"'"+POut.PInt   (but.Category)+"', "
				+"'"+POut.PBitmap(but.ButtonImage)+"')";
			but.ProcButtonNum=General.NonQ(command,true);
		}

		///<summary></summary>
		public static void Update(ProcButton but) {
			string command="UPDATE procbutton SET " 
				+ "Description = '" +POut.PString(but.Description)+"'"
				+ ",ItemOrder = '"  +POut.PInt   (but.ItemOrder)+"'"
				+ ",Category = '"   +POut.PInt   (but.Category)+"'"
				+ ",ButtonImage = '"+POut.PBitmap(but.ButtonImage)+"'"
				+" WHERE ProcButtonNum = '"+POut.PInt(but.ProcButtonNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(ProcButton but) {
			string command="DELETE FROM procbuttonitem WHERE ProcButtonNum = '"
				+POut.PInt(but.ProcButtonNum)+"'";
			General.NonQ(command);
			command="DELETE FROM procbutton WHERE ProcButtonNum = '"
				+POut.PInt(but.ProcButtonNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static ProcButton[] GetForCat(int selectedCat){
			ArrayList AL=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].Category==selectedCat){
					AL.Add(List[i]);
				}
			}
			ProcButton[] retVal=new ProcButton[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
		}

		/*//<summary>Used when a button is moved out of a category.  This leaves a 'hole' in the order, so we need to clean up the orders.  Remember to run Refresh before this.</summary>
		public static void ResetOrder(int cat){

		}*/

		///<summary>Deletes all current ProcButtons from the Chart module, and then adds the default ProcButtons.  Procedure codes must have already been entered or they cannot be added as a ProcButton.</summary>
		public static void SetToDefault() {
			string command= @"
				DELETE FROM procbutton;
				DELETE FROM procbuttonitem;
				DELETE FROM definition WHERE Category=26";
			General.NonQ(command);
			int category;//defNum
			int procButtonNum;
			int autoCodeNum;
			int autoCodeNum2;
			//General---------------------------------------------------------------------------------------------------------
			command="INSERT INTO definition (Category,ItemOrder,ItemName,ItemValue,ItemColor,IsHidden) "
				+"VALUES (26,0,'General','',0,0)";
			category=General.NonQ(command,true);
			//Amalgam
			autoCodeNum=AutoCodes.GetNumFromDescript("Amalgam");
			if(autoCodeNum!=0) {
				command="INSERT INTO procbutton (Description,ItemOrder,Category,ButtonImage) VALUES('Amalgam',0,"
					+POut.PInt(category)+@",'Qk12BgAAAAAAADYAAAAoAAAAFAAAABQAAAABACAAAAAAAAAAAADEDgAAxA4AAAAAAAAAAAAA/////////////////////9bW1v+ctbX/vebv/8XW1v+tvbX/hKWt/36crf9+nKX/nLW1/97v7//m9/f/pdbe/5y9vf/3////////////////////////////////////ztbO/5y1rf+cxcX/hKWt/5S1vf+cvcX/nMXO/5S9zv+Mrb3/jK2t/629vf+cxc7/nL29//f///////////////////////////////////+9zs7/fpSM/5S1vf+cxc7/tdbe/87m5v/O5u//1u/v/8Xe5v+lxdb/jK21/4Slrf+cvbX/9////////////////////////////////////7XW1v+ErbX/rc7W/8Xm5v/F3ub/3u/v/9739//W7+//zubv/87m7/+11t7/jK21/5y9vf/////////////////////////////////39/f/nLW9/5zFzv/O5ub/3vf3/8Xe5v/m////7////+/////O5u//zu/v/9739/+11tb/lLW1/+/39////////////////////////////9bW5v9NXb3/VWbv/8XW9//v////vdbm/+b3///v////9////73e3v+UtbX/5vf3/7XF9/9ufr3/zt7m////////////////////////////xcXv/xws3v8THO//bn73/87e5v+MrbX/xd7m/+/////v////rc7W/4ylpf/e7///PU33/xwk5v+1ve////////////////////////////+1ve//HCze/xwk7/80Pe//doy9/5y9vf/O5ub/9/////f///+1ztb/nLW1/4yc9/8cJOb/EyTv/4SU7////////////////////////////5yl7/8kLOb/HCzv/xwk7/80Rc7/foze/5Sl7/+crff/nK33/36U5v9dbsX/PUXv/xwk5v8cJOb/XW7m///////////////////////m9/f/foTv/yQs5v8cLO//HCzm/xws7/8kLOb/JCzm/yQs5v8kLOb/JCzm/xws5v8cJOb/HCzm/xwk7/9FVdb/9////////////////////+/39/9dbt7/HCTm/xws5v8cLOb/HCzm/xws5v8cLOb/HCzm/xws5v8cLOb/HCzm/xws5v8cLOb/HCTv/0VVzv/3/////////////////////////1Vm1v8cJO//HCzm/xws5v8cJOb/HCTv/xwk5v8cJOb/HCTm/xwk5v8cJO//HCTm/xws5v8cJO//RVXO//f39//////////////////3////RVXO/xwk7/8cLOb/HCTm/yw05v8sNOb/ND3v/zQ97/80Pe//ND3v/yw05v8kLO//HCTm/xwk7/9FVc7/9/f3//////////////////f///9FVc7/HCTv/xwk5v8sNOb/jJzm/3aErf+1xff/xdb3/8XW//+ElL3/nLXe/4yc9/8kLOb/HCTv/0VVzv/39/f/////////////////9////0VVzv8THO//ND3v/7XF9//O5ub/lK2l/87e1v/3////9////5y1rf/W7+//9////4yU9/8cJO//RVXW//f/////////////////////////boTF/1Vm7//Fzv//9////97v9/+1ztb/xdbW//f////m9/f/rcXO/+b39//3////7////5Sl7/9NXbX/9/f3//////////////////////+91tb/nL3F//f////v////5vf3/87m5v/e7/f/7////+b39//W7+//7////+/////v////tc7O/629vf////////////////////////////////+lxcX/pcXF/+/////3////1u/v/9739//3////3u/3/97v9//3////7////7XO1v+lvcX/9/////////////////////////////////////f39/+lvcX/nL3F/+b39//3////7////+/////m////7////97v7/+txcX/pb3F//f39/////////////////////////////////////////////f39/+txcX/lLW1/629vf/O3t7/3vf3/8Xe3v+lxcX/jKWc/7XFxf/39/f//////////////////////w==')";
				procButtonNum=General.NonQ(command,true);
				command="INSERT INTO procbuttonitem (ProcButtonNum,CodeNum,AutoCodeNum) VALUES ("+POut.PInt(procButtonNum)+",'',"
					+POut.PInt(autoCodeNum)+")";
				General.NonQ(command);
			}
			//Composite
			autoCodeNum=AutoCodes.GetNumFromDescript("Composite");
			if(autoCodeNum!=0) {
				command="INSERT INTO procbutton (Description,ItemOrder,Category,ButtonImage) VALUES('Composite',1,"
					+POut.PInt(category)+@",'Qk12BgAAAAAAADYAAAAoAAAAFAAAABQAAAABACAAAAAAAAAAAADEDgAAxA4AAAAAAAAAAAAA///////////e5v//RU3m/yQ05v8kNOb/JDTm/xwk7/8cJO//JCzm/36UnP9+lJT/fpSc/3aUlP9+lIz/xc7O/////////////////////////////////3Z+9/8kLOb/Znbv/3aE7/+ElO//fpTv/zQ97/8kLOb/rcXm/73e3v+11t7/tdbe/5zFzv+EpaX/1t7e////////////////////////////LDTv/11u5v/e7/f/5vf//+/////3////pbX//yQs7//e7///7////+b39//e7/f/zubv/5S1vf+tvbX///////////////////////////80Pff/XW7m/+b////3////7/////f///+1xf//JCzv/+b3///3////7////+b39//W7+//nL3F/629vf///////////////////////////z1F9/9dbu//7/f//+/////v////9////4yc//89Rff/5vf//+/////v////7////9bv7/+cvcX/rb29////////////////////////////ND3v/3aE7//3////7////+/////v////XWb3/36M9//3////7////+/////v////1u/3/5S1vf+9zs7///////////////////////////8sNO//doTv//f////v////7////+////80Pe//pbX///f////v////7////+/////e7/f/jK2t/9be3v///////////////////////////yw07/9ufu//7////+/////v////3u///zQ97/+9zv//9////+/////v////7////87m7/9+lIz/3u/v////////////////////////////JCzv/2Z27//m////7/////f///+MlP//VWb3//f////v////7////+/////v////zubm/36UlP/m7+////////////////////////////89Rff/VWbm/97v9//3////7////11m9/9mbvf/9////+/////v////7////+////+91t7/lK2t//f3/////////////////////////////5yl//80Rd7/zubv//f////O3v//LDTv/7XF///3////7////+/////v////5vf//6W9xf+ctbX/////////////////////////////////1t7//yQ05v+MnO///////4yU//89Rff/5vf//+/////v////7////+/////m9///nL3F/7XFzv/////////////////////////////////39///Zm73/0VV5v+9zvf/LDTv/5yl///3////7////+/////v////7////+b39/+UrbX/1t7e//////////////////////////////////////+UnPf/JCzm/yw07/9dbvf/5vf//+/////v////7////+/////v////1ubv/4ylpf/m7+///////////////////////////////////////97m//8sNO//XW7v/97v///3////7////+/////v////7////+/////F3t7/nK2t/////////////////////////////////////////////////25+vf+Mpc7/7////+/////v////7////+/////v////7////6W9vf/Fzs7/////////////////////////////////////////////////pb21/5Strf/O5ub/7////+/////v////7/////f////e7+//jK2t/+bv7//////////////////////////////////////////////////O5ub/fpyc/7XOzv/e9/f/9////+/////v////9////7XOzv+EpaX/5vf3/////////////////////////////////////////////////+bv7/+MvcX/lK2t/5y1tf/O5ub/5vf3/+bv7/+9zs7/lL29/4ytrf/3////////////////////////////////////////////////////3ubm/5TFxf+95u//rb29/4ylnP+cra3/lK2t/6W9vf+t1t7/nLW1/////////////////////////////////w==')";
				procButtonNum=General.NonQ(command,true);
				command="INSERT INTO procbuttonitem (ProcButtonNum,CodeNum,AutoCodeNum) VALUES ("+POut.PInt(procButtonNum)+",'',"
					+POut.PInt(autoCodeNum)+")";
				General.NonQ(command);
			}
			//Crown
			if(ProcedureCodes.IsValidCode("D2750") || ProcedureCodes.IsValidCode("N4118")) {
				command="INSERT INTO procbutton (Description,ItemOrder,Category,ButtonImage) VALUES('Crown',2,"
					+POut.PInt(category)+@",'Qk12BgAAAAAAADYAAAAoAAAAFAAAABQAAAABACAAAAAAAAAAAADEDgAAxA4AAAAAAAAAAAAA//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////+1ve//PVXW/1Vu7/9mdu//Znbv/2Z27/9mdu//XW7v/2Z27/9mbu//Zm7v/2Zu7/9dbu//XW7v/01m7/9FXeb/pa33/////////////////11m7/89Re//XWbv/11m7/9dZu//XWbv/11m7/9dZu//XWbv/11m7/9dZu//XWbv/11m7/9dZu//XWbv/z1F7/9NVe/////////////e3v//LDTv/6Wt9//39///9/f///f3///39///9/f///f3///39///9/f///f3///39///9/f///f3///39///vb3//zQ97//W1v///////5Sc9/80Re//5ub////////////////////////////////////////////////////////////////////////W1v//PUXv/73F9//39///XWbv/2Zu7////////////////////////////////////////////////////////////////////////////9bW//89Re//vcX3/87O//8sNO//tb3/////////////////////////////////////////////////////////////////////////////3t7//zRF7/+cpff/hIz3/z1F7//v7///////////////////////////////////////////////////////////////////////////////////XWbv/2529/9udvf/VV3v//f3//////////////////////////////////////////////////////////////////////////////////+MlPf/TVXv/2529/9VXe//9/f/////////////////////////////7+///6Wl9/+1vf///////////////////////////////////////6Wl9/80Re//bnb3/1Vd7//39//////////////////////////////W1v//RU3v/6Wl9///////////////////////////////////////3t7//yw07/9mdu//XWbv//f3/////////////////////////////87W//9FTe//zs7////////////////////////////////////////39///VV3v/3Z+9/9NVe//9/f/////////////////////////////1tb//1Vd7//Ozv////////////////////////////////////////f3//9VXe//vb3//yQs7//Ozv////////////////////////////+9xff/ND3v/4yU9//v7///////////////////////////////////tb3//yw07//v7///VV3v/z1F7/+cnPf/5ub///f3///m5v//jJT3/zQ97/89Re//LDTv/0VN7/+MlPf/pa33/87O///v9///5ub//5Sc9/8sNO//foT3///////e5v//foT3/z1N7/8sNO//VV3v/zQ97/9FTe//lJz3/+/v///e5v//foT3/01V7/9FTe//LDTv/0VN7/8sNO//ND3v/4yU9////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////w==')";
				procButtonNum=General.NonQ(command,true);
				if(ProcedureCodes.IsValidCode("D2750")) {
					command="INSERT INTO procbuttonitem (ProcButtonNum,CodeNum,AutoCodeNum) VALUES ("+POut.PInt(procButtonNum)
						+","+ProcedureCodes.GetCodeNum("D2750")+",0)";
					General.NonQ(command);
				}
				if(ProcedureCodes.IsValidCode("N4118")) {
					command="INSERT INTO procbuttonitem (ProcButtonNum,CodeNum,AutoCodeNum) VALUES ("+POut.PInt(procButtonNum)
						+","+ProcedureCodes.GetCodeNum("N4118")+",0)";
					General.NonQ(command);
				}
			}
			//RCT
			autoCodeNum=AutoCodes.GetNumFromDescript("Root Canal");
			if(autoCodeNum!=0) {
				command="INSERT INTO procbutton (Description,ItemOrder,Category,ButtonImage) VALUES('RCT',3,"
					+POut.PInt(category)+@",'Qk12BgAAAAAAADYAAAAoAAAAFAAAABQAAAABACAAAAAAAAAAAADEDgAAxA4AAAAAAAAAAAAA///////////////////////////////////////////F1tb/boyt/xwkpf/m5ub//////////////////////////////////////////////////////////////////////////////////////36MjP9VbpT/EySc/7XFxf//////////////////////////////////////////////////////////////////////////////////////XWZm/36lzv8kNMX/ZnZu//////////////////////////////////////////////////////////////////////////////////f39/9NXV3/fpz//wMD//9ddm7/zs7O////////////////////////////////////////////////////////////////////////////3t7e/2Z+dv9mfv//AwP//2aEfv+lpaX////////////////////////////////////////////////////////////////////////////FxcX/XXZ2/26E//8DA///jK2t/5ycnP///////////////////////////////////////////////////////////////////////////8XFxf9mfnb/fpT//wMD//+Era3/lJSU//f39///////////////////////////////////////////////////////////////////////nKWl/2aMjP9uhP//AwP//4y1tf+UnJz/9/f3//////////////////////////////////////////////////////////////////////9mbm7/ZoSE/2Z+//8DA///nMXF/4SMjP/39/f//////////////////////////////////////////////////////////////////////2Zubv9ujIT/Znb//wMD//+lzs7/foyM//f39///////////////////////////////////////////////////////////////////////PUVF/36lpf9ufv//AwP//63W1v9ufn7/9/f3/////////////////////////////////////////////////////////////////+/v7/9VVVX/jLW1/2Z2//8DA///td7m/2Z2dv/v7+//////////////////////////////////////////////////////////////////3t7e/1VVVf+Uxb3/bn7//wMD//+13ub/ZnZ2/+bm5v/////////////////////////////////////////////////////////////////e3t7/XWZu/5TFvf9mdv//AwP//7Xe5v9mdnb/3t7e/////////////////////////////////////////////////////////////////8XFxf9mbm7/nMXF/11m//8DA///zu/3/1VmXf/e3t7/////////////////////////////////////////////////////////////////zs7O/11mZv+cxcX/XW7//wMD///O7/f/VWZm/87Ozv/////////////////////////////////////////////////////////////////FxcX/ZnZ2/6XOzv9dbv//AwP//9bv9/9Vbm7/pa2t/////////////////////////////////////////////////////////////////62trf9VZmb/rdbW/01d//8DA///1u/3/01mZv+UlJT/////////////////////////////////////////////////////////////////nJyc/0VNTf+t3tb/XW7//wMD//+Urf//RV1d/4yUlP////////////////////////////////////////////////////////////////+1tbX/RV1d/7Xe3v9dZv//AwP//4yl//89VVX/foSE/////////////////////////////////w==')";
				procButtonNum=General.NonQ(command,true);
				command="INSERT INTO procbuttonitem (ProcButtonNum,CodeNum,AutoCodeNum) VALUES ("+POut.PInt(procButtonNum)+",'',"
					+POut.PInt(autoCodeNum)+")";
				General.NonQ(command);
			}
			//RCT BU PFM
			autoCodeNum=AutoCodes.GetNumFromDescript("Root Canal");
			autoCodeNum2=AutoCodes.GetNumFromDescript("BU/P&C");
			if(autoCodeNum!=0 || ProcedureCodes.IsValidCode("D2750")) {//we add this button if either RCT or crown is available.
				command="INSERT INTO procbutton (Description,ItemOrder,Category,ButtonImage) VALUES('RCT BU PFM',4,"
					+POut.PInt(category)+@",'Qk12BgAAAAAAADYAAAAoAAAAFAAAABQAAAABACAAAAAAAAAAAADEDgAAxA4AAAAAAAAAAAAA////////////////pcXF/63W7/89Te//Exzv/01V7/+95u//tebm/7Xm5v9+lO//HCTv/xwk7/9Vbu//pdbe/73m5v////////////////////////////////+cvb3/rdbv/z1N7/8cJO//PUXv/63W7/+15ub/tebm/11u7/8cJO//HCTv/01d7/+cztb/td7e////////////////////////////9/f//5y9tf+t1u//PU3v/xwk7/8kLO//pb33/73m5v/W7/f/PU3v/xwk7/8cJO//PU3v/4zF3v+13t7/////////////////////////////////lL21/6XW7/89Te//HCTv/xwk7/+ElPf/zvfv/5yt9/8kLO//HCTv/xwk7/89Te//lMXe/73e3v////////////////////////////////+UvbX/vd73/0VN7/8cJO//HCzv/yw97/9mbu//LDTv/xwk7/8cLO//HCTv/0VN7/+Uvc7/rc7O/////////////////////////////////6XO1v+13u//RU3v/xwk7/8cLO//HCTv/xwk7/8cJO//HCzv/xws7/8cJO//RU3v/4S13v+lztb////////////////////////////m5v//PU3v/zRF5v8kLO//EyTv/xwk7/8cJO//HCTv/xwk7/8cJO//HCTv/xMk7/8cJO//JCzv/zRF5v/W1v///////////////////////5yl9/8sNO//fn73/4SM9/+EjPf/hIz3/4SM9/+EjPf/hIz3/4SM9/+EjPf/hIz3/4SM9/+EhPf/ND3v/4yU9//////////////////39///TVXv/36E9/////////////////////////////////////////////////////////////////9mbvf/VV3v//f3/////////////8XF//8cLO//zs7//////////////////////////////////////////////////////////////////4SM9/8sPe//7+//////////////hIz3/0VN7//39///////////////////////////////////////////////////////////////////paX3/yQs7//m5v///////+bm//9FTe//hIT3///////////////////////////////////////////////////////////////////////Fxf//JCzv/7299///////xcX//yw07/+9vff//////////////////////////////////////////////////////////////////////+/v//89Re//VV3v///////Ozv//ND3v/7299////////////////////////////+/v///e3v//9/f/////////////////////////////9/f//1Vd7/9FTe///////8XO//80Pe//vb33////////////////////////////ra33/11m7//e3v//////////////////////////////////dn73/yQs7///////xcX//yw07//Fxf////////////////////////////+UlPf/Zm7v//////////////////////////////////////+1tf//JCzv///////W1v//NEXv/7299////////////////////////////5yl9/92fvf//////////////////////////////////////7299/8kLO////////f3//9dZu//fn73////////////////////////////foT3/z1F7//Ozv//////////////////////////////////dn73/zQ97////////////5yl9/8kLO//jJT3/+bm///39///5u///4SM9/8sNO//LDTv/yQs7/9+hPf/vb33/97e///39///5u///3Z+9/8kLO//vb33////////////9/f//5Sc9/9NVe//ND3v/z1F7/80Pe//TVXv/62t9//v7///nKX3/0VN7/8sNO//LDTv/01V7/80Pe//PUXv/7299////////////w==')";
				procButtonNum=General.NonQ(command,true);
				if(ProcedureCodes.IsValidCode("D2750")) {
					command="INSERT INTO procbuttonitem (ProcButtonNum,CodeNum,AutoCodeNum) VALUES ("+POut.PInt(procButtonNum)
						+","+ProcedureCodes.GetCodeNum("D2750")+",0)";
					General.NonQ(command);
				}
				if(ProcedureCodes.IsValidCode("N4118")) {
					command="INSERT INTO procbuttonitem (ProcButtonNum,CodeNum,AutoCodeNum) VALUES ("+POut.PInt(procButtonNum)
						+","+ProcedureCodes.GetCodeNum("N4118")+",0)";
					General.NonQ(command);
				}
				if(autoCodeNum!=0) {
					command="INSERT INTO procbuttonitem (ProcButtonNum,CodeNum,AutoCodeNum) VALUES ("+POut.PInt(procButtonNum)+",'',"
					+POut.PInt(autoCodeNum)+")";
					General.NonQ(command);
				}
				if(autoCodeNum2!=0) {
					command="INSERT INTO procbuttonitem (ProcButtonNum,CodeNum,AutoCodeNum) VALUES ("+POut.PInt(procButtonNum)+",'',"
					+POut.PInt(autoCodeNum2)+")";
					General.NonQ(command);
				}
			}
			//Bridge
			autoCodeNum=AutoCodes.GetNumFromDescript("Bridge");
			if(autoCodeNum!=0 || ProcedureCodes.IsValidCode("N4127")) {
				command="INSERT INTO procbutton (Description,ItemOrder,Category,ButtonImage) VALUES('Bridge',5,"
					+POut.PInt(category)+@",'')";
				procButtonNum=General.NonQ(command,true);
				if(ProcedureCodes.IsValidCode("N4127")) {
					command="INSERT INTO procbuttonitem (ProcButtonNum,CodeNum,AutoCodeNum) VALUES ("+POut.PInt(procButtonNum)
						+","+ProcedureCodes.GetCodeNum("N4127")+",0)";
					General.NonQ(command);
				}
				if(autoCodeNum!=0) {
					command="INSERT INTO procbuttonitem (ProcButtonNum,CodeNum,AutoCodeNum) VALUES ("+POut.PInt(procButtonNum)+",'',"
					+POut.PInt(autoCodeNum)+")";
					General.NonQ(command);
				}
			}
			//Build Up
			autoCodeNum=AutoCodes.GetNumFromDescript("BU/P&C");
			if(autoCodeNum!=0) {
				command="INSERT INTO procbutton (Description,ItemOrder,Category,ButtonImage) VALUES('BU/P&C',6,"
					+POut.PInt(category)+@",'')";
				procButtonNum=General.NonQ(command,true);
				command="INSERT INTO procbuttonitem (ProcButtonNum,CodeNum,AutoCodeNum) VALUES ("+POut.PInt(procButtonNum)+",'',"
					+POut.PInt(autoCodeNum)+")";
				General.NonQ(command);
			}
			//Exams/Cleanings Category--------------------------------------------------------------------------------------------
			command="INSERT INTO definition (Category,ItemOrder,ItemName,ItemValue,ItemColor,IsHidden) "
				+"VALUES (26,1,'Exams/Cleanings','',0,0)";
			category=General.NonQ(command,true);
			//PA
			if(ProcedureCodes.IsValidCode("D0220")) {
				command="INSERT INTO procbutton (Description,ItemOrder,Category,ButtonImage) VALUES('PA',0,"
					+POut.PInt(category)+@",'Qk12BgAAAAAAADYAAAAoAAAAFAAAABQAAAABACAAAAAAAAAAAADEDgAAxA4AAAAAAAAAAAAA////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////9/f3/0VFRf8DAwP/AwMD/wMDA/9dXV3/jIyM/yQkJP8DAwP/AwMD/4SEhP8LCwv/AwMD/wMDA/8DAwP/AwMD/wMDA/8DAwP/RUVF/97e3v+MjIz/AwMD/wMDA/8DAwP/AwMD/35+fv/FxcX/JCQk/wMDA/8DAwP/5ubm/5SUlP8DAwP/AwMD/wMDA/8DAwP/AwMD/wMDA/8DAwP/VVVV/wMDA/8DAwP/AwMD/wMDA/8LCwv/nJyc/+bm5v9dXV3/AwMD/wsLC/+MjIz/ZmZm/wMDA/8DAwP/AwMD/wMDA/8DAwP/AwMD/wMDA/8DAwP/AwMD/wMDA/8DAwP/AwMD/wsLC/+lpaX/xcXF/zQ0NP8DAwP/NDQ0/62trf9dXV3/JCQk/wMDA/8DAwP/AwMD/wMDA/8DAwP/AwMD/wMDA/8DAwP/AwMD/wMDA/8DAwP/TU1N/2ZmZv/Ozs7/dnZ2/wMDA/9dXV3/xcXF/+bm5v80NDT/AwMD/wMDA/8DAwP/AwMD/wMDA/8DAwP/AwMD/wMDA/8DAwP/AwMD/wMDA/9FRUX/lJSU/8XFxf+tra3/hISE/7W1tf/FxcX/tbW1/2ZmZv8DAwP/AwMD/wMDA/8DAwP/AwMD/wMDA/8DAwP/AwMD/wMDA/8DAwP/AwMD/01NTf/Ozs7/1tbW/+bm5v/Ozs7/7+/v/+bm5v/v7+//jIyM/yQkJP8DAwP/AwMD/wMDA/8DAwP/AwMD/wMDA/8DAwP/AwMD/wMDA/8DAwP/ZmZm/+bm5v//////5ubm/+/v7//v7+//5ubm//f39//W1tb/AwMD/wMDA/8DAwP/AwMD/wMDA/8DAwP/AwMD/wMDA/8DAwP/AwMD/wMDA/9mZmb////////////39/f/5ubm//f39///////9/f3/+bm5v80NDT/AwMD/wMDA/8DAwP/AwMD/wMDA/8DAwP/AwMD/wMDA/8DAwP/AwMD/1VVVf/39/f//////+/v7//m5ub/////////////////7+/v/yQkJP8DAwP/AwMD/wMDA/8DAwP/AwMD/wMDA/8DAwP/AwMD/wMDA/8DAwP/3t7e/+/v7//39/f/9/f3/9bW1v/m5ub////////////v7+//AwMD/wMDA/8DAwP/AwMD/wMDA/8DAwP/AwMD/wMDA/8DAwP/AwMD/wMDA/+EhIT/paWl/8XFxf//////3t7e/97e3v/v7+//5ubm/5ycnP8DAwP/AwMD/wMDA/8DAwP/AwMD/wMDA/8DAwP/TU1N/wsLC/8DAwP/AwMD/wMDA/89PT3/1tbW/87Ozv/FxcX/lJSU/35+fv+MjIz/dnZ2/wMDA/8DAwP/AwMD/wMDA/8DAwP/AwMD/2ZmZv//////XV1d/wMDA/8DAwP/AwMD/wMDA/8LCwv/AwMD/wMDA/8DAwP/AwMD/wMDA/8DAwP/AwMD/wMDA/8DAwP/AwMD/wMDA/9mZmb//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////w==')";
				procButtonNum=General.NonQ(command,true);
				command="INSERT INTO procbuttonitem (ProcButtonNum,CodeNum,AutoCodeNum) VALUES ("+POut.PInt(procButtonNum)
						+","+ProcedureCodes.GetCodeNum("D0220")+",0)";
				General.NonQ(command);
			}
			//NewChildExam







		}


		
	}

}










