using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class ProductCategoryDao
    {
        OnlineShopeeDbContext db = null;

        public ProductCategoryDao()
        {
            db = new OnlineShopeeDbContext();
        }
        public Content GetbyID(long id)
        {
            return db.Contents.Find(id);
        }
        public IEnumerable<ProductCategory> ListAllCategory(string searchString, int page, int pageSize)
        {
            //var pageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"].ToString);
            IQueryable<ProductCategory> model = db.ProductCategories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedBy).ToPagedList(page, pageSize);
        }
        public bool Insert(ProductCategory entity)
        {
            db.ProductCategories.Add(entity);
            db.SaveChanges();
            return true;
        }
        public List<ProductCategory> ListAllCategory()
        {
            return db.ProductCategories.Where(x => x.Status == true).ToList();
        }
        public ProductCategory GetByID(long id)
        {
            return db.ProductCategories.Find(id);
        }
        public bool Update(ProductCategory entity)
        {
            try
            {
                var category = db.ProductCategories.Find(entity.ID);
                category.Name = entity.Name;
                category.MetaTitle = entity.MetaTitle;
                category.ParentID = entity.ParentID;
                category.SeoTitle = entity.MetaTitle;
                category.MetaKeywords = entity.MetaTitle;
                category.MetaDescription = entity.MetaTitle;
                category.ShowOrHome = entity.ShowOrHome;
                category.CreatedDate = entity.CreatedDate;
                category.CreatedBy = entity.CreatedBy;
                category.Status = entity.Status;
                category.ParentID = entity.ParentID;
                category.ModifiedBy = entity.ModifiedBy;
                category.ModifiedDate = DateTime.Now;
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
            var entity = db.ProductCategories.Find(id);
            db.ProductCategories.Remove(entity);
            db.SaveChanges();
            return true;
        }
        public List<ProductCategory> ListAll()
        {
            return db.ProductCategories.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }
    }
}
