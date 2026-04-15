using System.Collections.Generic;
using UnityEngine;

namespace Group8.FinalsFrenzy.Destruction.Breakables.Assembly
{
    /// <summary>
    /// Connects nearby parts on start.
    /// </summary>
    [RequireComponent(typeof(Part))]
    public class JoinSurfaces : MonoBehaviour
    {
        private Part _part;
        private readonly HashSet<Part> _touchingParts = new();
        private Collider[] _touchingColliders = new Collider[32];

        private void Awake()
        {
            _part = GetComponent<Part>();

            var count = GetTouchingParts(out var colliders);
            for (int i = 0; i < count; i++)
            {
                var collider = colliders[i];
                if (!collider.TryGetComponent<Part>(out var part)) continue;
                _touchingParts.Add(part);
            }

            foreach (var neighbor in _touchingParts)
                if (_part.GetInstanceID() > neighbor.GetInstanceID())
                    new Weld(_part, neighbor);
        }

        int GetTouchingParts(out Collider[] results, int maxTouchingParts = 4)
        {
            if (_touchingColliders.Length < maxTouchingParts)
                _touchingColliders = new Collider[maxTouchingParts];

            var count = Physics.OverlapBoxNonAlloc(
                transform.position,
                transform.localScale / 2f + 0.01f * Vector3.one,
                _touchingColliders,
                transform.rotation
            );

            if (count == _touchingColliders.Length)
                return GetTouchingParts(out results, maxTouchingParts * 2);

            results = _touchingColliders;
            return count;
        }
    }
}
