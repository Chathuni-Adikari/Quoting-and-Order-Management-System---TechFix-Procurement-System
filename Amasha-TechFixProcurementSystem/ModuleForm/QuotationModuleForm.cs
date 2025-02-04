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
    public partial class QuotationModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-AGH0AS2;Initial Catalog=dbAmashaTechFix;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        int qty = 0;
        public QuotationModuleForm()
        {
            InitializeComponent();
            LoadSupplier();
            LoadProduct();
        }
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
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
            cm = new SqlCommand("SELECT * FROM tbProduct WHERE CONCAT(pid, pname, pqty, pdescription, pcategory) LIKE '%" + txtSearchProd.Text + "%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvProduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());
            }
            dr.Close();
            con.Close();
        }
        private void txtSearchSup_TextChanged(object sender, EventArgs e)
        {
            LoadSupplier();
        }
        private void txtSearchProd_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }
        private void UDQty_ValueChanged(object sender, EventArgs e)
        {
            GetQty();
            if (Convert.ToInt16(UDQty.Value) > qty)
            {
                MessageBox.Show("Instock quantity is not enough!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                UDQty.Value = UDQty.Value - 1;
                return;
            }
        }
        public void GetQty()
        {
            cm = new SqlCommand("SELECT pqty FROM tbProduct WHERE pid='" + txtPid.Text + "'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                qty = Convert.ToInt32(dr[0].ToString());
            }
            dr.Close();
            con.Close();
        }
        private void dgvSupplier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtSId.Text = dgvSupplier.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtSName.Text = dgvSupplier.Rows[e.RowIndex].Cells[2].Value.ToString();
        }
        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtPid.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtPName.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
        }
        private void btnQuotReq_Click(object sender, EventArgs e)
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
                if (MessageBox.Show("Are you sure you want to request this quotation?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    cm = new SqlCommand("INSERT INTO tbQuotation(qrdate, pid, pname, sid, sname, qty)VALUES(@qrdate, @pid,@pname, @sid,@sname, @qty)", con);
                    cm.Parameters.AddWithValue("@qrdate", dtQuotreq.Value);
                    cm.Parameters.AddWithValue("@pid", Convert.ToInt32(txtPid.Text));
                    cm.Parameters.AddWithValue("@pname", txtPName.Text);
                    cm.Parameters.AddWithValue("@sid", Convert.ToInt32(txtSId.Text));
                    cm.Parameters.AddWithValue("@sname", txtSName.Text);
                    cm.Parameters.AddWithValue("@qty", Convert.ToInt32(UDQty.Value));
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Quotation request has been successfully placed.");


                    cm = new SqlCommand("UPDATE tbProduct SET pqty=(pqty-@pqty) WHERE pid LIKE '" + txtPid.Text + "' ", con);
                    cm.Parameters.AddWithValue("@pqty", Convert.ToInt16(UDQty.Value));

                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    Clear();
                    LoadProduct();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Clear()
        {
            txtSId.Clear();
            txtSName.Clear();
            txtPid.Clear();
            txtPName.Clear();
            UDQty.Value = 0;
            dtQuotreq.Value = DateTime.Now;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
        }
        private void txtSId_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
