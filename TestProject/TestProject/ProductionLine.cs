using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestProject.Models;
using TestProject.Models.FullModels;

namespace TestProject
{
    public class ProductionLine
    {
        bool _hasEnoughResources;
        List<FullFactoryModel> _factories;
        Warehouse _warehouse;
        List<string> logs = new List<string>();
        int _tick = 0;

        public ProductionLine(DataModel dataModel)
        {
            _warehouse = new Warehouse();
            _factories = new List<FullFactoryModel>();

            TransformModel(dataModel);
        }

        public List<string> GetLogs()
        {
            logs.Add($"totalTime: {_tick}");
            logs.Add(_warehouse.PrintAllProducts());
            return logs;
        }

        public bool HasResourcesForNextTick()
        {
            if (_factories.Any(m => !String.IsNullOrEmpty(m.currentRecipe) && m.ticksLeft > 0))
            {
                return true;
            }
            else
            {
                return _factories.Any(m => String.IsNullOrEmpty(m.currentRecipe) && m.ticksLeft == 0 && m.recipes.Any(n => n.components.All(x => _warehouse.HasItemsInWarehouse(x.name, x.amount))));
            }
        }

        public void NextTick()
        {
            _factories.Where(m => !String.IsNullOrEmpty(m.currentRecipe) && m.ticksLeft > 0).ToList().ForEach(m =>
             {
                 m.ticksLeft--;
                 if (m.ticksLeft == 0)
                 {
                     var recipe = m.recipes.FirstOrDefault(rec => rec.recipe == m.currentRecipe);
                     _warehouse.PutItemsToWarehouse(recipe.product, recipe.productAmout);
                     m.currentRecipe = String.Empty;
                     logs.Add($"time: {_tick},	factory: {m.name}, recipe: {recipe.recipe}");
                 }
             });

            _factories.Where(m => String.IsNullOrEmpty(m.currentRecipe) && m.ticksLeft == 0).ToList().ForEach(m =>
            {
                var recipeFroProduction = m.recipes.FirstOrDefault(n => n.components.All(x => _warehouse.HasItemsInWarehouse(x.name, x.amount)));
                if (recipeFroProduction == null) return;
                recipeFroProduction.components.ForEach(component =>
                {
                    _warehouse.GetItemsFromWarehouse(component.name, component.amount);
                });

                m.currentRecipe = recipeFroProduction.recipe;
                m.ticksLeft = recipeFroProduction.duration;
            });

            _tick++;
        }

        private void TransformModel(DataModel dataModel)
        {
            // Filling factories
            dataModel.buildings.ForEach(building =>
            {
                var project = dataModel.projects.FirstOrDefault(proj => proj.name == building.project);
                var factoryRecipes = new List<FactoryRecipe>();
                project.abilities.ForEach(ability =>
                {

                    var recipe = dataModel.recipes.FirstOrDefault(rec => rec.name == ability.name);

                    factoryRecipes.Add(new FactoryRecipe()
                    {
                        recipe = recipe.name,
                        duration = ability.duration,
                        product = recipe.product.id,
                        productAmout = recipe.product.num,
                        components = recipe.components.Select(comp => new RecipeComponent() { amount = comp.num, name = comp.id }).ToList()
                    });
                });

                _factories.Add(new FullFactoryModel() { name = building.name, recipes = factoryRecipes });
            });

            // Filling warehouse
            dataModel.products.ForEach(m =>
            {
                _warehouse.PutItemsToWarehouse(m.id, m.num);
            });
        }
    }
}
