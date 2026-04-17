using UnityEngine;

namespace Group8.FinalsFrenzy.Audio
{
    /// <summary>
    /// Sets an AudioSource's volume based on the velocity of its Rigidbody.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(AudioSource))]
    public class VelocityToVolume : MonoBehaviour
    {
        [SerializeField]
        private AnimationCurve _velocityToVolumeCurve = AnimationCurve.Linear(0f, 0f, 10f, 1f);

        private AudioSource _audioSource;
        private Rigidbody _rigidbody;
        private bool _isSleeping;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnValidate()
        {
            if (!_audioSource) _audioSource = GetComponent<AudioSource>();
            if (!_audioSource) return;

            _audioSource.volume = 0f;
        }

        private void FixedUpdate()
        {
            bool isSleeping = _rigidbody.IsSleeping();
            if (isSleeping != _isSleeping)
            {
                if (isSleeping)
                    _audioSource.Pause();
                else
                    _audioSource.UnPause();
            }
            _isSleeping = isSleeping;
            if (isSleeping) return;

            var velocity = _rigidbody.linearVelocity.magnitude;
            var volume = _velocityToVolumeCurve.Evaluate(velocity);
            _audioSource.volume = volume;
        }
    }
}
