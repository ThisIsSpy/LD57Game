using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Score
{
    public class ScoreCounter : MonoBehaviour
    {
        private int score;
        private TextMeshProUGUI scoreCounter;

        public int Score { get { return score; }
            set 
            {
                if (value == score) return;
                score = Mathf.Clamp(value, 0, 999999);
                scoreCounter.text = score.ToString();
                PlayerPrefs.SetInt("Score", score);
            }
        }

        void Start()
        {
            scoreCounter = GetComponent<TextMeshProUGUI>();
            Score = 0;
        }
    }
}
