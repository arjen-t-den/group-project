using UnityEngine;

namespace Group8.FinalsFrenzy.Destruction.Breakables.Assembly
{
    /// <summary>
    /// A container for parts that swaps to a higher LOD when broken.
    /// </summary>
    public class PartLOD : Breakable
    {
        [SerializeField]
        private GameObject _nextLOD;

        /// <inheritdoc/>
        /// <summary>
        /// Swaps the current LOD out for the next LOD and breaks that.
        /// </summary>
        public override void Break(Vector3 point, Vector3 direction)
        {
            base.Break(point, direction);

            // Swap to higher LOD
            var nextLOD = Instantiate(_nextLOD, transform.position, transform.rotation);
            nextLOD.name = _nextLOD.name;

            gameObject.SetActive(false);
            Destroy(gameObject);

            // Look for even higher LODs
            var colliders = Physics.OverlapSphere(point, 0.001f);
            foreach (var collider in colliders)
            {
                // Continue if highest LOD hasn't been reached yet
                if (!collider.TryGetComponent<IBreakable>(out var breakable))
                    continue;

                // Break the highest LOD (single part)
                breakable.Break(point, direction);
                break;
            }
        }
    }
}
