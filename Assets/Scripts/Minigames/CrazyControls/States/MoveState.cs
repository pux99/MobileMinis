
namespace Minigames.CrazyControls.States
{
    public class MoveState:IPlayerState
    {
        public void Enter()
        {
            
        }

        public void Update(PlayerStateController player)
        {
            player.MoveController.SetDirection();
        }

        public void FixUpdate(PlayerStateController player)
        {
            player.MoveController.Move();
        }

        public void Exit()
        {
            
        }
        public override string ToString()
        {
            return "MoveState";
        }
    }
}