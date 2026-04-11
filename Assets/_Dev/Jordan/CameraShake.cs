using System.Collections;
using UnityEngine;

namespace Group8.FinalsFrenzy
{
    public class CameraShake : MonoBehaviour
    {
        private Coroutine _shakeCoroutine;

        //
        public void TriggerShake(float duration, float magnitude)
        {
            if (_shakeCoroutine != null)
                StopCoroutine(_shakeCoroutine);

            _shakeCoroutine = StartCoroutine(Shake(duration, magnitude));
        }

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