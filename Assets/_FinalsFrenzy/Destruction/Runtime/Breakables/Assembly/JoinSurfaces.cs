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
        private Collider[] _touchingColliders;

        private void Awake()
        {
            _part = GetComponent<Part>();

            var colliders = Physics.OverlapBox(transform.position, transform.localScale / 2f, transform.rotation);
            foreach (var collider in colliders)
            {
                if (!collider.TryGetComponent<Part>(out var part)) continue;
                _touchingParts.Add(part);
            }

            foreach (var neighbor in _touchingParts)
                new Weld(_part, neighbor);
        }

        private Collider[] GetTouchingParts(int maxTouchingParts = 32)
        {
            _touchingColliders = new Collider[maxTouchingParts];
            var count = Physics.OverlapBoxNonAlloc(transform.position, transform.localScale / 2f, _touchingColliders, transform.rotation);
            if (count < _touchingColliders.Length) return _touchingColliders;
            return GetTouchingParts(maxTouchingParts * 2);
        }
    }
}
