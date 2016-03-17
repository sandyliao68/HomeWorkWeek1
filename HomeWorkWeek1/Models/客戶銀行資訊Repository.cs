using System;
using System.Linq;
using System.Collections.Generic;
	
namespace HomeWorkWeek1.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        public override IQueryable<客戶銀行資訊> All()
        {
            // var 客戶銀行資訊 = db.客戶銀行資訊.Include(客 => 客.客戶資料).Where(客 => 客.客戶資料.是否已刪除 == false) ;    
            return base.All().Where(p => !p.是否已刪除 && !p.客戶資料.是否已刪除);
        }
        public IQueryable<客戶銀行資訊> All(bool isAll)
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
        public 客戶銀行資訊 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }
        public IQueryable<客戶銀行資訊> Query(string BankName)
        {
            var data = this.All().OrderByDescending(p => p.Id).AsQueryable();
            if (!String.IsNullOrEmpty(BankName))
            {
                data = data.Where(p => p.銀行名稱.Contains(BankName));
            }         
            return data;
        }
	}

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}