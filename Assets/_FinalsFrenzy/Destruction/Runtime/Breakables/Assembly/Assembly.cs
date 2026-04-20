using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Group8.FinalsFrenzy.Destruction.Breakables.Assembly
{
    /// <summary>
    /// Represents a collection of parts that move together as one rigid body.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class Assembly : MonoBehaviour
    {
        #region Properties
        /// <summary>
        /// The rigidbody component that represents the assembly's physics body.
        /// </summary>
        public Rigidbody Rigidbody { get; private set; }

        /// <summary>
        /// The linear velocity vector of the assembly.
        /// </summary>
        public Vector3 LinearVelocity
        {
            get => Rigidbody.linearVelocity;
            set => Rigidbody.linearVelocity = value;
        }

        /// <summary>
        /// The angular velocity vector of the assembly.
        /// </summary>
        public Vector3 AngularVelocity
        {
            get => Rigidbody.angularVelocity;
            set => Rigidbody.angularVelocity = value;
        }

        /// <summary>
        /// The center of mass of the assembly in world space.
        /// </summary>
        public Vector3 CenterOfMass => Rigidbody.worldCenterOfMass;

        /// <summary>
        /// The mass of the assembly.
        /// </summary>
        public float Mass
        {
            get => Rigidbody.mass;
            set => Rigidbody.mass = value;
        }

        /// <summary>
        /// The part automatically chosen to represent the assembly's root part.
        /// </summary>
        public Part RootPart { get; private set; }

        public List<Part> Parts { get; private set; } = new();
        #endregion

        private void Awake() => Rigidbody = GetComponent<Rigidbody>();

        private void Start()
        {
            if (Parts.Count > 0) return;
            var parts = FindObjectsByType<Part>(FindObjectsSortMode.None).ToList();
            Initialize(parts);
        }

        /// <returns>True if the assembly has a valid root part.</returns>
        public bool HasRootPart() => RootPart && Parts.Contains(RootPart);

        /// <returns>True if the assembly has no parts.</returns>
        public bool IsEmpty() => Parts.Count == 0;

        /// <summary>
        /// Selects a part to be the root of the assembly.
        /// </summary>
        /// <remarks>
        /// If a kinematic part is found, it is selected.
        /// Otherwise, the heaviest part is selected.
        /// </remarks>
        public void SelectRootPart()
        {
            if (HasRootPart()) return;

            Part bestPart = null;
            var bestMass = 0f;

            foreach (var part in Parts)
            {
                if (part.IsKinematic)
                {
                    bestPart = part;
                    break;
                }

                var mass = part.Mass;
                if (mass > bestMass)
                {
                    bestMass = mass;
                    bestPart = part;
                }
            }

            RootPart = bestPart;
            RootPart.GetComponent<MeshRenderer>().material.color = Color.red;
        }

        public void Initialize(List<Part> parts)
        {
            foreach (var part in parts)
                AddPart(part);

            RecalculateMass();
            SelectRootPart();
        }

        public void RecalculateMass()
        {
            Mass = 0f;

            foreach (var part in Parts)
                Mass += part.Mass;

            Rigidbody.mass = Mass;
        }

        private void AddPart(Part part)
        {
            Parts.Add(part);
            part.Assembly = this;
            part.transform.SetParent(transform);
        }

        public void RemovePart(Part part)
        {
            Parts.Remove(part);
            part.Assembly = null;
        }
    }
}
