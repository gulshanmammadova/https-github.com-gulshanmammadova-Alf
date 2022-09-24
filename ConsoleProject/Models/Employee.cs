using System;
using System.Collections.Generic;
using CoonsolProject.Models;
using CoonsolProject.Services;
using System.Text;

namespace CoonsolProject.Models
{
    class Employee
    {
        private static int _no;
        public string No;
        public string FullName;
        public string Position;
        public double Salary;
        public string DepartmentName;

        static Employee()
        {
            _no = 1000;
        }
        public Employee(string fullName ,string position, double salary, string departamentName)
        {
            FullName = fullName;
            Position = position;
            Salary = salary;
            DepartmentName = departamentName;
            _no++;
            No = $"{departamentName[0]}{departamentName[1]}{_no}";
        }
    }

}