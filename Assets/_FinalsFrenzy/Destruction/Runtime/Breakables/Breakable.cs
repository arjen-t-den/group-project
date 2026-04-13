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
        public event Action OnBreak;

        public virtual void Break() => OnBreak?.Invoke();
    }
}
