using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList.Mvc;
using System.Data.Entity.Validation;

namespace Models.Dao
{
    public class ProductDao
    {
        OnlineShopeeDbContext db = null;

        public ProductDao()
        {
            db = new OnlineShopeeDbContext();
        }
        public IEnumerable<Product> ListAllProduct(string searchString, int page, int pageSize)
        {
            //var pageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"].ToString);
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Description.Contains(searchString) || x.Code.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedBy).ToPagedList(page, pageSize);
        }
        public bool Insert(Product entity)
        {
            try
            {
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges
                db.Products.Add(entity);
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return false;
            }
            return true;
        }
        public Product GetByID(long id)
        {
            return db.Products.Find(id);
        }
        public bool Delete(long id)
        {
            var entity = db.Products.Find(id);
            db.Products.Remove(entity);
            return true;
        }
        public bool Update(Product entity)
        {
            try
            {
                var category = db.Products.Find(entity.ID);
                category.Name = entity.Name;
                category.Code = entity.Code;
                category.MetaTitle = entity.MetaTitle;
                category.Description = entity.Description;
                category.Image = entity.Image;
                category.MoreImages = entity.MoreImages;
                category.Price = entity.Price;
                category.PricePromotion = entity.PricePromotion;
                category.IndudedVAT = entity.IndudedVAT;
                category.Quantity = entity.Quantity;
                category.CategoryID = entity.CategoryID;
                category.Detail = entity.Detail;
                category.Warranty = entity.Warranty;
                category.MetaDescription = entity.MetaDescription;
                category.MetaKeywords = entity.MetaKeywords;
                category.Status = entity.Status;
                category.TopHot = entity.TopHot;
                category.ViewCount = entity.ViewCount;
                category.CreatedDate = entity.CreatedDate;
                category.CreatedBy = entity.CreatedBy;
                category.Status = entity.Status;
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
        public List<Product> ListNewProduct(int top)
        {
            return db.Products.OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }
        public List<Product> ListFeatureProduct(int top)
        {
            return db.Products.Where(x=>x.TopHot != null && x.TopHot > DateTime.Now).OrderByDescending(x => x.CreatedDate).ToList();
        }
    }
}
