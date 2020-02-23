using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class CategoryDao
    {
        OnlineShopeeDbContext db = null;

        public CategoryDao()
        {
            db = new OnlineShopeeDbContext();
        }
        public IEnumerable<Category> ListAllCategory(string searchString, int page, int pageSize)
        {
            //var pageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"].ToString);
            IQueryable<Category> model = db.Categories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedBy).ToPagedList(page, pageSize);
        }
        public List<Category> ListAll()
        {
            return db.Categories.Where(x => x.Status == true).ToList();
        }
        public Category GetByID(long id)
        {
            return db.Categories.Find(id);
        }
        public bool Insert(Category entity)
        {
            db.Categories.Add(entity);
            db.SaveChanges();
            return true;
        }
        public bool Update(Category entity)
        {
            try
            {
                var category = db.Categories.Find(entity.ID);
                category.Name = entity.Name;
                category.MetaTitle = entity.MetaTitle;
                category.ParentID = entity.ParentID;
                category.DisplayOrder = entity.DisplayOrder;
                category.SeoTitle = entity.SeoTitle;
                category.CreatedBy = entity.CreatedBy;
                category.CreatedDate = entity.CreatedDate;
                category.ModifiedDate = DateTime.Now;
                category.ModifiedBy = entity.ModifiedBy;
                category.MetaDescription = entity.MetaDescription;
                category.MetaKeywords = entity.MetaKeywords;
                category.ShowOrHome = entity.ShowOrHome;
                category.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Delete(long id)
        {
            var category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return true;
        }
        public bool ChangeStatus(long id)
        {
            var category = db.Categories.Find(id);
            category.Status = !category.Status;
            db.SaveChanges();
            return category.Status;
        }
    }
}
