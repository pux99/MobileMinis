using Minigames.GeneralUse;
using UnityEngine;

namespace Minigames.Factory
{
    public class PlaceThePiecesMimigameFactory : MinigameFactory
    {
        [SerializeField] private PlaceThePiecesMinigameController minigameController;
        public override MinigameController CreateMinigame()
        {
            return Instantiate(minigameController.gameObject).GetComponent<MinigameController>();
            
        }
    }
}