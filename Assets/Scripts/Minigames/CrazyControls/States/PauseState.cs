namespace Minigames.CrazyControls
{
    public class PauseState : IPlayerState
    {
        public IPlayerState Update(PlayerStateController player)
        {
            return this;
        }

        public IPlayerState FixUpdate(PlayerStateController player)
        {
            return this;
        }
    }
}