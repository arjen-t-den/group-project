using UnityEngine;

namespace Group8.FinalsFrenzy.Destruction.Breakables.Assembly
{
    public class AssemblyBreakable : Breakable
    {
        private GameObject _intact;
        private GameObject _broken;

        private void Awake()
        {
            _intact = gameObject;
            _broken = transform.parent.Find("Broken").gameObject;
        }

        public override void Break(Vector3 point, Vector3 direction)
        {
            base.Break(point, direction);

            // Swap intact and broken models
            _intact.SetActive(false);
            _broken.SetActive(true);

            // Break sub-breakable if it exists
            var colliders = Physics.OverlapSphere(point, 0.001f);
            foreach (var collider in colliders)
            {
                if (!collider.TryGetComponent<IBreakable>(out var breakable))
                    continue;

                breakable.Break(point, direction);
                break;
            }
        }
    }
}
