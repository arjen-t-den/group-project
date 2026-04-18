using UnityEngine;

namespace Group8.FinalsFrenzy.Destruction.Breakables.Assembly
{
    /// <summary>
    /// Holds 2 parts together in a relative position.
    /// </summary>
    public class Weld
    {
        /// <summary>
        /// The first <see cref="Part"/> that the weld connects.
        /// </summary>
        public Part Part0 { get; private set; }

        /// <summary>
        /// The second <see cref="Part"/> that the weld connects.
        /// </summary>
        public Part Part1 { get; private set; }

        /// <summary>
        /// Creates a new <see cref="Weld"/> between <paramref name="part0"/> and <paramref name="part1"/>.
        /// </summary>
        /// <param name="part0">The first <see cref="Part"/> that the weld connects.</param>
        /// <param name="part1">The second <see cref="Part"/> that the weld connects.</param>
        public Weld(Part part0, Part part1)
        {
            Part0 = part0;
            Part1 = part1;

            Part0.Welds.Add(this);
            Part1.Welds.Add(this);
        }

        /// <summary>
        /// Breaks the weld and removes references for garbage collection.
        /// </summary>
        public void Break()
        {
            var assembly = Part0.Assembly ?? Part1.Assembly;

            if (Part0) Part0.Welds.Remove(this);
            if (Part1) Part1.Welds.Remove(this);

            Part0 = null;
            Part1 = null;
        }
    }
}
