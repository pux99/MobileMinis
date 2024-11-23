using UnityEngine;

namespace Minigames.CrazyControls
{
    public interface IMovePreset
    {
        public void RightUp(CrazyMoveController controller);
        public void RightDown(CrazyMoveController controller);
        public void LeftUp(CrazyMoveController controller);
        public void LeftDown(CrazyMoveController controller);
        public void UpUp(CrazyMoveController controller);
        public void UpDown(CrazyMoveController controller);
        public void DownUp(CrazyMoveController controller);
        public void DownDown(CrazyMoveController controller);

    }
}
