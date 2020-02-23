using Models.Dao;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopee.Areas.Admin.Controllers
{
    public class ProductCategoryController : BaseController
    {
        // GET: Admin/ProductCategory
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ProductCategoryDao();
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
        public ActionResult Create(ProductCategory product)
        {
            bool result = new ProductCategoryDao().Insert(product);
            if (result)
            {
                SetAlert("Tạo sản phẩm thành công ", "success");
                return RedirectToAction("Index", "ProductCategory");
            }
            else
            {
                ModelState.AddModelError("", "Tạo thất bại !!!");
            }
            return View("Index");
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var category = new ProductCategoryDao().GetByID(id);
            return View(category);
        }
        [HttpPost]
        public ActionResult Edit(ProductCategory category)
        {
            bool result = new ProductCategoryDao().Update(category);
            if (result)
            {
                SetAlert("Update danh mục thành công ", "success");
                return RedirectToAction("Index", "ProductCategory");
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
            bool result = new ProductCategoryDao().Delete(id);
            if (result)
            {
                SetAlert("Xóa danh mục thành công ", "success");
                return RedirectToAction("Index", "ProductCategory");
            }
            else
            {
                ModelState.AddModelError("", "Xóa thất bại !!!");
            }
            return View();
        }
    }
}