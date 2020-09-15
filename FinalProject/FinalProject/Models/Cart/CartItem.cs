using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models.Cart   //購物車內 單一商品使用
{
    [Serializable] //可序列化
    public class CartItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string DefaultImageName { get; set; }

        public decimal Amount
        {
            get
            {
                return this.Price * this.Quantity;
            }
        }





    }
}