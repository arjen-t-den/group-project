using Group8.FinalsFrenzy.Destruction.Breakables;
using UnityEngine;

namespace Group8.FinalsFrenzy.Weapons
{
    /// <summary>
    /// Simple weapon that breaks the object in front of it.
    /// </summary>
    public class Hammer : MonoBehaviour, IWeapon
    {
        private readonly float _maxDistance = 3f;

        /// <summary>
        /// Break the breakable object ahead, if there is one within range.
        /// </summary>
        public void Attack()
        {
            var ray = new Ray(transform.position, transform.forward);
            if (!Physics.Raycast(ray, out var hit, _maxDistance)) return;
            if (!hit.collider.TryGetComponent(out IBreakable breakable)) return;
            breakable.BreakAtPoint(hit.point);
        }
    }
}
