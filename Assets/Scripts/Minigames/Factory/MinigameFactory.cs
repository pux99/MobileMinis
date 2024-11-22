using Minigames.GeneralUse;
using UnityEngine;

namespace Minigames.Factory
{
    public abstract class MinigameFactory: MonoBehaviour
    {
        public abstract MinigameController CreateMinigame();
    }
}
