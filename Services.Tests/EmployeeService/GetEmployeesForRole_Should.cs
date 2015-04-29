using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Data;
using Moq;
using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace Services.Tests
{
    [TestFixture]
    public class GetEmployeesForRole_Should
    {

        private EmployeeService _employeeService;
        private Mock<IUnitOfWorkFactory> _unitOfWorkFactory;
        private Mock<IUnitOfWork> _unitOfWork;

        [SetUp]
        public void SetUp()
        {
            _unitOfWorkFactory = new Mock<IUnitOfWorkFactory>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _employeeService = new EmployeeService(_unitOfWorkFactory.Object);

            _unitOfWorkFactory.Setup(f => f.Create()).Returns(_unitOfWork.Object);

        }

        [Test]
        public void Return_List_Of_Three_Employees_With_Assigned_Role()
        {        
            //Assign
            _unitOfWork.Setup(w => w.Employee.Find(It.IsAny<Expression<Func<Employee, bool>>>())).Returns(GetEmployeeList());

            //Act
            var result = _employeeService.GetEmployeesForRole(1);

            //Asserts
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("Employee Order 1", result[0].Name); // first row
            Assert.AreEqual("Employee Order 3", result[2].Name); // last row
        }

        [Test]
        public void Return_Empty_List_With_Unassigned_Role()
        {
            //Assign
            _unitOfWork.Setup(w => w.Employee.Find(It.IsAny<Expression<Func<Employee, bool>>>())).Returns(GetEmptyEmployeeList());

            //Act
            var result = _employeeService.GetEmployeesForRole(2);

            //Asserts
            Assert.AreEqual(0, result.Count);
        }

        private IEnumerable<Employee> GetEmptyEmployeeList()
        {
            return new List<Employee>();
        }

        private IEnumerable<Employee> GetEmployeeList()
        {
            return new List<Employee>
            {
                new Employee
                {
                    Name = "Employee Order 3",
                    Salaries = new Collection<Salary>
                    {
                        new Salary
                        {
                            AnnualAmount = 10134,
                            Currency = new Currency
                            {
                                Unit = "USD",
                                ConversionFactor = (decimal) 1.54
                            }
                        }
                    },
                    RoleId = 1 
                },
                new Employee
                {
                    Name = "Employee Order 1",
                    Salaries = new Collection<Salary>
                    {
                        new Salary
                        {
                            AnnualAmount = 12134,
                            Currency = new Currency
                            {
                                Unit = "USD",
                                ConversionFactor = (decimal) 1.54
                            }
                        }
                    },
                    RoleId = 1 
                },
                new Employee
                {
                    Name = "Employee Order 2",
                    Salaries = new Collection<Salary>
                    {
                        new Salary
                        {
                            AnnualAmount = 11134,
                            Currency = new Currency
                            {
                                Unit = "USD",
                                ConversionFactor = (decimal) 1.54
                            }
                        }
                    },
                    RoleId = 1 
                }
            };
        }

    }
}
