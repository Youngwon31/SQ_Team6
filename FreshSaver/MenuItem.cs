using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshSaver
{
    public class MenuItem
    {
        public int MenuItemID { get; set; }
        public string MenuName { get; set; }
        public string Description { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal DiscountedPrice { get; set; }
        public int Stock { get; set; }
        public string ImageURL { get; set; }
        public int Quantity { get; set; }
        public decimal ItemTotal { get; set; }
        public string ItemURL { get; set; }
    }
}