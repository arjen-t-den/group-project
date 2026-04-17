using Group8.FinalsFrenzy.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Group8.FinalsFrenzy
{
    public class Footsteps : MonoBehaviour
    {
        private GameObject player;
        private FirstPersonMovement _movement;
        public AudioSource footstepSounds, sprintSounds;

        void Start()
        {
            player = GameObject.Find("FootBall");

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
        }
    }
}
