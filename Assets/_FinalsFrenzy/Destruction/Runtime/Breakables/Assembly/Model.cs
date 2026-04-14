using System.Collections.Generic;
using UnityEngine;

namespace Group8.FinalsFrenzy.Destruction.Breakables.Assembly
{
    public class Model : Breakable
    {
        private readonly List<Part> _parts;
        private Model _childModel;

        private void Awake()
        {
            _childModel = transform.GetComponentInChildren<Model>();

            // Add all child parts (not including grandchildren)
            foreach (Transform transform in transform)
            {
                if (!transform.TryGetComponent<Part>(out var part)) continue;
                _parts.Add(part);
            }
        }

        public override void BreakAtPoint(Vector3 point)
        {
            base.BreakAtPoint(point);

            if (_childModel)
                _childModel.BreakAtPoint(point);
            else
                FractureAtPoint(point);

        }

        private void FractureAtPoint(Vector3 point)
        {

        }
    }
}
