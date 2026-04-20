using UnityEngine;

namespace Group8.FinalsFrenzy.Player
{
    /// <summary>
    /// An interface for objects that can be grabbed by the player.
    /// </summary>
    public interface IGrabbable
    {
        Rigidbody Rigidbody { get; }

        /// <summary>
        /// Grab the object.
        /// </summary>
        public void Grab();

        /// <summary>
        /// Release the object.
        /// </summary>
        public void Release();
    }
}
