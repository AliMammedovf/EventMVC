using BlogerMVC.Business.Exceptions;
using BlogerMVC.Business.Services.Abstract;
using BlogerMVC.Core.Models;
using BlogerMVC.Core.RepositoryAbstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogerMVC.Business.Services.Concret;

public class EventService : IEventService
{
	private readonly IEventRepository _eventRepository;
	private readonly IWebHostEnvironment _env;

    public EventService(IEventRepository eventRepository, IWebHostEnvironment env)
    {
        _env = env;
		_eventRepository = eventRepository;
    }
    public async Task AddAsyncEvent(Event events)
	{
		if (events == null) throw new FileNullException("file tapilmadi!");

		if(events.ImageFile.ContentType != "image/png" && events.ImageFile.ContentType != "image/jpeg")
		{
			throw new FileContentException("file uygun deyil!");
		}

		if(events.ImageFile.Length > 2097152)
		{
			throw new FileSizeException("file olcusu 2mb artiq ola bilmez!");
		}

		string fileName= Guid.NewGuid().ToString() + Path.GetExtension(events.ImageFile.FileName);

		string path= _env.WebRootPath + "\\uploads\\events\\" + fileName;

		using (FileStream fileStream = new FileStream(path, FileMode.Create))
		{
			events.ImageFile.CopyTo(fileStream);
		}

		events.ImageUrl=fileName;

		await _eventRepository.AddAsync(events);
		await _eventRepository.CommitAsync();

	}

	public void DeleteEvent(int id)
	{
		var exsist = _eventRepository.Get(x => x.Id == id);

		if (exsist == null) throw new IdNullException("id bos ola bilmez!");

		string path = _env.WebRootPath + "\\uploads\\events\\" + exsist.ImageUrl;

		if(!File.Exists(path))
		{
			throw new FileNullException("file tapilmadi!");
		}

		File.Delete(path);

		_eventRepository.Delete(exsist);
		_eventRepository.Commit();
	}

	public List<Event> GetAllEvents(Func<Event, bool>? func = null)
	{
		return _eventRepository.GetAll(func);
	}

	public Event GetEvent(Func<Event, bool>? func = null)
	{
		return _eventRepository.Get(func);
	}

	public async void UpdateEvent(int id, Event newEvents)
	{
		var oldEvents = _eventRepository.Get(x => x.Id == id);

		if (oldEvents == null) throw new IdNullException("id bos ola bilmez!");

		if(newEvents.ImageFile != null)
		{
			if (newEvents.ImageFile.ContentType != "image/png" && newEvents.ImageFile.ContentType != "image/jpeg")
			{
				throw new FileContentException("file uygun deyil!");
			}

			if (newEvents.ImageFile.Length > 2097152)
			{
				throw new FileSizeException("file olcusu 2mb artiq ola bilmez!");
			}

			string fileName = Guid.NewGuid().ToString() + Path.GetExtension(newEvents.ImageFile.FileName);

			string path = _env.WebRootPath + "\\uploads\\events\\" + fileName;

			using (FileStream fileStream = new FileStream(path, FileMode.Create))
			{
				newEvents.ImageFile.CopyTo(fileStream);
			}

			string oldPath = _env.WebRootPath + "\\uploads\\events\\" + oldEvents.ImageUrl;


			if (!File.Exists(oldPath))
			{
				throw new FileNullException("file tapilmadi!");
			}

			File.Delete(oldPath);

			oldEvents.ImageUrl= fileName;
		}

		oldEvents.Title = newEvents.Title;
		oldEvents.Content = newEvents.Content;
		oldEvents.Description = newEvents.Description;

	    _eventRepository.Commit();
	}
}
