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
    public class CategoryDalTests
    {
        ICategoryDAL dal;
        
        [OneTimeSetUp]
        public void InitialSetupTest()
        {
            dal = new CategoryDAL(ConfigurationManager.ConnectionStrings["TraidingCompany"].ConnectionString);
        }

        [Test]
        public void GetAllCategoryTest()
        {
            var category = dal.GetAllCategory();
            Assert.IsTrue(category.Any(c => c.CategoryName == "Phones"));
        }
    }
}
