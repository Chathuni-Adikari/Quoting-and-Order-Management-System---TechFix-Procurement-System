using Amasha_TechFixProcurementSystem.ModuleForm;
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

namespace Amasha_TechFixProcurementSystem.Database
{
    public partial class QuotationForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-AGH0AS2;Initial Catalog=dbAmashaTechFix;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public QuotationForm()
        {
            InitializeComponent();
            LoadQuotation();
        }

        public void LoadQuotation()
        {
            
            int i = 0;
            dgvQuotation.Rows.Clear();
            cm = new SqlCommand("SELECT quotationid, qrdate, Q.pid, P.pname, Q.sid, S.sname, qty  FROM tbQuotation AS Q JOIN tbSupplier AS S ON Q.sid=S.sid JOIN tbProduct AS P ON Q.pid=P.pid WHERE CONCAT (quotationid, qrdate, Q.pid, P.pname, Q.sid, S.sname, qty) LIKE '%" + txtSearch.Text + "%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvQuotation.Rows.Add(i, dr[0].ToString(), Convert.ToDateTime(dr[1].ToString()).ToString("dd/MM/yyyy"), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());
                
            }
            dr.Close();
            con.Close();

            lblQty.Text = i.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            QuotationModuleForm moduleForm = new QuotationModuleForm();
            moduleForm.ShowDialog();
            LoadQuotation();
        }

        private void dgvQuotation_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvQuotation.Columns[e.ColumnIndex].Name;

            if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this quotation?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbQuotation WHERE quotationid LIKE '" + dgvQuotation.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record has been successfully deleted!");

                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();

                }
            }
            LoadQuotation();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadQuotation();
        }

        private void lblQty_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
