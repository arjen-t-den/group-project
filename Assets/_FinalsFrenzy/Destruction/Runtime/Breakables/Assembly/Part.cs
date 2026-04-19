using System.Collections.Generic;
using UnityEngine;

namespace Group8.FinalsFrenzy.Destruction.Breakables.Assembly
{
    public class Part : Breakable
    {
        public float Mass = 1f;
        public bool IsKinematic;

        public Assembly Assembly { get; set; }
        public List<Weld> Welds = new();

        /// <inheritdoc/>
        /// <summary>
        /// Breaks all of the part's welds and then destroys itself.
        /// </summary>
        public override void Break(Vector3 point, Vector3 direction)
        {
            base.Break(point, direction);

            var assembly = Assembly;

            foreach (var weld in Welds.ToArray())
                weld.Break();

            Assembly.RemovePart(this);
            if (assembly) Assemblies.Rebuild(assembly);
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            if (!Assembly) return;
            Assembly.Parts.Remove(this);
        }
    }
}
