using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace employees_Managment
{
    public partial class Payroll : UserControl
    {
        public Payroll()
        {
            InitializeComponent();
            string sql = "SELECT e.Name, CONCAT('$', ROUND((SUM(TIMESTAMPDIFF(MINUTE, a.TimeIn, CASE WHEN a.TimeOut < a.TimeIn THEN ADDTIME(a.TimeOut, '24:00:00') ELSE a.TimeOut END))/60)*e.Salary, 1)) as payroll, ROUND((SUM(TIMESTAMPDIFF(MINUTE, a.TimeIn, CASE WHEN a.TimeOut < a.TimeIn THEN ADDTIME(a.TimeOut, '24:00:00') ELSE a.TimeOut END))/60)) as total_Hour\r\nFROM employee e \r\nJOIN attendance a ON e.ID = a.EmployeeID GROUP BY e.Name;";
            MySqlConnection conn = DbEmployee.GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            adp.Fill(tbl);
            dataGridView1.DataSource = tbl;
            conn.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            DbEmployee.DisplayAndSearch("SELECT e.Name, CONCAT('$', ROUND((SUM(TIMESTAMPDIFF(MINUTE, a.TimeIn, CASE WHEN a.TimeOut < a.TimeIn THEN ADDTIME(a.TimeOut, '24:00:00') ELSE a.TimeOut END))/60)*e.Salary, 1)) as payroll, ROUND((SUM(TIMESTAMPDIFF(MINUTE, a.TimeIn, CASE WHEN a.TimeOut < a.TimeIn THEN ADDTIME(a.TimeOut, '24:00:00') ELSE a.TimeOut END))/60)) as total_Hour\r\nFROM employee e \r\nJOIN attendance a ON e.ID = a.EmployeeID GROUP BY e.Name having Name LIKE '%" + txtSearch.Text + "%'", dataGridView1);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
