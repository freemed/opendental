using System;
using System.Collections.Generic;
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
            return Regex.IsMatch(arch,".*64.*");
        }

    }
}
