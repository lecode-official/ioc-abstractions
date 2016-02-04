
namespace System.InversionOfControl.Abstractions
{
    /// <summary>
    /// Represents an abstraction layer for inversion of control containers.
    /// </summary>
    public interface IIocContainer : IReadOnlyIocContainer, IDisposable
    {
        #region Methods

        /// <summary>
        /// Registers a type and binds it to itself in the specified scope.
        /// </summary>
        /// <typeparam name="T">The type that is to be registered.</typeparam>
        /// <param name="scope">The scope in which the type should be resolved.</param>
        void RegisterType<T>(Scope scope = Scope.Transient);

        /// <summary>
        /// Registers a type and binds it to itself in the specified scope.
        /// </summary>
        /// <typeparam name="T">The type that is to be registered.</typeparam>
        /// <param name="whenInjectedInto">The type is only resolved when it is injected into this type.</param>
        /// <param name="onlyInjectExactlyInto">Determines whether the type is resolved when being injected into the specified type and its sub-types only only when being injected into the specified type.</param>
        /// <param name="scope">The scope in which the type should be resolved.</param>
        void RegisterType<T>(Type whenInjectedInto, bool onlyInjectExactlyInto = false, Scope scope = Scope.Transient);

        /// <summary>
        /// Registers a type and binds it to the specified type in the specified scope.
        /// </summary>
        /// <typeparam name="T">The type that is to be registered.</typeparam>
        /// <typeparam name="U">The type to which the registered type is being bound.</typeparam>
        /// <param name="scope">The scope in which the type should be resolved.</param>
        void RegisterType<T, U>(Scope scope = Scope.Transient) where U : T;

        /// <summary>
        /// Registers a type and binds it to the specified type in the specified scope.
        /// </summary>
        /// <typeparam name="T">The type that is to be registered.</typeparam>
        /// <typeparam name="U">The type to which the registered type is being bound.</typeparam>
        /// <param name="whenInjectedInto">The type is only resolved when it is injected into this type.</param>
        /// <param name="onlyInjectExactlyInto">Determines whether the type is resolved when being injected into the specified type and its sub-types only only when being injected into the specified type.</param>
        /// <param name="scope">The scope in which the type should be resolved.</param>
        void RegisterType<T, U>(Type whenInjectedInto, bool onlyInjectExactlyInto = false, Scope scope = Scope.Transient) where U : T;

        /// <summary>
        /// Registers a type and binds it to the specified factory method in the specified scope.
        /// </summary>
        /// <typeparam name="T">The type that is to be registered.</typeparam>
        /// <param name="factory">The factory method that resolves the registered type.</param>
        /// <param name="scope">The scope in which the type should be resolved.</param>
        void RegisterType<T>(Func<T> factory, Scope scope = Scope.Transient);

        /// <summary>
        /// Registers a type and binds it to the specified factory method in the specified scope.
        /// </summary>
        /// <typeparam name="T">The type that is to be registered.</typeparam>
        /// <param name="factory">The factory method that resolves the registered type.</param>
        /// <param name="whenInjectedInto">The type is only resolved when it is injected into this type.</param>
        /// <param name="onlyInjectExactlyInto">Determines whether the type is resolved when being injected into the specified type and its sub-types only only when being injected into the specified type.</param>
        /// <param name="scope">The scope in which the type should be resolved.</param>
        void RegisterType<T>(Func<T> factory, Type whenInjectedInto, bool onlyInjectExactlyInto = false, Scope scope = Scope.Transient);

        #endregion
    }
}