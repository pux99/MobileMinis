using UnityEngine;

namespace Minigames.CrazyControls.States
{
    public class ConfuseState:IPlayerState
    {
        public void Enter()
        {
            
        }

        public void Update(PlayerStateController player)
        {
            player.ChangeState(new IdleState());
            Debug.Log("Confusion not implemented");
        }

        public void FixUpdate(PlayerStateController player)
        {
            Debug.Log("Confusion not implemented");
        }

        public void Exit()
        {
            
        }
    }
}