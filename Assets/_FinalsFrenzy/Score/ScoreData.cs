using UnityEngine;

namespace Group8.FinalsFrenzy
{
    [CreateAssetMenu(fileName = "ScoreData", menuName = "Scriptable Objects/ScoreData")]
    public class ScoreData : ScriptableObject
    {
        public int pointMultiplier = 1;
        public string objectType = "misc";
        
    }
}
