using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Collectables
{
    pistol,
    pistolAmmo,
    bigGun,
    biggunAmmo,
    grenade,
    grenadeAmmo,
    healthWeak,
    healthStrong,
    armorWeak,
    armorStrong
}
public class CollectablesScript : MonoBehaviour
{
    [SerializeField] private Collectables _collect;
    private string _type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (_collect)
            {
                case Collectables.pistol:
                    {
                        _type = "Pistol";
                        AmmoUI.ammo += 30;
                        ShootingScript.damage = 1;
                        break;
                    }
                case Collectables.pistolAmmo:
                    {
                        _type = "Pistol Ammo";
                        AmmoUI.ammo += 10;
                        break;
                    }
                case Collectables.bigGun:
                    {
                        _type = "Big gun";
                        AmmoUI.ammo += 24;
                        ShootingScript.damage = 3;
                        break;
                    }
                case Collectables.biggunAmmo:
                    {
                        _type = "Big gun Ammo";
                        AmmoUI.ammo += 6;
                        break;
                    }
                case Collectables.grenade:
                    {
                        _type = "Grenade";
                        AmmoUI.ammo += 5;
                        break;
                    }
                case Collectables.grenadeAmmo:
                    {
                        _type = "Grenade Ammo";
                        AmmoUI.ammo += 1;
                        break;
                    }
                case Collectables.healthWeak:
                    {
                        _type = "Small Health pack";
                        HealthUI.health += 25;
                        break;
                    }
                case Collectables.healthStrong:
                    {
                        _type = "Large Health pack";
                        HealthUI.health += 50;
                        break;
                    }
                case Collectables.armorWeak:
                    {
                        _type = "Weak armor";
                        ArmorUI.armor += 25;
                        break;
                    }
                case Collectables.armorStrong:
                    {
                        _type = "Strong armor";
                        ArmorUI.armor += 50;
                        break;
                    }
            }
            Debug.Log("You have collected a " + _type);
            Destroy(gameObject);
        }
    }
}
