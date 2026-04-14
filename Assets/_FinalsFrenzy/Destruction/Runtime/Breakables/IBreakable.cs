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
        /// Breaks the object at the specified point.
        /// </summary>
        /// <param name="point">The point where the fracture originated.</param>
        void BreakAtPoint(Vector3 point);
    }
}
