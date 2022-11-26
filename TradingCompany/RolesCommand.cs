using System;
using DAL.ADO;
using DTO;

namespace TradingCompany
{
    public class RolesCommand
    {
        public static void DeleteRoles(RolesDAL rolesDal)
        {
            Console.WriteLine("Deleting Role");
            Console.WriteLine("Input RoleID: ");
            string ids = Console.ReadLine();
            int id = Convert.ToInt32(ids); rolesDal.DeleteRoles(id);
            Console.WriteLine("Successfully deleted");
        }


        public static void CreateRoles(RolesDAL rolesDal)
        {
            Console.WriteLine("Creating New Roles");
            Console.WriteLine("Input Role Name: ");
            string _roleName = Console.ReadLine();

            RolesDTO myRoles = new RolesDTO
            {
                RoleName = _roleName
            };

            try
            {
                myRoles = rolesDal.CreatRoles(myRoles);
                Console.WriteLine($"New RoleID is {myRoles.ID_Role}");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("Data inputted incorrectly.");
            }
        }

        public static void GetAllRoles(RolesDAL rolesDal)
        {
            var roles = rolesDal.GetAllRoles();
            Console.WriteLine("Roles:");
            foreach (var r in roles)
            {
                Console.WriteLine($"{r.ID_Role,8} | {r.RoleName,11} | {r.RowInsertTime,12} |");
            }
        }

        public static void UpdateRoles(RolesDAL rolesDal)
        {
            Console.WriteLine("Updating role");
            Console.WriteLine("Input RoleID: ");

            string ids = Console.ReadLine();
            int id = Convert.ToInt32(ids);

            RolesDTO myRoles = rolesDal.GetRolesById(id);

            if (myRoles is null)
            {
                Console.WriteLine("Invalid input!\n");
                return;
            }

            Console.WriteLine(" Updating Role:",
            myRoles.RoleName);
            try
            {
                Console.WriteLine("Input new Role: ");
                myRoles.RoleName = Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("ERROR.");
            }
            myRoles.RowUpdateTime = DateTime.UtcNow;
            myRoles = rolesDal.UpdateRoles(myRoles, id);
            Console.WriteLine($"Updated successfully!");
        }
    }
}
