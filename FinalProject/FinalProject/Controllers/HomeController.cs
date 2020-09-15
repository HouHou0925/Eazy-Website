﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;


namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (Models.ItemEntities db = new Models.ItemEntities())
            {
                var result = (from s in db.Products select s).ToList();
                return View(result);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Details(int id)
        {
            using (Models.ItemEntities db = new Models.ItemEntities())
            {
                var result = (from s in db.Products
                              where s.Id == id
                              select s).FirstOrDefault();

                if (result == default(Models.Product))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(result);
                }
            }
        }

        [HttpPost]  // 限定使用POST
        [Authorize] // 登入會員才可留言
        public ActionResult AddComment(int id, string Content)
        {
           


          

                var userId = HttpContext.User.Identity.GetUserId(); //取得目前登入使用者Id
                





                var currentDateTime = DateTime.Now;

                var comment = new Models.ProductCommet()
                {
                    ProductId = id,
                    Content = Content,
                    UserId = userId,
                    CreateDate = currentDateTime,
                    UserName = User.Identity.GetUserName() 
                };

                using (Models.ItemEntities db = new Models.ItemEntities())
                {
                    db.ProductCommets.Add(comment);
                    db.SaveChanges();
                }

                return RedirectToAction("Details", new { id = id });
            
        }




    }
}