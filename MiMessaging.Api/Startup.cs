using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiMessaging.Api.Models;

namespace MiMessaging.Api
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
			services
				.AddResponseCompression(options => 
				{
					options.Providers.Add<BrotliCompressionProvider>();
					options.Providers.Add<GzipCompressionProvider>();
				})
				.AddMvc()
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
				// TODO: Find out if below setting is needed and safe
				// https://stackoverflow.com/questions/7397207
				.AddJsonOptions(x => x
					.SerializerSettings
					.ReferenceLoopHandling =
						Newtonsoft.Json.ReferenceLoopHandling.Ignore);

			var connection = @"Server=tcp:mipocdb.database.windows.net,1433;" +
				"Initial Catalog=MiMessaging;Persist Security Info=False;" +
				"User ID=P88A2DR0;Password=&Dt2VgBcgV!3G3HeY;MultipleActiveResultSets=False;" +
				"Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
			services.AddDbContext<MiMessagingContext>
				(options => options.UseSqlServer(connection));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			string cachePeriod;
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseCors(builder =>
					builder
						.AllowAnyOrigin()
						.AllowAnyMethod()
						.AllowAnyHeader());
				cachePeriod = "604800";
			}
			else
			{
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
				cachePeriod = "600";
			}

			app.UseResponseCompression();
			app.UseDefaultFiles();
			app.UseStaticFiles(new StaticFileOptions
			{
				OnPrepareResponse = ctx =>
				{
					ctx
						.Context
						.Response
						.Headers
						.Append("Cache-Control", $"public, max-age={cachePeriod}");
				}
			});
			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}
