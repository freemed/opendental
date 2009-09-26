using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace OpenDentBusiness {
	public abstract class PluginBase {
		private Form host;
		
		///<summary>This will be a refrence to the main FormOpenDental so that it can be used by    the plugin if needed.  It is set once on startup, so it's a good place to put startup code.</summary>
		public virtual Form Host { 
			get { 
				return host; 
			}
			set {
				host=value; 
			}
		}

		///<summary>These types of hooks are designed to completely replace the existing functionality of specific methods.  They always belong at the top of a method.</summary>
		public virtual bool HookMethod(object sender,string methodName,params object[] parameters) {
			return false;//by default, no hooks are implemented.
		}

		///<summary>These types of hooks allow adding extra code in at some point without disturbing the existing code.</summary>
		public virtual bool HookAddCode(object sender,string hookName,params object[] parameters) {
			return false;
		}

		public virtual void LaunchToolbarButton(long patNum) {

		}

	}
}
