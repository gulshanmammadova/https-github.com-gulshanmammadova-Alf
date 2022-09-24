using System;
using CoonsolProject.Models;
using CoonsolProject.Interfaces;
using CoonsolProject.Services;

namespace CoonsolProject
{
    class Program
    {
        //for Alf
        static void Main(string[] args)
        {
            IHumanResourceManager humanResourceManager = new HumanResourceManager();
            do
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\"Mammadov Company\"-e xos gelmissiniz!");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine("Etmek istediyiniz elameti secin;");
                Console.WriteLine(" 1.Departamenet yaratmaq");
                Console.WriteLine(" 2.Isci elave etmek");
                Console.WriteLine(" 3.Departamenet siyahisi  gostermek");
                Console.WriteLine(" 4.Departmanetde deyisiklik etmek");
                Console.WriteLine(" 5.Iscilerin siyahisini gostermek");
                Console.WriteLine(" 6.Departamentdeki iscilerin siyahisini gostermrek");
                Console.WriteLine(" 7.Isci uzerinde deyisiklik etmek");
                Console.WriteLine(" 8.Departamentden isci silinmesi");
                string answerStr = Console.ReadLine();
                int answerNum;
                while (!int.TryParse(answerStr, out answerNum) || answerNum < 1 || answerNum > 8)
                {
                    Console.WriteLine("Duzgun Secim Edin");
                    answerStr = Console.ReadLine();
                }
                switch (answerNum)
                {
                    case 1:
                        Console.Clear();
                        AddDepartment(ref humanResourceManager);
                        break;
                    case 2:
                        Console.Clear();
                        AddEmployee(ref humanResourceManager);
                        break;
                    case 3:
                        Console.Clear();
                        GetDepartment(ref humanResourceManager);
                        break;
                    case 4:
                        Console.Clear();
                        EditDepartment(ref humanResourceManager);                       
                        break;
                    case 5:
                        Console.Clear();
                        ListOfAllEmployees(ref humanResourceManager);                        
                        break;
                    case 6:
                        Console.Clear();
                        ListOfDepartmentWorker(ref humanResourceManager);
                        break;
                    case 7:
                        Console.Clear();
                        EditEmployee(ref humanResourceManager);
                        break;
                    case 8:
                        //yaza bilmedim
                        break;
                }

            }
            while (true);
        }
       
        static bool CheckFullName(string fullName)
        {
            string newFullName = fullName.Trim();
            string[] strArr = newFullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            newFullName = string.Join(' ', strArr);

            if (!string.IsNullOrEmpty(fullName) && char.IsUpper(fullName[0]) && char.IsLower(fullName[fullName.Length - 1]) && newFullName.Length == fullName.Length)
            {
                for (int i = 1; i < fullName.Length; i++)
                {
                    if (char.IsLower(fullName[i]) || (fullName[i] == ' ' && char.IsUpper(fullName[i + 1])))
                    {
                        return true;
                    }
                }

            }
            return false;
        }
        static void AddDepartment(ref IHumanResourceManager humanResourceManager)
        {
            Console.WriteLine("Yaradilacaq department adini qeyd et;");
            string departamentName = Console.ReadLine();
            while (!(departamentName.Length > 2))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("departament adini yeniden qeyd et");
                Console.ForegroundColor = ConsoleColor.Cyan;
                departamentName = Console.ReadLine();
            }         
            Console.WriteLine(" isci Limitini Daxil Et");
            string limitstr = Console.ReadLine();
            byte workerLimit;

            while (!byte.TryParse(limitstr, out workerLimit) || workerLimit < 12 || workerLimit > 19) 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Duzgun isci Limitini Daxil Et:");
                Console.ForegroundColor = ConsoleColor.Cyan;
                limitstr = Console.ReadLine();
            }
            Console.WriteLine(" salary Limitini Daxil Et");
            string salarystr = Console.ReadLine();
            double salaryLimit;

            while (!(double.TryParse(salarystr, out salaryLimit)) || salaryLimit < 250)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Duzgun maas Limitini Daxil Et: Minimum 250 Ola Biler");
                Console.ForegroundColor = ConsoleColor.Cyan;
                salarystr = Console.ReadLine();
            } 
            humanResourceManager.AddDepartment(departamentName, workerLimit, salaryLimit);
            Console.WriteLine($"departament ugurla yaradildi\n size is heyatinizda ugurlar!");

        }
        static void AddEmployee(ref IHumanResourceManager humanResourceManager)
        {
            Console.WriteLine("movcud departamentlerin siyahisi;");
            GetDepartment(ref humanResourceManager); 
            Console.WriteLine("isci elave edilecek department adini qeyd et;");
            string departamentName = Console.ReadLine();
            foreach (Department department in humanResourceManager.Departments)
            {

                while (!humanResourceManager.CheckDepartmentName(departamentName))
                {
                    Console.WriteLine("departament adini yeniden qeyd et");
                    departamentName = Console.ReadLine();
                }
            }
            Console.WriteLine("adi ve soy adi daxil et");
            string fullName = Console.ReadLine();
            while (CheckFullName(fullName) == false)
            {
                Console.WriteLine("ad ve soy adi duzgun daxil et");
                fullName = Console.ReadLine();

            }
            Console.WriteLine(" Maasi Daxil Et");
            string salaryst = Console.ReadLine();
            double salary;
            while (!(double.TryParse(salaryst, out salary)) || salary < 250)
            {
                Console.WriteLine("Duzgun maas Daxil Et: Minimum 250 Ola Biler");
                salaryst = Console.ReadLine();
            }
            Console.WriteLine("Position daxil et");
            string position = Console.ReadLine();
            while (position.Length <= 2)
            {
                Console.WriteLine("Positionu yeniden daxil et");
                position = Console.ReadLine();
            }
            humanResourceManager.AddEmployee(fullName,position,salary,departamentName);
        }
        static void RemoveEmployee(ref IHumanResourceManager humanResourceManager)
        {
            Console.WriteLine("departamenti secin");
            int num = 1;

            foreach (Department department in humanResourceManager.Departments)
            {
                Console.WriteLine($"Departament {num} Name :{department.Name}");
                num++;
            }
            string numstr = Console.ReadLine();
            int num1;
            while (!int.TryParse(numstr, out num1) || num1 < humanResourceManager.Departments.Length)
            {
                Console.WriteLine("Duzgun Secim Edin");
                numstr = Console.ReadLine();
            }
            for (int i = 0; i < humanResourceManager.Departments.Length; i++)
            {
            }
            //    { string temp ;
            //        if (num == i) 
            //        {
            //            temp = humanResourceManager.Departments[i].ToString();
            //            humanResourceManager.Departments[i] = humanResourceManager.Departments[humanResourceManager.Departments.Length - 1];
            //            temp= humanResourceManager.Departments[humanResourceManager.Departments.Length - 1].ToString();
            //        }
            //    }
            //    Console.WriteLine("yeni isci siyahisi");
            //    Array.Resize(ref humanResourceManager.de, )
            //} 
        }
        static void GetDepartment(ref IHumanResourceManager humanResourceManager)
        {
            Console.WriteLine("hal hazirda movcud departamentlerin siyahisi;");
            foreach (Department department in humanResourceManager.Departments)
            {
                Console.WriteLine($"department name :{department.Name}\nDepartment salary Limit{department.SalaryLimit}\ndepartment Worker  Limit :{department.WorkerLimit}"); 
            }
        }
        static void  EditDepartment(ref IHumanResourceManager humanResourceManager)
        {         
            GetDepartment(ref humanResourceManager);              
            Console.WriteLine("siyahidan deyismek istediyiniz departamentin adini secin;");
            foreach (Department department in humanResourceManager.Departments)
            {
                Console.WriteLine(department.Name);
            }
            string name = Console.ReadLine();
            if(!humanResourceManager.CheckDepartmentName(name))
            {
                Console.WriteLine("Bu ada mexsus department yoxdur !");
                return;
            }
            Console.WriteLine("Yeni department Name daxil edin ");
            string newDepartmentName = Console.ReadLine();
            while (!(newDepartmentName.Length > 2))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("departament adini yeniden qeyd et");
                Console.ForegroundColor = ConsoleColor.Cyan;
                newDepartmentName  = Console.ReadLine();
            }

            Console.WriteLine(" yeni isci Limitini Daxil Et");
            string newlimit = Console.ReadLine();
            byte newwrkrLimit;

            while (!byte.TryParse(newlimit, out newwrkrLimit))
            {
                Console.WriteLine("Duzgun isci Limitini Daxil Et:");
                newlimit = Console.ReadLine();
            }
            Console.WriteLine(" salary Limitini Daxil Et");
            string newsalarystr = Console.ReadLine();
            double newsalaryLimit;

            while (!(double.TryParse(newsalarystr, out newsalaryLimit)) || newsalaryLimit < 250)
            {
                Console.WriteLine("Duzgun maas Limitini Daxil Et: Minimum 250 Ola Biler");
                newsalarystr = Console.ReadLine();
            }

            Console.WriteLine("Departamentin adi isteye uygun olaraq ugurla deyisdirildi!");


             humanResourceManager.EditDepartment(newDepartmentName, newwrkrLimit, newsalaryLimit);
        }
        static void ListOfDepartmentWorker(ref IHumanResourceManager humanResourceManager)
        {
            foreach (Department department in humanResourceManager.Departments)
            {
                Console.WriteLine(department.Name);
            }
            Console.WriteLine("departament adini qeyd edin; ");
            string depnm = Console.ReadLine();
            while (!humanResourceManager.CheckDepartmentName(depnm))
            {
                Console.WriteLine("Yeniden duzgun daxil edin ");
                depnm = Console.ReadLine();
            }
            Department thisDepartment = humanResourceManager.CheckDepartmet(depnm);
            if (thisDepartment == null)
            {
                Console.WriteLine("bu adda department yoxdur!!!");
                return;
            }
            foreach (Employee employee in thisDepartment.Employees)
            {
                Console.WriteLine($"ad ve soyad-{employee.FullName}\nMaas-{employee.Salary}\n" +
                           $"Vezife-{employee.Position}\ndepartamentinin adi - {employee.DepartmentName}\nisci nomresi{employee.No}");
                Console.WriteLine("\n*********************\n");
            }

        } 

        static void ListOfAllEmployees(ref IHumanResourceManager humanResourceManager)
        {
            foreach (Department department in humanResourceManager.Departments)
            {
                foreach (Employee employees in department.Employees)
                {
                        Console.WriteLine($"ad ve soyad-{employees.FullName}\nMaas-{employees.Salary}\n" +
                            $"Vezife-{employees.Position}\ndepartamentinin adi - {employees.DepartmentName}\nisci nomresi{employees.No}");
                    Console.WriteLine("\n*********************\n");

                }
            }
        }
        static void EditEmployee(ref IHumanResourceManager humanResourceManager)
        {

            GetDepartment(ref humanResourceManager);
            Console.WriteLine("siyahidan deyismek istediyiniz departamentin adini secin;");
            foreach (Department department in humanResourceManager.Departments)
            {
                Console.WriteLine(department.Name);
            }
            string name = Console.ReadLine();
          
            if (!humanResourceManager.CheckDepartmentName(name))
            {
                Console.WriteLine("Bu ada mexsus department yoxdur !");
                return;
            }
            Department thisDepartment = humanResourceManager.CheckDepartmet(name);
            foreach (Employee employee in thisDepartment.Employees)
            {
                Console.WriteLine($"Name :{employee.FullName}\nEiscinin nomresi - {employee.No}");
            }
            Console.WriteLine("deyismek istediyin iscinin nomresini yazin");
            string salarys = "";
            string newposition = "";
            double newsalary = 0;
            string checkNO = Console.ReadLine();
            foreach (Employee employee in thisDepartment.Employees)
            {
                if (employee.No.ToUpper()==checkNO.ToUpper())
                {
                    Console.WriteLine("yeni  Maasi Daxil Et");
                    salarys = Console.ReadLine();
                    while (!(double.TryParse(salarys, out newsalary)) || newsalary < 250)
                    {
                        Console.WriteLine("Duzgun maas Daxil Et: Minimum 250 Ola Biler");
                        salarys = Console.ReadLine();
                    }
                    Console.WriteLine("yeni Position daxil et");
                     newposition = Console.ReadLine();
                    while (newposition.Length <= 2)
                    {
                        Console.WriteLine("Positionu yeniden daxil et");
                        newposition = Console.ReadLine();
                    }
                }
            }
            humanResourceManager.EditEmployee(newsalary, newposition,checkNO);
        }
    }
}
