using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.Models.FullModels
{
    public class FactoryRecipe
    {
        public string recipe { get; set; }
        public int duration { get; set; }
        public List<RecipeComponent> components { get; set; }
        public string product { get; set; }
        public int productAmout { get; set; }
    }
}
