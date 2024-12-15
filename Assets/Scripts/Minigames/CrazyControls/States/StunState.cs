using UnityEngine;

namespace Minigames.CrazyControls.States
{
    public class StunState:IPlayerState
    {
        private float _countDown=1;
        public void Enter()
        {
            _countDown = 1;
        }

        public void Update(PlayerStateController player)
        {
            if (_countDown < 0)
            {
                player.ChangeState(new IdleState());
            }
            _countDown -= Time.deltaTime;
        }

        public void FixUpdate(PlayerStateController player)
        {
            
        }

        public void Exit()
        {
            
        }
        public override string ToString()
        {
            return "StunState";
        }
    }
}