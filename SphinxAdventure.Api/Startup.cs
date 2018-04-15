using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Paramore.Brighter.AspNetCore;
using Paramore.Darker.AspNetCore;
using SphinxAdventure.Core.CommandHandlers;
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

            services.AddScoped<LiteRepository>(sp => new LiteRepository(
                Configuration.GetConnectionString("LiteDB")));
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
