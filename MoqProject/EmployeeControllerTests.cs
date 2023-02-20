using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestingMoqDomain.Entity;
using UnitTestingMoqDomain.Repositories;
using UnitTestingWithMoq.Controllers;
using Xunit;

namespace MoqProject
{
    public class EmployeeControllerTests
    {
        private readonly Mock<IEmployeeRepository> service;

        public EmployeeControllerTests()
        {
            service = new Mock<IEmployeeRepository>();
        }

        [Fact]
        //naming convention MethodName_expectedBehavior_StateUnderTest
        public void GetEmployee_ListOfEmployee_EmployeeExistsInRepo()
        {
            //arrange
            var employee = GetSampleEmployee();
            service.Setup(x => x.GetAll())
                .Returns(GetSampleEmployee);
            var controller = new EmployeeController(service.Object);

            //act
            var actionResult = controller.GetEmployee();
            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value as IEnumerable<Employee>;

            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(GetSampleEmployee().Count(), actual.Count());
        }

        [Fact]
        public void GetEmployeeId_EmployeeObject_EmployeeWithSpecificeIdExists()
        {
            //arrange
            var employees = GetSampleEmployee();
            var firstEmployee = employees[0];
            service.Setup(x => x.GetById((long)1))
                .Returns(firstEmployee);
            var controller = new EmployeeController(service.Object);

            //act
            var actionResult = controller.GetEmployeeById((long)1);
            var result = actionResult.Result as OkObjectResult;

            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(firstEmployee, result.Value);
            //result.Value.Should().BeEquivalentTo (firstEmployee);
        }

        [Fact]
        public void GetEmployeeById_shouldReturnBadRequest_EmployeeWithIDNotExist()
        {
            //arrange
            var employee = GetSampleEmployee();
            var firstEmployee = employee[0];
            service.Setup(x => x.GetById((long)1))
                .Returns(firstEmployee);
            var controller = new EmployeeController(service.Object);

            //act
            var actionResult = controller.GetEmployeeById((long)4);

            //assert
            var result = actionResult.Result;
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Theory]
        [InlineData(18)]
        [InlineData(20)]
        public void checkIfUserCanBeVoter_true_ageGreaterThan18(int age)
        {
            //arrange
            var conroller = new EmployeeController(null);

            //act
            var actual = conroller.checkIfUserCanBeVoter(age); 

            //Assert
            Assert.True(actual);
        }

        [Theory]
        [InlineData(17)]
        [InlineData(15)]
        public void checkIfUserCanBeVoter_true_ageLessThan18(int age)
        {
            //arrange
            var controller = new EmployeeController(null);

            //act
            var actual = controller.checkIfUserCanBeVoter(age);

            //assert
            Assert.False(actual);
        }

        [Fact]
        public void CreateEmployee_CreatedStatus_PassingEmployeeObjectToCreate()
        {
            var employees = GetSampleEmployee();
            var newEmployee = employees[0];
            var controller = new EmployeeController(service.Object);
            var actionResult = controller.CreateEmployee(newEmployee);
            var result = actionResult.Result;
            Assert.IsType<CreatedAtActionResult>(result);
        }

        private List<Employee> GetSampleEmployee()
        {
            List<Employee> output = new List<Employee>
            {
                new Employee
                {
                    FirstName = "Alex",
                    LastName = "Teka",
                    PhoneNumber = "0912345678",
                    DateOfBirth = DateTime.Now,
                    Email = "alex@gmail.com",
                    EmployeeId = 1
                },
                new Employee
                {
                    FirstName = "Tesfay",
                    LastName = "Bsrat",
                    PhoneNumber = "0912344567",
                    DateOfBirth = DateTime.Now,
                    Email = "tesgay@gmail.com",
                    EmployeeId = 2
                },
                new Employee
                {
                    FirstName = "Aregawi",
                    LastName = "Team",
                    PhoneNumber = "0987654321",
                    DateOfBirth = DateTime.Now,
                    Email = "tesfay@gmail.com",
                    EmployeeId = 3
                }
            };
            return output;
        }
    }
}
