using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental.Bridges{
	/// <summary></summary> Implement OWANDY
    public class Owandy
    {
        /// <summary></summary>
        public Owandy()
        {

        }
        // AAD External Call declaration for Owandy bridge (Start)
        /// <summary></summary>
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        /// <summary></summary>
        [DllImport("user32.dll")]
        public static extern Boolean IsWindow(IntPtr hWnd);
        /// <summary></summary>
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, Int32 wParam, String lParam);

        /// <summary></summary>
        //public IntPtr formHandle;
        /// <summary></summary>
        public static IntPtr hwndLink;
        /// <summary></summary>
        public const string szClass = "MjLinkWndClass";
        /// <summary></summary>
        public const int WM_SETTEXT = 0x000C;
        // AAD External Call declaration for Owandy bridge (nd)

        //static extern long SendMessage(long hWnd, long Msg, long wParam, string lParam);
        ///<summary>Launches the program using a combination of command line characters and the patient.Cur data.</summary>
        public static void SendData(Program ProgramCur, Patient pat)
        {   
            //ProgramProperties.GetForProgram();
            string info;
            if (pat != null)
            {
                info = "/P:" + pat.PatNum + "," + pat.LName + "," + pat.FName;
                // info = "/P:1,DEMO,Patient1";
                //Patient id can be any string format
                try
                {
                    //formHandle = Parent.Handle;
                    string argument;
                    argument = "C /LINK "; //+ formHandle;
                    System.Diagnostics.Process.Start(ProgramCur.Path, argument);
                    //Call ExecCommand
                    ExecCmd(info);

                }
                catch
                {
                    MessageBox.Show(ProgramCur.Path + " is not available, or there is an error in the command line options.");
                }
            }//if patient is loaded
            else
            {
                try
                {
                    Process.Start(ProgramCur.Path);//should start Owandy without bringing up a pt.
                }
                catch
                {
                    MessageBox.Show(ProgramCur.Path + " is not available.");
                }
            }
        }
        /// <summary></summary>
        public static void ExecCmd(string lpszCmdLine)
        {
            IntPtr lResp;
            if (IsWindow(hwndLink) == false)
                hwndLink = FindWindow(szClass, null);
            if (IsWindow(hwndLink) == true)
                lResp = SendMessage(hwndLink, WM_SETTEXT, 0, lpszCmdLine);
        }
    }    
		
}










