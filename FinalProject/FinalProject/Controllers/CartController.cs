using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        //取得目前購物車頁面

        public ActionResult GetCart()
        {
            return PartialView("_CartPartial");
        }

        //以id加入Product至購物車，並回傳購物車頁面
        [Authorize] //登入會員才可新增
        public ActionResult AddToCart(int id)
        {
            var currentCart = Models.Cart.Operation.GetCurrentCart();
            currentCart.AddProduct(id);
            return PartialView("_CartPartial");
        }

        //以id移除購物車Product，並回傳購物車頁面
        [Authorize]
        public ActionResult RemoveFromCart(int id)
        {
            var currentCart = Models.Cart.Operation.GetCurrentCart();
            currentCart.RemoveProduct(id);
            return PartialView("_CartPartial");
        }

        //清空購物車，並回傳購物車頁面
        [Authorize]
        public ActionResult ClearCart()
        {
            var currentCart = Models.Cart.Operation.GetCurrentCart();
            currentCart.ClearCart();
            return PartialView("_CartPartial");
        }


    }
}