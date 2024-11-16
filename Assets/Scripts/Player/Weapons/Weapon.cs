using Core;
using Effects;
using Minigames.GeneralUse;
using UnityEngine;

namespace Minigames.Weapons
{
    [CreateAssetMenu(fileName = "weapon",menuName = "MobileMinis/Weapons")]
    public class Weapon : ScriptableObject
    {
        [SerializeField] private MinigameController minigameController;
        [SerializeField] private IEffect completingEffect;
        [SerializeField] private IEffect losingEffect;
        [SerializeField] private int completingEffectValue;
        [SerializeField] private int losingEffectValue;
        [SerializeField] private Sprite weaponArt;
        

        public MinigameController MinigameController => minigameController;

        public IEffect CompletingEffect => completingEffect;

        public IEffect LosingEffect => losingEffect;

        public int CompletingEffectValue => completingEffectValue;
        public int LosingEffectValue => losingEffectValue;

        public Sprite WeaponArt => weaponArt;
    }
}
