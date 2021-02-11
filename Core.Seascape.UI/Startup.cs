using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.IO;

namespace Core.Seascape.UI
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
            services.AddAutoMapper(typeof(Startup));

            var rootPath = Configuration.GetValue<string>(WebHostDefaults.ContentRootKey);
            var viewPath = Path.Combine(rootPath, "Views");
            var viewLocations = ViewLocations(viewPath);

            services.AddControllersWithViews().AddRazorOptions(options =>
            {
                options.ViewLocationFormats.Clear();
                options.ViewLocationFormats.Add("~/Views/{1}/{0}.cshtml");
                options.ViewLocationFormats.Add("~/Views/Shared/{0}.cshtml");

                foreach (var location in viewLocations)
                {
                    if (string.IsNullOrWhiteSpace(location)) continue;

                    options.ViewLocationFormats.Add($"{location.Substring(rootPath.Length).Replace(@"\", @"/")}/{{0}}.cshtml");
                }
            }).AddRazorRuntimeCompilation();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private IEnumerable<string> ViewLocations(string rootPath)
        {
            var locations = new List<string>();

            foreach (var viewPath in Directory.GetDirectories(rootPath))
            {
                locations.Add(viewPath);

                locations.AddRange(ViewLocations(viewPath));
            }

            return locations;
        }
    }
}
