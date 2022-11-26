using DTO;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces
{
    public interface ISupplyManager
    {
        SupplyDTO AddSupply(SupplyDTO supply);
        SupplyDTO GetSupplyById(int supplyID);
        void DeleteSupply(int supplyId);
        SupplyDTO UpdateSupply(SupplyDTO s, int id);
        List<SupplyDTO> GetListOfSupply();
        List<SupplyDTO> SearchGoodsByCategory(int id);
        List<SupplyDTO> GetSort();
        List<CategoryDTO> GetListOfCategory();
        List<RolesDTO> GetListOfRoles();
        List<string> GetNameCategory();
        List<string> GetPersonLogin();
    }
}
