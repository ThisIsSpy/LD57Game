using DepthMeter;
using Player;
using Score;
using UnityEngine;

namespace Core
{ 
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private PlayerHandler playerHandler;
        [SerializeField] private InputListener inputListener;
        [SerializeField] private ScoreCounter scoreCounter;
        [SerializeField] private PlayerHPDisplay playerHPDisplay;
        [SerializeField] private DepthMeterDisplayer depthMeterDisplayer;

        void Start()
        {
            playerHandler.Construct(scoreCounter, playerHPDisplay);
            inputListener.Construct(playerHandler);
            depthMeterDisplayer.Construct(playerHandler);
        }
    }
}