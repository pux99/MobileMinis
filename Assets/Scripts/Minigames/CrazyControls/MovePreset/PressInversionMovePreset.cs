namespace Minigames.CrazyControls
{
    public class PressInversionMovePreset : IMovePreset
    {
        public void RightUp(CrazyMoveController controller)=>controller.moveRight=true;
        public void RightDown(CrazyMoveController controller)=>controller.moveRight=false;
        public void LeftUp(CrazyMoveController controller)=>controller.moveLeft=true;
        public void LeftDown(CrazyMoveController controller)=>controller.moveLeft=false;
        public void UpUp(CrazyMoveController controller)=>controller.moveUp=true;
        public void UpDown(CrazyMoveController controller)=>controller.moveUp=false;
        public void DownUp(CrazyMoveController controller)=>controller.moveDown=true;
        public void DownDown(CrazyMoveController controller)=>controller.moveDown=false;
    }
}