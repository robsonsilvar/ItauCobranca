using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Itau.Cobranca.Dados;
using Itau.Cobranca.Dominio.Repository;
using Itau.Cobranca.Dominio.Service;
using Itau.Cobranca.Service;
using Itau.Notificacao.Dominio.Contrato.Service;
using Itau.Notificacao.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Itau.Cobranca.WebAPI
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
            services.AddCors();           

            services.AddControllers();

            services.AddScoped<ICobrancaService, CobrancaService>();         
            services.AddScoped<ICobrancaRepository, CobrancaRepository>();
            services.AddScoped<INotificacaoEmailService, NotificacaoEmailService>();
            services.AddScoped<INotificacaoSMSService, NotificacaoSMSService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(
                 options => options.SetIsOriginAllowed(x => _ = true).AllowAnyMethod().AllowAnyHeader().AllowCredentials()
             );

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

           
        }
    }
}
