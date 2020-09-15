using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class ManageUserController : Controller
    {
        // GET: ManageUser


        [Authorize]
        public ActionResult Index()
        {
            ViewBag.ResultMessage = TempData["ResultMessage"];


            var userName = User.Identity.GetUserName();

            if( userName == "Admin")
            {
                using (Models.UserEntities db = new Models.UserEntities())
                {   //抓取所有AspNetUsers中的資料並放入Models.ManageUser中
                    var result = (from s in db.AspNetUsers
                                  select new Models.ManageUser
                                  {
                                      Id = s.Id,
                                      UserName = s.UserName,
                                      Email = s.Email

                                  }).ToList();


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
        public ActionResult Edit(string id)
        {
            using (Models.UserEntities db = new Models.UserEntities())
            {
                var result = (from s in db.AspNetUsers
                              where s.Id == id
                              select new Models.ManageUser
                              {
                                  Id = s.Id,
                                  UserName = s.UserName,
                                  Email = s.Email
                              }).FirstOrDefault();
                if (result != default(Models.ManageUser))
                {
                    return View(result);
                }
            }
            //設定錯誤訊息
            TempData["ResultMessage"] = String.Format("使用者[{0}]不存在，請重新操作", id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Models.ManageUser postback)
        {
            using (Models.UserEntities db = new Models.UserEntities())
            {
                var result = (from s in db.AspNetUsers
                              where s.Id == postback.Id
                              select s).FirstOrDefault();
                if (result != default(Models.AspNetUser))
                {
                    result.UserName = postback.UserName;
                    result.Email = postback.Email;
                    db.SaveChanges();
                    //設定成功訊息
                    TempData["ResultMessage"] = String.Format("使用者[{0}]成功編輯", postback.UserName);
                    return RedirectToAction("Index");
                }
            }
            //設定錯誤訊息
            TempData["ResultMessage"] = String.Format("使用者[{0}]不存在，請重新操作", postback.UserName);
            return RedirectToAction("Index");
        }
    }
}