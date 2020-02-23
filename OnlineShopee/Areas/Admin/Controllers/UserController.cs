using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.Dao;
using Models.EF;
using OnlineShopee.Common;

namespace OnlineShopee.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(User user)
        {
            var dao = new UserDao();
            var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
            user.Password = encryptedMd5Pas;
            long id = dao.Insert(user);
            if (id > 0)
            {
                SetAlert("Thêm user thành công ", "success");
                return RedirectToAction("Index", "User");
            }
            else
            {
                ModelState.AddModelError("", "Thêm thất bại !!!");
            }
            return View("Index");
        }
        public ActionResult Edit(int id)
        {
            var user = new UserDao().ViewDetail(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(Content content)
        {
            var dao = new ContentDao();
            bool result = dao.Update(content);
            if (result)
            {
                SetAlert("Update bài viết thành công ", "success");
                return RedirectToAction("Index", "Content");
            }
            else
            {
                ModelState.AddModelError("", "Update thất bại !!!");
            }
            return View("Index");
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new UserDao().Delete(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new UserDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}