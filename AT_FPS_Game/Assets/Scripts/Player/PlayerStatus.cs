using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static int health;
    public static int armor;
    public static int ammo;

    public static bool hasArmor;

    private int _healthMax;
    private int _armorMax;
    private int _ammoMax;


    private void Awake()
    {
        health = 100;
        armor = 0;
        ammo = 0;

        hasArmor = false;

        _healthMax = 100;
        _armorMax = 100;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
        UpdateArmor();
        UpdateAmmo();
    }

    private void UpdateHealth()
    {
        if (health <= 0)
        {
            //Add game over stuff here
        }
        else if (health > _healthMax)
        {
            health = _healthMax;
        }
    }

    private void UpdateArmor()
    {
        if (hasArmor)
        {
            if (armor <= 0)
            {
                armor = 0;
                hasArmor = false;
            }

            else if (armor > _armorMax)
            {
                armor = _armorMax;
            }
        }
        else
        {
            return;
        }
    }
    private void UpdateAmmo()
    {
        if (ammo <= 0)
        {
            ammo = 0;
        }
        else if (ammo > _ammoMax)
        {
            ammo = _ammoMax;
        }
    }

}
