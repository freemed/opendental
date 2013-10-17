using System;
using System.Windows.Forms;
using OpenDentBusiness;
using System.Collections.Generic;
using System.Drawing;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormMapSetup:Form {

		#region Init
		
		public FormMapSetup() {
			InitializeComponent();
		}

		private void FormMapSetup_Load(object sender,EventArgs e) {
			//Get latest prefs. We will use them to setup our clinic panel.
			Cache.Refresh(InvalidType.Prefs);
			//fill the employee list			
			FillEmployees();
			//set preview defaults
			this.numFloorHeightFeet.Value=this.mapAreaPanel.FloorHeightFeet;
			this.numFloorWidthFeet.Value=this.mapAreaPanel.FloorWidthFeet;
			this.numPixelsPerFoot.Value=this.mapAreaPanel.PixelsPerFoot;
			//map panel
			mapAreaPanel_MapAreaChanged(this,new EventArgs());
		}

		private void FillEmployees() {
			List<Employee> employees=new List<Employee>(Employees.ListShort);
			employees.Sort(new Employees.EmployeeComparer(Employees.EmployeeComparer.SortBy.ext));
			gridEmployees.BeginUpdate();
			gridEmployees.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Ext. - Name",0);
			col.TextAlign=HorizontalAlignment.Left;
			gridEmployees.Columns.Add(col);
			gridEmployees.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<employees.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(employees[i].PhoneExt+" - "+employees[i].FName+" "+employees[i].LName);
				row.Tag=employees[i];
				//row.ColorBackG=gridEmployees.Rows.Count%2==0?Color.LightGray:Color.White;
				gridEmployees.Rows.Add(row);
			}
			gridEmployees.EndUpdate();
		}
				
		private void mapAreaPanel_MapAreaChanged(object sender,EventArgs e) {
			mapAreaPanel.Clear(false);
			//All prefs are hard-coded for now. We will likely create a parent table which will hold these values in the event that we release this feature to customers.
			/*
			//Load clinic panel values from database.
			bool showGrid=false;
			if(bool.TryParse(PrefC.GetRaw("clinicMapPanelHQ.ShowGrid"),out showGrid)) {
				checkShowGrid.Checked=showGrid;
			}
			bool showOutline=false;
			if(bool.TryParse(PrefC.GetRaw("clinicMapPanelHQ.ShowOutline"),out showOutline)) {
				checkShowOutline.Checked=showOutline;
			}
			int floorWidthFeet=0;
			if(int.TryParse(PrefC.GetRaw("clinicMapPanelHQ.FloorWidthFeet"),out floorWidthFeet)) {
				numFloorWidthFeet.Value=floorWidthFeet;
			}
			int floorHeightFeet=0;
			if(int.TryParse(PrefC.GetRaw("clinicMapPanelHQ.FloorHeightFeet"),out floorHeightFeet)) {
				numFloorHeightFeet.Value=floorHeightFeet;
			}
			int pixelsPerFoot=0;
			if(int.TryParse(PrefC.GetRaw("clinicMapPanelHQ.PixelsPerFoot"),out pixelsPerFoot)) {
				numPixelsPerFoot.Value=pixelsPerFoot;
			}
			*/
			//fill the panel
			List<MapArea> clinicMapItems=MapAreas.Refresh();
			for(int i=0;i<clinicMapItems.Count;i++) {
				if(clinicMapItems[i].ItemType==MapItemType.Room) {
					mapAreaPanel.AddCubicle(clinicMapItems[i]);
				}
				else if(clinicMapItems[i].ItemType==MapItemType.DisplayLabel) {
					mapAreaPanel.AddDisplayLabel(clinicMapItems[i]);
				}
			}
		}

		///<summary>This function will NOT update the prefs table (for now). All pref values for this form are hard-coded until we decide to make this a cutsomer-facing feature.</summary>
		private void TryUpdatePref(string prefName,string newValue) {
			return;
			try {
				Prefs.UpdateRaw(prefName,newValue);
			}
			catch(Exception e) {
				MessageBox.Show("Updating preference table failed. Any changes you made were not saved.\r\n\r\n"+e.Message);
			}
		}

		#endregion

		#region Check Boxes

		private void checkShowGrid_CheckedChanged(object sender,EventArgs e) {
			mapAreaPanel.ShowGrid=checkShowGrid.Checked;
			if(sender!=null) { //Sender will be null on form load. Otherwise save the new value.
				TryUpdatePref("clinicMapPanelHQ.ShowGrid",mapAreaPanel.ShowGrid.ToString());
			}
		}

		private void checkShowOutline_CheckedChanged(object sender,EventArgs e) {
			mapAreaPanel.ShowOutline=checkShowOutline.Checked;
			if(sender!=null) { //Sender will be null on form load. Otherwise save the new value.
				TryUpdatePref("clinicMapPanelHQ.ShowOutline",mapAreaPanel.ShowOutline.ToString());
			}
		}

		#endregion

		#region Numerics

		private void numericFloorWidthFeet_ValueChanged(object sender,EventArgs e) {
			mapAreaPanel.FloorWidthFeet=(int)numFloorWidthFeet.Value;
			if(sender!=null) { //Sender will be null on form load. Otherwise save the new value.
				TryUpdatePref("clinicMapPanelHQ.FloorWidthFeet",mapAreaPanel.FloorWidthFeet.ToString());
			}
		}

		private void numericFloorHeightFeet_ValueChanged(object sender,EventArgs e) {
			mapAreaPanel.FloorHeightFeet=(int)numFloorHeightFeet.Value;
			if(sender!=null) { //Sender will be null on form load. Otherwise save the new value.
				TryUpdatePref("clinicMapPanelHQ.FloorHeightFeet",mapAreaPanel.FloorHeightFeet.ToString());
			}
		}

		private void numericPixelsPerFoot_ValueChanged(object sender,EventArgs e) {
			mapAreaPanel.PixelsPerFoot=(int)numPixelsPerFoot.Value;
			if(sender!=null) { //Sender will be null on form load. Otherwise save the new value.
				TryUpdatePref("clinicMapPanelHQ.PixelsPerFoot",mapAreaPanel.PixelsPerFoot.ToString());
			}
		}

		#endregion

		#region Menus

		private void newCubicleToolStripMenuItem_Click(object sender,EventArgs e) {
			FormMapAreaEdit FormEP=new FormMapAreaEdit();
			FormEP.MapItem=new MapArea();
			FormEP.MapItem.IsNew=true;
			FormEP.MapItem.Width=6;
			FormEP.MapItem.Height=6;
			FormEP.MapItem.ItemType=MapItemType.Room;
			PointF xy=GetXYFromMenuLocation(sender);
			FormEP.MapItem.XPos=Math.Round(xy.X,3);
			FormEP.MapItem.YPos=Math.Round(xy.Y,3);
			if(FormEP.ShowDialog(this)!=DialogResult.OK) {
				return;
			}
			mapAreaPanel_MapAreaChanged(this,new EventArgs());
		}

		private void newLabelToolStripMenuItem_Click(object sender,EventArgs e) {
			FormMapAreaEdit FormEP=new FormMapAreaEdit();
			FormEP.MapItem=new MapArea();
			FormEP.MapItem.IsNew=true;
			FormEP.MapItem.Width=6;
			FormEP.MapItem.Height=6;
			FormEP.MapItem.ItemType=MapItemType.DisplayLabel;
			PointF xy=GetXYFromMenuLocation(sender);
			FormEP.MapItem.XPos=Math.Round(xy.X,3);
			FormEP.MapItem.YPos=Math.Round(xy.Y,3);
			if(FormEP.ShowDialog(this)!=DialogResult.OK) {
				return;
			}
			mapAreaPanel_MapAreaChanged(this,new EventArgs());
		}

		#endregion
				
		#region Map Panel

		private void mapAreaPanel_MouseUp(object sender,MouseEventArgs e) {
			if(e.Button!=MouseButtons.Right) {
				return;
			}
			newCubicleToolStripMenuItem.Tag=e.Location;
			newLabelToolStripMenuItem.Tag=e.Location;
			menu.Show(mapAreaPanel,e.Location);	
		}

		private PointF GetXYFromMenuLocation(object sender) {
			PointF xy=PointF.Empty;
			if(sender!=null
				&& sender is ToolStripMenuItem 
				&& ((ToolStripMenuItem)sender).Tag!=null
				&& ((ToolStripMenuItem)sender).Tag is System.Drawing.Point) {
				xy=MapAreaPanel.ConvertScreenLocationToXY(((System.Drawing.Point)((ToolStripMenuItem)sender).Tag),mapAreaPanel.PixelsPerFoot);
			}
			return xy;
		}

		#endregion

		#region Grid

		private void gridEmployees_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			//edit this entry
			if(gridEmployees.Rows[e.Row].Tag==null || !(gridEmployees.Rows[e.Row].Tag is Employee)) {
				return;
			}
			Employee employee=(Employee)gridEmployees.Rows[e.Row].Tag;
			FormMapAreaEdit FormEP=new FormMapAreaEdit();
			FormEP.MapItem=new MapArea();
			FormEP.MapItem.IsNew=true;
			FormEP.MapItem.Width=6;
			FormEP.MapItem.Height=6;
			FormEP.MapItem.ItemType=MapItemType.Room;
			FormEP.MapItem.Extension=employee.PhoneExt;
			if(FormEP.ShowDialog(this)!=DialogResult.OK) {
				return;
			}
			mapAreaPanel_MapAreaChanged(this,new EventArgs());
		}
	
		#endregion

		#region Buttons

		private void butBuildFromPhoneTable_Click(object sender,EventArgs e) {
			if(MessageBox.Show("This action will clear all information from clinicmapitem table and recreated it from current phone table rows. Would you like to continue?","",MessageBoxButtons.YesNo)!=System.Windows.Forms.DialogResult.Yes) {
				return;
			}
			butClear_Click(this,new EventArgs());
			List<Phone> phones=Phones.GetPhoneList();
			int defaultSizeFeet=6;
			int row=1;
			int column=0;
			for(int i=0;i<78;i++) {
				if(row>=7) {
					if(++column>8) {
						column=3;
						row++;
					}
				}
				else {
					if(++column>10) {
						column=1;
						row++;
					}
					if(row==7) {
						column=3;
						//row=8;
					}
				}

				//Phone phone=phones[i];
				MapArea clinicMapItem=new MapArea();
				clinicMapItem.Description="";
				clinicMapItem.Extension=i; //phone.Extension;
				clinicMapItem.Width=defaultSizeFeet;
				clinicMapItem.Height=defaultSizeFeet;
				clinicMapItem.XPos=(1*column)+((column-1)*defaultSizeFeet);
				clinicMapItem.YPos=1+((row-1)*defaultSizeFeet);
				mapAreaPanel.AddCubicle(clinicMapItem);
				MapAreas.Insert(clinicMapItem);
			}
		}

		private void butClear_Click(object sender,EventArgs e) {
			if(MessageBox.Show("This will delete all items from database permanently. Are you sure you want to continue?","",MessageBoxButtons.YesNo)!=DialogResult.Yes) {
				return;
			}
			mapAreaPanel.Clear(true);
		}

		private void butAddRandomCubicle_Click(object sender,EventArgs e) {
			MapArea clinicMapItem=mapAreaPanel.AddRandomCubicle();
			MapAreas.Insert(clinicMapItem);
		}

		private void butClose_Click(object sender,EventArgs e) {
			this.Close();
		}

		#endregion		
	}
}
