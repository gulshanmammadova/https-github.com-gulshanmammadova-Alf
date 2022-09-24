using System;
using System.Collections.Generic;
using System.Text;
using CoonsolProject.Models;
using CoonsolProject.Services;

namespace CoonsolProject.Interfaces
{
    interface IHumanResourceManager
    {
        Department[] Departments { get; } 
        public void AddEmployee(string fullname, string position, double salary,string departmentName);
        public void AddDepartment(string name, byte workerLimit, double salaryLimit);
        public void EditDepartment(string departmentName,byte workerLimit ,double salaryLimit);
        public void EditEmployee(double editSalary, string editPosition, string editEmployeeNo);
        public bool RemoveEmployee(string workerNo, string departmentName);
        public bool CheckDepartmentName(string departmentName);

        public Department CheckDepartmet(string departmentName);
       
    }
}