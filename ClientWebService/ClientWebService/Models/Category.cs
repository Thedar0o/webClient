using System.Collections.Generic;

namespace ClientWebService.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryID { get; set; }

        //Navigation props to SubCategory 
        public virtual ICollection<SubCategory> SubCategory { get; set; }
    }
}