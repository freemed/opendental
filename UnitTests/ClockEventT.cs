using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;

namespace UnitTests {
	public class ClockEventT {

		public static long InsertWorkPeriod(long emp,DateTime start,DateTime stop) {
			ClockEvent ce=new ClockEvent();
			ce.ClockStatus=TimeClockStatus.Home;
			ce.EmployeeNum=emp;
			ce.TimeDisplayed1=start;
			ce.TimeEntered1=start;
			ce.TimeDisplayed2=stop;
			ce.TimeEntered2=stop;
			ce.ClockEventNum = ClockEvents.Insert(ce);
			ClockEvents.Update(ce);//Updates TimeDisplayed1 because it defaults to now().
			return ce.ClockEventNum;
		}

		public static long InsertBreak(long emp,DateTime start,int minutes) {
			ClockEvent ce=new ClockEvent();
			ce.ClockStatus=TimeClockStatus.Break;
			ce.EmployeeNum=emp;
			ce.TimeDisplayed1=start;
			ce.TimeEntered1=start;
			ce.TimeDisplayed2=start.AddMinutes(minutes);
			ce.TimeEntered2=start.AddMinutes(minutes);
			ce.ClockEventNum = ClockEvents.Insert(ce);
			ClockEvents.Update(ce);//Updates TimeDisplayed1 because it defaults to now().
			return ce.ClockEventNum;
		}


	}
}
