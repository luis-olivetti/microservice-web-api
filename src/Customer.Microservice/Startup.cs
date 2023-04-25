using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Customer.Microservice.Consumer;
using Customer.Microservice.Data;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Customer.Microservice
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
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("DefaultConnection"),
                   b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            #region Swagger
            services.AddSwaggerGen(c =>
            {
                // c.IncludeXmlComments(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Customer.Microservice.xml"));
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Customer Microservice API",
                });
            });
            #endregion

            services.AddMassTransit(x =>
            {
                x.AddConsumer<CustomerConsumer>();
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host(new Uri("rabbitmq://guest:guest@microservices_rabbitmq:5672"));
                    cfg.ReceiveEndpoint("fila", ep =>
                    {
                        ep.PrefetchCount = 10;
                        ep.UseMessageRetry(r => r.Interval(2, 100));
                        ep.ConfigureConsumer<CustomerConsumer>(provider);
                    });
                }));
            });

            services.AddTransient<DbInitialiser>();

            services.AddControllers();
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

            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer.Microservice");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var services = app.ApplicationServices.CreateScope().ServiceProvider;
            var initialiser = services.GetRequiredService<DbInitialiser>();
            initialiser.Run();
        }
    }
}
