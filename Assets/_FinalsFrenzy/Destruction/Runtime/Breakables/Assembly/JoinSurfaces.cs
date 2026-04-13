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
        private HashSet<Part> _neighbors = new();

        private void Awake()
        {
            _part = GetComponent<Part>();

            var colliders = Physics.OverlapBox(transform.position, transform.localScale / 2f, transform.rotation);
            foreach (var collider in colliders)
            {
                if (!collider.TryGetComponent<Part>(out var part)) continue;
                _neighbors.Add(part);
            }

            foreach (var neighbor in _neighbors)
            {
                var weld = new Weld(_part, neighbor);
                _part.Welds.Add(weld);
                neighbor.Welds.Add(weld);
            }
        }
    }
}
