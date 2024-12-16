namespace Minigames.CrazyControls.States
{
    public interface IPlayerState
    {
        void Enter(PlayerStateController player);
        void Update(PlayerStateController player);
        void FixUpdate(PlayerStateController player);
        void Exit(PlayerStateController player);
    }
}
