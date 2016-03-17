using System;
using System.Linq;
using System.Collections.Generic;
	
namespace HomeWorkWeek1.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Where(p => !p.是否已刪除 && !p.客戶資料.是否已刪除);
        }

        public IQueryable<客戶聯絡人> All(bool isAll)
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
        public 客戶聯絡人 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }
        public IQueryable<客戶聯絡人> Query(string PName, string Title)
        {
            var data = this.All().OrderByDescending(p => p.Id).AsQueryable();
            if (!String.IsNullOrEmpty(PName))
            {
                data = data.Where(p => p.姓名.Contains(PName));
            }
            if (!String.IsNullOrEmpty(Title))
            {
                data = data.Where(p => p.職稱.Contains(Title));
            }  
            return data;
        }
	}

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}