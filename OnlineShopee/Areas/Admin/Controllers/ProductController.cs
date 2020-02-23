using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.Dao;
using Models.EF;

namespace OnlineShopee.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ProductDao();
            var model = dao.ListAllProduct(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBagCategory();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            bool result = new ProductDao().Insert(product);
            if (result)
            {
                SetAlert("Tạo sản phẩm thành công ", "success");
                return RedirectToAction("Index", "Product");
            }
            else
            {
                ModelState.AddModelError("", "Tạo thất bại !!!");
            }
            SetViewBagCategory();
            return View();
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var product = new ProductDao().GetByID(id);
            SetViewBagCategory(product.CategoryID);
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            bool result = new ProductDao().Update(product);
            if (result)
            {
                SetAlert("Update sản phẩm thành công ", "success");
                return RedirectToAction("Index", "Product");
            }
            else
            {
                ModelState.AddModelError("", "Update thất bại !!!");
            }
            SetViewBagCategory(product.CategoryID);
            return View();
        }
        public void SetViewBagCategory(long? selectedId = null)
        {
            var dao = new ProductCategoryDao();
            ViewBag.CategoryID = new SelectList(dao.ListAllCategory(), "ID", "Name", selectedId);
        }
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            bool result = new ProductDao().Delete(id);
            if (result)
            {
                SetAlert("Xóa sản phẩm thành công ", "success");
                return RedirectToAction("Index", "Product");
            }
            else
            {
                ModelState.AddModelError("", "Xóa thất bại !!!");
            }
            return View();
        }
    }
}