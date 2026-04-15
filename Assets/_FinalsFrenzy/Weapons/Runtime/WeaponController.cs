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
        public static WeaponController Instance {get; private set;}
        /// <summary>
        /// The currently held weapon.
        /// </summary>
        public Weapon Weapon;

        [SerializeField]
        private InputActionReference _attack;

        private void Awake() 
        { 
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                UnityEngine.Debug.Log("Duplicate deleted");
            } else {
                Instance = this;
            }
            _attack.action.Enable();
        }

        private void OnEnable() => _attack.action.performed += Attack;

        private void OnDisable() => _attack.action.performed -= Attack;

        private void Attack(InputAction.CallbackContext _) => Weapon.Attack(transform.position, transform.forward);
    }
}
