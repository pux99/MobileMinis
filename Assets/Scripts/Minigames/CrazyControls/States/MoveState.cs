
namespace Minigames.CrazyControls.States
{
    public class MoveState:IPlayerState
    {
        public void Enter(PlayerStateController player)
        {
            player.MoveController.SetDirection();
        }

        public void Update(PlayerStateController player)
        {
            player.MoveController.SetDirection();
        }

        public void FixUpdate(PlayerStateController player)
        {
            player.MoveController.Move();
        }

        public void Exit(PlayerStateController player)
        {
            
        }
        public override string ToString()
        {
            return "MoveState";
        }
    }
}