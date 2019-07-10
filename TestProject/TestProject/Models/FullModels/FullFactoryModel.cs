using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Models.FullModels;

namespace TestProject.Models.FullModels
{
    public class FullFactoryModel
    {
        public string name { get; set; }
        public List<FactoryRecipe> recipes { get; set; }
        public string currentRecipe { get; set; }
        public int ticksLeft { get; set; }
    }
}
