using System.Reflection;

namespace User.Persistence
{
    /// <summary>
    /// Represents the Users module infrastructure assembly reference.
    /// </summary>
    public static class AssemblyReference
    {
        /// <summary>
        /// The assembly.
        /// </summary>
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}
