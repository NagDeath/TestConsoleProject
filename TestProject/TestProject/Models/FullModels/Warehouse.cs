using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestProject.Models.FullModels
{
    public class Warehouse
    {
        private Dictionary<string, int> _items = new Dictionary<string, int>();

        public bool HasItemsInWarehouse(string name, int amount)
        {
            if (!_items.ContainsKey(name)) return false;
            
            if (_items[name] < amount)
                return false;
            else
                return true;
        }

        public void GetItemsFromWarehouse(string name, int amount)
        {           
            _items[name] -= amount;
        }

        public void PutItemsToWarehouse(string name, int amount)
        {
            if (!_items.ContainsKey(name))
            {
                _items[name] = amount;
            }
            else
            {
                _items[name] += amount;
            }
        }

        public string PrintAllProducts()
        {
            return String.Join(',', _items.Select(m => $"{m.Key}: {m.Value}"));
        }


    }
}
