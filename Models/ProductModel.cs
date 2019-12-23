using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BazarDeLaHess.Models
{
    public class ProductModel
    {
        private List<Item> products;

        public ProductModel()
        {
            this.products = new List<Item>() {
                new Item {
                    id_item = 1,
                    name = "Tabouret haut",
                    description = "Un tabouret pas mal haut ...",
                    available = true,
                    price = 10,
                    picture = "000001.jpg"
                },
                new Item {
                    id_item = 2,
                    name = "Tabouret noir + pied",
                    description = "Un tabouret noir et un endroit où posser ses pieds.",
                    available = true,
                    price = 15,
                    picture = "000002.jpg"
                },
                new Item {
                    id_item = 3,
                    name = "Tabouret moche",
                    description = "Un tabouret petit et moche",
                    available = true,
                    price = 50,
                    picture = "000003.jpg"
                }
            };
        }

        public List<Item> findAll()
        {
            return this.products;
        }

        public Item find(int id)
        {
            return this.products.Single(p => p.id_item.Equals(id));
        }
    }
}