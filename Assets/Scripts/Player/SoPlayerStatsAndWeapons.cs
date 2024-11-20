using System;
using Core;
using ManagerScripts;
using Minigames.Weapons;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "weapon",menuName = "MobileMinis/Player")]
    public class SoPlayerStatsAndWeapons : MonoBehaviour
    {
        public static SoPlayerStatsAndWeapons Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

        [SerializeField] private int maxHp;
        [SerializeField] private int level;
        [SerializeField] private int exp;
        [SerializeField] private Weapon attackWeapon;
        [SerializeField] private Weapon defenceWeapon;
        [SerializeField] private Weapon supportWeapon;

        public int MaxHp => maxHp;
        public int Level => level;
        public int Exp => exp;

        public Weapon AttackWeapon => attackWeapon;
        public Weapon DefenceWeapon => defenceWeapon;
        public Weapon SupportWeapon => supportWeapon;


        public enum WeaponType
        {
            Attack,
            Defence,
            Support
        }
        public void ChangeWeapon(Weapon newWeapon,WeaponType type)
        {
            switch (type)
            {
                case WeaponType.Attack:
                    attackWeapon = newWeapon;
                    break;
                case WeaponType.Defence:
                    defenceWeapon = newWeapon;
                    break;
                case WeaponType.Support:
                    supportWeapon = newWeapon;
                    break;
                default:
                    break;
            }
        
        }

        public void GainExp(int amount)
        {
            exp += amount;
            CheckLevelUp();
        }

        private void CheckLevelUp()
        {
            if (exp >= Mathf.Pow(level, 2) + 5)
            {
                level++;
                exp = 0;
                ServiceLocator.Instance.GetService<EventManager>().OnLevelUP();
            }
        }
    }
}
