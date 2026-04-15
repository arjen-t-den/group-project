using UnityEngine;

namespace Group8.FinalsFrenzy.Weapons
{
    /// <summary>
    /// Base scriptable object for weapons to inherit from.
    /// </summary>
    public abstract class Weapon : ScriptableObject, IWeapon
    {
        public abstract string itemLabel {get;}
        public virtual void Attack(Vector3 origin, Vector3 direction) { }
    }
}
