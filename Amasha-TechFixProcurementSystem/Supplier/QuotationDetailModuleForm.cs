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
    public partial class QuotationDetailModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-AGH0AS2;Initial Catalog=dbAmashaTechFix;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        int qty = 0;

        public QuotationDetailModuleForm()
        {
            InitializeComponent();
            LoadSupplier();
            LoadProduct();
            LoadQuotation();
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
                dgvQuotation.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());
            }
            dr.Close();
            con.Close();
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void btnCreateQuotDetail_Click(object sender, EventArgs e)
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
                if (MessageBox.Show("Are you sure you want to create this record?", "Creating Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    cm = new SqlCommand("INSERT INTO tbQuotationDetail(quotationid, pid, pname, sid, sname, price, quantity, discount, total, qrdate, appdate)VALUES(@quotationid, @pid, @pname, @sid, @sname, @price, @quantity, @discount, @total, @qrdate, @appdate)", con);
                    cm.Parameters.AddWithValue("@quotationid", Convert.ToInt32(txtQid.Text));
                    cm.Parameters.AddWithValue("@pid", Convert.ToInt32(txtPid.Text));
                    cm.Parameters.AddWithValue("@pname", txtPName.Text);
                    cm.Parameters.AddWithValue("@sid", Convert.ToInt32(txtSId.Text));
                    cm.Parameters.AddWithValue("@sname", txtSName.Text);
                    cm.Parameters.AddWithValue("@price", Convert.ToInt32(txtPrice.Text));
                    cm.Parameters.AddWithValue("@quantity", Convert.ToInt32(Qty.Text));
                    cm.Parameters.AddWithValue("@discount", Convert.ToInt32(txtDiscount.Text));
                    cm.Parameters.AddWithValue("@total", Convert.ToInt32(txtTotal.Text));
                    cm.Parameters.AddWithValue("@qrdate", txtQuotReq.Text);
                    cm.Parameters.AddWithValue("@appdate", dtQuotDetailApp.Value);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Quotation Detail Record has been successfully saved.");

                    Clear();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            txtQuotReq.Text = dgvQuotation.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void txtSearchQuot_TextChanged(object sender, EventArgs e)
        {
            LoadQuotation();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            txtSId.Clear();
            txtSName.Clear();

            txtPid.Clear();
            txtPName.Clear();

            txtQid.Clear();

            txtPrice.Clear();
            txtDiscount.Clear();
            txtTotal.Clear();
            txtQuotReq.Clear();

            dtQuotDetailApp.Value = DateTime.Now;
        }

        private void Qty_ValueChanged(object sender, EventArgs e)
        {
            GetQty();
            if (Convert.ToInt16(Qty.Value) > qty)
            {
                MessageBox.Show("Instock quantity is not enough!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Qty.Value = Qty.Value - 1;
                return;
            }
            if (Convert.ToInt16(Qty.Value) > 0)
            {
                int total = Convert.ToInt16(txtPrice.Text) * Convert.ToInt16(Qty.Value);
                txtTotal.Text = total.ToString();
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

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void txtPid_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtQuotReq_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtQid_TextChanged(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtPName_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblOid_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void txtSId_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtQuotDetailApp_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
