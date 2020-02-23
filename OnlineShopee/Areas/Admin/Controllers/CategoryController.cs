using Models.Dao;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopee.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new CategoryDao();
            var model = dao.ListAllCategory(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category entity)
        {
            var result = new CategoryDao().Insert(entity);
            if (result)
            {
                SetAlert("Tạo danh mục thành công ", "success");
                return RedirectToAction("Index", "Category");
            }
            else
            {
                ModelState.AddModelError("", "Tạo thất bại !!!");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var category = new CategoryDao().GetByID(id);
            return View(category);
        }
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            bool result = new CategoryDao().Update(category);
            if (result)
            {
                SetAlert("Update danh mục thành công ", "success");
                return RedirectToAction("Index", "Category");
            }
            else
            {
                ModelState.AddModelError("", "Update thất bại !!!");
            }
            return View();
        }
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            bool result = new CategoryDao().Delete(id);
            if (result)
            {
                SetAlert("Xóa danh mục thành công ", "success");
                return RedirectToAction("Index", "Category");
            }
            else
            {
                ModelState.AddModelError("", "Xóa thất bại !!!");
            }
            return View("Index");
        }
        [HttpPost]
        public JsonResult Slug(string slug)
        {
            string result = ToUrlSlug(slug);
            return Json(new
            {
                slug = result
            });
        }
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new CategoryDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}