
using LanguageExt;

using MoviesAPI.Executables;
using MoviesAPI.Extensions;
using MoviesAPI.Models;

using Neo4j.Driver;

namespace MoviesAPI.Services;

public class PersonService
{
    private readonly IDriver _driver;

    public PersonService(IDriver driver) => _driver = driver;

    public async ValueTask<IEnumerable<Person>> GetActors(string dbname, CancellationToken cancellationToken = default)
    {
        var exe = new FlexExecutable<Person>(_driver, dbname);
        var actors = await exe.MaterializeNodes<Person>(cancellationToken);
        return actors;
    }

    public async ValueTask<Option<Person>> GetActor(string name, string dbname, CancellationToken cancellationToken = default)
    {
        var filter_op = new HotChocolate.Data.Neo4J.Language.Operator(name);
        var exe = new FlexExecutable<Person>(_driver, dbname)
            .WithFiltering(new HotChocolate.Data.Neo4J.Language.CompoundCondition(filter_op));

        var actor = await exe.MaterializeNode<Person>(cancellationToken);
        return actor is null ? Option<Person>.None : actor;
    }

    public async ValueTask<Person> CreateActor(string name, string dbname, CancellationToken cancellationToken = default)
    {
        Person star = new()
        {
            Name = name,
        };

        var actor = await GetActor(name, dbname, cancellationToken);
        actor.IfSome(actor => star = actor);

        var exe = new FlexExecutable<Person>(_driver, dbname);
        var result = await exe.UpdateAsync(star, cancellationToken);

        return star;
    }
}
