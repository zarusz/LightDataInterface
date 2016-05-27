# LightDataInterface

##Benefits

* Common and light DAL interfaces make your application layer code independent of the specific DAL technology.
* Easier to switch from an existing DAL implementation (e.g. NHibernate) to another (e.g. Entity Framework).
* Eliminates boilerplate when coordinating DAL (e.g. executing unit of work in a transaction).

##Features

###Common interfaces
The DAL client code (e.g. application layer) uses minimalistic and straightforward interfaces to coordinate the data access layer. No dependencies on any particular framework (e.g. NHibernate, ADO.NET or EntityFramework).

* `IDataSession`
* `IUnitOfWork`
* `IDataSessionFactory`
* `IUnitOfWorkFactory`
* `DataSession`

When you decide to move to a different data access implementation or have two different DAL strategies - this gets easier.

Package: `LightDataInterface`

###Providers for your favorite data access libraries

* Adapter for EntityFramework
  * Package: `LightDataInterface.EntityFramework`
* Adapter for NHibernate (*pending*)
  * Package: `LightDataInterface.NHibernate`
* Adapter for MongoDb (*roadmap/future*)
* Adapter for ADO.NET (*roadmap/future*)

### WebApi integration

`[UnitOfWork]` attribute wraps controller's action in a transaction (`UnitOfWork`). When all executes fine the transaction is commited automatically, otherwise when an exception occurs the transaction is rolled back automatically. 

```CS
[HttpPost]
[Route("{markId}/Like")]
[UnitOfWork] // method wrapped in a transaction
public HttpResponseMessage Like(int markId, bool value)
{
    var mark = _markRepo.FindById(markId, MarkFetchStrategy.Like);
    if (mark == null)
    {
        return Request.CreateResponse(HttpStatusCode.NotFound);
    }

    mark.SetLiked(CurrentUser, value);
    return Request.CreateResponse(HttpStatusCode.OK);
}
```

Package: `LightDataInterface.Extra.WebApi`

## Examples

### Entity Framework and WebApi example

#### Domain model layer

ToDo

#### Services layer (WebApi)

ToDo

#### Data access layer (EntityFramework)

ToDo

#### Setup/configuration (Autofac)

ToDo

##Packages
Name | Descripton | Dependencies
------------ | ------------- | -------------
`LightDataInterface` | The interfaces to work with LightDataInterface | `Common.Logging`
`LightDataInterface.Core` | The core classes that help with setup and provide reusable runtime implementation for DAL providers | `LightDataInterface`
`LightDataInterface.EntityFramework` | Provider for Entity Framework | `LightDataInterface.Core` `EntityFramework`
`LightDataInterface.Extra.WebApi` | Adds integration goodies for WebApi | `LightDataInterface.Core` `Microsoft.AspNet.WebApi.Core`

##License

[Apache License 2.0](http://www.apache.org/licenses/LICENSE-2.0)
