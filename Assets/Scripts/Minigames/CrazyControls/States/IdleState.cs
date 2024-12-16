using UnityEngine;

namespace Minigames.CrazyControls.States
{
    public class IdleState:IPlayerState
    {
        public void Enter(PlayerStateController player)
        {
            player.MoveController.StopMovement();
        }

        public void Update(PlayerStateController player)
        {
            player.MoveController.SetDirection();
        }

        public void FixUpdate(PlayerStateController player)
        {
            player.MoveController.StopMovement();
        }

        public void Exit(PlayerStateController player)
        {
            
        }

        public override string ToString()
        {
            return "IdleState";
        }
    }
}