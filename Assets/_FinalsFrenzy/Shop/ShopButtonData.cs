using UnityEngine;

namespace Group8.FinalsFrenzy.Shop
{
    [CreateAssetMenu(fileName = "ShopButtonData", menuName = "Scriptable Objects/ShopButtonData")]
    public class ShopButtonData : ScriptableObject
    {
        public int cost = 0;
        
        public bool bought = false;
    }
}
