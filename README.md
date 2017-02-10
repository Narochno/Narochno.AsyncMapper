# Narochno.AsyncMapper

This is a library that sits on top of [AutoMapper](http://automapper.org/) and aims to basically provide [TypeConverters](https://github.com/AutoMapper/AutoMapper/wiki/Custom-type-converters) which are async.

AutoMapper still does the generic work of mapping from objects.  However, to fill in extra data, sometimes you need an async source.

## Why async?

Problem domain:

* Aggregate domain objects that are built from different sources of data.
* The biggest source of non-database data is a cache, which could be all async!
* Fitting a domain on top of an aging database schema is hard.  So lots of code may be required to adapt this schema to a domain.  Other async sources may be required.

Fitting the glue of building/mapping a domain object from data into a AutoMapper mapping just makes sense.

## How does it work?

* Use `IAsyncMapper` instead of `IMapper` in your code.
* Implement `IAsyncMapping` for your desired mappers.
* `AsyncMapper` will fallback to `IMapper` when an instance of `IAsyncMapping` for the requested map isn't found.

## Collection Handling

* `IAsyncMapper` will do a loop for collections like `IMapper` will do.
* Implement `IAsyncCollectionMapping` for optimized handling of `IEnumerable` mappings for batching async operations.

## Components in a Toy Sample

* Setup `AutoMapper` using `Profile` classes for organization: [MappingProfile.cs](https://github.com/Narochno/Narochno.AsyncMapper/blob/master/misc/AsyncMapperTester/Domain/Mapping/MappingProfile.cs)
* Use `AutoMapper` in your async mappers if needed like so: [SourceClassMapping.cs](https://github.com/Narochno/Narochno.AsyncMapper/blob/master/misc/AsyncMapperTester/Domain/Mapping/SourceClassMapping.cs)
* Glue together! [Program.cs](https://github.com/Narochno/Narochno.AsyncMapper/blob/master/misc/AsyncMapperTester/Program.cs) 

## Future work

* Optimize/cache reflection operations.
* Support more than `netstandard1.6` ?
* Support using Dependency Injection other than through `IServiceProvider` ?

