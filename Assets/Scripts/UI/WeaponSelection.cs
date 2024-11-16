using System.Collections.Generic;
using Minigames.Weapons;
using Player;
using UnityEngine;
using UnityEngine.Serialization;
using Image = UnityEngine.UI.Image;

public class WeaponSelection : MonoBehaviour
{
    [SerializeField] private List<Weapon> weaponList;
    private int _currentWeapon;
    [SerializeField] private Image weaponImage;
    [SerializeField] private WeaponChanger weaponChanger;
    [SerializeField] private SoPlayerStatsAndWeapons.WeaponType weaponType;

    public void ChangeWeapon(int index)
    {
        _currentWeapon += index;
        if (_currentWeapon>=weaponList.Count||index<0)
        {
            if (_currentWeapon < 0)
                _currentWeapon = weaponList.Count - 1;
            else
                _currentWeapon = 0;
        }
        weaponImage.sprite = weaponList[_currentWeapon].WeaponArt;
        weaponChanger.ChangeWeapon(weaponList[_currentWeapon], weaponType);
    }
}
