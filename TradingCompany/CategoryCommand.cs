using System;
using DAL.ADO;
using DTO;

namespace TradingCompany
{
    public class CategoryCommand
    {
        public static void DeleteCategory(CategoryDAL categoryDal)
        {
            Console.WriteLine("Deleting Category");
            Console.WriteLine("Input CategoryID: ");
            string ids = Console.ReadLine();
            int id = Convert.ToInt32(ids); categoryDal.DeleteCategory(id);
            Console.WriteLine("Successfully deleted");
        }

        public static void UpdateCategory(CategoryDAL categoryDal)
        {
            Console.WriteLine("Input CategoryID: ");
            string ids = Console.ReadLine();
            int id = Convert.ToInt32(ids);

            CategoryDTO myCategory = categoryDal.GetCategoryById(id);

            if (myCategory is null)
            {
                Console.WriteLine("Invalid input!\n");
                return;
            }

            Console.WriteLine("Updating Category:",
            myCategory.CategoryName);
            try
            {
                myCategory.RowUpdateTime = DateTime.UtcNow;
                Console.WriteLine("Input new Category: ");
                myCategory.CategoryName = Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("ERROR.");
            }
            myCategory = categoryDal.UpdateCategory(myCategory,id);
            Console.WriteLine($"Updated successfully!");

        }



        public static void CreateCategory(CategoryDAL categoryDal)
        {
            Console.WriteLine("Creating New Category");
            Console.WriteLine("Input Category Name: ");
            string _categoryName = Console.ReadLine();

            CategoryDTO myCategory = new CategoryDTO
            {
                CategoryName = _categoryName
            };

            try
            {
                myCategory = categoryDal.CreateCategory(myCategory);
                Console.WriteLine($"New CategoryID is {myCategory.ID_Category}");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("Data inputted incorrectly.");
            }
        }

        public static void GetAllCategory(CategoryDAL categoryDal)
        {
            var category = categoryDal.GetAllCategory();
            Console.WriteLine("Category:");
            foreach (var c in category)
            {
                Console.WriteLine($" {c.ID_Category,7} |  {c.CategoryName,25} |  {c.RowInsertTime,15} |  {c.RowUpdateTime,15} |");
            }
        }
    }
}
