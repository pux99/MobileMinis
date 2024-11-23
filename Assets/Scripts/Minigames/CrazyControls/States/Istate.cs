namespace Minigames.CrazyControls
{
    public interface IPlayerState
    {
        IPlayerState Update(PlayerStateController player);
        IPlayerState FixUpdate(PlayerStateController player);
    }
}
