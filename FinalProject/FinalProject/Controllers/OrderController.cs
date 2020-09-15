using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace FinalProject.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }


        [Authorize]
        [HttpPost]
        public ActionResult Index(Models.OrderModel.Ship postback)
        {
            if (this.ModelState.IsValid)
            {   
                var currentcart = Models.Cart.Operation.GetCurrentCart();//取得目前購物車

                
                var userId = HttpContext.User.Identity.GetUserId();//取得登入使用者Id

                using (Models.ItemEntities db = new Models.ItemEntities())
                {
                    
                    var order = new Models.Order()
                    {
                        UserId = userId,
                        RecieverName = postback.RecieverName,
                        RecieverPhone = postback.RecieverPhone,
                        RecieverAddress = postback.RecieverAddress
                    };
                   
                    db.Orders.Add(order);   //加其入Orders資料表後，儲存變更
                    db.SaveChanges();

                    
                    var orderDetails = currentcart.ToOrderDetailList(order.Id);//取得購物車中OrderDetai物件


                    db.OrderDetails.AddRange(orderDetails);//將其加入OrderDetails資料表後，儲存變更
                    db.SaveChanges();
                }

                var currentCart = Models.Cart.Operation.GetCurrentCart();
                currentCart.ClearCart();
                return Redirect("/Order/MyOrder");
            }
            return View();
        }

        [Authorize]
        public ActionResult MyOrder()
        {
            var UserId = HttpContext.User.Identity.GetUserId();
            using (Models.ItemEntities db = new Models.ItemEntities())
            {
                var result = (from s in db.Orders where s.UserId == UserId select s).ToList();

                return View(result);
            }

        }

        [Authorize]
        public ActionResult MyOrderDetail(int id)
        {

            using (Models.ItemEntities db = new Models.ItemEntities())
            {
                var result = (from s in db.OrderDetails where s.OrderId == id select s).ToList();

                if(result.Count == 0)
                {
                    return RedirectToAction("Index");

                }
                else
                {
                    return View(result);
                }


            }


        }



    }
}