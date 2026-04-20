using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Group8.FinalsFrenzy.Player
{
    public class FirstPersonCamera : MonoBehaviour
    {
        [SerializeField]
        private InputActionReference _lookReference;

        [SerializeField]
        private float _sensitivity = 10f;
        public Slider sensitivitySlider;

        private Vector2 _cameraRotation;

        private readonly float _maxAngle = 90f;
        private readonly float _minAngle = -90f;

        private Transform _player;

        private void Awake()
        {
            _player = transform.parent;
        }

        private void OnEnable() => Cursor.lockState = CursorLockMode.Locked;

        private void OnDisable() => Cursor.lockState = CursorLockMode.None;

        public void AdjustSensitivity()
        {
            _sensitivity = (float)sensitivitySlider.value / 500.0f;
            //_sensitivity = 0.1f;
        }

        private void Update()
        {
            var look = _sensitivity * _lookReference.action.ReadValue<Vector2>();

            _cameraRotation += look;
            _cameraRotation.y = Mathf.Clamp(_cameraRotation.y, _minAngle, _maxAngle);
            var xRotation = Quaternion.AngleAxis(_cameraRotation.x, Vector3.up);
            var yRotation = Quaternion.AngleAxis(_cameraRotation.y, Vector3.left);

            _player.rotation = xRotation;
            transform.localRotation = yRotation;
        }
    }
}
