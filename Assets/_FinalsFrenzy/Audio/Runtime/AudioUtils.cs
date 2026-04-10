using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace Group8.FinalsFrenzy.Audio
{
    public static class AudioUtils
    {
        private class CoroutineRunner : MonoBehaviour { }

        private static CoroutineRunner _runner;

        private static CoroutineRunner Runner
        {
            get
            {
                if (_runner) return _runner;

                var runnerObject = new GameObject("AudioUtils Runner");
                Object.DontDestroyOnLoad(runnerObject);
                _runner = runnerObject.AddComponent<CoroutineRunner>();
                return _runner;
            }
        }

        public static void PlayResourceAtPoint(AudioResource resource, Vector3 position, float volume = 1f)
        {
            GameObject gameObject = new("One shot audio");
            gameObject.transform.position = position;

            var audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.resource = resource;
            audioSource.spatialBlend = 1f;
            audioSource.volume = volume;
            audioSource.Play();

            Runner.StartCoroutine(DestroyWhenFinished(audioSource));
        }

        private static IEnumerator DestroyWhenFinished(AudioSource source)
        {
            while (source.isPlaying)
                yield return null;

            Object.Destroy(source.gameObject);
        }
    }
}
