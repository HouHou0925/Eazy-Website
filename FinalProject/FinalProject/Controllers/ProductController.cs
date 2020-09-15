using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace FinalProject.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        [Authorize]
        public ActionResult Index()
        {
            var userName = User.Identity.GetUserName();
            if( userName == "Admin")
            {

                List<Models.Product> result = new List<Models.Product>();

                ViewBag.ResultMessage = TempData["ResultMsg"];

                using (Models.ItemEntities db = new Models.ItemEntities())
                {
                    result = (from s in db.Products select s).ToList();
                    return View(result);

                }
            }
            else
            {
                return Content("<script>alert('權限不足 滾啦!');history.go(-1);</script>");
                //return Content("<script>alert('權限不足 滾啦!');window.location.href='../Home/Index';</script>");
                //return Redirect("/Home/Index");
            }

        }

        [Authorize]
        public ActionResult Creat()
        {
            return View();
        }


        [HttpPost]
        [Authorize]
        public ActionResult Creat(Models.Product postinfo)
        {

            if (this.ModelState.IsValid)
            {
                using (Models.ItemEntities db = new Models.ItemEntities())
                {
                    postinfo.DefaultImageName = postinfo.DefaultImageName + ".png";
                    db.Products.Add(postinfo);
                    db.SaveChanges();

                    TempData["ResultMsg"] = string.Format("商品[{0}]成功建立", postinfo.Name);

                }
                return RedirectToAction("Index");

            }

            ViewBag.ResultMessage = "資料有誤";

            return View(postinfo);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            using (Models.ItemEntities db = new Models.ItemEntities())
            {
                var result = (from s in db.Products where s.Id == id select s).FirstOrDefault();
                if (result != default(Models.Product))  //驗證是否有資料存在
                {
                    return View(result);
                }
                else
                {
                    TempData["ResultMsg"] = "資料有誤";
                    return RedirectToAction("Index");
                }
            }


        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Models.Product postinfo)
        {
            if (this.ModelState.IsValid)
            {
                using (Models.ItemEntities db = new Models.ItemEntities())
                {
                    var result = (from s in db.Products where s.Id == postinfo.Id select s).FirstOrDefault();
                    result.Name = postinfo.Name;
                    result.PublishDate = postinfo.PublishDate;
                    result.Status = postinfo.Status;
                    result.Price = postinfo.Price;
                    result.Quantity = postinfo.Quantity;
                    result.CategoryID = postinfo.CategoryID;
                    result.Description = postinfo.Description;
                    result.DefaultImageId = postinfo.DefaultImageId;
                    result.DefaultImageName = postinfo.DefaultImageName;

                    db.SaveChanges();
                    TempData["ResultMsg"] = string.Format("商品[{0}]成功修改", postinfo.Name);
                    return RedirectToAction("Index");


                }
            }
            else
            {
                return View(postinfo);
            }

        }

        [HttpPost]
        [Authorize]
        public ActionResult Delete(int id)
        {

            using (Models.ItemEntities db = new Models.ItemEntities())
            {
                var result = (from s in db.Products where s.Id == id select s).FirstOrDefault();
                if (result != default(Models.Product))  //資料判斷有無
                {
                    db.Products.Remove(result);
                    db.SaveChanges();
                    TempData["ResultMsg"] = string.Format("商品[{0}]成功刪除", result.Name);
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ResultMsg"] = "指定資料不存在";
                    return RedirectToAction("Index");
                }


            }

        }
    }
}