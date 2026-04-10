using UnityEngine;
using UnityEngine.Audio;

namespace Group8.FinalsFrenzy.Destruction
{
    [RequireComponent(typeof(IBreakable))]
    public class BreakSoundPlayer : MonoBehaviour
    {
        [SerializeField]
        private AudioClip _audioClip;

        private IBreakable _breakable;

        private void Awake() => _breakable = GetComponent<IBreakable>();

        private void OnEnable() => _breakable.OnBreak += PlayBreakSound;

        private void OnDisable() => _breakable.OnBreak -= PlayBreakSound;

        private void PlayBreakSound() => AudioSource.PlayClipAtPoint(_audioClip, transform.position);
    }
}
