using System.Collections.Generic;

namespace Models
{
    public class DataModel
    {
        public List<ProductModel> products { get; set; }
        public List<RecipeModel> recipes { get; set; }
        public List<ProjectModel> projects { get; set; }
        public List<BuildingModel> buildings { get; set; }
    }
}
