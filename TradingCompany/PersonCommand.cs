using System;
using DAL.ADO;
using DTO;
using System.Configuration;
using System.Text;

namespace TradingCompany
{
    public class PersonCommand
    {
        public static void CreatePerson(PersonDAL personDal)
        {
            string connStr = ConfigurationManager.ConnectionStrings["TraidingCompany"].ConnectionString;
            var rolesDal = new RolesDAL(connStr);
            Console.WriteLine("Creating New Person");
            Console.WriteLine("Select a Role: ");
            RolesCommand.GetAllRoles(rolesDal);
            string _roleId = (Console.ReadLine());
            int _roleId2 = Convert.ToInt32(_roleId);
            Console.WriteLine("Input First Name: ");
            string _firstName = Console.ReadLine();
            Console.WriteLine("Input Last Name: ");
            string _lastName = Console.ReadLine();
            Console.WriteLine("Input Login: ");
            string _log = Console.ReadLine();
            Console.WriteLine("Input Password: ");
            byte[] _passw = Encoding.UTF8.GetBytes(Console.ReadLine());

            PersonDTO myPerson = new PersonDTO
            {
                ID_Role = _roleId2,
                FirstName = _firstName,
                LastName = _lastName,
                Login = _log,
                Password = _passw
            };

            try
            {
                myPerson = personDal.CreatPerson(myPerson);
                Console.WriteLine($"New PersonID is {myPerson.ID_Person}");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("Data inputted incorrectly.");
            }
        }
        public static void DeletePerson(PersonDAL personDal)
        {
            Console.WriteLine("Deleting Person");
            Console.WriteLine("Input PersonID: ");
            string ids = Console.ReadLine();
            int id = Convert.ToInt32(ids); personDal.DeletePerson(id);
            Console.WriteLine("Successfully deleted");
        }

        public static void GetAllPerson(PersonDAL personDal)
        {
            var person = personDal.GetAllPerson();
            Console.WriteLine("Person:");
            foreach (var p in person)
            {
                Console.WriteLine($"{p.ID_Person,6}|{p.ID_Role,6}|{p.FirstName,7}|{p.LastName,13}|{p.Login,22}|{p.Password,15}|{p.RowInsertTime,15}| {p.RowUpdateTime,15} |");
            }
        }

        public static void UpdatePerson(PersonDAL personDal)
        {
            Console.WriteLine("Updating person");
            Console.WriteLine("Input PersonID: ");
            string connStr = ConfigurationManager.ConnectionStrings["TraidingCompany"].ConnectionString;
            var perDal = new PersonDAL(connStr);
            GetAllPerson(perDal);

            string ids = Console.ReadLine();
            int id = Convert.ToInt32(ids);

            PersonDTO myPerson = personDal.GetPersonById(id);
            myPerson.RowUpdateTime = DateTime.UtcNow;

            if (myPerson is null)
            {
                Console.WriteLine("Invalid input!\n");
                return;
            }

            Console.WriteLine("~ Updating Person:",
            myPerson.ID_Role,
            myPerson.FirstName,
            myPerson.LastName,
            myPerson.Login,
            myPerson.Password
            );
                while (true)
                {
                    Console.WriteLine(@"
        1 - update Id_Role
        2 - update FirsName
        3 - update LastName
        4 - update Login
        5 - update Password

");

                    string m = Console.ReadLine();
                    Console.WriteLine("\t");
                    switch (m)
                    {
                        case "1":
                        myPerson.RowUpdateTime = DateTime.UtcNow;
                        Console.WriteLine("Input RolesID: ");
                        var rolDal = new RolesDAL(connStr);
                        RolesCommand.GetAllRoles(rolDal);
                        string idst = Console.ReadLine();
                        int idr = Convert.ToInt32(ids);
                        myPerson.ID_Role = idr;
                        myPerson = personDal.UpdatePerson(myPerson, id);
                        Console.WriteLine($"Updated successfully!");
                        break;
                        
                        case "2":
                        Console.WriteLine("Input new First Name: ");
                        myPerson.FirstName = Console.ReadLine();
                        myPerson.RowUpdateTime = DateTime.UtcNow;
                        myPerson = personDal.UpdatePerson(myPerson, id);
                        Console.WriteLine($"Updated successfully!");
                        break;
                            
                        case "3":
                        Console.WriteLine("Input new Last Name: ");
                        myPerson.LastName = Console.ReadLine();
                        myPerson.RowUpdateTime = DateTime.UtcNow;
                        myPerson = personDal.UpdatePerson(myPerson, id);
                        Console.WriteLine($"Updated successfully!");
                        break;
                            
                        case "4":
                        Console.WriteLine("Input new Login: ");
                        myPerson.Login = Console.ReadLine();
                        myPerson.RowUpdateTime = DateTime.UtcNow;
                        myPerson = personDal.UpdatePerson(myPerson, id);
                        Console.WriteLine($"Updated successfully!");
                        break;
                            
                        case "5":
                        Console.WriteLine("Input new Password: ");
                        //myPerson.Password = Console.ReadLine();
                        myPerson.RowUpdateTime = DateTime.UtcNow;
                        myPerson = personDal.UpdatePerson(myPerson, id);
                        Console.WriteLine($"Updated successfully!");
                        break;
                        case "0": return;

                    }
                }
        }
    }
}
