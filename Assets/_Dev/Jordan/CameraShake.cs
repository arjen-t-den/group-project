using System.Collections;
using UnityEngine;

namespace Group8.FinalsFrenzy
{
    public class CameraShake : MonoBehaviour
    {
        private Coroutine _shakeCoroutine;

        // Call this method to trigger the camera shake effect, checks if the shake is already happening and stops it before starting a new one
        // @Parameters: duration - how long the shake should last, magnitude - how intense the shake should be

        // Example: public CameraShake cameraShake; cameraShake.TriggerShake(1f, 0.5f); 
        // ^shakes the camera for 1 second with a magnitude of 0.5
        public void TriggerShake(float duration, float magnitude)
        {
            if (_shakeCoroutine != null)
                StopCoroutine(_shakeCoroutine);

            _shakeCoroutine = StartCoroutine(Shake(duration, magnitude));
        }

        // Shaking effect is created by randomly altering the camera's local rotation around the Z-axis for a specified duration and magnitude, with a gradual decrease in intensity over time
        private IEnumerator Shake(float duration, float magnitude)
        {
            float elapsed = 0.0f;
            float initialMagnitude = magnitude;

            while (elapsed < duration)
            {
                float z = Random.Range(-1f, 1f) * initialMagnitude;
                Vector3 currentRot = transform.localEulerAngles;
                transform.localRotation = Quaternion.Euler(currentRot.x, currentRot.y, z);

                elapsed += Time.deltaTime;
                initialMagnitude *= 0.99f;

                yield return null;
            }

            Vector3 finalRot = transform.localEulerAngles;
            transform.localRotation = Quaternion.Euler(finalRot.x, finalRot.y, 0f);
            _shakeCoroutine = null;
        }
    }
}