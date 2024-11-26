using Core;
using Effects;
using HealthSystem;
using Minigames.Factory;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player.Weapons
{
    [CreateAssetMenu(fileName = "weapon",menuName = "MobileMinis/Weapons")]
    public class Weapon : ScriptableObject
    {
        [FormerlySerializedAs("minigameController")] [SerializeField] private MinigameFactory minigameFactory;
        [SerializeField] private IEffect winEffect;
        [SerializeField] private TargetGiver.Target completingTarget;
        [SerializeField] private IEffect loseEffect;
        [SerializeField] private TargetGiver.Target  losingTarget;
        [SerializeField] private int completingEffectValue;
        [SerializeField] private int losingEffectValue;
        [SerializeField] private Sprite weaponArt;
        

        public MinigameFactory MinigameFactory => minigameFactory;

        public IEffect CompletingEffect => winEffect;

        public UHealth CompletingTarget => ServiceLocator.Instance.GetService<TargetGiver>().GetTarget(completingTarget);

        public IEffect LosingEffect => loseEffect;

        public UHealth LosingTarget => ServiceLocator.Instance.GetService<TargetGiver>().GetTarget(losingTarget);

        public int CompletingEffectValue => completingEffectValue;
        public int LosingEffectValue => losingEffectValue;

        public Sprite WeaponArt => weaponArt;
    }
}
