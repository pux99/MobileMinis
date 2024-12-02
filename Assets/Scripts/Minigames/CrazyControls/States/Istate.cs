namespace Minigames.CrazyControls.States
{
    public interface IPlayerState
    {
        void Enter();
        void Update(PlayerStateController player);
        void FixUpdate(PlayerStateController player);
        void Exit();
    }
}
