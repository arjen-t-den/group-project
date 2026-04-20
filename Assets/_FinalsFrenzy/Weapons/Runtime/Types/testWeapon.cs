using Group8.FinalsFrenzy.Destruction.Breakables;
using Group8.FinalsFrenzy.Button;
using UnityEngine;

namespace Group8.FinalsFrenzy.Weapons
{
    [CreateAssetMenu(menuName = "Finals Frenzy/Weapons/testweapon")]
    public class testWeapon : Weapon
    {
        [SerializeField]
        private float _maxDistance = 5f;
        public override string itemLabel => "testWeapon";

        /// <summary>
        /// Break a breakable object in front of it, if there is one within range.
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
            breakable.Break(origin, direction);
        }
    }
}
