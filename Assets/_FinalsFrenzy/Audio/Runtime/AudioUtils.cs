using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace Group8.FinalsFrenzy.Audio
{
    /// <summary>
    /// Extension methods for working with audio in the game.
    /// </summary>
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

        /// <summary>
        /// Plays an AudioResource at a given position in world space.
        /// </summary>
        /// <param name="resource">Audio data to play.</param>
        /// <param name="position">Position in world space from which sound originates.</param>
        /// <param name="volume">Playback volume (range from 0.0 - 1.0).</param>
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
