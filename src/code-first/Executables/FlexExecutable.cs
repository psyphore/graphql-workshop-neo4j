using HotChocolate.Data.Neo4J;
using HotChocolate.Data.Neo4J.Execution;
using HotChocolate.Data.Neo4J.Language;
using HotChocolate.Data.Neo4J.Sorting;
using HotChocolate.Language;

using MoviesAPI.Extensions;

using Neo4j.Driver;

using System.Collections;

namespace MoviesAPI.Executables;

public class FlexExecutable<T> : INeo4JExecutable, IExecutable<T>
{
    private readonly IDriver _driver;
    private readonly string _dbname;

    private static Node Node => Cypher.NamedNode(typeof(T).Name);

    public object Source => FlexExecutable<T>.GetSession(_driver, _dbname);

    private IAsyncSession Session => Source.As<IAsyncSession>();

    /// <summary>
    /// The filter definition that was set by <see cref="WithFiltering"/>
    /// </summary>
    private CompoundCondition? _filters;

    /// <summary>
    /// The sort definition that was set by <see cref="WithSorting"/>
    /// </summary>
    private Neo4JSortDefinition[]? _sorting;

    /// <summary>
    /// The projection definition that was set by <see cref="WithProjection"/>
    /// </summary>
    private object[]? _projection;

    /// <summary>
    /// The skip paging definition
    /// </summary>
    private int? _skip;

    /// <summary>
    /// The limit paging definition
    /// </summary>
    private int? _limit;

    public FlexExecutable(IDriver driver, string databaseName)
    {
        _driver = driver;
        _dbname = databaseName;
    }

    public static IAsyncSession GetSession(IDriver _driver, string databaseName) =>
        _driver.AsyncSession(o => o.WithDatabase(databaseName));

    public INeo4JExecutable WithFiltering(CompoundCondition filters)
    {
        _filters = filters;
        return this;
    }

    public INeo4JExecutable WithSorting(Neo4JSortDefinition[] sorting)
    {
        _sorting = sorting;
        return this;
    }

    public INeo4JExecutable WithSkip(int skip)
    {
        _skip = skip;
        return this;
    }

    public INeo4JExecutable WithLimit(int limit)
    {
        _limit = limit;
        return this;
    }

    public INeo4JExecutable WithProjection(object[] projection)
    {
        _projection = projection;
        return this;
    }

    /// <inheritdoc />
    public async ValueTask<IList> ToListAsync(CancellationToken cancellationToken)
    {
        IResultCursor cursor = await Session.ReadTransactionAsync(tx => tx.RunAsync(Pipeline().Build()));
        return (await cursor.MapAsync<T>(cancellationToken)).ToList();
    }

    /// <inheritdoc />
    public async ValueTask<object?> FirstOrDefaultAsync(CancellationToken cancellationToken)
    {
        IResultCursor cursor = await Session.ReadTransactionAsync(tx => tx.RunAsync(Pipeline().Build()));
        return (await cursor.MapAsync<T>(cancellationToken)).FirstOrDefault();
    }

    /// <inheritdoc />
    public async ValueTask<object?> SingleOrDefaultAsync(CancellationToken cancellationToken)
    {
        IResultCursor cursor = await Session.ReadTransactionAsync(tx => tx.RunAsync(Pipeline().Build()));
        return (await cursor.MapAsync<T>(cancellationToken)).SingleOrDefault();
    }

    public async Task<IEnumerable<T>> UpdateAsync(T node, CancellationToken cancellationToken)
    {
        var records = await Session.WriteTransactionAsync(async tx =>
        {
            var cursor = await tx.RunAsync(Pipeline().Build());
            return await cursor.MapAsync<T>(cancellationToken);
        });

        return records;

    }

    /// <inheritdoc />
    public string Print() => Pipeline().Build();

    /// <inheritdoc />
    public StatementBuilder Pipeline()
    {
        StatementBuilder statement = Cypher.Match(Node).Return(Node);

        if (_filters is not null)
        {
            statement.Match(new Where(_filters), Node);
        }

        if (_projection is not null)
        {
            statement.Return(Node.Project(_projection));
        }

        if (_sorting is null)
        {
            return statement;
        }

        var sorts = new List<SortItem>();

        foreach (Neo4JSortDefinition sort in _sorting)
        {
            SortItem sortItem = Cypher.Sort(Node.Property(sort.Field));
            if (sort.Direction == SortDirection.Ascending)
            {
                sorts.Push(sortItem.Ascending());
            }
            else if (sort.Direction == SortDirection.Descending)
            {
                sorts.Push(sortItem.Descending());
            }
        }

        statement.OrderBy(sorts);

        if (_limit is not null)
        {
            statement.Limit((int)_limit);
        }

        if (_skip is not null)
        {
            statement.Limit((int)_skip);
        }

        return statement;
    }
}
