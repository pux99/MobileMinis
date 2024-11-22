using Minigames.GeneralUse;
using UnityEngine;

namespace Minigames.Factory
{
    public class ChocolateMimigameFactory : MinigameFactory
    {
        [SerializeField] private MeasureTheChocolateMinigameController minigameController;
        public override MinigameController CreateMinigame()
        {
            return Instantiate(minigameController.gameObject).GetComponent<MinigameController>();
            
        }
    }
}