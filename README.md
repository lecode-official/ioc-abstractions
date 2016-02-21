# IoC Abstractions

![IoC Abstractions Logo](https://github.com/lecode-official/ioc-abstractions/blob/master/Documentation/Images/Banner.png "IoC Abstractions Logo")

Represents an abstraction layer for multiple IoC containers. Using the abstractions, applications can use an inversion of control container for dependency
injection, without directly seeing the underlying API. This makes it very easy to switch between different IoC implementations.

## Acknowledgments

This project would not be possible without the great contributions of the open source community. The IoC Abstractions project was build using these awesome
open source projects:

**[Ninject](https://github.com/ninject/Ninject)** - Ninject is a lightning-fast, ultra-lightweight dependency injector for .NET applications. It helps you
split your application into a collection of loosely-coupled, highly-cohesive pieces, and then glue them back together in a flexible manner. By using Ninject
to support your software's architecture, your code will become easier to write, reuse, test, and modify.

## Using the Project

Currently the project supports implementations for [Ninject](http://www.ninject.org/) and [Simple IoC](https://github.com/lecode-official/simple-ioc). The
projects are available on NuGet: https://www.nuget.org/packages/System.InversionOfControl.Abstractions.Ninject/ and
https://www.nuget.org/packages/System.InversionOfControl.Abstractions.SimpleIoc/.

If you want to use Ninject as an inversion of control container, then use the following package:

```batch
PM> Install-Package System.InversionOfControl.Abstractions.Ninject
```

If you want to use Simple IoC as an inversion of control container, then use the following package:

```batch
PM> Install-Package System.InversionOfControl.Abstractions.SimpleIoc
```

If you want to you can download and manually build the solution. The project was built using Visual Studio 2015. Basically any version of Visual Studio 2015
will suffice, no extra plugins or tools are needed (except for the NuGet projects, which need the
[NuBuild Project System](https://visualstudiogallery.msdn.microsoft.com/3efbfdea-7d51-4d45-a954-74a2df51c5d0) Visual Studio extension for building the NuGet
packagea). Just clone the Git repository, open the solution in Visual Studio, and build the solution.

```batch
git pull https://github.com/lecode-official/ioc-abstractions.git
```

## Samples

It is really simple to create a new IoC container. At first you have to choose an IoC implementation, currently Ninject and Simple IoC are supported. This is
how you create a new Simple IoC or Ninject IoC container:

```csharp
IIocContainer ninjectIocContainer = new NinjectIocContainer();
IIocContainer simpleIocContainer = new SimpleIocContainer();
```

Once you have obtained an IoC container, you can start to bind types to the IoC container:

```csharp
iocContainer.RegisterType<IVehicle, Car>();
```

You can also bind types only when they are injected into another type. In this instance, a `Motorcycle` is injected into a `Person` if the `Person` is a `SuperCoolPerson`:

```csharp
iocContainer.RegisterType<IVehicle, Motorcycle>(typeof(SuperCoolPerson)); // Obviously super cool people drive motorcycles!
```

Now you can use the IoC container to retrieve instances of the types:

```csharp
Person person = iocContainer.GetInstance<Person>();
Person superCoolPerson = iocContainer.GetInstance<SuperCoolPerson>();
```

You will notice, when you evaluate these two statements, that `person` will get an instance `Car` inject, while the `superCoolPerson` will get an instance of `Motorcycle`
injected. You can also pass arguments to the `GetInstance` method, which will be prioritzed when injecting values into the constructors. When you evaluate
the following statement, you will see, that the `namedPerson` will get the `string` `"Bob"` injected as its name.

```csharp
Person namedPerson = iocContainer.GetInstance<NamedPerson>("Bob");
```

## Adding an Implementation

If you want to provide an implementation for the IoC Abstractions for an IoC container that is currently not supported, then you should install the following
package, which is also available on NuGet https://www.nuget.org/packages/System.InversionOfControl.Abstractions/:

```batch
PM> Install-Package System.InversionOfControl.Abstractions
```

Just implement the `IIocContainer` interface and you are good to go.

## Contributions

Currently we are not accepting any contributors, but if you want to help, we would greatly appreciate feedback and bug reports. To file a bug, please use GitHub's
issue system. Alternatively, you can clone the repository and send us a pull request.
