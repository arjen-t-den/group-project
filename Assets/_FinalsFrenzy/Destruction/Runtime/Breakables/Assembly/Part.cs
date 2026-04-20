using System.Collections.Generic;
using UnityEngine;

namespace Group8.FinalsFrenzy.Destruction.Breakables.Assembly
{
    /// <summary>
    /// A collider which can be connected to other parts using <see cref="Weld"/>s.
    /// </summary>
    public class Part : MonoBehaviour
    {
        public float Mass = 1f;
        public bool IsKinematic;

        public Assembly Assembly { get; set; }
        public List<Weld> Welds = new();
    }
}
