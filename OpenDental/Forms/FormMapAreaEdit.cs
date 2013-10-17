using System;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormMapAreaEdit:Form {

		///<summary>The item being edited</summary>
		public MapArea MapItem;

		public FormMapAreaEdit() {
			InitializeComponent();
		}

		private void FormMapAreaEdit_Load(object sender,EventArgs e) {
			//show/hide fields according to MaptItemType
			textBoxExtension.Visible=MapItem.ItemType==MapItemType.Room;
			labelExtension.Visible=MapItem.ItemType==MapItemType.Room;
			textBoxHeightFeet.Visible=MapItem.ItemType==MapItemType.Room;
			labelHeight.Visible=MapItem.ItemType==MapItemType.Room;
			labelDescription.Text=MapItem.ItemType==MapItemType.Room?"Description (shown when extension is 0)":"Text";
			//populate the fields
			textBoxXPos.Text=MapItem.XPos.ToString();
			textBoxYPos.Text=MapItem.YPos.ToString();
			textBoxExtension.Text=MapItem.Extension.ToString();
			textBoxWidthFeet.Text=MapItem.Width.ToString();
			textBoxHeightFeet.Text=MapItem.Height.ToString();
			textBoxDescription.Text=MapItem.Description.ToString();
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=System.Windows.Forms.DialogResult.Cancel;
		}

		private void butOK_Click(object sender,EventArgs e) {
			try {
				if(PIn.Double(textBoxXPos.Text)<0) {
					textBoxXPos.Focus();
					throw new Exception("Invalid XPos");
				}
				if(PIn.Double(textBoxYPos.Text)<0) {
					textBoxYPos.Focus();
					throw new Exception("Invalid YPos");
				}
				if(PIn.Double(textBoxWidthFeet.Text)<=0) {
					textBoxWidthFeet.Focus();
					throw new Exception("Invalid Width");
				} 
				if(PIn.Double(textBoxHeightFeet.Text)<=0) {
					textBoxHeightFeet.Focus();
					throw new Exception("Invalid Height");
				}
				if(PIn.Int(textBoxExtension.Text)<0) {
					textBoxExtension.Focus();
					throw new Exception("Invalid Extension");
				}
				MapItem.Extension=PIn.Int(textBoxExtension.Text);
				MapItem.XPos=PIn.Double(textBoxXPos.Text);
				MapItem.YPos=PIn.Double(textBoxYPos.Text);
				MapItem.Width=PIn.Double(textBoxWidthFeet.Text);
				MapItem.Height=PIn.Double(textBoxHeightFeet.Text);
				MapItem.Description=PIn.String(textBoxDescription.Text);
				if(MapItem.IsNew) {
					MapAreas.Insert(MapItem);
				}
				else {
					MapAreas.Update(MapItem);
				}
				DialogResult=System.Windows.Forms.DialogResult.OK;
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(MapItem.IsNew) {
				return;
			}
			if(MessageBox.Show("Remove cubicle?","",MessageBoxButtons.YesNo)!=DialogResult.Yes) {
				return;
			}
			MapAreas.Delete(MapItem.MapAreaNum);
			DialogResult=System.Windows.Forms.DialogResult.OK;
		}
	}
}
