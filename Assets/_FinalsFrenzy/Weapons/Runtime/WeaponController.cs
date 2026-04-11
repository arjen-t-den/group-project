using Group8.FinalsFrenzy.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Group8.FinalsFrenzy.Weapons
{
    /// <summary>
    /// Handles the switching and attacking of the player's weapons.
    /// </summary>
    public class WeaponController : MonoBehaviour
    {
        [SerializeField]
        private InputActionReference _attack;

        [SerializeField]
        [RequireInterface(typeof(IWeapon))]
        private Object _weapon;
        public IWeapon Weapon
        {
            get => _weaponRef.Get(_weapon);
            set => _weaponRef.Set(ref _weapon, value);
        }
        private readonly UnityObjectReferenceCache<IWeapon, Object> _weaponRef = new();

        private void Awake() => _attack.action.Enable();

        private void OnEnable() => _attack.action.performed += Attack;

        private void OnDisable() => _attack.action.performed -= Attack;

        private void Attack(InputAction.CallbackContext _) => Weapon.Attack();
    }
}
