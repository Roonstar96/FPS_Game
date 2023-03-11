using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

enum Collectables
{
    pistol,
    pistolAmmo,
    shotGun,
    shotgunAmmo,
    bigGun,
    biggunAmmo,
    healthWeak,
    healthStrong,
    armorWeak,
    armorStrong
}
public class CollectablesScript : MonoBehaviour
{
    [SerializeField] private Collectables _collect;
    [SerializeField] private PlayerStatus _playerStat;
    [SerializeField] private WeaponManager _weaponMan;
    //[SerializeField] private Canvas _canvas;

    private string _type;

    private void Awake()
    {
        _playerStat = GameObject.Find("Player").GetComponent<PlayerStatus>();
        _weaponMan = GameObject.Find("Player").GetComponent<WeaponManager>();
        //_canvas = Canvas.;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (_collect)
            {
                case Collectables.pistol:
                    {
                        _type = "Pistol";
                        WeaponManager.PistolAmmo += 45;
                        WeaponManager.HasPistol = true;
                        break;
                    }
                case Collectables.pistolAmmo:
                    {
                        _type = "Pistol Ammo";
                        WeaponManager.PistolAmmo += 15;
                        break;
                    }
                case Collectables.shotGun:
                    {
                        _type = "Shotgun";
                        WeaponManager.ShotgunAmmo += 30;
                        WeaponManager.HasShotgun = true;
                        break;
                    }
                case Collectables.shotgunAmmo:
                    {
                        _type = "Shotgun Ammo";
                        WeaponManager.ShotgunAmmo += 8;
                        break;
                    }
                case Collectables.bigGun:
                    {
                        _type = "Big gun";
                        WeaponManager.BigGunAmmo += 20;
                        WeaponManager.HasBiggun = true;
                        break;
                    }
                case Collectables.biggunAmmo:
                    {
                        _type = "Big gun Ammo";
                        WeaponManager.BigGunAmmo += 5;
                        break;
                    }
                case Collectables.healthWeak:
                    {
                        _type = "Small Health pack";
                        PlayerStatus.Health += 25;
                        break;
                    }
                case Collectables.healthStrong:
                    {
                        _type = "Large Health pack";
                        PlayerStatus.Health += 50;
                        break;
                    }
                case Collectables.armorWeak:
                    {
                        _type = "Weak armor";
                        PlayerStatus.Armour += 25;
                        break;
                    }
                case Collectables.armorStrong:
                    {
                        _type = "Strong armor";
                        PlayerStatus.Armour += 50;
                        break;
                    }
            }
            Debug.Log("You have collected a " + _type);
            Destroy(gameObject);
        }
    }
}
