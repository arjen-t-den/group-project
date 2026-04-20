using UnityEngine;

namespace Group8.FinalsFrenzy.Destruction.Breakables.Assembly
{
    /// <summary>
    /// Attached to a part to allow it to be broken.
    /// </summary>
    [RequireComponent(typeof(Part))]
    public class PartBreakable : Breakable
    {
        private Part _part;

        private void Awake() => _part = GetComponent<Part>();

        /// <inheritdoc/>
        /// <summary>
        /// Breaks all of the part's welds and then destroys itself.
        /// </summary>
        public override void Break(Vector3 point, Vector3 direction)
        {
            base.Break(point, direction);

            var assembly = _part.Assembly;

            foreach (var weld in _part.Welds.ToArray())
                weld.Break();

            if (assembly)
            {
                assembly.RemovePart(_part);
                Assemblies.Rebuild(assembly);
            }

            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            if (!_part.Assembly) return;
            _part.Assembly.RemovePart(_part);
        }
    }
}
