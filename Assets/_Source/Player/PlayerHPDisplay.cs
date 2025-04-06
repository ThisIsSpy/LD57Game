using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerHPDisplay : MonoBehaviour
    {
        [SerializeField] private int startingLives = 3;
        [SerializeField] private GameObject lifeIconPrefab;
        public event Action ZeroLivesLeft;
        private int lives;
        private bool isSetup;
        public int Lives { get { return lives; }
            set
            {
                if (value == lives || !isSetup) return;
                lives = Mathf.Clamp(value, 0, startingLives);
                int livesLeft = lives;
                for (int i = 0; i < lifeIcons.Count; i++)
                {
                    if(livesLeft > 0)
                    {
                        lifeIcons[i].enabled = true;
                        livesLeft--;
                    }
                    else
                    {
                        lifeIcons[i].enabled = false;
                    }
                }
                if (lives <= 0)
                {
                    PlayerPrefs.SetString("GameOver", "Yes");
                    ZeroLivesLeft?.Invoke();
                    //SceneManager.LoadScene("GameOver");
                }
            }
        }
        private List<SpriteRenderer> lifeIcons;

        void Start()
        {
            isSetup = false;
            lifeIcons = new();
            startingLives = PlayerPrefs.GetInt("Lives", startingLives);
            for(int i = 0; i < startingLives; i++)
            {
                GameObject instantiatedIconPrefab = Instantiate(lifeIconPrefab, new(transform.position.x, transform.position.y, -1f), Quaternion.identity, transform);
                lifeIcons.Add(instantiatedIconPrefab.GetComponent<SpriteRenderer>());
            }
            isSetup = true;
            Lives = startingLives;
        }
    }
}