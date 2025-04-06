using UnityEngine;

namespace Difficulty
{
    public class DifficultySelectionButton : MonoBehaviour
    {
        [SerializeField] private int startingLives = 3;
        [SerializeField] private float timeBeforeGameEnds = 180f;

        public void SetDifficulty()
        {
            PlayerPrefs.SetInt("Lives", startingLives);
            PlayerPrefs.SetFloat("Timer", timeBeforeGameEnds);
        }
    }
}