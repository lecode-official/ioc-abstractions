
#region Using Directives

using Ninject;
using Ninject.Parameters;
using System.Linq;

#endregion

namespace System.InversionOfControl.Abstractions.Ninject
{
    /// <summary>
    /// Represents an implementation of the IoC Abstractions for Ninject.
    /// </summary>
    public class NinjectIocContainer : IIocContainer
    {
        #region Constructors

        /// <summary>
        /// Initializes a new <see cref="NinjectIocContainer"/> instance.
        /// </summary>
        public NinjectIocContainer()
            : this(new StandardKernel()) { }

        /// <summary>
        /// Initializes a new <see cref="NinjectIocContainer"/> instance.
        /// </summary>
        /// <param name="kernel">The Simple IoC kernel that is to be used.</param>
        public NinjectIocContainer(IKernel kernel)
        {
            this.Kernel = kernel;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the Ninject kernel, which is used internally for the dependency injection.
        /// </summary>
        public IKernel Kernel { get; private set; }

        #endregion

        #region IIocContainer Implementation

        /// <summary>
        /// Resolves an instance of the specified type.
        /// </summary>
        /// <param name="typeToResolve">The type that is to be resolved.</param>
        /// <returns>Returns a resolved instance of the specified type.</returns>
        public object GetInstance(Type typeToResolve) => this.Kernel.Get(typeToResolve);

        /// <summary>
        /// Resolves an instance of the specified type.
        /// </summary>
        /// <param name="typeToResolve">The type that is to be resolved.</param>
        /// <param name="constructorParameters">Constructor parameters, which are preferred over registered types.</param>
        /// <returns>Returns a resolved instance of the specified type.</returns>
        public object GetInstance(Type typeToResolve, params object[] constructorParameters) => this.Kernel.Get(typeToResolve, constructorParameters.Select(parameter => new TypeMatchingConstructorArgument(parameter.GetType(), (context, target) => parameter)).ToArray());

        /// <summary>
        /// Resolves an instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type that is to be resolved.</typeparam>
        /// <returns>Returns a resolved instance of the specified type.</returns>
        public T GetInstance<T>() => this.Kernel.Get<T>();

        /// <summary>
        /// Resolves an instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type that is to be resolved.</typeparam>
        /// <param name="constructorParameters">Constructor parameters, which are preferred over registered types.</param>
        /// <returns>Returns a resolved instance of the specified type.</returns>
        public T GetInstance<T>(params object[] constructorParameters) => this.Kernel.Get<T>(constructorParameters.Select(parameter => new TypeMatchingConstructorArgument(parameter.GetType(), (context, target) => parameter)).ToArray());

        /// <summary>
        /// Registers a type and binds it to itself in the specified scope.
        /// </summary>
        /// <typeparam name="T">The type that is to be registered.</typeparam>
        /// <param name="scope">The scope in which the type should be resolved.</param>
        public void RegisterType<T>(Scope scope = Scope.Transient)
        {
            if (scope == Scope.Transient)
                this.Kernel.Bind<T>().ToSelf().InTransientScope();
            else
                this.Kernel.Bind<T>().ToSelf().InSingletonScope();
        }

        /// <summary>
        /// Registers a type and binds it to the specified factory method in the specified scope.
        /// </summary>
        /// <typeparam name="T">The type that is to be registered.</typeparam>
        /// <param name="factory">The factory method that resolves the registered type.</param>
        /// <param name="scope">The scope in which the type should be resolved.</param>
        public void RegisterType<T>(Func<T> factory, Scope scope = Scope.Transient)
        {
            if (scope == Scope.Transient)
                this.Kernel.Bind<T>().ToMethod(context => factory()).InTransientScope();
            else
                this.Kernel.Bind<T>().ToMethod(context => factory()).InSingletonScope();
        }

        /// <summary>
        /// Registers a type and binds it to itself in the specified scope.
        /// </summary>
        /// <typeparam name="T">The type that is to be registered.</typeparam>
        /// <param name="whenInjectedInto">The type is only resolved when it is injected into this type.</param>
        /// <param name="onlyInjectExactlyInto">Determines whether the type is resolved when being injected into the specified type and its sub-types only only when being injected into the specified type.</param>
        /// <param name="scope">The scope in which the type should be resolved.</param>
        public void RegisterType<T>(Type whenInjectedInto, bool onlyInjectExactlyInto = false, Scope scope = Scope.Transient)
        {
            if (onlyInjectExactlyInto && scope == Scope.Transient)
                this.Kernel.Bind<T>().ToSelf().WhenInjectedExactlyInto(whenInjectedInto).InTransientScope();
            else if (onlyInjectExactlyInto && scope == Scope.Singleton)
                this.Kernel.Bind<T>().ToSelf().WhenInjectedExactlyInto(whenInjectedInto).InSingletonScope();
            else if (!onlyInjectExactlyInto && scope == Scope.Transient)
                this.Kernel.Bind<T>().ToSelf().WhenInjectedInto(whenInjectedInto).InTransientScope();
            else
                this.Kernel.Bind<T>().ToSelf().WhenInjectedInto(whenInjectedInto).InSingletonScope();
        }

        /// <summary>
        /// Registers a type and binds it to the specified factory method in the specified scope.
        /// </summary>
        /// <typeparam name="T">The type that is to be registered.</typeparam>
        /// <param name="factory">The factory method that resolves the registered type.</param>
        /// <param name="whenInjectedInto">The type is only resolved when it is injected into this type.</param>
        /// <param name="onlyInjectExactlyInto">Determines whether the type is resolved when being injected into the specified type and its sub-types only only when being injected into the specified type.</param>
        /// <param name="scope">The scope in which the type should be resolved.</param>
        public void RegisterType<T>(Func<T> factory, Type whenInjectedInto, bool onlyInjectExactlyInto = false, Scope scope = Scope.Transient)
        {
            if (onlyInjectExactlyInto && scope == Scope.Transient)
                this.Kernel.Bind<T>().ToMethod(context => factory()).WhenInjectedExactlyInto(whenInjectedInto).InTransientScope();
            else if (onlyInjectExactlyInto && scope == Scope.Singleton)
                this.Kernel.Bind<T>().ToMethod(context => factory()).WhenInjectedExactlyInto(whenInjectedInto).InSingletonScope();
            else if (!onlyInjectExactlyInto && scope == Scope.Transient)
                this.Kernel.Bind<T>().ToMethod(context => factory()).WhenInjectedInto(whenInjectedInto).InTransientScope();
            else
                this.Kernel.Bind<T>().ToMethod(context => factory()).WhenInjectedInto(whenInjectedInto).InSingletonScope();
        }

        /// <summary>
        /// Registers a type and binds it to the specified type in the specified scope.
        /// </summary>
        /// <typeparam name="T">The type that is to be registered.</typeparam>
        /// <typeparam name="U">The type to which the registered type is being bound.</typeparam>
        /// <param name="scope">The scope in which the type should be resolved.</param>
        public void RegisterType<T, U>(Scope scope = Scope.Transient) where U : T
        {
            if (scope == Scope.Transient)
                this.Kernel.Bind<T>().To<U>().InTransientScope();
            else
                this.Kernel.Bind<T>().To<U>().InSingletonScope();
        }

        /// <summary>
        /// Registers a type and binds it to the specified type in the specified scope.
        /// </summary>
        /// <typeparam name="T">The type that is to be registered.</typeparam>
        /// <typeparam name="U">The type to which the registered type is being bound.</typeparam>
        /// <param name="whenInjectedInto">The type is only resolved when it is injected into this type.</param>
        /// <param name="onlyInjectExactlyInto">Determines whether the type is resolved when being injected into the specified type and its sub-types only only when being injected into the specified type.</param>
        /// <param name="scope">The scope in which the type should be resolved.</param>
        public void RegisterType<T, U>(Type whenInjectedInto, bool onlyInjectExactlyInto = false, Scope scope = Scope.Transient) where U : T
        {
            if (onlyInjectExactlyInto && scope == Scope.Transient)
                this.Kernel.Bind<T>().To<U>().WhenInjectedExactlyInto(whenInjectedInto).InTransientScope();
            else if (onlyInjectExactlyInto && scope == Scope.Singleton)
                this.Kernel.Bind<T>().To<U>().WhenInjectedExactlyInto(whenInjectedInto).InSingletonScope();
            else if (!onlyInjectExactlyInto && scope == Scope.Transient)
                this.Kernel.Bind<T>().To<U>().WhenInjectedInto(whenInjectedInto).InTransientScope();
            else
                this.Kernel.Bind<T>().To<U>().WhenInjectedInto(whenInjectedInto).InSingletonScope();
        }

        #endregion

        #region IDisposable Implementation

        /// <summary>
        /// Disposes of all the resources that have been created by the <see cref="SimpleIocContainer"/>.
        /// </summary>
        public void Dispose()
        {
            if (this.Kernel != null)
                this.Kernel.Dispose();
            this.Kernel = null;
        }

        #endregion
    }
}