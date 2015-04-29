using System;
using Services;

namespace EmployeeSalarySearch
{
    class Program
    {
        static void Main(string[] args)
        {
            var employeeService = new EmployeeService();

            if (args.Length == 1)
            {
                // T A S K   2  - Employee name search
                Console.WriteLine("Employee Search");

                string employeeName = args[0];

                var employeeSalary = employeeService.GetEmployeeSalary(employeeName);

                if (employeeSalary != null)
                {
                    Console.WriteLine("Employee: {0}, Local Currency: {1}, Local Salary {2}, GBP Salary £{3}", 
                        employeeSalary.Name, employeeSalary.CurrencyUnit, employeeSalary.LocalCurrencySalary, employeeSalary.GbpSalary);
                }
                else
                {
                    Console.WriteLine("Employee {0} not found", employeeName);
                }

            }
            else
            {           
                // T A S K   3 - Staff Level Employee List  in order of who is paid the most.
                var employeeSalaries = employeeService.GetEmployeesForRole(1);

                foreach (var employeeSalary in employeeSalaries)
                {
                    Console.WriteLine("Employee: {0}, Local Currency: {1}, Local Salary {2}, GBP Salary £{3}",
                        employeeSalary.Name, employeeSalary.CurrencyUnit, employeeSalary.LocalCurrencySalary, employeeSalary.GbpSalary);
                }

            }


            Console.ReadLine();
        }
    }
}
