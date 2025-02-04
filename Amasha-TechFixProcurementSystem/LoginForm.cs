using Amasha_TechFixProcurementSystem.Supplier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Amasha_TechFixProcurementSystem
{
    public partial class LoginForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-AGH0AS2;Initial Catalog=dbAmashaTechFix;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public LoginForm()
        {
            InitializeComponent();
        }
        private void checkBoxPass_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPass.Checked == false)
                txtPass.UseSystemPasswordChar = true;
            else
                txtPass.UseSystemPasswordChar = false;
        }
        private void lblClear_Click(object sender, EventArgs e)
        {
            txtRole.Clear();
            txtPass.Clear();
        }
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Exit Application","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                cm = new SqlCommand("SELECT * FROM tbUsers WHERE userrole = @userrole AND password = @password", con);
                cm.Parameters.AddWithValue("@userrole", txtRole.Text);
                cm.Parameters.AddWithValue("@password", txtPass.Text);

                con.Open();
                dr = cm.ExecuteReader();

                if (dr.Read()) // Check if any row is returned
                {
                    string fullname = dr["fullname"].ToString();
                    string userrole = dr["userrole"].ToString();

                    MessageBox.Show("Welcome " + fullname + " (" + userrole + ")", "ACCESS GRANTED", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (userrole == "Admin")
                    {
                        MainForm main = new MainForm(); // Replace with Admin-specific form if applicable
                        this.Hide();
                        main.ShowDialog();
                    }
                    else if (userrole == "Supplier")
                    {
                        SupplierForm supplierForm = new SupplierForm(); // Replace with Supplier-specific form if applicable
                        this.Hide();
                        supplierForm.ShowDialog();
                    }
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Invalid user role or password!", "ACCESS DENIED", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                dr?.Close();
                con?.Close();
            }
        }

    }
}
