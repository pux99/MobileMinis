using UnityEngine;

namespace Minigames.CrazyControls.States
{
    public class ConfuseState:IPlayerState
    {
        public IPlayerState Update(PlayerStateController player)
        {
            Debug.Log("Confusion not implemented");
            return player.idleState;
        }

        public IPlayerState FixUpdate(PlayerStateController player)
        {
            Debug.Log("Confusion not implemented");
            return player.idleState;
        }
    }
}