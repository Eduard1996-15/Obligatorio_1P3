using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repositorios.RepositoriosMemoria;
using Datos.RepositoriosAdo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repositorios.RepositoriosAdo;
using Negocio.InterfacesRepositorios;
using Dominio.InterfacesRepositorios;
using Microsoft.AspNetCore.Http;

namespace Presentacion
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
            services.AddSession();
            services.AddControllersWithViews();
            services.AddScoped<IRepositorioUsuario, RepositorioClienteAdo>();
            services.AddScoped<IRepositorioCompra, RepositorioCompraAdo>();
            services.AddScoped<IRepositorioPlanta, RepositorioPlantaAdo>();
            services.AddScoped<IRepositorioTipoPlanta, RepositorioTipoPlantaAdo>();
            services.AddScoped<IRepositorioParametro, RepositorioParametroADO>();
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
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Usuario}/{action=Login}/{id?}");
            });
        }
    }
}
