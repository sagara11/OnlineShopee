using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.Dao;
using Models.EF;

namespace OnlineShopee.Areas.Admin.Controllers
{
    public class ContentController : BaseController
    {
        // GET: Admin/Content
        public ActionResult Index(string searchString, int page=1, int pageSize = 10)
        {
            var model = new ContentDao().ListAllContent(searchString, page, pageSize);
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Content model)
        {
            if(ModelState.IsValid)
            {
                var dao = new ContentDao();
                bool created = dao.Insert(model);
                if(created)
                {
                    SetAlert("Thêm bài viết thành công ", "success");
                    return RedirectToAction("Index", "Content");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm thất bại !!!");
                }
            }
            SetViewBag();
            return View();
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var dao = new ContentDao();
            var content = dao.GetbyID(id);
            SetViewBag(content.CategoryID);
            return View(content);
        }
        [HttpPost]
        public ActionResult Edit(Content model)
        {
            if (ModelState.IsValid)
            {
                var dao = new ContentDao();
                bool result = dao.Update(model);
                if (result)
                {
                    SetAlert("Update bài viết thành công ", "success");
                    return RedirectToAction("Index", "Content");
                }
                else
                {
                    ModelState.AddModelError("", "Update thất bại !!!");
                }
            }
            SetViewBag(model.CategoryID);
            return View();
        }
        public void SetViewBag(long? selectedId = null)
        {
            var dao = new CategoryDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            bool result = new ContentDao().Delete(id);
            if (result)
            {
                SetAlert("Xóa bài viết thành công ", "success");
                return RedirectToAction("Index", "Content");
            }
            else
            {
                ModelState.AddModelError("", "Xóa thất bại !!!");
            }
            return View("Index");
        }
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new ContentDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}