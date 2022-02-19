
using HotChocolate.Data;

using MoviesAPI.Models;
using MoviesAPI.Services;

namespace MoviesAPI.Schema.Genres;

[ExtendObjectType(OperationTypeNames.Query)]
public class CyberpunkQueries
{
    [GraphQLName("actors")]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public async ValueTask<IQueryable<Person>> GetActors(
        [Service] PersonService service,
        [GlobalState("neo4jdatabase")] string dbname) =>
        (await service.GetActors(dbname))
        .AsQueryable();

    [GraphQLName("movies")]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public async ValueTask<IQueryable<Movie>> GetMovies(
        [Service] MovieService service,
        [GlobalState("neo4jdatabase")] string dbname) =>
        (await service.GetMovies(dbname))
        .AsQueryable();

}
