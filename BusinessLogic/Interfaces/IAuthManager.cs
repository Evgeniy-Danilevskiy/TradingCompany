using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IAuthManager
    {
        bool Login(string login, string password);
        PersonDTO GetPersonByLogin(string login);
        PersonDTO GetPersonById(int id);
        List<PersonDTO> GetPersons();
    }
}
