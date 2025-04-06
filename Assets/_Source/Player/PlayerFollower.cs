using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerFollower : MonoBehaviour
    {
        private PlayerHandler playerHandler;
        void Start()
        {
            playerHandler = FindObjectOfType<PlayerHandler>();
        }

        void Update()
        {
            transform.position = new(playerHandler.transform.position.x, playerHandler.transform.position.y, -10f);
        }
    }
}