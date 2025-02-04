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
    public partial class OrderModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-AGH0AS2;Initial Catalog=dbAmashaTechFix;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        int qty = 0;
        public OrderModuleForm()
        {
            InitializeComponent();
            LoadProduct();
            LoadSupplier();
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
            cm = new SqlCommand("SELECT * FROM tbProduct WHERE CONCAT(pid, pname, pqty, pprice, pdescription, pcategory, psuppliername) LIKE '%" + txtSearchProd.Text + "%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvProduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());
            }
            dr.Close();
            con.Close();
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void btnOrder_Click(object sender, EventArgs e)
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
                if (MessageBox.Show("Are you sure you want to place this record?", "Creating Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    cm = new SqlCommand("INSERT INTO tbOrder(sid, sname, pid, pname, pprice, orderqty, ordertotal, date)VALUES(@sid, @sname, @pid, @pname, @pprice, @orderqty, @ordertotal, @date)", con);
                    cm.Parameters.AddWithValue("@sid", Convert.ToInt32(txtSId.Text));
                    cm.Parameters.AddWithValue("@sname", txtSName.Text);
                    cm.Parameters.AddWithValue("@pid", Convert.ToInt32(txtPid.Text));
                    cm.Parameters.AddWithValue("@pname", txtPName.Text);
                    cm.Parameters.AddWithValue("@pprice", Convert.ToInt32(txtPrice.Text));
                    cm.Parameters.AddWithValue("@orderqty", Convert.ToInt32(Qty.Text));
                    cm.Parameters.AddWithValue("@ordertotal", Convert.ToInt32(txtTotal.Text));
                    cm.Parameters.AddWithValue("@date", dtQuotDetailApp.Value);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Order has been successfully placed.");

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
        private void txtSearchSup_TextChanged(object sender, EventArgs e)
        {
            LoadSupplier();
        }
        private void txtSearchProd_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
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
            txtPrice.Clear();
            txtTotal.Clear();

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
        private void dgvProduct_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            txtPid.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtPName.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
        }
    }
}
