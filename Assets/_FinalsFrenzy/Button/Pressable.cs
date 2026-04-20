using UnityEngine;
using System;

namespace Group8.FinalsFrenzy.Button
{
    public class Pressable : MonoBehaviour
    {
        public event Action OnPress;
        public virtual void Press() => OnPress?.Invoke();
    }
}