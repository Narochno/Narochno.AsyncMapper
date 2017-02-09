using System.Collections.Generic;
using System.Threading.Tasks;

namespace Narochno.AsyncMapper
{
    public interface IAsyncMapper<TSource>
    {
        Task<TDestination> Map<TDestination>(TSource source);
        Task<IList<TDestination>> Map<TDestination>(IEnumerable<TSource> source);
    }
}