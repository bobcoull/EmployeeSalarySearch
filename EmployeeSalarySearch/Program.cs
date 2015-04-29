using System;
using Services;

namespace EmployeeSalarySearch
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                // T A S K   2  - Employee name search
                Console.WriteLine("Employee Search");

                string employeeName = args[0];

                var employeeService = new EmployeeService();

                var employeeSalery = employeeService.GetEmployeeSalary(employeeName);

                if (employeeSalery != null)
                {
                    Console.WriteLine("Employee: {0}, Local Currency: {1}, Local Salary {2}, GBP Salary £{3}", 
                        employeeSalery.Name, employeeSalery.CurrencyUnit, employeeSalery.LocalCurrencySalary, employeeSalery.GbpSalary);
                }
                else
                {
                    Console.WriteLine("Employee {0} not found", employeeName);
                }

            }
            else
            {           
                // T A S K   3 - Staff Level Employee List  in order of who is paid the most.
                
            }


            Console.ReadLine();
        }
    }
}
