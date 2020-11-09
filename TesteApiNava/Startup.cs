using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            services.AddControllers();
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase(databaseName: "Test"));
            // Add framework services.
            //services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var options = new DbContextOptionsBuilder<ApiContext>()
    .UseInMemoryDatabase(databaseName: "Test")
    .Options;

            AdicionarDadosTeste(new ApiContext(options));
        }
        private static void AdicionarDadosTeste(ApiContext context)
        {
            var testeVendedor = new Models.Vendedor
            {
                Id = 1,
                Nome = "Daniel",
                Email = "danielferreiram@gmail.com",
                Cpf = "12345678900",
                Telefone = "31993599252"
            };
            context.vendedor.Add(testeVendedor);
            var testeVenda = new Models.Venda
            {
                IdentificadorVenda = 1,
                VendedorId = testeVendedor.Id,
                Itens = "Carro",
                Status = "Aguardando pagamento",
                DataVenda = DateTime.Now
            };
            context.venda.Add(testeVenda);
            context.SaveChanges();
        }
    }
}
