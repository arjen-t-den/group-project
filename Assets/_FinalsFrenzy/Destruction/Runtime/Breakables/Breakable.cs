using System;
using UnityEngine;

namespace Group8.FinalsFrenzy.Destruction.Breakables
{
    /// <summary>
    /// Base class for breakable objects.
    /// </summary>
    [DisallowMultipleComponent]
    public class Breakable : MonoBehaviour, IBreakable
    {
        /// <summary>
        /// Invoked when the object is broken.
        /// </summary>
        public event Action OnBreak;

        /// <summary>
        /// Called when the object is broken.
        /// </summary>
        public virtual void Break() => OnBreak?.Invoke();
    }
}
