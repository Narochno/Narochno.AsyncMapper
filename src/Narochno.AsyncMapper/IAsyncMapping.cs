using System.Threading.Tasks;

namespace Narochno.AsyncMapper
{
    public interface IAsyncMapping<TSource, TDestination>
    {
        Task<TDestination> Map(TSource source);
    }
}