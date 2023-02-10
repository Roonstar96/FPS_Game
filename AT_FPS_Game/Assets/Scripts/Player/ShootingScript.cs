using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    [Header("Shooting settings")]
    [SerializeField] private Transform _transform;
    [SerializeField] private int _weaponRange;
    public int damage;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shooting();
        }
    }

    private void Shooting()
    {
        if (AmmoUI.ammo > 0)
        {
            Debug.Log("BANG!");
            AmmoUI.ammo -= 1;

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
                hit.collider.gameObject.GetComponent<Enemy>()._health -= 1;
            }
            //raycast stuff here

                //GameObject bullet = (GameObject)Instantiate(projectile, weapon.transform.position, Quaternion.identity);
                //bullet.gameObject.GetComponent<Rigidbody>().velocity = playerRotation.transform.forward * 50;
        }
    }

    private void HitEnvironment()
    {

    }

    private void HitDestructableObject()
    {

    }
    private void DamageEnemy()
    {
        //add code to reduce enemy health by damage variable

    }
}
