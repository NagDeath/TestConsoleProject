using System.Collections.Generic;

namespace Models
{
    public class RecipeModel
    {
        public string name { get; set; }
        public List<ComponentModel> components { get; set; }
        public ProductModel product { get; set; }
    }
}