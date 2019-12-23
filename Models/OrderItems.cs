using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BazarDeLaHess.Models
{
    public class OrderItems
    {
        private int quantity;
        public int id_order { get; set; }
        public Item item { get; set; }
        public int Quantity 
        { 
            get { return this.quantity; } 
            set { 
                this.quantity = value;
                this.total_price = this.quantity * this.item.price;
            } 
        }
        public float total_price { get; set; }
    }
}