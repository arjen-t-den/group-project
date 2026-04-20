using Group8.FinalsFrenzy.Button;
using Group8.FinalsFrenzy.Player;
using UnityEngine;

namespace Group8.FinalsFrenzy.Weapons
{
    [CreateAssetMenu(menuName = "Finals Frenzy/Weapons/Projectile")]
    public class ProjectileWeapon : Weapon
    {
        [SerializeField]
        private Rigidbody _projectile;

        [SerializeField]
        private float _strength = 20f;

        public override string itemLabel => "Projectile";

        /// <summary>
        /// Break the breakable object ahead, if there is one within range.
        /// </summary>
        public override void Attack(Vector3 origin, Vector3 direction)
        {
            var projectile = Instantiate(_projectile);
            projectile.transform.position = origin + direction * 0.5f;
            projectile.linearVelocity = FindAnyObjectByType<FootBall>().GetComponent<Rigidbody>().linearVelocity;
            projectile.AddForce(direction * _strength, ForceMode.VelocityChange);

            var ray = new Ray(origin, direction);
            if (!Physics.Raycast(ray, out var hit, 3f)) return;
            if (hit.collider.TryGetComponent(out Pressable pressable))
            {
                pressable.Press();
                return;
            }
        }
    }
}
