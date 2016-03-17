using System;
using System.Linq;
using System.Collections.Generic;
	
namespace HomeWorkWeek1.Models
{   
	public  class vwClientRepository : EFRepository<vwClient>, IvwClientRepository
	{

	}

	public  interface IvwClientRepository : IRepository<vwClient>
	{

	}
}