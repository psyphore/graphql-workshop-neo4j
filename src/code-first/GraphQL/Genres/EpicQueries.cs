using HotChocolate.Data;
using HotChocolate.Data.Neo4J;
using HotChocolate.Data.Neo4J.Execution;

using MoviesAPI.Models;

using Neo4j.Driver;

namespace MoviesAPI.Schema.Genres;

[ExtendObjectType(OperationTypeNames.Query)]
public class EpicQueries
{
    [GraphQLName("actors")]
    [UseNeo4JDatabase(databaseName: "epic")]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public Neo4JExecutable<Actor> GetActors(
        [ScopedService] IAsyncSession session) =>
        new(session);

    [GraphQLName("movies")]
    [UseNeo4JDatabase(databaseName: "epic")]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public Neo4JExecutable<Movie> GetMovies(
        [ScopedService] IAsyncSession session) =>
        new(session);
}
