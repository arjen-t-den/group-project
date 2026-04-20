using UnityEngine;

namespace Group8.FinalsFrenzy.Destruction.Breakables
{
    /// <summary>
    /// A breakable object that forwards its break to its parent.
    /// </summary>
    public class ChildBreakable : Breakable
    {
        private IBreakable _parentBreakable;

        private void Awake() => _parentBreakable = transform.parent.GetComponentInParent<IBreakable>();

        /// <inheritdoc/>
        /// <summary>
        /// Breaks the parent.
        /// </summary>
        public override void Break(Vector3 point, Vector3 direction)
        {
            base.Break(point, direction);
            _parentBreakable?.Break(point, direction);
        }
    }
}
