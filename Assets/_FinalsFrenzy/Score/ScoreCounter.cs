using UnityEngine;
using Group8.FinalsFrenzy.Destruction.Breakables;
using System.Diagnostics;
using System.Linq;
using TMPro;

namespace Group8.FinalsFrenzy.Score
{
    /// <summary>
    /// Singleton class to keep track of the players score in a scene. 
    /// </summary>
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _scoreText;

        public static ScoreCounter Instance {get; private set;}
        /// <summary>
        /// long which stores the score value.
        /// </summary>
        public long score {get; private set;}
        /// <summary>
        /// possible score values.
        /// </summary>
        private int[] scoreValues = {5,10,15,25,50,75,100};
        /// <summary>
        /// stores the last object destroyed
        /// </summary>
        private string lastdestroyedObject = "";
        /// <summary>
        /// stores the 4 objects destroyed before the most recently desroyed object.
        /// </summary>
        private string[] destroyedObjects = new string[4];
        /// <summary>
        /// index that points to the oldest object label in destroyedObjects. 
        /// </summary>
        private int desObjIndex = 0;

        private void Awake() {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                UnityEngine.Debug.Log("Duplicate deleted");
            } else {
                Instance = this;
                //UnityEngine.Debug.Log("Singleton made");
            }
            if (PlayerPrefs.GetInt("Score") == 0)
            {
                PlayerPrefs.SetInt("Score", 0);
            }
            score = PlayerPrefs.GetInt("Score");
        } 

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void updateDesObj(string label)
        {
            destroyedObjects[desObjIndex] = lastdestroyedObject;
            lastdestroyedObject = label;
            if (desObjIndex >= destroyedObjects.Length-1)
            {
                desObjIndex = 0;
                return;
            }
            desObjIndex++;
        }

        /// <summary>
        /// picks a number from the scoreValues array by generatinga random index.
        /// </summary>
        /// <returns>points as an int.</returns>
        public int generatePoints()
        {
            int index = Random.Range(0, scoreValues.Length);
            return scoreValues[index];
        }

        /// <summary>
        /// adds a new value to the score.
        /// </summary>
        public void addScore(int multiplier){
            int points = generatePoints();
            if (destroyedObjects.Contains(lastdestroyedObject))
            {
                score += points;
            } else
            {
                score += points * multiplier;
            }
            PlayerPrefs.SetInt("Score", (int)score);
            UnityEngine.Debug.Log("Score: " + score);
            _scoreText.text = "Score: " + score.ToString();
        }

        public void subScore(int value)
        {
            score -= value;
            UnityEngine.Debug.Log("Score: " + score);
        }

    }
}
