using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Narochno.AsyncMapper;

namespace AsyncMapperTester
{
    public class SourceClassMapping : IAsyncMapping<SourceClass, DestinationClass>
    {
        private readonly IMapper mapper;

        public SourceClassMapping(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public async Task<DestinationClass> Map(SourceClass source)
        {
            var dest = mapper.Map<DestinationClass>(source);
            using (var reader = new StreamReader(File.OpenRead("project.json")))
            {
                dest.Extra = await reader.ReadToEndAsync();
            }
            return dest;
        }
    }
}