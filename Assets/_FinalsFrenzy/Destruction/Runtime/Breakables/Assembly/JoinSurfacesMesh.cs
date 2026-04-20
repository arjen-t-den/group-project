using System.Collections.Generic;
using Group8.FinalsFrenzy.Destruction.Breakables.Assembly;
using UnityEngine;

namespace Group8.FinalsFrenzy.Destruction
{
    /// <summary>
    /// Connects nearby parts on start.
    /// </summary>
    [RequireComponent(typeof(Part))]
    public class JoinSurfacesMesh : MonoBehaviour
    {
        private Part _part;
        private readonly HashSet<Part> _touchingParts = new();
        private Collider _collider;

        private void Awake()
        {
            _part = GetComponent<Part>();
            _collider = GetComponent<Collider>();
            GetTouchingParts();

            foreach (var neighbor in _touchingParts)
                if (_part.GetInstanceID() > neighbor.GetInstanceID())
                    new Weld(_part, neighbor);
        }

        private void GetTouchingParts()
        {
            var bounds = _collider.bounds;
            var colliders = Physics.OverlapBox(bounds.center, bounds.extents + Vector3.one * 0.01f);

            foreach (var collider in colliders)
            {
                var point1 = _collider.ClosestPoint(collider.transform.position);
                var point2 = collider.ClosestPoint(_collider.transform.position);

                if (Vector3.SqrMagnitude(point1 - point2) > 0.005f)
                    continue;

                if (!collider.TryGetComponent<Part>(out var part)) continue;
                _touchingParts.Add(part);
            }
        }
    }
}
