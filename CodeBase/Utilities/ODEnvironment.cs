using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeBase{
  public class ODEnvironment{

    public static bool Is64BitOperatingSystem(){
      string arch="";
      try{
          arch=Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
      }catch{
          //May fail if the environemnt variable is not present on the target machine (i.e. Unix).
      }
			bool retVal=Regex.IsMatch(arch,".*64.*");
			return retVal; 
    }

		///<summary>Will return true if the provided id matches the local computer name or a local IPv4 or IPv6 address. Will return false if id is 'localhost' or '127.0.0.1'. Returns false in all other cases.</summary>
		public static bool IdIsThisComputer(string id){
			id=id.ToLower();
			//Compare ID against the local host name.
			if(Environment.MachineName.ToLower()==id){
			  return true;
			}
			IPHostEntry iphostentry=Dns.GetHostEntry(Environment.MachineName);
			//Check against the local computer's IP addresses (does not include 127.0.0.1). Includes IPv4 and IPv6.
			foreach(IPAddress ipaddress in iphostentry.AddressList){
			  if(ipaddress.ToString()==id){
			    return true;
			  }
			}
			return false;
		}

  }
}
