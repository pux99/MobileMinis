namespace Minigames.CrazyControls.MovePreset
{
    public class InvertedMovePreset : IMovePreset
    {
        public void RightUp(CrazyMoveController controller)=>controller.moveLeft=false;
        public void RightDown(CrazyMoveController controller)=>controller.moveLeft=true;
        public void LeftUp(CrazyMoveController controller)=>controller.moveRight=false;
        public void LeftDown(CrazyMoveController controller)=>controller.moveRight=true;
        public void UpUp(CrazyMoveController controller)=>controller.moveDown=false;
        public void UpDown(CrazyMoveController controller)=>controller.moveDown=true;
        public void DownUp(CrazyMoveController controller)=>controller.moveUp=false;
        public void DownDown(CrazyMoveController controller)=>controller.moveUp=true;
    }
}