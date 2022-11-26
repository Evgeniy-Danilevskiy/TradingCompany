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
    public class SupplyDalTests
    {
        ISupplyDAL dal;

        [OneTimeSetUp]
        public void InitialSetupTest()
        {
            dal = new SupplyDAL(ConfigurationManager.ConnectionStrings["TraidingCompany"].ConnectionString);
        }

        [Test]
        public void GetAllSupplyTest()
        {
            var supply = dal.GetAllSupply();
            Assert.IsTrue(supply.Any(s => s.ID_Person == 1005));
            Assert.IsTrue(supply.Any(s => s.ID_Category == 1002));
            Assert.IsTrue(supply.Any(s => s.NameGoods == "IPhone 13"));
            Assert.IsTrue(supply.Any(s => s.Number == 10));
            Assert.IsTrue(supply.Any(s => s.PriceUnit == 28561));
        }
    }
}
