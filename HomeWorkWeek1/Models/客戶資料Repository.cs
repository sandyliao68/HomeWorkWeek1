using System;
using System.Linq;
using System.Collections.Generic;
	
namespace HomeWorkWeek1.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        public override IQueryable<客戶資料>  All()
        {
           
            return base.All().Where(p => !p.是否已刪除);

        }
        public IQueryable<客戶資料> All(bool isAll)
        {
            if (isAll)
            {
                //原生
                return base.All();
            }
            else
            {
                //Repository
                return this.All();
            }
        }
        public 客戶資料 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id  == id);
        }

        public IQueryable<客戶資料> Query(string keyword, string drpCategory)
        {
           var data = this.All().OrderByDescending(p => p.Id).AsQueryable();         
            if (!String.IsNullOrEmpty(keyword))
            {
                data = data.Where(p => p.客戶名稱.Contains(keyword));
            }
            if (!String.IsNullOrEmpty(drpCategory))
            {
                data = data.Where(p => p.客戶分類.Contains(drpCategory));
            }
            return data;
        }
	}

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}