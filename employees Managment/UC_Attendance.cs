using Attendances_Managment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Mysqlx.Datatypes.Scalar.Types;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace employees_Managment
{
    public partial class UC_Attendance : UserControl
    {
        public UC_Attendance(bool opened)
        {
            InitializeComponent();
            if (!opened)
            {
                serialPort1.Open();
            }

            Display();
        }
        public void Display()
        {
            try
            {
                if (dataGridView1.InvokeRequired)
                {
                    dataGridView1.Invoke(new Action(Display));
                }
                else
                {
                    DBAttendance.DisplayAndSearch("SELECT * FROM attendance;", dataGridView1);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
    }
    private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        try{ 
            string incomeString = serialPort1.ReadLine();
            string myStringWithoutLastChar = incomeString.Substring(0, incomeString.Length - 1);
            int stringLength = incomeString.Length;
            if (stringLength <= 3)
            {
                if (incomeString.Trim().Equals("9"))
                {
                    return;
                }
                if (DBAttendance.updateAttendance(incomeString.Replace(" ", "")) == 0)
                {
                    DBAttendance.AddattendanceFinger(incomeString.Replace(" ", ""));
                   }
                    Display();


                }
            else if (stringLength >= 8)
            {
                if (DBAttendance.updateAttendanceByrfid(myStringWithoutLastChar.Replace(" ", "")) == 0)
                {
                    DBAttendance.AddattendanceRFID(myStringWithoutLastChar.Replace(" ", ""));
                }
                Console.WriteLine(incomeString.Replace(" ", ""));
                Display();
            }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        private void UC_Attendance_Load(object sender, EventArgs e)
        {
        }

        private void UC_Attendance_Leave(object sender, EventArgs e)
        {

        }
    }

}

