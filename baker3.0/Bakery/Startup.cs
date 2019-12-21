using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bakery.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Bakery
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            // l�gga till alla services vi beh�ver f�r applikationen samt dependencies

            services.AddDbContext<AppDbContext>(options =>     // database
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

          
            services.AddScoped<IProductRepository,ProductRepository >();
        
            services.AddControllersWithViews(); // MVC


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // h�ller reda p� pipeline, vad som h�nder n�r sidan laddas

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();   // environmentcheck f�r att se vilken mode k�r p�
            }

            app.UseHttpsRedirection();  // ber programmet att k�ra https
            app.UseStaticFiles(); // g�r s� att programmet kan k�ra statisk filer, bilder, javafiles etc
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=index}/{Action=index}/{id?}"); // �ndra default sida
            });
        }
    }
}
