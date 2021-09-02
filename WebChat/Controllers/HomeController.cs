using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebChat.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebChat.Controllers
{
	public class HomeController : Controller
	{
		readonly WebChatDbContext db;

		public HomeController(WebChatDbContext _db)
		{
			db = _db;
		}

		public async Task<IActionResult> Index()
		{
			if (User.Identity.IsAuthenticated)
			{
				var currentUserName = HttpContext.User.Identity.Name;
				var listUsers = await db.AppUsers
								.AsNoTracking()		// Tăng tốc độ truy vấn database
								.Where(u => u.Username != currentUserName)
								.ToListAsync();
				return View("Chat", listUsers);
			}
			else
			{
				return View();
			}
		}
	}
}
