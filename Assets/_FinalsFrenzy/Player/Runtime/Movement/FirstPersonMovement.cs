using UnityEngine;
using UnityEngine.InputSystem;

namespace Group8.FinalsFrenzy.Player
{
    /// <summary>
    /// Handles character's virtual movement with smooth locomotion.
    /// Also handles running speed changes.
    /// </summary>
    public class FirstPersonMovement : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The transform that determines the forward direction for movement. Typically this would be the camera transform.")]
        private Transform _forwardTransform;

        [Header("Settings")]
        [SerializeField]
        [Tooltip("The walk speed of the character (in m/s).")]
        private float _defaultWalkSpeed = 1.5f;

        /// <summary>
        /// The speed the character walks at (in m/s).
        /// </summary>
        public float WalkSpeed { get; set; }

        /// <summary>
        /// The product of this and the walk speed is the run speed.
        /// </summary>
        [Tooltip("The product of this and the walk speed is the run speed.")]
        public float RunSpeedMultiplier = 2f;

        /// <summary>
        /// Specifies the run mode.
        /// Defaults to Toggle.
        /// </summary>
        [Tooltip("Specifies whether the run button can be toggled or needs to be held to keep running.")]
        public RunModeType RunMode = RunModeType.Toggle;

        /// <summary>
        /// Specifies whether the run button can be toggled or needs to be held to keep running.
        /// </summary>
        public enum RunModeType
        {
            Toggle,
            Hold
        }

        [Header("Input Actions")]
        [SerializeField]
        private InputActionReference _moveAction;

        [SerializeField]
        private InputActionReference _runAction;

        /// <summary>
        /// The foot ball that handles the actual movement of the character.
        /// </summary>
        public FootBall FootBall { get; private set; }

        private Vector2 _moveDirection;

        /// <summary>
        /// Is the character currently running?
        /// </summary>
        public bool IsRunning { get; private set; }

        private bool _isRunPressed;

        private Quaternion HeadForwardRotation => Quaternion.LookRotation(Vector3.Cross(_forwardTransform.right, Vector3.up));

        private void Awake()
        {
            FootBall = GetComponentInChildren<FootBall>();

            _moveAction.action.Enable();
            _runAction.action.Enable();

            ResetWalkSpeed();
        }

        private void OnEnable()
        {
            _moveAction.action.performed += OnMove;
            _moveAction.action.canceled += OnMove;

            _runAction.action.performed += OnToggleRun;
            _runAction.action.canceled += OnToggleRun;
        }

        private void OnDisable()
        {
            _moveAction.action.performed -= OnMove;
            _moveAction.action.canceled -= OnMove;

            _runAction.action.performed -= OnToggleRun;
            _runAction.action.canceled -= OnToggleRun;
        }

        private void OnMove(InputAction.CallbackContext context) => _moveDirection = context.ReadValue<Vector2>();
        private void OnToggleRun(InputAction.CallbackContext context)
        {
            if (RunMode == RunModeType.Toggle)
            {
                if (context.performed)
                    IsRunning = !IsRunning;
            }
            else
            {
                _isRunPressed = context.performed;
            }
        }

        private void FixedUpdate() => Move();

        private void Move()
        {
            if (RunMode == RunModeType.Hold) IsRunning = _isRunPressed;

            if (RunMode == RunModeType.Toggle && _moveDirection.magnitude < 0.1f)
                IsRunning = false;

            var currentSpeed = WalkSpeed;
            if (IsRunning)
                currentSpeed *= RunSpeedMultiplier;

            var targetLinearVelocity = HeadForwardRotation * new Vector3(_moveDirection.x, 0, _moveDirection.y) * currentSpeed;
            FootBall.RollFromLinearVelocity(targetLinearVelocity);
        }

        /// <summary>
        /// Resets walk speed to default walk speed
        /// </summary>
        public void ResetWalkSpeed() => WalkSpeed = _defaultWalkSpeed;
    }
}
