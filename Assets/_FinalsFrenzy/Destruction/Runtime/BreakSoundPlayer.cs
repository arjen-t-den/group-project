using Group8.FinalsFrenzy.Audio;
using Group8.FinalsFrenzy.Utilities;
using UnityEngine;
using UnityEngine.Audio;

namespace Group8.FinalsFrenzy.Destruction
{
    [RequireComponent(typeof(IBreakable))]
    public class BreakSoundPlayer : MonoBehaviour
    {
        [SerializeField]
        private AudioResource _audioResource;

        private IBreakable _breakable;

        private void Awake() => _breakable = GetComponent<IBreakable>();

        private void OnEnable() => _breakable.OnBreak += PlayBreakSound;

        private void OnDisable() => _breakable.OnBreak -= PlayBreakSound;

        private void PlayBreakSound() => AudioUtils.PlayResourceAtPoint(_audioResource, transform.position);
    }
}
