using DTO;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IPersonDAL
    {
        List<PersonDTO> GetAllPerson();
        PersonDTO GetPersonById(int personId);
        PersonDTO UpdatePerson(PersonDTO p, int id);
        PersonDTO CreatPerson(PersonDTO p);
        void DeletePerson(int personId);
        bool Login(string login, string password);
        PersonDTO GetPersonByLogin(string login);
    }
}
