using System.Collections.Generic;
using UnityEngine;

namespace Group8.FinalsFrenzy.Destruction.Breakables.Assembly
{
    public class Model : Breakable
    {
        private readonly List<Part> _parts;
        private Model _brokenModel;

        private void Awake()
        {
            _brokenModel = transform.GetComponentInChildren<Model>();

            // Add all child parts (not including grandchildren)
            foreach (Transform transform in transform)
            {
                if (!transform.TryGetComponent<Part>(out var part)) continue;
                _parts.Add(part);
            }
        }

        public override void Break(Vector3 point, Vector3 direction)
        {
            base.Break(point, direction);

            if (_brokenModel)
                _brokenModel.Break(point, direction);
            else
                FractureAtPoint(point, direction);

        }

        private void FractureAtPoint(Vector3 point, Vector3 direction)
        {
            
        }
    }
}
