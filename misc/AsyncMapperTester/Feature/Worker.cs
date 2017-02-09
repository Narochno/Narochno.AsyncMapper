using System.Threading.Tasks;
using Narochno.AsyncMapper;

namespace AsyncMapperTester.Feature
{
    public class Worker
    {
        private readonly IAsyncMapper<SourceClass> mapper;

        public Worker(IAsyncMapper<SourceClass> mapper)
        {
            this.mapper = mapper;
        }

        public async Task<DestinationClass> DoWork(SourceClass source)
        {
            return await mapper.Map<DestinationClass>(source);
        }
    }
}