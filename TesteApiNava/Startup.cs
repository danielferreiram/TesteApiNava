using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TesteApiNava.Models;

namespace TesteApiNava
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
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase());
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            var context = app.ApplicationServices.GetService<ApiContext>();
            AdicionarDadosTeste(context);
            app.UseMvc();
        }
        private static void AdicionarDadosTeste(ApiContext context)
        {
            var testeVendedor = new Models.Vendedor
            {
                vendedorId = 1,
                Nome = "Daniel",
                Email = "danielferreiram@gmail.com",
                Cpf = "12345678900",
                Telefone = "31993599252"
            };
            context.vendedor.Add(testeVendedor);
            var testeVenda = new Models.Venda
            {
                Id = 1,
                VendedorId = testeVendedor.vendedorId,
                Itens ="Carro",
                Status = "Aguardando pagamento",
                DataVenda = DateTime.Now
            };
            context.venda.Add(testeVenda);
            context.SaveChanges();
        }
    }
}
