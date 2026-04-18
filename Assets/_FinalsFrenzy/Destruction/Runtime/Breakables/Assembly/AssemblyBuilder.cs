using System.Collections.Generic;
using UnityEngine;

namespace Group8.FinalsFrenzy.Destruction.Breakables.Assembly
{
    public static class AssemblyBuilder
    {
        /// <summary>
        /// Builds assemblies from a list of parts, grouping them by their weld connections.
        /// </summary>
        /// <param name="parts">The list of parts to include in the assembly.</param>
        public static void Build(List<Part> parts)
        {
            var visitedParts = new HashSet<Part>();

            foreach (var part in parts)
            {
                if (visitedParts.Contains(part)) continue;
                var connected = GroupFromPart(part, visitedParts);
                CreateAssembly(connected);
            }
        }

        /// <summary>
        /// Groups parts connected to the start part through welds.
        /// </summary>
        /// <param name="start">An arbitrary part in the group to traverse from.</param>
        /// <param name="visited">Parts to avoid that have already been visited.</param>
        /// <returns>A list of parts connected to the start part through welds.</returns>
        private static List<Part> GroupFromPart(Part start, HashSet<Part> visited)
        {
            var group = new List<Part>();
            var stack = new Stack<Part>();
            stack.Push(start);

            while (stack.Count > 0)
            {
                var part = stack.Pop();
                if (!visited.Add(part)) continue;
                group.Add(part);

                foreach (var weld in part.Welds)
                {
                    var other = weld.Part0 == part ? weld.Part1 : weld.Part0;
                    if (other) stack.Push(other);
                }
            }

            return group;
        }

        /// <summary>
        /// Creates an assembly from a group of parts.
        /// </summary>
        /// <param name="parts">The list of parts to include in the assembly.</param>
        /// <returns>The created assembly.</returns>
        public static Assembly CreateAssembly(List<Part> parts)
        {
            var gameObject = new GameObject("Assembly");
            var assembly = gameObject.AddComponent<Assembly>();
            assembly.Initialize(parts);
            return assembly;
        }

        private static readonly HashSet<Assembly> _rebuilding = new();

        /// <summary>
        /// Rebuilds an assembly from its parts, creating new assemblies for any disconnected groups.
        /// </summary>
        /// <param name="assembly">The assembly to rebuild.</param>
        public static void Rebuild(Assembly assembly)
        {
            if (!_rebuilding.Add(assembly)) return;

            var parts = assembly.Parts;
            
            foreach (var part in parts)
                part.Assembly = null;

            var linearVelocity = assembly.LinearVelocity;
            var angularVelocity = assembly.AngularVelocity;

            var groups = FindDisconnectedGroups(parts);

            foreach (var group in groups)
            {
                var newAssembly = CreateAssembly(group);

                newAssembly.LinearVelocity = linearVelocity;
                newAssembly.AngularVelocity = angularVelocity;
            }

            _rebuilding.Remove(assembly);

            Object.Destroy(assembly.gameObject);
        }

        /// <summary>
        /// Finds groups of parts still connected by welds.
        /// </summary>
        /// <remarks>
        /// These will become assemblies.
        /// </remarks>
        /// <param name="parts">The list of parts to check.</param>
        /// <returns>A list of groups of parts.</returns>
        private static List<List<Part>> FindDisconnectedGroups(List<Part> parts)
        {
            var groups = new List<List<Part>>();
            var visitedParts = new HashSet<Part>();

            foreach (var part in parts)
            {
                if (visitedParts.Contains(part)) continue;
                var group = GroupFromPart(part, visitedParts);
                groups.Add(group);
            }

            return groups;
        }
    }
}
