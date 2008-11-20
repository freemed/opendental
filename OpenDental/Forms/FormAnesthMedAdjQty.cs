using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;
using System.Text.RegularExpressions;
using OpenDental.DataAccess;

namespace OpenDental 
{
    public partial class FormAnesthMedAdjQty : Form
    {
        string notes, anesthmedname, howsupplied, qty, adj, num;
        int i, newQty, oldQty, newAdj, num2 = 0, qtyOnHand = 0,rownumber;
        Boolean flag = false;
        public AnesthMedsInventoryAdj MedInvAdj;

        public FormAnesthMedAdjQty(string medname, string howsupplied, int qty,int rownum)
        {
            InitializeComponent();
            textAnesthMedName.Text = medname;
            textAnesthHowSupplied.Text = howsupplied;
            textBoxqty.Text =Convert.ToString(qty);
            rownumber = rownum;
        }
        private void butOK_Click(object sender, EventArgs e)
        {
            anesthmedname = textAnesthMedName.Text;
            howsupplied = textAnesthHowSupplied.Text;
            qty = textBoxqty.Text;

            int qty2 = 0;
            if (qty != "" && qty != null)
            {
                qty2 = Convert.ToInt32(qty);
            }
            if (qty != "" && qty != null)
            {
                qtyOnHand = 0;
                if (qty != "" && qty != null)
                    qtyOnHand = Convert.ToInt32(qty);
                adj = textBoxadj.Text;
                if(textBoxnotes.Text.Trim() == "")
                {
                    MessageBox.Show("Enter Notes.");
                    return;
                }
                if (textBoxadj.Text.Trim() == "")
                {
                    MessageBox.Show("Enter The Value In Quantity Adj Field.");
                    return;
                }
                if (adj != "")
                    {
                        Regex regex = new Regex("^-[0-9]+$|^[0-9]+$");
                        if (!regex.IsMatch(adj))
                        {
                            MessageBox.Show("Enter Valid adjustment quantity on hand");
                            return;
                        }
                        else
                        {
                            if (adj.Contains("-"))
                            {
                                if (adj.Length > 5)
                                {
                                    MessageBox.Show("Adjustment field accepts 4 digit integer");
                                    return;
                                }
                                num = adj.Substring(1);
                                num2 = Convert.ToInt32(num);
                                newQty = Convert.ToInt32(adj);
                                oldQty = Convert.ToInt32(qty);
                                newAdj = Convert.ToInt32(adj);
                            }
                            else if (adj.Length <= 4)
                            {
                                newQty = Convert.ToInt32(adj);
                                oldQty = Convert.ToInt32(qty);
                                newAdj = Convert.ToInt32(adj);
                            }
                            
                            else
                            {
                                oldQty = Convert.ToInt32(qty);
                                MessageBox.Show("Adjustment field accepts 4 digit integer");
                                return;
                                flag = true;
                            }
                            if (adj.Contains("-") && (num2 > oldQty))
                            {
                                MessageBox.Show("Adjustment field should be less than the Quantity on Hand.");
                                return;
                            }
                            else
                            {
                                notes = textBoxnotes.Text;
                                newQty = oldQty + (newAdj);
								MedInvAdj.AnestheticMedNum = AnesthMedsInventoryAdjs.getmedName(anesthmedname, howsupplied, Convert.ToInt32(qty));
                                AMedications.updateMed_adjRH(anesthmedname,howsupplied, qtyOnHand, adj, notes, oldQty,rownumber);
                                AnesthMedsInventoryAdjs.updateMed_adj_qty(anesthmedname,howsupplied,qtyOnHand,newQty); 
                                if (flag != true)
                                {
                                    this.Hide();
                                    FormAnestheticMedsAdjQtys FAMI = new FormAnestheticMedsAdjQtys();
                                    FAMI.ShowDialog();
                                }
                            }
                        }
                    }
                else
                    {
                        notes = textBoxnotes.Text;
                        AMedications.update(anesthmedname, howsupplied, qtyOnHand, notes, qty2,AnesthMedsInventoryAdjs.getmedName(anesthmedname, howsupplied,Convert.ToInt32(qty)));
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("You cannot adjust the quantity as Quantity on hand is empty.");
                    return;
                }
            }

        private void FormAdj_Load(object sender, EventArgs e)
        {
            MedInvAdj = AnesthMedsInventoryAdjs.GetLim();
            if (MedInvAdj == null)
            {
                for (int i = 0; i < AnesthMedsInventoryAdjs.List.Length; i++)
                {
                    MedInvAdj.AnestheticMedNum = AnesthMedsInventoryAdjs.List[i].AnestheticMedNum;
                    MedInvAdj.AdjPos = AnesthMedsInventoryAdjs.List[i].AdjPos;
                    MedInvAdj.AdjNeg = AnesthMedsInventoryAdjs.List[i].AdjNeg;
                    MedInvAdj.Provider = AnesthMedsInventoryAdjs.List[i].Provider;
                    MedInvAdj.Notes = AnesthMedsInventoryAdjs.List[i].Notes;
                    MedInvAdj.TimeStamp = AnesthMedsInventoryAdjs.List[i].TimeStamp;
                }
            }
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormAnestheticMedsAdjQtys FAMI = new FormAnestheticMedsAdjQtys();
            FAMI.ShowDialog();
        }
    }
}
