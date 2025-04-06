using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Timer
{
    public class GameTimer : MonoBehaviour
    {
        [SerializeField] private float timeBeforeGameEnds = 180f;
        private TextMeshProUGUI timerDisplay;
        private float elapsedSeconds = 0f;
        private float remainingTime;

        void Start()
        {
            timerDisplay = GetComponent<TextMeshProUGUI>();
            timeBeforeGameEnds = PlayerPrefs.GetFloat("Timer", timeBeforeGameEnds);
            remainingTime = timeBeforeGameEnds;
        }

        void Update()
        {
            elapsedSeconds += Time.deltaTime;
            remainingTime = timeBeforeGameEnds - elapsedSeconds;
            float minutes = Mathf.FloorToInt(remainingTime / 60);
            float seconds = Mathf.FloorToInt(remainingTime % 60);
            if(minutes < 0) minutes = 0;
            if(seconds < 0) seconds = 0;
            string minutesStr = minutes.ToString();
            string secondsStr = seconds.ToString();
            if(minutes < 10) minutesStr = "0" + minutesStr;
            if (seconds < 10) secondsStr = "0" + secondsStr;
            timerDisplay.text = $"{minutesStr}:{secondsStr}";
            if (remainingTime <= 0)
            {
                PlayerPrefs.SetString("GameOver", "No");
                SceneManager.LoadScene("GameOver");
            }
        }
    }
}