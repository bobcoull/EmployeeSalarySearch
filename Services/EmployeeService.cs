using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Data.Repositories;
using Services.Models;

namespace Services
{
    public interface IEmployeeService
    {
        EmployeeSalary GetEmployeeSalary(string employeeName);
    }

    public class EmployeeService : IEmployeeService
    {
        //private readonly IEmployeeRepository _employeeRepository;

        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public EmployeeService()
        {
            _unitOfWorkFactory = new UnitOfWorkFactory();
        }

        public EmployeeService(IUnitOfWorkFactory unitOfWorkFactory) 
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }


        public EmployeeSalary GetEmployeeSalary(string employeeName)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var employee = uow.Employee.GetAll().FirstOrDefault(e => e.Name == employeeName);

                if (employee != null)
                {
                    return MapEmployeeToEmployeeSalary(employee);
                }
                return null;
            
            }   
        }

        public List<EmployeeSalary> GetEmployeesForRole(int roleId)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var employees = uow.Employee.Find(e => e.RoleId == roleId);

                var employeeSalaries = new List<EmployeeSalary>();

                foreach (var employee in employees)
                {
                    employeeSalaries.Add(MapEmployeeToEmployeeSalary(employee));                    
                }
                return employeeSalaries.OrderByDescending(e => e.GbpSalary).ToList();
            }
        }


        private EmployeeSalary MapEmployeeToEmployeeSalary(Employee employee)
        {
            string currencyUnit = "";
            double gbpSalary = 0;
            double localCurrencySalary = 0;

            var salary = employee.Salaries.FirstOrDefault();
            if (salary != null)
            {
                currencyUnit = salary.Currency.Unit;
                gbpSalary = Math.Round((double)(salary.AnnualAmount / salary.Currency.ConversionFactor), 2);
                localCurrencySalary = salary.AnnualAmount;
            }

            return new EmployeeSalary
            {
                Id = employee.Id,
                Name = employee.Name,
                CurrencyUnit = currencyUnit,
                GbpSalary = gbpSalary,
                LocalCurrencySalary = localCurrencySalary
            };
        }
    
    }
}
