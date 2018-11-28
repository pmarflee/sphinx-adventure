using System;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Paramore.Brighter.AspNetCore;
using Paramore.Darker.AspNetCore;
using SphinxAdventure.Core.CommandHandlers;
using SphinxAdventure.Core.Entities;
using SphinxAdventure.Core.QueryHandlers;
using SphinxAdventure.Core.Infrastructure.Repositories;
using SphinxAdventure.Core.Infrastructure.Indexes.IndexProviders;
using YesSql.Sql;
using SphinxAdventure.Core.Infrastructure.Indexes.MappedIndexes;
using YesSql;
using System.Data;
using System.Linq;
using SphinxAdventure.Core.Factories;
using YesSql.Provider.Sqlite;
using System.IO;

namespace SphinxAdventure.Api
{
    public class Startup
    {
        public Startup(
            Microsoft.Extensions.Configuration.IConfiguration configuration,
            IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
            DatabaseFilePath = $@"{HostingEnvironment.ContentRootPath}\database\sphinx.db";
        }

        public Microsoft.Extensions.Configuration.IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }
        public string DatabaseFilePath { get; }

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

            services.AddDbProvider(config => config.UseSqLite($"Data Source={DatabaseFilePath}"));

            services.AddSingleton<IRepository<Game>, GameRepository>();
            services.AddSingleton<IRepository<User>, UserRepository>();
            services.AddSingleton<IFactory<Map>, MapFactory>();
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

            app.InitializeYesSql(DatabaseFilePath);
        }
    }

    static class YesSqlApplicationBuilderExtensions
    {
        public static void InitializeYesSql(this IApplicationBuilder app, string databaseFilePath)
        {
            var serviceProvider = app.ApplicationServices;
            var store = serviceProvider.GetService<IStore>();

            if (!store.IsDatabaseInitialized(databaseFilePath))
            {
                store.InitializeAsync().GetAwaiter().GetResult();
            }

            store.TryCreateIndex<UserByUsername, string>(nameof(User.Username));
            store.TryCreateIndex<GameByUserId, Guid>(nameof(Game.UserId));
            store.TryCreateEntityIdIndexes();

            store.RegisterIndexes(typeof(UserIndexProvider).Assembly);
        }

        private static bool IsDatabaseInitialized(this IStore store, string path)
        {
            CreateDatabaseIfNotExists(path);

            return DoesTableExist(store, "Document");
        }

        private static void TryCreateEntityIdIndexes(this IStore store)
        {
            var entityByIdType = typeof(EntityById);
            var derivedEntityByIdTypes = entityByIdType.Assembly
                .GetTypes()
                .Where(t => t.IsSubclassOf(entityByIdType));

            foreach (var type in derivedEntityByIdTypes)
            {
                store.TryCreateIndex<Guid>(type, nameof(Entity.EntityId));
            }
        }

        private static void CreateDatabaseIfNotExists(string path)
        {
            if (!File.Exists(path))
            {
                var fs = File.Create(path);
                fs.Close();
            }
        }

        private static void TryCreateIndex<TIndex, TIndexColumn>(
            this IStore store, string columnName)
        {
            store.TryCreateIndex<TIndexColumn>(typeof(TIndex), columnName);
        }

        private static void TryCreateIndex<TIndexColumn>(
            this IStore store, Type indexType, string columnName)
        {
            if (!store.DoesIndexExist(indexType))
            {
                store.CreateIndex<TIndexColumn>(indexType, columnName);
            }
        }

        private static void CreateIndex<TIndex, TIndexColumn>(
            this IStore store, string columnName)
        {
            store.CreateIndex<TIndexColumn>(typeof(TIndex), columnName);
        }

        private static void CreateIndex<TIndexColumn>(
            this IStore store, Type indexType, string columnName)
        {
            using (var session = store.CreateSession())
            {
                new SchemaBuilder(session)
                    .CreateMapIndexTable(indexType.Name, table => table
                        .Column<TIndexColumn>(columnName));
            }
        }

        private static bool DoesIndexExist<TIndex>(this IStore store)
        {
            return store.DoesIndexExist(typeof(TIndex));
        }

        private static bool DoesIndexExist(this IStore store, Type indexType)
        {
            return DoesTableExist(store, indexType.Name);
        }

        private static bool DoesTableExist(IStore store, string tableName)
        {
            using (var connection = CreateConnection(store))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"SELECT name FROM sqlite_master WHERE type = 'table' AND name = '{tableName}';";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private static IDbConnection CreateConnection(IStore store)
        {
            return store.Configuration.ConnectionFactory.CreateConnection();
        }
    }
}
