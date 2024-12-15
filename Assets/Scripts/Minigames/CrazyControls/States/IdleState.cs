using UnityEngine;

namespace Minigames.CrazyControls.States
{
    public class IdleState:IPlayerState
    {
        public void Enter()
        {
            
        }

        public void Update(PlayerStateController player)
        {
            player.MoveController.SetDirection();
        }

        public void FixUpdate(PlayerStateController player)
        {
            player.MoveController.StopMovement();
        }

        public void Exit()
        {
            
        }

        public override string ToString()
        {
            return "IdleState";
        }
    }
}