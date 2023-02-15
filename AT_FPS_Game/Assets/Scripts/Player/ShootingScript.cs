using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    [Header("Shooting settings")]
    [SerializeField] private Transform _transform;
    [SerializeField] private int _weaponRange;
    public static int damage;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shooting();
        }
    }

    private void Shooting()
    {
        if (PlayerStatus.ammo > 0)
        {
            Debug.Log("BANG!");
            PlayerStatus.ammo -= 1;

            RaycastHit hit;
            Physics.Raycast(_transform.position, _transform.forward, out hit, _weaponRange);
            Debug.DrawRay(_transform.position, _transform.forward * _weaponRange);

            if (hit.collider.tag == "Environment")
            {
                Debug.Log("You hit someithing!");
            }

            if (hit.collider.tag == "Enemy")
            {
                Debug.Log("You hit an enemy!");
                
                var enemy = hit.collider.gameObject.GetComponent<Enemy_2>()._health;
                DamageEnemy(enemy);
            }
        }
    }

    private void HitEnvironment()
    {

    }

    private void HitDestructableObject()
    {

    }
    private void DamageEnemy(int enemyHealth)
    {
        enemyHealth -= damage;
        //add code to reduce enemy health by damage variable
    }
}
