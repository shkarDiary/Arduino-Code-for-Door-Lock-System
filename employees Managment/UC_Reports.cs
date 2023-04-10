using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace employees_Managment
{
    public partial class UC_Reports : UserControl
    {
        public UC_Reports()
        {
            InitializeComponent();
            display();
        }

        private void UC_Reports_Load(object sender, EventArgs e)
        {

        }
        private void search()
        {
            
            
        }
        public void display()
        {
            String toDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            String fromDate = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            Console.Write(toDate);
            string sql = "SELECT e.name, e.id, a.date, TIMESTAMPDIFF(HOUR, a.Timein, a.timeout) + (CASE WHEN TIME(a.Timein) <= TIME(a.timeout) THEN 0 ELSE 24 END) AS Hour_Worked ,a.timein , a.timeout FROM Employee e JOIN Attendance a ON e.id = a.EmployeeID WHERE a.date BETWEEN @FROM AND @TO GROUP BY e.name;";
            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.Add("@FROM", MySqlDbType.Date).Value = fromDate;
            cmd.Parameters.Add("@TO", MySqlDbType.Date).Value = toDate;
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            adp.Fill(tbl);
            dataGridView1.DataSource = tbl;
            conn.Close();
        }
        public static MySqlConnection GetConnection()
        {
            String sql = "datasource=localhost;port=3306;username=root;password=;database=iot";
            MySqlConnection conn = new MySqlConnection(sql);
            try
            {
                conn.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("MYSQL Connection! \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return conn;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            display();
        }


        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
