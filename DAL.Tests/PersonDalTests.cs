using DAL.ADO;
using DAL.Interfaces;
using DTO;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL.Tests
{
    [TestFixture]
    public class PersonDalTests
    {
        IPersonDAL dal;

        [OneTimeSetUp]
        public void InitialSetupTest()
        {
            dal = new PersonDAL(ConfigurationManager.ConnectionStrings["TraidingCompany"].ConnectionString);
        }

        [Test]
        public void GetAllPersonTest()
        {
            var person = dal.GetAllPerson();
            Assert.IsTrue(person.Any(p => p.ID_Role == 1002));
            Assert.IsTrue(person.Any(p => p.FirstName == "Nastia"));
            Assert.IsTrue(person.Any(p => p.LastName == "Volosh"));
            Assert.IsTrue(person.Any(p => p.Login == "nastivka_lastivka"));
            //Assert.IsTrue(person.Any(p => p.Password == "20020319Petivna"));
        }

    }
}
