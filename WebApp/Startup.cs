using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFDataLibrary.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApp
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
            services.AddMvc(option => {
                option.EnableEndpointRouting = false;

            });
            services.AddDbContextPool<UndefinedContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
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
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMvc(routes =>
            {
                routes.MapRoute(null, "",
                    new
                    {
                        controller = "Product",
                        action = "List",
                        category = (string)null,
                        page = 1
                    });
                routes.MapRoute(null, "Page{page}",
                    new
                    {
                        controller = "Product",
                        action = "List",
                        category = (string)null
                    },
                    new
                    {
                        page = @"\d+"
                    });

                routes.MapRoute(null, "{category}",
                    new
                    {
                        controller = "Product",
                        action = "List",
                        page = 1
                    });

                routes.MapRoute(null, "{category}/Page{page}",
                    new { controller = "Product", action = "List" },
                    new
                    {
                        page = @"\d+"
                    });
            });
        }
    }
}
