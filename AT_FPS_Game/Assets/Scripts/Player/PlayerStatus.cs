using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _armour;
    [SerializeField] private WeaponManager _weapons;

    public static bool _hasArmour;

    private int _healthMax;
    private int _armorMax;
    private int _ammoMax;

    public int Health { get => _health; set => _health = value; }
    public int Armour { get => _armour; set => _armour = value; }

    private void Awake()
    {
        _health = 100;
        _armour = 0;

        _hasArmour = false;

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
        if (_armour > 0)
        {
            _hasArmour = true;
        }
        else if (_armour <= 0)
        {
            _armour = 0;
            _hasArmour = false;
        }
        else if (_armour > _armorMax)
        {
            _armour = _armorMax;
        }   
    }

    private void GameOver()
    {
        //Load game over scene here
    }
}
