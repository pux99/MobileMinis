using UnityEngine;

namespace Minigames.CrazyControls.States
{
    public class IdleState:IPlayerState
    {
        public IPlayerState Update(PlayerStateController player)
        {
            player.MoveController.SetDirection();
            if (player.MoveController.Direction != Vector2.zero)
                return player.moveState;
            return this;
        }

        public IPlayerState FixUpdate(PlayerStateController player)
        {
            player.MoveController.StopMovement();
            return this;
        }
    }
}