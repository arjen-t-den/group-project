using Group8.FinalsFrenzy.Player;
using System;
using UnityEngine;
using UnityEngine.InputSystem;


// this is a test script to test out the particle system and camera shake, not meant to be used in the final game
namespace Group8.FinalsFrenzy
{
    public class TestParticles : MonoBehaviour
    {
        private ParticleSystem explosion;
        private GameObject player;
        private FirstPersonMovement _movement;
        private AudioManager audioManager;
        private ParticlesManager particlesManager;

        public GameObject Camera; 
        public GameObject firePrefab;
        public CameraShake cameraShake;
        public AudioSource footstepSounds, sprintSounds;
        
        public float magnitude = 1f;
        public float duration = 1f;

        void Start()
        {
            explosion = GameObject.Find("ParticleEffects/ExplosionParticles").GetComponent<ParticleSystem>();
            player = GameObject.Find("FootBall");
            audioManager = FindFirstObjectByType<AudioManager>();
            particlesManager = FindFirstObjectByType<ParticlesManager>();

            if (player == null)
            {
                Debug.LogError("Could not find 'Body' in the scene!", this);
                return;
            }

            _movement = player.GetComponent<FirstPersonMovement>();

            if (_movement == null)
            {
                Debug.LogError("FirstPersonMovement not found on Body!", this);
            }
            else
            {
                Debug.Log("FirstPersonMovement found on Body.", this);
            }
        }

        void Update()
        {
            // spawns explosion and triggers camera shake when left mouse button is pressed, explosion spawns in front of the camera with height adjustment
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                particlesManager.PlayEffect("ExplosionParticles", player.transform.position, 3f);
                audioManager.Play("ExplosionImpact");
                cameraShake.TriggerShake(duration,magnitude);
            }

            // footstep sound logic, plays when the character is moving/running, stops when the character is not moving
            if (Keyboard.current.wKey.isPressed || Keyboard.current.aKey.isPressed || Keyboard.current.sKey.isPressed || Keyboard.current.dKey.isPressed)
            {
                footstepSounds.enabled = true;
                if (_movement.IsRunning && (Keyboard.current.wKey.isPressed || Keyboard.current.aKey.isPressed || Keyboard.current.sKey.isPressed || Keyboard.current.dKey.isPressed))
                {
                    footstepSounds.enabled = false;
                    sprintSounds.enabled = true;
                }
                else
                {
                    sprintSounds.enabled = false;
                }
            }
            else
            {
                footstepSounds.enabled = false;
                sprintSounds.enabled = false;
            }

            // spawns fire particles at the player's position when the F key is pressed
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
        // Spawns explosion in front of the camera with height adjustment
        // replaced with the particlemanager's function
        void SpawnExplosion(Vector3 position)
        {
            float distance = 2f;
            // position: player position
            // camera.transform.forward * distance: spawning in front of the camera
            // Vector3: height adjustment
            Vector3 spawnPos = position + Camera.transform.forward * distance + new Vector3(0f, 1.6f, 0f);
            audioManager.Play("ExplosionImpact");
            
            explosion.transform.position = spawnPos;
            explosion.Play();
        }
    }
}