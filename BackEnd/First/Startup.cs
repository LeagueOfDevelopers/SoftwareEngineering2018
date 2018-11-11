using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using EnglishLearner.Application;
using Swashbuckle.AspNetCore.Swagger;
using EnglishLearnerAPI.Filters;

namespace EnglishLearnerAPI
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
            CompositionRoot root = CompositionRoot.Configurate(new Random(),
                @"C:\Users\User\Desktop\proba\users.json",
                @"C:\Users\User\Desktop\proba\words.json");

            services.AddSingleton(root.SessionManager);
            services.AddSingleton(root.UserService);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info{ Title = "My API", Version = "v1" });
            });

            services.AddMvc(options => {
                options.Filters.Add(new ModelValidatorFilter());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc();
        }
    }
}
