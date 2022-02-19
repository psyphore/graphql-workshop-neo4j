
using HotChocolate.Data.Neo4J.Execution;

using Neo4j.Driver;

namespace MoviesAPI.Extensions;

public static class ExecutableExtensions
{
    public async static ValueTask<IEnumerable<T>> MaterializeNodes<T>(
        this INeo4JExecutable executable,
        CancellationToken cancellationToken = default)
    {
        var response = await executable.Source.As<IAsyncSession>().RunAsync(executable.Print());
        var items = await response.MapAsync<T>(cancellationToken);
        return items;
    }

    public async static ValueTask<T> MaterializeNode<T>(
        this INeo4JExecutable executable,
        CancellationToken cancellationToken = default)
    {
        var item = (await executable.FirstOrDefaultAsync(cancellationToken))
            .As<T>();

        return item;
    }
}
