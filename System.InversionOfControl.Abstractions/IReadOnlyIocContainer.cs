
namespace System.InversionOfControl.Abstractions
{
    /// <summary>
    /// Represents an abstraction layer for inversion of control containers, which can only resolve instances but not register types.
    /// </summary>
    public interface IReadOnlyIocContainer
    {
        #region Methods

        /// <summary>
        /// Resolves an instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type that is to be resolved.</typeparam>
        /// <returns>Returns a resolved instance of the specified type.</returns>
        T GetInstance<T>();

        /// <summary>
        /// Resolves an instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type that is to be resolved.</typeparam>
        /// <param name="constructorParameters">Constructor parameters, which are preferred over registered types.</param>
        /// <returns>Returns a resolved instance of the specified type.</returns>
        T GetInstance<T>(params object[] constructorParameters);

        /// <summary>
        /// Resolves an instance of the specified type.
        /// </summary>
        /// <param name="typeToResolve">The type that is to be resolved.</param>
        /// <returns>Returns a resolved instance of the specified type.</returns>
        object GetInstance(Type typeToResolve);

        /// <summary>
        /// Resolves an instance of the specified type.
        /// </summary>
        /// <param name="typeToResolve">The type that is to be resolved.</param>
        /// <param name="constructorParameters">Constructor parameters, which are preferred over registered types.</param>
        /// <returns>Returns a resolved instance of the specified type.</returns>
        object GetInstance(Type typeToResolve, params object[] constructorParameters);

        #endregion
    }
}