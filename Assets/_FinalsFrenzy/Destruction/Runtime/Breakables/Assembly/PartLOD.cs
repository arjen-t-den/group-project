using UnityEngine;

namespace Group8.FinalsFrenzy.Destruction.Breakables.Assembly
{
    /// <summary>
    /// A container for parts 
    /// </summary>
    public class PartLOD : Breakable
    {
        private GameObject _thisLOD;
        private GameObject _nextLOD;

        private void Awake()
        {
            _thisLOD = gameObject;
            _nextLOD = transform.parent.Find("Broken").gameObject;
        }

        public override void Break(Vector3 point, Vector3 direction)
        {
            base.Break(point, direction);

            // Swap to higher LOD
            _thisLOD.SetActive(false);
            _nextLOD.SetActive(true);

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
