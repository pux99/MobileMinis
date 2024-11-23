using Minigames.CrazyControls;
using Minigames.GeneralUse;
using UnityEngine;

namespace Minigames.Factory
{
    public class CrazyMinigameFactory : MinigameFactory
    {
        [SerializeField] private CrazyControlsMinigameController minigameController;
        public override MinigameController CreateMinigame()
        {
            return Instantiate(minigameController.gameObject).GetComponent<MinigameController>();
            
        }
    }
}