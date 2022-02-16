using HotChocolate.Data.Neo4J;
using HotChocolate.Data.Neo4J.Execution;

using LanguageExt;

using MoviesAPI.Models;

using Neo4j.Driver;

namespace MoviesAPI.Schema.Genres;

[ExtendObjectType(OperationTypeNames.Mutation)]
public class CyberpunkMutations
{
    private readonly string cyberpunkgenre = nameof(cyberpunkgenre);

    [GraphQLName("signActor")]
    [UseNeo4JDatabase(nameof(cyberpunkgenre))]
    public async ValueTask<Actor> SignActor(
        [ScopedService] IAsyncSession session,
        [ScopedService] CyberpunkQueries cyberpunkQueries,
        [GraphQLNonNullType] string fullName,
        [GraphQLNonNullType] string movie,
        CancellationToken cancellationToken = default
        )
    {
        var matched = await GetMovie(cyberpunkQueries, session, movie, cancellationToken);

        var actor = new Actor
        {
            Name = fullName,
        };

        matched.IfSome(m => actor.ActedIn.Add(m));

        Neo4JExecutable<Actor> exe = new(session);

        return await ValueTask.FromResult(actor);
    }

    [GraphQLName("produceMovie")]
    [UseNeo4JDatabase(nameof(cyberpunkgenre))]
    public async ValueTask<Movie> ProduceMovie(
        [ScopedService] IAsyncSession session,
        [GraphQLNonNullType] string title
        )
    {
        var movie = new Movie
        {
            Title = title
        };

        Neo4JExecutable<Movie> exe = new(session);

        return await ValueTask.FromResult(movie);
    }


    private static async ValueTask<Option<Movie>> GetMovie(CyberpunkQueries cyberpunkQueries, IAsyncSession session, string movie, CancellationToken cancellationToken = default)
    {
        // do some stuff on neo4j
        HotChocolate.Data.Neo4J.Language.Operator op = new(movie);

        var filter = new HotChocolate.Data.Neo4J.Language.CompoundCondition(
            op
            );
        var movies = cyberpunkQueries.GetMovies(session);

        var matched = await movies
            .WithFiltering(filter)
            .WithProjection(new[] { new Movie() })
            .FirstOrDefaultAsync(cancellationToken)
            ;

        var node = matched.As<Movie>();

        return node is null ? Option<Movie>.None : node;
    }



}
