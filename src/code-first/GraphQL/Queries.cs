﻿using HotChocolate.Data;
using HotChocolate.Data.Neo4J;
using HotChocolate.Data.Neo4J.Execution;

using MoviesAPI.Models;

using Neo4j.Driver;

namespace MoviesAPI.Schema;

[ExtendObjectType(OperationTypeNames.Query)]
public class MovieQueries
{
    [GraphQLName("actors")]
    [UseNeo4JDatabase(databaseName: "neo4j")]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public Neo4JExecutable<Person> GetActors(
        [ScopedService] IAsyncSession session) =>
        new(session);

    [GraphQLName("movies")]
    [UseNeo4JDatabase(databaseName: "neo4j")]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public Neo4JExecutable<Movie> GetMovies(
        [ScopedService] IAsyncSession session) =>
        new(session);
}
