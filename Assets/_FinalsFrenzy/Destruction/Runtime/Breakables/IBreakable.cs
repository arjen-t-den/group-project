using System;

namespace Group8.FinalsFrenzy.Destruction.Breakables
{
    /// <summary>
    /// Interface for breakable objects.
    /// </summary>
    public interface IBreakable
    {
        /// <summary>
        /// Invoked when the object is broken.
        /// </summary>
        event Action OnBreak;

        /// <summary>
        /// Called when the object is broken.
        /// </summary>
        void Break();
    }
}
