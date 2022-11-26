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
    public class RolesDalTests
    {
        IRolesDAL dal;

        [OneTimeSetUp]
        public void InitialSetupTest()
        {
            dal = new RolesDAL(ConfigurationManager.ConnectionStrings["TraidingCompany"].ConnectionString);
        }

        [Test]
        public void GetAllRolesTest()
        {
            var roles = dal.GetAllRoles();
            Assert.IsTrue(roles.Any(r => r.RoleName == "Admin"));
        }
    }
}
