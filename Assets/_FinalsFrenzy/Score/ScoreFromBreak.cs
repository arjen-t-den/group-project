using UnityEngine;
using Group8.FinalsFrenzy.Destruction.Breakables;

namespace Group8.FinalsFrenzy.Score
{
    [RequireComponent(typeof(IBreakable))]
    [DisallowMultipleComponent]
    public class ScoreFromBreak : MonoBehaviour
    {
        [SerializeField] private ScoreData scoreData;
        private IBreakable breakable;

        void Awake()
        {
            breakable = GetComponent<IBreakable>();
            UnityEngine.Debug.Log(breakable != null);
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        /// <summary>
        /// calls the addscore method from ScoreCounter and passes the objects point multiplier as a parameter.
        /// </summary>
        public void updateScoreCounter()
        {
            ScoreCounter.Instance.addScore(scoreData.pointMultiplier);
        }

        private void OnEnable() => breakable.OnBreak += updateScoreCounter;

        private void OnDisable() => breakable.OnBreak -= updateScoreCounter;
    }
}
