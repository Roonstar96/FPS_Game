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
    [SerializeField] private Animator _pisAnimator;
    [SerializeField] private Animator _shotAnimator;
    [SerializeField] private Animator _bigAnimator;
    [SerializeField] private Animator _currentAnimator;

    [Header("Weapon Booleans")]
    [SerializeField] private bool _pistolEquiped;
    [SerializeField] private bool _shotgunEquiped;
    [SerializeField] private bool _bigGunEquiped;
    public bool _hasPistol;
    public bool _hasShotgun;
    public bool _hasBigGun;
    public bool _isShooting;
    
    [Header("Shooting settings")]
    [SerializeField] private Transform _transform;
    [SerializeField] private PlayerStatus _playerStat;

    /*[Header("Canvas Objects")]
    [SerializeField] private Canvas _pistol;*/



    public int CurrentAmmo { get => _currentAmmo; set => _currentAmmo = value; }
    public int PistolAmmo { get => _pistolAmmo; set => _pistolAmmo = value; }
    public int ShotgunAmmo { get => _shotgunAmmo; set => _shotgunAmmo = value; }
    public int BigGunAmmo { get => _bigGunAmmo; set => _bigGunAmmo = value; }
    public bool PisEquip { get => _pistolEquiped; set => _pistolEquiped = value; }
    public bool ShotEquip { get => _shotgunEquiped; set => _shotgunEquiped = value; }
    public bool BigEquip { get => _bigGunEquiped; set => _bigGunEquiped = value; }

    private void Awake()
    {
        _hasPistol = false;
        _hasShotgun = false;
        _hasBigGun = false;
        _isShooting = false;
    }
    void Update()
    {
        CurrentWeaponSwitch();
        Shooting();
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
                _currentAnimator = _pisAnimator;
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
                _currentAnimator = _shotAnimator;
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
                _currentAnimator = _bigAnimator;
            }
            else
            {
                return;
            }
        }
    }

    private void Shooting()
    {
        if (_currentAmmo <= 0)
        {
            _currentAmmo = 0;
            return;
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_currentAnimator.GetBool("IsShooting"))
                {
                    return;
                }
                else
                {
                    _currentAnimator.SetBool("IsShooting", true);
                    Debug.Log("BANG!");

                    RaycastHit hit;
                    Physics.Raycast(_transform.position, _transform.forward, out hit, _weaponRange);
                    Debug.DrawRay(_transform.position, _transform.forward * _weaponRange);

                    /*if (hit.collider.tag == "Environment")
                    {
                        //HitEnvironment();
                    }
                    else if (hit.collider.tag == "Destructable")
                    {
                        //HitDestructableObject();
                    }*/
                    if (hit.collider.tag == "Enemy")
                    {
                        HitEnemy(hit.collider.gameObject);
                    }
                    else if (hit.collider.tag == "Boss")
                    {
                        HitBoss(hit.collider.gameObject);
                    }

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
                    Debug.Log("Shooting is over");
                    ResetAttack();
                }
            }
        }
    }

    private void ResetAttack()
    {
        _currentAnimator.SetBool("IsShooting", false);
    }

    private void HitEnemy(GameObject hitObject)
    {
        hitObject.GetComponent<Animator>().SetTrigger("IsHit");
        hitObject.GetComponent<EnemyScript>().EnemyHealth -= _damage;
    }

    private void HitBoss(GameObject hitObject)
    {
        if (hitObject.GetComponent<Boss>().Armour == true)
        {
            hitObject.GetComponent<Boss>().BossArmour -= _damage;
        }
        else
        {
            hitObject.GetComponent<Boss>().BossHealth -= _damage;
        }
    }

    private void HitEnvironment()
    {

    }

    private void HitDestructableObject()
    {

    }
}
