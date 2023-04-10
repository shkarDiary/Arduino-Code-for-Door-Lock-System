using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace employees_Managment
{
    internal class Employee
    {
        public String ID { get; set; }
        public String Name { get; set; }

        public String RFID { get; set; }
        String _salary;
        public String Salary { get { return Salary + '$'; } set => Salary = Salary; }

        public Employee(string iD, string name, string rFID, string salary)
        {
            ID = iD;
            Name = name;
            RFID = rFID;
            Salary = salary;
        }

    }
}
