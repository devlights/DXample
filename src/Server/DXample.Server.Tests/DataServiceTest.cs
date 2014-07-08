using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DXample.Server.Interfaces;
using NUnit.Framework;

namespace DXample.Server.Tests
{
    [TestFixture]
    class DataServiceTest
    {
        [TestCase(1)]
        [TestCase(100)]
        [TestCase(1000)]
        [TestCase(10000)]
        [TestCase(100000)]
        [TestCase(1000000)]
        public void GetAllEmployees_データが取得できる(int numberOfEmployees)
        {
            // Arrange
            DataServiceState.Instance.NumberOfEmployees = numberOfEmployees;

            var sut = new DataService();

            // Act
            var results = sut.GetAllEmployees();

            // Assert
            Assert.That(() => results.Count(), Is.EqualTo(numberOfEmployees));

            var item = results.First();
            Assert.That(() => item.EmployeeType, Is.EqualTo(EmployeeType.Developer));
            Assert.That(() => item.Name, Is.EqualTo("Employee1"));
            Assert.That(() => item.Age, Is.EqualTo(21));
            Assert.That(() => item.Salary, Is.GreaterThanOrEqualTo(200000));
        }
    }
}
