using System.Diagnostics;
using System.Runtime.Serialization;
using UnityEngine;
using Group8.FinalsFrenzy.Score;
using Group8.FinalsFrenzy.Button;


namespace Group8.FinalsFrenzy.Shop
{
    [RequireComponent(typeof(Pressable))]
    [DisallowMultipleComponent]
    public class shopButton : MonoBehaviour
    {
        [SerializeField] private ShopButtonData shopButtonData;

        private Pressable button;
        void Awake()
        {
            button = GetComponent<Pressable>();
        }
        void buy()
        {
            if (ScoreCounter.Instance.score >= shopButtonData.cost)
            {
                ScoreCounter.Instance.subScore(shopButtonData.cost);
                shopButtonData.bought = true;
                UnityEngine.Debug.Log("not poor");
            }
            else{
                UnityEngine.Debug.Log("Pooor!!");
            }
        }

        void equip()
        {
             UnityEngine.Debug.Log("weapon equiped");
        }

        void buttonPressed()
        {
            if (shopButtonData.bought)
            {
                equip();
            }
            else
            {
                buy();
            }
        }
        public void OnEnable() => button.OnPress += buttonPressed;
        public void OnDisable() => button.OnPress -= buttonPressed;
    }
}
