using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using CodeBase;

namespace OpenDentBusiness{
	public class Plugins {
		private static List<PluginContainer> PluginList;
		//public static bool Active=false;

		public static void LoadAllPlugins(Form host) {
			//No need to check RemotingRole; no call to db.
			PluginList=new List<PluginContainer>();
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return;//no plugins will load.  So from now on, we can assume a direct connection.
			}
			for(int i=0;i<ProgramC.Listt.Count;i++) {
				if(!ProgramC.Listt[i].Enabled) {
					continue;
				}
				if(ProgramC.Listt[i].PluginDllName=="") {
					continue;
				}
				string dllPath=ODFileUtils.CombinePaths(Application.StartupPath,ProgramC.Listt[i].PluginDllName);
				if(dllPath.Contains("[VersionMajMin]")) {
					Version vers=new Version(Application.ProductVersion);
					string dllPathWithVersion=dllPath.Replace("[VersionMajMin]",vers.Major.ToString()+"."+vers.Minor.ToString());
					dllPath=dllPath.Replace("[VersionMajMin]","");//now stripped clean
					if(File.Exists(dllPathWithVersion)){
						File.Copy(dllPathWithVersion,dllPath,true);
					}
				}
				if(!File.Exists(dllPath)) {
					continue;
				}
				PluginBase plugin=null;
				try {
					Assembly ass=Assembly.LoadFile(dllPath);
					string typeName=Path.GetFileNameWithoutExtension(dllPath)+".Plugin";
					Type type=ass.GetType(typeName);
					plugin=(PluginBase)Activator.CreateInstance(type);
					plugin.Host=host;
				}
				catch(Exception ex) {
					MessageBox.Show(ex.Message);
					continue;//don't add it to plugin list.
				}
				PluginContainer container=new PluginContainer();
				container.Plugin=plugin;
				container.ProgramNum=ProgramC.Listt[i].ProgramNum;
				PluginList.Add(container);
				//Active=true;
			}
		}

		///<summary>Will return true if a plugin implements this method, replacing the default behavior.</summary>
		public static bool HookMethod(object sender,string hookName,params object[] parameters) {
			for(int i=0;i<PluginList.Count;i++) {
				//if there are multiple plugins, we use the first implementation that we come to.
				if(PluginList[i].Plugin.HookMethod(sender,hookName,parameters)) {
					return true;
				}
			}
			return false;//no implementation was found
		}

		///<summary>Adds code without disrupting existing code.</summary>
		public static void HookAddCode(object sender,string hookName,params object[] parameters) {
			for(int i=0;i<PluginList.Count;i++) {
				if(PluginList[i].Plugin.HookAddCode(sender,hookName,parameters)) {
					return;
				}
			}
		}

		public static void LaunchToolbarButton(long programNum,long patNum) {
			for(int i=0;i<PluginList.Count;i++) {
				if(PluginList[i].ProgramNum==programNum) {
					PluginList[i].Plugin.LaunchToolbarButton(patNum);
					return;
				}
			}
		}


	}
}
