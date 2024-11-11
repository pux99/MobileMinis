using Effects;
using Minigames.GeneralUse;
using UnityEngine;

namespace Minigames.Weapons
{
    [CreateAssetMenu(fileName = "weapon",menuName = "")]
    public class Weapon : ScriptableObject
    {
        [SerializeField] private MinigameController minigameController;
        [SerializeField] private Effect completingEffect;
        [SerializeField] private Effect losingEffect;
        [SerializeField] private int completingEffectValue;
        [SerializeField] private int losingEffectValue;
        

        public MinigameController MinigameController => minigameController;

        public Effect CompletingEffect => completingEffect;

        public Effect LosingEffect => losingEffect;

        public int CompletingEffectValue => completingEffectValue;
        public int LosingEffectValue => losingEffectValue;
    }
}
