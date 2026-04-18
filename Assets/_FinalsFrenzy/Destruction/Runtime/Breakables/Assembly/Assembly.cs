using System.Collections.Generic;
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
        public Vector3 LinearVelocity => Rigidbody.linearVelocity;

        /// <summary>
        /// The angular velocity vector of the assembly.
        /// </summary>
        public Vector3 AngularVelocity => Rigidbody.angularVelocity;

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

        public HashSet<Part> Parts { get; private set; } = new();
        #endregion

        private void Awake() => Rigidbody = GetComponent<Rigidbody>();

        private void OnEnable()
        {
            RebuildAssembly();
            RootPart = SelectRoot();
        }

        /// <summary>
        /// Selects a part to be the root of the assembly.
        /// </summary>
        /// <remarks>
        /// If a kinematic part is found, it is selected.
        /// Otherwise, the heaviest part is selected.
        /// </remarks>
        /// <returns>The selected root part.</returns>
        public Part SelectRoot()
        {
            Part bestPart = null;
            var bestMass = 0f;

            foreach (var part in Parts)
            {
                if (part.IsKinematic) return part;

                var mass = part.Mass;
                if (mass > bestMass)
                {
                    bestMass = mass;
                    bestPart = part;
                }
            }

            return bestPart;
        }

        /// <summary>
        /// Rebuilds the assembly and splits it into multiple assemblies if necessary.
        /// </summary>
        public void RebuildAssembly()
        {
            if (!RootPart) RootPart = SelectRoot();
            RecalculateParts();
            RecalculateMass();
        }

        private void RecalculateParts()
        {
            Parts.Clear();

            var stack = new Stack<Part>();
            stack.Push(RootPart);

            while (stack.Count > 0)
            {
                var part = stack.Pop();

                if (!Parts.Add(part)) continue;

                foreach (var weld in part.Welds)
                {
                    var other = weld.Part0 == part ? weld.Part1 : weld.Part0;
                    stack.Push(other);
                }
            }
        }

        private void RecalculateMass()
        {
            Mass = 0f;
            foreach (var part in Parts)
                Mass += part.Mass;
        }
    }
}
