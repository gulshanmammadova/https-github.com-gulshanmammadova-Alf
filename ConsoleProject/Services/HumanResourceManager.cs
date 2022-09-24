using System;
using System.Collections.Generic;
using System.Text;
using CoonsolProject.Models;
using CoonsolProject.Services;
using CoonsolProject.Interfaces;

namespace CoonsolProject.Services
{
    class HumanResourceManager : IHumanResourceManager
    {
        private Department[] _departments;
        public HumanResourceManager()
        {
            _departments = new Department[0];
        }
        public Department[] Departments => _departments; 

        public void EditDepartment(string departmentName, byte workerLimit ,double salaryLimit)
        {
            foreach (Department department in _departments)
            {
                department.Name = departmentName;
                department.WorkerLimit = workerLimit;
                department.SalaryLimit = salaryLimit;
            }
        }

        public void AddEmployee(string fullname, string position, double salary,  string departmentName)
        {
            foreach (Department department in _departments)
            {
                if (department.WorkerLimit > department.Employees.Length)
                {
                    Employee employee1 = new Employee( fullname,position, salary, departmentName);

                    Array.Resize(ref department.Employees, department.Employees.Length + 1);
                    department.Employees[department.Employees.Length - 1] = employee1;
                }
                else
                {
                    Console.WriteLine($"{departmentName} -  Da Yer Yoxdur");
                }

            }
        }
        public void AddDepartment(string name, byte workerLimit, double salaryLimit)
        {
            Department department = new Department(name, workerLimit, salaryLimit);

            Array.Resize(ref _departments, _departments.Length + 1);
            _departments[_departments.Length - 1] = department;
        }
        public void EditEmployee(double editSalary, string editPosition,string editEmployeeNo)
        {
            if (Departments.Length == 0)
            {
                Console.WriteLine("ilk once department yaradilmalidi");
            }
            else
            {
                foreach (Department department in Departments)
                {
                    if (department.Employees.Length == 0)
                    {
                        Console.WriteLine($" Departamentde isci yoxdusa deyisiklik mumkun deyil!\nIlk once isci elave edin");
                    }
                    else
                    {
                        foreach (Employee employee in department.Employees)
                         {
                                if (employee.No.ToUpper() == editEmployeeNo.ToUpper())
                                {

                                    employee.Salary = editSalary;
                                    employee.Position = editPosition;

                                }
                         }
                        
                    }
                }

            }


        }
        public bool RemoveEmployee(string workerNo, string departmentName)
        {
            foreach (Department department in _departments)
            {
                if (department.Name.ToUpper() == departmentName.ToUpper())
                {
                    foreach (Employee employee in department.Employees)
                    {
                        if (workerNo.ToUpper() == employee.No.ToUpper())
                        {
                            return true;
                        }

                    }

                }
            }
            return false;
        }
        public bool CheckDepartmentName(string departmentName)
        {
            foreach (Department department in _departments)
            {
                if (departmentName.ToUpper() == department.Name.ToUpper())
                {
                    return true;
                }
            }
            return false;
        }

        public Department CheckDepartmet(string departmentName)
        {
            foreach (Department department in Departments)
            {
                if (departmentName.ToUpper() == department.Name.ToUpper())
                {
                    return department;
                }
            }
            return null;
        }
    }
}

