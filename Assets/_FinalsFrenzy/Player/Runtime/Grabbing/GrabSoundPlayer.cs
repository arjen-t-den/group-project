using UnityEngine;
using UnityEngine.Audio;

namespace Group8.FinalsFrenzy.Player
{
    /// <summary>
    /// Plays a sound when the player grabs or releases an object.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(GrabbingController))]
    public class GrabSoundPlayer : MonoBehaviour
    {
        [SerializeField]
        private AudioResource _grabSound;

        [SerializeField]
        private AudioResource _releaseSound;

        private GrabbingController _controller;
        private AudioSource _audioSource;

        private void Awake()
        {
            _controller = GetComponent<GrabbingController>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            _controller.OnGrab += PlayGrabSound;
            _controller.OnRelease += PlayReleaseSound;
        }

        private void OnDisable()
        {
            _controller.OnGrab -= PlayGrabSound;
            _controller.OnRelease -= PlayReleaseSound;
        }

        private void PlayGrabSound() => PlaySound(_grabSound);

        private void PlayReleaseSound() => PlaySound(_releaseSound);

        private void PlaySound(AudioResource resource)
        {
            _audioSource.resource = resource;
            _audioSource.Play();
        }
    }
}
