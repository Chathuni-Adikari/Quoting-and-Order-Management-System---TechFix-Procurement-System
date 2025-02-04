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
    public partial class InventoryForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-AGH0AS2;Initial Catalog=dbAmashaTechFix;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public InventoryForm()
        {
            InitializeComponent();
            LoadInventory();
        }

        public void LoadInventory()
        {

            int i = 0;
            dgvInevntory.Rows.Clear();
            cm = new SqlCommand("SELECT inventoryid, I.pid, P.pname, I.sid, S.sname, P.pqty, I.quotationid, Q.qty, I.quotationdetailid, D.appdate, totqty " +
                "FROM tbInventories AS I " +
                "JOIN tbSupplier AS S ON I.sid=S.sid JOIN tbProduct AS P ON I.pid=P.pid " +
                "JOIN tbQuotation AS Q ON I.quotationid=Q.quotationid " +
                "JOIN tbQuotationDetail AS D ON I.quotationdetailid=D.quotationdetailid WHERE CONCAT (inventoryid, I.pid, P.pname, I.sid, S.sname, P.pqty, I.quotationid, Q.qty, I.quotationdetailid, D.appdate, totqty) LIKE '%" + txtSearch.Text + "%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvInevntory.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString(), dr[9].ToString(), dr[10].ToString());
                
            }
            dr.Close();
            con.Close();

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadInventory();
        }

        private void dgvInevntory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvInevntory.Columns[e.ColumnIndex].Name;

            if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this inventory record?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbInventories WHERE inventoryid LIKE '" + dgvInevntory.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record has been successfully deleted!");

                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();

                }
            }
            LoadInventory();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            InventoryModuleForm moduleForm = new InventoryModuleForm();
            moduleForm.ShowDialog();
            LoadInventory();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            LoadInventory();
        }
    }
}
