using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.Forms;

namespace OpenDental {
	public partial class FormEhrSetup:Form {
		public FormEhrSetup() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormEhrSetup_Load(object sender,EventArgs e) {
			if(PrefC.GetBool(PrefName.EhrEmergencyNow)) {
				panelEmergencyNow.BackColor=Color.Red;
			}
			else {
				panelEmergencyNow.BackColor=SystemColors.Control;
			}
			if(!Security.IsAuthorized(Permissions.Setup,true)) {
				//Hide all the buttons except Emergency Now and Close.
				butICD9s.Visible=false;
				butAllergies.Visible=false;
				//Forumularies will now be checked through New Crop
				//butFormularies.Visible=false;
				butVaccineDef.Visible=false;
				butDrugManufacturer.Visible=false;
				butDrugUnit.Visible=false;
				butInboundEmail.Visible=false;
				butReminderRules.Visible=false;
				butEducationalResources.Visible=false;
				butRxNorm.Visible=false;
				butKeys.Visible=false;
				menuItemSettings.Visible=false;
			}
		}

		private void menuItemSettings_Click(object sender,EventArgs e) {
			FormEhrSettings FormES=new FormEhrSettings();
			FormES.ShowDialog();
		}

		private void butICD9s_Click(object sender,EventArgs e) {
			FormIcd9s FormE=new FormIcd9s();
			FormE.ShowDialog();
		}

		private void butAllergies_Click(object sender,EventArgs e) {
			FormAllergySetup FAS=new FormAllergySetup();
			FAS.ShowDialog();
		}

		//Formularies will now be checked through New Crop
		//private void butFormularies_Click(object sender,EventArgs e) {
		//	FormFormularies FormE=new FormFormularies();
		//	FormE.ShowDialog();
		//}

		private void butVaccineDef_Click(object sender,EventArgs e) {
			FormVaccineDefSetup FormE=new FormVaccineDefSetup();
			FormE.ShowDialog();
		}

		private void butDrugManufacturer_Click(object sender,EventArgs e) {
			FormDrugManufacturerSetup FormE=new FormDrugManufacturerSetup();
			FormE.ShowDialog();
		}

		private void butDrugUnit_Click(object sender,EventArgs e) {
			FormDrugUnitSetup FormE=new FormDrugUnitSetup();
			FormE.ShowDialog();
		}

		private void butInboundEmail_Click(object sender,EventArgs e) {
			FormEmailSetupEHR FormES=new FormEmailSetupEHR();
			FormES.ShowDialog();
		}

		private void butEmergencyNow_Click(object sender,EventArgs e) {
			if(PrefC.GetBool(PrefName.EhrEmergencyNow)) {
				panelEmergencyNow.BackColor=SystemColors.Control;
				Prefs.UpdateBool(PrefName.EhrEmergencyNow,false);
			}
			else {
				panelEmergencyNow.BackColor=Color.Red;
				Prefs.UpdateBool(PrefName.EhrEmergencyNow,true);
			}
			DataValid.SetInvalid(InvalidType.Prefs);
		}
		
		private void butReminderRules_Click(object sender,EventArgs e) {
			FormReminderRules FormRR = new FormReminderRules();
			FormRR.ShowDialog();
		}

		private void butEducationalResources_Click(object sender,EventArgs e) {
			FormEduResourceSetup FormEDUSetup = new FormEduResourceSetup();
			FormEDUSetup.ShowDialog();
		}

		private void butRxNorm_Click(object sender,EventArgs e) {
			FormRxNorms FormR=new FormRxNorms();
			FormR.ShowDialog();
		}

		private void butLoincs_Click(object sender,EventArgs e) {
			FormLoincs FormL=new FormLoincs();
			FormL.ShowDialog();
		}

