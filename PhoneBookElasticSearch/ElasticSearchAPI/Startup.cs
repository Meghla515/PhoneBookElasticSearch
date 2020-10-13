using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElasticSearchPersistence.Repository.ElasticSearch;
using ElasticSearchService.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nest;

namespace ElasticSearchAPI
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
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddTransient<IElasticSearchRepository, ElasticSearchRepository>();
            services.AddTransient<IESService, ESService>();

            services.AddSwaggerGen();

            var settings = new ConnectionSettings(new Uri("http://localhost:9200/"))
                .DefaultIndex("phonebook")
                .DefaultMappingFor<dynamic>(m => m
                    .IndexName("phonebook")
                );

            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Elastic Search API");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
