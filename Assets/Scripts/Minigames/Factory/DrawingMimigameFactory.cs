using Minigames.GeneralUse;
using UnityEngine;

namespace Minigames.Factory
{
    public class DrawingMimigameFactory : MinigameFactory
    {
        [SerializeField] private DrawingMinigameController minigameController;
        public override MinigameController CreateMinigame()
        {
            return Instantiate(minigameController.gameObject).GetComponent<MinigameController>();
            
        }
    }
}