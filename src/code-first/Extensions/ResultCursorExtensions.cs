
using Neo4j.Driver;

namespace MoviesAPI.Extensions;

public static class ResultCursorExtensions
{
    public static async ValueTask<IEnumerable<TReturn>> MapAsync<TReturn>(
        this IResultCursor resultCursor,
        CancellationToken cancellationToken = default) => await resultCursor
            .MapAsync(record => record.Map<TReturn>(), cancellationToken)
            .ConfigureAwait(false);

    private static async ValueTask<IEnumerable<TReturn>> MapAsync<TReturn>(
        this IResultCursor resultCursor,
        Func<IRecord, TReturn> mapFunc,
        CancellationToken cancellationToken = default)
    {
        var list = new List<TReturn>();
        while (await resultCursor.FetchAsync().ConfigureAwait(false))
        {
            list.Add(mapFunc(resultCursor.Current));
        }

        return list;
    }
}
