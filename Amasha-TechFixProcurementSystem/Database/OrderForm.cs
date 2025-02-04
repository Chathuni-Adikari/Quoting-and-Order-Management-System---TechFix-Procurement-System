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
    public partial class OrderForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-AGH0AS2;Initial Catalog=dbAmashaTechFix;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public OrderForm()
        {
            InitializeComponent();
            LoadOrder();
        }
        public void LoadOrder()
        {
            int i = 0;
            dgvOrder.Rows.Clear();
            cm = new SqlCommand("SELECT orderid, O.sid, S.sname, O.pid, P.pname, P.pprice, orderqty, ordertotal, date  FROM tbOrder AS O JOIN tbSupplier AS S ON O.sid=S.sid JOIN tbProduct AS P ON O.pid=P.pid WHERE CONCAT (orderid, O.sid, S.sname, O.pid, P.pname, P.pprice, orderqty, ordertotal, date) LIKE '%" + txtSearch.Text + "%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvOrder.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), Convert.ToDateTime(dr[8].ToString()).ToString("dd/MM/yyyy"));
            }
            dr.Close();
            con.Close();

            lblQty.Text = i.ToString();
        }
        private void dgvOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            OrderModuleForm moduleForm = new OrderModuleForm();
            moduleForm.ShowDialog();
            LoadOrder();
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadOrder();
        }
    }
}
