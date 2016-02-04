
namespace System.InversionOfControl.Abstractions.SimpleIoc
{
    /// <summary>
    /// Represents an implementation of the IoC Abstractions for Simple IoC.
    /// </summary>
    public class SimpleIocContainer : IIocContainer
    {
        #region Constructors

        /// <summary>
        /// Initializes a new <see cref="SimpleIocContainer"/> instance.
        /// </summary>
        public SimpleIocContainer()
            : this(new Kernel()) { }

        /// <summary>
        /// Initializes a new <see cref="SimpleIocContainer"/> instance.
        /// </summary>
        /// <param name="kernel">The Simple IoC kernel that is to be used.</param>
        public SimpleIocContainer(Kernel kernel)
        {
            this.Kernel = kernel;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the Simple IoC kernel, which is used internally for the dependency injection.
        /// </summary>
        public Kernel Kernel { get; private set; }

        #endregion

        #region IIocContainer Implementation

        /// <summary>
        /// Resolves an instance of the specified type.
        /// </summary>
        /// <param name="typeToResolve">The type that is to be resolved.</param>
        /// <returns>Returns a resolved instance of the specified type.</returns>
        public object GetInstance(Type typeToResolve) => this.Kernel.Resolve(typeToResolve);

        /// <summary>
        /// Resolves an instance of the specified type.
        /// </summary>
        /// <param name="typeToResolve">The type that is to be resolved.</param>
        /// <param name="constructorParameters">Constructor parameters, which are preferred over registered types.</param>
        /// <returns>Returns a resolved instance of the specified type.</returns>
        public object GetInstance(Type typeToResolve, params object[] constructorParameters) => this.Kernel.Resolve(typeToResolve, constructorParameters);

        /// <summary>
        /// Resolves an instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type that is to be resolved.</typeparam>
        /// <returns>Returns a resolved instance of the specified type.</returns>
        public T GetInstance<T>() => this.Kernel.Resolve<T>();

        /// <summary>
        /// Resolves an instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type that is to be resolved.</typeparam>
        /// <param name="constructorParameters">Constructor parameters, which are preferred over registered types.</param>
        /// <returns>Returns a resolved instance of the specified type.</returns>
        public T GetInstance<T>(params object[] constructorParameters) => this.Kernel.Resolve<T>(constructorParameters);

        /// <summary>
        /// Registers a type and binds it to itself in the specified scope.
        /// </summary>
        /// <typeparam name="T">The type that is to be registered.</typeparam>
        /// <param name="scope">The scope in which the type should be resolved.</param>
        public void RegisterType<T>(Scope scope = Scope.Transient) => this.Kernel.Bind<T>().ToSelf().InScope(scope == Scope.Transient ? ResolvingScope.Transient : ResolvingScope.Singleton);

        /// <summary>
        /// Registers a type and binds it to the specified factory method in the specified scope.
        /// </summary>
        /// <typeparam name="T">The type that is to be registered.</typeparam>
        /// <param name="factory">The factory method that resolves the registered type.</param>
        /// <param name="scope">The scope in which the type should be resolved.</param>
        public void RegisterType<T>(Func<T> factory, Scope scope = Scope.Transient) => this.Kernel.Bind<T>().ToFactory(factory).InScope(scope == Scope.Transient ? ResolvingScope.Transient : ResolvingScope.Singleton);

        /// <summary>
        /// Registers a type and binds it to itself in the specified scope.
        /// </summary>
        /// <typeparam name="T">The type that is to be registered.</typeparam>
        /// <param name="whenInjectedInto">The type is only resolved when it is injected into this type.</param>
        /// <param name="onlyInjectExactlyInto">Determines whether the type is resolved when being injected into the specified type and its sub-types only only when being injected into the specified type.</param>
        /// <param name="scope">The scope in which the type should be resolved.</param>
        public void RegisterType<T>(Type whenInjectedInto, bool onlyInjectExactlyInto = false, Scope scope = Scope.Transient)
        {
            if (onlyInjectExactlyInto)
                this.Kernel.Bind<T>().ToSelf().InScope(scope == Scope.Transient ? ResolvingScope.Transient : ResolvingScope.Singleton).WhenInjectedExactlyInto(whenInjectedInto);
            else
                this.Kernel.Bind<T>().ToSelf().InScope(scope == Scope.Transient ? ResolvingScope.Transient : ResolvingScope.Singleton).WhenInjectedInto(whenInjectedInto);
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
            if (onlyInjectExactlyInto)
                this.Kernel.Bind<T>().ToFactory(factory).InScope(scope == Scope.Transient ? ResolvingScope.Transient : ResolvingScope.Singleton).WhenInjectedExactlyInto(whenInjectedInto);
            else
                this.Kernel.Bind<T>().ToFactory(factory).InScope(scope == Scope.Transient ? ResolvingScope.Transient : ResolvingScope.Singleton).WhenInjectedInto(whenInjectedInto);
        }

        /// <summary>
        /// Registers a type and binds it to the specified type in the specified scope.
        /// </summary>
        /// <typeparam name="T">The type that is to be registered.</typeparam>
        /// <typeparam name="U">The type to which the registered type is being bound.</typeparam>
        /// <param name="scope">The scope in which the type should be resolved.</param>
        public void RegisterType<T, U>(Scope scope = Scope.Transient) where U : T => this.Kernel.Bind<T>().ToType<U>().InScope(scope == Scope.Transient ? ResolvingScope.Transient : ResolvingScope.Singleton);

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
            if (onlyInjectExactlyInto)
                this.Kernel.Bind<T>().ToType<U>().InScope(scope == Scope.Transient ? ResolvingScope.Transient : ResolvingScope.Singleton).WhenInjectedExactlyInto(whenInjectedInto);
            else
                this.Kernel.Bind<T>().ToType<U>().InScope(scope == Scope.Transient ? ResolvingScope.Transient : ResolvingScope.Singleton).WhenInjectedInto(whenInjectedInto);
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