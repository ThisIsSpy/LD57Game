using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Timer
{
    public class GameTimer : MonoBehaviour
    {
        [SerializeField] private float timeBeforeGameEnds = 300f;
        private TextMeshProUGUI timerDisplay;
        private float elapsedSeconds = 0f;
        private float remainingTime;

        void Start()
        {
            timerDisplay = GetComponent<TextMeshProUGUI>();
            remainingTime = timeBeforeGameEnds;
        }

        void Update()
        {
            elapsedSeconds += Time.deltaTime;
            remainingTime = timeBeforeGameEnds - elapsedSeconds;
            float minutes = Mathf.FloorToInt(remainingTime / 60);
            float seconds = Mathf.FloorToInt(remainingTime % 60);
            string minutesStr = minutes.ToString();
            string secondsStr = seconds.ToString();
            if(minutes < 10) minutesStr = "0" + minutesStr;
            if (seconds < 10) secondsStr = "0" + secondsStr;
            timerDisplay.text = $"{minutesStr}:{secondsStr}";
            if (remainingTime <= 0) SceneManager.LoadScene("MainMenu");
        }
    }
}