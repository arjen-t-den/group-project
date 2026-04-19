using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class MouseSensitivity : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public CharacterController player_controller;

        public float mouse_speed = 12f;

        void Update()
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * y;

            player_controller.Move(move * mouse_speed * Time.deltaTime);
        }
    }
