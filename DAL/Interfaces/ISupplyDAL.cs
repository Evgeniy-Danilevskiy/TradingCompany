using DTO;
using System.Collections.Generic;


namespace DAL.Interfaces
{
    public interface ISupplyDAL
    {
        List<SupplyDTO> GetAllSupply();
        SupplyDTO GetSupplyById(int supplyId);
        SupplyDTO UpdateSupply(SupplyDTO s, int id);
        SupplyDTO CreatSupply(SupplyDTO s);
        void DeleteSupply(int supplyId);
        List<SupplyDTO> GetSupplyByIdCategory(int id);
    }
}
