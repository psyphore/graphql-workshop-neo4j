using HotChocolate.Data;
using HotChocolate.Data.Neo4J;
using HotChocolate.Data.Neo4J.Execution;

using MoviesAPI.Models;

using Neo4j.Driver;

namespace MoviesAPI.Schema.Genres;

[ExtendObjectType(OperationTypeNames.Query)]
public class CyberpunkQueries
{
    private readonly string cyberpunkgenre = nameof(cyberpunkgenre);

    [GraphQLName("actors")]
    [UseNeo4JDatabase(nameof(cyberpunkgenre))]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public Neo4JExecutable<Actor> GetActors(
        [ScopedService] IAsyncSession session) =>
        new(session);

    [GraphQLName("movies")]
    [UseNeo4JDatabase(nameof(cyberpunkgenre))]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public Neo4JExecutable<Movie> GetMovies(
        [ScopedService] IAsyncSession session) =>
        new(session);

    [GraphQLName("movie")]
    [UseNeo4JDatabase(nameof(cyberpunkgenre))]
    public Neo4JExecutable<Movie> GetMovie(
        [ScopedService] IAsyncSession session,
        [GraphQLNonNullType] string movie)
    {
        Neo4JExecutable<Movie> exe = new(session);
        HotChocolate.Data.Neo4J.Language.Operator op = new(movie);
        var filter = new HotChocolate.Data.Neo4J.Language.CompoundCondition(op);
        return (Neo4JExecutable<Movie>)exe.WithFiltering(filter);
    }
}
