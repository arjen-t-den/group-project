using System;
using UnityEngine;

namespace Group8.FinalsFrenzy.Player
{
    /// <summary>
    /// Handles checking if the player is grounded.
    /// Applies counter-impulses to prevent the player from sliding down standable slopes.
    /// </summary>
    public class Grounding : MonoBehaviour
    {
        [SerializeField]
        [Range(0f, 89f)]
        [Tooltip("The maximum slope angle (in degrees) that can be considered ground.")]
        private float _maxSlopeAngle = 50f;

        private float _minSlopeDot;
        private readonly float _minFriction = 0.1f;

        private Rigidbody _rigidbody;

        /// <summary>
        /// The maximum slope angle (in degrees) that can be considered ground.
        /// </summary>
        public float MaxSlopeAngle
        {
            get => _maxSlopeAngle;
            set
            {
                _maxSlopeAngle = Mathf.Clamp(value, 0f, 89f);
                _minSlopeDot = Mathf.Cos((_maxSlopeAngle + 0.001f) * Mathf.Deg2Rad);
            }
        }

        /// <summary>
        /// Is the object currently grounded?
        /// </summary>
        public bool IsGrounded { get; private set; }

        /// <summary>
        /// The normal of the ground surface the object is currently standing on. Will be zero if not grounded.
        /// </summary>
        public Vector3 GroundNormal { get; private set; }

        /// <summary>
        /// Is the player slipping on a surface that is too steep or has too little friction to stand on?
        /// </summary>
        public bool IsSlipping { get; private set; }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            MaxSlopeAngle = _maxSlopeAngle;
        }

        private void FixedUpdate()
        {
            if (!enabled) return;

            IsGrounded = false;
            GroundNormal = Vector3.zero;

            IsSlipping = false;
        }

        private void OnCollisionStay(Collision collision)
        {
            if (!enabled || IsGrounded) return;

            IsSlipping = collision.collider.material.staticFriction < _minFriction;
            if (IsSlipping) return;

            var gravity = Physics.gravity;
            if (gravity.sqrMagnitude == 0f) return;

            var upDirection = -gravity.normalized;
            var otherBody = collision.body;

            for (int i = 0; i < collision.contactCount; i++)
            {
                var contactPoint = collision.GetContact(i);
                var groundNormal = contactPoint.normal;
                var slopeDot = Vector3.Dot(groundNormal, upDirection);

                IsSlipping = slopeDot < _minSlopeDot;
                if (IsSlipping) continue;

                IsGrounded = true;
                GroundNormal = groundNormal;

                Vector3 alongPlaneVector = Vector3.Cross(groundNormal, upDirection);
                Vector3 upPlaneVector = Vector3.Cross(alongPlaneVector, groundNormal);

                var impulse = contactPoint.impulse;
                var counterImpulse = impulse.magnitude / slopeDot * upPlaneVector;

                _rigidbody.AddForce(counterImpulse, ForceMode.Impulse);
                AddForceAtPosition(otherBody, -counterImpulse, contactPoint.point, ForceMode.Impulse);

                return;
            }
        }

        private void AddForceAtPosition(Component body, Vector3 force, Vector3 position, ForceMode mode)
        {
            switch (body)
            {
                case Rigidbody otherRigidbody:
                    otherRigidbody.AddForceAtPosition(force, position, mode);
                    break;
                case ArticulationBody otherArticulationBody:
                    otherArticulationBody.AddForceAtPosition(force, position, mode);
                    break;
            }
        }
    }
}
