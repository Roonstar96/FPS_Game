using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController cont;
    [SerializeField] private Transform playerRotation;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float sensitivity;

    [SerializeField] private Transform weapon;
    [SerializeField] private GameObject projectile;

    private float mouseX;
    private float moveX;
    private float moveZ;
    private Vector3 moveChar;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        playerRotation.Rotate(Vector3.up * mouseX);
        moveChar = transform.right * moveX + transform.forward * moveZ;
        cont.Move(moveChar * playerSpeed * Time.deltaTime);

        if (!AmmoUI.ammoLeft)
        {
            Debug.Log("No ammo");
        }
        else
        {
            Shooting();
        }
    }
    private void Shooting()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if(MagUI.mag == 0)
            {
                MagUI.mag -= 1;
                Debug.Log("Reloading");
            }

            else
            {
                Debug.Log("BANG!");
                MagUI.mag -= 1;

                GameObject bullet = (GameObject)Instantiate(projectile, weapon.transform.position, Quaternion.identity);
                bullet.gameObject.GetComponent<Rigidbody>().velocity = playerRotation.transform.forward * 50;
            }
        }
    }
}
