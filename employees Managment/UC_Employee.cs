using System;

using System.Windows.Forms;

namespace employees_Managment
{
    public partial class UC_Employee : UserControl
    {
        FormEmployee form;
        public UC_Employee()
        {
            InitializeComponent();
            Display();
        }

        public void Display()
        {
            DbEmployee.DisplayAndSearch("SELECT id,Name,RFIDData,Salary FROM employee", dataGridView);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            form = new FormEmployee(this);
            form.Clear();
            form.ShowDialog();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            DbEmployee.DisplayAndSearch("SELECT id,Name,RFIDData,Salary FROM employee WHERE Name LIKE '%" + txtSearch.Text + "%'", dataGridView);

        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                form.Clear();
                form.id = dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                form.name = dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
                form.rfid = dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
                form.salary = dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();

                form.UpdateInfo();
                form.ShowDialog();
                return;
            }
            if (e.ColumnIndex == 1)
            {
                if (MessageBox.Show("Are you want to delete emplyee record?", "information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    DbEmployee.DeleteEmployee(dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString());
                    Display();
                }
                return;
            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void UC_Employee_Load(object sender, EventArgs e)
        {

        }
    }
}
