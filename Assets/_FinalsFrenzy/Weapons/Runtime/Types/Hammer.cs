using Group8.FinalsFrenzy.Destruction.Breakables;
using Group8.FinalsFrenzy.Button;
using UnityEngine;

namespace Group8.FinalsFrenzy.Weapons
{
    /// <summary>
    /// Simple weapon that breaks the object in front of it.
    /// </summary>
    [CreateAssetMenu(menuName = "Finals Frenzy/Weapons/Hammer")]
    public class Hammer : Weapon
    {
        [SerializeField]
        private float _maxDistance = 3f;
        
        public override string itemLabel => "Hammer";
        /// <summary>
        /// Break the breakable object ahead, if there is one within range.
        /// </summary>
        public override void Attack(Vector3 origin, Vector3 direction)
        {
            var ray = new Ray(origin, direction);
            if (!Physics.Raycast(ray, out var hit, _maxDistance)) return;
            if (hit.collider.TryGetComponent(out Pressable pressable))
            {
                pressable.Press();
                return;
            }
            if (!hit.collider.TryGetComponent(out IBreakable breakable)) return;
            breakable.Break(hit.point, transform.forward);
        }
    }
}
