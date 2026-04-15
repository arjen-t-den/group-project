using UnityEngine;

namespace Group8.FinalsFrenzy.Weapons
{
    /// <summary>
    /// Interface for weapons that can be used by the player.
    /// </summary>
    public interface IWeapon
    {
        public void Attack(Vector3 origin, Vector3 direction);
    }
}
