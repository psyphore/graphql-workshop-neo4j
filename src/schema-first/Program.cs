using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Neo4j.Driver;

var builder = WebApplication.CreateBuilder(args);

var section = builder.Configuration.GetSection("Neo4JConnection");
Neo4JConnection conf = new();
section.Bind(conf);

var driver = GraphDatabase.Driver(conf.Url, AuthTokens.Basic(conf.Username, conf.Password));

builder.Services
    .AddSingleton(driver)
    .AddGraphQLServer()
    .AddQueryType()
    .AddMovieLibraryTypes();

await using var app = builder.Build();

app.MapGraphQL();

await app.RunAsync();
