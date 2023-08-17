using NewsPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.Utility
{
    public class Validation
    {
        public static bool IsStringANumber(string str)
        {
            var isNumeric = int.TryParse(str, out _);
            return isNumeric;
        }
        public static bool CategoryNameExists(List<Category> objCategoryList, string Name, int Id)
        {
            foreach (Category objCategory in objCategoryList)
            {
                if (Name.ToLower() == objCategory.Name.ToLower() && Id != objCategory.Id)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
