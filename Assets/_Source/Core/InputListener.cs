using Player;
using UnityEngine;

namespace Core
{
    public class InputListener : MonoBehaviour
    {
        private PlayerHandler playerHandler;
        private Rigidbody2D playerRB;

        public void Construct(PlayerHandler playerHandler)
        {
            this.playerHandler = playerHandler;
            playerRB = playerHandler.GetComponent<Rigidbody2D>();
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
                playerRB.AddRelativeForce(new(playerHandler.Speed, 0), ForceMode2D.Force);
            }
        }

        private void ListenFronBrakeInput()
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                playerRB.velocity = new(Mathf.Lerp(playerRB.velocity.x, 0, Time.deltaTime), Mathf.Lerp(playerRB.velocity.y, 0, Time.deltaTime));
            }
        }
    }
}
