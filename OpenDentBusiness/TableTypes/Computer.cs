using System;
using System.Collections;

namespace OpenDentBusiness{
	
	///<summary>Keeps track of the computers in an office.  The list will eventually become cluttered with the names of old computers that are no longer in service.  The old rows can be safely deleted.  Although the primary key is used in at least one table, this will probably be changed, and the computername will become the primary key.</summary>
	public class Computer{//
		///<summary>Primary key.</summary>
		public int ComputerNum;
		///<summary>Name of the computer.</summary>
		public string CompName;
		///<summary>Allows use to tell which computers are running.  All workstations record a heartbeat here at an interval of 3 minutes.  And when they shut down, they set this value to min.  So if the heartbeat is fairly fresh, then that's an accurate indicator of whether the computer is running.</summary>
		public DateTime LastHeartBeat;


	}

	

	



}









