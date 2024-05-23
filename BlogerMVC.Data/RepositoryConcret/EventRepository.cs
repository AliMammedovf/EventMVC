using BlogerMVC.Core.Models;
using BlogerMVC.Core.RepositoryAbstract;
using BlogerMVC.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogerMVC.Data.RepositoryConcret;

public class EventRepository : GenericRepository<Event>, IEventRepository
{
	public EventRepository(AppDbContext appDbContext) : base(appDbContext)
	{
	}
}
