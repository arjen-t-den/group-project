using System.Collections;
using UnityEngine;

namespace Group8.FinalsFrenzy
{
    public class CameraShake : MonoBehaviour
    {
        public IEnumerator Shake(float duration, float magnitude)
        {
            float elapsed = 0.0f;
            
            while (elapsed < duration)
            {
                float z = Random.Range(-1f, 1f) * magnitude;

                Vector3 currentRot = transform.localEulerAngles;
                transform.localRotation = Quaternion.Euler(currentRot.x, currentRot.y, z);

                elapsed += Time.deltaTime;
                if (magnitude > 0)
                {
                    magnitude = magnitude * 0.99f;
                }
                yield return null;
            }

            Vector3 finalRot = transform.localEulerAngles;
            transform.localRotation = Quaternion.Euler(finalRot.x, finalRot.y, 0f);
        }
    }
}