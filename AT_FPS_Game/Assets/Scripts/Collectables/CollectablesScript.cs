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
    armorStrong,
    keycard
}
public class CollectablesScript : MonoBehaviour
{
    [SerializeField] private Collectables _collect;
    //public static bool _access;
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
                        AmmoUI.ammoLeft = true;
                        MagUI.mag += 10;
                        break;
                    }
                case Collectables.pistolAmmo:
                    {
                        _type = "Pistol Ammo";
                        AmmoUI.ammo += 10;
                        AmmoUI.ammoLeft = true;
                        break;
                    }
                case Collectables.bigGun:
                    {
                        _type = "Big gun";
                        AmmoUI.ammo += 24;
                        AmmoUI.ammoLeft = true;
                        MagUI.mag += 6;
                        break;
                    }
                case Collectables.biggunAmmo:
                    {
                        _type = "Big gun Ammo";
                        AmmoUI.ammo += 6;
                        AmmoUI.ammoLeft = true;
                        break;
                    }
                case Collectables.grenade:
                    {
                        _type = "Grenade";
                        AmmoUI.ammo += 5;
                        AmmoUI.ammoLeft = true;
                        MagUI.mag += 1;
                        break;
                    }
                case Collectables.grenadeAmmo:
                    {
                        _type = "Grenade Ammo";
                        AmmoUI.ammo += 1;
                        AmmoUI.ammoLeft = true;
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
                case Collectables.keycard:
                    {
                        _type = "Key card";
                        //_access = true;
                        break;
                    }
            }
            Debug.Log("You have collected a " + _type);
            StartCoroutine(Collected());
        }
    }
    private IEnumerator Collected()
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
