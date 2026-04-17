using System.Collections.Generic;
using UnityEngine;

namespace Group8.FinalsFrenzy.Destruction.Breakables.Assembly
{
    public class Part : Breakable
    {
        public float Mass = 1f;
        public bool IsKinematic;
        public Assembly Assembly;
        public List<Weld> Welds = new();

        private void Awake() => Assembly = GetComponentInParent<Assembly>();

        public List<Part> GetAssembly(Part start)
        {
            var visited = new HashSet<Part>();
            var stack = new Stack<Part>();

            stack.Push(start);

            while (stack.Count > 0)
            {
                var part = stack.Pop();

                if (!visited.Add(part)) continue;

                foreach (var weld in part.Welds)
                {
                    var other = weld.Part0 == part ? weld.Part1 : weld.Part0;
                    stack.Push(other);
                }
            }

            return new List<Part>(visited);
        }

        public override void Break(Vector3 point, Vector3 direction)
        {
            base.Break(point, direction);

            foreach (var weld in Welds.ToArray())
                weld.Break();

            print("Broken welds for " + name);
            Destroy(gameObject);
        }
    }
}
