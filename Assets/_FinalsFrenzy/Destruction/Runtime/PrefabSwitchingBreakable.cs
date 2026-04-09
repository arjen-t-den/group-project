using UnityEngine;

namespace Group8.FinalsFrenzy.Destruction
{
    /// <summary>
    /// A simple prefab-switching breakable object.
    /// </summary>
    public class PrefabSwitchingBreakable : MonoBehaviour, IBreakable
    {
        [SerializeField]
        private GameObject _brokenPrefab;

        /// <summary>
        /// Instantiates a broken version of the object (if available) and destroys the current one.
        /// </summary>
        public void Break()
        {
            if (_brokenPrefab)
                Instantiate(_brokenPrefab, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }
}
