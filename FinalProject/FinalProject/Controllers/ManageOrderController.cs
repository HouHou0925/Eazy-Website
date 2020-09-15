using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace FinalProject.Controllers
{
    public class ManageOrderController : Controller
    {
        // GET: ManageOrder
        [Authorize]
        public ActionResult Index()
        {
            var userName = User.Identity.GetUserName();
            if( userName == "Admin")
            {

                using (Models.ItemEntities db = new Models.ItemEntities())
                {

                    var result = (from s in db.Orders select s).ToList();

                    return View(result);
                }

            }

            else
            {
                return Content("<script>alert('權限不足 滾啦!');history.go(-1);</script>");
                //return Content("<script>alert('權限不足 滾啦!');window.location.href='../Home/Index';</script>");
            }

        }


        public ActionResult Details(int id)
        {


            using (Models.ItemEntities db = new Models.ItemEntities())
            {

                var result = (from s in db.OrderDetails where s.OrderId == id select s).ToList();

                if (result.Count == 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {

                    return View(result);

                }
            }



        }


        public ActionResult ShowAll()
        {
            using (Models.ItemEntities db = new Models.ItemEntities())
            {

                var result = (from s in db.Orders select s).ToList();

                return View("Index", result);
            }
        }


        public ActionResult SerachByUserName( string name )
        {


            string searchUserId = null;
            using (Models.UserEntities db = new Models.UserEntities())   //查詢目前網站使用者暱稱符合UserName的UserId
            {   
                searchUserId = (from s in db.AspNetUsers
                                where s.UserName == name
                                select s.Id).FirstOrDefault();
            }


            
            if (!String.IsNullOrEmpty(searchUserId))
            {  
                using (Models.ItemEntities db = new Models.ItemEntities())    //如果有存在UserId則將此UserId的所有訂單找出
                {
                    var result = (from s in db.Orders
                                  where s.UserId == searchUserId
                                  select s).ToList();
                    
                    return View("Index", result);
                }
            }
            else
            {  
                return View("Index", new List<Models.Order>());
            }



        }





    }
}