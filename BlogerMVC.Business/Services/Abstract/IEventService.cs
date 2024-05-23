using BlogerMVC.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogerMVC.Business.Services.Abstract;

public interface IEventService
{
	Task AddAsyncEvent(Event events);

	void DeleteEvent(int id);

	void UpdateEvent(int id, Event newEvents);

	Event GetEvent(Func<Event, bool>? func=null);

	List<Event> GetAllEvents(Func<Event, bool>? func = null);
}
