
using employees_Managment;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Attendances_Managment
{
    internal class DBAttendance
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
        public static void AddattendanceFinger(String id)
        {
            String sql = "INSERT INTO attendance (EmployeeID, Date, TimeIn) SELECT id, CURRENT_DATE, CURRENT_TIME FROM Employee WHERE id = @ID";

            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.Add("@ID", MySqlDbType.Int64).Value = id;
            Console.WriteLine(cmd.CommandText);
            try
            {
                Console.WriteLine(cmd.ExecuteNonQuery());

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Not insert \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conn.Close();
        }
        public static void AddattendanceRFID(String rfid)
        {
            String sql = "INSERT INTO attendance (EmployeeID, Date, TimeIn) SELECT id, CURRENT_DATE, CURRENT_TIME FROM Employee WHERE RFIDData = @RFID";

            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.Add("@RFID", MySqlDbType.String).Value = rfid;

            try
            {
                // Print the SQL query to the console
                Console.WriteLine("SQL query: " + sql);

                // Execute the SQL query and store the number of rows affected
                int rowsAffected = cmd.ExecuteNonQuery();

                // Print the number of rows affected to the console
                Console.WriteLine("Rows affected: " + rowsAffected);
            }
            catch (MySqlException ex)
            {
                // Print the error message to the console
                Console.WriteLine("Error: " + ex.Message);

                // Show a message box with the error message
                MessageBox.Show("Not insert \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Close the database connection
                conn.Close();
            }
        }

        public static int updateAttendance(String id)
        {
            String sql = "UPDATE attendance AS a JOIN (  SELECT MAX(AttendanceID) AS last_attendanceid, EmployeeID    FROM attendance where timeout='00:00:00'  GROUP BY EmployeeID HAVING EmployeeID = @ID ) AS b ON a.EmployeeID = b.EmployeeID AND a.AttendanceID = b.last_attendanceid SET a.timeout = NOW();";

            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.Add("@ID", MySqlDbType.Int64).Value = id;
            try
            {
                int r = cmd.ExecuteNonQuery();
                Console.WriteLine(r);
                return r;

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Not Update \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conn.Close();
            return 0;
        }
        public static int updateAttendanceByrfid(String id)
        {
            String sql = "UPDATE attendance AS a JOIN (  SELECT MAX(AttendanceID) AS last_attendanceid, EmployeeID  FROM attendance, employee as e where timeout='00:00:00'   GROUP BY EmployeeID  HAVING employeeId = (SELECT id FROM employee where RFIDData = @ID)) AS b ON a.EmployeeID = b.EmployeeID AND a.AttendanceID = b.last_attendanceid SET a.timeout = NOW();";

            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.Add("@ID", MySqlDbType.VarChar).Value = id;
            try
            {
                int r = cmd.ExecuteNonQuery();
                Console.WriteLine(r);
                return r;

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Not Update \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conn.Close();
            return 0;
        }


        public static void DisplayAndSearch(String query, DataGridView dgv)
        {
                DataTable tbl = new DataTable();
            try{
                String sql = query;
                MySqlConnection conn = GetConnection();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                adp.Fill(tbl);
                dgv.DataSource = tbl;
                //Console.Write(dgv);   a
                conn.Close();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message+"hhhhhhhhhhhhhhhhhh");
            }
        }

    }
}
