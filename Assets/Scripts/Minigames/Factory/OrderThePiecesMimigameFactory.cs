using Minigames.GeneralUse;
using UnityEngine;

namespace Minigames.Factory
{
    public class OrderThePiecesMimigameFactory : MinigameFactory
    {
        [SerializeField] private OrderThePiecesMinigameController minigameController;
        public override MinigameController CreateMinigame()
        {
            return Instantiate(minigameController.gameObject).GetComponent<MinigameController>();
            
        }
    }
}