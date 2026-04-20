using UnityEngine;
using UnityEngine.InputSystem;

namespace Group8.FinalsFrenzy.Player
{
    /// <summary>
    /// Handles grabbing and releasing objects.
    /// </summary>
    [RequireComponent(typeof(ConfigurableJoint))]
    public class GrabbingController : MonoBehaviour
    {
        private readonly float _maxDistance = 3f;

        [SerializeField]
        private InputActionReference _grabReference;

        private Grabbable _grabbable;
        private ConfigurableJoint _joint;
        private Rigidbody _rigidbody;

        private void Awake() => _joint = GetComponent<ConfigurableJoint>();

        private void OnEnable()
        {
            _grabReference.action.performed += Grab;
            _grabReference.action.canceled += Release;
        }

        private void OnDisable()
        {
            _grabReference.action.performed -= Grab;
            _grabReference.action.canceled -= Release;
        }

        private void Grab(InputAction.CallbackContext _)
        {
            var ray = new Ray(transform.position, transform.forward);
            if (!Physics.Raycast(ray, out var hit, _maxDistance)) return;
            if (!hit.collider.TryGetComponent(out _grabbable)) return;

            _grabbable.Grab();

            _rigidbody = _grabbable.Rigidbody;
            if (!_rigidbody) return;

            _joint.connectedBody = _rigidbody;
        }

        private void Release(InputAction.CallbackContext _)
        {
            if (_grabbable) _grabbable.Release();
            _joint.connectedBody = null;
        }
    }
}
