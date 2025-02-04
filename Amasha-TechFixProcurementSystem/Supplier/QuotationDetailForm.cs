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

namespace Amasha_TechFixProcurementSystem.Supplier
{
    public partial class QuotationDetailForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-AGH0AS2;Initial Catalog=dbAmashaTechFix;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public QuotationDetailForm()
        {
            InitializeComponent();
            LoadQuotationDetail();
        }
        public void LoadQuotationDetail()
        {
            int i = 0;
            dgvQuotationDetail.Rows.Clear();
            cm = new SqlCommand("SELECT quotationdetailid, D.quotationid, D.pid, P.pname, D.sid, S.sname, price, quantity, discount, total, Q.qrdate, appdate  FROM tbQuotationDetail AS D JOIN tbSupplier AS S ON D.sid=S.sid JOIN tbProduct AS P ON D.pid=P.pid JOIN tbQuotation AS Q ON D.quotationid=Q.quotationid WHERE CONCAT (quotationdetailid, D.quotationid, D.pid, P.pname, D.sid, S.sname, price, quantity, discount, total, Q.qrdate, appdate) LIKE '%" + txtSearch.Text + "%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvQuotationDetail.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString(), dr[9].ToString(), dr[10].ToString(), Convert.ToDateTime(dr[11].ToString()).ToString("dd/MM/yyyy"));
            }
            dr.Close();
            con.Close();
            lblQty.Text = i.ToString();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            QuotationDetailModuleForm moduleForm = new QuotationDetailModuleForm();
            moduleForm.ShowDialog();
            LoadQuotationDetail();
        }
        private void dgvQuotationDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvQuotationDetail.Columns[e.ColumnIndex].Name;

            if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this record?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbQuotationDetail WHERE quotationdetailid LIKE '" + dgvQuotationDetail.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record has been successfully deleted!");

                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                }
            }
            LoadQuotationDetail();
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadQuotationDetail();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
