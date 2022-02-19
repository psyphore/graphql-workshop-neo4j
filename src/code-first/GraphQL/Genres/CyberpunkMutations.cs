
using MoviesAPI.Models;
using MoviesAPI.Services;

namespace MoviesAPI.Schema.Genres;

[ExtendObjectType(OperationTypeNames.Mutation)]
public class CyberpunkMutations
{
    private readonly string cyberpunkgenre = nameof(cyberpunkgenre);

    [GraphQLName("signActor")]
    public async ValueTask<Person> SignActor(
        [Service] PersonService service,
        [GlobalState("neo4jdatabase")] string dbname,
        [GraphQLNonNullType] string fullName,
        CancellationToken cancellationToken = default
        )
    {
        return await service.CreateActor(fullName, dbname, cancellationToken);
    }

    [GraphQLName("produceMovie")]
    public async ValueTask<Movie> ProduceMovie(
        [Service] MovieService service,
        [GlobalState("neo4jdatabase")] string dbname,
        [GraphQLNonNullType] string title
        )
    {
        return await service.UpdateMovie(title, dbname);
    }


    public async ValueTask<Movie> CastActor(
        [Service] MovieService service,
        [GlobalState("neo4jdatabase")] string dbname,
        [GraphQLNonNullType] string title,
        [GraphQLNonNullType] string actor
        )
    {
        return await service.CastActor(title, actor, dbname);
    }

}
