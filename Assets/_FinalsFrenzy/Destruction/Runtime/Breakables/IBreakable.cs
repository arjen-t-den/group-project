using System;
using UnityEngine;

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
        /// Breaks the object.
        /// </summary>
        /// <param name="point">The point of impact that caused the break.</param>
        /// <param name="direction">The direction of the applied impact force.</param>
        void Break(Vector3 point, Vector3 direction);
    }
}
