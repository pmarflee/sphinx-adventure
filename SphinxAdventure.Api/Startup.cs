using System;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Paramore.Brighter.AspNetCore;
using Paramore.Darker.AspNetCore;
using SphinxAdventure.Core.CommandHandlers;
using SphinxAdventure.Core.Entities;
using SphinxAdventure.Core.Infrastructure;
using SphinxAdventure.Core.QueryHandlers;
using SphinxAdventure.Core.Repositories;

namespace SphinxAdventure.Api
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
            services.AddBrighter()
                .AsyncHandlersFromAssemblies(typeof(CreateGameCommandHandlerAsync).Assembly);

            services.AddDarker()
                .AddHandlersFromAssemblies(typeof(GetGameQueryHandlerAsync).Assembly);

            services.AddMvc(options =>
            {
                options.Filters.Add(new ProducesAttribute("application/json"));
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Jwt";
                options.DefaultChallengeScheme = "Jwt";
            }).AddJwtBearer("Jwt", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    //ValidAudience = "the audience you want to validate",
                    ValidateIssuer = false,
                    //ValidIssuer = "the isser you want to validate",

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration["SecretKey"])),

                    ValidateLifetime = true, //validate the expiration and not before values in the token

                    ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
                };
            });

            services.AddSingleton(
                Configuration.GetSection("CosmosDB").Get<CosmosDbConfiguration>());

            services.AddSingleton<IDocumentClient, DocumentClient>(svp =>
            {
                var config = svp.GetService<CosmosDbConfiguration>();
                return new DocumentClient(new Uri(config.AccountEndpoint), config.AccountKeys,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });
            });
            services.AddSingleton<IRepository<Game>, GameRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
