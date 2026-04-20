using Group8.FinalsFrenzy.Destruction.Breakables;
using UnityEngine;

namespace Group8.FinalsFrenzy.Weapons
{
    public class Projectile : MonoBehaviour
    {
        private bool _hasCollided = false;

        private void Awake() => Destroy(gameObject, 3f);

        private void OnCollisionEnter(Collision collision)
        {
            if (_hasCollided) return;
            if (!collision.collider.TryGetComponent<IBreakable>(out var breakable)) return;
            _hasCollided = true;
            breakable.Break(collision.GetContact(0).point, collision.impulse);
            Destroy(gameObject);
        }
    }
}
