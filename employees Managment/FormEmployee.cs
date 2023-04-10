using Attendances_Managment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace employees_Managment
{
    public partial class FormEmployee : Form
    {
        private readonly UC_Employee _parent;
        public string id, name, rfid, salary;

        public FormEmployee(UserControl parent)
        {
            InitializeComponent();
            _parent = (UC_Employee)parent;

            if (!FormEmployeeInfo.opened)
            {
                // if the serial port is not open, open it
                try
                {
                    serialPort1.Open();
                    FormEmployeeInfo.opened = true;
                    Console.WriteLine("Serial port opened successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error opening serial port: " + ex.Message);
                }
            }
            else
            {
                // if the serial port is already open, do nothing
                Console.WriteLine("Serial port is already open.");
            }
        }


        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtID_MouseEnter(object sender, EventArgs e)
        {

        }

        private void TxtID_Enter(object sender, EventArgs e)
        {


        }

        private void TxtRFID_Enter(object sender, EventArgs e)
        {


        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string incomeString = serialPort1.ReadLine();
            int stringLength = incomeString.Length;
            if (stringLength <= 3)
            {
                if (incomeString.Trim().Equals("0"))
                {
                    MessageBox.Show("The Fingerprint enrollment is feild");
                }
                else
                {
                    if (incomeString.Trim().Equals("9"))
                    {
                        MessageBox.Show("No match finger found");
                    }
                    else
                    {
                        txtID.Text = incomeString;

                    }
                }
            }
            else if (stringLength >= 8)
            {
                txtRFID.Text = incomeString.Replace(" ", "");
            }

        }

        private void txtSalary_TextChanged(object sender, EventArgs e)
        {

        }

        private void FormEmployee_Load(object sender, EventArgs e)
        {

        }

        private void FormEmployee_Leave(object sender, EventArgs e)
        {

        }

        private void FormEmployee_FormClosed(object sender, FormClosedEventArgs e)
        {
            serialPort1.Close();
        }

        private void Fingerprint_Click(object sender, EventArgs e)
        {
            try
            {
                int id = DbEmployee.getLastEmployeeId() + 1;
                if(id == 9)
                {
                    id += 1;
                }
                string stringToSend = $"1{id}0";
                serialPort1.Write(stringToSend);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }

        }

        public void UpdateInfo()
        {
            btnSave.Text = "Update";
            txtID.Text = id;
            txtName.Text = name;
            txtRFID.Text = rfid;
            txtSalary.Text = salary;
        }
        public void Clear()
        {
            txtID.Text = txtName.Text = txtRFID.Text = txtSalary.Text = String.Empty;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (txtID.Text.Trim().Length == 0)
            {
                MessageBox.Show("Employee ID is Empty");
                return;
            }
            if (txtName.Text.Trim().Length == 0)
            {
                MessageBox.Show("Employee Name is Empty");
                return;
            }
            if (txtRFID.Text.Trim().Length == 0)
            {
                MessageBox.Show("Employee RFID is Empty");
                return;
            }
            if (txtSalary.Text.Trim().Length == 0)
            {
                MessageBox.Show("Employee Salary is Empty");
                return;
            }
            if (btnSave.Text == "Save")
            {

                Employee emp = new Employee(txtID.Text.Trim(), txtName.Text.Trim(), txtRFID.Text.Trim(), txtSalary.Text.Trim());
                DbEmployee.AddEmploye(emp);
                Clear();

            }
            if (btnSave.Text == "Update")
            {

                Employee emp = new Employee(txtID.Text.Trim(), txtName.Text.Trim(), txtRFID.Text.Trim(), txtSalary.Text.Trim());
                DbEmployee.UpdateEmploye(emp, id);


            }



            _parent.Display();


        }
    }
}
