using Minigames.GeneralUse;
using UnityEngine;

namespace Minigames.Factory
{
    public class FindThePiecesMimigameFactory : MinigameFactory
    {
        [SerializeField] private FindThePiecesMinigameController minigameController;
        public override MinigameController CreateMinigame()
        {
            return Instantiate(minigameController.gameObject).GetComponent<MinigameController>();
            
        }
    }
}