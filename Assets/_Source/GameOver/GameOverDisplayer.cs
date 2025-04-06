using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GameOver
{
    public class GameOverDisplayer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI gameOverText;
        [SerializeField] private TextMeshProUGUI gameOverDescriptionText;
        [SerializeField] private TextMeshProUGUI finalScoreText;
        [SerializeField] private TextMeshProUGUI furthestDepthText;

        void Start()
        {
            string gameOutcome = PlayerPrefs.GetString("GameOver", "No");
            if(gameOutcome == "No")
            {
                gameOverText.text = "Congratulations!";
                gameOverDescriptionText.text = "Congratulations! You have completed the game! Good job! Fantastic! Amazing! Wonderful! Unbelievable! Absolutely Mindblowing! Your parents and/or SO would be proud!";
            }
            else
            {
                gameOverText.text = "Game Over";
                gameOverDescriptionText.text = "What a shame. It seems that you have died. How unfortunate. Sad. Not cool. Your parents and/or SO would be disappointed";
            }
            finalScoreText.text = PlayerPrefs.GetInt("Score", 999999).ToString();
            furthestDepthText.text = $"{PlayerPrefs.GetFloat("Depth", 100)} m";
        }
    }
}