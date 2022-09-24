using System;
using System.Collections.Generic;
using System.Text;
using CoonsolProject.Models;
using CoonsolProject.Services;

namespace CoonsolProject.Models
{
    class Department
    {
        
        public string Name;
        public byte WorkerLimit;
        public double SalaryLimit;
        public Employee[] Employees = new Employee[0];


        public Department(string name, byte workerLimit, double salaryLimit)
        {
            Name = name;
            WorkerLimit = workerLimit;
            SalaryLimit = salaryLimit;
        }
        public void CalcaCalcSalaryAverage()    
        {
            double sum = 0;
            double avg = 0;
            foreach (var employee in Employees)
            {
                sum += employee.Salary;
            }
            avg = sum / Employees.Length;
            Console.WriteLine($"orta maas  {avg}  AZN");

        }
    }
}