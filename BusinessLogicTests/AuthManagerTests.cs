using BusinessLogic.Concrete;
using DAL.Interfaces;
using DTO;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace BusinessLogicTests
{
    [TestFixture]
    public class AuthManagerTests
    {
        private Mock<IPersonDAL> personDAL;

        protected TransactionScope transactionScope;
        AuthManager manager;
        List<PersonDTO> people;

        [SetUp]
        public void SetUp()
        {
            transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew);

            personDAL = new Mock<IPersonDAL>(MockBehavior.Strict);

            manager = new AuthManager(personDAL.Object);

            CreateUsers();
        }

        private void CreateUsers()
        {
            var user1 = new PersonDTO
            {
                ID_Person = 1,
                ID_Role = 1,
                FirstName = "Ivan",
                LastName = "Popel",
                Login = "ivan.popel",
                Password = Encoding.UTF8.GetBytes("user1"),
                Salt = new Guid(),
    
            };
            var user2 = new PersonDTO
            {
                ID_Person = 1,
                ID_Role = 1,
                FirstName = "Natsu",
                LastName = "Levko",
                Login = "natsu.levko",
                Password = Encoding.UTF8.GetBytes("user2"),
                Salt = new Guid(),
            };

            people = new List<PersonDTO> { user1, user2 };
        }

        [Test]
        public void LoginTest()
        {
            PersonDTO inPerson = people.First();
            bool resOut = false;
            personDAL.Setup(x => x.Login("ivan.popel", "user1")).Returns(resOut);

            var res = manager.Login("ivan.popel", "user1");

            Assert.AreEqual(res, resOut);
        }

        [Test]
        public void LoginTest2()
        {
            PersonDTO inPerson = people.Last();
            bool resOut = true;
            personDAL.Setup(x => x.Login("natsu.levko", "user2")).Returns(resOut);

            var res = manager.Login("natsu.levko", "user2");

            Assert.AreEqual(res, resOut);
        }

        [Test]
        public void CreatePersonTest()
        {
            PersonDTO inPerson = new PersonDTO
            {
                ID_Role = 1,
                FirstName = "Test1",
                LastName = "Test1",
                Login = "test1test",
                Password = Encoding.UTF8.GetBytes("test"),
            };
            PersonDTO outPerson = new PersonDTO { ID_Person = 1 };
            personDAL.Setup(x => x.CreatPerson(inPerson)).Returns(outPerson);

            var res = manager.CreatPerson(inPerson);

            Assert.IsNotNull(res);
            Assert.AreEqual(outPerson.ID_Person, res.ID_Person);
        }

        [Test]
        public void GetPersonByLoginTest()
        {
            string inLogin = "User";

            PersonDTO outPerson = new PersonDTO
            {
                ID_Role = 1,
                FirstName = "Test1",
                LastName = "Test1",
                Login = "User",
                Password = Encoding.UTF8.GetBytes("test"),
            };

            personDAL.Setup(x => x.GetPersonByLogin(inLogin)).Returns(outPerson);

            var res = manager.GetPersonByLogin(inLogin);

            Assert.IsNotNull(res);
            Assert.AreEqual(inLogin, res.Login);
        }
    }
}
