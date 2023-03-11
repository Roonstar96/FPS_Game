using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUICanvas : MonoBehaviour
{
    [Header("Weapon UI")]
    [SerializeField] private GameObject _pistolSlot;
    [SerializeField] private GameObject _shotgunSlot;
    [SerializeField] private GameObject _bigGunSlot;
    [SerializeField] private GameObject _pistolImage;
    [SerializeField] private GameObject _shotgunImage;
    [SerializeField] private GameObject _biggunImage;

    [SerializeField] private WeaponManager _weapMan;

    private void Awake()
    {
        _weapMan = GameObject.Find("Player").GetComponent<WeaponManager>();
        //_weapMan.enabled = true;
    }
    private void Update()
    {
        if (_weapMan.PisEquip)
        {
            _pistolSlot.SetActive(true);
            _shotgunSlot.SetActive(false);
            _bigGunSlot.SetActive(false);

            _pistolImage.SetActive(true);
            _shotgunImage.SetActive(false);
            _biggunImage.SetActive(false);
        }
        if(_weapMan.ShotEquip)
        {
            _pistolSlot.SetActive(false);
            _shotgunSlot.SetActive(true);
            _bigGunSlot.SetActive(false);

            _pistolImage.SetActive(false);
            _shotgunImage.SetActive(true);
            _biggunImage.SetActive(false);
        }
        if (_weapMan.BigEquip)
        {
            _pistolSlot.SetActive(false);
            _shotgunSlot.SetActive(false);
            _bigGunSlot.SetActive(true);

            _pistolImage.SetActive(false);
            _shotgunImage.SetActive(false);
            _biggunImage.SetActive(true);
        }
    }
}
