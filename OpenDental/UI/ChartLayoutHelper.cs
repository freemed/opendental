using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SparksToothChart;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental {
	public class ChartLayoutHelper {
		///<summary>This reduces the number of places where Programs.UsingEcwTight() is called.  This helps with organization.  All calls within this class must pass through here. </summary>
		private static bool UsingEcwTight() {
			return Programs.UsingEcwTight();
		}

		public static void Resize(ODGrid gridProg,Panel panelImages,Panel panelEcw,TabControl tabControlImages,Size ClientSize,ODGrid gridPtInfo) {
			if(ProgramC.HListIsNull()) {
				return;
			}
			if(UsingEcwTight()) {
				gridProg.Width=524;
				if(panelImages.Visible) {
					panelEcw.Height=tabControlImages.Top-panelEcw.Top+1-(panelImages.Height+2);
				}
				else {
					panelEcw.Height=tabControlImages.Top-panelEcw.Top+1;
				}
				return;
			}
			if(Programs.UsingOrion) {//full width
				gridProg.Width=ClientSize.Width-gridProg.Location.X-1;
			}
			else if(gridProg.Columns !=null && gridProg.Columns.Count>0) {
				int gridW=0;
				for(int i=0;i<gridProg.Columns.Count;i++) {
					gridW+=gridProg.Columns[i].ColWidth;
				}
				if(gridProg.Location.X+gridW+20 < ClientSize.Width) {//if space is big enough to allow full width
					gridProg.Width=gridW+20;
				}
				else {
					if(ClientSize.Width>gridProg.Location.X) {//prevents an error
						gridProg.Width=ClientSize.Width-gridProg.Location.X-1;
					}
				}
			}
			if(Programs.UsingOrion) {
				//gridPtInfo is up in the tabs and does not need to be resized.
			}
			else{
				gridPtInfo.Height=tabControlImages.Top-gridPtInfo.Top;
			}
		}

		public static void InitializeOnStartup(ContrChart contrChart,TabControl tabProc,ODGrid gridProg,Panel panelEcw,
			TabControl tabControlImages,Size ClientSize,
			ODGrid gridPtInfo,ToothChartWrapper toothChart,TextBox textTreatmentNotes,OpenDental.UI.Button butECWup,
			OpenDental.UI.Button butECWdown,TabPage tabPatInfo)
		{
			tabProc.SelectedIndex=0;
			tabProc.Height=259;
			if(UsingEcwTight()) {
				toothChart.Location=new Point(524+2,26);
				textTreatmentNotes.Location=new Point(524+2,toothChart.Bottom+1);
				textTreatmentNotes.Size=new Size(411,40);//make it a bit smaller than usual
				gridPtInfo.Visible=false;
				panelEcw.Visible=true;
				panelEcw.Location=new Point(524+2,textTreatmentNotes.Bottom+1);
				panelEcw.Size=new Size(411,tabControlImages.Top-panelEcw.Top+1);
				butECWdown.Location=butECWup.Location;//they will be at the same location, but just hide one or the other.
				butECWdown.Visible=false;
				tabProc.Location=new Point(0,28);
				gridProg.Location=new Point(0,tabProc.Bottom+2);
				gridProg.Height=ClientSize.Height-gridProg.Location.Y-2;
			}
			else {//normal:
				toothChart.Location=new Point(0,26);
				textTreatmentNotes.Location=new Point(0,toothChart.Bottom+1);
				textTreatmentNotes.Size=new Size(411,71);
				gridPtInfo.Visible=true;
				gridPtInfo.Location=new Point(0,textTreatmentNotes.Bottom+1);
				panelEcw.Visible=false;
				tabProc.Location=new Point(415,28);
				gridProg.Location=new Point(415,tabProc.Bottom+2);
				gridProg.Height=ClientSize.Height-gridProg.Location.Y-2;
			}
			if(Programs.UsingOrion) {
				textTreatmentNotes.Visible=false;
				contrChart.Controls.Remove(gridPtInfo);
				gridPtInfo.Visible=true;
				gridPtInfo.Location=new Point(0,0);
				gridPtInfo.Size=new Size(tabPatInfo.ClientSize.Width,tabPatInfo.ClientSize.Height);
				gridPtInfo.Anchor=AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
				tabPatInfo.Controls.Add(gridPtInfo);
				tabProc.SelectedTab=tabPatInfo;
				tabProc.Height=toothChart.Height-1;
				gridProg.Location=new Point(0,toothChart.Bottom+2);
				gridProg.HScrollVisible=true;
				gridProg.Height=ClientSize.Height-gridProg.Location.Y-2;
				gridProg.Width=ClientSize.Width-gridProg.Location.X-1;//full width
			}
			else {
				tabProc.TabPages.Remove(tabPatInfo);
			}
		}

		public static void SetGridProgWidth(ODGrid gridProg,Size ClientSize){
			if(UsingEcwTight()) {
				gridProg.Width=524;
			}
			if(Programs.UsingOrion) {//full width
				gridProg.Width=ClientSize.Width-gridProg.Location.X-1;
			}
			else if(gridProg.Columns !=null && gridProg.Columns.Count>0) {
				int gridW=0;
				for(int i=0;i<gridProg.Columns.Count;i++) {
					gridW+=gridProg.Columns[i].ColWidth;
				}
				if(gridProg.Location.X+gridW+20 < ClientSize.Width) {//if space is big enough to allow full width
					gridProg.Width=gridW+20;
				}
				else {
					gridProg.Width=ClientSize.Width-gridProg.Location.X-1;
				}
			}
		}

		public static void tabProc_MouseDown(int SelectedProcTab,ODGrid gridProg,TabControl tabProc,Size ClientSize,MouseEventArgs e) {
			if(Programs.UsingOrion) {
				return;//tabs never minimize
			}
			//selected tab will have changed, so we need to test the original selected tab:
			Rectangle rect=tabProc.GetTabRect(SelectedProcTab);
			if(rect.Contains(e.X,e.Y) && tabProc.Height>27) {//clicked on the already selected tab which was maximized
				tabProc.Height=27;
				tabProc.Refresh();
				gridProg.Location=new Point(tabProc.Left,tabProc.Bottom+1);
				gridProg.Height=ClientSize.Height-gridProg.Location.Y-2;
			}
			else if(tabProc.Height==27) {//clicked on a minimized tab
				tabProc.Height=259;
				tabProc.Refresh();
				gridProg.Location=new Point(tabProc.Left,tabProc.Bottom+1);
				gridProg.Height=ClientSize.Height-gridProg.Location.Y-2;
			}
			else {//clicked on a new tab
				//height will have already been set, so do nothing
			}
			SelectedProcTab=tabProc.SelectedIndex;
		}

		public static void GridPtInfoSetSize(ODGrid gridPtInfo,TabControl tabControlImages){
			if(!Programs.UsingOrion) {
				gridPtInfo.Height=tabControlImages.Top-gridPtInfo.Top;
			}
		}

	}

	//public enum ChartLayoutMode

}
