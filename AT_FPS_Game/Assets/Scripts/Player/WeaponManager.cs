using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponManager : MonoBehaviour
{
    [Header("Weapon Data")]
    [SerializeField] private int _pistolAmmo;
    [SerializeField] private int _shotgunAmmo;
    [SerializeField] private int _bigGunAmmo;
    [SerializeField] private int _currentAmmo;
    [SerializeField] private int _weaponRange;
    [SerializeField] private int _damage;

    [Header("Weapon Booleans")]
    [SerializeField] private bool _pistolEquiped;
    [SerializeField] private bool _shotgunEquiped;
    [SerializeField] private bool _bigGunEquiped;
    public bool _hasPistol;
    public bool _hasShotgun;
    public bool _hasBigGun;
    
    [Header("Shooting settings")]
    [SerializeField] private Transform _transform;
    [SerializeField] private PlayerStatus _playerStat;

    public int CurrentAmmo { get => _currentAmmo; set => _currentAmmo = value; }
    public int PistolAmmo { get => _pistolAmmo; set => _pistolAmmo = value; }
    public int ShotgunAmmo { get => _shotgunAmmo; set => _shotgunAmmo = value; }
    public int BigGunAmmo { get => _bigGunAmmo; set => _bigGunAmmo = value; }
    public bool PisEquip { get => _pistolEquiped; set => _pistolEquiped = value; }
    public bool ShotEquip { get => _shotgunEquiped; set => _shotgunEquiped = value; }
    public bool BigEquip { get => _bigGunEquiped; set => _bigGunEquiped = value; }



    private void Awake()
    {

    }
    void Update()
    {
        CurrentWeaponSwitch();

        if (Input.GetMouseButtonDown(0))
        {
            Shooting();
        }
    }

    private void CurrentWeaponSwitch()
    {
        if (Input.GetKeyDown("1"))
        {
            if (_hasPistol)
            {
                _currentAmmo = _pistolAmmo;
                _weaponRange = 50;
                _damage = 2;

                _pistolEquiped = true;
                _shotgunEquiped = false;
                _bigGunEquiped = false;
            }
            else
            {
                return;
            }
        }
        if (Input.GetKeyDown("2"))
        {
            if (_hasShotgun)
            {
                _currentAmmo = _shotgunAmmo;
                _weaponRange = 20;
                _damage = 5;

                _pistolEquiped = false;
                _shotgunEquiped = true;
                _bigGunEquiped = false;
            }
            else
            {
                return;
            }
        }
        if (Input.GetKeyDown("3"))
        {
            if (_hasBigGun)
            {
                _currentAmmo = _bigGunAmmo;
                _weaponRange = 100;
                _damage = 10;

                _pistolEquiped = false;
                _shotgunEquiped = false;
                _bigGunEquiped = true;
            }
            else
            {
                return;
            }
        }
    }

    private void Shooting()
    {
        if (_currentAmmo > 0)
        {
            Debug.Log("BANG!");
            _currentAmmo -= 1;

            if (_pistolEquiped)
            {
                _pistolAmmo -= 1;
            }
            if (_shotgunEquiped)
            {
                _shotgunAmmo -= 1;
            }
            if (_bigGunEquiped)
            {
                _bigGunAmmo -= 1;
            }

            RaycastHit hit;
            Physics.Raycast(_transform.position, _transform.forward, out hit, _weaponRange);
            Debug.DrawRay(_transform.position, _transform.forward * _weaponRange);

            if (hit.collider.tag == "Environment")
            {
                Debug.Log("You hit someithing!");
                //HitEnvironment();
            }
            else if (hit.collider.tag == "Destructable")
            {
                Debug.Log("You hit an exploding barrell!");
                //HitDestructableObject();
            }
            else if (hit.collider.tag == "Enemy")
            {
                Debug.Log("You hit an enemy!");
                
                hit.collider.gameObject.GetComponent<EnemyScript>().EnemyHealth -= _damage;
            }
        }
        else
        {
            _currentAmmo = 0;
            return;
        }
    }

    private void HitEnvironment()
    {

    }

    private void HitDestructableObject()
    {

    }

    /*private void DamageEnemy(int enemyHealth)
    {
        enemyHealth -= _damage;
        //add code to reduce enemy health by damage variable
    }*/
}
