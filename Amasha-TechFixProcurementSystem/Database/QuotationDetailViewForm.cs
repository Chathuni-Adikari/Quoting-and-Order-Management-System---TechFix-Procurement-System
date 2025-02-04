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
    public partial class QuotationDetailViewForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-AGH0AS2;Initial Catalog=dbAmashaTechFix;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public QuotationDetailViewForm()
        {
            InitializeComponent();
            LoadQuotationDetailView();
        }
        public void LoadQuotationDetailView()
        {
            int i = 0;
            dgvQuotationDetailView.Rows.Clear();
            cm = new SqlCommand("SELECT quotationdetailid, D.quotationid, D.pid, P.pname, D.sid, S.sname, price, quantity, discount, total, Q.qrdate, appdate  FROM tbQuotationDetail AS D JOIN tbSupplier AS S ON D.sid=S.sid JOIN tbProduct AS P ON D.pid=P.pid JOIN tbQuotation AS Q ON D.quotationid=Q.quotationid WHERE CONCAT (quotationdetailid, D.quotationid, D.pid, P.pname, D.sid, S.sname, price, quantity, discount, total, Q.qrdate, appdate) LIKE '%" + txtSearch.Text + "%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvQuotationDetailView.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString(), dr[9].ToString(), dr[10].ToString(), Convert.ToDateTime(dr[11].ToString()).ToString("dd/MM/yyyy"));
            }
            dr.Close();
            con.Close();
        }
        private void dgvQuotationDetailView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvQuotationDetailView.Columns[e.ColumnIndex].Name;

            if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this inventory record?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbQuotationDetail WHERE quotationdetailid LIKE '" + dgvQuotationDetailView.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record has been successfully deleted!");

                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                }
            }
            LoadQuotationDetailView();
        }
    }
}
