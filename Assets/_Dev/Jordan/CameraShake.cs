using System.Collections;
using UnityEngine;

namespace Group8.FinalsFrenzy
{
    public class CameraShake : MonoBehaviour
    {
        public IEnumerator Shake(float duration, float magnitude)
        {
            Vector3 originalPos = new Vector3(0f, 1.6f, 0f);
            Debug.Log(originalPos);
            float elapsed = 0.0f;

            while (elapsed < duration)
            {
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = Random.Range(-1f, 1f) * magnitude;

                transform.localPosition = new Vector3(x, y + 1.6f, originalPos.z);
                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.localPosition = originalPos;
        }
    }
}