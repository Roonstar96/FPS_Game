using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUICanvas : MonoBehaviour
{
    [Header("Weapon UI")]
    [SerializeField] private GameObject _pistol;
    [SerializeField] private GameObject _shotgun;
    [SerializeField] private GameObject _bigGun;


    [SerializeField] private WeaponManager _weapMan;

    private void Awake()
    {
        _weapMan = GameObject.Find("Player").GetComponent<WeaponManager>();
    }
    private void Update()
    {
        if (_weapMan.PisEquip)
        {
            _pistol.SetActive(true);
            _shotgun.SetActive(false);
            _bigGun.SetActive(false);
        }
        if(_weapMan.ShotEquip)
        {
            _pistol.SetActive(false);
            _shotgun.SetActive(true);
            _bigGun.SetActive(false);
        }
        if (_weapMan.BigEquip)
        {
            _pistol.SetActive(false);
            _shotgun.SetActive(false);
            _bigGun.SetActive(true);
        }
    }
}
