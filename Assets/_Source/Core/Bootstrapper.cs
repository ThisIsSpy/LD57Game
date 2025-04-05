using Player;
using UnityEngine;

namespace Core
{ 
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private PlayerHandler playerHandler;
        [SerializeField] private InputListener inputListener;

        void Start()
        {
            inputListener.Construct(playerHandler);
        }
    }
}