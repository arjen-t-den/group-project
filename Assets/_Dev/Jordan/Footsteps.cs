using UnityEngine;
using UnityEngine.InputSystem;

namespace Group8.FinalsFrenzy.Player
{
    [RequireComponent(typeof(FirstPersonMovement))]
    public class Footsteps : MonoBehaviour
    {
        private FirstPersonMovement _movement;

        [SerializeField]
        private AudioSource _walkSounds;

        [SerializeField]
        private AudioSource _runSounds;

        private bool _isWalking;
        private bool _isRunning;

        private void Awake() => _movement = GetComponent<FirstPersonMovement>();

        private void Start()
        {
            _walkSounds.Play();
            _runSounds.Play();
            _walkSounds.Pause();
            _runSounds.Pause();
        }

        private void Update()
        {
            bool isWalking = _movement.MoveDirection.sqrMagnitude > 0.5f;
            bool isRunning = _movement.IsRunning;

            if (isWalking != _isWalking || isRunning != _isRunning)
            {
                if (isWalking)
                {
                    if (_movement.IsRunning)
                    {
                        _walkSounds.Pause();
                        _runSounds.UnPause();
                    }
                    else
                    {
                        _walkSounds.UnPause();
                        _runSounds.Pause();
                    }
                }
                else
                {
                    _walkSounds.Pause();
                    _runSounds.Pause();
                }
            }

            _isWalking = isWalking;
            _isRunning = isRunning;
        }
    }
}
