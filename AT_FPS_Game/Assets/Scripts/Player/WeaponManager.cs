using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponManager : MonoBehaviour
{
    [Header("Weapon Data")]
    [SerializeField] private static int _pistolAmmo;
    [SerializeField] private static int _shotgunAmmo;
    [SerializeField] private static int _bigGunAmmo;
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
    [SerializeField] private static bool _hasPistol;
    [SerializeField] private static bool _hasShotgun;
    [SerializeField] private static bool _hasBigGun;
    public bool _isShooting;

    [Header("Weapon animation times")]
    [SerializeField] private float _pisAnimTime;
    [SerializeField] private float _shotAnimTime;
    [SerializeField] private float _bigAnimTime;
    [SerializeField] private float _currentAnimTime;

    [Header("Shooting settings")]
    [SerializeField] private Transform _transform;
    [SerializeField] private PlayerStatus _playerStat;

    UnityEvent _clickEvent;

    public int CurrentAmmo { get => _currentAmmo; set => _currentAmmo = value; }
    public static int PistolAmmo { get => _pistolAmmo; set => _pistolAmmo = value; }
    public static int ShotgunAmmo { get => _shotgunAmmo; set => _shotgunAmmo = value; }
    public static int BigGunAmmo { get => _bigGunAmmo; set => _bigGunAmmo = value; }
    public static bool HasPistol { get => _hasPistol; set => _hasPistol = value; }
    public static bool HasShotgun { get => _hasShotgun; set => _hasShotgun = value; }
    public static bool HasBiggun { get => _hasBigGun; set => _hasBigGun = value; } 
    public bool PisEquip { get => _pistolEquiped; set => _pistolEquiped = value; }
    public bool ShotEquip { get => _shotgunEquiped; set => _shotgunEquiped = value; }
    public bool BigEquip { get => _bigGunEquiped; set => _bigGunEquiped = value; }

    private void Awake()
    {
        _hasPistol = false;
        _hasShotgun = false;
        _hasBigGun = false;
        _isShooting = false;

        if (_clickEvent == null)
        {
            _clickEvent = new UnityEvent();
        }

        _clickEvent.AddListener(ShootingAnimation);
    }

    private void Update()
    {
        CurrentWeaponSwitch();
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
                _currentAnimTime = _pisAnimTime;
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
                _currentAnimTime = _shotAnimTime;
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
                _currentAnimTime = _bigAnimTime;
            }
        }

        if (!_currentAnimator.GetBool("IsShooting"))
        {
            if (Input.GetMouseButtonDown(0) && _currentAmmo > 0)
            {
                Debug.Log("click");
                _clickEvent.Invoke();
                //Shooting();
            }
            if (_currentAmmo <= 0)
            {
                _currentAmmo = 0;
            }
        }
    }

    public void ShootingAnimation()
    {
        Debug.Log("BANG!");
        _currentAnimator.SetBool("IsShooting", true);

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

        Debug.Log("Casting ray (winston)");
        ShootingRayCast();

        Debug.Log("Time to wait");
        new WaitForSeconds(_currentAnimTime);
    }

    private void ShootingRayCast()
    {
        Debug.Log("Rey palpatine is cast");
        RaycastHit hit;
        Physics.Raycast(_transform.position, _transform.forward, out hit, _weaponRange);
        Debug.DrawRay(_transform.position, _transform.forward * _weaponRange);

        Debug.Log("Now they are done");
        if (hit.collider.tag == "Enemy")
        {
            HitEnemy(hit.collider.gameObject);
        }
        /*if (hit.collider.tag == "Environment")
        {
            //HitEnvironment();
        }
        else if (hit.collider.tag == "Destructable")
        {
            //HitDestructableObject();
        }*/
        StartCoroutine(ResetAttack());
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(_currentAnimTime);
        _currentAnimator.SetBool("IsShooting", false);
        Debug.Log("Shooting is over");
    }

    public void HitEnemy(GameObject hitObject)
    {
        if (hitObject.tag == "Enemy")
        {
            hitObject.GetComponent<Animator>().SetTrigger("IsHit");
            hitObject.GetComponent<EnemyScript>().EnemyHealth -= _damage;
        }

        else if (hitObject.tag == "Boss")
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
    }

    private void HitEnvironment()
    {

    }

    private void HitDestructableObject()
    {

    }
}
