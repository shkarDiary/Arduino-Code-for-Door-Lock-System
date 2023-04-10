using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace employees_Managment
{
    internal class Attendance
    {
        public String AttID { get; }
        public String EmpID { get; set; }
        public String Date { get; set; }

        public String TimeIn { get; set; }
        public String TimeOut { get; set; }
        public Attendance(string empID, string date, string timeIn, string timeOut)
        {
            EmpID = empID;
            Date = date;
            TimeOut = timeOut;
            TimeIn = timeIn;
        }
    }
}
