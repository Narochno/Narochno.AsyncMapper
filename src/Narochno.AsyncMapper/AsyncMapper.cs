using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;

namespace Narochno.AsyncMapper
{
    public class AsyncMapper<TSource> : IAsyncMapper<TSource>
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IMapper mapper;

        public AsyncMapper(IServiceProvider serviceProvider, IMapper mapper)
        {
            this.serviceProvider = serviceProvider;
            this.mapper = mapper;
        }

        public async Task<TDestination> Map<TDestination>(TSource source)
        {
            var type = typeof(IAsyncMapping<,>).MakeGenericType(typeof(TSource), typeof(TDestination));
            var mapping = serviceProvider.GetService(type);
            if (mapping == null)
            {
                return mapper.Map<TDestination>(source);
            }

            var result = (Task<TDestination>)type.GetMethod("Map").Invoke(mapping, new object[] {source});
            return await result;
        }

        public async Task<IList<TDestination>> Map<TDestination>(IEnumerable<TSource> source)
        {

            var type = typeof(IAsyncCollectionMapping<,>).MakeGenericType(typeof(TSource), typeof(TDestination));
            var mapping = serviceProvider.GetService(type);

            if (mapping != null)
            {
                var collectionMapMethod = type.GetMethod("Map");
                var collectionResult = (Task<IEnumerable<TDestination>>)collectionMapMethod.Invoke(mapping, new object[] {source});
                return (await collectionResult).ToList();
            }

            type = typeof(IAsyncMapping<,>).MakeGenericType(typeof(TSource), typeof(TDestination));
            mapping = serviceProvider.GetService(type);

            var dest = new List<TDestination>();
            if (mapping == null)
            {
                foreach (var s in source)
                {
                    dest.Add(mapper.Map<TDestination>(s));
                }
                return dest;
            }

            var methodInfo = type.GetMethod("Map");
            foreach (var s in source)
            {
                var result = (Task<TDestination>)methodInfo.Invoke(mapping, new object[] {s});
                dest.Add(await result);
            }
            return dest;
        }
    }
}