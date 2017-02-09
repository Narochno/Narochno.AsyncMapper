using System.Collections.Generic;
using System.Threading.Tasks;

namespace Narochno.AsyncMapper
{
    public interface IAsyncCollectionMapping<TSource, TDestination>
    {
        Task<IEnumerable<TDestination>> Map(IEnumerable<TSource> source);
    }
}