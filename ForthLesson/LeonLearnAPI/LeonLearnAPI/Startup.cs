using LeonLearn;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using UserContext;
using WordContext;

namespace LeonLearnAPI
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
            var sqlConnectionString = Configuration.GetValue<string>("sqlConnectionString");
            IUserRepository sqlUserRepository = new SqlUserRepository(sqlConnectionString);
            IWordsRepository sqlWordsRepository = new SqlWordRepository(sqlConnectionString);

            var userConnectionString = Configuration.GetValue<string>("mongoUserConnectionString");
            IUserRepository mongoUserRepository = new MongoUserRepository(userConnectionString);
            
            var wordConnectionString = Configuration.GetValue<string>("mongoWordConnectionString");
            IWordsRepository mongoWordsRepository = new MongoWordsRepository(wordConnectionString);

            var mainService = new Service(sqlUserRepository, sqlWordsRepository);

            //services.AddSingleton(sqlUserRepository);
            services.AddSingleton(mainService);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info {Title = "My API", Version = "v1"}); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}