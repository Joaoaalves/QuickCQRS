using System.Reflection;

namespace Joaoaalves.QuickCQRS.Core.Modules
{
    public sealed class QuickCQRSOptions
    {
        internal List<Assembly> AssembliesInternal { get; } = [];
        /// <summary>
        /// Assemblies to be scanned for handlers.
        /// If not provided, all loaded assemblies will be scanned.
        /// If provided, will override any assemblies resolved from input parameters, and <b>forced to load</b>.
        /// </summary>
        public Assembly[] Assemblies { get; init; } = [];

        /// <summary>
        /// Add an Assembly explicitly to the list of assemblies to be scanned for handlers.
        /// </summary>
        public void AddAssembly(Assembly assembly)
        {
            ArgumentNullException.ThrowIfNull(assembly);
            AssembliesInternal.Add(assembly);
        }

        /// <summary>
        /// Add an assembly by a marker type
        /// </summary>
        public void AddAssembly<T>()
        {
            AssembliesInternal.Add(typeof(T).Assembly);
        }
    }
}