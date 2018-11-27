using System;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Paramore.Brighter.AspNetCore;
using Paramore.Darker.AspNetCore;
using SphinxAdventure.Core.CommandHandlers;
using SphinxAdventure.Core.Entities;
using SphinxAdventure.Core.QueryHandlers;
using SphinxAdventure.Core.Infrastructure.Repositories;
using YesSql.Provider.SqlServer;
using SphinxAdventure.Core.Infrastructure.Indexes.IndexProviders;
using YesSql.Sql;
using SphinxAdventure.Core.Infrastructure.Indexes.MappedIndexes;
using YesSql;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using SphinxAdventure.Core.Factories;

namespace SphinxAdventure.Api
{
    public class Startup
    {
        public Startup(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Microsoft.Extensions.Configuration.IConfiguration Configuration { get; }

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

            services.AddDbProvider(config =>
                config.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

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

            app.InitializeYesSql();
        }
    }

    static class YesSqlApplicationBuilderExtensions
    {
        public static void InitializeYesSql(this IApplicationBuilder app)
        {
            var serviceProvider = app.ApplicationServices;
            var store = serviceProvider.GetService<IStore>();

            if (!store.IsDatabaseInitialized())
            {
                store.InitializeAsync().GetAwaiter().GetResult();
            }

            store.TryCreateIndex<UserByUsername, string>(nameof(User.Username));
            store.TryCreateIndex<GameByUserId, Guid>(nameof(Game.UserId));
            store.TryCreateEntityIdIndexes();

            store.RegisterIndexes(typeof(UserIndexProvider).Assembly);
        }

        private static bool IsDatabaseInitialized(this IStore store)
        {
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
                    var outputParam = new SqlParameter("@Output", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(outputParam);
                    command.CommandText = $@"IF (EXISTS (SELECT * 
                                                        FROM INFORMATION_SCHEMA.TABLES
                                                        WHERE TABLE_SCHEMA = 'dbo'
                                                        AND TABLE_NAME = '{tableName}'))
                                                SET @Output = 1
                                            ELSE
                                                SET @Output = 0;";
                    var result = command.ExecuteNonQuery();

                    return (int)outputParam.Value == 1;
                }
            }
        }

        private static IDbConnection CreateConnection(IStore store)
        {
            return store.Configuration.ConnectionFactory.CreateConnection();
        }
    }
}
