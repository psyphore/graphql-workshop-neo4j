
using LanguageExt;

using MoviesAPI.Executables;
using MoviesAPI.Extensions;
using MoviesAPI.Models;

using Neo4j.Driver;

namespace MoviesAPI.Services;

public class MovieService
{
    private readonly IDriver _driver;

    public MovieService(IDriver driver) => _driver = driver;

    public async ValueTask<IEnumerable<Movie>> GetMovies(string dbname, CancellationToken cancellationToken = default)
    {
        var exe = new FlexExecutable<Movie>(_driver, dbname);
        var movies = await exe.MaterializeNodes<Movie>(cancellationToken: cancellationToken);
        return movies;
    }

    public async ValueTask<Option<Movie>> GetMovie(string name, string dbname, CancellationToken cancellationToken = default)
    {
        var filter_op = new HotChocolate.Data.Neo4J.Language.Operator(name);

        var exe = new FlexExecutable<Movie>(_driver, dbname)
            .WithFiltering(new HotChocolate.Data.Neo4J.Language.CompoundCondition(filter_op));

        var movie = await exe.MaterializeNode<Movie>(cancellationToken: cancellationToken);
        return movie is null ? Option<Movie>.None : movie;
    }

    public async ValueTask<Movie> UpdateMovie(string title, string dbname, CancellationToken cancellationToken = default)
    {
        var movie = new Movie
        {
            Title = title
        };

        var exists = await GetMovie(title, dbname, cancellationToken);
        exists.IfSome(m => movie = m);

        var exe = new FlexExecutable<Movie>(_driver, dbname);
        var result = await exe.UpdateAsync(movie, cancellationToken);

        return movie;
    }

    public async ValueTask<Movie> CastActor(string title, string actor, string dbname, CancellationToken cancellationToken = default)
    {
        var movie_exe = new FlexExecutable<Movie>(_driver, dbname);

        var movie = new Movie
        {
            Title = title
        };

        var star = new Person
        {
            Name = actor
        };

        movie.Actors.Add(star);

        return await Task.FromResult(movie);
    }

}
