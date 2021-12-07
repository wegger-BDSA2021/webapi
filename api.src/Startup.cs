using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.src.Controllers;
using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Services;

namespace api.src
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
            // services.AddDbContext<WeggerContext>(options =>
            //     options.UseSqlServer(Configuration.GetConnectionString("wegger")));

            // for testing the api locally :
            var _connection = new SqliteConnection("DataSource=:memory");
            _connection.Open();
            services.AddDbContext<WeggerContext>(options =>
            {
                options.UseSqlite(_connection);
            });


            services.AddScoped<IWeggerContext, WeggerContext>();
            services.AddScoped<IResourceRepository, ResourceRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();

            services.AddScoped<IResourceService, ResourceService>();


            // services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            // services.AddTransient<IEntityRepository, EntityRepository>();
            // continue to use the servies for dependecy injection  
            //  - for repositories
            //  - for services transfering data back and forth from the repos to the controllers applying the bussines logic 

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ValidatorActionFilter));
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "api.src", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "api.src v1"));
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
