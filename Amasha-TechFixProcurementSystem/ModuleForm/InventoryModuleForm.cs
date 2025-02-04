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

namespace Amasha_TechFixProcurementSystem.ModuleForm
{
    public partial class InventoryModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-AGH0AS2;Initial Catalog=dbAmashaTechFix;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public InventoryModuleForm()
        {
            InitializeComponent();
            LoadSupplier();
            LoadProduct();
            LoadQuotation();
            LoadQuotationDetail();
        }
        public void LoadSupplier()
        {
            int i = 0;
            dgvSupplier.Rows.Clear();
            cm = new SqlCommand("SELECT sid, sname FROM tbSupplier WHERE CONCAT(sid,sname) LIKE '%" + txtSearchSup.Text + "%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvSupplier.Rows.Add(i, dr[0].ToString(), dr[1].ToString());
            }
            dr.Close();
            con.Close();
        }
        public void LoadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbProduct WHERE CONCAT(pid, pname, pqty) LIKE '%" + txtSearchProd.Text + "%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvProduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString());
            }
            dr.Close();
            con.Close();

        }

        public void LoadQuotation()
        {
            int i = 0;
            dgvQuotation.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbQuotation WHERE CONCAT(quotationid, qrdate,pid,pname,sid,sname, qty) LIKE '%" + txtSearchQuot.Text + "%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvQuotation.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(),dr[5].ToString(),dr[6].ToString());
            }
            dr.Close();
            con.Close();

        }

        public void LoadQuotationDetail()
        {
            int i = 0;
            dgvQuotationDetail.Rows.Clear();
            cm = new SqlCommand("SELECT quotationdetailid, D.quotationid, D.pid, P.pname, D.sid, S.sname, price, quantity, discount, total, Q.qrdate, appdate  FROM tbQuotationDetail AS D JOIN tbSupplier AS S ON D.sid=S.sid JOIN tbProduct AS P ON D.pid=P.pid JOIN tbQuotation AS Q ON D.quotationid=Q.quotationid WHERE CONCAT (quotationdetailid, D.quotationid, D.pid, P.pname, D.sid, S.sname, price, quantity, discount, total, Q.qrdate, appdate) LIKE '%" + txtSearchQuotDet.Text + "%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvQuotationDetail.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString(), dr[9].ToString(), dr[10].ToString(), Convert.ToDateTime(dr[11].ToString()).ToString("dd/MM/yyyy"));

            }
            dr.Close();
            con.Close();

        }


        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtPid.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtPName.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtPQty.Text = dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dgvSupplier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtSId.Text = dgvSupplier.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtSName.Text = dgvSupplier.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void txtSearchSup_TextChanged(object sender, EventArgs e)
        {
            LoadSupplier();
        }

        private void txtSearchProd_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }

        private void dgvQuotation_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtQid.Text = dgvQuotation.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtUDQty.Text = dgvQuotation.Rows[e.RowIndex].Cells[7].Value.ToString();
        }

        private void dgvQuotationDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtQDid.Text = dgvQuotationDetail.Rows[e.RowIndex].Cells[1].Value.ToString();
            dtInventoryUpdated.Text = dgvQuotationDetail.Rows[e.RowIndex].Cells[12].Value.ToString();
        }
        private void txtSearchQuot_TextChanged(object sender, EventArgs e)
        {
            LoadQuotation();
        }
        public void Clear()
        {
            txtSId.Clear();
            txtSName.Clear();

            txtPid.Clear();
            txtPName.Clear();
            txtPQty.Clear();

            txtQid.Clear();
            txtUDQty.Clear();
            txtTotQty.Clear();

            txtQDid.Clear();
            dtInventoryUpdated.Clear();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnSaveInventoryRecord_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSId.Text == "")
                {
                    MessageBox.Show("Please select supplier!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtPid.Text == "")
                {
                    MessageBox.Show("Please select product!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtQid.Text == "")
                {
                    MessageBox.Show("Please select quotation!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtQDid.Text == "")
                {
                    MessageBox.Show("Please select quotation detail!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Are you sure you want to save this record?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    cm = new SqlCommand("INSERT INTO tbInventories(pid,pname, sid,sname, pqty, quotationid, qty, quotationdetailid, appdate, totqty)VALUES(@pid, @pname, @sid,@sname,@pqty,@quotationid, @qty, @quotationdetailid,  @appdate, @totqty)", con);
                    cm.Parameters.AddWithValue("@pid", Convert.ToInt32(txtPid.Text));
                    cm.Parameters.AddWithValue("@pname", txtPName.Text);
                    cm.Parameters.AddWithValue("@sid", Convert.ToInt32(txtSId.Text));
                    cm.Parameters.AddWithValue("@sname", txtSName.Text);
                    cm.Parameters.AddWithValue("@pqty", Convert.ToInt32(txtPQty.Text));
                    cm.Parameters.AddWithValue("@quotationid", Convert.ToInt32(txtQid.Text));
                    cm.Parameters.AddWithValue("@qty", Convert.ToInt32(txtUDQty.Text));
                    cm.Parameters.AddWithValue("@quotationdetailid", Convert.ToInt32(txtQDid.Text));
                    cm.Parameters.AddWithValue("@appdate", dtInventoryUpdated.Text);
                    cm.Parameters.AddWithValue("@totqty", Convert.ToInt32(txtTotQty.Text));
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Inventory Record has been successfully saved.");

                    Clear();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
