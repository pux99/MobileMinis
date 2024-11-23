using UnityEngine;

namespace Minigames.CrazyControls.States
{
    public class MoveState:IPlayerState
    {
        public IPlayerState Update(PlayerStateController player)
        {
            player.MoveController.SetDirection();
            return this;
        }

        public IPlayerState FixUpdate(PlayerStateController player)
        {
            if (player.MoveController.Direction == Vector2.zero)
                return player.idleState;
            player.MoveController.Move();
            return this;
        }
    }
}