		private void butSnomeds_Click(object sender,EventArgs e) {
			FormSnomeds FormS=new FormSnomeds();
			FormS.ShowDialog();
		}

//		private void butICD9CM31_Click(object sender,EventArgs e) {
//			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"This button does not perform error checking and only works from Ryan's computer.")) {
//				return;
//			}
//			Cursor=Cursors.WaitCursor;
//			try {
//				//Import ICD9-CM DX codes with long description----------------------------------------------------------------------------------------------------------
//				MsgBox.Show(this,"Importing ICD9-CM DX codes with long description.");
//				System.IO.StreamReader sr=new System.IO.StreamReader(@"C:\Users\Ryan\Desktop\ICD9CM31\DXL.txt");
//				string command=@"DROP TABLE IF EXISTS ICD9CMDX";
//				DataCore.NonQ(command);
//				command=@"CREATE TABLE `ICD9CMDX` (
//									`ICD9CMDXNum` BIGINT(20) NOT NULL AUTO_INCREMENT,
//									`CodeString` VARCHAR(255) DEFAULT '',
//									`DescriptionLong` VARCHAR(255) DEFAULT '',
//									`DescriptionShort` VARCHAR(255) DEFAULT '',
//									INDEX(CodeString),
//									PRIMARY KEY (`ICD9CMDXNum`)
//									) DEFAULT CHARSET=utf8;";
//				DataCore.NonQ(command);
//				string[] arrayICD9CM=new string[2];
//				string line="";
//				while(!sr.EndOfStream) {//each loop should read exactly one line of code.
//					line=sr.ReadLine();
//					arrayICD9CM[0]=line.Substring(0,5).Trim();//fixed width column
//					if(arrayICD9CM[0].Length>3){
//						arrayICD9CM[0]=arrayICD9CM[0].Substring(0,3)+"."+arrayICD9CM[0].Substring(3);
//					}
//					arrayICD9CM[1]=line.Substring(6);
//					command="INSERT INTO ICD9CMDX (CodeString,DescriptionLong) VALUES ('"+POut.String(arrayICD9CM[0])+"','"+POut.String(arrayICD9CM[1])+"')";
//					DataCore.NonQ(command);
//				}
//				//Import ICD9-CM DX codes with short description----------------------------------------------------------------------------------------------------------
//				MsgBox.Show(this,"Importing ICD9-CM DX codes with short description.");
//				sr.Dispose();
//				sr=new System.IO.StreamReader(@"C:\Users\Ryan\Desktop\ICD9CM31\DXS.txt");
//				while(!sr.EndOfStream) {//each loop should read exactly one line of code.
//					line=sr.ReadLine();
//					arrayICD9CM[0]=line.Substring(0,5).Trim();//fixed width column
//					if(arrayICD9CM[0].Length>3) {
//						arrayICD9CM[0]=arrayICD9CM[0].Substring(0,3)+"."+arrayICD9CM[0].Substring(3);
//					}
//					arrayICD9CM[1]=line.Substring(6);
//					command="UPDATE ICD9CMDX SET DescriptionShort='"+POut.String(arrayICD9CM[1])+"' WHERE CodeString='"+POut.String(arrayICD9CM[0])+"'";
//					DataCore.NonQ(command);
//				}
//				//Import ICD9-CM SG codes with long description----------------------------------------------------------------------------------------------------------
//				MsgBox.Show(this,"Importing ICD9-CM SG codes with long description.");
//				sr.Dispose();
//				sr=new System.IO.StreamReader(@"C:\Users\Ryan\Desktop\ICD9CM31\SGL.txt");
//				command=@"DROP TABLE IF EXISTS ICD9CMSG";
//				DataCore.NonQ(command);
//				command=@"CREATE TABLE `ICD9CMSG` (
//									`ICD9CMSGNum` BIGINT(20) NOT NULL AUTO_INCREMENT,
//									`CodeString` VARCHAR(255) DEFAULT '',
//									`DescriptionLong` VARCHAR(255) DEFAULT '',
//									`DescriptionShort` VARCHAR(255) DEFAULT '',
//									INDEX(CodeString),
//									PRIMARY KEY (`ICD9CMSGNum`)
//									) DEFAULT CHARSET=utf8;";
//				DataCore.NonQ(command);
//				while(!sr.EndOfStream) {//each loop should read exactly one line of code.
//					line=sr.ReadLine();
//					arrayICD9CM[0]=line.Substring(0,5).Trim();//fixed width column
//					if(arrayICD9CM[0].Length>2) {
//						arrayICD9CM[0]=arrayICD9CM[0].Substring(0,2)+"."+arrayICD9CM[0].Substring(2);
//					}
//					arrayICD9CM[1]=line.Substring(5);
//					command="INSERT INTO ICD9CMSG (CodeString,DescriptionLong) VALUES ('"+POut.String(arrayICD9CM[0])+"','"+POut.String(arrayICD9CM[1])+"')";
//					DataCore.NonQ(command);
//				}
//				//Import ICD9-CM SG codes with short description----------------------------------------------------------------------------------------------------------
//				MsgBox.Show(this,"Importing ICD9-CM SG codes with short description.");
//				sr.Dispose();
//				sr=new System.IO.StreamReader(@"C:\Users\Ryan\Desktop\ICD9CM31\SGS.txt");
//				while(!sr.EndOfStream) {//each loop should read exactly one line of code.
//					line=sr.ReadLine();
//					arrayICD9CM[0]=line.Substring(0,5).Trim();//fixed width column
//					if(arrayICD9CM[0].Length>2) {
//						arrayICD9CM[0]=arrayICD9CM[0].Substring(0,2)+"."+arrayICD9CM[0].Substring(2);
//					}
//					arrayICD9CM[1]=line.Substring(5);
//					command="UPDATE ICD9CMSG SET DescriptionShort='"+POut.String(arrayICD9CM[1])+"' WHERE CodeString='"+POut.String(arrayICD9CM[0])+"'";
//					DataCore.NonQ(command);
//				}
//			}
//			catch(Exception ex) {
//				MessageBox.Show(this,Lan.g(this,"Error importing ICD9CM codes:")+"\r\n"+ex.Message);
//			}
//			Cursor=Cursors.Default;
//		}

		private void butKeys_Click(object sender,EventArgs e) {
			FormEhrQuarterlyKeys formK=new FormEhrQuarterlyKeys();
			formK.ShowDialog();
		}

		private void button1_Click(object sender,EventArgs e) {
			FormCodeSystemsImport FormCSI=new FormCodeSystemsImport();
			FormCSI.ShowDialog();
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}


		

	}
}