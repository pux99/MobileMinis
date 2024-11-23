using UnityEngine;

namespace Minigames.CrazyControls.States
{
    public class StunState:IPlayerState
    {
        private float _countDown=1;
        public IPlayerState Update(PlayerStateController player)
        {
            if (_countDown < 0)
            {
                _countDown = 1;
                return player.idleState;
            }
            _countDown -= Time.deltaTime;
            return this;
        }

        public IPlayerState FixUpdate(PlayerStateController player)
        {
            return this;
        }
    }
}