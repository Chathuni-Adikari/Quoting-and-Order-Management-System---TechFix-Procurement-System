using Amasha_TechFixProcurementSystem.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Amasha_TechFixProcurementSystem.Supplier
{
    public partial class SupplierForm : Form
    {
        public SupplierForm()
        {
            InitializeComponent();
        }
        //to show subform form in supplierform
        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelSupplier.Controls.Add(childForm);
            panelSupplier.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void btnInventory_Click(object sender, EventArgs e)
        {
            openChildForm(new InventoryForm());
        }
        private void btnReqQuot_Click(object sender, EventArgs e)
        {
            openChildForm(new QuotationForm());
        }
        private void btnCreateQuot_Click(object sender, EventArgs e)
        {
            openChildForm(new QuotationDetailForm());
        }
        private void btnHome_Click(object sender, EventArgs e)
        {
            MainForm main = new MainForm();
            this.Hide();
            main.ShowDialog();
        }
    }
}
