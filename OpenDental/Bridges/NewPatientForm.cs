using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using OpenDentBusiness;

namespace OpenDental.Bridges
{
		///<summary></summary>
    public partial class NewPatientForm : Form
    {

        private string sURL = "";
				///<summary></summary>
        public NewPatientForm()
        {
            InitializeComponent();

        }


        private void AddResults(string sResult)
        {
            txtResults.Text = sResult + "\r\n" + txtResults.Text;
            txtResults.Refresh();
        }

        private void NewPatientForm_Load(object sender, EventArgs e)
        {

        }
				///<summary></summary>
        public void ShowDownload(string sURLValue)
        {
            sURL = sURLValue;
            this.Show();
        }

        private void btnImportForms_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            
            XmlDocument x = new XmlDocument();
            AddResults("Downloading new patient list...");
            try
            {
                x.Load(sURL);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot access account.\r\n\r\nPlease make sure URL, username and password are correct.\r\n\r\nContact www.NewPatientForm.com for help.\r\n\r\n" + ex.Message);
                this.Close();
                return;
            }

            if (x.DocumentElement.ChildNodes.Count == 0)
            {
                MessageBox.Show("No new patients.");
                this.Close();
                return;
            }

            //Now that we have loaded all the new patient forms, loop through
            //each patient, import the xml and store the pdf file
            foreach (XmlNode ndeMessage in x.DocumentElement.ChildNodes)
            {
                string sPatientName = "";

                try
                {
                    sPatientName += ndeMessage.SelectSingleNode("PatientIdentification/NameLast").InnerText;
                }
                catch
                {
                    AddResults("No lastname found.");
                }

                try
                {
                    sPatientName += ", " + ndeMessage.SelectSingleNode("PatientIdentification/NameFirst").InnerText;
                }
                catch
                {
                    AddResults("No firstname found.");
                }

                if (MessageBox.Show("Do you want to import information for " + sPatientName + " ?", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {

                    AddResults("Adding patient " + sPatientName);

                    string sPDF = "";

                    try
                    {
                        sPDF = ndeMessage.SelectSingleNode("PatientIdentification/NewPatientForm").InnerText;
                    }
                    catch
                    {
                        AddResults("No pdf form found.");
                    }

                    //We have the encoded pdf in sPDF string so lets
                    //delete that node and try to import the patient
                    ndeMessage.SelectSingleNode("PatientIdentification").RemoveChild(ndeMessage.SelectSingleNode("PatientIdentification/NewPatientForm"));

                    FormImportXML frmX = new FormImportXML();
                    frmX.textMain.Text = ndeMessage.OuterXml;
                    frmX.butOK_Click(null, null);


                    //The patient info is entered, let's save the pdf document to the images folder

                    try
                    {

                        //We'll be working with a document

                        //First make sure we have a directory and
                        //everything is up to date
                        ContrDocs cd = new ContrDocs();

                        cd.RefreshModuleData(frmX.existingPatOld.PatNum);


                        Document DocCur = new Document();

                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        DocCur.FileName = ".pdf";
                        DocCur.DateCreated = DateTime.Today;

                        //Find the category, hopefully 'Patient Information'
                        //otherwise, just default to first one
                        int iCategory = iCategory = DefB.Short[(int)DefCat.ImageCats][0].DefNum; ;
                        for (int i = 0; i < DefB.Short[(int)DefCat.ImageCats].Length; i++)
                        {
                            if (DefB.Short[(int)DefCat.ImageCats][i].ItemName == "Patient Information")
                            {
                                iCategory = DefB.Short[(int)DefCat.ImageCats][i].DefNum;
                            }

                        }
                        DocCur.DocCategory = iCategory;
                        DocCur.ImgType = ImageType.Document;
                        DocCur.Description = "New Patient Form";
                        DocCur.WithPat = cd.PatCur.PatNum;
                        Documents.Insert(DocCur,cd.PatCur);//this assigns a filename and saves to db


                        try
                        {

                            // Convert the Base64 UUEncoded input into binary output.
                            byte[] binaryData = System.Convert.FromBase64String(sPDF);

                            // Write out the decoded data.
                            System.IO.FileStream outFile = new System.IO.FileStream(cd.patFolder + DocCur.FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                            outFile.Write(binaryData, 0, binaryData.Length);
                            outFile.Close();
                            //Above is the code to save the file to a particular directory from NewPatientForm.com
                        }
                        catch
                        {
                            MessageBox.Show(Lan.g(this, "Unable to write pdf file to disk."));
                            Documents.Delete(DocCur);
                        }



                    }
                    catch
                    {
                        AddResults("Could not save pdf file to patient's file.");
                    }

                    AddResults("Done writing pdf file to disk");

                }
                else
                {
                    AddResults("Cacelled import for " + sPatientName + ".");
                }


            }
            this.Cursor = Cursors.Default;
            MessageBox.Show("Import complete.\r\n\r\nIf any form imports were cancelled or unsuccessful\r\nthey will need to be imported manually.");

            btnImportForms.Enabled = false;
            //clear form instanciations
           
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}