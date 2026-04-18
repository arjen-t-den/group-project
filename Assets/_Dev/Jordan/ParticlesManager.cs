using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

namespace Group8.FinalsFrenzy
{
    // A particle manager that allows you to instantiate particle systems by name and automatically destroy them after a set duration
    // @Parameter: effectname - the name of the particle system to play, position - where to spawn the particle system, duration - how long the particle system should last before being destroyed
    // declare ParticlesManager pm
    // then assign it by using pm = FindFirstObjectByType<ParticlesManager>(); 
    public class ParticlesManager : MonoBehaviour
    {
        public static ParticlesManager instance { get; private set; }

        [SerializeField] private ParticleSystem[] effects;

        private Dictionary<string, ParticleSystem> effectsDictionary;

        private void Awake()
        {
            // single manager set up
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);

            effectsDictionary = new Dictionary<string, ParticleSystem>();
            foreach (var effect in effects)
            {
                if (!effectsDictionary.ContainsKey(effect.name))
                {
                    effectsDictionary.Add(effect.name, effect);
                }
                else
                {
                    Debug.LogWarning($"Duplicate particle system name detected: {effect.name}. Only the first one will be stored.");
                }
            }
        }

        public void PlayEffect(string effectName, Vector3 position, float duration)
        {
            if (!effectsDictionary.TryGetValue(effectName, out  ParticleSystem system))
            {
                Debug.Log($"Particle system with name {effectName} not found!");
                return;
            }

            ParticleSystem instance = Instantiate(system, position, Quaternion.identity);
            instance.Play();
            StartCoroutine(DestroyAfterDuration(instance.gameObject, duration));
        }

        private IEnumerator DestroyAfterDuration(GameObject obj, float duration)
        {
            yield return new WaitForSeconds(duration);
            Destroy(obj);
        }
    }
}
