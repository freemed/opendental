using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text; 
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{

	///<summary></summary>
	public class Shared{

		///<summary></summary>
		public Shared(){
			
		}

		///<summary>Converts numbers to ordinals.  For example, 120 to 120th, 73 to 73rd.  Probably doesn't work too well with foreign language translations.  Used in the Birthday postcards.</summary>
		public static string NumberToOrdinal(int number){
			string str=number.ToString();
			string last=str.Substring(str.Length-1);
			switch(last){
				case "0":
				case "4":
				case "5":
				case "6":
				case "7":
				case "8":
				case "9":
					return str+"th";
				case "1":
					return str+"st";
				case "2":
					return str+"nd";
				case "3":
					return str+"rd";
			}
			return "";//will never happen
		}


	}

/*=================================Class DataValid=========================================
===========================================================================================*/

	///<summary>Handles a global event to keep local data synchronized.</summary>
	public class DataValid{

		///<summary></summary>
		public static event OpenDental.ValidEventHandler BecameInvalid;	

		/*
		///<summary>Triggers an event that causes a signal to be sent to all other computers telling them what kind of locally stored data needs to be updated.  Either supply a set of flags for the types, or supply a date if the appointment screen needs to be refreshed.  Yes, this does immediately refresh the local data, too.  The AllLocal override does all types except appointment date for the local computer only, such as when starting up.</summary>
		public static void SetInvalid(List<int> itypes){
			OnBecameInvalid(new OpenDental.ValidEventArgs(DateTime.MinValue,itypes,false,0));
		}*/

		///<summary>Triggers an event that causes a signal to be sent to all other computers telling them what kind of locally stored data needs to be updated.  Either supply a set of flags for the types, or supply a date if the appointment screen needs to be refreshed.  Yes, this does immediately refresh the local data, too.  The AllLocal override does all types except appointment date for the local computer only, such as when starting up.</summary>
		public static void SetInvalid(params InvalidType[] itypes){
			List<int> itypeList=new List<int>();
			for(int i=0;i<itypes.Length;i++){
				itypeList.Add((int)itypes[i]);
			}
			OnBecameInvalid(new OpenDental.ValidEventArgs(DateTime.MinValue,itypeList,false,0));
		}

		///<summary>Triggers an event that causes a signal to be sent to all other computers telling them what kind of locally stored data needs to be updated.  Either supply a set of flags for the types, or supply a date if the appointment screen needs to be refreshed.  Yes, this does immediately refresh the local data, too, except Appointments.  The AllLocal override does all types except appointment date for the local computer only, such as when starting up.</summary>
		public static void SetInvalid(DateTime date){
			List<int> itypeList=new List<int>();
			itypeList.Add((int)InvalidType.Date);
			OnBecameInvalid(new OpenDental.ValidEventArgs(date,itypeList,false,0));
		}

		///<summary>Triggers an event that causes a signal to be sent to all other computers telling them what kind of locally stored data needs to be updated.  Either supply a set of flags for the types, or supply a date if the appointment screen needs to be refreshed.  Yes, this does immediately refresh the local data, too.  The AllLocal override does all types except appointment date for the local computer only, such as when starting up.</summary>
		public static void SetInvalid(bool onlyLocal){
			List<int> itypeList=new List<int>();
			itypeList.Add((int)InvalidType.AllLocal);
			OnBecameInvalid(new OpenDental.ValidEventArgs(DateTime.MinValue,itypeList,true,0));
		}

		public static void SetInvalidTask(long taskNum,bool isPopup) {
			List<int> itypeList=new List<int>();
			if(isPopup){
				itypeList.Add((int)InvalidType.TaskPopup);
			}
			else{
				itypeList.Add((int)InvalidType.Task);
			}
			OnBecameInvalid(new OpenDental.ValidEventArgs(DateTime.MinValue,itypeList,false,taskNum));
		}

		///<summary></summary>
		protected static void OnBecameInvalid(OpenDental.ValidEventArgs e){
			if(BecameInvalid !=null){
				BecameInvalid(e);
			}
		}

	}

	///<summary></summary>
	public delegate void ValidEventHandler(ValidEventArgs e);

	///<summary></summary>
	public class ValidEventArgs : System.EventArgs{
		private DateTime dateViewing;
		private List<int> itypes;
		private bool onlyLocal;
		private long taskNum;
		
		///<summary></summary>
		public ValidEventArgs(DateTime dateViewing,List<int> itypes,bool onlyLocal,long taskNum)
			: base() {
			this.dateViewing=dateViewing;
			this.itypes=itypes;
			this.onlyLocal=onlyLocal;
			this.taskNum=taskNum;
		}

		///<summary></summary>
		public DateTime DateViewing{
			get{return dateViewing;}
		}

		///<summary></summary>
		public List<int> ITypes{
			get{return itypes;}
		}

		///<summary></summary>
		public bool OnlyLocal{
			get{return onlyLocal;}
		}

		///<summary></summary>
		public long TaskNum {
			get{return taskNum;}
		}

	}

	/*=================================Class GotoModule==================================================
	===========================================================================================*/

	///<summary>Used to trigger a global event to jump between modules and perform actions in other modules.  PatNum is optional.  If 0, then no effect.</summary>
	public class GotoModule{
		///<summary></summary>
		public static event ModuleEventHandler ModuleSelected;

		/*
		///<summary>This triggers a global event which the main form responds to by going directly to a module.</summary>
		public static void GoNow(DateTime dateSelected,Appointment pinAppt,int selectedAptNum,int iModule,int claimNum) {
			OnModuleSelected(new ModuleEventArgs(dateSelected,pinAppt,selectedAptNum,iModule,claimNum));
		}*/

		///<summary>Goes directly to an existing appointment.</summary>
		public static void GotoAppointment(DateTime dateSelected,long selectedAptNum) {
			OnModuleSelected(new ModuleEventArgs(dateSelected,new List<long>(),selectedAptNum,0,0,0,0));
		}

		///<summary>Goes directly to a claim in someone's Account.</summary>
		public static void GotoClaim(long claimNum) {
			OnModuleSelected(new ModuleEventArgs(DateTime.MinValue,new List<long>(),0,2,claimNum,0,0));
		}

		///<summary>Goes directly to an Account.  Sometimes, patient is selected some other way instead of being passed in here, so OK to pass in a patNum of zero.</summary>
		public static void GotoAccount(long patNum) {
			OnModuleSelected(new ModuleEventArgs(DateTime.MinValue,new List<long>(),0,2,0,patNum,0));
		}
		
		///<summary>Goes directly to Family module.  Sometimes, patient is selected some other way instead of being passed in here, so OK to pass in a patNum of zero.</summary>
		public static void GotoFamily(long patNum) {
			OnModuleSelected(new ModuleEventArgs(DateTime.MinValue,new List<long>(),0,1,0,patNum,0));
		}

		///<summary>Puts appointment on pinboard, then jumps to Appointments module.  Sometimes, patient is selected some other way instead of being passed in here, so OK to pass in a patNum of zero.</summary>
		public static void PinToAppt(List<long> pinAptNums,long patNum) {
			OnModuleSelected(new ModuleEventArgs(DateTime.Today,pinAptNums,0,0,0,patNum,0));
		}

		///<summary>Jumps to Images module and pulls up the specified image.</summary>
		public static void GotoImage(long patNum,long docNum) {
			OnModuleSelected(new ModuleEventArgs(DateTime.MinValue,new List<long>(),0,5,0,patNum,docNum));
		}

		///<summary></summary>
		protected static void OnModuleSelected(ModuleEventArgs e){
			if(ModuleSelected !=null){
				ModuleSelected(e);
			}
		}
	}

	///<summary>This is used for our global module events.</summary>
	public delegate void ModuleEventHandler(ModuleEventArgs e);

	///<summary></summary>
	public class ModuleEventArgs : System.EventArgs{
		private DateTime dateSelected;
		private List<long> pinAppts;
		private long selectedAptNum;
		private int iModule;
		private long claimNum;
		private long patNum;
		private long docNum;//image
		
		///<summary></summary>
		public ModuleEventArgs(DateTime dateSelected,List<long> pinAppts,long selectedAptNum,int iModule,
			long claimNum,long patNum,long docNum)
			: base()
		{
			this.dateSelected=dateSelected;
			this.pinAppts=pinAppts;
			this.selectedAptNum=selectedAptNum;
			this.iModule=iModule;
			this.claimNum=claimNum;
			this.patNum=patNum;
			this.docNum=docNum;
		}

		///<summary>If going to the ApptModule, this lets you pick a date.</summary>
		public DateTime DateSelected{
			get{return dateSelected;}
		}

		///<summary>The aptNums of the appointments that we want to put on the pinboard of the Apt Module.</summary>
		public List<long> PinAppts {
			get{return pinAppts;}
		}

		///<summary></summary>
		public long SelectedAptNum {
			get{return selectedAptNum;}
		}

		///<summary></summary>
		public int IModule{
			get{return iModule;}
		}

		///<summary>If going to Account module, this lets you pick a claim.</summary>
		public long ClaimNum {
			get{return claimNum;}
		}

		///<summary></summary>
		public long PatNum {
			get { return patNum; }
		}

		///<summary>If going to Images module, this lets you pick which image.</summary>
		public long DocNum {
			get { return docNum; }
		}
	}


	

}
