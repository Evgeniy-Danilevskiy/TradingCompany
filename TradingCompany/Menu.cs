using DAL.ADO;
using System;
using System.Configuration;


namespace TradingCompany
{
    public static class Menu
    {
        public static string connStr = ConfigurationManager.ConnectionStrings["TraidingCompany"].ConnectionString;
        static bool bbooll = true;

        public static void ShowMenu()
        {
            while (bbooll)
            {
                Console.WriteLine(@"
            TRAIDING COMPANY - Goods receiver.
        1 - CATEGORY
        2 - PERSON
        3 - ROLES
        4 - SUPPLY
      0 - return

");
                string f = Console.ReadLine();
                Console.WriteLine("\t");
                switch (f)
                {
                    case "1":
                        MenuCategory(connStr);
                        break;
                    case "2":
                        MenuPerson(connStr);
                        break;
                    case "3":
                        MenuRoles(connStr);
                        break;
                    case "4":
                        MenuSupply(connStr);
                        break;
                    case "0":
                        bbooll = false;
                        break;
                }
            }
        }

        private static void MenuSupply(string connStr)
        {
            var supplyDal = new SupplyDAL(connStr);
            while (bbooll)
            {
                Console.WriteLine(@"
            TRAIDING COMPANY - Goods receiver.
                ***SUPPLY***
                1 - Create
                2 - Read
                3 - Update
                4 - Delete
              0 - Show main menu

");
                string f = Console.ReadLine();
                Console.WriteLine("\t");
                switch (f)
                {
                    case "1":
                        SupplyCommand.CreateSupply(supplyDal);
                        break;
                    case "2":
                        SupplyCommand.GetAllSupply(supplyDal);
                        break;
                    case "3":
                        SupplyCommand.UpdateSupply(supplyDal);
                        break;
                    case "4":
                        SupplyCommand.DeleteSupply(supplyDal);
                        break;
                    case "0":
                        ShowMenu();
                        bbooll = false;
                        break;
                }
            }
        }

        private static void MenuRoles(string connStr)
        {
            var rolesDal = new RolesDAL(connStr);
            while (bbooll)
            {
                Console.WriteLine(@"
            TRAIDING COMPANY - Goods receiver.
                ***ROLES***
                1 - Create
                2 - Read
                3 - Update
                4 - Delete
              0 - Show main menu

");
                string f = Console.ReadLine();
                Console.WriteLine("\t");
                switch (f)
                {
                    case "1":
                        RolesCommand.CreateRoles(rolesDal);
                        break;
                    case "2":
                        RolesCommand.GetAllRoles(rolesDal);
                        break;
                    case "3":
                        RolesCommand.UpdateRoles(rolesDal);
                        break;
                    case "4":
                        RolesCommand.DeleteRoles(rolesDal);
                        break;
                    case "0":
                        ShowMenu();
                        bbooll = false;
                        break;
                }
            }
        }

        private static void MenuPerson(string connStr)
        {
            var personDal = new PersonDAL(connStr);
            while (bbooll)
            {
                Console.WriteLine(@"
            TRAIDING COMPANY - Goods receiver.
                ***PERSON***
                1 - Create
                2 - Read
                3 - Update
                4 - Delete
              0 - Show main menu

");
                string f = Console.ReadLine();
                Console.WriteLine("\t");
                switch (f)
                {
                    case "1":
                        PersonCommand.CreatePerson(personDal);
                        break;
                    case "2":
                        PersonCommand.GetAllPerson(personDal);
                        break;
                    case "3":
                        PersonCommand.UpdatePerson(personDal);
                        break;
                    case "4":
                        PersonCommand.DeletePerson(personDal);
                        break;
                    case "0":
                        ShowMenu();
                        bbooll = false;
                        break;
                }
            }
        }

        private static void MenuCategory(string connStr)
        {
            var categoryDal = new CategoryDAL(connStr);
            while (bbooll)
            {
                Console.WriteLine(@"
            TRAIDING COMPANY - Goods receiver.
                ***CATEGORY***
                1 - Create
                2 - Read
                3 - Update
                4 - Delete
              0 - Show main menu

");
                string f = Console.ReadLine();
                Console.WriteLine("\t");
                switch (f)
                {
                    case "1":
                        CategoryCommand.CreateCategory(categoryDal);
                        break;
                    case "2":
                        CategoryCommand.GetAllCategory(categoryDal);
                        break;
                    case "3":
                        CategoryCommand.UpdateCategory(categoryDal);
                        break;
                    case "4":
                        CategoryCommand.DeleteCategory(categoryDal);
                        break;
                    case "0":
                        ShowMenu();
                        bbooll = false;
                        break;
                }
            }
        }
    }
}
