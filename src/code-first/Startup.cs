using HotChocolate.Data.Neo4J;

using MoviesAPI.Schema;

using Neo4j.Driver;

namespace MoviesAPI;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration) => Configuration = configuration;

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
        var section = Configuration.GetSection("Neo4JConnection");
        Neo4JConnection conf = new();

        section.Bind(conf);

        IDriver driver = GraphDatabase.Driver(conf.Url, AuthTokens.Basic(conf.Username, conf.Password));

        services
            .AddSingleton(driver)
            .AddGraphQLServer()
                .AddQueryType(q => q.Name("Query"))
                    .AddType<MovieQueries>()
            .AddNeo4JFiltering()
            .AddNeo4JSorting()
            .AddNeo4JProjections();

        services.AddCors();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseCors(policy => policy.AllowAnyOrigin());

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
        }

        app.UseRouting();
        app.UseHttpsRedirection();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGraphQL();
            endpoints.MapGet("/", context =>
            {
                context.Response.Redirect("/graphql", true);
                return Task.CompletedTask;
            });
        });
    }
}
