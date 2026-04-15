using UnityEngine;

namespace Group8.FinalsFrenzy.Destruction.Breakables
{
    /// <summary>
    /// A simple prefab-switching breakable object.
    /// </summary>
    public class PrefabSwitchingBreakable : Breakable
    {
        [SerializeField]
        private GameObject _brokenPrefab;

        private Rigidbody _rigidbody;

        private void Awake() => _rigidbody = GetComponentInParent<Rigidbody>();

        /// <summary>
        /// Instantiates a broken version of the object (if available) and destroys the current one.
        /// </summary>
        public override void Break(Vector3 point, Vector3 direction)
        {
            base.Break(point, direction);

            if (!_brokenPrefab)
            {
                Destroy(gameObject);
                return;
            }

            // Instantiate the broken prefab
            var brokenGameObject = Instantiate(_brokenPrefab, transform.position, transform.rotation);

            if (!_rigidbody)
            {
                Destroy(gameObject);
                return;
            }

            // Conserve linear and angular velocity of the broken pieces
            foreach (var rigidbody in brokenGameObject.GetComponentsInChildren<Rigidbody>())
            {
                rigidbody.linearVelocity = _rigidbody.GetPointVelocity(rigidbody.position);
                rigidbody.angularVelocity = _rigidbody.angularVelocity;
            }

            Destroy(gameObject);
        }
    }
}
