using Effects;
using Minigames.Factory;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player.Weapons
{
    [CreateAssetMenu(fileName = "weapon",menuName = "MobileMinis/Weapons")]
    public class Weapon : ScriptableObject
    {
        [FormerlySerializedAs("minigameController")] [SerializeField] private MinigameFactory minigameFactory;
        [SerializeField] private IEffect completingEffect;
        [SerializeField] private IEffect losingEffect;
        [SerializeField] private int completingEffectValue;
        [SerializeField] private int losingEffectValue;
        [SerializeField] private Sprite weaponArt;
        

        public MinigameFactory MinigameFactory => minigameFactory;

        public IEffect CompletingEffect => completingEffect;

        public IEffect LosingEffect => losingEffect;

        public int CompletingEffectValue => completingEffectValue;
        public int LosingEffectValue => losingEffectValue;

        public Sprite WeaponArt => weaponArt;
    }
}
