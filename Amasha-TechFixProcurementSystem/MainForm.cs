using Amasha_TechFixProcurementSystem.Database;
using Amasha_TechFixProcurementSystem.Logics;
using Amasha_TechFixProcurementSystem.ModuleForm;
using Amasha_TechFixProcurementSystem.Supplier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Amasha_TechFixProcurementSystem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        //to show subform form in mainform
        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelMain.Controls.Add(childForm);
            panelMain.Tag = childForm;

            childForm.BringToFront();
            childForm.Show();
        }
        private void btnUser_Click(object sender, EventArgs e)
        {
            openChildForm(new UserForm());
        }
        private void btnSupplier_Click(object sender, EventArgs e)
        {
            openChildForm(new Database.SupplierForm());
        }
        private void btnProduct_Click(object sender, EventArgs e)
        {
            openChildForm(new ProductForm());
        }
        private void btnQuotation_Click(object sender, EventArgs e)
        {
            openChildForm(new QuotationForm());
        }
        private void btnInventory_Click(object sender, EventArgs e)
        {
            openChildForm(new InventoryForm());
        }
        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }
        private void btnHome_Click(object sender, EventArgs e)
        {
            MainForm main = new MainForm();
            this.Hide();
            main.ShowDialog();
        }
        private void btnQuotationDetails_Click(object sender, EventArgs e)
        {
            openChildForm(new QuotationDetailViewForm());
        }
        private void btnOrders_Click(object sender, EventArgs e)
        {
            openChildForm(new OrderForm());
        }
    }
}
