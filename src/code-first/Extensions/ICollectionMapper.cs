
using System.Collections;

namespace MoviesAPI.Extensions
{
    public interface ICollectionMapper
    {
        IEnumerable MapValues(IEnumerable fromList, Type toInstanceOfType);
    }
}
