using System.Collections.Generic;
using UnityEngine;

namespace Group8.FinalsFrenzy.Destruction.Breakables.Assembly
{
    /// <summary>
    /// Helper methods for rebuilding assembles.
    /// </summary>
    public static class Assemblies
    {
        /// <summary>
        /// Groups parts connected to the start part through welds.
        /// </summary>
        /// <param name="start">An arbitrary part in the group to traverse from.</param>
        /// <param name="visited">Parts to avoid that have already been visited.</param>
        /// <returns>A list of parts connected to the start part through welds.</returns>
        private static List<Part> GetGroupFromPart(Part start, HashSet<Part> visited)
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
        public static Assembly CreateAssembly(List<Part> parts, string name)
        {
            var gameObject = new GameObject(name + " (Broken)");
            var assembly = gameObject.AddComponent<Assembly>();
            assembly.Initialize(parts, name);
            return assembly;
        }

        private static void DestroyAssembly(Assembly assembly)
        {
            _rebuildingAssemblies.Remove(assembly);
            Object.Destroy(assembly.gameObject);
        }

        private static readonly HashSet<Assembly> _rebuildingAssemblies = new();

        /// <summary>
        /// Rebuilds an assembly from its parts, creating new assemblies for any disconnected groups.
        /// </summary>
        /// <param name="assembly">The assembly to rebuild.</param>
        public static void Rebuild(Assembly assembly)
        {
            if (!_rebuildingAssemblies.Add(assembly)) return;

            if (!assembly.HasRootPart())
            {
                if (assembly.IsEmpty())
                {
                    DestroyAssembly(assembly);
                    return;
                }

                assembly.SelectRootPart();
            }

            var rootPart = assembly.RootPart;
            var parts = assembly.Parts;

            var linearVelocity = assembly.LinearVelocity;
            var angularVelocity = assembly.AngularVelocity;

            var groups = FindDisconnectedGroups(parts, rootPart);

            // Do not rebuild if assembly is intact.
            if (groups.Count == 0)
            {
                _rebuildingAssemblies.Remove(assembly);
                return;
            }

            // Remove disconnected parts from the assembly.
            foreach (var group in groups)
                foreach (var part in group)
                    assembly.RemovePart(part);

            assembly.RecalculateMass();

            // Create new assemblies for each group.
            foreach (var group in groups)
            {
                var newAssembly = CreateAssembly(group, assembly.Name);

                newAssembly.LinearVelocity = linearVelocity;
                newAssembly.AngularVelocity = angularVelocity;
            }

            _rebuildingAssemblies.Remove(assembly);
        }

        /// <summary>
        /// Finds groups of parts still connected by welds.
        /// </summary>
        /// <remarks>
        /// These will become assemblies.
        /// </remarks>
        /// <param name="parts">The list of parts to check.</param>
        /// <returns>A list of groups of parts.</returns>
        private static List<List<Part>> FindDisconnectedGroups(List<Part> parts, Part rootPart)
        {
            var groups = new List<List<Part>>();
            var visitedParts = new HashSet<Part>();

            GetGroupFromPart(rootPart, visitedParts);

            foreach (var part in parts)
            {
                if (visitedParts.Contains(part)) continue;
                var group = GetGroupFromPart(part, visitedParts);
                groups.Add(group);
            }

            return groups;
        }
    }
}
