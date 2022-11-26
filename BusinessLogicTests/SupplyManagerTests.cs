using BusinessLogic.Concrete;
using DAL.Interfaces;
using DTO;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace BusinessLogicTests
{
    [TestFixture]
    public class SupplyManagerTests
    {
        private Mock<ISupplyDAL> supplyDAL;
        private Mock<ICategoryDAL> categoryDAL;
        private Mock<IPersonDAL> personDAL;
        private Mock<IRolesDAL> rolesDAL;
        private SupplyManager manager;

        [SetUp]
        public void Setup()
        {
            supplyDAL = new Mock<ISupplyDAL>(MockBehavior.Strict);
            categoryDAL = new Mock<ICategoryDAL>(MockBehavior.Strict);
            personDAL = new Mock<IPersonDAL>(MockBehavior.Strict);
            rolesDAL = new Mock<IRolesDAL>(MockBehavior.Strict);

            manager = new SupplyManager(categoryDAL.Object,personDAL.Object,rolesDAL.Object,supplyDAL.Object);
        }

        [Test]
        public void AddSupplyTest()
        {
            SupplyDTO inSupply = new SupplyDTO
            {
                ID_Person = 2,
                ID_Category = 3,
                NameGoods = "Phone",
                Number = 4,
                PriceUnit = 1200
            };
            SupplyDTO outSupply = new SupplyDTO { ID_Supply = 1 };

            supplyDAL.Setup(d => d.CreatSupply(inSupply)).Returns(outSupply);
            var res = manager.AddSupply(inSupply);

            Assert.IsNotNull(res);
            Assert.AreEqual(outSupply.ID_Supply,res.ID_Supply);
        }

        [Test]
        public void SortSupplyTest() //sort price min->max
        {
            
            List<SupplyDTO> inSupply = new List<SupplyDTO>();
            inSupply.Add(new SupplyDTO
            {
                ID_Person = 2,
                ID_Category = 1,
                NameGoods = "Phone",
                Number = 4,
                PriceUnit = 25000
            });

            inSupply.Add(new SupplyDTO
            {
                ID_Person = 2,
                ID_Category = 2,
                NameGoods = "Phone",
                Number = 4,
                PriceUnit = 200
            });

            inSupply.Add(new SupplyDTO
            {
                ID_Person = 2,
                ID_Category = 2,
                NameGoods = "Phone",
                Number = 4,
                PriceUnit = 1000
            });

            List<SupplyDTO> outSupply = new List<SupplyDTO> { inSupply[1], inSupply[2], inSupply[0] };

            supplyDAL.Setup(x => x.GetAllSupply()).Returns(inSupply);

            var res = manager.GetSort();

            Assert.AreEqual(res, outSupply);
        }

        [Test]
        public void SearchSupplyTest()
        {
            int IdCategory = 1;
            List<SupplyDTO> inSupply = new List<SupplyDTO>();
            inSupply.Add(new SupplyDTO { 
                ID_Person = 2,
                ID_Category = 1,
                NameGoods = "Phone",
                Number = 4,
                PriceUnit = 1200
            });

            inSupply.Add(new SupplyDTO
            {
                ID_Person = 2,
                ID_Category = 2,
                NameGoods = "Phone",
                Number = 4,
                PriceUnit = 1200
            });

            List<SupplyDTO> outSupply = new List<SupplyDTO>();
            outSupply.Add(new SupplyDTO
            {
                ID_Person = 2,
                ID_Category = 1,
                NameGoods = "Phone",
                Number = 4,
                PriceUnit = 1200
            });
            supplyDAL.Setup(x=>x.GetSupplyByIdCategory(IdCategory)).Returns(outSupply);

            var res = manager.SearchGoodsByCategory(IdCategory);
            Assert.IsNotNull(res);
            Assert.AreEqual(res, outSupply);
        }
    }
}


