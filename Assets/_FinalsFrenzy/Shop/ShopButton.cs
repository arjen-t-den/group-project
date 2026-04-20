using System.Diagnostics;
using System.Runtime.Serialization;
using UnityEngine;
using Group8.FinalsFrenzy.Score;
using Group8.FinalsFrenzy.Button;
using Group8.FinalsFrenzy.Weapons;
using TMPro;


namespace Group8.FinalsFrenzy.Shop
{
    [RequireComponent(typeof(Pressable))]
    [DisallowMultipleComponent]
    public class shopButton : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _weaponText;

        [SerializeField] private ShopButtonData shopButtonData;
        [SerializeField] private Weapon weapon;
        
        private string weaponName;
        private Pressable button;
        void Awake()
        {
            button = GetComponent<Pressable>();
            if (weapon.itemLabel != null)
            {
                weaponName = weapon.itemLabel;
                
            } else {
                weaponName = "unnamed";
            }

            
        }
        void Start()
        {
            if (!(PlayerPrefs.GetString(weaponName) == string.Empty))
            {
                UnityEngine.Debug.Log(" already bought");
                InventoryManager.Instance.rememberWeapon(weaponName);
            }
        }
        void buy()
        {
            if (ScoreCounter.Instance.score >= shopButtonData.cost)
            {
                ScoreCounter.Instance.subScore(shopButtonData.cost);
                InventoryManager.Instance.addWeapon(weaponName);
                UnityEngine.Debug.Log("not poor");
                equip();
            }
            else{
                UnityEngine.Debug.Log("Pooor!!");
            }
        }

        void equip()
        {
            WeaponController.Instance.Weapon = weapon;
             UnityEngine.Debug.Log("weapon equiped");
            _weaponText.text = weapon.name;
        }

        void buttonPressed()
        {
            if (InventoryManager.Instance.isWeaponOwned(weaponName))
            {
                equip();
            }
            else
            {
                buy();
            }
            PlayerPrefs.Save();
        }
        public void OnEnable() => button.OnPress += buttonPressed;
        public void OnDisable() => button.OnPress -= buttonPressed;
    }
}
