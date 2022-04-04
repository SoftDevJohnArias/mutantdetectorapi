using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using MutantDetector.Core.Interfaces;
using MutantDetector.Infraestructure.Repositories;
using MutantDetector.Infraestructure.Data;


using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Certificate;

namespace MutantDetector.Api
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
            services.AddAuthentication(
              CertificateAuthenticationDefaults.AuthenticationScheme)
             .AddCertificate();

            
            services.AddResponseCaching();
            services.AddControllers();
            services.AddTransient<IDNAResultRepository, DNAResultRepository>();
            services.AddCors(options => {
                options.AddPolicy("foo", builder =>
                {

                    builder
                                 .WithOrigins("https://*.*.*.*")
                                 .AllowAnyMethod()
                                 .AllowAnyHeader()
                                 .AllowCredentials();
                });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "MutantDetector Prueba MELI API",
                    Description = "Valida la cadena para saber si eres mutante :)",
                    Contact = new OpenApiContact
                    {
                        Name = "John Arias",
                        Email = "johnalexanderariasariza@gmail.com",
                    },
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });
            services.AddDbContext<mutantdnaContext>
                (options => options.UseSqlServer
                (Configuration.GetConnectionString("mutantdna"))
            );

        }

       

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseAuthentication();
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseResponseCaching();
            app.UseRouting();
            app.UseCors("AllowAll");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
