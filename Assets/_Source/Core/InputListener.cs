using Player;
using UnityEngine;

namespace Core
{
    public class InputListener : MonoBehaviour
    {
        private PlayerHandler playerHandler;

        public void Construct(PlayerHandler playerHandler)
        {
            this.playerHandler = playerHandler;
        }

        void Update()
        {
            ListenForMovementInput();
            ListenFronBrakeInput();
        }

        private void ListenForMovementInput()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                playerHandler.PlayerRB.AddRelativeForce(new(playerHandler.Speed, 0), ForceMode2D.Force);
            }
        }

        private void ListenFronBrakeInput()
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                playerHandler.PlayerRB.velocity = new(0.1f, 0.1f);
            }
        }
    }
}
