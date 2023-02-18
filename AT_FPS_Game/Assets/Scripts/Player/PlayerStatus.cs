using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _armor;
    [SerializeField] private WeaponManager _weapons;

    public static bool hasArmor;

    private int _healthMax;
    private int _armorMax;
    private int _ammoMax;

    public int Health { get => _health; set => _health = value; }
    public int Armor { get => _armor; set => _armor = value; }

    private void Awake()
    {
        _health = 100;
        _armor = 0;

        hasArmor = false;

        _healthMax = 100;
        _armorMax = 100;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
        UpdateArmor();
    }

    private void UpdateHealth()
    {

        if (_health <= 0)
        {
            GameOver();
        }
        else if (_health > _healthMax)
        {
            _health = _healthMax;
        }
    }

    private void UpdateArmor()
    {
        if (_armor > 0)
        {
            hasArmor = true;
        }
        else if (_armor <= 0)
        {
            _armor = 0;
            hasArmor = false;
        }
        else if (_armor > _armorMax)
        {
            _armor = _armorMax;
        }   
    }

    private void GameOver()
    {
        //Load game over scene here
    }
}
