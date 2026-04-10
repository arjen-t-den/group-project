using System.Collections.Generic;
using UnityEngine;

namespace Group8.FinalsFrenzy.Destruction
{
    [RequireComponent(typeof(Rigidbody))]
    public class Assembly : MonoBehaviour
    {
        public Rigidbody Rigidbody;
        public Part RootPart;

        private void Awake() => Rigidbody = GetComponent<Rigidbody>();

        public Part SelectRoot(List<Part> assembly)
        {
            Part bestPart = null;
            var bestMass = -1f;

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
