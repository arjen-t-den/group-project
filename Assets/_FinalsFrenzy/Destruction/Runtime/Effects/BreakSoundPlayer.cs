using Group8.FinalsFrenzy.Audio;
using UnityEngine;
using UnityEngine.Audio;

namespace Group8.FinalsFrenzy.Destruction.Breakables
{
    /// <summary>
    /// Plays a sound when the breakable is broken.
    /// </summary>
    [RequireComponent(typeof(IBreakable))]
    public class BreakSoundPlayer : MonoBehaviour
    {
        [SerializeField]
        private AudioResource _audioResource;
        [SerializeField] 
        private CameraShake _cameraShake;

        private IBreakable _breakable;
        private ParticlesManager pm;

        private void Awake()
        {
            _breakable = GetComponent<IBreakable>();
            pm = FindFirstObjectByType<ParticlesManager>(); 
        } 

        private void OnEnable()
        {
            _breakable.OnBreak += PlayBreakSound;
            _breakable.OnBreak += PlayBreakEffect;
            _breakable.OnBreak += PlayCameraShake;
        } 

        private void OnDisable()
        {
            _breakable.OnBreak -= PlayBreakSound;
            _breakable.OnBreak -= PlayBreakEffect;
            _breakable.OnBreak -= PlayCameraShake;
        }

        private void PlayBreakSound() => AudioUtils.PlayResourceAtPoint(_audioResource, transform.position);
        private void PlayBreakEffect() {
            pm.PlayEffect("ObjectShatter", transform.position, 1f);
            pm.PlayEffect("StarShatter", transform.position, 1f);
            pm.PlayEffect("TextGREAT", transform.position, 1f);
        } 
        private void PlayCameraShake()
        {
            _cameraShake?.TriggerShake(.5f, 3f);
        }
    }
}
