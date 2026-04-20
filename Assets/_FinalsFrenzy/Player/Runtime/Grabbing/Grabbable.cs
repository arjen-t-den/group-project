using System;
using UnityEngine;

namespace Group8.FinalsFrenzy.Player
{
    /// <summary>
    /// Base class for grabbables to inherit from.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class Grabbable : MonoBehaviour, IGrabbable
    {
        public event Action OnGrab;
        public event Action OnRelease;

        private Collider _collder;

        public Rigidbody Rigidbody => _collder.attachedRigidbody;

        private void Awake() => _collder = GetComponent<Collider>();

        public virtual void Grab()
        {
            var rigidbody = _collder.attachedRigidbody;
        }

        public virtual void Release()
        {
            
        }
    }
}
