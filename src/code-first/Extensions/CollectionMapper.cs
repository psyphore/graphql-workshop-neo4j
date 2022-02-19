
using ServiceStack.Text;

using System.Collections;

namespace MoviesAPI.Extensions
{
    public class CollectionMapper<TResult> : ICollectionMapper
    {
        public IEnumerable MapValues(IEnumerable fromList, Type toInstanceOfType)
        {
            var to = (ICollection<TResult>)TranslateListWithElements<TResult>
                .CreateInstance(toInstanceOfType);

            foreach (object item in fromList)
            {
                to.Add(ValueMapper.MapValue<TResult>(item));
            }

            return to;
        }
    }
}
