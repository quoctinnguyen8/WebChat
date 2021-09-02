using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebChat.Entities
{
	public class WebChatDbContext : DbContext
	{

		public DbSet<AppUser> AppUsers { get; set; }

		public WebChatDbContext(DbContextOptions options) : base(options)
		{
		}

	}
}
