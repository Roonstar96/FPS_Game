using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BombType
{
    Projectile,
    LandMine
}

public class Projectile_Mine : MonoBehaviour
{
    [Header("Object variables")]
    [SerializeField] private BombType _bombType;
    [SerializeField] private Animator _anim;
    [SerializeField] private BoxCollider _cube;
    [SerializeField] private Vector3 _centre;

    [Header("Explosion variables")]
    [SerializeField] private int _damage;
    [SerializeField] private float _detectRadius;
    [SerializeField] private float _damageRadius;
    [SerializeField] private bool _inDamageRange;
    [SerializeField] private LayerMask _isPlayer;

    private void Awake()
    {
        switch(_bombType)
        {
            case (BombType.Projectile):
                {
                    _detectRadius = 1f;
                    _damageRadius = 3.5f;
                    _damage = 10;
                    break;
                }
            case (BombType.LandMine):
                {
                    _detectRadius = 2f;
                    _damageRadius = 3.5f;
                    _damage = 10;
                    break;
                }
        }
        _cube.size = new Vector3(_detectRadius, 0.5f, _detectRadius + 1);
        _centre = gameObject.transform.position;
        _anim.SetBool("OnHit", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        RaycastHit hit;
        _inDamageRange = Physics.CheckSphere(_centre, _damageRadius, _isPlayer);
        _anim.SetBool("OnHit", true);

        if (_inDamageRange)
        {
            if(PlayerStatus._hasArmour)
            {
                PlayerStatus.Armour -= _damage;
                PlayerStatus.Health -= (_damage / 4);
            }
            else 
            {
                PlayerStatus.Health -= _damage;
            }
        }
        StartCoroutine(DestroyBomb());
    }

    private IEnumerator DestroyBomb()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Destroying Bomb");
        Destroy(gameObject);
    }
}   
