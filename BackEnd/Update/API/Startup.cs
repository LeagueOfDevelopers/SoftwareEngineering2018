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
using EnglishLearner.infrastructure;


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
            string mongoConnectionString = Configuration.GetValue<string>("mongoConnectionString");
            int sizeExercise = Configuration.GetValue<int>("sizeExercise");
            MongoWordRepository words = new MongoWordRepository(mongoConnectionString);
            MongoUserRepository users = new MongoUserRepository(mongoConnectionString);

            IWordManager wordManager = new WordManager(new Random(), words);
            IUserService userService = new UserService(users, wordManager);
            ISessionManager sessionManager = new SessionManager(users, wordManager, sizeExercise);
            
            services.AddSingleton(sessionManager);
            services.AddSingleton(userService);
            services.AddSingleton(wordManager);

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
