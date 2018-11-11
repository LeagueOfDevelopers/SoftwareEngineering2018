using System;
using System.Collections.Generic;
using BusinessEntities;
using BusinessServices.Interfaces;
using BusinessServices.Services;
using Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using WithDbLoDSprintApi.Filters;

namespace WithDbLoDSprintApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetValue<string>("mongoConnectionString");

            var dictionaryRepository = new MongoDictionaryRepository(connectionString);
            var userRepository = new MongoUserRepository(connectionString);
            var sessionRepository = new InMemorySessionRepository(new Dictionary<Guid, Session>());

            var administratorId = Configuration.GetValue<Guid>("administratorId");
            var administratorName = Configuration.GetValue<string>("administratorName");
            var administratorService = new AdministratorService(
                dictionaryRepository, 
                new Administrator(administratorId, administratorName));

            var userService = new UserService(userRepository);

            var oneSessionWordsCount = Configuration.GetValue<int>("oneSessionWordsCount");
            var startSessionService = new StartSessionService(
                oneSessionWordsCount,
                dictionaryRepository,
                sessionRepository,
                userRepository);

            var finishSessionService = new FinishSessionService(
                dictionaryRepository,
                sessionRepository, 
                userRepository);

            services.AddSingleton<IAdministratorService>(administratorService);
            services.AddSingleton<IUserService>(userService);
            services.AddSingleton<IStartSessionService>(startSessionService);
            services.AddSingleton<IFinishSessionService>(finishSessionService);

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ExceptionFilter));
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ModelValidationFilter));
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

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
