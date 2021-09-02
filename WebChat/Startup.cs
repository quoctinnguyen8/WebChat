using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebChat.Entities;
using WebChat.Hubs;

namespace WebChat
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();
			services.AddSignalR();
			services.AddDbContext<WebChatDbContext>(options =>
			{
				options.UseSqlServer(Configuration.GetConnectionString("WebChat"));
			});
			// Đăng nhập
			services.AddAuthentication("Cookies")
					.AddCookie(options =>
					{
						options.LoginPath = "/dang-nhap";   // đường dẫn trang đăng nhập
						options.ExpireTimeSpan = TimeSpan.FromHours(6); // tự đăng xuất sau 6h
						options.Cookie.HttpOnly = true; // lý do bảo mật
					});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();
			// Đăng nhập
			app.UseCookiePolicy();
			app.UseAuthentication();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapHub<ChatHub>("/chat");

				// Map URL "/dang-ky" cho "/AppUser/SignUp"
				endpoints.MapControllerRoute(
					name: "signup",
					pattern: "dang-ky",
					defaults: new { controller = "AppUser", action = "SignUp" });

				// URL mới cho trang đăng nhập
				endpoints.MapControllerRoute(
					name: "login",
					pattern: "dang-nhap",
					defaults: new { controller = "AppUser", action = "Login" });

				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
