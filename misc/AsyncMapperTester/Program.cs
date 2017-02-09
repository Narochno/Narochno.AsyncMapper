using System;
using AsyncMapperTester.Feature;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Narochno.AsyncMapper;

namespace AsyncMapperTester
{
    public class Program
    {
        public static void Main()
        {
            var services = new ServiceCollection();
            services.AddAutoMapper();
            services.AddAsyncMapper();

            var provider = services.BuildServiceProvider();

            var worker = ActivatorUtilities.CreateInstance<Worker>(provider);

            var destination = worker.DoWork(new SourceClass()
                {
                    Name = "source"
                })
                .Result;

            Console.WriteLine(destination.Name);
            Console.WriteLine(destination.Extra);
        }
    }
}