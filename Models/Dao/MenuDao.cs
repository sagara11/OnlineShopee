using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class MenuDao
    {
        OnlineShopeeDbContext db = null;

        public MenuDao()
        {
            db = new OnlineShopeeDbContext();
        }
        public List<Menu> ListByGroupID(int groupId)
        {
            return db.Menus.Where(x => x.TypeID == groupId && x.Status == true).OrderBy(x=>x.DisplayOrder).ToList();
        }
    }
}
