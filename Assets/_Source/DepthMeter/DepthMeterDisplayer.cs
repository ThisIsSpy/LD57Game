using Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DepthMeter
{
    public class DepthMeterDisplayer : MonoBehaviour
    {
        private TextMeshProUGUI depthMeterText;
        private PlayerHandler playerHandler;
        private float depth;

        public void Construct(PlayerHandler playerHandler)
        {
            this.playerHandler = playerHandler;
        }

        void Start()
        {
            depthMeterText = GetComponent<TextMeshProUGUI>();
        }

        void Update()
        {
            depth = Mathf.Round(Mathf.Abs(playerHandler.transform.position.y - 54));
            depthMeterText.text = $"{depth} m";
            if(depth > PlayerPrefs.GetFloat("Depth", 0)) PlayerPrefs.SetFloat("Depth", depth);
        }
    }
}