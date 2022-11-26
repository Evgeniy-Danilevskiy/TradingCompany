using BusinessLogic.Interfaces;
using DAL.Interfaces;
using DTO;
using System.Collections.Generic;

namespace BusinessLogic.Concrete
{
    public class AuthManager : IAuthManager
    {
        private readonly IPersonDAL _personDAL;

        public AuthManager(IPersonDAL personDAL)
        {
            _personDAL = personDAL;
        }

        public PersonDTO GetPersonById(int id)
        {
            return _personDAL.GetPersonById(id);
        }

        public PersonDTO CreatPerson(PersonDTO p)
        {
            return _personDAL.CreatPerson(p);
        }

        public PersonDTO GetPersonByLogin(string login)
        {
            return _personDAL.GetPersonByLogin(login);
        }

        public List<PersonDTO> GetPersons()
        {
            return _personDAL.GetAllPerson();
        }

        public bool Login(string login, string password)
        {
            return _personDAL.Login(login, password);
        }
    }
}
