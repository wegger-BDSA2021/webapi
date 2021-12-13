using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.src.Controllers;
using api.src.Services;
using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using Microsoft.Identity.Web;
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAd"));

            // services.AddDbContext<WeggerContext>(options =>
            //     options.UseSqlServer(Configuration.GetConnectionString("Wegger")));

            string connection = Configuration.GetConnectionString("Wegger");
            System.Console.WriteLine(connection);

            services.AddDbContext<WeggerContext>(options =>
                options.UseSqlServer(connection));
            // for testing the api locally :
            // var _connection = new SqliteConnection("DataSource=:memory");
            // _connection.Open();
            // services.AddDbContext<WeggerContext>(options =>
            // {
            //     options.UseSqlite(_connection);
            // });


            services.AddScoped<IWeggerContext, WeggerContext>();
            services.AddScoped<IResourceRepository, ResourceRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();

            services.AddScoped<IResourceService, ResourceService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IUserService, UserService>();


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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Wegger web API", Version = "v1" });
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows()
                    {
                        Implicit = new OpenApiOAuthFlow()
                        {
                            AuthorizationUrl = new Uri("https://login.microsoftonline.com/22684afa-265e-416b-93fb-6aee2b9a8a15/oauth2/v2.0/authorize"),
                            TokenUrl = new Uri("https://login.microsoftonline.com/22684afa-265e-416b-93fb-6aee2b9a8a15/oauth2/v2.0/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                { "api://23d9a97d-3387-499c-83bc-0ae84d198403/ReadAccess", "Reads resources" }
                            }
                        }
                    }
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                     {
                     new OpenApiSecurityScheme
                        {
                        Reference = new OpenApiReference
                        {
                        Type = ReferenceType.SecurityScheme,
                        Id = "oauth2"
                        },
                                Scheme = "oauth2",
                                Name = "oauth2",
                                In = ParameterLocation.Header
                     },
                        new List<string>()
                     }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Wegger web API v1");
                    c.OAuthClientId("23d9a97d-3387-499c-83bc-0ae84d198403");
                    c.OAuthClientSecret("9Nv7Q~HGEG4K9xNQ3.XeL7Z_53p2ORRENNzA.");
                    c.OAuthUseBasicAuthenticationWithAccessCodeGrant();

                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
