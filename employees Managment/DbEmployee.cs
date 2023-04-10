using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace employees_Managment
{
    internal class DbEmployee
    {
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
        public static void AddEmploye(Employee emp)
        {
            String sql = "INSERT INTO employee VALUES (@id,@Name,@RFIDData,@Salary)";

            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.Add("@id", MySqlDbType.Int64).Value = emp.ID;
            cmd.Parameters.Add("@Name", MySqlDbType.VarChar).Value = emp.Name;
            cmd.Parameters.Add("@RFIDData", MySqlDbType.VarChar).Value = emp.RFID;
            cmd.Parameters.Add("@Salary", MySqlDbType.Int64).Value = emp.Salary;
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Add successfully \n", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Not insert \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conn.Close();
        }

        public static void UpdateEmploye(Employee emp, String id)
        {
            String sql = "UPDATE employee SET Name = @Name, RFIDData = @RFIDData,Salary = @Salary WHERE id = @id";

            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.Add("@id", MySqlDbType.Int64).Value = emp.ID;
            cmd.Parameters.Add("@Name", MySqlDbType.VarChar).Value = emp.Name;
            cmd.Parameters.Add("@RFIDData", MySqlDbType.VarChar).Value = emp.RFID;
            cmd.Parameters.Add("@Salary", MySqlDbType.Int64).Value = emp.Salary;
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Update successfully \n", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Not Update \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conn.Close();
        }

        public static void DeleteEmployee(String id)
        {
            String sql = "DELETE FROM employee WHERE id = @id";
            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = id;
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Delete successfully \n", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Not Delete \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conn.Close();

        }
        public static int getLastEmployeeId()
        {
            String sql = "SELECT COALESCE(MAX(id), 0) FROM employee; ";
            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            int result = (int)cmd.ExecuteScalar();
            conn.Close();
            return result;
        }

        public static void DisplayAndSearch(String query, DataGridView dgv)
        {
            String sql = query;
            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            adp.Fill(tbl);
            dgv.DataSource = tbl;
            conn.Close();
        }
        public static void totalHourWork()
        {
            string sql = "SELECT e.Name, SUM(TIMESTAMPDIFF(MINUTE, a.TimeIn, CASE WHEN a.TimeOut < a.TimeIn THEN ADDTIME(a.TimeOut, '24:00:00') ELSE a.TimeOut END))/60 as total_minutes FROM employee e JOIN attendance a ON e.ID = a.EmployeeID GROUP BY e.Name;";

        } 
        public static void searchEmployeeByDate(String dateFrom, string dateTo)
        {
            string sql = "SELECT e.name, e.id FROM Employee e JOIN Attendance a ON e.id = a.EmployeeID WHERE a.date BETWEEN @FROM AND @TO GROUP BY e.name;";
            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.Add("@FROM", MySqlDbType.Date).Value = dateFrom;
            cmd.Parameters.Add("@TO", MySqlDbType.Date).Value = dateTo;
        }

    }
}
