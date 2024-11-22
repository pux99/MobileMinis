using Minigames.GeneralUse;
using UnityEngine;

namespace Minigames.Factory
{
    public class MazeMimigameFactory : MinigameFactory
    {
        [SerializeField] private MazeMinigameController minigameController;
        public override MinigameController CreateMinigame()
        {
            return Instantiate(minigameController.gameObject).GetComponent<MinigameController>();
        }
    }
}