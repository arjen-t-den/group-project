using UnityEngine;
using UnityEngine.InputSystem;

namespace Group8.FinalsFrenzy.Destruction
{
    public class Gun : MonoBehaviour
    {
        [SerializeField]
        private InputActionReference _attack;

        private readonly float _maxDistance = 5f;

        private void Awake() => _attack.action.Enable();

        private void OnEnable() => _attack.action.performed += Attack;

        private void OnDisable() => _attack.action.performed -= Attack;

        private void Attack(InputAction.CallbackContext _)
        {
            var ray = new Ray(transform.position, transform.forward);
            if (!Physics.Raycast(ray, out var hit, _maxDistance)) return;
            if (!hit.collider.TryGetComponent(out IBreakable breakable)) return;
            breakable.Break();
        }
    }
}
