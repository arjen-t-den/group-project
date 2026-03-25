using UnityEngine;
using UnityEngine.InputSystem;

namespace Group8.FinalsFrenzy
{
    public class TestParticles : MonoBehaviour
    {
        private ParticleSystem explosion;
        public GameObject Camera;
        public GameObject firePrefab;
        private GameObject player;
        public CameraShake cameraShake;

        void Start()
        {
            explosion = GameObject.Find("ExplosionParticles").GetComponent<ParticleSystem>();
            player = GameObject.Find("Body");
        }

        void Update()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                SpawnExplosion(player.transform.position);
                StartCoroutine(cameraShake.Shake(1f, 1f));
            }
                

            if (Keyboard.current.fKey.wasPressedThisFrame)
            {
                SpawnFire(player.transform.position);
            }

        }
        void SpawnFire(Vector3 position)
        {
            GameObject instance = Instantiate(firePrefab, position, Quaternion.identity);
            ParticleSystem system = instance.GetComponent<ParticleSystem>();
            system.Play();
            // Destroy(instance, 3f);
        }
        // this method takes the player position, and by adding the camera.transform.forward, can spawn the explosion in front of the camera. The only downside is that it is a fixed spawn so it spawns "through wall". There are three ways to fix this, one is to literally giving it the spawn position, rendering it on another layer like how the guns in cs are rendered, or just do a line check. 
        void SpawnExplosion(Vector3 position)
        {
            float distance = 2f;
            // position: player position
            // camera.transform.forward * distance: spawning in front of the camera
            // Vector3: height adjustment
            Vector3 spawnPos = position + Camera.transform.forward * distance + new Vector3(0f, 1.6f, 0f);
            explosion.transform.position = spawnPos;
            explosion.Play();
        }
    }
}