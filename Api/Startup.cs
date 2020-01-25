using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Api.Modules.System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

namespace Api
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
			services.AddControllers();
			
			services.AddCors(options =>
			{
				options.AddPolicy("app-cors-policy",
					builder =>
					{
						builder.WithOrigins("http://localhost:8080")
							.WithHeaders(HeaderNames.ContentType);
					});
			});
			
			AddApiServices(services);
		}

		// All the custom services that make up the API layer
		private void AddApiServices(IServiceCollection services)
		{
			services.AddSingleton(ApplicationInfo.BuildApplicationInfo());
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			
			app.UseCors("app-cors-policy");

			app.UseHttpsRedirection();

			// Allows for serving the JS SPA content directly from the API app for convenience
			app.UseDefaultFiles(); // Serves index.html to root directory
			app.UseStaticFiles(); // Serves files from wwwroot

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
		}
	}
}
