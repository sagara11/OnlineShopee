using Models.EF;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class ContentDao
    {
        OnlineShopeeDbContext db = null;

        public ContentDao()
        {
            db = new OnlineShopeeDbContext();
        }
        public Content GetbyID(long id)
        {
            return db.Contents.Find(id);
        }
        public IEnumerable<Content> ListAllContent(string searchString, int page, int pageSize)
        {
            //var pageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"].ToString);
            IQueryable<Content> model = db.Contents;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Description.Contains(searchString) || x.Detail.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedBy).ToPagedList(page, pageSize);
        }
        public bool Insert(Content entity)
        {
            db.Contents.Add(entity);
            db.SaveChanges();
            return true;
        }
        public bool Update(Content entity)
        {
            try
            {
                var content = db.Contents.Find(entity.ID);
                content.Name = entity.Name;
                content.Image = entity.Image;
                content.Detail = entity.Detail;
                content.MetaTitle = entity.MetaTitle;
                content.CreatedDate = entity.CreatedDate;
                content.CreatedBy = entity.CreatedBy;
                content.Status = entity.Status;
                content.CategoryID = entity.CategoryID;
                content.Description = entity.Description;
                content.ModifiedBy = entity.ModifiedBy;
                content.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool ChangeStatus(long id)
        {
            var content = db.Contents.Find(id);
            content.Status = !content.Status;
            db.SaveChanges();
            return content.Status;
        }
        public bool Delete(long id)
        {
            var content = db.Contents.Find(id);
            db.Contents.Remove(content);
            db.SaveChanges();
            return true;
        }
    }
}
