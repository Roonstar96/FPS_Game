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
                        PlayerStatus.ammo += 30;
                        ShootingScript.damage = 1;
                        break;
                    }
                case Collectables.pistolAmmo:
                    {
                        _type = "Pistol Ammo";
                        PlayerStatus.ammo += 10;
                        break;
                    }
                case Collectables.bigGun:
                    {
                        _type = "Big gun";
                        PlayerStatus.ammo += 24;
                        ShootingScript.damage = 3;
                        break;
                    }
                case Collectables.biggunAmmo:
                    {
                        _type = "Big gun Ammo";
                        PlayerStatus.ammo += 6;
                        break;
                    }
                case Collectables.grenade:
                    {
                        _type = "Grenade";
                        PlayerStatus.ammo += 5;
                        break;
                    }
                case Collectables.grenadeAmmo:
                    {
                        _type = "Grenade Ammo";
                        PlayerStatus.ammo += 1;
                        break;
                    }
                case Collectables.healthWeak:
                    {
                        _type = "Small Health pack";
                        PlayerStatus.health += 25;
                        break;
                    }
                case Collectables.healthStrong:
                    {
                        _type = "Large Health pack";
                        PlayerStatus.health += 50;
                        break;
                    }
                case Collectables.armorWeak:
                    {
                        _type = "Weak armor";
                        PlayerStatus.armor += 25;
                        PlayerStatus.hasArmor = true;
                        break;
                    }
                case Collectables.armorStrong:
                    {
                        _type = "Strong armor";
                        PlayerStatus.armor += 50;
                        PlayerStatus.hasArmor = true;
                        break;
                    }
            }
            Debug.Log("You have collected a " + _type);
            Destroy(gameObject);
        }
    }
}
