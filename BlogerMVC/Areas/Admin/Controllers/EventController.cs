using BlogerMVC.Business.Exceptions;
using BlogerMVC.Business.Services.Abstract;
using BlogerMVC.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BlogerMVC.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "SuperAdmin")]
	public class EventController : Controller
	{
		private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }
        public IActionResult Index()
		{
			var events= _eventService.GetAllEvents();
			return View(events);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Event events)
		{
		    if(!ModelState.IsValid) 
				return View();

			try
			{
				await _eventService.AddAsyncEvent(events);
			}
			catch(FileNullException ex)
			{
				ModelState.AddModelError("ImageFile", ex.Message);
				return View();
			}
			catch(FileContentException ex)
			{
				ModelState.AddModelError("ImageFile", ex.Message);
				return View();
			}
			catch (FileSizeException ex)
			{
				ModelState.AddModelError("ImageFile", ex.Message);
				return View();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return RedirectToAction("Index");	
		}

		public IActionResult Delete(int id)
		{
			var exsist = _eventService.GetEvent(x => x.Id == id);

			if(exsist == null)
			{
				return NotFound();
			}

			return View(exsist);
		}

		[HttpPost]
		public IActionResult DeletePost(int id)
		{
			if (!ModelState.IsValid)
				return View();

			try
			{
				_eventService.DeleteEvent(id);
			}
			catch (IdNullException ex)
			{
				return NotFound();
			}
			catch (FileNullException ex)
			{
				return NotFound();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return RedirectToAction("Index");

		}

		public IActionResult Update(int id)
		{
			var oldEvent= _eventService.GetEvent(x=>x.Id == id);

			if(oldEvent == null)
			{
				return NotFound();
			}

			return View(oldEvent);
		}

		[HttpPost]
		public IActionResult Update(Event events)
		{
			if (!ModelState.IsValid)
				return View();


			try
			{
				_eventService.UpdateEvent(events.Id, events);
			}
			catch (IdNullException ex)
			{
				ModelState.AddModelError("ImageFile", ex.Message);
				return View();
			}
			catch (FileContentException ex)
			{
				ModelState.AddModelError("ImageFile", ex.Message);
				return View();
			}
			catch (FileSizeException ex)
			{
				ModelState.AddModelError("ImageFile", ex.Message);
				return View();
			}
			catch (FileNullException ex)
			{
				ModelState.AddModelError("ImageFile", ex.Message);
				return View();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return RedirectToAction("Index");
		}
	}
}
