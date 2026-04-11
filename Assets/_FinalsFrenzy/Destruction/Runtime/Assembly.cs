using System.Collections.Generic;
using UnityEngine;

namespace Group8.FinalsFrenzy.Destruction
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
        public float Mass => Rigidbody.mass;

        /// <summary>
        /// The part automatically chosen to represent the assembly's root part.
        /// </summary>
        public Part RootPart { get; private set; }
        #endregion

        private void Awake() => Rigidbody = GetComponent<Rigidbody>();

        private void OnEnable() => RebuildAssembly();

        /// <summary>
        /// Selects a part to be the root of the assembly.
        /// If a kinematic part is found, it is selected.
        /// Otherwise, the heaviest part is selected.
        /// </summary>
        /// <param name="assembly">The assembly of parts to select the root from.</param>
        /// <returns></returns>
        public Part SelectRoot(List<Part> assembly)
        {
            Part bestPart = null;
            var bestMass = 0f;

            foreach (var part in assembly)
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

        public void RebuildAssembly()
        {
            
        }
    }
}
