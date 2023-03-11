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
    [Header("Sprite Variables")]
    [SerializeField] private Sprite _project;
    [SerializeField] private Sprite _mine;
    [SerializeField] private SpriteRenderer _spriteRend;

    [Header("Object variables")]
    [SerializeField] private BombType _bombType;
    [SerializeField] private Animator _anim;
    [SerializeField] private float _radius;
    [SerializeField] private float _damage;

    private void Awake()
    {
        switch(_bombType)
        {
            case (BombType.Projectile):
                {
                    _spriteRend.sprite = _project;
                    _radius = 3.5f;
                    _damage = 5;
                    break;
                }
            case (BombType.LandMine):
                {
                    _spriteRend.sprite = _mine;
                    _radius = 3.5f;
                    _damage = 5;
                    break;
                }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //trigger animation
        //activate sphere
        //damage anything in the sphere
        //destroy object
    }

}   
