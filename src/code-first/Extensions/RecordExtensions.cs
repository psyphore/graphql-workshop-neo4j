
using Neo4j.Driver;

namespace MoviesAPI.Extensions
{
    public static class RecordExtensions
    {
        public static TReturn Map<TReturn>(this IRecord record) => ValueMapper.MapValue<TReturn>(record[0]);
    }
}
