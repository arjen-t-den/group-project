using UnityEngine;

namespace Group8.FinalsFrenzy.Destruction.Breakables
{
    /// <summary>
    /// Breaks an object if it's collides with a high enough force.
    /// </summary>
    [RequireComponent(typeof(IBreakable))]
    public class BreakOnCollision : MonoBehaviour
    {
        [SerializeField]
        private float _thresholdImpulse = 50f;

        private IBreakable _breakable;

        private void Awake()
        {
            _breakable = GetComponent<IBreakable>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            var impulse = collision.impulse;
            if (impulse.magnitude < _thresholdImpulse) return;

            var point = collision.GetContact(0).point;
            _breakable.Break(point, impulse.normalized);
        }
    }
}
